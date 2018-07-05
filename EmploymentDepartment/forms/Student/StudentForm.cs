using EmploymentDepartment.BL;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public partial class StudentForm : BaseStudentForm, IStudent
    {
        #region IStudent Implementation.

        public new string ApplicationFormNumber
        {
            get
            {
                return String.IsNullOrEmpty(tbApplicationFormNumber.Text) ? null : tbApplicationFormNumber.Text;
            }
            set
            {
                tbApplicationFormNumber.Text = value;
            }
        }

        string  IStudent.Name
        {
            get
            {
                return String.IsNullOrEmpty(tbName.Text) ? null : tbName.Text; 
            }
            set
            {
                tbName.Text = value;
            }
        }

        public new string Surname
        {
            get
            {
                return String.IsNullOrEmpty(tbSurname.Text) ? null : tbSurname.Text;
            }
            set
            {
                tbSurname.Text = value;
            }
        }

        public new string Patronymic
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

        public new DateTime DOB
        {
            get
            {
                return DateTime.Parse(tbDOB.Text);
            }
            set
            {
                tbDOB.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public new int Gender
        {
            get
            {
                return cmbGender.SelectedIndex+1;
            }
            set
            {
                cmbGender.SelectedIndex = value - 1;
            }
        }

        public new bool MaritalStatus
        {
            get
            {
                return cmbMaritalStatus.SelectedIndex == 1; 
            }
            set
            {
                cmbMaritalStatus.SelectedIndex = value ? 1 : 0;
            }
        }

        public new int YearOfGraduation
        {
            get
            {
                return Int32.Parse(tbYearOfGraduation.Text);
            }
            set
            {
                tbYearOfGraduation.Text = value.ToString();
            }
        }

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

        public new EducationLevelType LevelOfEducation
        {
            get
            {
                return (EducationLevelType)(cmbLevelOfEducation.SelectedIndex + 1);
            }
            set
            {
                cmbLevelOfEducation.SelectedIndex = (int)value - 1;
            }
        }

        public new int FieldOfStudy
        {
            get
            {
                return Int32.Parse(cmbFieldOfStudy.SelectedValue.ToString());
            }
            set
            {
                cmbFieldOfStudy.SelectedValue = value; 
            }
        }

        public new string StudyGroup
        {
            get
            {
                return String.IsNullOrEmpty(tbStudyGroup.Text) ? null : tbStudyGroup.Text;
            }
            set
            {
                tbStudyGroup.Text = value;
            }
        }

        public new decimal Rating
        {
            get
            {
                return Decimal.Parse(tbRating.Text);
            }
            set
            {
                tbRating.Text = value.ToString();
            }
        }

        public new int? PreferentialCategory
        {
            get
            {                
                return LinkPreferentialCategory?.ID;
            }
            set
            {
                LinkPreferentialCategory = main.PreferentialCategories.FirstOrDefault(i => i.ID == value);
            }
        }

        public new bool SelfEmployment
        {
            get
            {
                return cbSelfEmployment.Checked;
            }
            set
            {
                cbSelfEmployment.Checked = value;
            }
        }

        public new string City
        {
            get
            {
                return String.IsNullOrEmpty(tbCity.Text) ? null : tbCity.Text;
            }
            set
            {
                tbCity.Text = value;
            }
        }

        string IStudent.Region
        {
            get
            {
                return String.IsNullOrEmpty(tbRegion.Text) ? null : tbRegion.Text;
            }
            set
            {
                tbRegion.Text = value;
            }
        }

        public new string District
        {
            get
            {
                return String.IsNullOrEmpty(tbDistrict.Text) ? null : tbDistrict.Text;
            }
            set
            {
                tbDistrict.Text = value;
            }
        }

        public new string Address
        {
            get
            {
                return String.IsNullOrEmpty(tbAddress.Text) ? null : tbAddress.Text;
            }
            set
            {
                tbAddress.Text = value;
            }
        }

        public new string RegCity
        {
            get
            {
                return String.IsNullOrEmpty(tbRegCity.Text) ? null : tbRegCity.Text;
            }
            set
            {
                tbRegCity.Text = value;
            }
        }

        public new string RegRegion
        {
            get
            {
                return String.IsNullOrEmpty(tbRegRegion.Text) ? null : tbRegRegion.Text;
            }
            set
            {
                tbRegRegion.Text = value;
            }
        }

        public new string RegDistrict
        {
            get
            {
                return String.IsNullOrEmpty(tbRegDistrict.Text) ? null : tbRegDistrict.Text;
            }
            set
            {
                tbRegDistrict.Text = value;
            }
        }

        public new string RegAddress
        {
            get
            {
                return String.IsNullOrEmpty(tbRegAddress.Text) ? null : tbRegAddress.Text;
            }
            set
            {
                tbRegAddress.Text = value;
            }
        }

        public new string Phone
        {
            get
            {
                return String.IsNullOrEmpty(tbPhone.Text) ? null : tbPhone.Text;
            }
            set
            {
                tbPhone.Text = value;
            }
        }

        public new string Email
        {
            get
            {
                return String.IsNullOrEmpty(tbEmail.Text) ? null : tbEmail.Text;
            }
            set
            {
                tbEmail.Text = value;
            }
        }

        #endregion
        
        public IPreferentialCategory LinkPreferentialCategory
        {
            get
            {
                return linkPreferentialCategory.Tag as IPreferentialCategory;
            }
            set
            {
                linkPreferentialCategory.Tag = value;

                linkClear.Visible = Type != ActionType.View;

                string text = Type == ActionType.View ? "Отсутствует" : "Выбрать студента ...";
                text = value == null ? text : $"{value.Name}";
                int width = this.Width - lblPreferentialCategory.Width - 70;

                linkPreferentialCategory.Text = Extentions.ShortenString(text, width, linkPreferentialCategory.Font);
            }
        }

        public StudentForm(ActionType type, IStudent student = null):base(type, student)
        {
            InitializeComponent();        
        }

        // Модальное окно для просмотра информации.
        public StudentForm(MainMDIForm mainForm, IStudent student) : base(mainForm, student)
        {
            InitializeComponent();
        }
        
        // Обработка события изменения размера формы.
        private void StudentForm_SizeChanged(object sender, EventArgs e)
        {
            mainPanel.AutoScroll = this.Size.Height < 650;
            LinkPreferentialCategory = LinkPreferentialCategory;
        }
        
        protected override ErrorProvider GetErrorProvider()
        {
            return errorProvider;
        }
            
        #region Поведение элементов управления.

        // Установка одинаковых значений адресов.
        private void SetRegAddressValues(bool isEmpty)
        {
            tbRegCity.Text = isEmpty ? "" : tbCity.Text;
            tbRegRegion.Text = isEmpty ? "" : tbRegion.Text;
            tbRegDistrict.Text = isEmpty ? "" : tbDistrict.Text;
            tbRegAddress.Text = isEmpty ? "" : tbAddress.Text;
            errorProvider.SetError(tbRegCity, "");
            errorProvider.SetError(tbRegAddress, "");
        }

        // Устанавливает варианты выпадающего списка "Семейное положение" в зависимости от выбранного пола.
        private void SetMartialStatusByGender(GenderType type)
        {
            cmbMaritalStatus.Items.Clear();
            switch(type)
            {
                case GenderType.Female:
                    cmbMaritalStatus.Items.Add("Не замужем");
                    cmbMaritalStatus.Items.Add("Замужем");
                    break;
                default:
                    cmbMaritalStatus.Items.Add("Не женат");
                    cmbMaritalStatus.Items.Add("Женат");                     
                    break;
            }

            cmbMaritalStatus.SelectedIndex = 0;
        }

        // Смена значений в выдающем списке "Пол". Обработка события.
        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMartialStatusByGender((GenderType)cmbGender.SelectedIndex + 1);
            cmbMaritalStatus.Enabled = true;
        }

        // Смена значений в выдающем списке "Уровень образования". Обработка события.
        private void cmbLevelOfEducation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var faculties = main.EntGetter.GetFaculties((EducationLevelType)(cmbLevelOfEducation.SelectedIndex + 1)).Select(i=> i as IFaculty).ToList();
            cmbFaculty.BindComboboxData(faculties);
            var specializations = main.Specializations.Where(i => (int)i.LevelOfEducation == cmbLevelOfEducation.SelectedIndex + 1 && i.Faculty == (int)cmbFaculty.SelectedValue).Select(i => i as ISpecialization).ToList();
            cmbFieldOfStudy.BindComboboxData(specializations);

            cmbFaculty.Enabled = faculties.Count() > 0;
            cmbFieldOfStudy.Enabled = specializations.Count() > 0;
        }

        // Смена значений в выдающем списке "Факультет". Обработка события.
        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = cmbFaculty.SelectedValue;
            int id;

            if (!Int32.TryParse(value + "", out id))
                return;

            var sp = main.Specializations.Where(i => (int)i.LevelOfEducation == cmbLevelOfEducation.SelectedIndex + 1 && i.Faculty == id).Select( z => z as ISpecialization).ToList();
            cmbFieldOfStudy.BindComboboxData(sp);
        }

        // Валидация нажатия клавиши при вводе значений ФИО. Разрешены А-яA-z-. Обработка события.
        private void tbSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extentions.SurnameKeyPressValidator(e.KeyChar);
        }

        // Смена состояния флага "Адреса совпадают ...". Обработка события.
        private void cbAddressesAreEquals_CheckedChanged(object sender, EventArgs e)
        {
            gbRegAddress.Enabled = !cbAddressesAreEquals.Checked;

            if (cbAddressesAreEquals.Checked)
            {
                SetRegAddressValues(true);
            }
            else
            {
                tbRegCity.Focus();
            }
        }

        #endregion

        #region Validations

        // Валидация выпадающих списков обязательных к выбору элемента при изменении выбранного индекса.
        private void RequiredCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Extentions.RequiredComboBoxValidating(sender as ComboBox, errorProvider);            
        }

        // Валидация текстового поля "Год рождения".
        private void tbDOB_Validating(object sender, CancelEventArgs e)
        {
            DateTime dt;
            string error;

            if (!DateTime.TryParse(tbDOB.Text, out dt))
                error = "Укажите корректную дату рождения";
            else error = dt.Year < 1900 ? "Укажите корректную дату рождения" : "";

            errorProvider.SetError(tbDOB, error);
        }

        // Валадация текстового поля "Год окончания ВУЗа".
        private void tbYearOfGraduation_Validating(object sender, CancelEventArgs e)
        {
            int year;
           
            if (!Int32.TryParse(tbYearOfGraduation.Text, out year))
                errorProvider.SetError(tbYearOfGraduation, "Укажите корректный год окончания университета");
            else
                if(year < 2018)
                errorProvider.SetError(tbYearOfGraduation, "Год окончания университета должен быть больше 2018");
            else
                errorProvider.SetError(tbYearOfGraduation, "");
        }
        #endregion

        #region IEditable interfac implemantation.

        // Валидация полей на форме.
        public override bool ValidateFields()
        {
            // Установка одинаковых адресов в полях.
            if (cbAddressesAreEquals.Checked)
                SetRegAddressValues(false);

            return Extentions.ValidateFields(this, errorProvider);
        }

        // Задает полям исходные значения.
        public override void SetDefaultValues()
        {
            this.SetPropertiesValue<IStudent>(Entity, "");
            if (tbRegCity.Text == tbCity.Text && !string.IsNullOrEmpty(tbRegCity.Text) &&
                tbRegRegion.Text == tbRegion.Text && !string.IsNullOrEmpty(tbRegRegion.Text) &&
                tbRegDistrict.Text == tbDistrict.Text && !string.IsNullOrEmpty(tbRegDistrict.Text) &&
                tbRegAddress.Text == tbAddress.Text && !string.IsNullOrEmpty(tbRegAddress.Text))
                cbAddressesAreEquals.Checked = true;
        }
        #endregion

        #region Обработка событий для выбора льготной категирии.

        // Нажатие на элемент управление для выбора льготной категории. Обработка события.
        private void linkPreferentialCategory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new PreferentialCategoryPicker(main.PreferentialCategories,this);
            form.ShowDialog(this);
        }

        // Нажатие на элемент управления "Очистить". Обработка события.
        private void linkClear_Click(object sender, EventArgs e)
        {
            LinkPreferentialCategory = null;
        }

        // Обработка события нажития клавиши при активном элементе для выбора льготной категории. Обработка события.
        private void linkPreferentialCategory_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Открыть окно на space.
            if (e.KeyCode == Keys.Space)
            {
                linkPreferentialCategory_LinkClicked(sender, null);
                return;
            }

            // Очистить на backspace and delete.
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                linkClear_Click(sender, null);
                return;
            }
        }
        #endregion

        private void StudentForm_Load(object sender, EventArgs e)
        {
            mainPanel.Enabled = Type != ActionType.View;
        }

        private void RequiredTB_Validating(object sender, CancelEventArgs e) => this.RequiredTextBox_Validating(sender, e);

        private void RequireCMB_Validating(object sender, CancelEventArgs e) => RequiredComboBox_Validating(sender, e);
    }
}
