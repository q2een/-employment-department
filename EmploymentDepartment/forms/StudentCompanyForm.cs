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
    public partial class StudentCompanyForm : Form
    {

        private readonly MainMDIForm main;
        public StudentCompanyForm(MainMDIForm main)
        {
            InitializeComponent();

            this.main = main;
        }

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
            }
        }

        // Нажатие на элемент управления "Очистить". Обработка события.
        private void linkClear_Click(object sender, EventArgs e)
        {
            var link = sender as Label;

            if (link.Name == "linkStudent") 
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
            LinkStudent = main.EntGetter.GetStudents()[1];
        }

        private void linkVacancy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkVacancy = main.EntGetter.GetVacancies()[0];
        }

        private void cbUnivercityEmployment_CheckedChanged(object sender, EventArgs e)
        {
            vacancyPanel.Enabled = cbUnivercityEmployment.Checked;
            tbCompany.Enabled = !cbUnivercityEmployment.Checked;
            tbPost.Enabled = !cbUnivercityEmployment.Checked;

            if(!cbUnivercityEmployment.Checked)
            {
                LinkVacancy = null;
                tbCompany.Focus();

            }
            else
            {
                errorProvider.SetError(tbCompany, "");
                errorProvider.SetError(tbPost, "");
            }
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

    }
}
