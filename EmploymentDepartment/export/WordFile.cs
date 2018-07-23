using System;
using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace EmploymentDepartment
{
    class WordFile
    {
        protected Word.Application Application { get; set; }
        protected Word.Document Document { get; set; }

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
    }
}
