using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public interface IDataView
    {
        void View();
        void Insert();
        void Edit();
        void Remove();
    }
}
