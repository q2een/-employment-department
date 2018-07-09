using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
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
        void Save();

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
