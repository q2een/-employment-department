using System;

namespace EmploymentDepartment.BL
{
    public interface IStudent
    {
        int ID { get;}
        string ApplicationFormNumber { get;}
        string Surname { get;}
        string Name { get;}
        string Patronymic { get;}
        DateTime DOB { get;}
        GenderType Gender { get;}
        bool MaritalStatus { get;}
        int YearOfGraduation { get;}
        int Faculty { get;}
        EducationLevelType LevelOfEducation { get;}
        int FieldOfStudy { get;}
        string StudyGroup { get;}
        decimal Rating { get;}
        string PreferentialCategory { get;}
        bool SelfEmployment { get;}
        string City { get;}
        string Region { get;}
        string District { get;}
        string Address { get;}
        string RegCity { get;}
        string RegRegion { get;}
        string RegDistrict { get;}
        string RegAddress { get;}
        string Phone { get;}
        string Email { get;}
    }
}
