using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface ICompany
    {
        int ID { get; set; }
        string CompanyNumber { get; set; }
        string Name { get; set; }
        string NameOfStateDepartment { get; set; }
        string City { get; set; }
        string Region { get; set; }
        string District { get; set; }
        string Address { get; set; }
        string DirectorName { get; set; }
        string DirectorSurname { get; set; }
        string DirectorPatronymic { get; set; }
        string ContactName { get; set; }
        string ContactSurname { get; set; }
        string ContactPatronymic { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string Note { get; set; }
    }
}
