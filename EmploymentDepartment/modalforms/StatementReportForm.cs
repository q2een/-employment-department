using EmploymentDepartment.BL;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет окно для формирования отчета "Ведомость распределения выпускников, которые окончили ВУЗ" в заданном году.
    /// </summary>
    public partial class StatementReportForm : Form
    {
        private readonly IExport export;
        private readonly string fileName;

        /// <summary>
        /// Предоставляет окно для формирования отчета "Ведомость распределения выпускников, которые окончили ВУЗ" в заданном году.
        /// </summary>
        /// <param name="export">Экземпляр класса, реализующего интерфейс <c>IExport</c></param>
        /// <param name="filePath">Полный путь к файлу с отчетом</param>
        public StatementReportForm(IExport export, string filePath)
        {
            InitializeComponent();
            this.export = export;
            this.fileName = filePath;
        }

        // Кнопка "ОК". Обработка события нажатия на кнопку.
        private void btnOK_Click(object sender, EventArgs e)
        {
            tbYear.Focus();
            btnOK.Focus();

            if (errorProvider.GetError(tbYear) != "")
                return;

            int year = int.Parse(tbYear.Text);

            var doc = new WordFile(Directory.GetCurrentDirectory() + @"\templates\Statement.docx");

            var dataTable = export.GetStatementReport(year);

            doc.AddTable(dataTable);
            doc.ReplaceWordText("{year}", year.ToString());
            doc.Save(fileName);

            this.Close();
        }

        // Обработка события проверки на корректность поля для ввода года окончания ВУЗа.
        private void tbYear_Validating(object sender, CancelEventArgs e)
        {
            int year;

            if (!Int32.TryParse(tbYear.Text, out year) || year < 2018)
                errorProvider.SetError(tbYear, "Укажите год окончания универсистета");
            else
                errorProvider.SetError(tbYear, "");
        }
    }
}
