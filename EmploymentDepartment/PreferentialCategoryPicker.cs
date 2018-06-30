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
        public PreferentialCategoryPicker()
        {
            InitializeComponent();
        }

        private void PreferentialCategoryPicker_Load(object sender, EventArgs e)
        {
            listView1.EnsureVisible(1);
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
           /* if (e.KeyCode == Keys.Up)
            {
                listView1.Items[0].EnsureVisible();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                listView1.Items[1].EnsureVisible(); ;
                e.Handled = true;
            }      */
        }
    }
}
