namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Представляет объект, который можно идентифицировать.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>             
        int ID { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        string Name { get; set; }
    }
}
