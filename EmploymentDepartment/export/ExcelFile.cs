using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет класс для создания Excel файла.
    /// </summary>
    class ExcelFile
    {
        Excel.Application ex;
        Excel.Workbook workBook;
        private int sheetsCount;

        /// <summary>
        /// Предоставляет класс для создания Excel файла.
        /// </summary>
        /// <param name="sheets">Количество листов в книге</param>
        public ExcelFile(int sheets)
        {
            ex = new Excel.Application();
            ex.Visible = false;
            ex.DisplayAlerts = false;

            this.sheetsCount = sheets;

            ex.SheetsInNewWorkbook = sheets;
            workBook = ex.Workbooks.Add();
        }

        /// <summary>
        /// Добавляет лист в книгу Excel.
        /// </summary>
        /// <param name="table">Данные, отображаемы на листе</param>
        /// <param name="sheetName">Наименование листа</param>
        /// <param name="filter">Фильтр для данных. Необязательный параметр</param>
        /// <param name="sort">Сортировка данных. Необязательный параметр</param>
        public void AddSheet(DataTable table, string sheetName, string filter = null, string sort = null)
        {
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(sheetsCount--);
            sheet.Name = ValidateName(sheetName);

            // Задаем наименования столбцов.
            for (int i = 1; i < table.Columns.Count + 1; i++)
            {
                sheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
            }
            
            var rows = table.Select(filter, sort);
            
            // Проход по всем строкам.                        
            for (int j = 0; j < rows.Length; j++)
                for (int k = 0; k < table.Columns.Count; k++)
                    sheet.Cells[j + 2, k + 1] = rows[j].ItemArray[k].ToString();

            string lastRange = GetExcelColumnNameByIndex(table.Columns.Count + 1) + (rows.Length + 2).ToString();
            
            // Автоматическое выравнивание всех данных.
            var range = sheet.get_Range("A1", lastRange);
            range.EntireColumn.AutoFit();
            range.EntireRow.AutoFit();
        }

        /// <summary>
        /// Сохраняет изменения в файл.
        /// </summary>
        /// <param name="filename">Полный путь к файлу</param>
        public void Save(string filename)
        {
            ex.Application.ActiveWorkbook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal);
            workBook.Close();
            ex.Quit();
        }

        // Возвращает корректное наименование листа исходя из правил:
        // * длина введенного имени не превышает 31 знака;
        // * имя не содержит ни одного из следующих знаков:  :  \  /  ?  *  [  или  ];
        // * имя не оставлено пустым.
        private string ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name.Trim()))
                return "Новый лист";

            name = name.Replace(":", "").Replace("\\", "").Replace("/", "").Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");

            if (name.Length > 31)
                return name.Remove(30, name.Length - 30);

            return name;
        }

        // Возвращает имя столбца в зависимости от переданного числа. 1 - А. 2 - B ... 27 - АА.
        private string GetExcelColumnNameByIndex(int index)
        {
            char c = 'A';
            var letters = new char[2];
            for (int i = 1; i < index; i++)
            {
                if(c == 'Z')
                {
                    letters[0] = letters[0] == default(char) ? 'A' : (char)((int)letters[0] + 1);
                    c = 'A';
                }
                c = (char)((int)c + 1);
            }

            if (letters[0] == default(char))
                return c + "";

            letters[1] = c;

            return new string(letters);
        }
    }
}
