using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public interface IDataSourceView
    {
        System.Windows.Forms.BindingSource DataSource { get; }

    }
}
