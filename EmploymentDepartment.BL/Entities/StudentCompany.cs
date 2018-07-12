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
         
        public int? Vacancy { get; set; }
        
        [DisplayName("Название гос. органа")]
        public string NameOfStateDepartment { get; set; }
        
        [DisplayName("Предприятие")]
        public string NameOfCompany { get; set; }

        public bool Status { get; set; }
        
        [DisplayName("Должность")]
        public string Post { get; set; }

        [DisplayName("Оклад")]
        public decimal? Salary { get; set; }

        [DisplayName("Год трудоустройства")]
        public int YearOfEmployment { get; set; }

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
                return NameOfCompany;
            }
            set
            {
            }
        }
    }
}
