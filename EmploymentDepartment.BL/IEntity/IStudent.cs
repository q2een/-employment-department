using System;
using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public interface IStudent : IIdentifiable
    {
        [DisplayName("ID")]
        int ID { get; set; }

        [DisplayName("Шифр анкеты")]
        string ApplicationFormNumber { get; set; }

        [DisplayName("Фамилия")]
        string Surname { get; set; }

        [DisplayName("Имя")]
        string Name { get; set; }

        [DisplayName("Отчество")]
        string Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        DateTime DOB { get; set; }

        [DisplayName("Пол")]
        string GenderName { get;}

        int Gender { get; set; }

        [DisplayName("Семейное положение")]
        string MartialStatusString { get;}

        bool MaritalStatus { get; set; }

        [DisplayName("Год окончания университета")]
        int YearOfGraduation { get; set; }

        [DisplayName("Факультет")]
        string FacultyName { get; }

        EducationLevelType LevelOfEducation { get; set; }

        int Faculty { get; set; }

        [DisplayName("Уровень образования")]
        string EducationLevel { get;}

        [DisplayName("Профиль(Специализация)")]
        string Specialization { get;}

        int FieldOfStudy { get; set; }

        [DisplayName("Группа")]
        string StudyGroup { get; set; }

        [DisplayName("Рейтинг")]
        decimal Rating { get; set; }

        [DisplayName("Льготная категория")]
        string PreferentialCategoryText { get;}

        int? PreferentialCategory { get; set; }

        [DisplayName("Самостоятельное трудоустройство")]
        string SelfEmploymentText { get; }

        bool SelfEmployment { get; set; }

        [DisplayName("Город (проживание)")]
        string City { get; set; }

        //[DisplayName("Область (проживание)")]
        string Region { get; set; }

        //[DisplayName("Район (проживание)")]
        string District { get; set; }

        //[DisplayName("Адрес (проживание)")]
        string Address { get; set; }

        //[DisplayName("Город (регистрация)")]
        string RegCity { get; set; }

        //[DisplayName("Область (регистрация)")]
        string RegRegion { get; set; }

        //[DisplayName("Район (регистрация)")]
        string RegDistrict { get; set; }

        //[DisplayName("Адрес (регистрация)")]
        string RegAddress { get; set; }

        [DisplayName("Телефон")]
        string Phone { get; set; }

        //[DisplayName("Электронный адрес")]
        string Email { get; set; }
    }
}
