using EmploymentDepartment.BL;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public partial class PreferentialCategoryForm : BasePreferentialCategory, IPreferentialCategory
    {
        public PreferentialCategoryForm(MainMDIForm main, ActionType type, IPreferentialCategory faculty = null) : base(type, faculty)
        {
            InitializeComponent();

            this.main = main;

            if (Type == ActionType.Edit)
            {
                btnApply.Text = "Применить";
            }
        }

        public PreferentialCategoryForm(MainMDIForm main, IPreferentialCategory faculty) : base(main, faculty)
        {
            InitializeComponent();
        }

        #region IPreferentialCategory implementation.

        string IPreferentialCategory.Name
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

        private void PreferentialCategoryForm_Load(object sender, EventArgs e)
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
