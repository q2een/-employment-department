using System;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет модальное окно для редактирования информации в текстовом поле.
    /// </summary>
    public partial class EditModalForm : Form
    {
        private readonly TextBox tb;

        /// <summary>
        /// Предоставляет модальное окно для редактирования информации в текстовом поле.
        /// </summary>
        /// <param name="tb">Тектовое поле</param>
        public EditModalForm(TextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
            tbEdit.Text = tb.Text;
            tbEdit.MaxLength = tb.MaxLength;            
        }

        // Обработка события загрузки окна.
        private void EditModalForm_Load(object sender, EventArgs e)
        {
            tbEdit.Select(tbEdit.Text.Length, 0);
        }

        // Обработка события нажатия на кнопку "Подтвердить".
        // Текст, указанный в текстовом поле формы передается в свойство текст текстового поля, переданного через конструктор.
        private void btnApply_Click(object sender, EventArgs e)
        {
            tb.Text = tbEdit.Text;
            this.Close();
        }
    }
}
