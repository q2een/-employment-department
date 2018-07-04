using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface IStudentCompany : IIdentifiable
    {
        int ID { get; set; }
        int Student { get; set; }
        string CompanyName { get; set; }
        bool Status { get; set; }
        int? Vacancy { get; set; }
        string Post { get; set; }
        string Note { get; set; }
    }
}
