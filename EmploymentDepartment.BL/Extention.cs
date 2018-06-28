using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

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

        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
