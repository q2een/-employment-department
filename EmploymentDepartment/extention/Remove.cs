using EmploymentDepartment.BL;
using System;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет статический класс, содержащий методы для удаления данных из БД через интерфейс WinForms.
    /// </summary>
    public static class Remove
    {
        /// <summary>
        /// Удаляет сущность из БД и возвращает истину в случает успешного выполнения операции.
        /// </summary>
        /// <param name="entity">Объект сущности</param>
        /// <param name="getter">Экземпляр класса, реализующего интерфейс <c>IEntityGetter</c></param>
        /// <returns>Результат выполнения операции</returns>
        public static bool RemoveEntity<T>(this T entity, IEntityGetter getter) where T : class, IIdentifiable
        {
            if (entity.GetDialogResultByEntityType() != DialogResult.OK)
                return false;

            try
            {
                getter.RemoveEntity(entity);
                return true;
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message, "Запись не удалена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Возаращает возвращаемое значение диалогового окна в зависимости от типа сущности.
        private static DialogResult GetDialogResultByEntityType<T>(this T entity)
        {
            if (entity is IStudent)
                return Student();
            if (entity is ICompany)
                return Company();
            if (entity is IFaculty)
                return Faculty();
            if (entity is IPreferentialCategory)
                return PreferentialCategory();
            if (entity is ISpecialization)
                return Specialization();
            if (entity is IStudentCompany)
                return StudentCompany(entity as IStudentCompany);
            if (entity is IVacancy)
                return Vacancy();

            return DialogResult.Abort;
        }

        #region Диалоговыые окна для разных сущностей.
        private static DialogResult PreferentialCategory()
        {
            return GetMessageBoxResult("При удалении льготной категории все ссылки на нее будут удалены из анкет студентов. Продолжить ?");
        }
        private static DialogResult Specialization()
        {
            return GetMessageBoxResult("Удалить выбранный профиль подготовки ?");
        }
        private static DialogResult Faculty()
        {
            return GetMessageBoxResult("Удалить выбранный факультет ?");
        }
        private static DialogResult Vacancy()
        {
            var msg = "Место работы, привязанное к выбранной вакансии не будет удалено. Если Вы хотите удалить и место работы и вакансию необходимо удалить место работы. Продолжить удаление выбранной вакансии ?";

            return GetMessageBoxResult(msg);
        }
        private static DialogResult StudentCompany(IStudentCompany studentCompany)
        {  
            var msg = studentCompany.Vacancy == null ? "Удалить место работы ?" :"Будет удалено и место работы, и вакансия, привязанная к нему. Продолжить удаление места работы ?";

            return GetMessageBoxResult(msg);
        }
        private static DialogResult Company()
        {
            return GetMessageBoxResult("Удалить выбранное предприятие ?");
        }
        private static DialogResult Student ()
        {
            return GetMessageBoxResult("Вместе с анкетой студента будет удалено и место работы, и вакансия, привязанная к нему. Продолжить удаление анкеты студента ?");
        }
        #endregion

        // Возвращает возвращаемое значение диалогового окна. 
        private static DialogResult GetMessageBoxResult(string message)
        {
            return MessageBox.Show(message, "Подтверждение удаления", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}
