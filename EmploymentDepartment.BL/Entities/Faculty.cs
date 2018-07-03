using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class Faculty : IIdentifiable, IFaculty
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
