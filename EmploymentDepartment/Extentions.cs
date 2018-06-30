using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public static class Extentions
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public static bool SurnameKeyPressValidator(char KeyChar)
        {
            return !(char.IsLetter(KeyChar) || KeyChar == (char)Keys.Back || KeyChar == '-' || KeyChar == (char)Keys.Back);
        }

        public static void SetPropertiesValue<T>(this T self, T source, params string[] ignore) where T : class
        {
            if (self == null || source == null)
                return;

            Type type = typeof(T);
            List<string> ignoreList = new List<string>(ignore);

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (ignoreList.Contains(property.Name))
                    continue;

                var value = type.GetProperty(property.Name).GetValue(source, null);
                property.SetValue(self, value, null);
            }              
        }

        public static List<string> GetPropertiesDifference<T>(this T self, T to, params string[] ignore) where T : class
        {
            var dif = new List<string>();

            if (self == null || to == null)
                return dif;
            
            Type type = typeof(T);
            List<string> ignoreList = new List<string>(ignore);

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!ignoreList.Contains(property.Name))
                {
                    object selfValue = type.GetProperty(property.Name).GetValue(self, null);
                    object toValue = type.GetProperty(property.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        dif.Add($"{property.Name} = '{toValue}'");
                }
            }

            return dif;       
        }

        public static bool IsPropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            return GetPropertiesDifference<T>(self, to, ignore).Count == 0;
        }
    }
}
