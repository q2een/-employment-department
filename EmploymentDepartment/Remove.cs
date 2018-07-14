using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public static class Remove
    {
        public static bool RemoveEntity<T>(this T entity, IEntityGetter getter) where T : class, IIdentifiable
        {
            if (entity.GetDialogRezultByEntityType() != DialogResult.OK)
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

        private static DialogResult GetDialogRezultByEntityType<T>(this T entity)
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
        private static DialogResult GetMessageBoxResult(string message)
        {
            return MessageBox.Show(message, "Подтверждение удаления", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}
