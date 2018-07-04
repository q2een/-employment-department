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
    public partial class PreferentialCategoryForm : EditModal, IEditable<IPreferentialCategory>, IPreferentialCategory
    {
        public ActionType Type { get; set; }
        public IPreferentialCategory Entity { get; set; }
        private MainMDIForm main;

        public PreferentialCategoryForm(MainMDIForm main, ActionType type, IPreferentialCategory preferentialCategory = null)
        {
            if (main == null || (type == ActionType.Edit && preferentialCategory == null))
                throw new ArgumentNullException();

            InitializeComponent();

            this.Type = type;
            this.Entity = preferentialCategory;
            this.main = main;

            if (Type == ActionType.Edit)
            {
                this.Text = $"Редактирование льготной категории";
                btnApply.Text = "Применить";
            }

            if (Type == ActionType.Edit) SetDefaultValues();
        }

        #region IFaculty implementation
        int id;
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        string IPreferentialCategory.Name
        {
            get
            {
                return tbEdit.Text.Replace("\n", " ");
            }

            set
            {
                tbEdit.Text = value;
            }
        }
        #endregion

        public void Save()
        {
            try
            {
                //this.Save(Entity, main.DBGetter, "preferentialcategory", "ID");

                MessageBox.Show($"Информация о льготной категории изменена", "Редактирование информации", MessageBoxButtons.OK, MessageBoxIcon.Information);

                main.UpdatePreferentialCategories();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Insert()
        {
            try
            {
                //this.Insert<PreferentialCategoryForm, IPreferentialCategory>(main.DBGetter, "preferentialcategory", "ID");

                var msg = $"Льготная категоря добавлена в базу";

                MessageBox.Show(msg, "Добавление льготной категории", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetDefaultValues() => this.SetPropertiesValue(Entity, "");

        public bool ValidateFields() => Extentions.ValidateFields(this, errorProvider);

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Type == ActionType.Add)
                this.Insert();

            if (Type == ActionType.Edit)
                this.Save();

            main.UpdatePreferentialCategories();
        }
    }
}
