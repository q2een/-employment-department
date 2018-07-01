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

        #region Validate Controls

        /// <summary>
        /// Возвращает результат проверки соответсвия ошибок при помощи <c>errorProvider</c>.
        /// </summary>
        /// <param name="container">Контейнер элементов управления для проверки</param>
        /// <param name="errorProvider">Интерфейс пользователя, указывающий на наличие ошибки</param>
        /// <returns></returns>
        public static bool ValidateControls(Control container, ErrorProvider errorProvider)
        {
            bool result = true;
            return ValidateControls(container, errorProvider, ref container, ref result);
        }

        private static bool ValidateControls(Control container, ErrorProvider errorProvider, ref Control lastControl, ref bool result)
        {   
            foreach (Control control in container.Controls)
            {
                if (!(control is TextBox) && !(control is MaskedTextBox) && !(control is ComboBox))
                {
                    // Если нет вложенных элементов управления.
                    if (control.Controls.Count == 0)
                        continue;

                    // Если есть - рекурсивный вызов.
                    ValidateControls(control, errorProvider, ref lastControl,ref result);
                    continue;
                }
                else
                {
                    // Результат проверки. Если есть ошибка хоть на одном элементе управления - false.
                    result = result && string.IsNullOrEmpty(errorProvider.GetError(lastControl));
                    
                    // Фокус на элементе управления, для осуществления валидации.
                    control.Focus();

                    // Значение предыдущего контрола для осуществления валидации.
                    lastControl = control;
                }
            }

            // Устанавливаем фокус на контейнер, чтобы произошла валидация последнего выбранного контрола.
            container.Focus();
            
            return result && string.IsNullOrEmpty(errorProvider.GetError(lastControl));
        }

        public static void RequiredComboBoxValidating(ComboBox cmb, ErrorProvider errorProvider)
        {
            if (cmb.SelectedIndex < 0)
                errorProvider.SetError(cmb, "Необходимо выбрать элемент из выпадающего списка");
            else
                errorProvider.SetError(cmb, "");
        }

        public static void RequiredTextBoxValidating(Control tb, ErrorProvider errorProvider)
        {
            if (String.IsNullOrEmpty(tb.Text.Replace(".", "").Trim()))
                errorProvider.SetError(tb, "Поле обязательно к заполнению");
            else
                errorProvider.SetError(tb, ""); 
        }
       
        public static bool SurnameKeyPressValidator(char KeyChar)
        {
            return !(char.IsLetter(KeyChar) || KeyChar == (char)Keys.Back || KeyChar == '-' || KeyChar == (char)Keys.Back);
        }
        #endregion

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
