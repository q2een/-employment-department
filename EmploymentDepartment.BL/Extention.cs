using System.Collections.Generic;
using System.Reflection;

namespace EmploymentDepartment.BL
{
    public static class Extention
    {
        /// <summary>
        /// Заполняет свойства объекта из коллекции "Ключ-Значение", где ключ соответствует наименованию свойства.
        /// </summary>
        /// <param name="obj">Объект, свойства которого необходимо заполнить значениями</param>
        /// <param name="dict">Коллекции "Ключ-Значение", где ключ соответствует наименованию свойства объекта</param>
        public static void SetProperties<T>(this T obj, Dictionary<string, object> dict)
        {
            if (dict == null)
                return;

            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (dict.ContainsKey(property.Name))
                {
                    property.SetValue(obj, dict[property.Name], null);
                }
            }
        }
    }
}
