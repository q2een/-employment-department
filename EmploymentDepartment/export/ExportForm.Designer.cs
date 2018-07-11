namespace EmploymentDepartment
{
    partial class ExportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbList = new System.Windows.Forms.RadioButton();
            this.rbFiles = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbTables = new System.Windows.Forms.GroupBox();
            this.cbVacancies = new System.Windows.Forms.CheckBox();
            this.cbPreferentialCategories = new System.Windows.Forms.CheckBox();
            this.cbStudentCompanies = new System.Windows.Forms.CheckBox();
            this.cbSpecializations = new System.Windows.Forms.CheckBox();
            this.cbFaculties = new System.Windows.Forms.CheckBox();
            this.cbCompanies = new System.Windows.Forms.CheckBox();
            this.cbSudents = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.gbTables.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbList);
            this.groupBox1.Controls.Add(this.rbFiles);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.tbPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // rbList
            // 
            this.rbList.AutoSize = true;
            this.rbList.Checked = true;
            this.rbList.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbList.Location = new System.Drawing.Point(6, 97);
            this.rbList.Name = "rbList";
            this.rbList.Size = new System.Drawing.Size(279, 20);
            this.rbList.TabIndex = 4;
            this.rbList.TabStop = true;
            this.rbList.Text = "Каждая таблица на отдельном листе";
            this.rbList.UseVisualStyleBackColor = true;
            // 
            // rbFiles
            // 
            this.rbFiles.AutoSize = true;
            this.rbFiles.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbFiles.Location = new System.Drawing.Point(6, 71);
            this.rbFiles.Name = "rbFiles";
            this.rbFiles.Size = new System.Drawing.Size(275, 20);
            this.rbFiles.TabIndex = 3;
            this.rbFiles.Text = "Каждая таблица в отдельном файле";
            this.rbFiles.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowse.Location = new System.Drawing.Point(331, 42);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Обзор";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbPath
            // 
            this.tbPath.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPath.Location = new System.Drawing.Point(6, 42);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(322, 23);
            this.tbPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Путь к папке для экспорта";
            // 
            // gbTables
            // 
            this.gbTables.Controls.Add(this.cbVacancies);
            this.gbTables.Controls.Add(this.cbPreferentialCategories);
            this.gbTables.Controls.Add(this.cbStudentCompanies);
            this.gbTables.Controls.Add(this.cbSpecializations);
            this.gbTables.Controls.Add(this.cbFaculties);
            this.gbTables.Controls.Add(this.cbCompanies);
            this.gbTables.Controls.Add(this.cbSudents);
            this.gbTables.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbTables.Location = new System.Drawing.Point(12, 144);
            this.gbTables.Name = "gbTables";
            this.gbTables.Size = new System.Drawing.Size(409, 125);
            this.gbTables.TabIndex = 1;
            this.gbTables.TabStop = false;
            this.gbTables.Text = "Таблицы для экспорта";
            // 
            // cbVacancies
            // 
            this.cbVacancies.AutoSize = true;
            this.cbVacancies.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbVacancies.Location = new System.Drawing.Point(6, 98);
            this.cbVacancies.Name = "cbVacancies";
            this.cbVacancies.Size = new System.Drawing.Size(91, 20);
            this.cbVacancies.TabIndex = 3;
            this.cbVacancies.Text = "Вакансии";
            this.cbVacancies.UseVisualStyleBackColor = true;
            // 
            // cbPreferentialCategories
            // 
            this.cbPreferentialCategories.AutoSize = true;
            this.cbPreferentialCategories.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbPreferentialCategories.Location = new System.Drawing.Point(137, 81);
            this.cbPreferentialCategories.Name = "cbPreferentialCategories";
            this.cbPreferentialCategories.Size = new System.Drawing.Size(164, 20);
            this.cbPreferentialCategories.TabIndex = 6;
            this.cbPreferentialCategories.Text = "Льготные категории";
            this.cbPreferentialCategories.UseVisualStyleBackColor = true;
            // 
            // cbStudentCompanies
            // 
            this.cbStudentCompanies.AutoSize = true;
            this.cbStudentCompanies.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbStudentCompanies.Location = new System.Drawing.Point(137, 29);
            this.cbStudentCompanies.Name = "cbStudentCompanies";
            this.cbStudentCompanies.Size = new System.Drawing.Size(197, 20);
            this.cbStudentCompanies.TabIndex = 4;
            this.cbStudentCompanies.Text = "Места работы студентов";
            this.cbStudentCompanies.UseVisualStyleBackColor = true;
            // 
            // cbSpecializations
            // 
            this.cbSpecializations.AutoSize = true;
            this.cbSpecializations.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSpecializations.Location = new System.Drawing.Point(137, 55);
            this.cbSpecializations.Name = "cbSpecializations";
            this.cbSpecializations.Size = new System.Drawing.Size(169, 20);
            this.cbSpecializations.TabIndex = 5;
            this.cbSpecializations.Text = "Профили подготовки";
            this.cbSpecializations.UseVisualStyleBackColor = true;
            // 
            // cbFaculties
            // 
            this.cbFaculties.AutoSize = true;
            this.cbFaculties.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbFaculties.Location = new System.Drawing.Point(6, 75);
            this.cbFaculties.Name = "cbFaculties";
            this.cbFaculties.Size = new System.Drawing.Size(108, 20);
            this.cbFaculties.TabIndex = 2;
            this.cbFaculties.Text = "Факультеты";
            this.cbFaculties.UseVisualStyleBackColor = true;
            // 
            // cbCompanies
            // 
            this.cbCompanies.AutoSize = true;
            this.cbCompanies.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCompanies.Location = new System.Drawing.Point(6, 52);
            this.cbCompanies.Name = "cbCompanies";
            this.cbCompanies.Size = new System.Drawing.Size(115, 20);
            this.cbCompanies.TabIndex = 1;
            this.cbCompanies.Text = "Предприятия";
            this.cbCompanies.UseVisualStyleBackColor = true;
            // 
            // cbSudents
            // 
            this.cbSudents.AutoSize = true;
            this.cbSudents.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSudents.Location = new System.Drawing.Point(6, 29);
            this.cbSudents.Name = "cbSudents";
            this.cbSudents.Size = new System.Drawing.Size(91, 20);
            this.cbSudents.TabIndex = 0;
            this.cbSudents.Text = "Студенты";
            this.cbSudents.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExport.Location = new System.Drawing.Point(309, 275);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(112, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Подтвердить";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 305);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gbTables);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Export";
            this.Text = "Экспорт данных";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbTables.ResumeLayout(false);
            this.gbTables.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbList;
        private System.Windows.Forms.RadioButton rbFiles;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbTables;
        private System.Windows.Forms.CheckBox cbVacancies;
        private System.Windows.Forms.CheckBox cbPreferentialCategories;
        private System.Windows.Forms.CheckBox cbStudentCompanies;
        private System.Windows.Forms.CheckBox cbSpecializations;
        private System.Windows.Forms.CheckBox cbFaculties;
        private System.Windows.Forms.CheckBox cbCompanies;
        private System.Windows.Forms.CheckBox cbSudents;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}