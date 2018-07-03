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
        private readonly StudentForm studentForm;
        private readonly List<PreferentialCategory> categories;

        public PreferentialCategoryPicker(List<PreferentialCategory> categories, StudentForm studentForm)
        {
            if (categories == null || studentForm == null)
                throw new ArgumentNullException();

            this.studentForm = studentForm;
            this.categories = categories;

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

            var elen = categories.FirstOrDefault(i => studentForm.LinkPreferentialCategory.IsPropertiesAreEqual((IPreferentialCategory)i));
            var index = categories.IndexOf(elen);

            if (index >= 0)
                mainDgv.Rows[index].Cells[1].Selected = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var selected = mainDgv.SelectedRows[0].Index;
            studentForm.LinkPreferentialCategory = categories[selected];
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
