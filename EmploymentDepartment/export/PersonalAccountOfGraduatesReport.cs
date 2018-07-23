using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace EmploymentDepartment
{
    class PersonalAccountOfGraduatesReport : WordFile
    {
        public PersonalAccountOfGraduatesReport(string templateFile) : base(templateFile)
        {

        }

        public override void AddTable(DataTable dataTable)
        {
            try
            {
                var range = Document.Content;
                range = Document.Bookmarks[1].Range;

                Word.Table table = Document.Tables.Add(range, dataTable.Rows.Count, dataTable.Columns.Count);
                table.Borders.Enable = 1;

                object[] currRow = null;

                for (int j = 1; j <= table.Rows.Count; j++)
                {
                    if (currRow == null || !currRow[0].Equals(dataTable.Rows[j - 1].ItemArray[0]))
                    {
                        table.Rows[j].Cells[1].Range.Text = j.ToString();
                        table.Rows[j].Cells[2].Range.Text = $"{dataTable.Rows[j - 1].ItemArray[0]}\n{dataTable.Rows[j - 1].ItemArray[1]}";
                    }
                    for (int k = 3; k <= table.Columns.Count; k++)
                    {
                        if(currRow == null || !currRow[0].Equals(dataTable.Rows[j - 1].ItemArray[0]) || !currRow[k-1].Equals(dataTable.Rows[j - 1].ItemArray[k-1]))                
                            table.Rows[j].Cells[k].Range.Text = dataTable.Rows[j - 1].ItemArray[k - 1].ToString();
                    }

                    currRow = dataTable.Rows[j-1].ItemArray;
                }

                List<Word.Cell> arr = new List<Word.Cell>();

                for (int i = 2; i <= table.Rows.Count; i++)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (table.Rows[i].Cells[j].Range.Text.Length > 2)
                            continue;

                        arr.Add(table.Rows[i - 1].Cells[j]);
                        arr.Add(table.Rows[i].Cells[j]);
                    }                                                                                     
                }

                if (arr.Count % 2 != 0)
                    return;

                for (int i = 0; i < arr.Count; i+=2)
                {
                    arr[i].Merge(arr[i + 1]);
                }
            }
            catch (Exception ex)
            {
                Document.Close();
                Application.Quit();
                throw ex;
            }
        }
    }
}
