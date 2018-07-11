using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет окно для входа в программу и подключения к БД.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Предоставляет окно для входа в программу и подключения к БД.
        /// </summary>
        public LoginForm()
        {   
            InitializeComponent();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.connection))
            {
                this.Hide();
                ShowMainForm(Properties.Settings.Default.connection);
            }
        }

        // Возвращает роль пользователя в зависимости от полученных из БД данных.
        private UserRole GetRole(List<Dictionary<string, object>> grants)
        {
            foreach (var dict in grants)
                foreach (var value in dict.Values)
                {
                    if (!value.ToString().Contains("ON *.*") && !value.ToString().Contains("ON `work`"))
                        continue;

                    if (value.ToString().Contains("ALL PRIVILEGES"))
                        return UserRole.Administrator;

                    if (value.ToString().Contains("SELECT") && value.ToString().Contains("INSERT") && value.ToString().Contains("UPDATE"))
                    {
                        if (value.ToString().Contains("DELETE"))
                            return UserRole.Administrator;

                        return UserRole.Moderator;
                    }
                }  

            return UserRole.None;
        }

        // Отображает главное окно программы при корректно введенных пользователем данных.
        private void ShowMainForm(string connection)
        {
            try
            {
                var db = new MySqlDB(connection);

                var grants = db.GetCollection("SHOW GRANTS FOR CURRENT_USER()");

                var role = GetRole(grants);

                if (role == UserRole.None)
                    throw new Exception("У Вас нет прав для доступа к базе данных");

                if (cbRemeber.Checked)
                {
                    Properties.Settings.Default.connection = connection;
                    Properties.Settings.Default.Save();
                }

                var form = new MainMDIForm(db, role);
                this.Hide();
                form.Show(this);

            }
            catch (Exception ex)
            {
                this.Show();
                MessageBox.Show(ex.Message, "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Нажатие на кнопку "Войти". Обработка события.
        private void btnApply_Click(object sender, EventArgs e)
        {
            var connection = $"Database=work;Data Source={tbHost.Text};Port={tbPort.Text};User Id={tbLogin.Text};Password={tbPassword.Text};CharSet=utf8;";
            ShowMainForm(connection);
        }

        // Проверка корректности ввода данных в поле "Порт". Разрешен ввод только цифр.
        private void tbPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '.' || (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back);
        }
    }
}
