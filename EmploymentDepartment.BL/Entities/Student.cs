using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class Student : IIdentifiable
    {
        public int ID { get; set; }
        public string ApplicationFormNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DOB { get; set; }
        public GenderType Gender { get; set; }
        public bool MaritalStatus { get; set; }
        public int YearOfGraduation { get; set; }
        public int Faculty { get; set; }
        public int FieldOfStudy { get; set; }
        public string StudyGroup { get; set; }
        public decimal Rating { get; set; }
        public string PreferentialCategory { get; set; }
        public bool SelfEmployment { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string RegCity { get; set; }
        public string RegRegion { get; set; }
        public string RegDistrict { get; set; }
        public string RegAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
