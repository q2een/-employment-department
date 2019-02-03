using EmploymentDepartment.BL;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет модальное окно для редактирования (добавления, просмотра) информации о профиле подготовки.
    /// </summary>
    public partial class SpecializationForm : BaseSpecializationForm, ISpecialization
    {
        /// <summary>
        /// Предоставляет модальное окно для редактирования (добавления) информации о профиле подготовки.
        /// </summary>
        /// <param name="main">Главное окно программы</param>
        /// <param name="type">Тип действия (редактирование или добавление)</param>
        /// <param name="entity">Экземпляр класса, реализующего интерфейс <c>ISpecialization</c></param>
        /// <param name="viewContext">Экземпляр класса, реализующего интерфейс <c>IDataListView</c></param>
        public SpecializationForm(MainMDIForm main, ActionType type, ISpecialization entity, IDataListView<ISpecialization> viewContext) : base(type, entity, viewContext)
        {
            InitializeComponent();

            this.Main = main;
        }

        /// <summary>
        /// Предоставляет модальное окно для просмотра информации о профиле подготовки.
        /// </summary>
        /// <param name="main">Главное окно программы</param>
        /// <param name="specialization">Экземпляр класса, реализующего интерфейс <c>ISpecialization</c></param>
        public SpecializationForm(MainMDIForm main, ISpecialization specialization) : base(main, specialization)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Устанавливает значения по умолчанию.
        /// </summary>
        public override void SetDefaultValues()
        {
            cmbFaculty.BindComboboxData(Main.Entities.GetEntities<Faculty>().Select(i => i as IFaculty).ToList());
            this.SetPropertiesValue<ISpecialization>(Entity, "FacultyName", "LevelOfEducationName");
        }
         
          
        protected override ErrorProvider GetErrorProvider()
        {
            return errorProvider;
        }

        // Обработка события загрузки формы.
        private void SpecializationForm_Load(object sender, EventArgs e)
        {
            if (Type == ActionType.Add)
                cmbLevelOfEducation.SelectedIndex = 0;

            if (Type == ActionType.Edit) 
                btnApply.Text = "Применить";

            mainPanel.Enabled = btnApply.Visible = Type != ActionType.View;
        }

        // Обработка события нажатия на кнопку "Подтверидть".
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Type == ActionType.Add)
                this.AddNewItem();

            if (Type == ActionType.Edit)
                this.Save();
        }
                  
        #region Validating events.

        // Обработка события проверки корректности для выпадающего списка.
        private new void RequiredComboBox_Validating(object sender, CancelEventArgs e)
        {
            var cmb = sender as ComboBox;
            Extentions.RequiredComboBoxValidating(cmb, errorProvider);
        }

        // Обработка события проверки корректности для текстового поля.
        private new void RequiredTextBox_Validating(object sender, CancelEventArgs e)
        {
            var tb = sender as Control;
            Extentions.RequiredTextBoxValidating(tb, errorProvider);
        }
        #endregion

        #region ISpecialization implementation.

        /// <summary>
        /// Возвращает или задает идентификатор факультета.
        /// </summary>
        public new int Faculty
        {
            get
            {
                return Int32.Parse(cmbFaculty.SelectedValue.ToString());
            }
            set
            {
                cmbFaculty.SelectedValue = value;
            }
        }

        /// <summary>
        /// Возвращает или задает наименование профиля подготовки.
        /// </summary>
        string ISpecialization.Name
        {
            get
            {
                return tbFieldOfStudy.Text.Replace("\n", " ").Trim();
            }
            set
            {
                tbFieldOfStudy.Text = value;
            }
        }

        /// <summary>
        /// Возвращет или задает идентификатор уровня образования.
        /// </summary>
        public new long LevelOfEducation
        {
            get
            {
                return cmbLevelOfEducation.SelectedIndex + 1;
            }
            set
            {
                cmbLevelOfEducation.SelectedIndex = (int)value - 1;
            }
        }

        /// <summary>
        /// Возвращает наименование факультета.
        /// </summary>
        public new string FacultyName
        {
            get
            {
                return cmbFaculty.Text;
            }
        }

        /// <summary>
        /// Возвращает наименование уровня образования.
        /// </summary>
        public new string LevelOfEducationName
        {
            get
            {
                return cmbLevelOfEducation.Text;
            }
        }
        #endregion

    }
}
