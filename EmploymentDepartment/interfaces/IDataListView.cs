using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public interface IDataListView<T>
    {
        List<T> Data { get; set; }
        ViewType Type { get; set; }
    }
}
