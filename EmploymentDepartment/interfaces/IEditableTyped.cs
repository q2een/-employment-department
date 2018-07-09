namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет интерфейс указывающий на изменяемость и редактируемость данных.
    /// </summary>
    public interface IEditable<out T> : IEditable where T: BL.IIdentifiable
    {
        T Entity { get;}
    }
}
