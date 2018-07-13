using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public interface IDataBase
    {
        string Connection { get;}

        UserRole GetUserRole();

        IEnumerable<Dictionary<string, object>> GetCollection(string query);

        DataTable GetDataTable(string query);

        long Insert(string tableName, Dictionary<string, object> fields);

        void Update(string tableName, int id, Dictionary<string, object> fields);

        void Query(string query);
    }
}
