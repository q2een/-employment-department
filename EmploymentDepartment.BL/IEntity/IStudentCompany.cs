using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public interface IStudentCompany : IIdentifiable
    {
        [DisplayName("ID")]
        int ID { get; set; }

        [DisplayName("ФИО студента")]
        string StudentFullName { get; }

        int Student { get; set; }
        
        int? Vacancy { get; set; }
        
        [DisplayName("Название гос. органа")]
        string NameOfStateDepartment { get; set; }

        [DisplayName("Предприятие")]
        string NameOfCompany { get; set; }
        
        bool Status { get; set; }              

        [DisplayName("Должность")]
        string Post { get; set; }

        [DisplayName("Оклад")]
        decimal? Salary { get; set; }
         
        [DisplayName("Год трудоустройства")]
        int YearOfEmployment { get; set; }

        [DisplayName("Статус")]
        string StatusText { get;}

        [DisplayName("Шифр вакансии")]
        string VacancyNumber { get;}

        [DisplayName("Примечание")]
        string Note { get; set; }
    }
}
