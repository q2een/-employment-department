using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class EntitiesGetter
    {
        private readonly IDataBase db;

        private string Student
        {
            get
            {
                return "SELECT s.ID, s.ApplicationFormNumber, s.Name, s.Surname, s.Patronymic, s.DOB, s.Gender + 0 AS Gender,"+
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
                return "SELECT v.ID, v.VacancyNumber, v.Post, v.Employer, c.Name as CompanyName, v.WorkArea, v.Salary, v.IsActive, v.SalaryNote,v.Gender AS GenderName, v.Gender + 0 AS Gender, v.Features FROM vacancy v INNER JOIN company c ON v.Employer = c.ID";
            }
        }

        private string StudentCompany
        {
            get
            {
                return "SELECT s.ID, s.Student,CONCAT(s1.Surname,\" \", s1.Name,\" \", s1.Patronymic) AS StudentFullName, s.CompanyName, s.Status, s.Post, s.YearOfEmployment, s.Vacancy, v.VacancyNumber AS VacancyNumber, s.Note FROM studentcompany s LEFT JOIN vacancy v ON s.Vacancy = v.ID INNER JOIN student s1 ON s.Student = s1.ID";
            }
        }

        public string PreferentialCategory
        {
            get
            {
                return "SELECT * FROM preferentialcategory";
            }
        }

        public EntitiesGetter(IDataBase dbGetter)
        {
            this.db = dbGetter;
        }

        private List<T> GetEntities<T>(string dbQuery) where T : new()
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


        #region Single Entities.

        private T GetSingle<T>(string query) where T : class, IIdentifiable, new()
        {
            var dict = db.GetCollection(query);

            if (dict == null || dict.Count != 1)
                return null;

            var entity = new T();
            entity.SetProperties(dict[0]);

            return entity;
        }

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

        public Student GetStudent(int id)
        {
            return GetSingle<Student>($"{Student} WHERE s.ID = {id}");
        }
        
        public Company GetCompany(int id)
        {
            return GetSingle<Company>("SELECT * FROM company WHERE ID = " + id);
        }

        public StudentCompany GetStudentCompany(int id)
        {
            return GetSingle<StudentCompany>($"{StudentCompany} WHERE s.ID = {id}");
        }

        public Vacancy GetVacancy(int id)
        {
            return GetSingle<Vacancy>($"{Vacancy} WHERE v.ID = {id}");
        }

        public Faculty GetFaculty(int id)
        {
            return GetSingle<Faculty>($"{Faculty} WHERE f.ID = {id}");
        }

        public Specialization GetSpecialization(int id)
        {
            return GetSingle<Specialization>($"{Specialization} WHERE s.ID = {id}");
        }

        public PreferentialCategory GetPreferentialCategory(int id)
        {
            return GetSingle<PreferentialCategory>($"{PreferentialCategory} WHERE ID = {id}");
        }

        #endregion


        public List<Student> GetStudents(string dbQuery)
        {
            return GetEntities<Student>(dbQuery);
        }

        public List<Student> GetStudents()
        {
            return GetEntities<Student>(Student);
        }

        public List<Company> GetCompanies(string dbQuery)
        {
            return GetEntities<Company>(dbQuery);
        }

        public List<Company> GetCompanies()
        {
            return GetEntities<Company>(Company);
        }

        public List<StudentCompany> GetCompaniesByStudentID(int id)
        {
            return GetEntities<StudentCompany>(StudentCompany + $" WHERE s1.ID = {id}");
        }

        public List<StudentCompany> GetSudentsByCompanyID(int id)
        {
            return GetEntities<StudentCompany>(StudentCompany + $" WHERE v.Employer= {id}");
        }

        public List<StudentCompany> GetStudentCompanies(string dbQuery)
        {
            return GetEntities<StudentCompany>(dbQuery);
        }

        public List<StudentCompany> GetStudentCompanies()
        {
            return GetEntities<StudentCompany>(StudentCompany);
        }

        public List<Vacancy> GetVacanciesByCompanyID(int id)
        {
            return GetEntities<Vacancy>(Vacancy + $" WHERE c.ID = {id}");
        }

        public List<Vacancy> GetVacancies(string dbQuery)
        {
            return GetEntities<Vacancy>(dbQuery);
        }

        public List<Vacancy> GetVacancies()
        {
            return GetEntities<Vacancy>(Vacancy);
        }

        public List<Faculty> GetFaculties(string dbQuery)
        {
            return GetEntities<Faculty>(dbQuery);
        }

        public List<Faculty> GetFaculties()
        {
            return GetEntities<Faculty>(Faculty);
        }

        public List<Faculty> GetFaculties(EducationLevelType type)
        {
            return GetEntities<Faculty>("SELECT f.ID, f.Name FROM specialization s INNER JOIN faculty f ON s.Faculty = f.ID WHERE s.LevelOfEducation = " + (int)type + " GROUP BY f.Name");
        }

        public List<Specialization> GetSpecializations(string dbQuery)
        {
            return GetEntities<Specialization>(dbQuery);
        }

        public List<Specialization> GetSpecializations()
        {
            return GetEntities<Specialization>(Specialization);
        }

        public List<PreferentialCategory> GetPreferentialCategories(string dbQuery)
        {
            return GetEntities<PreferentialCategory>(dbQuery);
        }

        public List<PreferentialCategory> GetPreferentialCategories()
        {
            return GetEntities<PreferentialCategory>(PreferentialCategory);
        }

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
