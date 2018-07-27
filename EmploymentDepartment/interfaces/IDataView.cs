namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет интерфейс укзывающий, что реализующий его класс, предназначен для просмотра коллекции данных и некоторых операций над этими данными.
    /// </summary>
    public interface IDataView
    {
        /// <summary>
        /// Количество элементов.
        /// </summary>
        int ItemsCount { get; }

        /// <summary>
        /// Тип просмотра.
        /// </summary>
        ViewType Type { get;}

        /// <summary>
        /// Показать подробную информацию о элементе
        /// </summary>
        void View();

        /// <summary>
        /// Добавить новый элемент.
        /// </summary>
        void Insert();

        /// <summary>
        /// Редактировать элемент.
        /// </summary>
        void Edit();

        /// <summary>
        /// Удалить эелемент.
        /// </summary>
        void Remove();
    }
}
