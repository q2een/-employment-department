namespace EmploymentDepartment
{
    partial class PreferentialCategoryForm
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tbEdit
            // 
            this.errorProvider.SetIconAlignment(this.tbEdit, System.Windows.Forms.ErrorIconAlignment.BottomRight);
            this.errorProvider.SetIconPadding(this.tbEdit, -35);
            this.tbEdit.MaxLength = 2000;
            this.tbEdit.Size = new System.Drawing.Size(464, 201);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 201);
            this.panel1.Size = new System.Drawing.Size(464, 38);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnApply.Location = new System.Drawing.Point(341, 6);
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // PreferentialCategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 239);
            this.Name = "PreferentialCategoryForm";
            this.Text = "Добавление льготной категории";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}