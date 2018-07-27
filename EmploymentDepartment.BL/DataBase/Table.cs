using System;

namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Предоставляет статический класс, содержащий методы, связанные со структурой БД.
    /// </summary>
    public static class Tables
    {
        /// <summary>
        /// Возвращает наименование таблицы в БД, которой соответствует тип переданного объекта.
        /// </summary>
        /// <param name="obj">Объект, для которого необходимо определить наименование таблицы в БД</param>
        /// <returns>Наименование таблицы в БД</returns>
        public static Table GetTableNameByType<T>(T obj) where T : class, IIdentifiable
        {
            if (obj is IStudent)
                return Table.student;
            if (obj is ICompany)
                return Table.company;
            if (obj is IFaculty)
                return Table.faculty;
            if (obj is IPreferentialCategory)
                return Table.preferentialcategory;
            if (obj is ISpecialization)
                return Table.specialization;
            if (obj is IStudentCompany)
                return Table.studentcompany;
            if (obj is IVacancy)
                return Table.vacancy;

            throw new ArgumentException();
        }
    }

    /// <summary>
    /// Предоставляет пречислитель, содержащий наименования таблиц в БД.
    /// </summary>
    public enum Table
    {
        /// <summary>
        /// Студент.
        /// </summary>
        student,
        /// <summary>
        /// Предприятие.
        /// </summary>
        company,
        /// <summary>
        /// Факультет.
        /// </summary>
        faculty,
        /// <summary>
        /// Профиль подготовки.
        /// </summary>
        specialization,
        /// <summary>
        /// Вакансия.
        /// </summary>
        vacancy,
        /// <summary>
        /// Места работы студента.
        /// </summary>
        studentcompany,
        /// <summary>
        /// Льготная категория.
        /// </summary>
        preferentialcategory
    }
}
