using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public partial class StudentForm : Form, IStudent, IEditable<IStudent>
    {
        #region IStudent Implementation.
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

        public string ApplicationFormNumber
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

        string IStudent.Name
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

        public string Surname
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

        public string Patronymic
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

        public DateTime DOB
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

        public int Gender
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

        public bool MaritalStatus
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

        public int YearOfGraduation
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

        public EducationLevelType LevelOfEducation
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

        public int FieldOfStudy
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

        public string StudyGroup
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

        public decimal Rating
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

        public int? PreferentialCategory
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

        public bool SelfEmployment
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

        public string City
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

        public new string Region
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

        public string District
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

        public string Address
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

        public string RegCity
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

        public string RegRegion
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

        public string RegDistrict
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

        public string RegAddress
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

        public string Phone
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

        public string Email
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
        
        public IStudent Entity { get; private set; }
        public ActionType Type { get; private set; }
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

        private MainMDIForm main;

        public StudentForm(ActionType type, IStudent student = null)
        {
            if (type == ActionType.Edit && student == null)
                throw new ArgumentNullException();

            InitializeComponent();
           
            this.Entity = type == ActionType.Add ? null : student;
            this.Type = type;

            SetFormText(student);          
        }

        // Модальное окно для просмотра информации.
        public StudentForm(MainMDIForm mainForm, IStudent student) : this(ActionType.View, student)
        {
            this.main = mainForm;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        // Обработка события загрузки формы.
        private void StudentForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;

            SetDefaultValues();
            mainPanel.Enabled = Type != ActionType.View;
        }
        
        // Обработка события изменения размера формы.
        private void StudentForm_SizeChanged(object sender, EventArgs e)
        {
            mainPanel.AutoScroll = this.Size.Height < 650;
            LinkPreferentialCategory = LinkPreferentialCategory;
        }
        
        // Устанавливает заголовок окна.
        private void SetFormText(IStudent student)
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о студенте [{student.Surname} {student.Name} {student.Patronymic}]";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление анкеты студента";
                    break;
                case ActionType.View:
                    this.Text = $"{student.Surname} {student.Name} {student.Patronymic} - Просмотр анкеты студента";
                    break;
            }                
        }
        
        #region Поведение кнопок "..." (Редакторовать)
        private void lblEdit_Click(object sender, EventArgs e)
        {
            new EditModalForm((sender as Label).Tag as TextBox).ShowDialog();
        }

        private void lblEdit_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.RoyalBlue;
        }

        private void lblEdit_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = SystemColors.ControlText;
        }
        #endregion

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
            var faculties = main.EntGetter.GetFaculties((EducationLevelType)(cmbLevelOfEducation.SelectedIndex + 1));
            cmbFaculty.BindComboboxData(faculties);
            var specializations = main.Specializations.Where(i => (int)i.LevelOfEducation == cmbLevelOfEducation.SelectedIndex + 1 && i.Faculty == (int)cmbFaculty.SelectedValue).ToList();
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

            var sp = main.Specializations.Where(i => (int)i.LevelOfEducation == cmbLevelOfEducation.SelectedIndex + 1 && i.Faculty == id).ToList();
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

        // Валидация выпадающих списков обязательных к выбору элемента.
        private void RequiredComboBox_Validating(object sender, CancelEventArgs e)
        {
            var cmb = sender as ComboBox;
            Extentions.RequiredComboBoxValidating(cmb, errorProvider);
        }

        // Валидация текстовых полей обязательных к заполнению.
        private void RequiredTextBox_Validating(object sender, CancelEventArgs e)
        {
            var tb = sender as Control;
            Extentions.RequiredTextBoxValidating(tb, errorProvider);
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

        #region IEditable interfaces implemantation.

        // Валидация полей на форме.
        public bool ValidateFields()
        {
            // Установка одинаковых адресов в полях.
            if (cbAddressesAreEquals.Checked)
                SetRegAddressValues(false);

            return Extentions.ValidateFields(this, errorProvider);
        }

        // Задает полям исходные значения.
        public void SetDefaultValues()
        {
            this.SetPropertiesValue<IStudent>(Entity, "");
            if (tbRegCity.Text == tbCity.Text && !string.IsNullOrEmpty(tbRegCity.Text) &&
                tbRegRegion.Text == tbRegion.Text && !string.IsNullOrEmpty(tbRegRegion.Text) &&
                tbRegDistrict.Text == tbDistrict.Text && !string.IsNullOrEmpty(tbRegDistrict.Text) &&
                tbRegAddress.Text == tbAddress.Text && !string.IsNullOrEmpty(tbRegAddress.Text))
                cbAddressesAreEquals.Checked = true;
        }

        // Сохраняет внесенные изменения в БД.
        public void Save()
        {
            /*if (!ValidateFields() || Type != ActionType.Edit)
                return;

            try
            {
                // Поля не учитываются в таблице в БД.
                var nameValue = Entity.GetPropertiesDifference<IStudent>(this, "ID", "LevelOfEducation", "Faculty");

                if (nameValue.Count == 0)
                    return;

                // Обновляем данные
                main.DBGetter.Update("Student", ID, nameValue);

                MessageBox.Show($"Информация о студенте обновлена\nФИО студента: {Surname} {((IStudent)this).Name} {Patronymic}", "Редактирование информации", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Присваеваем свойству новые исходные значения.
                Entity = ((IStudent)this).GetInstance<IStudent, Student>();
                SetFormText(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            var msg = $"Информация о студенте обновлена\nФИО студента: {Surname} {((IStudent)this).Name} {Patronymic}";
            if (this.UpdateFormEntityInDataBase<StudentForm,IStudent>(main.DBGetter, msg, "ID", "LevelOfEducation", "Faculty"))
                SetFormText(this); 
        }

        // TODO
        // Удаление анкеты пользователя из БД.
        public void Remove()
        {
            throw new NotImplementedException();
        }

        // Добавляет данные в БД.
        public void Insert()
        {
            /*if (!ValidateFields() || Type != ActionType.Add)
                return;

            try
            {
                // Поля не учитываются в таблице в БД.
                var nameValue = this.GetPropertiesNameValuePair<IStudent>(true, "ID", "LevelOfEducation", "Faculty");

                // Добавляем запись в БД.
                main.DBGetter.Insert("Student", nameValue);

                MessageBox.Show($"Студент {Surname} {Name} {Patronymic}\nдобавлен в базу", "Добавление студента", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     */

            var msg = $"Студент {Surname} {((IStudent)this).Name} {Patronymic}\nдобавлен в базу";
            this.InsertFormEntityToDataBase<StudentForm, IStudent>(main.DBGetter, msg, "ID", "LevelOfEducation", "Faculty");
                
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

        private void StudentForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (Type != ActionType.View)
                return;

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
