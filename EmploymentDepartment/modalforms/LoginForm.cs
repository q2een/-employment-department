using EmploymentDepartment.BL;
using System;
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
                 ShowMainForm(Properties.Settings.Default.connection);
             else
                 this.Show();     
        }
        
        // Отображает главное окно программы при корректно введенных пользователем данных.
        private void ShowMainForm(string connection)
        {
            try
            {
                var db = new MySqlDB(connection);

                var role = db.GetUserRole();

                if (role == UserRole.None)
                    throw new Exception("У Вас нет прав для доступа к базе данных");

                if (cbRemeber.Checked)
                {
                    Properties.Settings.Default.connection = connection;
                    Properties.Settings.Default.Save();
                }

                var form = new MainMDIForm(db, role);

                // Очищаем поля логин-пароль для того, чтобы после вызова смены пользователя нельзя было снова зайти под теми же данными.
                // Необходимо так как данная форма не закрывается и снова вызыается, а просто скрывается.
                tbLogin.Text = String.Empty;
                tbPassword.Text = String.Empty;

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

        // Обработка события закрытия окна.
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрытие приложения. Закрывает приложение и в случае, если закрывается главное окно программы
            // так как родителем главного окна является данный экземпляр окна.
            Application.Exit();
        }
    }
}
