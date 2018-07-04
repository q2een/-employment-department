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
    public partial class VacancyForm : Form, IVacancy, IEditable<IVacancy>
    {
        public IVacancy Entity { get; private set; }
        public ActionType Type { get; private set; }
        private MainMDIForm main;

        public ICompany LinkStudent
        {
            get
            {
                return linkCompany.Tag as ICompany;
            }
            private set
            {
                linkCompany.Tag = value;
                string text = value == null ? "Выбрать студента ..." : $"{value.Surname} {value.Name} {value.Patronymic}";
                linkCompany.Text = Extentions.ShortenString(text, studentPanel.Width - linkClear.Width - 90, linkCompany.Font);

                linkClear.Visible = Type != ActionType.View;
            }
        }

        #region IVacancy implementation
        int id;
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string VacancyNumber
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Post
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Employer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string WorkArea
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal Salary
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool? IsActive
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SalaryNote
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Gender
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Features
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public VacancyForm(ActionType type, IVacancy vacancy = null)
        {
            if (type == ActionType.Edit && vacancy == null)
                throw new ArgumentNullException();

            InitializeComponent();

            this.Entity = type == ActionType.Add ? null : vacancy;
            this.Type = type;
        }

        // Модальное окно для просмотра информации.
        public VacancyForm(MainMDIForm mainForm, IVacancy vacancy) : this(ActionType.View, vacancy)
        {
            this.main = mainForm;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        // Обработка события загрузки формы.
        private void VacancyForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;

            SetDefaultValues();
            SetFormText();
            mainPanel.Enabled = Type != ActionType.View;
        }

        // Устанавливает заголовок окна.
        private void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о вакансии";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление вакансииа";
                    break;
                case ActionType.View:
                    this.Text = $"Просмотр вакансии";
                    break;
            }
        }

        private void VacancyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (Type != ActionType.View)
                return;

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void linkCompany_Validating(object sender, CancelEventArgs e)
        {
            if (linkCompany.Tag.Equals("Выбрать предприятие..."))
                errorProvider.SetError(linkCompany, "Необходимо выбрать предприятие");
            else
                errorProvider.SetError(linkCompany, "");
        }

        #region IEditable interfaces implemantation.

        public bool ValidateFields() => Extentions.ValidateFields(this, errorProvider);

        public void SetDefaultValues() => this.SetPropertiesValue<IVacancy>(Entity, null);

        public void Save()
        {
            var msg = $"Информация о вакансии обновлена";
            if (this.UpdateFormEntityInDataBase<VacancyForm, IVacancy>(main.DBGetter, msg, "ID", "Name"))
                SetFormText();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Insert()
        {
            var msg = $"Информация о вакансии добавлена в базу";
            this.InsertFormEntityToDataBase<VacancyForm, IVacancy>(main.DBGetter, msg, "ID", "Name");
        }
        #endregion

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
