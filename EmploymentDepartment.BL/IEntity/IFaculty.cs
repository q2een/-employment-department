using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface IFaculty : IIdentifiable
    {
        int ID { get; set; }
        string Name { get; set; }
    }
}
