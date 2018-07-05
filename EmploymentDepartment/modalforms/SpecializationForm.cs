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
    public partial class SpecializationForm : BaseSpecializationForm, ISpecialization
    {
        public SpecializationForm(MainMDIForm main, ActionType type, ISpecialization specialization = null) : base(type, specialization)
        {
            InitializeComponent();

            this.main = main;

            if (Type == ActionType.Edit)
            {
                btnApply.Text = "Применить";
            }
        }

        public SpecializationForm(MainMDIForm main, ISpecialization specialization) : base(main, specialization)
        {
            InitializeComponent();
        }

        #region ISpecialization implementation.
        public new int Faculty
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
        public new int LevelOfEducation
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
        
        #region Validating events.
        private new void RequiredComboBox_Validating(object sender, CancelEventArgs e)
        {
            var cmb = sender as ComboBox;
            Extentions.RequiredComboBoxValidating(cmb, errorProvider);
        }

        private new void RequiredTextBox_Validating(object sender, CancelEventArgs e)
        {
            var tb = sender as Control;
            Extentions.RequiredTextBoxValidating(tb, errorProvider);
        }
        #endregion

        protected override ErrorProvider GetErrorProvider()
        {
            return errorProvider;
        }

        private void SpecializationForm_Load(object sender, EventArgs e)
        {
            cmbFaculty.BindComboboxData(main.Faculties);            
            mainPanel.Enabled = btnApply.Visible = Type != ActionType.View;
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
