using System;
using System.Windows.Forms;
using System.Linq;

namespace EmploymentDepartment
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Не передается как аргумент метода Run(), так как данную форму нужно скрывать 
            // в случае если данные сохранены пользователем.
            var login = new LoginForm(args.Contains("-debug"));

            Application.Run();
        }
    }
}
