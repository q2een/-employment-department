using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class Vacancy : IVacancy
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Шифр вакансии")]
        public string VacancyNumber { get; set; }

        [DisplayName("Должность")]
        public string Post { get; set; }

        [DisplayName("Предприятие")]
        public string CompanyName { get; set; }

        public int Employer { get; set; }

        [DisplayName("Рабочая область")]
        public string WorkArea { get; set; }

        [DisplayName("Оклад")]
        public decimal Salary { get; set; }

        public bool? IsActive { get; set; }

        [DisplayName("Условия оплаты труда")]
        public string SalaryNote { get; set; }

        [DisplayName("Пол")]
        public string GenderName { get; set; }

        public int Gender { get; set; }

        [DisplayName("Дополнительная информация")]
        public string Features { get; set; }

        string IIdentifiable.Name
        {
            get
            {
                return VacancyNumber;
            }
            set
            {

            }
        }
    }
}
