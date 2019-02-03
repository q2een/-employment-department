using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет статический клалсс, содержащий часто использующиеся методы, в том числе и методы расширений.
    /// </summary>
    public static class Extentions
    {
        #region Validate Controls

        /// <summary>
        /// Возвращает результат валидации полей контейнера <c>container</c>.
        /// </summary>
        /// <param name="container">Контейнер, содержащий поля для валидации</param>
        /// <param name="errorProvider">Интерфейс пользователя, указывающий на наличие ошибки</param>
        /// <returns>Результат валидации полей</returns>
        public static bool ValidateFields(this Control container, ErrorProvider errorProvider)
        {
            if (!ValidateControls(container, errorProvider))
            {
                SystemSounds.Beep.Play();
                return false;
            }

            return true;
        }

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

        /// <summary>
        /// Возвращает результат проверки на ошибки элементов управления в заданном контейнере.
        /// Проверка осуществляется путем поочередного задания фокуса на элемент упралвения.
        /// </summary>
        /// <param name="container">Контейнер элементов управления для проверки</param>
        /// <param name="errorProvider">Интерфейс пользователя, указывающий на наличие ошибки</param>
        /// <param name="lastControl">Послдедний элемент управления, находящийся под фокусом</param>
        /// <param name="result">Результат</param>
        /// <returns></returns>
        private static bool ValidateControls(Control container, ErrorProvider errorProvider, ref Control lastControl, ref bool result)
        {
            foreach (Control control in container.Controls)
            {
                if (!(control is TextBox) && !(control is MaskedTextBox) && !(control is ComboBox) && !(control is LinkLabel))
                {
                    // Если нет вложенных элементов управления.
                    if (control.Controls.Count == 0)
                        continue;

                    // Если есть - рекурсивный вызов.
                    ValidateControls(control, errorProvider, ref lastControl, ref result);
                    continue;
                }
                else
                {
                    var a = errorProvider.GetError(lastControl);
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
            var b = errorProvider.GetError(lastControl);
            return result && string.IsNullOrEmpty(errorProvider.GetError(lastControl));
        }

        /// <summary>
        /// Валидация выпадающих списков обязательных к выбору элемента.
        /// </summary>
        /// <param name="cmb">Выпадающий список обязательный к выбору элемента.</param>
        /// <param name="errorProvider">Интерфейс пользователя, указывающий на наличие ошибки</param>
        public static void RequiredComboBoxValidating(ComboBox cmb, ErrorProvider errorProvider)
        {
            if (cmb.SelectedIndex < 0 && cmb.Enabled)
                errorProvider.SetError(cmb, "Необходимо выбрать элемент из выпадающего списка");
            else
                errorProvider.SetError(cmb, "");
        }

        /// <summary>
        /// Валидация текстовых полей обязательных к заполнению.
        /// </summary>
        /// <param name="tb">Текстовое поле обязательное к заполнению</param>
        /// <param name="errorProvider">Интерфейс пользователя, указывающий на наличие ошибки</param>
        public static void RequiredTextBoxValidating(Control tb, ErrorProvider errorProvider)
        {
            if (String.IsNullOrEmpty(tb.Text.Replace(".", "").Trim()) && tb.Enabled)
                errorProvider.SetError(tb, "Поле обязательно к заполнению");
            else
                errorProvider.SetError(tb, "");
        }

        /// <summary>
        /// Валидация нажатия клавиши при вводе значений ФИО. Разрешены А-яA-z-
        /// </summary>
        /// <param name="KeyChar">Введенный символ</param>
        /// <returns>true, в случае если <c>KeyChar</c> валидный</returns>
        public static bool SurnameKeyPressValidator(char KeyChar)
        {
            return !(char.IsLetter(KeyChar) || KeyChar == (char)Keys.Back || KeyChar == '-' || KeyChar == (char)Keys.Back || KeyChar == (char)Keys.Space);
        }
        #endregion

        /// <summary>
        /// Добавляет элемент в обобщенную коллекцию.
        /// </summary>
        /// <param name="collection">Исходная коллекция</param>
        /// <param name="value">Новый обхект, которых необходимо добавить в коллекцию</param>
        /// <returns>Расширенная коллекция, содержащая новый элемент</returns>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> collection, T value)
        {
            foreach (var item in collection)
            {
                yield return item;
            }

            yield return value;
        }

        /// <summary>
        /// Отключает возможность редактировать элемент упраления <c>control</c>, переключая фокус на
        /// элемент управления <c>focusTarget</c>.
        /// </summary>
        /// <param name="control">Элемент управления в котором необходимо отключить возможность редактирования</param>
        /// <param name="focusTarget">Элемент управления на который переводится фокус</param>
        public static void Disable(this Control control, Control focusTarget)
        {
            control.Disable(delegate { focusTarget.Focus(); });
        }

        // Отключает возможность редактировать элемент упраления. 
        private static void Disable(this Control control, EventHandler evnDel)
        {
            control.TabStop = false;
            control.Enter += evnDel;
        }

        /// <summary>
        /// Отключает возможность редактировать элементы упраления, расположенные в контейнере <c>container</c>, за исключением элементов управления <c>ignore</c>. 
        /// </summary>
        /// <param name="container">Контейнер, содержащий элементы управления для отключения возможности редактировать</param>
        /// <param name="ignore">Элементу управления, которые необходимо пропустить и оставить редактируемыми</param>
        public static void DisableControls(this Control container, params Control[] ignore)
        {
            EventHandler evnt = delegate { container.Focus(); };

            foreach (Control control in container.Controls)
            {
                if (ignore.Contains(control))
                    continue;

                if (control.Controls.Count > 0)
                {
                    DisableControls(control, ignore);
                    continue;
                }

                control.Disable(evnt);
            }
        }

        /// <summary>
        /// Включает или отключает двойную буферезацию для элемента управления DataGridView.
        /// </summary>
        /// <param name="dgv">Элемент управления, для которого необходимо установить двойную буферезацию</param>
        /// <param name="enable">Флаг, указывающий необходимо ли включить двойную буферезацию</param>
        public static void DoubleBuffered(this DataGridView dgv, bool enable)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, enable, null);
        }

        /// <summary>
        /// Обрезает строку для ее корректного отображения на окне и возвращает результат.
        /// </summary>
        /// <param name="myString">Строка для обрезания</param>
        /// <param name="width">Размер для строки</param>
        /// <param name="font">Шрифт</param>
        /// <returns>Обрезанная под размеры строку</returns>
        public static string ShortenString(string myString, int width, Font font)
        {
            string result = string.Copy(myString);
            TextRenderer.MeasureText(result, font, new Size(width, 0), TextFormatFlags.EndEllipsis | TextFormatFlags.ModifyString);

            return result;
        }

        /// <summary>
        /// Осуществляет привязку данных к выпадающему списку.
        /// </summary>
        /// <typeparam name="T">Класс, реализующий интерфейс IIdentifiable</typeparam>
        /// <param name="cmb">Выпадающий список</param>
        /// <param name="data">Коллекция данных</param>
        public static void BindComboboxData<T>(this ComboBox cmb, List<T> data) where T : IIdentifiable
        {
            var value = cmb.SelectedValue;
            int id;

            cmb.DataSource = null;
            cmb.Items.Clear();
            cmb.DataSource = data;
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "ID";            


            // Выделить элемент (если он существует), который был активен до изменений.
            if (!Int32.TryParse(value + "", out id))
                return;

            var elem = data.FirstOrDefault(i => i.ID == id);

            if (elem == null)
                return;

            cmb.SelectedIndex = data.IndexOf(elem);
        }

        /// <summary>
        /// Устанавливает значения свойст одного экземпляра класса <c>self</c> значениями другого экземпляра класса<c>source</c>.
        /// </summary>
        /// <param name="self">Экземмпляр класса</param>
        /// <param name="source">Экземмпляр класса-источника</param>
        /// <param name="ignore">Набор имен свойств, которые необходимо проигнорировать</param>
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

        /// <summary>
        /// Возвращает коллекцию "Ключ-Значение", где Ключ - имя свойства объекта <c>self</c> класcа, Значение - его значение. 
        /// </summary> 
        /// <param name="self">Объект класса</param>
        /// <param name="isNotNullOnly">Учитывать только свойства со значением</param>
        /// <param name="ignore">Набор имен свойств, которые необходимо проигнорировать</param>
        /// <returns>Коллекцию "Ключ-Значение"</returns>
        public static Dictionary<string, object> GetPropertiesNameValuePair<T>(this T self,bool isNotNullOnly, params string[] ignore) where T : class, IIdentifiable
        {
            var nameValue = new Dictionary<string, object>();

            if (self == null)
                return nameValue;

            Type type = typeof(T);
            List<string> ignoreList = new List<string>(ignore);

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!ignoreList.Contains(property.Name))
                {
                    object value = type.GetProperty(property.Name).GetValue(self, null);

                    if (isNotNullOnly && value == null || string.IsNullOrEmpty(value.ToString()))
                        continue;

                    nameValue.Add(property.Name, value);
                }
            }

            return nameValue;
        }

        /// <summary>
        /// Получает отличающиеся значения свойств экземпляров класса одного типа.
        /// </summary>
        /// <param name="self">Экземпляр класса</param>
        /// <param name="to">Экземпляр класса</param>
        /// <param name="ignore">Набор имен свойств, которые необходимо проигнорировать</param>
        /// <returns>Коллекцию "Ключ-Значение"</returns>
        public static Dictionary<string,object> GetPropertiesDifference<T>(this T self, T to, params string[] ignore) where T : class
        {
            var dif = new Dictionary<string, object>();

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
                        dif.Add(property.Name,toValue);
                }
            }

            return dif;       
        }

        /// <summary>
        /// Возвращает истину, если значения свойств объектов класса одинаковы. 
        /// </summary>
        /// <param name="self">Экземпляр класса</param>
        /// <param name="to">Экземпляр класса</param>
        /// <param name="ignore">Набор имен свойств, которые необходимо проигнорировать</param>
        /// <returns>Истину, если значения свойств объектов класса одинаковы.</returns>
        public static bool IsPropertiesAreEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            return GetPropertiesDifference<T>(self, to, ignore).Count == 0;
        }
        
        /// <summary>
        /// Возвращает объект класса <c>U</c>.
        /// </summary>
        /// <typeparam name="T">Реализация интерфейса</typeparam>
        /// <typeparam name="U">Объект класса</typeparam>
        /// <param name="self">ОБъект, реализующий интерфейс <c>T</c></param>
        /// <returns>Объект класса <c>U</c></returns>
        public static U GetInstance<T,U>(this T self) where U : class, new()
        {
            var result = new U();

            if (self == null)
                return null;

            Type type = typeof(T);
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = type.GetProperty(property.Name).GetValue(self, null);
                type.GetProperty(property.Name).SetValue(result, value, null);
            }

            return result;
        }

        /// <summary>
        /// Сохраняет внесенные изменения в БД.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="self">Экземпляр класса со свойствами  для сохранения</param>
        /// <param name="db">Экземпляр объекта, реализующий интерфейс <c>IDataBase</c></param
        /// <param name="informationMessage">Сообщение об успешних изменениях</param>
        /// <param name="ignore">Набор имен свойств, которые необходимо проигнорировать</param>
        /// <returns>Результат операции</returns>
        public static bool UpdateFormEntityInDataBase<T, U>(this T self, IDataBase db, string informationMessage, params string[] ignore) where T : Form, IEditable<U>, U where U : class, IIdentifiable
        {
            if (!self.ValidateFields() || self.Type != ActionType.Edit)
                return false;

            try
            {   
                var nameValue = self.Entity.GetPropertiesDifference<U>(self as U, ignore);

                if (nameValue.Count == 0)
                    return false;

                // Обновляем данные
                db.Update(Tables.GetTableNameByType<U>(self).ToString(), self.ID, nameValue);

                MessageBox.Show(informationMessage, "Редактирование информации", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Присваеваем свойству новые исходные значения.
                self.Entity.SetPropertiesValue(self, ignore);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Добавляет в базу данных запись со значениями свойств объекта класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="self">Экземпляр класса со свойствами  для добавления</param>
        /// <param name="db">Экземпляр объекта, реализующий интерфейс <c>IDataBase</c></param
        /// <param name="informationMessage">Сообщение об успешних изменениях</param>
        /// <param name="ignore">Набор имен свойств, которые необходимо проигнорировать</param>
        /// <returns>Результат операции</returns>
        public static bool InsertFormEntityToDataBase<T, U>(this T self, IDataBase db, string informationMessage, params string[] ignore) where T: Form, IEditable<U>, U where U : class, IIdentifiable
        {
            if (!self.ValidateFields() || self.Type != ActionType.Add)
                return false;

            try
            {
                // Поля не учитываются в таблице в БД.
                var nameValue = (self as U).GetPropertiesNameValuePair<U>(true, ignore);

                // Добавляем запись в БД.
                self.ID = (int)db.Insert(Tables.GetTableNameByType<U>(self).ToString(), nameValue);

                MessageBox.Show(informationMessage, "Операция добавления", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message, "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Возвращает коллекцию значние публичных свойств объекта <c>self</c>, которые помечены атрибутом DisplayNameAttribute.
        /// </summary>
        /// <param name="self">Объект, значения публичных свойств которого необходимо получить</param>
        /// <returns>Коллекция значний публичных свойств объекта</returns>
        public static object[] GetDisplayedPropertiesValue<T>(this T self) where T:IIdentifiable
        {
            var propValues = new List<object>();

            var type = typeof(T);

            foreach (var propertyInfo in type.GetProperties())
            {
                var attr = Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute)) as DisplayNameAttribute;

                if (attr != null)
                    propValues.Add(type.GetProperty(propertyInfo.Name).GetValue(self,null));
            }

            return propValues.ToArray();
        }

        /// <summary>
        /// Возвращает коллекцию "Ключ" - "Значение", где Ключ - имя свойства, а Значение - содержимое атрибута DisplayNameAttribute.
        /// </summary>
        /// <param name="self">Объект, содержащий свойства</param>
        /// <returns>Коллекция "Ключ" - "Значение", где Ключ - имя свойства, а Значение - содержимое атрибута DisplayNameAttribute</returns>
        public static Dictionary<string, string> GetTypePropertiesNameDisplayName<T>(this T self)
        {
            var nameDisplayName = new Dictionary<string, string>();

            var type = typeof(T);
            foreach (var propertyInfo in type.GetProperties())
            {
                var attr = Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute)) as DisplayNameAttribute;

                if (attr != null)
                    nameDisplayName.Add(propertyInfo.Name, attr.DisplayName);
            }

            return nameDisplayName;
        }
    }
}
