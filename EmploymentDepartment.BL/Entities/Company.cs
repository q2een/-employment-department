using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class Company: IIdentifiable, ICompany
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Шифр предприятия")]
        public string CompanyNumber { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Название гос. органа")]
        public string NameOfStateDepartment { get; set; }

        [DisplayName("Город")]
        public string City { get; set; }

        [DisplayName("Область")]
        public string Region { get; set; }

        [DisplayName("Район")]
        public string District { get; set; }
        
        [DisplayName("Адрес")]
        public string Address { get; set; }
        
        [DisplayName("Имя (Директор)")]
        public string DirectorName { get; set; }
        
        [DisplayName("Фамилия (Директор)")]
        public string DirectorSurname { get; set; }
        
        [DisplayName("Отчество (Директор)")]
        public string DirectorPatronymic { get; set; }
        
        [DisplayName("Имя (Контактное лицо)")]
        public string ContactName { get; set; }
        
        [DisplayName("Фамилия (Контактное лицо)")]
        public string ContactSurname { get; set; }
        
        [DisplayName("Отчество (Контактное лицо)")]
        public string ContactPatronymic { get; set; }
        
        [DisplayName("Телефон")]
        public string Phone { get; set; }
        
        [DisplayName("Электронный адрес")]
        public string Email { get; set; }
        
        [DisplayName("Примечание")]
        public string Note { get; set; }
    }
}
