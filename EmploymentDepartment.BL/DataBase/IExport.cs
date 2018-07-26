using System.Data;

namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Предоставляет интерфейс для экспорта данных и формирования отчетов.
    /// </summary>
    public interface IExport
    {
        /// <summary>
        /// Возвращает все записи о студентах из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetStudents();

        /// <summary>
        /// Возвращает все записи о предприятиях из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetCompanies();

        /// <summary>
        /// Возвращает все записи о местах работы студента из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetStudentCompanies();

        /// <summary>
        /// Возвращает все записи о вакансиях из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetVacancies();

        /// <summary>
        /// Возвращает все записи о факультетах из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetFaculties();

        /// <summary>
        /// Возвращает все записи о направлениях подготовки из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetSpecializations();

        /// <summary>
        /// Возвращает все записи о льготных категориях из БД.
        /// </summary>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetPreferentialCategories();

        /// <summary>
        /// Возвращает данные для отчета "Ведомость распределения выпускников, которые окончили ВУЗ" в заданном году <c>year</c>.
        /// </summary>
        /// <param name="year">Год окончиния ВУЗа</param>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetStatementReport(int year);

        /// <summary>
        /// Возвращает данные для отчета "Ведомость персонального учета выпускников".
        /// </summary>
        /// <param name="fielOfStudy">Направление подготовки</param>
        /// <param name="studyGroup">Группа</param>
        /// <param name="year">Год окончания ВУЗа</param>
        /// <returns>Экземпляр класса <c>DataTable</c></returns>
        DataTable GetPersonalAccountReport(int fielOfStudy, string studyGroup, int year);

        /// <summary>
        /// Возвращает год окончания ВУЗа для студентов заданного группы и направления подготовки
        /// </summary>
        /// <param name="fielOfStudy">Направление подготовки</param>
        /// <param name="studyGroup">Группа</param>
        /// <returns>Год окночания ВУЗа</returns>
        int GetYearOfGraduation(int fielOfStudy, string studyGroup);
    }
}
