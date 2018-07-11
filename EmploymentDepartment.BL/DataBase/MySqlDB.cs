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

        public MySqlDB()
        {

        }

        public MySqlDB(string connection)
        {
            this.connection = connection;
        }

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
                    case 0: throw new Exception("Проверьте правильность введенных данных для подключения");
                    case 1451: throw new Exception("Не удалось выполнить операцию. Данаая запись используется в других таблицах!");
                    case 1042: // Исключения при отсутствии соединения
                    case 1044: // Или неправильно введенной комбинации "имя пользователя - пароль"
                    case 1045: throw new Exception("Не удалось подключиться к базе данных. Возможно, отсутствует подключение к Интернет или база данных недоступна");
                    case 1264: throw new Exception("Проверьте правильность введенных данных!");
                    default: throw new Exception("Ошибка при обращении к базе данных.Ошибка №" + MySqlEx.Number);
                }
            }

            return queryList;
        }

        public DataTable GetDataTable(string query)
        {
            var table = new DataTable();
            try
            {
                using (var conn = new MySqlConnection(this.connection))
                {
                    conn.Open();

                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        using (var da = new MySqlDataAdapter(cmd))
                            da.Fill(table);
                    }
                }
            }
            catch (MySqlException MySqlEx)
            {
                throw ExecptionHandler(MySqlEx);
            }

            return table;
        }

        private Exception ExecptionHandler(MySqlException MySqlEx)
        {
            switch (MySqlEx.Number)
            {
                case 0: return new Exception("Проверьте правильность введенных данных для подключения");
                case 1451: return new Exception("Не удалось выполнить операцию. Данаая запись используется в других таблицах!");
                case 1042: // Исключения при отсутствии соединения
                case 1044: // Или неправильно введенной комбинации "имя пользователя - пароль"
                case 1045: return new Exception("Не удалось подключиться к базе данных. Возможно, отсутствует подключение к Интернет или база данных недоступна");
                case 1264: return new Exception("Проверьте правильность введенных данных!");
                default: return new Exception("Ошибка при обращении к базе данных.Ошибка №" + MySqlEx.Number);
            }
        }

        public long Insert(string tableName, Dictionary<string, object> nameValue)
        {
            if (nameValue == null || nameValue.Count == 0)
                throw new ArgumentNullException();
            
            string fields = "", values = "";

            foreach (var kv in nameValue)
            {
                var value = TypeValidator(kv.Value);

                fields += $"{kv.Key},";
                values += $"{value},";
            }

            try
            {
                using (var conn = new MySqlConnection(this.connection))
                {
                    var command = new MySqlCommand($"INSERT INTO {tableName} ({fields.TrimEnd(',')}) VALUES ({values.TrimEnd(',')})", conn);
                    conn.Open();
                    command.ExecuteNonQuery();
                    return command.LastInsertedId;
                }
            }
            catch (MySqlException MySqlEx)
            {
                throw ExecptionHandler(MySqlEx);
            }
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

        public object GetSimple(string query)
        {
            try
            {
                using (var conn = new MySqlConnection(this.connection))
                {
                    var command = new MySqlCommand(query, conn);
                    conn.Open();
                    return command.ExecuteScalar();
                }
            }
            catch (MySqlException MySqlEx)
            {
                throw ExecptionHandler(MySqlEx);
            }
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
                throw ExecptionHandler(MySqlEx);
            }
        }
    }
}
