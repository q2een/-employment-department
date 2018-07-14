using System;
using System.Collections.Generic;
using System.Linq;

namespace EmploymentDepartment.BL
{
    public class MySqlGetter : IEntityGetter
    {
        public MySqlGetter(IDataBase dbGetter)
        {
            this.db = dbGetter;
        }

        /// <summary>
        /// Возвращает сущность из БД по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность из БД</returns>
        public T GetSingle<T>(int id) where T : class, IIdentifiable
        {
            if (typeof(T) == typeof(Student))
                return GetStudent(id) as T;
            if (typeof(T) == typeof(Company))
                return GetCompany(id) as T;
            if (typeof(T) == typeof(Faculty))
                return GetFaculty(id) as T;
            if (typeof(T) == typeof(PreferentialCategory))
                return GetPreferentialCategory(id) as T;
            if (typeof(T) == typeof(Specialization))
                return GetSpecialization(id) as T;
            if (typeof(T) == typeof(StudentCompany))
                return GetStudentCompany(id) as T;
            if (typeof(T) == typeof(Vacancy))
                return GetVacancy(id) as T;

            return null;
        }

        /// <summary>
        /// Возвращает коллекцию сущностей из БД.
        /// </summary>
        /// <returns>Коллекция сущностей из БД</returns>
        public IEnumerable<T> GetEntities<T>() where T : class, IIdentifiable
        {
            if (typeof(T) == typeof(Student))
                return GetStudents() as IEnumerable<T>;
            if (typeof(T) == typeof(Company))
                return GetCompanies() as IEnumerable<T>;
            if (typeof(T) == typeof(Faculty))
                return GetFaculties() as IEnumerable<T>;
            if (typeof(T) == typeof(PreferentialCategory))
                return GetPreferentialCategories() as IEnumerable<T>;
            if (typeof(T) == typeof(Specialization))
                return GetSpecializations() as IEnumerable<T>;
            if (typeof(T) == typeof(StudentCompany))
                return GetStudentCompanies() as IEnumerable<T>;
            if (typeof(T) == typeof(Vacancy))
                return GetVacancies() as IEnumerable<T>;

            return null;
        }

        /// <summary>
        /// Удаляет из БД запись, соответствующую сущности.
        /// </summary>
        /// <param name="entity">Сущность, соответствующая удаляемой записи</param>
        public void RemoveEntity<T>(T entity) where T : IIdentifiable
        {
            if (entity is IStudent)
            {
                RemoveStudent(entity.ID);
                return;
            }
            if (entity is ICompany)
            {
                RemoveCompany(entity.ID);
                return;
            }
            if (entity is IFaculty)
            {
                RemoveFaculty(entity.ID);
                return;
            }
            if (entity is IPreferentialCategory)
            {
                RemovePreferentialCategory(entity.ID);
                return;
            }
            if (entity is ISpecialization)
            {
                RemoveSpecialization(entity.ID);
                return;
            }
            if (entity is IStudentCompany)
            {
                RemoveStudentCompany(entity.ID);
                return;
            }
            if (entity is IVacancy)
            {
                RemoveVacancy(entity.ID);
                return;
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Возвращает коллекцию мест работы по идентификатору студента.
        /// </summary>
        /// <param name="id">Идентификатор записи студента</param>
        /// <returns>Коллекция мест работы студента</returns>
        public IEnumerable<IStudentCompany> GetCompaniesByStudentID(int id)
        {
            return GetEntities<StudentCompany>(StudentCompany + $" WHERE s1.ID = {id}");
        }

        /// <summary>
        /// Возвращает коллекцию мест работы по идентификатору предприятия.
        /// </summary>
        /// <param name="id">Идентификатор записи предприятия</param>
        /// <returns>Коллекция мест работы на предприятии</returns>
        public IEnumerable<IStudentCompany> GetSudentsByCompanyID(int id)
        {
            return GetEntities<StudentCompany>(StudentCompany + $" WHERE v.Employer= {id}");
        }

        /// <summary>
        /// Возвращает коллекцию мест работы по идентификатору вакансии.
        /// </summary>
        /// <param name="id">Идентификатор записи вакансии</param>
        /// <returns>Коллекция мест работы по заданной вакансии</returns>
        public IEnumerable<IStudentCompany> GetSudentsByVacancyID(int id)
        {
            return GetEntities<StudentCompany>(StudentCompany + $" WHERE v.ID = {id}");
        }

        /// <summary>
        /// Возвращает коллекцию вакансий по идентификатору предприятия.
        /// </summary>
        /// <param name="id">Идентификатор записи предприятия</param>
        /// <returns>Коллекция вакансий на предприятии</returns> 
        public IEnumerable<IVacancy> GetVacanciesByCompanyID(int id)
        {
            return GetEntities<Vacancy>(Vacancy + $" WHERE c.ID = {id}");
        }

        /// <summary>
        /// Возвращает коллекцию факультетов на которых есть заданный уровень образования.
        /// </summary>
        /// <param name="type">Уровень образования</param>
        /// <returns>Коллекция факультетов</returns>
        public IEnumerable<IFaculty> GetFaculties(EducationLevelType type)
        {
            return GetEntities<Faculty>("SELECT f.ID, f.Name FROM specialization s INNER JOIN faculty f ON s.Faculty = f.ID WHERE s.LevelOfEducation = " + (int)type + " GROUP BY f.Name");
        }
     
        #region Single Entities private methods.

        private T GetSingle<T>(string query) where T : class, IIdentifiable, new()
        {
            var dict = db.GetCollection(query);

            if (dict == null || dict.Count() != 1)
                return null;

            var entity = new T();
            entity.SetProperties(dict.First());

            return entity;
        }

        private Student GetStudent(int id)
        {
            return GetSingle<Student>($"{Student} WHERE s.ID = {id}");
        }

        private Company GetCompany(int id)
        {
            return GetSingle<Company>("SELECT * FROM company WHERE ID = " + id);
        }

        private StudentCompany GetStudentCompany(int id)
        {
            return GetSingle<StudentCompany>($"{StudentCompany} WHERE s.ID = {id}");
        }

        private Vacancy GetVacancy(int id)
        {
            return GetSingle<Vacancy>($"{Vacancy} WHERE v.ID = {id}");
        }

        private Faculty GetFaculty(int id)
        {
            return GetSingle<Faculty>($"{Faculty} WHERE f.ID = {id}");
        }

        private Specialization GetSpecialization(int id)
        {
            return GetSingle<Specialization>($"{Specialization} WHERE s.ID = {id}");
        }

        private PreferentialCategory GetPreferentialCategory(int id)
        {
            return GetSingle<PreferentialCategory>($"{PreferentialCategory} WHERE ID = {id}");
        }

        #endregion

        #region Collection of entities private methods.

        private IEnumerable<T> GetEntities<T>(string dbQuery) where T : new()
        {
            var result = new List<T>();

            foreach (var dict in db.GetCollection(dbQuery))
            {
                var entity = new T();
                entity.SetProperties(dict);
                result.Add(entity);
            }

            return result;
        }

        private IEnumerable<Student> GetStudents()
        {
            return GetEntities<Student>(Student);
        }

        private IEnumerable<Company> GetCompanies()
        {
            return GetEntities<Company>(Company);
        }

        private IEnumerable<StudentCompany> GetStudentCompanies()
        {
            return GetEntities<StudentCompany>(StudentCompany);
        }

        private IEnumerable<Vacancy> GetVacancies()
        {
            return GetEntities<Vacancy>(Vacancy + " WHERE s.Vacancy IS NULL");
        }

        private IEnumerable<Faculty> GetFaculties()
        {
            return GetEntities<Faculty>(Faculty);
        }

        private IEnumerable<Specialization> GetSpecializations()
        {
            return GetEntities<Specialization>(Specialization);
        }

        private IEnumerable<PreferentialCategory> GetPreferentialCategories()
        {
            return GetEntities<PreferentialCategory>(PreferentialCategory);
        }

        #endregion
        
        #region Remove privite methods.

        private void RemovePreferentialCategory(int id)
        {
            db.Query($"UPDATE student s SET s.PreferentialCategory = NULL WHERE s.PreferentialCategory = {id}");
            db.Query($"DELETE FROM preferentialcategory WHERE ID = {id}");
        }

        private void RemoveSpecialization(int id)
        {
            var dt = db.GetDataTable($"SELECT s.ID FROM student s WHERE s.FieldOfStudy = {id}");

            if (dt.Rows.Count > 0)
                throw new Exception("Для удаления профиля подготовки необходимо переместить или удалить всех студентов, которые к нему прикреплены");

            db.Query($"DELETE FROM specialization WHERE ID = {id}");

        }

        private void RemoveFaculty(int id)
        {
            var dt = db.GetDataTable($"SELECT f.ID FROM faculty f INNER JOIN specialization s ON f.ID = s.faculty WHERE f.ID = {id}");

            if (dt.Rows.Count > 0)
                throw new Exception("Для удаления факультета необходимо переместить или удалить все профили подготовки, которые к нему прикреплены");

            db.Query($"DELETE FROM faculty WHERE ID = {id}");
        }

        private void RemoveVacancy(int id)
        {
            db.Query($"UPDATE studentcompany s SET s.vacancy = NULL WHERE s.vacancy = {id}");
            db.Query($"DELETE FROM vacancy WHERE ID = {id}");
        }

        private void RemoveStudentCompany(int id)
        {
            db.Query($"DELETE s, v.* FROM vacancy v RIGHT JOIN studentcompany s ON v.ID = s.Vacancy WHERE s.ID = {id}");
        }

        private void RemoveCompany(int id)
        {
            var dt = db.GetDataTable($"SELECT s.ID FROM vacancy v WHERE v.Employer = {id}");

            if (dt.Rows.Count > 0)
                throw new Exception("Для удаления предприятия необходимо переместить или удалить все вакансии, которые к нему прикреплены");

            db.Query($"DELETE FROM company WHERE ID = {id}");
        }

        private void RemoveStudent(int id)
        {
            db.Query($"DELETE s, v.* FROM vacancy v RIGHT JOIN studentcompany s ON v.ID = s.Vacancy WHERE s.Student = {id}");
            db.Query($"DELETE FROM student WHERE ID = {id}");
        }

        #endregion

        private readonly IDataBase db;

        #region Default queries.

        private string Student
        {
            get
            {
                return "SELECT s.ID, s.ApplicationFormNumber, s.Name, s.Surname, s.Patronymic, s.DOB, s.Gender + 0 AS Gender," +
                       "s.MaritalStatus, s.YearOfGraduation, d.LevelOfEducation,d.Faculty,d.FacultyName, d.Specialization, s.StudyGroup,s.Rating," +
                       "s.PreferentialCategory, s.SelfEmployment,s.City, s.Region, s.District, s.Address, s.RegCity,s.RegRegion, s.RegDistrict," +
                       "s.RegAddress, s.Phone, s.Email, s.FieldOfStudy FROM student s INNER JOIN(SELECT sp.LevelOfEducation + 0 as LevelOfEducation," +
                       "sp.Name AS Specialization, f.ID AS Faculty, f.Name AS FacultyName, sp.ID FROM specialization sp INNER JOIN faculty f ON sp.Faculty = f.ID) AS d ON s.FieldOfStudy = d.ID";
            }
        }

        private string Faculty
        {
            get
            {
                return "SELECT f.ID, f.Name FROM faculty f";
            }
        }

        private string Specialization
        {
            get
            {
                return "SELECT s.ID, s.Faculty, f.Name AS FacultyName, s.LevelOfEducation AS LevelOfEducationName, s.LevelOfEducation + 0 AS LevelOfEducation, s.Name FROM specialization s INNER JOIN faculty f ON s.Faculty = f.ID";
            }
        }

        private string Company
        {
            get
            {
                return "SELECT * FROM company";
            }
        }

        private string Vacancy
        {
            get
            {
                return "SELECT v.ID, v.VacancyNumber, v.Post, v.Employer, c.Name as CompanyName, v.WorkArea, v.Salary, v.IsActive, v.SalaryNote,v.Gender AS GenderName," +
                       "v.Gender + 0 AS Gender, v.Features FROM vacancy v Left JOIN company c ON v.Employer = c.ID LEFT JOIN studentcompany s ON v.ID = s.vacancy ";
            }
        }

        private string StudentCompany
        {
            get
            {
                return "SELECT s.ID, s.Student,CONCAT_WS(' ', s1.Surname, s1.Name, s1.Patronymic) AS StudentFullName, s.NameOfCompany, s.Status, s.Post, s.YearOfEmployment, s.Vacancy, v.VacancyNumber AS VacancyNumber, s.Salary, s.NameOfStateDepartment, s.Note FROM studentcompany s LEFT JOIN vacancy v ON s.Vacancy = v.ID INNER JOIN student s1 ON s.Student = s1.ID";
            }
        }

        private string PreferentialCategory
        {
            get
            {
                return "SELECT * FROM preferentialcategory";
            }
        }

        #endregion

        public static Table GetTableNameByType<T>(T obj) where T:class, IIdentifiable
        {
            if (obj is IStudent)
                return Table.student;
            if (obj is ICompany)
                return Table.company;
            if (obj is IFaculty)
                return Table.faculty;
            if (obj is IPreferentialCategory)
                return Table.preferentialcategory;
            if (obj is ISpecialization)
                return Table.specialization;
            if (obj is IStudentCompany)
                return Table.studentcompany;
            if (obj is IVacancy)
                return Table.vacancy;

            throw new ArgumentException();
        }
    }
}
