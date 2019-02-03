using System;
using System.IO;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет форму для настройки формирования файлов-отчетов.
    /// </summary>
    internal partial class ReportSettingsForm : Form
    {
        private readonly MainMDIForm main;

        /// <summary>
        /// Предоставляет форму для настройки формирования файлов-отчетов.
        /// </summary>
        /// <param name="main">Экземпляр главного MDI-окна</param>
        public ReportSettingsForm(MainMDIForm main)
        {
            InitializeComponent();
            this.main = main;

            // Параметры из настроек.
            this.cbRequirePath.Checked = Properties.Settings.Default.isRquirePath;
            this.cbCreateDateFolder.Checked = Properties.Settings.Default.createSubfolder;
            this.tbPath.Text = Properties.Settings.Default.reportPath;
            this.cbOpenFile.Checked = Properties.Settings.Default.openAfterCreate;
        }

        // Обзор. Обработка события нажатия на кнопку.
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        // Запрашивать путь для сохранения отчета. Обработка события изменения состояния флажка.
        private void cbRequirePath_CheckedChanged(object sender, EventArgs e)
        {
            cbCreateDateFolder.Enabled = btnBrowse.Enabled = !cbRequirePath.Checked;
        }

        // Сохранить. Обработка события нажатия на кнопку.
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!cbRequirePath.Checked)
            {
                if(!Directory.Exists(tbPath.Text))
                    MessageBox.Show("Укажите существующий путь для сохранения отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Properties.Settings.Default.isRquirePath = cbRequirePath.Checked;
            Properties.Settings.Default.openAfterCreate = cbOpenFile.Checked;

            if (!cbRequirePath.Checked)
            {
                Properties.Settings.Default.createSubfolder = cbCreateDateFolder.Checked;
                Properties.Settings.Default.reportPath = tbPath.Text;
            }

            Properties.Settings.Default.Save();

            main.SetReportCreatorPropertiesBySettings();

            this.Dispose();
            this.Close();
        }
    }
}
