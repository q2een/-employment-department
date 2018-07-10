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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // TODO ADD USER ROLE CHECK
        private UserRole GetRole(List<Dictionary<string, object>> grants)
        {
            var role = UserRole.None;

           /* foreach (var dict in grants)
            {
                foreach (var value in dict.Values)
                {  
                    if (!value.ToString().Contains("ON *.*") && !value.ToString().Contains("ON *.*"))
                       //if(value.ToString().Contains("INSERT") && )
                    }   
            }    */

            return role;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                var connection = $"Database=work;Data Source={tbHost.Text};Port={tbPort.Text};User Id={tbLogin.Text};Password={tbPassword.Text};CharSet=utf8;";

                var db = new MySqlDB(connection);

                var grants = db.GetCollection("SHOW GRANTS FOR CURRENT_USER()");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
