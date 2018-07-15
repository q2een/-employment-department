using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public partial class StatementReportForm : Form
    {
        private readonly IDataBase db;
        private readonly string fileName;

        public StatementReportForm(IDataBase db, string filePath)
        {
            InitializeComponent();
            this.db = db;
            this.fileName = filePath;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            tbYear.Focus();
            btnOK.Focus();

            if (errorProvider.GetError(tbYear) != "")
                return;

            int year = int.Parse(tbYear.Text);

            var doc = new WordFile(Directory.GetCurrentDirectory() + @"\templates\Statement.docx");

            string data = " SELECT '' AS '№ п/п', CONCAT_WS(' ', s.Surname, s.Name, s.Patronymic) AS 'ФИО студента', (CASE WHEN s.IsMale <> 0 THEN 'Мужской' ELSE 'Женский' END) AS 'Пол', YEAR(s.DOB) AS 'Год рождения'," +
                               "(CASE WHEN s.MaritalStatus <> 0 THEN 'Женат(Замужем)' ELSE 'Не женат(Не замужем)' END) AS 'Семейное положение'," +
                               "CONCAT_WS(', ', s.Region, s.District, s.City, s.Address) AS 'Адрес места жительства (адрес родителей)'," +
                               "s1.Name AS Специальность, s1.NameOfStateDepartment AS 'Название государственного органа', s1.NameOfCompany AS 'Название организации', s1.Post AS 'Должность, квалификация'," +
                               $"(CASE WHEN s.SelfEmployment <> 0 THEN 'Да' ELSE 'Нет' END) AS  'Предоставляется право самостоятельного трудоустройства', '' AS Подпись FROM student s LEFT JOIN specialization s1 ON s.FieldOfStudy = s1.ID LEFT JOIN (SELECT s.Student, s.Post, s.NameOfCompany, s.NameOfStateDepartment FROM studentcompany s) AS s1 ON s.ID = s1.Student WHERE s.YearOfGraduation = {year}";

            var dataTable = db.GetDataTable(data);

            doc.AddTable(dataTable);
            doc.ReplaceWordText("{year}", year.ToString());
            doc.Save(fileName);

            this.Close();
        }

        private void tbYear_Validating(object sender, CancelEventArgs e)
        {
            int year;

            if (!Int32.TryParse(tbYear.Text, out year) || year < 2010)
                errorProvider.SetError(tbYear, "Укажите год окончания универсистета");
            else
                errorProvider.SetError(tbYear, "");
        }
    }
}
