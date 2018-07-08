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

        [DisplayName("Предприятие")]
        string CompanyName { get; set; }
        
        bool Status { get; set; }              

        int? Vacancy { get; set; }

        [DisplayName("Должность")]
        string Post { get; set; }

        [DisplayName("Статус")]
        string StatusText { get;}

        [DisplayName("Шифр вакансии")]
        string VacancyNumber { get;}

        [DisplayName("Примечание")]
        string Note { get; set; }
    }
}
