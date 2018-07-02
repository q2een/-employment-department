using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public class MySqlDB: IDataBase
    {
        private readonly string connection = "Database=work;Data Source=localhost;User Id=root;Password=root;CharSet=utf8;";

        public List<Dictionary<string, object>> GetCollection(string query)
        {
            List<Dictionary<string, object>> queryList = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new MySqlConnection(this.connection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable schema = reader.GetSchemaTable();
                            while (reader.Read())
                            {
                                var dict = new Dictionary<string, object>();
                                foreach (DataRow row in schema.Rows)
                                {
                                    string s = row[schema.Columns["ColumnName"]].ToString();
                                    object tmp = reader[s];

                                    if (tmp.GetType() == new Double().GetType())
                                        tmp = Int32.Parse(tmp + "");

                                    if (tmp is DBNull)
                                        tmp = null;

                                    dict.Add(s, tmp);
                                }
                                queryList.Add(dict);
                            }
                        }
                    }
                }
            }
            catch (MySqlException MySqlEx)
            {
                switch (MySqlEx.Number)
                {
                    case 1451: throw new Exception("Не удалось выполнить операцию. Данаая запись используется в других таблицах!");
                    case 1042: // Исключения при отсутствии соединения
                    case 1044: // Или неправильно введенной комбинации "имя пользователя - пароль"
                    case 1045: throw new Exception("Не удалось подключиться к базе данных!");
                    case 1264: throw new Exception("Проверьте правильность введенных данных!");
                    default: throw new Exception("Ошибка при обращении к базе данных! #" + MySqlEx.Number);
                }
            }

            return queryList;
        }
        
        public void Insert(string tableName, Dictionary<string, object> nameValue)
        {
            if (nameValue == null || nameValue.Count == 0)
                return;
            
            string fields = "", values = "";

            foreach (var kv in nameValue)
            {
                var value = TypeValidator(kv.Value);

                fields += $"{kv.Key},";
                values += $"{value},";
            }

            Query($"INSERT INTO {tableName} ({fields.TrimEnd(',')}) VALUES ({values.TrimEnd(',')})");
        }

        public void Update(string tableName, int id, Dictionary<string, object> fields)
        {
            if (fields == null || fields.Count == 0)
                return;

            string values = "";
            foreach (var kv in fields)
            {
                object value = TypeValidator(kv.Value);
                
                values += $"{kv.Key} = {value},";
            }

            Query($"UPDATE {tableName} SET {values.TrimEnd(',')} WHERE id = {id}");
        }

        private object TypeValidator(object obj)
        {
            if (obj == null)
                return "NULL";

            if (obj is DateTime)
                obj = ((DateTime)obj).ToString("yyyy/MM/dd");

            if (obj.GetType() == true.GetType())
                obj = (bool)obj ? 1 : 0;

            if (obj.GetType() == ((decimal)0.00).GetType())
                obj = obj.ToString().Replace(",", ".");

            return $"'{obj}'";
        }

        /// <summary>
        /// Выполняет запрос к БД
        /// </summary>
        /// <param name="query">Строка запроса к БД</param>
        private void Query(string query)
        {
            try
            {
                using (var conn = new MySqlConnection(this.connection))
                {
                    var command = new MySqlCommand(query, conn);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException MySqlEx)
            {
                switch (MySqlEx.Number)
                {
                    case 1451: throw new Exception("Не удалось выполнить операцию! Данаая запись используется в других таблицах!");
                    case 1042: // Исключения при отсутствии соединения
                    case 1044: // Или неправильно введенной комбинации "имя пользователя - пароль"
                    case 1045: throw new Exception("Не удалось подключиться к базе данных!");
                    case 1264: throw new Exception("Проверьте правильность введенных данных!");
                    default: throw new Exception("Ошибка при обращении к базе данных! #" + MySqlEx.Number);
                }
            }
        }
    }
}
