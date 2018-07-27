namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет интерфейс указывающий на изменяемость и редактируемость данных.
    /// </summary>
    /// <typeparam name="T">Объект, который можно идентифицовроать</typeparam>
    public interface IEditable<out T> : IEditable where T: BL.IIdentifiable
    {
        /// <summary>
        /// Возвращает сущность.
        /// </summary>
        T Entity { get;}
    }
}
