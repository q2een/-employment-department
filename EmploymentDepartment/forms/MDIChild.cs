using EmploymentDepartment.BL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет класс, содержащий методы, предназначенные для дочерних MDI окон для редактирования, удаления, просмотра и добавления данных.
    /// </summary>
    public partial class MDIChild<T> : Form, IEditable<T>, IIdentifiable where T : class, IIdentifiable
    {
        /// <summary>
        /// Возвращает или задает экземпляр класса главного MDI окна. 
        /// </summary>
        protected MainMDIForm Main { get; set; }

        /// <summary>
        /// Возвращает массив наименований свойств, которые игнорируются для заполнения.
        /// </summary>
        protected virtual string[] IngnoreProperties { get; }

        /// <summary>
        /// Возвращает или задает объект сущности.
        /// </summary>
        public T Entity { get; set; }

        /// <summary>
        /// Возвращает или задает тип окна.
        /// </summary>
        public ActionType Type { get; set; }

        /// <summary>
        /// Возвращает или задает контекст из которого был открыт данный экземпляр окна.
        /// </summary>
        protected IDataListView<T> ViewContext { get; set; }

        public MDIChild()
        {

        }

        /// <summary>
        /// Предоставляет класс, содержащий методы, предназначенные для дочерних MDI окон для редактирования, удаления, просмотра и добавления данных.
        /// </summary>
        /// <param name="type">Тип действия</param>
        /// <param name="entity">Объект сущности</param>
        public MDIChild(ActionType type, T entity = null)
        {
            if (type == ActionType.Edit && entity == null)
                throw new ArgumentNullException();

            InitializeComponent();

            this.Entity = type == ActionType.Add ? null : entity;
            this.Type = type;
        }

        /// <summary>
        /// Предоставляет класс, содержащий методы, предназначенные для дочерних MDI окон для редактирования, удаления, просмотра и добавления данных.
        /// </summary>
        /// <param name="type">Тип действия</param>
        /// <param name="entity">Объект сущности</param>
        /// <param name="viewContext">Контекст из которого был открыт данный экземпляр окна.</param>
        public MDIChild(ActionType type, T entity, IDataListView<T> viewContext) : this(type, entity)
        {
            this.ViewContext = viewContext;
        }
 
        /// <summary>
        /// Предоставляет класс, содержащий методы, предназначенные для дочерних MDI окон для редактирования, удаления, просмотра и добавления данных.
        /// Модальное окно для просмотра информации.
        /// </summary>
        /// <param name="mainForm">Экземпляр класса главного MDI окна</param>
        /// <param name="entity">Объект сущности</param>
        public MDIChild(MainMDIForm mainForm, T entity) : this(ActionType.View, entity)
        {
            this.Main = mainForm;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        // Обработка события загрузки формы.
        private void MDIChild_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && Main == null)
                throw new ArgumentNullException();
            
            this.Main = Main == null ? this.MdiParent as MainMDIForm : Main;

            this.KeyPreview = Type == ActionType.View;

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
            if (Type == ActionType.View || Type == ActionType.Add)
                return;

            var nameValue = this.Entity.GetPropertiesDifference<T>(this as T, IngnoreProperties);

            if (nameValue.Count() == 0)
                return;

            this.Activate();

            DialogResult dialogResult = MessageBox.Show("Закрытие окна приведет к потере несохраненных изменений. Сохранить изменения перед закрытием?", "Закрытие окна", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    if (!this.Save())
                        e.Cancel = true;
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// Задает название окна.
        /// </summary>
        protected virtual void SetFormText()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает интерфейс пользователя, указывающий на наличие ошибки.
        /// </summary>
        /// <returns>Интерфейс пользователя, указывающий на наличие ошибки</returns>
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

        /// <summary>
        /// Задает полям исходные значения.
        /// </summary>
        public virtual void SetDefaultValues()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сохраняет внесенные изменения.
        /// </summary>
        public virtual bool Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удаление данных.
        /// </summary>
        public virtual void Remove()
        {
            if(!Entity.RemoveEntity(Main?.Entities))
                return;

            if (ViewContext != null)
                ViewContext.RemoveDataTableRow(Entity);

            this.Close();
        }

        /// <summary>
        /// Добавляет данные.
        /// </summary>
        public virtual void AddNewItem()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Валидация полей.
        /// </summary>
        /// <returns>Истина если поля валидны</returns>
        public virtual bool ValidateFields()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
