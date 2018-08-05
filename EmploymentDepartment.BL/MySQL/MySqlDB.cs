using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Предоставляет класс для работы с СУБД MySql.
    /// </summary>
    public class MySqlDB : IDataBase
    {
        // Строка подключения к БД.
        private readonly string connection;

        /// <summary>
        /// Возвращает строку подключения к БД.
        /// </summary>
        public string Connection
        {
            get
            {
                return connection;
            }
        }
        
        /// <summary>
        /// Возвращает или задает экземпляр класса для работы с сущностями из БД (СУБД MySQL). Позволяет получать и удалять сущности.
        /// </summary>
        public IEntityGetter Entities { get; set; }

        /// <summary>
        /// Возвращает или задает экземпляр класса для экспорта данных и формирования отчетов.
        /// </summary>
        public IExport Export { get; set; }

        /// <summary>
        /// Предоставляет класс для работы с СУБД MySql.
        /// </summary>
        /// <param name="connection">Строка подключения к СУБД</param>
        public MySqlDB(string connection)
        {
            this.connection = connection;
            this.Export = new MySqlExport(this);
            this.Entities = new MySqlGetter(this);
        }

        /// <summary>
        /// Возвращает права текущего пользователя, определенные в СУБД.
        /// </summary>
        /// <returns>Права пользователя</returns>
        public UserRole GetUserRole()
        {
            var grants = GetCollection("SHOW GRANTS FOR CURRENT_USER()");

            return GetRole(grants);
        }

        /// <summary>
        /// Возвращает коллекцию коллекций "Ключ" - "Значение", где ключ - имя поля, а значение - его значение для данной записи.
        /// </summary>
        /// <param name="query">Запрос к БД</param>
        /// <returns>Коллекцию коллекций "Ключ" - "Значение"</returns>
        public IEnumerable<Dictionary<string, object>> GetCollection(string query)
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

        /// <summary>
        /// Возвращает экземпляр класса <c>DataTable</c> содержащий данные, возвращенные СУБД для данного запроса.
        /// </summary>
        /// <param name="query">Запрос к БД</param>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
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

        /// <summary>
        /// Выполняет добавление записи в указанную таблицу и возвращает идентификатор добавленной записи.
        /// </summary>
        /// <param name="tableName">Таблица в БД</param>
        /// <param name="fields">Коллекция "Ключ-Значение", где ключ - имя поля, а значение - его значение для добавляемой записи</param>
        /// <returns>Идентификатор добавленной записи</returns>
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

        /// <summary>
        /// Выполняет обновление записи с указанным идентификатором в указанной таблице.
        /// </summary>
        /// <param name="tableName">Наименование таблицы</param>
        /// <param name="id">Идентификатор записи</param>
        /// <param name="fields">Коллекция "Ключ-Значение", где ключ - имя поля, а значение - его значение для обновляемой записи</param>
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
       
        /// <summary>
        /// Выполняет запрос к БД
        /// </summary>
        /// <param name="query">Строка запроса к БД</param>
        public void Query(string query)
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
        
        // Возвращает роль пользователя в зависимости от полученных из БД данных.
        private UserRole GetRole(IEnumerable<Dictionary<string, object>> grants)
        {
            foreach (var dict in grants)
                foreach (var value in dict.Values)
                {
                    if (!value.ToString().Contains("ON *.*") && !value.ToString().Contains("ON `work`"))
                        continue;

                    if (value.ToString().Contains("ALL PRIVILEGES"))
                        return UserRole.Administrator;

                    if (value.ToString().Contains("SELECT") && value.ToString().Contains("INSERT") && value.ToString().Contains("UPDATE"))
                    {
                        if (value.ToString().Contains("DELETE"))
                            return UserRole.Administrator;

                        return UserRole.Moderator;
                    }
                }

            return UserRole.None;
        }
        
        // Возвращает данные в корректном для СУБД MySQL виде.
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

        // Возвращает текст ошибки в зависимости от номера ошибка MySqlEx. 
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
    }
}
