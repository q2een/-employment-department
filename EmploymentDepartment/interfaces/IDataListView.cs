namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет интерфейс укзывающий, что реализующий его класс, предназначен для просмотра коллекции данных(у которой есть источник данных) и некоторых операций над этими данными.
    /// </summary>
    /// <typeparam name="T">Объект класса, который можно идентифицировать</typeparam>
    public interface IDataListView<T> : IDataView, IDataSourceView where T: BL.IIdentifiable
    {
        /// <summary>
        /// Возвращает коллекцию данных.
        /// </summary>
        System.Collections.Generic.IEnumerable<T> Data { get; }

        /// <summary>
        /// Задает строку для объекта класса <c>DataTable</c> в зависимости от сущности.
        /// </summary>
        /// <param name="entity">Сущность</param>
        void SetDataTableRow(T entity);

        /// <summary>
        /// Задает строку для объекта класса <c>DataTable</c> в зависимости от сущности.
        /// </summary>
        /// <param name="entity">Сущность</param>
        void RemoveDataTableRow(T entity);

        /// <summary>
        /// Возвращает выбранную сущность.
        /// </summary>
        /// <returns>Выбранная сущность</returns>
        T GetSelectedEntity();  
    }
}
