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
    public partial class StudentCompanyForm : Form, IStudentCompany, IEditable<IStudentCompany>
    {
        public IStudentCompany Entity { get; private set; }
        public ActionType Type { get; private set; }
        private MainMDIForm main;

        public IStudent LinkStudent
        {
            get
            {
                return linkStudent.Tag as IStudent;
            }
            private set
            {                   
                linkStudent.Tag = value;
                string text = value == null ? "Выбрать студента ..." : $"{value.Surname} {value.Name} {value.Patronymic}";
                linkStudent.Text = Extentions.ShortenString(text, studentPanel.Width - linkStudentClear.Width - 90, linkStudent.Font);

                linkStudentClear.Visible = Type != ActionType.View;
            }
        }

        public IVacancy LinkVacancy
        {
            get
            {
                return linkVacancy.Tag as IVacancy;
            }
            private set
            {
                linkVacancy.Tag = value;
                string text = value == null ? "Выбрать вакансию ..." : $"Вакансия №{value.VacancyNumber}";
                linkVacancy.Text = Extentions.ShortenString(text, vacancyPanel.Width - linkVacancyClear.Width - 90, linkVacancy.Font);
                tbCompany.Text = value == null ? "" : main.EntGetter.GetCompanyById(value.Employer);
                tbPost.Text = value?.Post;

                linkVacancyClear.Visible = Type != ActionType.View;
            }
        }

        public StudentCompanyForm(ActionType type, IStudentCompany company = null)
        {
            if (type == ActionType.Edit && company == null)
                throw new ArgumentNullException();

            InitializeComponent();

            this.Entity = type == ActionType.Add ? null : company;
            this.Type = type;  
        }

        // Модальное окно для просмотра информации.
        public StudentCompanyForm(MainMDIForm mainForm, IStudentCompany company) : this(ActionType.View, company)
        {
            this.main = mainForm;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        // Обработка события загрузки формы.
        private void StudentCompanyForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;

            SetDefaultValues();
            SetFormText();
            mainPanel.Enabled = Type != ActionType.View;
        }
        
        private void cbUnivercityEmployment_CheckedChanged(object sender, EventArgs e)
        {
            vacancyPanel.Enabled = cbUnivercityEmployment.Checked;
            tbCompany.Enabled = !cbUnivercityEmployment.Checked;
            tbPost.Enabled = !cbUnivercityEmployment.Checked;

            if (!cbUnivercityEmployment.Checked)
            {
                LinkVacancy = null;
                tbCompany.Focus();
                errorProvider.SetError(linkVacancy, "");
            }
            else
            {
                linkVacancy.Focus();
                errorProvider.SetError(tbCompany, "");
                errorProvider.SetError(tbPost, "");
            }
        }

        private void StudentCompanyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (Type != ActionType.View)
                return;

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        // Устанавливает заголовок окна.
        private void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о месте работы студента";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление места работы студента";
                    break;
                case ActionType.View:
                    this.Text = $"Просмотр места работы. Студент: {LinkStudent.Surname} {LinkStudent.Name} {LinkStudent.Patronymic}";
                    break;
            }
        }

        #region link elements events
        // Нажатие на элемент управления "Очистить". Обработка события.
        private void linkClear_Click(object sender, EventArgs e)
        {
            var link = sender as Label;

            if (link.Name == "linkStudent" || link.Name == "linkStudentClear") 
                LinkStudent = null;
            else
                LinkVacancy = null;
        }

        // Обработка события нажития клавиши при активном элементе для выбора льготной категории. Обработка события.
        private void link_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var link = sender as LinkLabel;

            // Открыть окно на space.
            if (e.KeyCode == Keys.Space)
            {
                if (link.Name == "linkStudent")
                    linkStudent_LinkClicked(sender, null);
                else
                    linkVacancy_LinkClicked(sender, null);              
                return;
            }

            // Очистить на backspace and delete.
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                linkClear_Click(sender, null);
                return;
            }
        }

        private void linkStudent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LinkStudent == null)
            {
                LinkStudent = main.EntGetter.GetStudents()[1];
                return;
            }

            var form = new StudentForm(main, LinkStudent);
            form.ShowDialog(this);
        }

        private void linkVacancy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkVacancy = main.EntGetter.GetVacancies()[0];
        }
        #endregion

        #region Validating
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

        // Валадация текстового поля "Год трудоустройства".
        private void tbYearOfEmployment_Validating(object sender, CancelEventArgs e)
        {
            int year;

            if (!Int32.TryParse(tbYearOfEmployment.Text, out year))
                errorProvider.SetError(tbYearOfEmployment, "Укажите корректный год трудоустройства");
            else
                if (year < 2018)
                errorProvider.SetError(tbYearOfEmployment, "Год трудоустройства должен быть больше 2018");
            else
                errorProvider.SetError(tbYearOfEmployment, "");
        }

        private void linkStudent_Validating(object sender, CancelEventArgs e)
        {
            if (LinkStudent == null)
                errorProvider.SetError(linkStudent, "Необходимо выбрать студента");
            else
                errorProvider.SetError(linkStudent, "");
        }

        private void linkVacancy_Validating(object sender, CancelEventArgs e)
        {
            if (LinkVacancy == null)
                errorProvider.SetError(linkVacancy, "Необходимо выбрать вакансию");
            else
                errorProvider.SetError(linkVacancy, "");
        }
        #endregion

        #region IEditable interfaces implemantation.

        public bool ValidateFields() => Extentions.ValidateFields(this, errorProvider);


        public void SetDefaultValues() => this.SetPropertiesValue<IStudentCompany>(Entity, null);
        
        public void Save()
        {
            var msg = $"Информация о месте работы студента обновлена";
            if (this.UpdateFormEntityInDataBase<StudentCompanyForm, IStudentCompany>(main.DBGetter, msg, "ID", "Name"))
                SetFormText();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Insert()
        {
            var msg = $"Информация о месте работы студента добавлена в базу";
            this.InsertFormEntityToDataBase<StudentCompanyForm, IStudentCompany>(main.DBGetter, msg, "ID", "Name");
        }
        #endregion
        
        #region IStudentCompany implementation
        int id;
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

        public int Student
        {
            get
            {
                return LinkStudent.ID;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        string IStudentCompany.CompanyName
        {
            get
            {
                return tbCompany.Text;
            }
            set
            {
                tbCompany.Text = value;
            }
        }

        public bool Status
        {
            get
            {
                return cmbStatus.SelectedIndex == 0 ? true : false;
            }

            set
            {
                cmbStatus.SelectedIndex = value ? 0 : 1;
            }
        }

        public int? Vacancy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Post
        {
            get
            {
                return tbPost.Text;
            }

            set
            {
                tbPost.Text = value;
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
        #endregion

    }
}
