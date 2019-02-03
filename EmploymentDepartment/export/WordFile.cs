using System;
using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет класса для создания Word файла.
    /// </summary>
    class WordFile
    {
        /// <summary>
        /// Возвращает или задает приложение.
        /// </summary>
        protected Word.Application Application { get; set; }

        /// <summary>
        /// Возвращает или задает документ.
        /// </summary>
        protected Word.Document Document { get; set; }

        /// <summary>
        /// Предоставляет класса для создания Word файла по шаблону.
        /// </summary>
        /// <param name="templateFile">Файл шаблона</param>
        public WordFile(string templateFile)
        {
            try
            {
                Application = new Word.Application();
                Application.Visible = false;
                Application.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

                Document = Application.Documents.Open(templateFile);
            }
            catch (Exception ex)
            {
                Document?.Close();
                Application?.Quit();
                throw ex;
            }
        }

        /// <summary>
        /// Замещает текст в файле-шаблоне.
        /// </summary>
        /// <param name="textToReplace">Текст, который нужно заместить</param>
        /// <param name="text">ТЕкст, которым нужно заместить</param>
        public virtual void ReplaceWordText(string textToReplace, string text)
        {
            try
            {
                var range = Document.Content;
                range.Find.ClearFormatting();
                range.Find.Execute(FindText: textToReplace, ReplaceWith: text);
            }
            catch (Exception ex)
            {
                Document.Close();
                Application.Quit();
                throw ex;
            }
        }

        /// <summary>
        /// Добавляет таблицу в файл-шаблон. Таблица добавляется вместо первой закладки в файле-шаблоне.
        /// </summary>
        /// <param name="dataTable">Объект класса <c>DataTable</c></param>
        public virtual void AddTable(DataTable dataTable)
        {
            try
            {
                var range = Document.Content;
                range = Document.Bookmarks[1].Range;

                Word.Table table = Document.Tables.Add(range, dataTable.Rows.Count + 1, dataTable.Columns.Count);
                table.Borders.Enable = 1;
                for (int i = 1; i <= table.Columns.Count; i++)
                {
                    table.Rows[1].Cells[i].Range.Text = dataTable.Columns[i-1].ColumnName;
                }
                
                for (int j = 2; j <= table.Rows.Count; j++)
                {
                    table.Rows[j].Cells[1].Range.Text = (j - 1).ToString();

                    for (int k = 2; k <= table.Columns.Count; k++)
                    {
                        table.Rows[j].Cells[k].Range.Text = dataTable.Rows[j - 2].ItemArray[k - 1].ToString();
                    }
                }   
            }
            catch (Exception ex)
            {
                Document.Close();
                Application.Quit();
                throw ex;
            }
        }

        /// <summary>
        /// Сохраняет изменения в указанный файл.
        /// </summary>
        /// <param name="filename">Полный путь к файлу.</param>
        public virtual void Save(string filename)
        {
            try
            {
                Document.SaveAs(filename);
            }
            finally
            {
                Document.Close();
                Application.Quit();
            }
        }

        // Освобождение ресурсов.
        ~WordFile()
        {
            try
            {
                Document.Close();
                Application.Quit();
            }
            catch
            {

            }
        }
    }
}
