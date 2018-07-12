using System;
using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace EmploymentDepartment
{
    class WordFile
    {
        Word.Application app;
        Word.Document document;

        public WordFile(string templateFile)
        {
            try
            {
                app = new Word.Application();
                app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                app.Visible = false;
                app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

                document = app.Documents.Open(templateFile);
            }
            catch (Exception ex)
            {
                document?.Close();
                app?.Quit();
                throw ex;
            }
        }

        public void ReplaceWordText(string textToReplace, string text)
        {
            try
            {
                var range = document.Content;
                range.Find.ClearFormatting();
                range.Find.Execute(FindText: textToReplace, ReplaceWith: text);
            }
            catch (Exception ex)
            {
                document.Close();
                app.Quit();
                throw ex;
            }
        }

        public void AddTable(DataTable dataTable)
        {
            try
            {
                var range = document.Content;
                range = document.Bookmarks[1].Range;

                Word.Table table = document.Tables.Add(range, dataTable.Rows.Count + 1, dataTable.Columns.Count);
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
                document.Close();
                app.Quit();
                throw ex;
            }
        }

        public void Save(string filename)
        {
            try
            {
                document.SaveAs(filename);
            }
            finally
            {
                document.Close();
                app.Quit();
            }
        }
    }
}
