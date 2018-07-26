using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Предоставляет интерфейс для работы с СУБД.
    /// </summary>
    public interface IDataBase
    {
        /// <summary>
        /// Возвращает строку подключения к БД.
        /// </summary>
        string Connection { get; }

        /// <summary>
        /// Возвращает или задает экземпляр класса для работы с сущностями из БД. Позволяет получать и удалять сущности.
        /// </summary>
        IEntityGetter Entities { get; set; }

        /// <summary>
        /// Возвращает или задает экземпляр класса для экспорта данных и формирования отчетов.
        /// </summary>
        IExport Export { get; set; }

        /// <summary>
        /// Возвращает права текущего пользователя, определенные в СУБД.
        /// </summary>
        /// <returns>Права пользователя</returns>
        UserRole GetUserRole();

        /// <summary>
        /// Возвращает коллекцию коллекций "Ключ" - "Значение", где ключ - имя поля, а значение - его значение для данной записи.
        /// </summary>
        /// <param name="query">Запрос к БД</param>
        /// <returns>Коллекцию коллекций "Ключ" - "Значение"</returns>
        IEnumerable<Dictionary<string, object>> GetCollection(string query);

        /// <summary>
        /// Возвращает экземпляр класса <c>DataTable</c> содержащий данные, возвращенные СУБД для данного запроса.
        /// </summary>
        /// <param name="query">Запрос к БД</param>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetDataTable(string query);

        /// <summary>
        /// Выполняет добавление записи в указанную таблицу и возвращает идентификатор добавленной записи.
        /// </summary>
        /// <param name="tableName">Таблица в БД</param>
        /// <param name="fields">Коллекция "Ключ-Значение", где ключ - имя поля, а значение - его значение для добавляемой записи</param>
        /// <returns>Идентификатор добавленной записи</returns>
        long Insert(string tableName, Dictionary<string, object> fields);

        /// <summary>
        /// Выполняет обновление записи с указанным идентификатором в указанной таблице.
        /// </summary>
        /// <param name="tableName">Наименование таблицы</param>
        /// <param name="id">Идентификатор записи</param>
        /// <param name="fields">Коллекция "Ключ-Значение", где ключ - имя поля, а значение - его значение для обновляемой записи</param>
        void Update(string tableName, int id, Dictionary<string, object> fields);

        /// <summary>
        /// Выолняет запрос к БД.
        /// </summary>
        /// <param name="query">Запрос к БД</param>
        void Query(string query);
    }
}
