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
    public partial class CompanyForm : BaseCompanyForm, ICompany
    {
        // Модальное окно для просмотра информации.
        public CompanyForm(MainMDIForm mainForm, ICompany company) : base(mainForm, company)
        {
            InitializeComponent();
        }

        public CompanyForm(ActionType type, ICompany entity, IDataListView<ICompany> viewContext) : base(type, entity, viewContext)
        {
            InitializeComponent();
        }

        public CompanyForm(ActionType type, ICompany company = null) :base(type,company)
        {
            InitializeComponent();
        }

        protected override ErrorProvider GetErrorProvider()
        {
            return errorProvider;
        }

        private void tbSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extentions.SurnameKeyPressValidator(e.KeyChar);
        }

        private void CompanyFrom_SizeChanged(object sender, EventArgs e)
        {
            mainPanel.AutoScroll = this.Size.Height < 575;
        }

        private void CompanyFrom_Load(object sender, EventArgs e)
        {
            if (Type == ActionType.View)
            {
                mainPanel.DisableControls();
            }
        }

        // Валидация обязательного текстового поля.
        private void RequiredTB_Validating(object sender, CancelEventArgs e) => this.RequiredTextBox_Validating(sender, e);
        
        #region Поведение кнопок "..." (Редакторовать)
        protected void lableEdit_Click(object sender, EventArgs e) => this.lblEdit_Click(sender, e);

        protected void lableEdit_MouseHover(object sender, EventArgs e) => this.lblEdit_MouseHover(sender, e);

        protected void lableEdit_MouseLeave(object sender, EventArgs e) => this.lblEdit_MouseLeave(sender, e);
        #endregion

        #region ICompany implementation.

        public new string Address
        {
            get
            {
                return tbAddress.Text;
            }

            set
            {
                tbAddress.Text = value;
            }
        }

        public new string City
        {
            get
            {
                return tbCity.Text;
            }

            set
            {
                tbCity.Text = value;
            }
        }

        public new string CompanyNumber
        {
            get
            {
                return tbCompanyNumber.Text;
            }

            set
            {
                tbCompanyNumber.Text = value;
            }
        }

        public new string ContactName
        {
            get
            {
                return tbContactName.Text;
            }

            set
            {
                tbContactName.Text = value;
            }
        }

        public new string ContactPatronymic
        {
            get
            {
                return string.IsNullOrEmpty(tbContactPatronymic.Text) ? null : tbContactPatronymic.Text;
            }

            set
            {
                tbContactPatronymic.Text = value;
            }
        }

        public new string ContactSurname
        {
            get
            {
                return tbContactSurname.Text;
            }

            set
            {
                tbContactSurname.Text = value;
            }
        }

        public new string DirectorName
        {
            get
            {
                return tbName.Text;
            }

            set
            {
                tbName.Text = value;
            }
        }

        public new string DirectorPatronymic
        {
            get
            {
                return string.IsNullOrEmpty(tbPatronymic.Text) ? null : tbPatronymic.Text;
            }

            set
            {
                tbPatronymic.Text = value;
            }
        }

        public new string DirectorSurname
        {
            get
            {
                return tbSurname.Text;
            }

            set
            {
                tbSurname.Text = value;
            }
        }

        public new string District
        {
            get
            {
                return string.IsNullOrEmpty(tbDistrict.Text) ? null : tbDistrict.Text;
            }

            set
            {
                tbDistrict.Text = value;
            }
        }

        public new string Email
        {
            get
            {
                return tbEmail.Text;
            }

            set
            {
                tbEmail.Text = value;
            }
        }

        public new string NameOfStateDepartment
        {
            get
            {
                return string.IsNullOrEmpty(tbNameOfStateDepartment.Text) ? null : tbNameOfStateDepartment.Text;
            }

            set
            {
                tbNameOfStateDepartment.Text = value;
            }
        }

        public new string Note
        {
            get
            {
                return string.IsNullOrEmpty(tbNote.Text) ? null : tbNote.Text;
            }

            set
            {
                tbNote.Text = value;
            }
        }

        public new string Phone
        {
            get
            {
                return tbPhone.Text;
            }

            set
            {
                tbPhone.Text = value;
            }
        }

        string ICompany.Region
        {
            get
            {
                return string.IsNullOrEmpty(tbRegion.Text) ? null : tbRegion.Text;
            }

            set
            {
                tbRegion.Text = value;
            }
        }

        string ICompany.Name
        {
            get
            {
                return tbOrganizationName.Text;
            }
            set
            {
                tbOrganizationName.Text = value;
            }

        }

        #endregion

    }
}
