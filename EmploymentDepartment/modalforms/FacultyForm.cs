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
    public partial class FacultyForm : EditModal, IFaculty, IEditable<IFaculty>
    {
        public ActionType Type { get; set; }
        public IFaculty Entity { get; set; }
        private MainMDIForm main;

        public FacultyForm(MainMDIForm main, ActionType type, IFaculty faculty = null)
        {
            if (main == null || (type == ActionType.Edit && faculty == null))
                throw new ArgumentNullException();

            InitializeComponent();

            this.Type = type;
            this.Entity = faculty;
            this.main = main;

            if (Type == ActionType.Edit)
            {
                this.Text = $"[{faculty.Name}] - Редактирование";
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

        string IFaculty.Name
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
               // this.Save(Entity, main.DBGetter, "faculty", "ID");

                MessageBox.Show($"Информация о факультете\nНаименование факультета: {((IFaculty)this).Name}", "Редактирование информации", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                //this.Insert<FacultyForm, IFaculty>(main.DBGetter, "faculty", "ID");

                var msg = $"Факультет добавлен в базу.\nНаименование факультета: {((IFaculty)this).Name}";

                MessageBox.Show(msg, "Добавление факультета", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            main.UpdateFaculties();
            main.UpdateSpecializations();
        }
    }
}
