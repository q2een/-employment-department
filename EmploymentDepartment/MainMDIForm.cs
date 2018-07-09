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


        #region Show child froms.         

        public void ShowMdiChild<U, T>(U entity, ActionType type, IDataListView<U> viewContext) where U: class, IIdentifiable where T : MDIChild<U>
        {
            var form = this.MdiChildren.FirstOrDefault(i => (i is MDIChild<U>) && (i as MDIChild<U>).Type == type && (i as MDIChild<U>).ID == entity.ID);

            if (form == null)
                form = Activator.CreateInstance(typeof(T), new object[] { type, entity, viewContext }) as T;

            form.MdiParent = this;
            form.Show();
            form.Activate();
        }

        public void ShowViewDialog<U, T>(U entity) where U : class, IIdentifiable where T : MDIChild<U>
        {
            var form = Activator.CreateInstance(typeof(T), new object[] { this, entity }) as T;
            form.ShowDialog(this);
        }

        public void ShowFormByViewType<U,T>(ActionType type, U entity, IDataListView<U> viewContext) where U : class, IIdentifiable where T : MDIChild<U>
        {
            if (type == ActionType.View)
            {
                ShowViewDialog<U, T>(entity);
                return;
            }

            ShowMdiChild<U, T>(entity, type, viewContext);
        }

        public void ShowFormByType<U>(ActionType type, U entity, IDataListView<U> viewContext = null) where U : class, IIdentifiable
        {
            if (entity is IVacancy)
            { 
                ShowFormByViewType<IVacancy, VacancyForm>(type, entity as IVacancy, viewContext as IDataListView<IVacancy>);
            }

            if (entity is IStudent)
            {
                ShowFormByViewType<IStudent, StudentForm>(type, entity as IStudent, viewContext as IDataListView<IStudent>);
            }  

            if (entity is ICompany)
            {
                ShowFormByViewType<ICompany, CompanyForm>(type, entity as ICompany, viewContext as IDataListView<ICompany>);
            }
           
            if (entity is IStudentCompany)
            {
                ShowFormByViewType<IStudentCompany, StudentCompanyForm>(type, entity as IStudentCompany, viewContext as IDataListView<IStudentCompany>);
            }

            if (entity is ISpecialization)
            {
                var form = new SpecializationForm(this,type, entity as ISpecialization, viewContext as IDataListView<ISpecialization>);
                form.ShowDialog();
            }   

            if (entity is IFaculty)
            {
                var form = new FacultyForm(this, type, entity as IFaculty, viewContext as IDataListView<IFaculty>);
                form.ShowDialog();
            }

            if (entity is IPreferentialCategory)
            {
                var form = new PreferentialCategoryForm(this, type, entity as IPreferentialCategory, viewContext as IDataListView<IPreferentialCategory>);
                form.ShowDialog();
            }
        }

        public void ShowAddForm<T>() where T: class, IIdentifiable, new()
        {
            ShowFormByType(ActionType.Add, new T(), null);
        }

        private DataViewForm<T> GetDataViewForm<T>() where T : class, IIdentifiable
        {
            if (!(typeof(T) is IFaculty) && (typeof(T) is ISpecialization) && (typeof(T) is IPreferentialCategory))
                return null;

            return this.MdiChildren.FirstOrDefault(i => (i is DataViewForm<T>) && (i as DataViewForm<T>).Type == ViewType.Edit) as DataViewForm<T>;
        }

        public void ShowEditDataViewForm<T>(string text, List<T> data) where T : class, IIdentifiable
        {
            var form = GetDataViewForm<T>();
            form = form == null ? new DataViewForm<T>(text,ViewType.Edit, data) : form;
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

        private void MainMDIForm_MdiChildActivate(object sender, EventArgs e)
        {
            
            SetNavigator();
            SetEditMIByActiveChild();

        }

        private void SetNavigationTSItemsVisible(bool isVisible)
        {
            tsMoveFirstItem.Visible = isVisible;
            tsMovePreviousItem.Visible = isVisible;
            tsSeparator.Visible = isVisible;
            tsPositionItem.Visible = isVisible;
            tsCountItem.Visible = isVisible;
            tsSeparator1.Visible = isVisible;
            tsMoveNextItem.Visible = isVisible;
            tsMoveLastItem.Visible = isVisible; 
            tsViewItem.Visible = isVisible;
            tsViewItemSeparator.Visible = isVisible;
            tsAddNewItem.Visible = isVisible;
            tsEditItem.Visible = isVisible;
        }

        private void SetNavigator()
        {
            var active = this.ActiveMdiChild;

            SetNavigationTSItemsVisible(active is IDataSourceView);

            if (!(active is IDataView) && !(active is IDataSourceView))
                return;

            toolStrip.BindingSource = (active as IDataSourceView)?.DataSource;
        }

        #region Пункт меню "Правка".
        // Управление видимостью пункта меню и элементов на панели инструментов.
        private void SetEditMIByActiveChild()
        {
            var active = this.ActiveMdiChild;
            
            editMI.Visible = (active is IDataView) || (active is IDataSourceView) || (active is IEditable);

            // Пункты меню для просмотра списка элементов.
            var isDataView = active is IDataSourceView;
                        
            entityViewMI.Visible = isDataView;
            tsViewItem.Visible = tsViewItemSeparator.Visible = isDataView;

            entityInserMI.Visible = tsAddNewItem.Visible = isDataView;
            entityEditMI.Visible = tsEditItem.Visible = isDataView;
            entityEditSeparatorMI.Visible = isDataView; 
            entityAddNEditSeparatorMI.Visible = isDataView;

            // Пункты меню для добавления / редактирования элементов.
            var isIEditable = active is IEditable;

            saveMI.Visible = tsSaveChanges.Visible = saveSeparatorMI.Visible = isIEditable && ((active as IEditable).Type == ActionType.Edit || (active as IEditable).Type == ActionType.Add);
            setDefaultValueMI.Visible = tsSetDefaultValues.Visible = setDefaultValueSeparatorMI.Visible = saveSeparatorMI.Visible = isIEditable && ((active as IEditable).Type == ActionType.Edit);
            entityRemoveMI.Visible = tsDeleteItem.Visible = (isIEditable && ((active as IEditable).Type == ActionType.Edit)) || isDataView;
            tsNavigationSeparator.Visible = (isIEditable && ((active as IEditable).Type == ActionType.Edit)) || isDataView;
            
            // Пункты меню для определенных сущностей
            ShowStudentCompaniesItemsVisible();
            ShowVacanciesByCompanyItemsVisible();
            ShowStudentsByCompanyItemsVisible();
            entityTempItemsSeparator.Visible = IsActiveEditOrViewStudentForm() || IsActiveEditOrViewCompanyForm();

            
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
            if (ActiveMdiChild is IDataView)
            {
                (ActiveMdiChild as IDataView)?.Remove();
                return;
            }

            var active = ActiveMdiChild as IEditable;

            if (active == null || active.Type == ActionType.Add || active.Type == ActionType.View)
                return;

            active.Remove();
        }

        private void setDefaultValueMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IEditable)?.SetDefaultValues();
        }

        private void saveMI_Click(object sender, EventArgs e)
        {
            var active = ActiveMdiChild as IEditable;

            if (active == null || active.Type == ActionType.View)
                return;

            if (active.Type == ActionType.Add)
            {
                active.AddNewItem();
                return;
            }

            if (active.Type == ActionType.Edit)
                active.Save();
        }

        #region Временнные пункты меню. Активны только при необходимых выбранных сущностях.

        private void ShowStudentCompaniesItemsVisible()
        {  
            tsShowStudentCompanies.Visible = IsActiveEditOrViewStudentForm();
            showStudentCompaniesMI.Visible = IsActiveEditOrViewStudentForm();
        }

        private bool IsActiveEditOrViewStudentForm()
        {
            bool visible = false;

            visible = ActiveMdiChild is DataViewForm<Student>;

            if (ActiveMdiChild is StudentForm)
            {
                visible = true;

                if ((ActiveMdiChild as StudentForm)?.Type == ActionType.Add)
                    visible = false;
            }

            return visible;
        }

        private bool IsActiveEditOrViewCompanyForm()
        {
            bool visible = false;

            visible = ActiveMdiChild is DataViewForm<Company>;

            if (ActiveMdiChild is CompanyForm)
            {
                visible = true;

                if ((ActiveMdiChild as CompanyForm)?.Type == ActionType.Add)
                    visible = false;
            }

            return visible;
        }

        private void ShowVacanciesByCompanyItemsVisible()
        {
            tsShowVacanciesByCompany.Visible = IsActiveEditOrViewCompanyForm();
            showVacanciesByCompanyMI.Visible = IsActiveEditOrViewCompanyForm();
        }

        private void ShowStudentsByCompanyItemsVisible()
        {


            tsShowStudentsByCompany.Visible = IsActiveEditOrViewCompanyForm();
            showStudentsByCompanyMI.Visible = IsActiveEditOrViewCompanyForm();
        }

        // Показать места работы студента.
        private void tsShowStudentCompanies_Click(object sender, EventArgs e)
        {
            IStudent entity = null;

            if (ActiveMdiChild is DataViewForm<Student>)
            {
                entity = (ActiveMdiChild as DataViewForm<Student>).GetSelectedEntity();
            }

            if (ActiveMdiChild is StudentForm)
            {
                if ((ActiveMdiChild as StudentForm).Type == ActionType.Add)
                    return;

                entity = (ActiveMdiChild as StudentForm).Entity;
            }

            if (entity == null)
                return;

            ShowEditDataViewForm($"{entity.Surname} {entity.Name} {entity.Patronymic}. Места работы", EntGetter.GetCompaniesByStudentID(entity.ID));
        }

        // Показать вакансии на выбранном предприятии.
        private void tsShowVacanciesByCompany_Click(object sender, EventArgs e)
        {
            ICompany entity = null;

            if (ActiveMdiChild is DataViewForm<Company>)
            {
                entity = (ActiveMdiChild as DataViewForm<Company>).GetSelectedEntity();
            }

            if (ActiveMdiChild is CompanyForm)
            {
                if ((ActiveMdiChild as CompanyForm).Type == ActionType.Add)
                    return;

                entity = (ActiveMdiChild as CompanyForm).Entity;
            }

            if (entity == null)
                return;

            ShowEditDataViewForm($"Вакансии на предприятии: «{entity.Name}»", EntGetter.GetVacanciesByCompanyID(entity.ID));
        }

        // Показать студентов на предприятии.
        private void tsShowStudentsByCompany_Click(object sender, EventArgs e)
        {
            ICompany entity = null;

            if (ActiveMdiChild is DataViewForm<Company>)
            {
                entity = (ActiveMdiChild as DataViewForm<Company>).GetSelectedEntity();
            }

            if (ActiveMdiChild is CompanyForm)
            {
                if ((ActiveMdiChild as CompanyForm).Type == ActionType.Add)
                    return;

                entity = (ActiveMdiChild as CompanyForm).Entity;
            }

            if (entity == null)
                return;

            ShowEditDataViewForm($"Cтуденты на предприятии: «{entity.Name}»", EntGetter.GetSudentsByCompanyID(entity.ID));

        }

        #endregion

        #endregion

        private void экспортДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #region Пункт меню "Данные"
        private void dataVacanciesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm("Вакансии", EntGetter.GetVacancies());
        }

        private void dataCompaniesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm("Предприятия", EntGetter.GetCompanies());
        }

        private void dataStudentsMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm("Студенты", EntGetter.GetStudents());
        }

        #region Пункт меню "Данные - Служебные".

        private void dataFacultiesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm("Факультеты", this.Faculties);
        }

        private void dataSpecializationsMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm("Профили подготовки", this.Specializations);
        }

        private void dataPreferentialCategoriesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm("Льготные категории", this.PreferentialCategories);
        }

        #endregion

        #endregion

        private void tsPositionItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '.' || !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete);
        }

    }
}
