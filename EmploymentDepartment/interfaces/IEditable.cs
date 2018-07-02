using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    interface IEditable
    {
        ActionType Type { get;}
        void SetDefaultValues();
        void SaveChanges();
        void Remove();
        void Add();
    }
}
