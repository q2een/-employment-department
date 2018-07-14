using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public interface IVacancy: IIdentifiable
    {
        [DisplayName("ID")]
        int ID { get; set; }

        [DisplayName("Шифр вакансии")]
        string VacancyNumber { get; set; }

        [DisplayName("Должность")]
        string  Post { get; set; }

        [DisplayName("Предприятие")]
        string CompanyName { get;}

        int Employer { get; set; }
        
        [DisplayName("Рабочая область")]
        string  WorkArea { get; set; }

        [DisplayName("Оклад")]
        decimal Salary { get; set; }

        bool? IsActive { get; set; }

        [DisplayName("Условия оплаты труда")]
        string SalaryNote { get; set; }

        [DisplayName("Пол")]
        string GenderName { get;}

        long Gender { get; set; }

        [DisplayName("Дополнительная информация")]
        string Features { get; set; }
    }
}
