namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Предоставляет перечислитель, указывающий права пользователя.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Нет прав.
        /// </summary>
        None,
        /// <summary>
        /// Редактор.
        /// </summary>
        Moderator,
        /// <summary>
        /// Администратор.
        /// </summary>
        Administrator
    }
}
