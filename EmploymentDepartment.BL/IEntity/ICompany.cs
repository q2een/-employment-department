using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public interface ICompany : IIdentifiable
    {
        [DisplayName("ID")]
        int ID { get; set; }

        [DisplayName("Шифр предприятия")]
        string CompanyNumber { get; set; }

        [DisplayName("Название")]
        string Name { get; set; }

        [DisplayName("Название гос. органа")]
        string NameOfStateDepartment { get; set; }

        [DisplayName("Город")]
        string City { get; set; }

        [DisplayName("Область")]
        string Region { get; set; }

        [DisplayName("Район")]
        string District { get; set; }

        [DisplayName("Адрес")]
        string Address { get; set; }

        [DisplayName("Имя (Директор)")]
        string DirectorName { get; set; }

        [DisplayName("Фамилия (Директор)")]
        string DirectorSurname { get; set; }

        [DisplayName("Отчество (Директор)")]
        string DirectorPatronymic { get; set; }

        [DisplayName("Имя (Контактное лицо)")]
        string ContactName { get; set; }

        [DisplayName("Фамилия (Контактное лицо)")]
        string ContactSurname { get; set; }

        [DisplayName("Отчество (Контактное лицо)")]
        string ContactPatronymic { get; set; }

        [DisplayName("Телефон")]
        string Phone { get; set; }

        [DisplayName("Электронный адрес")]
        string Email { get; set; }

        [DisplayName("Примечание")]
        string Note { get; set; }
    }

}
