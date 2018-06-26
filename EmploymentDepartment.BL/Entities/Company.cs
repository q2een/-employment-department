using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class Company
    {
        public int ID { get; set; }   
        public string CompanyNumber { get; set; }
        public string Name { get; set; }
        public string NameOfStateDepartment { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string DirectorName { get; set; }
        public string DirectorSurname { get; set; }
        public string DirectorPatronymic { get; set; }
        public string ContactName { get; set; }
        public string ContactSurname { get; set; }
        public string ContactPatronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }
}
