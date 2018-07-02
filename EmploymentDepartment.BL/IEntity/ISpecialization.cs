using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface ISpecialization
    {
        int ID { get; set; }
        int Faculty { get; set; }
        EducationLevelType LevelOfEducation { get; set; }
        string Name { get; set; } 
    }
}
