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
    public partial class StudentForm : Form, IStudent, IEditable
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

        public new string Name
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
                var id = main.PreferentialCategories.FirstOrDefault(i => i.Name.Equals(linkPreferentialCategory.Tag));
                return id?.ID;
            }
            set
            {
                PreferentialCategory pc = null;
                if (value != null)
                    pc = main.PreferentialCategories.FirstOrDefault(i => i.ID == value);

                linkPreferentialCategory.Tag = pc?.Name ?? "Выбрать льготную категорию...";
                linkPreferentialCategory.Text = ShortenString();
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


        public IStudent Student { get; private set; }
        public ActionType Type { get; private set; }
        private MainMDIForm main;

        public StudentForm(ActionType type, Student student = null)
        {
            if (type == ActionType.Edit && student == null)
                throw new ArgumentNullException();

            InitializeComponent();
           
            this.Student = student;
            this.Type = type;

            SetFormText(student);
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm))
                throw new Exception();

            this.main = this.MdiParent as MainMDIForm;
            InitFiels();
        }
        
        private void StudentForm_SizeChanged(object sender, EventArgs e)
        {
            mainPanel.AutoScroll = this.Size.Height < 650;
            linkPreferentialCategory.Text = ShortenString();
        }
        
        private void SetFormText(IStudent student)
        {
            if (Type == ActionType.Edit)
                this.Text = $"Редактирование информации о студенте [{student.Surname} {student.Name} {student.Patronymic}]";
        }
        
        private void InitFiels()
        {
            this.SetPropertiesValue<IStudent>(Student, "");
            if (tbRegCity.Text == tbCity.Text && tbRegRegion.Text == tbRegion.Text && tbRegDistrict.Text == tbDistrict.Text && tbRegAddress.Text == tbAddress.Text)
                cbAddressesAreEquals.Checked = true;
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

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMartialStatusByGender((GenderType)cmbGender.SelectedIndex + 1);
            cmbMaritalStatus.Enabled = true;
        }

        private void BindComboboxData<T>(ComboBox cmb, List<T> data) where T : IIdentifiable
        {
            var value = cmb.SelectedValue;
            int id;

            cmb.DataSource = null;
            cmb.Items.Clear();
            cmb.DataSource = data;
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "ID";

            // Выделить элемент (если он существует), который был активен до изменений.
            if (!Int32.TryParse(value + "", out id))
                return;

            var elem = data.FirstOrDefault(i => i.ID == id);

            if (elem == null)
                return;

            cmb.SelectedIndex = data.IndexOf(elem);
        }

        private void cmbLevelOfEducation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var faculties = main.EntGetter.GetFaculties((EducationLevelType)(cmbLevelOfEducation.SelectedIndex + 1));
            BindComboboxData(cmbFaculty, faculties);
            var specializations = main.Specializations.Where(i => (int)i.LevelOfEducation == cmbLevelOfEducation.SelectedIndex + 1 && i.Faculty == (int)cmbFaculty.SelectedValue).ToList();
            BindComboboxData(cmbFieldOfStudy, specializations);

            cmbFaculty.Enabled = faculties.Count() > 0;
            cmbFieldOfStudy.Enabled = specializations.Count() > 0;
        }

        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = cmbFaculty.SelectedValue;
            int id;

            if (!Int32.TryParse(value + "", out id))
                return;

            var sp = main.Specializations.Where(i => (int)i.LevelOfEducation == cmbLevelOfEducation.SelectedIndex + 1 && i.Faculty == id).ToList();
            BindComboboxData(cmbFieldOfStudy, sp);
        }
       
        private void cmbFaculty_DropDown(object sender, EventArgs e)
        {

        }

        private void tbSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extentions.SurnameKeyPressValidator(e.KeyChar);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            /* Control a = mainPanel;
             bool res = true;
             if(Extentions.ValidateControls(this, errorProvider))
                 MessageBox.Show("Test");
             else
                 SystemSounds.Beep.Play();

             btnApply.Focus(); */


            //MessageBox.Show(Student.IsPropertiesEqual<IStudent>(this,"") ? "true" : "false") ;
        }

        private void SetRegAddressValues(bool isEmpty)
        {
            tbRegCity.Text = isEmpty ? "" : tbCity.Text;
            tbRegRegion.Text = isEmpty ? "" : tbRegion.Text;
            tbRegDistrict.Text = isEmpty ? "" : tbDistrict.Text;
            tbRegAddress.Text = isEmpty ? "" : tbAddress.Text;
            errorProvider.SetError(tbRegCity, "");
            errorProvider.SetError(tbRegAddress, "");
        }

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

        private void RequiredCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Extentions.RequiredComboBoxValidating(sender as ComboBox, errorProvider);            
        }

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

        private void tbDOB_Validating(object sender, CancelEventArgs e)
        {
            DateTime dt;
            string error;

            if (!DateTime.TryParse(tbDOB.Text, out dt))
                error = "Укажите корректную дату рождения";
            else error = dt.Year < 1900 ? "Укажите корректную дату рождения" : "";

            errorProvider.SetError(tbDOB, error);
        }

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSetSource_Click(object sender, EventArgs e)
        {
           
        }

        #region IEditable interfaces implemantation.

        private bool ValidateFields()
        {
            if (cbAddressesAreEquals.Checked)
                SetRegAddressValues(false);

            if (!Extentions.ValidateControls(this, errorProvider))
            {
                SystemSounds.Beep.Play();
                return false;
            }

            return true;
        }

        public void SetDefaultValues()
        {
            InitFiels();
        }

        public void SaveChanges()
        {
            if (!ValidateFields() || Type != ActionType.Edit)
                return;

            try
            {
                // Поля не учитываются в таблице в БД.
                var nameValue = Student.GetPropertiesDifference<IStudent>(this, "ID", "LevelOfEducation", "Faculty");

                if (nameValue.Count == 0)
                    return;

                // Обновляем данные
                main.DBGetter.Update("Student", ID, nameValue);

                MessageBox.Show($"Информация о студенте обновлена\nФИО студента: {Surname} {Name} {Patronymic}", "Редактирование информации", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Присваеваем свойству новые исходные значения.
                Student = ((IStudent)this).GetInstance<IStudent, Student>();

                SetFormText(this);
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

        public void Add()
        {
            if (!ValidateFields() || Type != ActionType.Add)
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
            }
        }

        #endregion

        public string ShortenString()
        {
            string result = string.Copy(linkPreferentialCategory.Tag.ToString());

            int width = this.Width - lblPreferentialCategory.Width - 70;
            TextRenderer.MeasureText(result, linkPreferentialCategory.Font, new Size(width, 0), TextFormatFlags.EndEllipsis | TextFormatFlags.ModifyString);

            return result;
        }

        private void linkPreferentialCategory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new PreferentialCategoryPicker(main.PreferentialCategories, linkPreferentialCategory, linkPreferentialCategory.Tag.ToString());
            form.ShowDialog(this);
            linkPreferentialCategory.Text = ShortenString();
        }

        private void linkClear_Click(object sender, EventArgs e)
        {
            linkPreferentialCategory.Tag = "Выбрать льготную категорию...";
            linkPreferentialCategory.Text = ShortenString();
        }

        private void StudentForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (linkPreferentialCategory.Focused)
            {


            }
        }

        private void linkPreferentialCategory_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                linkPreferentialCategory_LinkClicked(sender, null);
                return;
            }

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                linkClear_Click(sender, null);
                return;
            }
        }
    }
}
