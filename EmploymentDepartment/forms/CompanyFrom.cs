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
    public partial class CompanyFrom : Form, ICompany, IEditable<ICompany>
    {
        #region ICompany implementation.

        public string Address
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

        public string City
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

        public string CompanyNumber
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

        public string ContactName
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

        public string ContactPatronymic
        {
            get
            {
                return tbContactPatronymic.Text;
            }

            set
            {
                tbContactPatronymic.Text = value;
            }
        }

        public string ContactSurname
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

        public string DirectorName
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

        public string DirectorPatronymic
        {
            get
            {
                return tbPatronymic.Text;
            }

            set
            {
                tbPatronymic.Text = value;
            }
        }

        public string DirectorSurname
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

        public string District
        {
            get
            {
                return tbDistrict.Text;
            }

            set
            {
                tbDistrict.Text = value;
            }
        }

        public string Email
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

        public string NameOfStateDepartment
        {
            get
            {
                return tbNameOfStateDepartment.Text;
            }

            set
            {
                tbNameOfStateDepartment.Text = value;
            }
        }

        public string Note
        {
            get
            {
                return tbNote.Text;
            }

            set
            {
                tbNote.Text = value;
            }
        }

        public string Phone
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
                return tbRegion.Text;
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

        public ICompany Entity { get; private set; }
        public ActionType Type { get; private set; }
        private MainMDIForm main;

        // Модальное окно для просмотра информации.
        public CompanyFrom(MainMDIForm mainForm, ICompany company) : this(ActionType.View, company)
        {
            this.main = mainForm;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        public CompanyFrom(ActionType type, ICompany company = null)
        {
            if (type == ActionType.Edit && company == null)
                throw new ArgumentNullException();

            InitializeComponent();

            this.Entity = type == ActionType.Add ? null : company;
            this.Type = type;

            SetFormText(company);
        }

        private void CompanyFrom_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;

            SetDefaultValues();
            mainPanel.Enabled = Type != ActionType.View;
        }

        // Устанавливает заголовок окна.
        private void SetFormText(ICompany company)
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о предприятии [{company.Name}]";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление предприятия в базу";
                    break;
                case ActionType.View:
                    this.Text = $"[{company.Name}] - Просмотр информации о предприятии";
                    break;
            }
        }

        private void tbSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extentions.SurnameKeyPressValidator(e.KeyChar);
        }

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

        #region IEditable interfaces implemantation.

        public bool ValidateFields() => Extentions.ValidateFields(this, errorProvider);

        public void SetDefaultValues() => this.SetPropertiesValue<ICompany>(Entity, "");

        public void Save()
        {
            var msg = $"Информация о предприятии «{(this as ICompany).Name}» обновлена";

            if (this.UpdateFormEntityInDataBase<CompanyFrom, ICompany>(main.DBGetter, msg, "ID"))
                SetFormText(this);
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Insert()
        {
            var msg = $"Предприятие «{(this as ICompany).Name}»\nдобавлено в базу";

            if (this.UpdateFormEntityInDataBase<CompanyFrom, ICompany>(main.DBGetter, msg, "ID"))
                SetFormText(this);
        }

        #endregion

        private void RequiredTextBox_Validating(object sender, CancelEventArgs e)
        {
            var tb = sender as Control;
            Extentions.RequiredTextBoxValidating(tb, errorProvider);
        }

        private void CompanyFrom_SizeChanged(object sender, EventArgs e)
        {
            mainPanel.AutoScroll = this.Size.Height < 575;
        }
    }
}
