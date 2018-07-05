using EmploymentDepartment.BL;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public partial class FacultyForm : BaseFacultyForm, IFaculty
    {
        public FacultyForm(MainMDIForm main, ActionType type, IFaculty faculty = null) : base(type, faculty)
        {
            InitializeComponent();

            this.main = main;

            if (Type == ActionType.Edit)
            {
                btnApply.Text = "Применить";
            }
        }

        public FacultyForm(MainMDIForm main, IFaculty faculty) : base(main, faculty)
        {
            InitializeComponent();
        }

        #region IFaculty implementation.

        string IFaculty.Name
        {
            get
            {
                return tbEdit.Text.Replace("\n", " ").Trim();
            }
            set
            {
                tbEdit.Text = value;
            }
        }
        #endregion

        #region Validating events.

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

        private void FacultyForm_Load(object sender, EventArgs e)
        {
            mainPanel.Enabled = controlPanel.Visible = Type != ActionType.View;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Type == ActionType.Add)
                this.Insert();

            if (Type == ActionType.Edit)
                this.Save();
        }
    }
}
