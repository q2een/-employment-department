using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет окно для экспорта данных из БД в формат .xls.
    /// </summary>
    public partial class ExportForm : Form
    {
        private readonly IDataBase db;

        /// <summary>
        /// Предоставляет окно для экспорта данных из БД в excel
        /// </summary>
        /// <param name="database">Экземпляр класса, реализующего интерфейс <c>IDataBase</c>, для доступа к базе данных</param>
        public ExportForm(IDataBase database)
        {
            InitializeComponent();

            this.db = database;
        }

        // Возвращает истину если ни один из флажков на форме не был выбран.
        private bool IsTablesNotSelected()
        {
            foreach (Control control in gbTables.Controls)
            {
                if ((control is CheckBox) && (control as CheckBox).Checked)
                    return false;
            }

            return true;
        }

        // Записать содержимое в один файл но на разные листы.
        private void WriteInOneFile()
        {
            var tables = GetExportDataTables();

            var doc = new ExcelFile(tables.Count);

            foreach (var table in tables)
            {
                doc.AddSheet(table, table.TableName);
            }

            doc.Save(tbPath.Text + $@"\Таблицы_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}.xls");
        }

        // Записать содержимое в разные файлы.
        private void WriteInDifferentFiles()
        {
            var tables = GetExportDataTables();
            
            foreach (var table in tables)
            {
                var doc = new ExcelFile(1);
                doc.AddSheet(table, table.TableName);
                doc.Save(tbPath.Text + $@"\{table.TableName}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}.xls");
            }             
        }

        // Возвращает таблицу полученную из БД.
        private DataTable GetDataTable(string query, string tableName)
        {
            var table = db.GetDataTable(query);
            table.TableName = tableName;

            return table;
        }

        // Возвращает список таблиц соответствующих выбору пользователя. 
        private List<DataTable> GetExportDataTables()
        {
            var tables = new List<DataTable>();

            if (cbPreferentialCategories.Checked)
                tables.Add(GetDataTable(preferentialcategories, "Льготные категории"));

            if (cbSpecializations.Checked)
                tables.Add(GetDataTable(specializations, "Профили подготовки"));

            if (cbFaculties.Checked)
                tables.Add(GetDataTable(faculties, "Факультеты"));

            if (cbVacancies.Checked)
                tables.Add(GetDataTable(vacancies, "Вакансии"));

            if (cbStudentCompanies.Checked)
                tables.Add(GetDataTable(studentcompanies, "Места работы студентов"));

            if (cbCompanies.Checked)
                tables.Add(GetDataTable(companies, "Предприятия"));

            if (cbSudents.Checked)
                tables.Add(GetDataTable(students, "Студенты"));

            return tables;
        }

        // Нажатие на кнопку "Обзор". Обработка события.
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                tbPath.Text = folderBrowserDialog.SelectedPath;
        }

        // Нажатие на кнопку "Подтвердить". Обработка события.
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbPath.Text.Trim()) || !System.IO.Directory.Exists(tbPath.Text.Trim()))
                    throw new Exception("Укажите корректный путь для сохранения файлов");
             
                if (IsTablesNotSelected())
                    throw new Exception("Не выбраны таблицы для экспорта");

                if (rbFiles.Checked)
                    WriteInDifferentFiles();
                else
                    WriteInOneFile();

                MessageBox.Show("Экспорт данных успешно осуществлен", "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #region SQL queries

        private const string students = " SELECT s.ApplicationFormNumber AS 'Шифр анкеты', s.Surname AS 'Фамилия', s.Name AS 'Имя', s.Patronymic AS 'Отчество', s.DOB AS 'Дата рождения', s.Gender AS 'Пол'," +
                                        "(CASE WHEN s.MaritalStatus <> 0 THEN 'Женат(Замужем)' ELSE 'Не женат(Не замужем)' END) AS 'Семейное положение'," +
                                        "s1.Факультет, s1.`Профиль подготовки`, s1.`Уровень образования`, s.YearOfGraduation AS 'Год окончания университета', s.StudyGroup AS 'Группа'," +
                                        "s.Rating AS 'Рейтинг', p.Name AS 'Льготная категория'," +
                                        "(CASE WHEN s.SelfEmployment <> 0 THEN 'Да' ELSE 'Нет' END) AS  'Самостоятельное трудоустройство', s.City AS 'Город (проживание)', s.Region AS 'Область (проживание)', s.District AS 'Район (проживание)'," +
                                        "s.Address AS 'Адрес (проживание)', s.RegCity AS 'Город (регистрация)', s.RegRegion AS 'Область (регистрация)', s.RegDistrict AS 'Район (регистрация)', s.RegAddress AS 'Адрес (регистрация)', s.Phone AS 'Телефон', s.Email AS 'Электронный адрес' " +
                                        "FROM student s LEFT JOIN(SELECT s1.ID, f.Name AS 'Факультет', s1.Name AS 'Профиль подготовки', s1.LevelOfEducation AS 'Уровень образования' " +
                                        "FROM specialization s1 LEFT JOIN faculty f ON s1.Faculty = f.ID) s1 ON s.FieldOfStudy = s1.ID LEFT JOIN preferentialcategory p ON s.PreferentialCategory = p.ID";

        private const string companies = "SELECT c.CompanyNumber AS 'Шифр предприятия',c.Name AS 'Наименование',c.NameOfStateDepartment AS 'Название государственного органа'," +
                                         "c.City AS 'Город', c.Region AS 'Область', c.District AS 'Район', c.Address AS 'Адрес', c.DirectorSurname AS 'Фамилия (Директор)'," +
                                         "c.DirectorName AS 'Имя (Директор)', c.DirectorPatronymic AS 'Отчество (Директор)',c.ContactSurname AS 'Фамилия (Контактное лицо)'," +
                                         "c.ContactName AS 'Имя (Контактное лицо)', c.ContactPatronymic AS 'Отчество (Контактное лицо)',c.Phone AS 'Телефон',"+
                                         "c.Email AS 'Электронный адрес', c.Note AS 'Примечание' FROM company c";

        private const string studentcompanies = "SELECT CONCAT(s1.Surname,' ', s1.Name,' ', s1.Patronymic) AS 'ФИО студента', s1.ApplicationFormNumber AS 'Шифр анкеты студента'," + 
                                                "s1.StudyGroup AS 'Группа', s.CompanyName AS 'Наименование предприятия',(CASE WHEN s.Status <> 0 THEN 'Работает' ELSE 'Не работает' END) AS 'Статус', s.Post AS 'Должность'," +
                                                "v.VacancyNumber AS 'Шифр занимаемой вакансии', s.YearOfEmployment AS 'Год трудоустройства', s.Note AS 'Примечание' FROM studentcompany s LEFT JOIN student s1 ON s.Student = s1.ID LEFT JOIN vacancy v ON s.Vacancy = v.ID";

        private const string vacancies = "SELECT v.VacancyNumber AS 'Шифр вакансии', c.Name AS 'Предприятие', v.Post AS 'Должность', v.WorkArea AS 'Рабочая область',"+
                                         "v.Salary AS 'Оклад', v.SalaryNote AS 'Условия оплаты труда', v.Gender AS 'Пол', v.Features AS 'Дополнительная информация',"+
                                         "s1.ApplicationFormNumber AS 'Шифр анкеты студента, занимающего вакансию'FROM vacancy v LEFT JOIN company c ON v.Employer = c.ID LEFT JOIN studentcompany s ON v.ID = s.Vacancy LEFT JOIN student s1 ON s1.ID = s.Student";

        private const string faculties = "SELECT Name AS 'Наименование' FROM faculty";

        private const string specializations = "SELECT f.Name AS 'Факультет', s.Name AS 'Наименование', s.LevelOfEducation AS 'Уровень образования' FROM specialization s LEFT JOIN faculty f ON s.Faculty = f.ID";

        private const string preferentialcategories = "SELECT Name AS 'Наименование' FROM preferentialcategory ";

        #endregion
    }
}
