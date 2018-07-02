using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class Specialization : IIdentifiable, ISpecialization
    {
        public int ID { get; set; }
        public int Faculty { get; set; }
        public EducationLevelType LevelOfEducation { get; set; }
        public string Name { get; set; }        
    }
}
