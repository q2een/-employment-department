using System;
using System.Data;
using System.Linq;

namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Предоставляет класс для экспорта и формирования отчетов из СУБД MySQL.
    /// </summary>
    public class MySqlExport : IExport
    {
        private readonly MySqlDB dataBase;

        /// <summary>
        /// Предоставляет класс для экспорта и формирования отчетов из СУБД MySQL.
        /// </summary>
        /// <param name="db">Экземпляр класса <c>MySqlDB</c></param>
        public MySqlExport(MySqlDB db)
        {
            this.dataBase = db;
        }

        // Возвращает таблицу полученную из БД.
        private DataTable GetDataTable(string query, string tableName)
        {
            var table = dataBase.GetDataTable(query);
            table.TableName = tableName;

            return table;
        }

        /// <summary>
        /// Возвращает все записи о студентах из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetStudents()
        {
            string students = " SELECT s.ApplicationFormNumber AS 'Шифр анкеты', s.Surname AS 'Фамилия', s.Name AS 'Имя', s.Patronymic AS 'Отчество', s.DOB AS 'Дата рождения', (CASE WHEN s.IsMale <> 0 THEN 'Мужской' ELSE 'Женский' END) AS 'Пол'," +
                                         "(CASE WHEN s.MaritalStatus <> 0 THEN 'Женат(Замужем)' ELSE 'Не женат(Не замужем)' END) AS 'Семейное положение'," +
                                         "s1.Факультет, s1.`Профиль подготовки`, s1.`Уровень образования`, s.YearOfGraduation AS 'Год окончания университета', s.StudyGroup AS 'Группа'," +
                                         "s.Rating AS 'Рейтинг', p.Name AS 'Льготная категория'," +
                                         "(CASE WHEN s.SelfEmployment <> 0 THEN 'Да' ELSE 'Нет' END) AS  'Самостоятельное трудоустройство', s.City AS 'Город (проживание)', s.Region AS 'Область (проживание)', s.District AS 'Район (проживание)'," +
                                         "s.Address AS 'Адрес (проживание)', s.RegCity AS 'Город (регистрация)', s.RegRegion AS 'Область (регистрация)', s.RegDistrict AS 'Район (регистрация)', s.RegAddress AS 'Адрес (регистрация)', s.Phone AS 'Телефон', s.Email AS 'Электронный адрес' " +
                                         "FROM student s LEFT JOIN(SELECT s1.ID, f.Name AS 'Факультет', s1.Name AS 'Профиль подготовки', s1.LevelOfEducation AS 'Уровень образования' " +
                                         "FROM specialization s1 LEFT JOIN faculty f ON s1.Faculty = f.ID) s1 ON s.FieldOfStudy = s1.ID LEFT JOIN preferentialcategory p ON s.PreferentialCategory = p.ID";

            return GetDataTable(students, "Студенты");
        }

        /// <summary>
        /// Возвращает все записи о предприятиях из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetCompanies()
        {
            string companies = "SELECT c.CompanyNumber AS 'Шифр предприятия',c.Name AS 'Наименование',c.NameOfStateDepartment AS 'Название государственного органа'," +
                                         "c.City AS 'Город', c.Region AS 'Область', c.District AS 'Район', c.Address AS 'Адрес', c.DirectorSurname AS 'Фамилия (Директор)'," +
                                         "c.DirectorName AS 'Имя (Директор)', c.DirectorPatronymic AS 'Отчество (Директор)',c.ContactSurname AS 'Фамилия (Контактное лицо)'," +
                                         "c.ContactName AS 'Имя (Контактное лицо)', c.ContactPatronymic AS 'Отчество (Контактное лицо)',c.Phone AS 'Телефон'," +
                                         "c.Email AS 'Электронный адрес', c.Note AS 'Примечание' FROM company c";

            return GetDataTable(companies, "Предприятия");
        }

        /// <summary>
        /// Возвращает все записи о местах работы студента из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetStudentCompanies()
        {
            string studentcompanies = "SELECT CONCAT_WS(' ',s1.Surname, s1.Name, s1.Patronymic) AS 'ФИО студента', s1.ApplicationFormNumber AS 'Шифр анкеты студента'," +
                                                "s1.StudyGroup AS 'Группа', s.NameOfCompany AS 'Наименование предприятия',(CASE WHEN s.Status <> 0 THEN 'Работает' ELSE 'Не работает' END) AS 'Статус', s.Post AS 'Должность'," +
                                                "v.VacancyNumber AS 'Шифр занимаемой вакансии', s.YearOfEmployment AS 'Год трудоустройства', s.Note AS 'Примечание' FROM studentcompany s LEFT JOIN student s1 ON s.Student = s1.ID LEFT JOIN vacancy v ON s.Vacancy = v.ID";

            return GetDataTable(studentcompanies, "Места работы студентов");
        }

        /// <summary>
        /// Возвращает все записи о вакансиях из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetVacancies()
        {
            string vacancies = "SELECT v.VacancyNumber AS 'Шифр вакансии', c.Name AS 'Предприятие', v.Post AS 'Должность', v.WorkArea AS 'Рабочая область'," +
                                          "v.Salary AS 'Оклад', v.SalaryNote AS 'Условия оплаты труда', v.Gender AS 'Пол', v.Features AS 'Дополнительная информация'," +
                                          "s1.ApplicationFormNumber AS 'Шифр анкеты студента, занимающего вакансию'FROM vacancy v LEFT JOIN company c ON v.Employer = c.ID LEFT JOIN studentcompany s ON v.ID = s.Vacancy LEFT JOIN student s1 ON s1.ID = s.Student";

            return GetDataTable(vacancies, "Вакансии");
        }

        /// <summary>
        /// Возвращает все записи о факультетах из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetFaculties()
        {
            return GetDataTable("SELECT Name AS 'Наименование' FROM faculty", "Факультеты");
        }

        /// <summary>
        /// Возвращает все записи о направлениях подготовки из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetSpecializations()
        {
            string specializations = "SELECT f.Name AS 'Факультет', s.Name AS 'Наименование', s.LevelOfEducation AS 'Уровень образования' FROM specialization s LEFT JOIN faculty f ON s.Faculty = f.ID";

            return GetDataTable(specializations, "Профили подготовки");
        }

        /// <summary>
        /// Возвращает все записи о льготных категориях из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetPreferentialCategories()
        {
            return GetDataTable("SELECT Name AS 'Наименование' FROM preferentialcategory", "Льготные категории");
        }

        /// <summary>
        /// Возвращает данные для отчета "Ведомость распределения выпускников, которые окончили ВУЗ" в заданном году <c>year</c>.
        /// </summary>
        /// <param name="year">Год окончиния ВУЗа</param>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetStatementReport(int year)
        {
            string statement = " SELECT '' AS '№ п/п', CONCAT_WS(' ', s.Surname, s.Name, s.Patronymic) AS 'ФИО студента', (CASE WHEN s.IsMale <> 0 THEN 'Мужской' ELSE 'Женский' END) AS 'Пол', YEAR(s.DOB) AS 'Год рождения'," +
                   "(CASE WHEN s.MaritalStatus <> 0 THEN 'Женат(Замужем)' ELSE 'Не женат(Не замужем)' END) AS 'Семейное положение'," +
                   "CONCAT_WS(', ', s.Region, s.District, s.City, s.Address) AS 'Адрес места жительства (адрес родителей)'," +
                   "s1.Name AS Специальность, s1.NameOfStateDepartment AS 'Название государственного органа', s1.NameOfCompany AS 'Название организации', s1.Post AS 'Должность, квалификация'," +
                   $"(CASE WHEN s.SelfEmployment <> 0 THEN 'Да' ELSE 'Нет' END) AS  'Предоставляется право самостоятельного трудоустройства', '' AS Подпись FROM student s LEFT JOIN specialization s1 ON s.FieldOfStudy = s1.ID LEFT JOIN (SELECT s.Student, s.Post, s.NameOfCompany, s.NameOfStateDepartment FROM studentcompany s) AS s1 ON s.ID = s1.Student WHERE s.YearOfGraduation = {year}";

            return dataBase.GetDataTable(statement);
        }

        /// <summary>
        /// Возвращает данные для отчета "Ведомость персонального учета выпускников".
        /// </summary>
        /// <param name="fielOfStudy">Направление подготовки</param>
        /// <param name="studyGroup">Группа</param>
        /// <param name="year">Год окончания ВУЗа</param>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        public DataTable GetPersonalAccountReport(int fielOfStudy, string studyGroup, int year)
        {
           return dataBase.GetDataTable($"SELECT CONCAT_WS(' ',s.Surname ,s.Name, s.Patronymic) AS FullName," +
                               "CONCAT_WS(', ', CONCAT('Область: ', s.Region), CONCAT('Район: ', s.District), CONCAT('Город: ', s.City), CONCAT('Адрес: ', s.Address)) AS Address, " +
                               "year1.NameOfCompany, year1.Post, year1.Note, year2.NameOfCompany, year2.Post, year2.Note, year3.NameOfCompany, year3.Post, year3.Note FROM student s " +
                               $"LEFT JOIN(SELECT s.NameOfCompany, s.Post, s.Note, s.Student FROM studentcompany s WHERE s.YearOfEmployment = '{year}') AS year1 ON s.ID = year1.Student " +
                               $"LEFT JOIN(SELECT s.NameOfCompany, s.Post, s.Note, s.Student FROM studentcompany s WHERE s.YearOfEmployment = '{++year}') AS year2 ON s.ID = year2.Student " +
                               $"LEFT JOIN(SELECT s.NameOfCompany, s.Post, s.Note, s.Student FROM studentcompany s WHERE s.YearOfEmployment = '{++year}') AS year3 ON s.ID = year3.Student " +
                               $"WHERE s.FieldOfStudy = {fielOfStudy} AND s.StudyGroup = '{studyGroup}' ORDER BY FullName");
        }

        /// <summary>
        /// Возвращает год окончания ВУЗа для студентов заданного группы и направления подготовки
        /// </summary>
        /// <param name="fielOfStudy">Направление подготовки</param>
        /// <param name="studyGroup">Группа</param>
        /// <returns>Год окночания ВУЗа</returns>
        public int GetYearOfGraduation(int fielOfStudy, string studyGroup)
        {
            var student = dataBase.GetCollection($"SELECT s.YearOfGraduation from student s where s.FieldOfStudy = {fielOfStudy} AND s.StudyGroup = '{studyGroup}' LIMIT 0,1");

            if (student.Count() <= 0)
                throw new Exception("Данной группы нет в базе данных или данной группе нет студентов. Проверьте указанные данные");

            int year;

            if (!int.TryParse((student.First()).Values.First().ToString(), out year))
                throw new Exception("Невозможно определить год окончания университета для студентов группы. Проверьте указанные данные");

            return year;
        }
    }
}
