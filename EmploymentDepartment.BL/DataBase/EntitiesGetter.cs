using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class EntitiesGetter
    {
        private readonly IDBGetter db;

        private string Student
        {
            get
            {
                return "SELECT s.ID, s.ApplicationFormNumber, s.Name, s.Surname, s.Patronymic, s.DOB, s.Gender + 0 AS Gender," +
                       "s.MaritalStatus, s.YearOfGraduation, d.LevelOfEducation, d.Faculty, d.Specialization, s.StudyGroup," +
                       "s.Rating, s.PreferentialCategory, s.SelfEmployment, s.City, s.Region, s.District, s.Address, s.RegCity," +
                       "s.RegRegion, s.RegDistrict, s.RegAddress, s.Phone, s.Email FROM student s INNER JOIN" +
                       "(SELECT sp.LevelOfEducation + 0 as LevelOfEducation, sp.Name AS Specialization, f.ID AS Faculty, " +
                       "sp.ID FROM specialization sp INNER JOIN faculty f ON sp.Faculty = f.ID) AS d ON s.FieldOfStudy = d.ID";
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
                return "SELECT s.ID, s.Faculty, s.LevelOfEducation + 0 AS LevelOfEducation, s.Name FROM specialization s";
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
                return "SELECT v.ID, v.VacancyNumber, v.Post, v.Employer, v.WorkArea, v.Salary, v.IsActive, v.SalaryNote, v.Gender + 0 AS Gender, v.Features FROM vacancy v";
            }
        }

        private string StudentCompany
        {
            get
            {
                return "SELECT * FROM studentcompany";
            }
        }

        public EntitiesGetter(IDBGetter dbGetter)
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

        public List<StudentCompany> GetStudentCompanies(string dbQuery)
        {
            return GetEntities<StudentCompany>(dbQuery);
        }

        public List<StudentCompany> GetStudentCompanies()
        {
            return GetEntities<StudentCompany>(StudentCompany);
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
    }
}
