namespace EmploymentDepartment
{
    partial class StudentCompanyForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbYearOfEmployment = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbNote = new System.Windows.Forms.TextBox();
            this.tbPost = new System.Windows.Forms.TextBox();
            this.tbCompany = new System.Windows.Forms.TextBox();
            this.vacancyPanel = new System.Windows.Forms.Panel();
            this.linkVacancyClear = new System.Windows.Forms.Label();
            this.linkVacancy = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbUnivercityEmployment = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.studentPanel = new System.Windows.Forms.Panel();
            this.linkStudentClear = new System.Windows.Forms.Label();
            this.linkStudent = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.lblPreferentialCategory = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.mainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.vacancyPanel.SuspendLayout();
            this.studentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.tableLayoutPanel1);
            this.mainPanel.Controls.Add(this.tbNote);
            this.mainPanel.Controls.Add(this.tbPost);
            this.mainPanel.Controls.Add(this.tbCompany);
            this.mainPanel.Controls.Add(this.vacancyPanel);
            this.mainPanel.Controls.Add(this.label7);
            this.mainPanel.Controls.Add(this.label5);
            this.mainPanel.Controls.Add(this.cbUnivercityEmployment);
            this.mainPanel.Controls.Add(this.label14);
            this.mainPanel.Controls.Add(this.label6);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.studentPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(549, 436);
            this.mainPanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 228);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(525, 60);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbStatus);
            this.panel3.Controls.Add(this.label32);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(262, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(263, 60);
            this.panel3.TabIndex = 1;
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.errorProvider.SetIconPadding(this.cmbStatus, -35);
            this.cmbStatus.Items.AddRange(new object[] {
            "Работает",
            "Не работает"});
            this.cmbStatus.Location = new System.Drawing.Point(6, 32);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(257, 24);
            this.cmbStatus.TabIndex = 2;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label32.ForeColor = System.Drawing.Color.Tomato;
            this.label32.Location = new System.Drawing.Point(54, 7);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(14, 16);
            this.label32.TabIndex = 1;
            this.label32.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "Статус";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbYearOfEmployment);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(262, 60);
            this.panel4.TabIndex = 0;
            // 
            // tbYearOfEmployment
            // 
            this.tbYearOfEmployment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbYearOfEmployment.BeepOnError = true;
            this.tbYearOfEmployment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorProvider.SetIconPadding(this.tbYearOfEmployment, -35);
            this.tbYearOfEmployment.Location = new System.Drawing.Point(0, 32);
            this.tbYearOfEmployment.Mask = "0000";
            this.tbYearOfEmployment.Name = "tbYearOfEmployment";
            this.tbYearOfEmployment.Size = new System.Drawing.Size(243, 23);
            this.tbYearOfEmployment.TabIndex = 2;
            this.tbYearOfEmployment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbYearOfEmployment.Validating += new System.ComponentModel.CancelEventHandler(this.tbYearOfEmployment_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(-3, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(153, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Год трудоустройства";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.Color.Tomato;
            this.label12.Location = new System.Drawing.Point(147, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 16);
            this.label12.TabIndex = 1;
            this.label12.Text = "*";
            // 
            // tbNote
            // 
            this.tbNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNote.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNote.Location = new System.Drawing.Point(107, 304);
            this.tbNote.MaxLength = 3000;
            this.tbNote.Multiline = true;
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(430, 120);
            this.tbNote.TabIndex = 12;
            // 
            // tbPost
            // 
            this.tbPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPost.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.tbPost, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbPost.Location = new System.Drawing.Point(138, 190);
            this.tbPost.MaxLength = 200;
            this.tbPost.Name = "tbPost";
            this.tbPost.Size = new System.Drawing.Size(399, 23);
            this.tbPost.TabIndex = 9;
            // 
            // tbCompany
            // 
            this.tbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCompany.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.tbCompany, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tbCompany.Location = new System.Drawing.Point(138, 156);
            this.tbCompany.MaxLength = 200;
            this.tbCompany.Name = "tbCompany";
            this.tbCompany.Size = new System.Drawing.Size(399, 23);
            this.tbCompany.TabIndex = 6;
            // 
            // vacancyPanel
            // 
            this.vacancyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vacancyPanel.Controls.Add(this.linkVacancyClear);
            this.vacancyPanel.Controls.Add(this.linkVacancy);
            this.vacancyPanel.Controls.Add(this.label2);
            this.vacancyPanel.Controls.Add(this.label3);
            this.vacancyPanel.Enabled = false;
            this.vacancyPanel.Location = new System.Drawing.Point(12, 92);
            this.vacancyPanel.Name = "vacancyPanel";
            this.vacancyPanel.Size = new System.Drawing.Size(525, 47);
            this.vacancyPanel.TabIndex = 3;
            // 
            // linkVacancyClear
            // 
            this.linkVacancyClear.AutoSize = true;
            this.linkVacancyClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkVacancyClear.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkVacancyClear.ForeColor = System.Drawing.Color.Blue;
            this.linkVacancyClear.Location = new System.Drawing.Point(0, 22);
            this.linkVacancyClear.Name = "linkVacancyClear";
            this.linkVacancyClear.Size = new System.Drawing.Size(71, 16);
            this.linkVacancyClear.TabIndex = 3;
            this.linkVacancyClear.Text = "Очистить";
            this.linkVacancyClear.Click += new System.EventHandler(this.linkClear_Click);
            // 
            // linkVacancy
            // 
            this.linkVacancy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkVacancy.AutoEllipsis = true;
            this.linkVacancy.AutoSize = true;
            this.linkVacancy.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.linkVacancy, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.linkVacancy.Location = new System.Drawing.Point(97, 15);
            this.linkVacancy.Name = "linkVacancy";
            this.linkVacancy.Size = new System.Drawing.Size(150, 16);
            this.linkVacancy.TabIndex = 2;
            this.linkVacancy.TabStop = true;
            this.linkVacancy.Tag = "Выбрать вакансию...";
            this.linkVacancy.Text = "Выбрать вакансию...";
            this.linkVacancy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVacancy_LinkClicked);
            this.linkVacancy.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.link_PreviewKeyDown);
            this.linkVacancy.Validating += new System.ComponentModel.CancelEventHandler(this.linkVacancy_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(66, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Вакансия";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Tomato;
            this.label7.Location = new System.Drawing.Point(92, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "*";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(104, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "*";
            // 
            // cbUnivercityEmployment
            // 
            this.cbUnivercityEmployment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUnivercityEmployment.AutoSize = true;
            this.cbUnivercityEmployment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUnivercityEmployment.Location = new System.Drawing.Point(12, 67);
            this.cbUnivercityEmployment.Name = "cbUnivercityEmployment";
            this.cbUnivercityEmployment.Size = new System.Drawing.Size(231, 20);
            this.cbUnivercityEmployment.TabIndex = 2;
            this.cbUnivercityEmployment.Text = "Трудоустроен университетом";
            this.cbUnivercityEmployment.UseVisualStyleBackColor = true;
            this.cbUnivercityEmployment.CheckedChanged += new System.EventHandler(this.cbUnivercityEmployment_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 304);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 16);
            this.label14.TabIndex = 11;
            this.label14.Text = "Примечание";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Должность";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Предприятие";
            // 
            // studentPanel
            // 
            this.studentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.studentPanel.Controls.Add(this.linkStudentClear);
            this.studentPanel.Controls.Add(this.linkStudent);
            this.studentPanel.Controls.Add(this.label11);
            this.studentPanel.Controls.Add(this.lblPreferentialCategory);
            this.studentPanel.Location = new System.Drawing.Point(12, 12);
            this.studentPanel.Name = "studentPanel";
            this.studentPanel.Size = new System.Drawing.Size(525, 47);
            this.studentPanel.TabIndex = 1;
            // 
            // linkStudentClear
            // 
            this.linkStudentClear.AutoSize = true;
            this.linkStudentClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkStudentClear.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkStudentClear.ForeColor = System.Drawing.Color.Blue;
            this.linkStudentClear.Location = new System.Drawing.Point(0, 22);
            this.linkStudentClear.Name = "linkStudentClear";
            this.linkStudentClear.Size = new System.Drawing.Size(71, 16);
            this.linkStudentClear.TabIndex = 3;
            this.linkStudentClear.Text = "Очистить";
            this.linkStudentClear.Click += new System.EventHandler(this.linkClear_Click);
            // 
            // linkStudent
            // 
            this.linkStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkStudent.AutoEllipsis = true;
            this.linkStudent.AutoSize = true;
            this.linkStudent.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorProvider.SetIconAlignment(this.linkStudent, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.linkStudent.Location = new System.Drawing.Point(97, 14);
            this.linkStudent.Name = "linkStudent";
            this.linkStudent.Size = new System.Drawing.Size(145, 16);
            this.linkStudent.TabIndex = 2;
            this.linkStudent.TabStop = true;
            this.linkStudent.Tag = "Выбрать студента";
            this.linkStudent.Text = "Выбрать студента...";
            this.linkStudent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkStudent_LinkClicked);
            this.linkStudent.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.link_PreviewKeyDown);
            this.linkStudent.Validating += new System.ComponentModel.CancelEventHandler(this.linkStudent_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.Tomato;
            this.label11.Location = new System.Drawing.Point(57, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 16);
            this.label11.TabIndex = 1;
            this.label11.Text = "*";
            // 
            // lblPreferentialCategory
            // 
            this.lblPreferentialCategory.AutoSize = true;
            this.lblPreferentialCategory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreferentialCategory.Location = new System.Drawing.Point(0, 4);
            this.lblPreferentialCategory.Name = "lblPreferentialCategory";
            this.lblPreferentialCategory.Size = new System.Drawing.Size(63, 16);
            this.lblPreferentialCategory.TabIndex = 0;
            this.lblPreferentialCategory.Text = "Студент";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 500;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // StudentCompanyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 436);
            this.Controls.Add(this.mainPanel);
            this.Icon = Properties.Resources._16;
            this.MinimumSize = new System.Drawing.Size(565, 475);
            this.Name = "StudentCompanyForm";
            this.Text = "Добавление информации о месте работы";
            this.Load += new System.EventHandler(this.StudentCompanyForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.vacancyPanel.ResumeLayout(false);
            this.vacancyPanel.PerformLayout();
            this.studentPanel.ResumeLayout(false);
            this.studentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbPost;
        private System.Windows.Forms.TextBox tbCompany;
        private System.Windows.Forms.Panel vacancyPanel;
        private System.Windows.Forms.Label linkVacancyClear;
        private System.Windows.Forms.LinkLabel linkVacancy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbUnivercityEmployment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel studentPanel;
        private System.Windows.Forms.Label linkStudentClear;
        private System.Windows.Forms.LinkLabel linkStudent;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblPreferentialCategory;
        private System.Windows.Forms.MaskedTextBox tbYearOfEmployment;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbNote;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}