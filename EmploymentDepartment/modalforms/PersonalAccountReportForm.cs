using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет форму для формирования отчета "Ведомость персонального учета выпускников".
    /// </summary>
    public partial class PersonalAccountReportForm : Form
    {
        private readonly MainMDIForm main;
        private IEnumerable<Specialization> specializations = null;
        private readonly string fileName;

        /// <summary>
        /// Предоставляет форму для формирования отчета "Ведомость персонального учета выпускников"
        /// </summary>
        /// <param name="mainForm">Экземпляр класса главного окна</param>
        /// <param name="fileName">Полный путь к файлу отчета</param>
        public PersonalAccountReportForm(MainMDIForm mainForm, string fileName)
        {
            InitializeComponent();
            this.main = mainForm;
            this.fileName = fileName;
        }

        // Смена значений в выдающем списке "Факультет". Обработка события.
        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = cmbFaculty.SelectedValue;
            int id;

            if (!Int32.TryParse(value + "", out id))
                return;

            var sp = specializations.Where(i => i.Faculty == id).Select(z => z as ISpecialization).ToList();
            cmbFieldOfStudy.BindComboboxData(sp);
        }

        // Обработка события загрузки окна.
        private void PersonalAccountReportForm_Load(object sender, EventArgs e)
        {
            var faculties = main.Entities.GetEntities<Faculty>().ToList();
            specializations = main.Entities.GetEntities<Specialization>();
            cmbFaculty.BindComboboxData(faculties);

            cmbFaculty_SelectedIndexChanged(sender, e);
        }

        // Нажатие на кнопку "Подтвердить". Обработка события.
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbStudyGroup.Text.Trim().Length < 2)
                    throw new Exception("Укажите группу");

                int fielOfStudy; 
                if (!Int32.TryParse(cmbFieldOfStudy.SelectedValue + "", out fielOfStudy))
                    throw new Exception("Укажите профиль подготовки");

                var student = main.DataBase.GetCollection($"SELECT s.YearOfGraduation from student s where s.FieldOfStudy = {fielOfStudy} AND s.StudyGroup = '{tbStudyGroup.Text.Trim()}' LIMIT 0,1");

                if (student.Count() <= 0)
                    throw new Exception("Данной группы нет в базе данных или данной группе нет студентов. Проверьте указанные данные");

                int year, year2, year3;

                if (!int.TryParse((student.First()).Values.First().ToString(), out year))
                    throw new Exception("Невозможно определить год окончания университета для студентов группы. Проверьте указанные данные");

                year2 = year + 1;
                year3 = year + 2; 

                var table = main.DataBase.GetDataTable($"SELECT CONCAT_WS(' ',s.Surname ,s.Name, s.Patronymic) AS FullName," +
                                              "CONCAT_WS(', ', CONCAT('Область: ', s.Region), CONCAT('Район: ', s.District), CONCAT('Город: ', s.City), CONCAT('Адрес: ', s.Address)) AS Address, " +
                                              "year1.NameOfCompany, year1.Post, year1.Note, year2.NameOfCompany, year2.Post, year2.Note, year3.NameOfCompany, year3.Post, year3.Note FROM student s " +
                                              $"LEFT JOIN(SELECT s.NameOfCompany, s.Post, s.Note, s.Student FROM studentcompany s WHERE s.YearOfEmployment = '{year}') AS year1 ON s.ID = year1.Student " +
                                              $"LEFT JOIN(SELECT s.NameOfCompany, s.Post, s.Note, s.Student FROM studentcompany s WHERE s.YearOfEmployment = '{year2}') AS year2 ON s.ID = year2.Student " +
                                              $"LEFT JOIN(SELECT s.NameOfCompany, s.Post, s.Note, s.Student FROM studentcompany s WHERE s.YearOfEmployment = '{year3}') AS year3 ON s.ID = year3.Student " +
                                              $"WHERE s.FieldOfStudy = {fielOfStudy} AND s.StudyGroup = '{tbStudyGroup.Text.Trim()}' ORDER BY FullName");

                var doc = new PersonalAccountOfGraduatesReport(System.IO.Directory.GetCurrentDirectory() + @"\templates\personalAccountOfGraduates.docx");

                doc.ReplaceWordText("{Faculty}", cmbFaculty.Text);
                doc.ReplaceWordText("{FieldOfStudy}", cmbFieldOfStudy.Text);
                doc.ReplaceWordText("{StudyGroup}", tbStudyGroup.Text.Trim());
                doc.ReplaceWordText("{Year1}", year.ToString());
                doc.ReplaceWordText("{Year2}", year2.ToString());
                doc.ReplaceWordText("{Year3}", year3.ToString());

                doc.AddTable(table);
                doc.Save(fileName);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при сохранении отчета", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
