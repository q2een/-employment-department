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
    public partial class CompanyFrom : Form
    {
        public Company Company { get; private set; }
        public ActionType Type { get; private set; }

        public CompanyFrom(ActionType type, Company company = null)
        {
            if (type == ActionType.Edit && company == null)
                throw new ArgumentNullException();

            InitializeComponent();

            this.Company = company;
            this.Type = type;

            if (type == ActionType.Edit)
            {
                btnRemove.Visible = true;
                btnApply.Text = "Сохранить";
                this.Text = $"Редактирование информации о предприятии [{company.Name}]";
            }
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            new EditModalForm((sender as Label).Tag as TextBox).ShowDialog();
        }

        private void lblEdit_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.RoyalBlue;
        }

        private void lblEdit_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = SystemColors.ControlText;
        }

        private void tbSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extentions.SurnameKeyPressValidator(e.KeyChar);
        }
    }
}
