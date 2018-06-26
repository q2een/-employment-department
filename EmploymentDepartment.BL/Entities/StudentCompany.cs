using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class StudentCompany
    {
        public int ID { get; set; }
        public int Student { get; set; }
        public string CompanyName { get; set; }
        public bool Status { get; set; }
        public int? Vacancy { get; set; }
        public string Post { get; set; }
        public string Note { get; set; }
    }
}
