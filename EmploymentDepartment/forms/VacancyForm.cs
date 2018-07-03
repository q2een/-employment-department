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
    public partial class VacancyForm : Form
    {
        public VacancyForm()
        {
            InitializeComponent();
        }

        private void linkPreferentialCategory_Validating(object sender, CancelEventArgs e)
        {

        }

        private void linkCompany_Validating(object sender, CancelEventArgs e)
        {
            if (linkCompany.Tag.Equals("Выбрать предприятие..."))
                errorProvider.SetError(linkCompany, "Необходимо выбрать предприятие");
            else
                errorProvider.SetError(linkCompany, "");
        }

        #region Обработка событий для выбора льготной категирии.

        // Обрезает строку для ее корректного отображения на окне.
        private string ShortenString(string tagText = null)
        {
            if (tagText == null)
                tagText = linkCompany.Tag.ToString();
            else
                linkCompany.Tag = tagText;

            string result = string.Copy(linkCompany.Tag.ToString());

            int width = this.Width - lblPreferentialCategory.Width - 70;
            TextRenderer.MeasureText(result, linkCompany.Font, new Size(width, 0), TextFormatFlags.EndEllipsis | TextFormatFlags.ModifyString);

            return result;
        }

        // Нажатие на элемент управление для выбора льготной категории. Обработка события.
        private void linkCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /* var form = new PreferentialCategoryPicker(main.PreferentialCategories, linkPreferentialCategory, linkPreferentialCategory.Tag.ToString());
             form.ShowDialog(this); */
            linkCompany.Text = ShortenString();
        }

        // Нажатие на элемент управления "Очистить". Обработка события.
        private void linkClear_Click(object sender, EventArgs e)
        {
            linkCompany.Text = ShortenString("Выбрать предприятие...");
        }

        // Обработка события нажития клавиши при активном элементе для выбора льготной категории. Обработка события.
        private void linkCompany_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Открыть окно на space.
            if (e.KeyCode == Keys.Space)
            {
                linkCompany_LinkClicked(sender, null);
                return;
            }

            // Очистить на backspace and delete.
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                linkClear_Click(sender, null);
                return;
            }
        }
        #endregion
    }
}
