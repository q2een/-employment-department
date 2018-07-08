using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class StudentCompany : IStudentCompany
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("ФИО студента")]
        public string StudentFullName { get; set; }

        public int Student { get; set; }

        [DisplayName("Предприятие")]
        public string CompanyName { get; set; }

        public bool Status { get; set; }

        public int? Vacancy { get; set; }

        [DisplayName("Должность")]
        public string Post { get; set; }

        [DisplayName("Статус")]
        public string StatusText
        {
            get
            {
                return Status ? "Работает" : "Не работает";
            }
        }

        [DisplayName("Шифр вакансии")]
        public string VacancyNumber { get; set; }

        [DisplayName("Примечание")]
        public string Note { get; set; }

        public string Name
        {
            get
            {
                return CompanyName;
            }
            set
            {
            }
        }
    }
}
