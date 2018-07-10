using EmploymentDepartment.Properties;

namespace EmploymentDepartment
{
    partial class DataViewForm<T>
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainDgv = new ADGV.AdvancedDataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.noDataBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainDgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noDataBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainDgv
            // 
            this.mainDgv.AllowUserToAddRows = false;
            this.mainDgv.AllowUserToDeleteRows = false;
            this.mainDgv.AutoGenerateContextFilters = true;
            this.mainDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.mainDgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.mainDgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.mainDgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainDgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.mainDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDgv.DateWithTime = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDgv.DefaultCellStyle = dataGridViewCellStyle4;
            this.mainDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mainDgv.Location = new System.Drawing.Point(0, 0);
            this.mainDgv.MultiSelect = false;
            this.mainDgv.Name = "mainDgv";
            this.mainDgv.RowHeadersVisible = false;
            this.mainDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDgv.Size = new System.Drawing.Size(737, 399);
            this.mainDgv.TabIndex = 0;
            this.mainDgv.TimeFilter = false;
            this.mainDgv.SortStringChanged += new System.EventHandler(this.mainDgv_SortStringChanged);
            this.mainDgv.FilterStringChanged += new System.EventHandler(this.mainDgv_FilterStringChanged);
            this.mainDgv.DoubleClick += new System.EventHandler(this.mainDgv_DoubleClick);
            this.mainDgv.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.mainDgv_PreviewKeyDown);
            // 
            // noDataBox
            // 
            this.noDataBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noDataBox.Image = global::EmploymentDepartment.Properties.Resources.nodata;
            this.noDataBox.Location = new System.Drawing.Point(0, 0);
            this.noDataBox.Name = "noDataBox";
            this.noDataBox.Size = new System.Drawing.Size(737, 399);
            this.noDataBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.noDataBox.TabIndex = 1;
            this.noDataBox.TabStop = false;
            this.noDataBox.Visible = false;
            // 
            // DataViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 399);
            this.Controls.Add(this.noDataBox);
            this.Controls.Add(this.mainDgv);
            this.Icon = global::EmploymentDepartment.Properties.Resources._16;
            this.Name = "DataViewForm";
            this.Text = "DataViewForm";
            this.Load += new System.EventHandler(this.DataViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainDgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noDataBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ADGV.AdvancedDataGridView mainDgv;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.PictureBox noDataBox;
    }
}