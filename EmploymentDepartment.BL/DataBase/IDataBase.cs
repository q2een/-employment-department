using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface IDataBase
    {
        List<Dictionary<string, object>> GetCollection(string query);
        long Insert(string tableName, Dictionary<string, object> fields);
        void Update(string tableName, int id, Dictionary<string, object> fields);
        object GetSimple(string query);
    }
}
