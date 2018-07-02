using System;

namespace EmploymentDepartment.BL
{
    public interface IStudent
    {
        int ID { get; set; }
        string ApplicationFormNumber { get; set;}
        string Surname { get; set;}
        string Name { get; set;}
        string Patronymic { get; set;}
        DateTime DOB { get; set;}
        int Gender { get; set;}
        bool MaritalStatus { get; set;}
        int YearOfGraduation { get; set;}
        EducationLevelType LevelOfEducation { get; set;}
        int Faculty { get; set; }
        int FieldOfStudy { get; set;}
        string StudyGroup { get; set;}
        decimal Rating { get; set;}
        int? PreferentialCategory { get; set;}
        bool SelfEmployment { get; set;}
        string City { get; set;}
        string Region { get; set;}
        string District { get; set;}
        string Address { get; set;}
        string RegCity { get; set;}
        string RegRegion { get; set;}
        string RegDistrict { get; set;}
        string RegAddress { get; set;}
        string Phone { get; set;}
        string Email { get; set;}
    }
}
