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
    public partial class EditModalForm : Form
    {
        private readonly TextBox tb;

        public EditModalForm(TextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
            tbEdit.Text = tb.Text;            
        }

        private void EditModalForm_Load(object sender, EventArgs e)
        {
            tbEdit.Select(tbEdit.Text.Length, 0);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            tb.Text = tbEdit.Text;
            this.Close();
        }


    }
}
