using System;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Не передается как аргумент метода Run(), так как данную форму нужно скрывать 
            // в случае если данные сохранены пользователем.
            var login = new LoginForm();

            Application.Run();
        }
    }
}
