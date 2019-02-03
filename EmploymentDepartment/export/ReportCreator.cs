using EmploymentDepartment.BL;
using System;
using System.IO;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет класс для формирования и сохранения отчетов.
    /// </summary>
    internal class ReportCreator
    {
        private readonly FolderBrowserDialog saveFolder;

        /// <summary>
        /// Сигнатура методов сохранения отчета.
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        public delegate void SaveReport(IStudent student, ISpecialization specialization, IStudentCompany workPlace);

        /// <summary>
        /// Предоставляет класс для формирования и сохранения отчетов.
        /// </summary>
        /// <param name="saveFolder">Экземпляр класса для выбора папки в которой будет сохранен отчет</param>
        public ReportCreator(FolderBrowserDialog saveFolder)
        {
            this.saveFolder = saveFolder;
        }

        /// <summary>
        /// Сохраняет отчет "Свидетельство о направлении на работу".
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        public void SaveCertificate(IStudent student, ISpecialization specialization, IStudentCompany workPlace)
        {
            var filename = $"{student.Surname}_свидетельство_о_направлении_{DateTime.Now.ToString("dd_MM_HH_m")}.docx";
            SaveEmploymentReport(student, specialization, workPlace, GetPath(filename), CertificatePath);
        }

        /// <summary>
        /// Сохраняет отчет "Уведомление к свидетельству о направлении на работу".
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        public void SaveNotification(IStudent student, ISpecialization specialization, IStudentCompany workPlace)
        {
            var filename = $"{student.Surname}_уведомление_{DateTime.Now.ToString("dd_MM_HH_m")}.docx";
            SaveEmploymentReport(student, specialization, workPlace, GetPath(filename), NotificationPath);
        }

        /// <summary>
        /// Сохраняет отчет "Подтверждение прибытия к свидетельству о направлении на работу".
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        public void SaveConfirmationOfArrival(IStudent student, ISpecialization specialization, IStudentCompany workPlace)
        {
            var filename = $"{student.Surname}_подтверждение_прибытия_{DateTime.Now.ToString("dd_MM_HH_m")}.docx";
            SaveEmploymentReport(student, specialization, null, GetPath(filename), ConfirmationOfArrival);
        }

        /// <summary>
        /// Сохраняет отчет "Справка о самостоятельном трудоустройстве".
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        public void SaveSelfEmployment(IStudent student, ISpecialization specialization, IStudentCompany workPlace)
        {
            var filename = $"{student.Surname}_справка_{DateTime.Now.ToString("dd_MM_HH_m")}.docx";
            SaveSelfEmploymentReport(student, specialization, GetPath(filename), SelfEmploymentPath);
        }

        /// <summary>
        /// Сохраняет отчет "Подтверждение прибытия к справке о самостоятельном трудоустройстве".
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        public void SaveSelfEmploymentConfirmationOfArrival(IStudent student, ISpecialization specialization, IStudentCompany workPlace)
        {
            var filename = $"{student.Surname}_подтверждение_{DateTime.Now.ToString("dd_MM_HH_m")}.docx";
            SaveSelfEmploymentReport(student, specialization, GetPath(filename), SelfEmploymentConfirmationOfArrivalPath);
        }

        /// <summary>
        /// Формирует справки - направления на работу.
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        /// <param name="path">Путь к сохраняемому файлу</param>
        /// <param name="template">Путь к файлу-шаблону</param>
        private void SaveEmploymentReport(IStudent student, ISpecialization specialization, IStudentCompany workPlace, string path, string template)
        {
            var doc = new WordFile(template);

            doc.ReplaceWordText("{surname}", student.Surname.Trim());
            doc.ReplaceWordText("{name}", student.Name.Trim());
            doc.ReplaceWordText("{patronymic}", student.Patronymic.Trim());
            doc.ReplaceWordText("{year}", student.YearOfGraduation.ToString());
            doc.ReplaceWordText("{chiper}", GetChiper(student, specialization));
            doc.ReplaceWordText("{specialization}", GetFullSpecializationText(specialization));
            doc.ReplaceWordText("{nameOfStateDepartment}", workPlace?.NameOfStateDepartment.Trim());
            doc.ReplaceWordText("{nameOfCompany}", workPlace?.NameOfCompany.Trim());
            doc.ReplaceWordText("{post}", workPlace?.Post.Trim());

            doc.Save(path);

            if (OpenAfterCreate)
                System.Diagnostics.Process.Start(path);
            else
                MessageBox.Show($"Файл {path}", "Отчет сформирован", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Формирует справки для самостоятельного трудоустройста.
        /// </summary>
        /// <param name="student">Экземпляр класса с данными о студенте</param>
        /// <param name="specialization">Экземпляр класса с данными о специализации</param>
        /// <param name="workPlace">Экземпляр класса с данными о месте работы студента</param>
        /// <param name="path">Путь к сохраняемому файлу</param>
        /// <param name="template">Путь к файлу-шаблону</param>
        private void SaveSelfEmploymentReport(IStudent student, ISpecialization specialization, string path, string template)
        {
            var doc = new WordFile(template);

            doc.ReplaceWordText("{surname}", student.Surname.Trim());
            doc.ReplaceWordText("{name}", student.Name.Trim());
            doc.ReplaceWordText("{patronymic}", student.Patronymic.Trim());
            doc.ReplaceWordText("{year}", student.YearOfGraduation.ToString());
            doc.ReplaceWordText("{chiper}", GetChiper(student, specialization));
            doc.ReplaceWordText("{specialization}", GetFullSpecializationText(specialization));

            doc.Save(path);

            if (OpenAfterCreate)
                System.Diagnostics.Process.Start(path);
            else
                MessageBox.Show($"Файл {path}", "Отчет сформирован", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Получает папку для сохранения отчета и возвращает полный путь к файлу.
        private string GetPath(string fileName)
        {
            if (IsRequirePath)
            {
                if (saveFolder.ShowDialog() == DialogResult.OK)
                    return Path.Combine(saveFolder.SelectedPath, fileName);

                throw new FileNotFoundException();
            }

            var path = $@"{SaveFolderPath}\";

            if (CreateSubfolder)
            {
                path += DateTime.Now.ToString("dd/MM/yyyy");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }

            return Path.Combine(path, fileName);
        }

        // Возвращает текст специализации.
        private string GetFullSpecializationText(ISpecialization specialization)
        {
            var education = specialization.LevelOfEducation;

            string separator = education == 1 ? "профиль подготовки" : education == 2 ? "специализация" : "магистерская программа";

            return $"{specialization.SpecialtyCode} «{specialization.SpecialtyName}» {separator} «{specialization.SpecialtyProfileName}»";
        }

        // Возвращает шифр в формате {Шифр из БД}{Год окончания ВУЗа}/{Идентификатор студента}
        private string GetChiper(IStudent student, ISpecialization specialization)
        {
            return $"{specialization.Cipher}{(student.YearOfGraduation % 2000).ToString()}/{student.ID}";
        }

        #region Параметры сохранения отчета.

        /// <summary>
        /// Возвращает или задает флаг, указывающий на необходимость запрашивать путь для сохранения отчетов.
        /// </summary>
        public bool IsRequirePath { get; set; }

        /// <summary>
        /// Возвращает или задает путь для сохранения отчетов.
        /// </summary>
        public string SaveFolderPath { get; set; }

        /// <summary>
        /// Возвращает или задает флаг, указывающий на необходимость создания вложенной папки с текущей датой.
        /// </summary>
        public bool CreateSubfolder { get; set; }

        /// <summary>
        /// Возвращает или задает флаг, указывающий на необходимость открытия файла в редакторе по-умолчанию.
        /// </summary>
        public bool OpenAfterCreate { get; set; }

        #endregion

        #region Путь к файлам - шаблонам.

        /// <summary>
        /// Возвращает или задает путь к шаблону справки о самостоятельном трудоустройстве.
        /// </summary>
        public string SelfEmploymentPath { get; set; } = Directory.GetCurrentDirectory() + @"\templates\selfEmployment.docx";
        /// <summary>
        /// Возвращает или задает путь к шаблону подтверждения прибытия к справке о самостоятельном трудоустройстве.
        /// </summary>
        public string SelfEmploymentConfirmationOfArrivalPath { get; set; } = Directory.GetCurrentDirectory() + @"\templates\confirmationOfArrivalSelf.docx";
        /// <summary>
        /// Возвращает или задает путь к шаблону свидетельства о направлении на работу.
        /// </summary>
        public string CertificatePath { get; set; } = Directory.GetCurrentDirectory() + @"\templates\certificate.docx";
        /// <summary>
        /// Возвращает или задает путь к шаблону подтверждения прибытия к свидетельству о направлении на работу.
        /// </summary>
        public string ConfirmationOfArrival { get; set; } = Directory.GetCurrentDirectory() + @"\templates\confirmationOfArrival.docx";
        /// <summary>
        /// Возвращает или задает путь к шаблону уведомления к свидетельству о направлении на работу.
        /// </summary>
        public string NotificationPath { get; set; } = Directory.GetCurrentDirectory() + @"\templates\notification.docx";

        #endregion
    }
}
