using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class StudentCompany : IStudentCompany
    {
        public int ID { get; set; }
        public int Student { get; set; }
        public string CompanyName { get; set; }
        public bool Status { get; set; }
        public int? Vacancy { get; set; }
        public string Post { get; set; }
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
