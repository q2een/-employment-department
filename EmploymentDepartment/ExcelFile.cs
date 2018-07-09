using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmploymentDepartment
{
    class ExcelFile
    {
        Excel.Application ex;
        Excel.Workbook workBook;
        private int sheetsCount;

        public ExcelFile(int sheets)
        {
            ex = new Excel.Application();
            ex.Visible = true;
            ex.DisplayAlerts = false;

            this.sheetsCount = sheets;

            ex.SheetsInNewWorkbook = sheets;
            workBook = ex.Workbooks.Add();
        }

        public void AddSheet(DataTable table, string sheetName, string filter = null, string sort = null)
        {
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            sheet.Name = sheetName;

            for (int i = 1; i < table.Columns.Count + 1; i++)
            {
                sheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
            }
            
            var rows = table.Select(filter, sort);

            for (int j = 0; j < rows.Length; j++)
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    sheet.Cells[j + 2, k + 1] = rows[j].ItemArray[k].ToString();
                }
            }

            string lastRange = GetExcelColumnNameByIndex(table.Columns.Count + 1) + (rows.Length + 2).ToString();
            var range = sheet.get_Range("A1", lastRange);
            range.EntireColumn.AutoFit();
            range.EntireRow.AutoFit();
        }

        public void Save(string filename)
        {
            ex.Application.ActiveWorkbook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal);
        }
        
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
