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

        public ExcelFile(DataTable table)
        {
            ex = new Excel.Application();
            ex.Visible = true;
            ex.SheetsInNewWorkbook = 1;
            workBook = ex.Workbooks.Add();
            ex.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Отчет за 13.12.2017";

            for (int i = 1; i < table.Columns.Count + 1; i++)
            {
                sheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
            }
            var range = sheet.Cells[1, table.Columns.Count];
            range.EntireColumn.AutoFit();
            range.EntireRow.AutoFit();

            for (int j = 0; j < table.Rows.Count; j++)
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    sheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                }
                range = sheet.Cells[j + 2, table.Columns.Count];
                range.EntireColumn.AutoFit();
                range.EntireRow.AutoFit();
            }

            


            ex.Application.ActiveWorkbook.SaveAs("doc.xls", Excel.XlFileFormat.xlWorkbookNormal);
        }

        private void ExportDataSetToExcel(DataSet ds)
        {
            //System.IO.File.WriteAllText(@"E:\Org1.xls", "");
            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"E:\123.xls");

            foreach (DataTable table in ds.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();

        }
    }
}
