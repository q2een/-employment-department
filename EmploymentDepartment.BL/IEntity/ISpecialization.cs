using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface ISpecialization : IIdentifiable
    {
        int ID { get; set; }
        int Faculty { get; set; }
        int LevelOfEducation { get; set; }
        string Name { get; set; } 
    }
}
