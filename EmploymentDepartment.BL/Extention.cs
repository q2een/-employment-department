using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EmploymentDepartment.BL
{
    public static class Extention
    {
        public static void SetProperties<T>(this T obj, Dictionary<string, object> dict)
        {
            if (dict == null)
                return;

            var properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (dict.ContainsKey(property.Name))
                {
                    property.SetValue(obj, dict[property.Name], null);
                }
            }
        }
    }
}
