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
    public partial class SpecializationForm : Form, ISpecialization, IEditable<ISpecialization>
    {
        public ActionType Type { get; set; }
        public ISpecialization Entity { get; set; }
        private MainMDIForm main;

        public SpecializationForm(MainMDIForm main, ActionType type, ISpecialization specialization = null)
        {
            if (main == null || (type == ActionType.Edit && specialization == null))
                throw new ArgumentNullException();

            InitializeComponent();

            this.Type = type;
            this.Entity = specialization;
            this.main = main;

            if (Type == ActionType.Edit)
            {
                this.Text = $"[{specialization.Name}] - Редактирование";
                btnApply.Text = "Применить";
            }
        }
        
        #region ISpecialization implementation.
        public int Faculty
        {
            get
            {
                return Int32.Parse(cmbFaculty.SelectedValue.ToString());
            }
            set
            {
                cmbFaculty.SelectedValue = value;
            }
        }
        private int id;
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

        string ISpecialization.Name
        {
            get
            {
                return tbFieldOfStudy.Text.Replace("\n"," ").Trim();
            }
            set
            {
                tbFieldOfStudy.Text = value;
            }
        }

        public int LevelOfEducation
        {
            get
            {
                return cmbLevelOfEducation.SelectedIndex + 1;
            }
            set
            {
                cmbLevelOfEducation.SelectedIndex = value - 1;
            }
        }
        #endregion

        #region IEditable implementation.
        public void Insert()
        {
            try
            {
                this.Insert<SpecializationForm, ISpecialization>(main.DBGetter, "specialization", "ID");

                var msg = $"Профиль подготовки добавлен в базу.\nНаименование профиля: {((ISpecialization)this).Name}";

                MessageBox.Show(msg, "Добавление профиля подготовки", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            if (!ValidateFields() || Type != ActionType.Edit)
                return;

            try
            {
                // Поля не учитываются в таблице в БД.
                var nameValue = Entity.GetPropertiesDifference<ISpecialization>(this, "ID");

                if (nameValue.Count == 0)
                    return;

                // Обновляем данные
                main.DBGetter.Update("specialization", ID, nameValue);

                MessageBox.Show($"Информация о профиле подготовки обновлена\nНаименование профиля: {((ISpecialization)this).Name}", "Редактирование информации", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetDefaultValues() => this.SetPropertiesValue(Entity, "");

        public bool ValidateFields() => Extentions.ValidateFields(this, errorProvider);
        #endregion
        
        #region Validating events.
        private void RequiredComboBox_Validating(object sender, CancelEventArgs e)
        {
            var cmb = sender as ComboBox;
            Extentions.RequiredComboBoxValidating(cmb, errorProvider);
        }

        private void RequiredTextBox_Validating(object sender, CancelEventArgs e)
        {
            var tb = sender as Control;
            Extentions.RequiredTextBoxValidating(tb, errorProvider);
        }
        #endregion 

        private void SpecializationForm_Load(object sender, EventArgs e)
        {
            cmbFaculty.BindComboboxData(main.Faculties);
            if(Type == ActionType.Edit) SetDefaultValues();
        }

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
