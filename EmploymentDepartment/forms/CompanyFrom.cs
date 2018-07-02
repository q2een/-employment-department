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
    public partial class CompanyFrom : Form, ICompany, IEditable
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

        public new string Region
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

        public new string Name
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

        public ICompany Company { get; private set; }
        public ActionType Type { get; private set; }
        private MainMDIForm main;

        public CompanyFrom(ActionType type, ICompany company = null)
        {
            if (type == ActionType.Edit && company == null)
                throw new ArgumentNullException();

            InitializeComponent();

            this.Company = company;
            this.Type = type;

            // Устанавливаем заголовок окна.
            SetFormText(company.Name);
        }

        private void CompanyFrom_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm))
                throw new Exception();

            this.main = this.MdiParent as MainMDIForm;
            
            InitFiels();
        }
        
        private void InitFiels()
        {
            this.SetPropertiesValue<ICompany>(Company, ""); ;
        }
        
        private void SetFormText(string companyName)
        {
            if (Type == ActionType.Edit)
                this.Text = $"Редактирование информации о предприятии [{companyName}]";
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

        public void SetDefaultValues()
        {
            InitFiels();
        }

        public void Save()
        {
            if (!ValidateFields() || Type != ActionType.Edit)
                return;

            try
            {
                // Поля не учитываются в таблице в БД.
                var nameValue = Company.GetPropertiesDifference<ICompany>(this, "ID");

                // Обновляем данные
                main.DBGetter.Update("Company", ID, nameValue);

                MessageBox.Show($"Информация о предприятии {this.Name} обновлена", "Редактирование информации", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Присваеваем свойству новые исходные значения.
                Company = ((ICompany)this).GetInstance<ICompany, Company>();

                // Устанавливаем заголовок окна.
                SetFormText(this.Name);

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
            if (!ValidateFields() || Type != ActionType.Add)
                return;

            try
            {
                // Поля не учитываются в таблице в БД.
                var nameValue = this.GetPropertiesNameValuePair<ICompany>(true, "ID");

                // Добавляем запись в БД.
                main.DBGetter.Insert("Company", nameValue);

                MessageBox.Show($"Предприятие «{Name}»\nдобавлено в базу", "Добавление предприятия", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
