using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет окно для экспорта данных из БД в формат .xls.
    /// </summary>
    public partial class ExportForm : Form
    {
        private readonly IExport export;

        /// <summary>
        /// Предоставляет окно для экспорта данных из БД в формат .xls.
        /// </summary>
        /// <param name="export">Экземпляр класса, реализующего интерфейс <c>IExport</c></param>
        public ExportForm(IExport export)
        {
            InitializeComponent();

            this.export = export;
        }

        // Возвращает истину если ни один из флажков на форме не был выбран.
        private bool IsTablesNotSelected()
        {
            foreach (Control control in gbTables.Controls)
            {
                if ((control is CheckBox) && (control as CheckBox).Checked)
                    return false;
            }

            return true;
        }

        // Записать содержимое в один файл но на разные листы.
        private void WriteInOneFile()
        {
            var tables = GetExportDataTables();

            var doc = new ExcelFile(tables.Count);

            foreach (var table in tables)
            {
                doc.AddSheet(table, table.TableName);
            }

            doc.Save(tbPath.Text + $@"\Таблицы_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}.xls");
        }

        // Записать содержимое в разные файлы.
        private void WriteInDifferentFiles()
        {
            var tables = GetExportDataTables();
            
            foreach (var table in tables)
            {
                var doc = new ExcelFile(1);
                doc.AddSheet(table, table.TableName);
                doc.Save(tbPath.Text + $@"\{table.TableName}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}.xls");
            }             
        }

        // Возвращает список таблиц соответствующих выбору пользователя. 
        private List<DataTable> GetExportDataTables()
        {
            var tables = new List<DataTable>();

            if (cbPreferentialCategories.Checked)
                tables.Add(export.GetPreferentialCategories());

            if (cbSpecializations.Checked)
                tables.Add(export.GetSpecializations());

            if (cbFaculties.Checked)
                tables.Add(export.GetFaculties());

            if (cbVacancies.Checked)
                tables.Add(export.GetVacancies());

            if (cbStudentCompanies.Checked)
                tables.Add(export.GetStudentCompanies());

            if (cbCompanies.Checked)
                tables.Add(export.GetCompanies());

            if (cbSudents.Checked)
                tables.Add(export.GetStudents());

            return tables;
        }

        // Нажатие на кнопку "Обзор". Обработка события.
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                tbPath.Text = folderBrowserDialog.SelectedPath;
        }

        // Нажатие на кнопку "Подтвердить". Обработка события.
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbPath.Text.Trim()) || !System.IO.Directory.Exists(tbPath.Text.Trim()))
                    throw new Exception("Укажите корректный путь для сохранения файлов");
             
                if (IsTablesNotSelected())
                    throw new Exception("Не выбраны таблицы для экспорта");

                if (rbFiles.Checked)
                    WriteInDifferentFiles();
                else
                    WriteInOneFile();

                MessageBox.Show("Экспорт данных успешно осуществлен", "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
