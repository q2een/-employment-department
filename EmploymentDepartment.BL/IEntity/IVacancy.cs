using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface IVacancy
    {
        int ID { get; set; }
        string VacancyNumber { get; set; }
        string Post { get; set; }
        int Employer { get; set; }
        string WorkArea { get; set; }
        decimal Salary { get; set; }
        bool? IsActive { get; set; }
        string SalaryNote { get; set; }
        int Gender { get; set; }
        string Features { get; set; }
    }
}
