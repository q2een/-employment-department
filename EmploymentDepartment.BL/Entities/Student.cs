using System;
using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class Student : IIdentifiable, IStudent
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Шифр анкеты")]
        public string ApplicationFormNumber { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime DOB { get; set; }

        [DisplayName("Пол")]
        public string GenderName
        {
            get
            {
                return Gender == 1 ? "Мужской" : "Женский";
            }
        }

        public int Gender { get; set; }

        [DisplayName("Семейное положение")]
        public string MartialStatusString
        {
            get
            {
                return !MaritalStatus ? "Не женат\n(Не замужем)" : "Женат\n(Замужем)";
            }
        }

        public bool MaritalStatus { get; set; }

        [DisplayName("Год окончания университета")]
        public int YearOfGraduation { get; set; }

        [DisplayName("Факультет")]
        public string FacultyName { get; set; }

        public EducationLevelType LevelOfEducation { get; set; }

        public int Faculty { get; set; }
         
        [DisplayName("Уровень образования")]
        public string EducationLevel
        {
            get
            {
                switch (LevelOfEducation)
                {
                    case EducationLevelType.Bachelor:
                        return "Бакалавриат";
                    case EducationLevelType.Specialist:
                        return "Специалитет";
                    default:
                        return "Магистратура";
                }
            }
        }

        [DisplayName("Профиль(Специализация)")]
        public string Specialization { get; set; }

        public int FieldOfStudy { get; set; }

        [DisplayName("Группа")]
        public string StudyGroup { get; set; }

        [DisplayName("Рейтинг")]
        public decimal Rating { get; set; }

        [DisplayName("Льготная категория")]
        public string PreferentialCategoryText
        {
            get
            {
                return PreferentialCategory == null ? "Нет" : "Да";
            }
        }

        public int? PreferentialCategory { get; set; }

        [DisplayName("Самостоятельное трудоустройство")]
        public string SelfEmploymentText
        {
            get
            {
                return SelfEmployment ? "Да" : "Нет";
            }
        }

        public bool SelfEmployment { get; set; }

        [DisplayName("Город (проживание)")]
        public string City { get; set; }

        //[DisplayName("Область (проживание)")]
        public string Region { get; set; }

        //[DisplayName("Район (проживание)")]
        public string District { get; set; }

        //[DisplayName("Адрес (проживание)")]
        public string Address { get; set; }

        //[DisplayName("Город (регистрация)")]
        public string RegCity { get; set; }

        //[DisplayName("Область (регистрация)")]
        public string RegRegion { get; set; }

        //[DisplayName("Район (регистрация)")]
        public string RegDistrict { get; set; }

        //[DisplayName("Адрес (регистрация)")]
        public string RegAddress { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }

        //[DisplayName("Электронный адрес")]
        public string Email { get; set; }
    }
}
