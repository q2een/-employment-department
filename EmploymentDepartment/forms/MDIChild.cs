using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    // Добавить отключение элементов управления в контроле, кроме ссылок. для простотра информации.

    public partial class MDIChild<T> : Form, IEditable<T>, IIdentifiable where T : class, IIdentifiable
    {
        protected MainMDIForm main { get; set; }
        public T Entity { get; set; }
        public ActionType Type { get; set; }
        protected IDataListView<T> ViewContext { get; set; }

        public MDIChild()
        {

        }

        public MDIChild(ActionType type, T entity = null)
        {
            if (type == ActionType.Edit && entity == null)
                throw new ArgumentNullException();

            InitializeComponent();

            this.Entity = type == ActionType.Add ? null : entity;
            this.Type = type;
        }

        public MDIChild(ActionType type, T entity, IDataListView<T> viewContext) : this(type, entity)
        {
            this.ViewContext = viewContext;
        }

        // Модальное окно для просмотра информации.
        public MDIChild(MainMDIForm mainForm, T entity) : this(ActionType.View, entity)
        {
            this.main = mainForm;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        // Обработка события загрузки формы.
        private void MDIChild_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();
            
            this.main = main == null ? this.MdiParent as MainMDIForm : main;

            SetDefaultValues();
            SetFormText();
        }

        // Обработка события нажатия на клавишу при модальном окне.
        private void MDIChild_KeyDown(object sender, KeyEventArgs e)
        {
            if (Type != ActionType.View)
                return;

            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        // Обработка события закрытия окна.
        private void MDIChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Type == ActionType.View)
                return;

            var nameValue = this.Entity.GetPropertiesDifference<T>(this as T, "");

            if (nameValue.Count() == 0)
                return;

            this.Activate();

            DialogResult dialogResult = MessageBox.Show("Закрытие окна приведет к потере несохраненных изменений. Сохранить изменения перед закрытием?", "Закрытие окна", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    this.Save();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        protected virtual void SetFormText()
        {
            throw new NotImplementedException();
        }

        protected virtual ErrorProvider GetErrorProvider()
        {
            throw new NotImplementedException();
        }

        #region Поведение кнопок "..." (Редакторовать)
        protected void lblEdit_Click(object sender, EventArgs e)
        {
            new EditModalForm((sender as Label).Tag as TextBox).ShowDialog();
        }

        protected void lblEdit_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.RoyalBlue;
        }

        protected void lblEdit_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = SystemColors.ControlText;
        }
        #endregion

        #region Validation

        // Валидация выпадающих списков обязательных к выбору элемента.
        protected void RequiredComboBox_Validating(object sender, CancelEventArgs e)
        {
            var cmb = sender as ComboBox;
            Extentions.RequiredComboBoxValidating(cmb, GetErrorProvider());
        }

        // Валидация текстовых полей обязательных к заполнению.
        protected void RequiredTextBox_Validating(object sender, CancelEventArgs e)
        {
            var tb = sender as Control;
            Extentions.RequiredTextBoxValidating(tb, GetErrorProvider());
        }

        #endregion

        #region IIdentifiable 

        public int ID { get; set; }

        string IIdentifiable.Name { get; set; }

        #endregion

        #region IEditable

        public virtual void SetDefaultValues()
        {
            throw new NotImplementedException();
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public virtual void Remove()
        {
            Entity.RemoveEntity(main?.Entities);

            if (ViewContext != null)
                ViewContext.RemoveDataTableRow(Entity);
        }

        public virtual void AddNewItem()
        {
            throw new NotImplementedException();
        }

        public virtual bool ValidateFields()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
