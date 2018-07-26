using System.Collections.Generic;

namespace EmploymentDepartment.BL
{
    /// <summary>
    /// Предоставляет интерфейс для работы с сущностями из БД. Позволяет получать и удалять сущности.
    /// </summary>
    public interface IEntityGetter
    { 
        /// <summary>
        /// Возвращает коллекцию сущностей из БД.
        /// </summary>
        /// <returns>Коллекция сущностей из БД</returns>
        IEnumerable<T> GetEntities<T>() where T : class, IIdentifiable;

        /// <summary>
        /// Возвращает сущность из БД по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность из БД</returns>
        T GetSingle<T>(int id) where T : class, IIdentifiable;
        
        /// <summary>
        /// Удаляет из БД запись, соответствующую сущности.
        /// </summary>
        /// <param name="entity">Сущность, соответствующая удаляемой записи</param>
        void RemoveEntity<T>(T entity) where T : IIdentifiable;

        /// <summary>
        /// Возвращает коллекцию факультетов на которых есть заданный уровень образования.
        /// </summary>
        /// <param name="type">Уровень образования</param>
        /// <returns>Коллекция факультетов</returns>
        IEnumerable<IFaculty> GetFaculties(EducationLevelType type);

        /// <summary>
        /// Возвращает коллекцию мест работы по идентификатору студента.
        /// </summary>
        /// <param name="id">Идентификатор записи студента</param>
        /// <returns>Коллекция мест работы студента</returns>
        IEnumerable<IStudentCompany> GetCompaniesByStudentID(int id);

        /// <summary>
        /// Возвращает коллекцию мест работы по идентификатору предприятия.
        /// </summary>
        /// <param name="id">Идентификатор записи предприятия</param>
        /// <returns>Коллекция мест работы на предприятии</returns>
        IEnumerable<IStudentCompany> GetSudentsByCompanyID(int id);

        /// <summary>
        /// Возвращает коллекцию мест работы по идентификатору вакансии.
        /// </summary>
        /// <param name="id">Идентификатор записи вакансии</param>
        /// <returns>Коллекция мест работы по заданной вакансии</returns>
        IEnumerable<IStudentCompany> GetSudentsByVacancyID(int id);

        /// <summary>
        /// Возвращает коллекцию вакансий по идентификатору предприятия.
        /// </summary>
        /// <param name="id">Идентификатор записи предприятия</param>
        /// <returns>Коллекция вакансий на предприятии</returns> 
        IEnumerable<IVacancy> GetVacanciesByCompanyID(int id);
    }
}
