namespace EmploymentDepartment
{
    partial class VacancyForm
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
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tbVacanciesCount = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSalary = new System.Windows.Forms.TextBox();
            this.companyPanel = new System.Windows.Forms.Panel();
            this.linkClear = new System.Windows.Forms.Label();
            this.linkCompany = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.lblPreferentialCategory = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFeatures = new System.Windows.Forms.TextBox();
            this.tbSalaryNote = new System.Windows.Forms.TextBox();
            this.tbWorkArea = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPost = new System.Windows.Forms.TextBox();
            this.lblVacanciesCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbVacancyNumber = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVacanciesCount)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.companyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.tbVacanciesCount);
            this.mainPanel.Controls.Add(this.tableLayoutPanel1);
            this.mainPanel.Controls.Add(this.companyPanel);
            this.mainPanel.Controls.Add(this.label8);
            this.mainPanel.Controls.Add(this.label41);
            this.mainPanel.Controls.Add(this.label10);
            this.mainPanel.Controls.Add(this.label9);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.tbFeatures);
            this.mainPanel.Controls.Add(this.tbSalaryNote);
            this.mainPanel.Controls.Add(this.tbWorkArea);
            this.mainPanel.Controls.Add(this.label7);
            this.mainPanel.Controls.Add(this.tbPost);
            this.mainPanel.Controls.Add(this.lblVacanciesCount);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.tbVacancyNumber);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(584, 456);
            this.mainPanel.TabIndex = 0;
            // 
            // tbVacanciesCount
            // 
            this.tbVacanciesCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVacanciesCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbVacanciesCount.Location = new System.Drawing.Point(497, 7);
            this.tbVacanciesCount.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.tbVacanciesCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbVacanciesCount.Name = "tbVacanciesCount";
            this.tbVacanciesCount.Size = new System.Drawing.Size(75, 23);
            this.tbVacanciesCount.TabIndex = 3;
            this.tbVacanciesCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 229);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(555, 38);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbGender);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(277, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 38);
            this.panel2.TabIndex = 1;
            // 
            // cmbGender
            // 
            this.cmbGender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.cmbGender, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.cmbGender.Items.AddRange(new object[] {
            "Мужской",
            "Женский",
            "Без разницы"});
            this.cmbGender.Location = new System.Drawing.Point(62, 9);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(213, 24);
            this.cmbGender.TabIndex = 2;
            this.cmbGender.Validating += new System.ComponentModel.CancelEventHandler(this.cmbGender_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(3, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Пол";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(33, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "*";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.tbSalary);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(277, 38);
            this.panel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(-3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Оклад";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(43, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "*";
            // 
            // tbSalary
            // 
            this.tbSalary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.tbSalary, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbSalary.Location = new System.Drawing.Point(74, 9);
            this.tbSalary.MaxLength = 10;
            this.tbSalary.Name = "tbSalary";
            this.tbSalary.Size = new System.Drawing.Size(200, 22);
            this.tbSalary.TabIndex = 2;
            this.tbSalary.Validating += new System.ComponentModel.CancelEventHandler(this.tbSalaryDecimal_Validating);
            // 
            // companyPanel
            // 
            this.companyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.companyPanel.Controls.Add(this.linkClear);
            this.companyPanel.Controls.Add(this.linkCompany);
            this.companyPanel.Controls.Add(this.label11);
            this.companyPanel.Controls.Add(this.lblPreferentialCategory);
            this.companyPanel.Location = new System.Drawing.Point(15, 76);
            this.companyPanel.Name = "companyPanel";
            this.companyPanel.Size = new System.Drawing.Size(557, 47);
            this.companyPanel.TabIndex = 6;
            // 
            // linkClear
            // 
            this.linkClear.AutoSize = true;
            this.linkClear.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkClear.ForeColor = System.Drawing.Color.Blue;
            this.linkClear.Location = new System.Drawing.Point(20, 24);
            this.linkClear.Name = "linkClear";
            this.linkClear.Size = new System.Drawing.Size(71, 16);
            this.linkClear.TabIndex = 0;
            this.linkClear.Text = "Очистить";
            this.linkClear.Click += new System.EventHandler(this.linkClear_Click);
            // 
            // linkCompany
            // 
            this.linkCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkCompany.AutoEllipsis = true;
            this.linkCompany.AutoSize = true;
            this.linkCompany.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.linkCompany, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.linkCompany.Location = new System.Drawing.Point(123, 15);
            this.linkCompany.Name = "linkCompany";
            this.linkCompany.Size = new System.Drawing.Size(170, 16);
            this.linkCompany.TabIndex = 3;
            this.linkCompany.TabStop = true;
            this.linkCompany.Tag = "Выбрать предприятие...";
            this.linkCompany.Text = "Выбрать предприятие...";
            this.linkCompany.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCompany_LinkClicked);
            this.linkCompany.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.linkCompany_PreviewKeyDown);
            this.linkCompany.Validating += new System.ComponentModel.CancelEventHandler(this.linkCompany_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.Tomato;
            this.label11.Location = new System.Drawing.Point(89, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 16);
            this.label11.TabIndex = 2;
            this.label11.Text = "*";
            // 
            // lblPreferentialCategory
            // 
            this.lblPreferentialCategory.AutoSize = true;
            this.lblPreferentialCategory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreferentialCategory.Location = new System.Drawing.Point(-3, 4);
            this.lblPreferentialCategory.Name = "lblPreferentialCategory";
            this.lblPreferentialCategory.Size = new System.Drawing.Size(96, 16);
            this.lblPreferentialCategory.TabIndex = 1;
            this.lblPreferentialCategory.Text = "Предприятие";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Tomato;
            this.label8.Location = new System.Drawing.Point(92, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "*";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label41.ForeColor = System.Drawing.Color.Tomato;
            this.label41.Location = new System.Drawing.Point(124, 9);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(14, 16);
            this.label41.TabIndex = 1;
            this.label41.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(12, 370);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 32);
            this.label10.TabIndex = 12;
            this.label10.Text = "Дополнительная\r\nинформация";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(12, 287);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 32);
            this.label9.TabIndex = 10;
            this.label9.Text = "Условия оплаты \r\nтруда";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Рабочая область";
            // 
            // tbFeatures
            // 
            this.tbFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFeatures.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbFeatures.Location = new System.Drawing.Point(141, 367);
            this.tbFeatures.MaxLength = 300;
            this.tbFeatures.Multiline = true;
            this.tbFeatures.Name = "tbFeatures";
            this.tbFeatures.Size = new System.Drawing.Size(431, 66);
            this.tbFeatures.TabIndex = 13;
            // 
            // tbSalaryNote
            // 
            this.tbSalaryNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSalaryNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSalaryNote.Location = new System.Drawing.Point(141, 284);
            this.tbSalaryNote.MaxLength = 300;
            this.tbSalaryNote.Multiline = true;
            this.tbSalaryNote.Name = "tbSalaryNote";
            this.tbSalaryNote.Size = new System.Drawing.Size(431, 66);
            this.tbSalaryNote.TabIndex = 11;
            // 
            // tbWorkArea
            // 
            this.tbWorkArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWorkArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbWorkArea.Location = new System.Drawing.Point(141, 145);
            this.tbWorkArea.MaxLength = 300;
            this.tbWorkArea.Multiline = true;
            this.tbWorkArea.Name = "tbWorkArea";
            this.tbWorkArea.Size = new System.Drawing.Size(431, 66);
            this.tbWorkArea.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(12, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Должность";
            // 
            // tbPost
            // 
            this.tbPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.tbPost, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbPost.Location = new System.Drawing.Point(156, 39);
            this.tbPost.MaxLength = 200;
            this.tbPost.Name = "tbPost";
            this.tbPost.Size = new System.Drawing.Size(416, 22);
            this.tbPost.TabIndex = 5;
            this.tbPost.Validating += new System.ComponentModel.CancelEventHandler(this.tbSalary_Validating);
            // 
            // lblVacanciesCount
            // 
            this.lblVacanciesCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVacanciesCount.AutoSize = true;
            this.lblVacanciesCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVacanciesCount.Location = new System.Drawing.Point(367, 9);
            this.lblVacanciesCount.Name = "lblVacanciesCount";
            this.lblVacanciesCount.Size = new System.Drawing.Size(124, 16);
            this.lblVacanciesCount.TabIndex = 0;
            this.lblVacanciesCount.Text = "Количество мест";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Шифр вакансии";
            // 
            // tbVacancyNumber
            // 
            this.tbVacancyNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVacancyNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.tbVacancyNumber, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbVacancyNumber.Location = new System.Drawing.Point(156, 6);
            this.tbVacancyNumber.MaxLength = 50;
            this.tbVacancyNumber.Name = "tbVacancyNumber";
            this.tbVacancyNumber.Size = new System.Drawing.Size(205, 22);
            this.tbVacancyNumber.TabIndex = 2;
            this.tbVacancyNumber.Validating += new System.ComponentModel.CancelEventHandler(this.tbSalary_Validating);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // VacancyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 456);
            this.Controls.Add(this.mainPanel);
            this.Icon = global::EmploymentDepartment.Properties.Resources._16;
            this.MinimumSize = new System.Drawing.Size(600, 495);
            this.Name = "VacancyForm";
            this.Text = "Добавление вакансии";
            this.Load += new System.EventHandler(this.VacancyForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVacanciesCount)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.companyPanel.ResumeLayout(false);
            this.companyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSalary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbVacancyNumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel companyPanel;
        private System.Windows.Forms.Label linkClear;
        private System.Windows.Forms.LinkLabel linkCompany;
        private System.Windows.Forms.Label lblPreferentialCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbWorkArea;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbFeatures;
        private System.Windows.Forms.TextBox tbSalaryNote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPost;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.NumericUpDown tbVacanciesCount;
        private System.Windows.Forms.Label lblVacanciesCount;
    }
}