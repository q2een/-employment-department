using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public interface IIdentifiable
    {
        int ID { get; set; }
        string Name { get; set; }
    }
}
