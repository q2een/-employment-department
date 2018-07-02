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
    public partial class ChildForm : Form
    {      
        public ActionType Type { get; private set; }
        protected MainMDIForm main;

        public ChildForm()
        {
            InitializeComponent();
        }

        public ChildForm(ActionType type)
        {

            InitializeComponent();

            this.Type = type;

            this.Load += ChildForm_Load;
        }

        private void ChildForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm))
                throw new Exception();

            this.main = this.MdiParent as MainMDIForm;
            InitFiels();
        }

        public virtual void InitFiels() { }

        protected void lblEdit_Click(object sender, EventArgs e)
        {
            new EditModalForm((sender as Label).Tag as TextBox).ShowDialog();
        }

        protected void lblEdit_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.RoyalBlue;
        }

        protected void lblEdit_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = SystemColors.ControlText;
        }
    }
}
