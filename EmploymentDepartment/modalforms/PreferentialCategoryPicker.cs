using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public partial class PreferentialCategoryPicker : Form
    {
        private readonly Control control;
        private readonly string value;

        public PreferentialCategoryPicker(List<PreferentialCategory> categories, Control control, string value = "")
        {
            if (categories == null)
                throw new ArgumentNullException();

            this.control = control;
            this.value = value;

            InitializeComponent();

            mainDgv.DataSource = categories;             
        }

        private void PreferentialCategoryPicker_Load(object sender, EventArgs e)
        {
            mainDgv.ColumnHeadersVisible = false;
          
            mainDgv.Columns[1].ReadOnly = true;
            mainDgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            mainDgv.Columns[1].FillWeight = 100;

            mainDgv.Columns[0].Visible = false;

            foreach (DataGridViewColumn c in mainDgv.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Verdana", 9.75F, GraphicsUnit.Point);
            }

            SelectRowByCategoryID(value);
        }

        private void SelectRowByCategoryID(string value)
        {
            foreach (DataGridViewRow row in mainDgv.Rows)
                if (row.Cells[1].Value.ToString().Equals(value))
                    row.Cells[1].Selected = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var selected = mainDgv.SelectedRows[0];         
            control.Tag = selected.Cells[1].Value.ToString();
            this.Close();
        }

        private void mainDgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnApply_Click(sender,null);
                e.Handled = true;
                return;
            }
        }
    }
}
