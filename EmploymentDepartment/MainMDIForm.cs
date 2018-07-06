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
    public partial class MainMDIForm : Form
    {
        public IDataBase DBGetter { get; set; }
        public EntitiesGetter EntGetter { get; set; }

        public List<Faculty> Faculties { get; set; }
        public List<Specialization> Specializations { get; set; }
        public List<PreferentialCategory> PreferentialCategories { get; set; }

        public MainMDIForm()
        {
            InitializeComponent();
            DBGetter = new MySqlDB();
            EntGetter = new EntitiesGetter(DBGetter);

            UpdateFaculties();
            UpdateSpecializations();
            UpdatePreferentialCategories();
        }

        #region Получение базовых списков.

        /// <summary>
        /// Обновляет коллекцию с данными о факультетах из БД.
        /// </summary>
        public void UpdateFaculties()
        {
            this.Faculties = EntGetter.GetFaculties(); 
        }

        /// <summary>
        /// Обновляет коллекцию с данными о профилях подготовки из БД.
        /// </summary>
        public void UpdateSpecializations()
        {
            this.Specializations = EntGetter.GetSpecializations();
        }

        /// <summary>
        /// Обновляет коллекцию с данными о льготных категориях из БД.
        /// </summary>
        public void UpdatePreferentialCategories()
        {
            this.PreferentialCategories = EntGetter.GetPreferentialCategories();
        }

        #endregion

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Пункт меню "Окно".

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        private void MainMDIForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
       
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!(this.ActiveMdiChild is IUpdateble))
                return;

            var active = this.ActiveMdiChild as IUpdateble;

            active.Save();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (!(this.ActiveMdiChild is IInsertable))
                return;

            var active = this.ActiveMdiChild as IInsertable;

            active.Insert();
        }

        #region Show child froms.         

        public void ShowMdiChild<U, T>(U entity, ActionType type) where U: class, IIdentifiable where T : MDIChild<U>
        {
            var form = this.MdiChildren.FirstOrDefault(i => (i is MDIChild<U>) && (i as MDIChild<U>).Type == type && (i as MDIChild<U>).ID == entity.ID);

            if (form == null)
                form = Activator.CreateInstance(typeof(T), new object[] { type, entity }) as T;

            form.MdiParent = this;
            form.Show();
            form.Activate();
        }

        public void ShowViewDialog<U, T>(U entity) where U : class, IIdentifiable where T : MDIChild<U>
        {
            var form = Activator.CreateInstance(typeof(T), new object[] { this, entity }) as T;
            form.ShowDialog(this);
        }

        public void ShowFormByViewType<U,T>(ActionType type, U entity) where U : class, IIdentifiable where T : MDIChild<U>
        {
            if (type == ActionType.View)
            {
                ShowViewDialog<U, T>(entity);
                return;
            }

            ShowMdiChild<U, T>(entity, type);
        }

        public void ShowFormByType<U>(ActionType type, U entity) where U : class, IIdentifiable
        {
            if (entity is IVacancy)
            { 
                ShowFormByViewType<IVacancy, VacancyForm>(type, entity as IVacancy);
            }

            if (entity is IStudent)
            {
                ShowFormByViewType<IStudent, StudentForm>(type, entity as IStudent);
            }  

            if (entity is ICompany)
            {
                ShowFormByViewType<ICompany, CompanyFrom>(type, entity as ICompany);
            }
           
            if (entity is IStudentCompany)
            {
                ShowFormByViewType<IStudentCompany, StudentCompanyForm>(type, entity as IStudentCompany);
            }

            if (entity is ISpecialization)
            {
                var form = new SpecializationForm(this,type, entity as ISpecialization);
                form.ShowDialog();
            }

            if (entity is IFaculty)
            {
                var form = new FacultyForm(this, type, entity as IFaculty);
                form.ShowDialog();
            }

            if (entity is IPreferentialCategory)
            {
                var form = new PreferentialCategoryForm(this, type, entity as IPreferentialCategory);
                form.ShowDialog();
            }
        }

        public void ShowAddForm<T>() where T: class, IIdentifiable, new()
        {
            ShowFormByType(ActionType.Add, new T());
        }

        private DataViewForm<T> GetDataViewForm<T>() where T : class, IIdentifiable
        {
            if (!(typeof(T) is IFaculty) && (typeof(T) is ISpecialization) && (typeof(T) is IPreferentialCategory))
                return null;

            return this.MdiChildren.FirstOrDefault(i => (i is DataViewForm<T>) && (i as DataViewForm<T>).Type == ViewType.Edit) as DataViewForm<T>;
        }

        public void ShowEditDataViewForm<T>(List<T> data) where T : class, IIdentifiable
        {
            var form = GetDataViewForm<T>();
            form = form == null ? new DataViewForm<T>(ViewType.Edit, data) : form;
            form.MdiParent = this;
            form.Show();
            form.Activate();
        }

        #endregion

        #region Пункт меню "Добавить".

        private void addStudentMI_Click(object sender, EventArgs e)
        {
            ShowAddForm<Student>();
        }

        private void addStudentCompanyMI_Click(object sender, EventArgs e)
        {
            ShowAddForm<StudentCompany>();
        }

        private void addCompanyMI_Click(object sender, EventArgs e)
        {
            ShowAddForm<Company>();
        }

        private void addVacancyMI_Click(object sender, EventArgs e)
        {
            ShowAddForm<Vacancy>();
        }

        #endregion

        #region Пункт меню "Служебные - Добавть".

        private void dataFacultiesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm(this.Faculties);
        }

        private void dataSpecializationsMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm(this.Specializations);
        }

        private void dataPreferentialCategoriesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm(this.PreferentialCategories);
        }

        #endregion

        private void MainMDIForm_MdiChildActivate(object sender, EventArgs e)
        {
            SetEntityMITextByActiveChild();
        }

        #region Пункт меню "Сущность".

        private void SetEntityMITextByActiveChild()
        {
            var active = this.ActiveMdiChild;

            string text = null;

            if (active is DataViewForm<Student>)
                text = "Студент";
            if (active is DataViewForm<Company>)
                text = "Предприятие";
            if (active is DataViewForm<Vacancy>)
                text = "Вакансия";
            if (active is DataViewForm<StudentCompany>)
                text = "Место работы";
            if (active is DataViewForm<Faculty>)
                text = "Факультет";
            if (active is DataViewForm<Specialization>)
                text = "Профиль";
            if (active is DataViewForm<PreferentialCategory>)
                text = "Льготная категория";

            entityMI.Text = text;
            entityMI.Visible = text != null;
        }

        private void entityViewMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IDataView)?.View();
        }

        private void entityInserMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IDataView)?.Insert();
        }

        private void entityEditMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IDataView)?.Edit();
        }

        private void entityRemoveMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IDataView)?.Remove();
        }

        #endregion

        private void экспортДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataVacanciesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm(EntGetter.GetVacancies());
        }

        private void dataCompaniesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm(EntGetter.GetCompanies());
        }

        private void dataStudentsMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm(EntGetter.GetStudents());
        }
    }
}
