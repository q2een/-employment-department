namespace EmploymentDepartment
{
    partial class ReportSettingsForm
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
            this.cbRequirePath = new System.Windows.Forms.CheckBox();
            this.cbCreateDateFolder = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbOpenFile = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbRequirePath
            // 
            this.cbRequirePath.AutoSize = true;
            this.cbRequirePath.Checked = true;
            this.cbRequirePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRequirePath.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbRequirePath.Location = new System.Drawing.Point(12, 12);
            this.cbRequirePath.Name = "cbRequirePath";
            this.cbRequirePath.Size = new System.Drawing.Size(315, 20);
            this.cbRequirePath.TabIndex = 0;
            this.cbRequirePath.Text = "Запрашивать путь для сохранения отчета";
            this.cbRequirePath.UseVisualStyleBackColor = true;
            this.cbRequirePath.CheckedChanged += new System.EventHandler(this.cbRequirePath_CheckedChanged);
            // 
            // cbCreateDateFolder
            // 
            this.cbCreateDateFolder.AutoSize = true;
            this.cbCreateDateFolder.Enabled = false;
            this.cbCreateDateFolder.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCreateDateFolder.Location = new System.Drawing.Point(12, 64);
            this.cbCreateDateFolder.Name = "cbCreateDateFolder";
            this.cbCreateDateFolder.Size = new System.Drawing.Size(346, 20);
            this.cbCreateDateFolder.TabIndex = 0;
            this.cbCreateDateFolder.Text = "Создавать вложенную папку с текущей датой";
            this.cbCreateDateFolder.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowse.Location = new System.Drawing.Point(322, 124);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Обзор";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbPath
            // 
            this.tbPath.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPath.Location = new System.Drawing.Point(12, 124);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(304, 23);
            this.tbPath.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(157, 163);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbOpenFile
            // 
            this.cbOpenFile.AutoSize = true;
            this.cbOpenFile.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbOpenFile.Location = new System.Drawing.Point(12, 38);
            this.cbOpenFile.Name = "cbOpenFile";
            this.cbOpenFile.Size = new System.Drawing.Size(271, 20);
            this.cbOpenFile.TabIndex = 0;
            this.cbOpenFile.Text = "Открывать отчет после сохранения";
            this.cbOpenFile.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Папка для сохранения отчетов";
            // 
            // ReportSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 196);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.cbOpenFile);
            this.Controls.Add(this.cbCreateDateFolder);
            this.Controls.Add(this.cbRequirePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 177);
            this.Name = "ReportSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры отчетов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbRequirePath;
        private System.Windows.Forms.CheckBox cbCreateDateFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox cbOpenFile;
        private System.Windows.Forms.Label label1;
    }
}