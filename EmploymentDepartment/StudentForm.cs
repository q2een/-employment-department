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
    public partial class StudentForm : Form, IStudent
    {
        #region IStudent Implementation.
        public int ID
        {
            get
            {
                return Student?.ID ?? 0;
            }
        }

        public string ApplicationFormNumber
        {
            get
            {
                return tbApplicationFormNumber.Text;
            }
        }

        string IStudent.Name
        {
            get
            {
                return tbName.Text;
            }
        }

        public string Surname
        {
            get
            {
                return tbSurname.Text;
            }
        }

        public string Patronymic
        {
            get
            {
                return tbPatronymic.Text;
            }
        }

        public DateTime DOB
        {
            get
            {
                return DateTime.Parse(tbDOB.Text);
            }
        }

        public GenderType Gender
        {
            get
            {
                return (GenderType)cmbGender.SelectedIndex+1;
            }
        }

        public bool MaritalStatus
        {
            get
            {
                return cmbMaritalStatus.SelectedIndex == 1; 
            }
        }

        public int YearOfGraduation
        {
            get
            {
                return Int32.Parse(tbYearOfGraduation.Text);
            }
        }

        public int Faculty
        {
            get
            {
                return Int32.Parse(cmbFaculty.SelectedValue.ToString());
            }
        }

        public EducationLevelType LevelOfEducation
        {
            get
            {
                return (EducationLevelType)(cmbLevelOfEducation.SelectedIndex + 1);
            }
        }

        public int FieldOfStudy
        {
            get
            {
                return Int32.Parse(cmbFieldOfStudy.SelectedValue.ToString());
            }
        }

        public string StudyGroup
        {
            get
            {
                return tbStudyGroup.Text;
            }
        }

        public decimal Rating
        {
            get
            {
                return Decimal.Parse(tbRating.Text);
            }
        }

        public string PreferentialCategory
        {
            get
            {
                return null; throw new NotImplementedException();
            }
        }

        public bool SelfEmployment
        {
            get
            {
                return cbSelfEmployment.Checked;
            }
        }

        public string City
        {
            get
            {
                return tbCity.Text;
            }
        }

        string IStudent.Region
        {
            get
            {
                return tbRegion.Text;
            }
        }

        public string District
        {
            get
            {
                return tbDistrict.Text;
            }
        }

        public string Address
        {
            get
            {
                return tbAddress.Text;
            }
        }

        public string RegCity
        {
            get
            {
                return tbRegCity.Text;
            }
        }

        public string RegRegion
        {
            get
            {
                return tbRegRegion.Text;
            }
        }

        public string RegDistrict
        {
            get
            {
                return tbRegDistrict.Text;
            }
        }

        public string RegAddress
        {
            get
            {
                return tbRegAddress.Text;
            }
        }

        public string Phone
        {
            get
            {
                return tbPhone.Text;
            }
        }

        public string Email
        {
            get
            {
                return tbEmail.Text;
            }
        }

        #endregion


        public Student Student { get; private set; }
        public ActionType Type { get; private set; }
        private MainMDIForm main;

        public StudentForm(ActionType type, Student student = null)
        {
            if (type == ActionType.Edit && student == null)
                throw new ArgumentNullException();

            InitializeComponent();
           
            this.Student = student;
            this.Type = type;

            if(type == ActionType.Edit)
            {
                btnRemove.Visible = false;
                btnApply.Text = "Сохранить";
                this.Text = $"Редактирование информации о студенте [{student.Surname} {student.Name} {student.Patronymic}]" ;
            }
        }

        private void StudentForm_SizeChanged(object sender, EventArgs e)
        {
            mainPanel.AutoScroll = this.Size.Height < 650;
        }

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
            var fc = main.EntGetter.GetFaculties((EducationLevelType)(cmbLevelOfEducation.SelectedIndex + 1));
            BindComboboxData(cmbFaculty, fc);
            var sp = main.Specializations.Where(i => (int)i.LevelOfEducation == cmbLevelOfEducation.SelectedIndex + 1 && i.Faculty == (int)cmbFaculty.SelectedValue).ToList();
            BindComboboxData(cmbFieldOfStudy, sp);
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

        private void StudentForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm))
                throw new Exception();

            this.main = this.MdiParent as MainMDIForm;
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
            //MessageBox.Show(FieldOfStudy + "");
            MessageBox.Show(Student.PublicInstancePropertiesEqual<IStudent>(this,"") ? "true" : "false") ;
        }
    }
}
