using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет форму для формирования отчета "Ведомость персонального учета выпускников".
    /// </summary>
    public partial class PersonalAccountReportForm : Form
    {
        private readonly MainMDIForm main;
        private IEnumerable<Specialization> specializations = null;
        private readonly string fileName;

        /// <summary>
        /// Предоставляет форму для формирования отчета "Ведомость персонального учета выпускников"
        /// </summary>
        /// <param name="mainForm">Экземпляр класса главного окна</param>
        /// <param name="fileName">Полный путь к файлу отчета</param>
        public PersonalAccountReportForm(MainMDIForm mainForm, string fileName)
        {
            InitializeComponent();
            this.main = mainForm;
            this.fileName = fileName;
        }

        // Смена значений в выдающем списке "Факультет". Обработка события.
        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = cmbFaculty.SelectedValue;
            int id;

            if (!Int32.TryParse(value + "", out id))
                return;

            var sp = specializations.Where(i => i.Faculty == id).Select(z => z as ISpecialization).ToList();
            cmbFieldOfStudy.BindComboboxData(sp);
        }

        // Обработка события загрузки окна.
        private void PersonalAccountReportForm_Load(object sender, EventArgs e)
        {
            var faculties = main.Entities.GetEntities<Faculty>().ToList();
            specializations = main.Entities.GetEntities<Specialization>();
            cmbFaculty.BindComboboxData(faculties);

            cmbFaculty_SelectedIndexChanged(sender, e);
        }

        // Нажатие на кнопку "Подтвердить". Обработка события.
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbStudyGroup.Text.Trim().Length < 2)
                    throw new Exception("Укажите группу");

                int fielOfStudy; 
                if (!Int32.TryParse(cmbFieldOfStudy.SelectedValue + "", out fielOfStudy))
                    throw new Exception("Укажите профиль подготовки");

                int year = main.DataBase.Export.GetYearOfGraduation(fielOfStudy, tbStudyGroup.Text.Trim());
                var table = main.DataBase.Export.GetPersonalAccountReport(fielOfStudy, tbStudyGroup.Text.Trim(),year);

                var doc = new PersonalAccountOfGraduatesReport(System.IO.Directory.GetCurrentDirectory() + @"\templates\personalAccountOfGraduates.docx");

                this.Hide();

                Task t = new Task(() => {

                    MessageBox.Show("Сохрениение файла может занять некоторое время. После успешного сохранения будет показано уведомление", "Формирование отчета", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    doc.ReplaceWordText("{Faculty}", cmbFaculty.Text);
                    doc.ReplaceWordText("{FieldOfStudy}", cmbFieldOfStudy.Text);
                    doc.ReplaceWordText("{StudyGroup}", tbStudyGroup.Text.Trim());
                    doc.ReplaceWordText("{Year1}", year.ToString());
                    doc.ReplaceWordText("{Year2}", (++year).ToString());
                    doc.ReplaceWordText("{Year3}", (++year).ToString());

                    doc.AddTable(table);
                    doc.Save(fileName);
                });
                t.Start();

                t.ContinueWith((task) => MessageBox.Show($"Файл {fileName}", "Ведомость персонального учета выпускников", MessageBoxButtons.OK, MessageBoxIcon.Information));

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при сохранении отчета", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
