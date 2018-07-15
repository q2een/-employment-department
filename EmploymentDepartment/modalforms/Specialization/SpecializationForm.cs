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
        public SpecializationForm(MainMDIForm main, ActionType type, ISpecialization entity, IDataListView<ISpecialization> viewContext) : base(type, entity, viewContext)
        {
            InitializeComponent();

            this.main = main;
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
        public new long LevelOfEducation
        {
            get
            {
                return cmbLevelOfEducation.SelectedIndex + 1;
            }
            set
            {
                cmbLevelOfEducation.SelectedIndex = (int)value - 1;
            }
        }

        public new string FacultyName
        {
            get
            {
                return cmbFaculty.Text;
            }
        }

        public new string LevelOfEducationName
        {
            get
            {
                return cmbLevelOfEducation.Text;
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
            if (Type == ActionType.Add)
                cmbLevelOfEducation.SelectedIndex = 0;

            if (Type == ActionType.Edit) 
                btnApply.Text = "Применить";

            mainPanel.Enabled = btnApply.Visible = Type != ActionType.View;
        }

        public override void SetDefaultValues()
        {
            cmbFaculty.BindComboboxData(main.Entities.GetEntities<Faculty>().Select(i => i as IFaculty).ToList());
            this.SetPropertiesValue<ISpecialization>(Entity, "FacultyName", "LevelOfEducationName");
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Type == ActionType.Add)
                this.AddNewItem();

            if (Type == ActionType.Edit)
                this.Save();
        }
    }
}
