namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет интерфейс указывающий на изменяемость и редактируемость данных.
    /// </summary>
    public interface IEditable
    {
        /// <summary>
        /// Тип действия. 
        /// </summary>
        ActionType Type { get; }

        /// <summary>
        /// Задает полям исходные значения.
        /// </summary>
        void SetDefaultValues();

        /// <summary>
        /// Сохраняет внесенные изменения.
        /// </summary>
        bool Save();

        /// <summary>
        /// Удаление данных.
        /// </summary>
        void Remove();

        /// <summary>
        /// Добавляет данные.
        /// </summary>
        void AddNewItem();

        /// <summary>
        /// Валидация полей.
        /// </summary>
        /// <returns>Истина если поля валидны</returns>
        bool ValidateFields();
    }
}
