using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface IDBGetter
    {
        List<Dictionary<string, object>> GetCollection(string query);
    }
}
