using EmploymentDepartment.BL;
using SharpUpdate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет главное MDI окно программы.
    /// </summary>
    public partial class MainMDIForm : Form, ISharpUpdatable
    {
        /// <summary>
        /// Возвращшает или задает экземпляр класса для работы с БД.
        /// </summary>
        public IDataBase DataBase { get; private set; }

        /// <summary>
        /// Возвращшает или задает экземпляр класса для получения сущностей из БД.
        /// </summary>
        public IEntityGetter Entities { get; private set; }

        #region ISharpUpdatable

        /// <summary>
        /// Наименование приложения.
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return "Отдел трудоустройства выпускников ДонГТУ";
            }
        }

        /// <summary>
        /// ID приложения
        /// </summary>
        public string ApplicationID
        {
            get
            {
                return "EmploymentDepartment";
            }
        }

        /// <summary>
        /// Сборка.
        /// </summary>
        public Assembly ApplicationAssembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        }

        /// <summary>
        /// Значок.
        /// </summary>
        public Icon ApplicationIcon
        {
            get
            {
                return this.Icon;
            }
        }

        /// <summary>
        /// URL для обновления.
        /// </summary>
        public Uri UpdateXmlLocation
        {
            get
            {
                return new Uri("http://194.79.62.216:8082/releases/project.xml"); 
            }
        }

        /// <summary>
        /// Контекст.
        /// </summary>
        public Form Context
        {
            get
            {
                return this;
            }
        }

        #endregion

        private readonly SharpUpdater updater;
        private readonly UserRole userRole;
        private readonly ReportCreator reportCreator;
        private bool isUnlogin = false;
        private bool isSearchShown = false;

        /// <summary>
        /// Предоставляет главное MDI окно программы
        /// </summary>
        /// <param name="db">Экземпляр класса для работы с БД.</param>
        /// <param name="userRole">Права пользователя</param>
        public MainMDIForm(IDataBase db, UserRole userRole)
        {
            InitializeComponent();
            this.DataBase = db;
            this.Entities = db.Entities;
            this.userRole = userRole;
            this.updater = new SharpUpdater(this);
                        
            this.reportCreator = new ReportCreator(new FolderBrowserDialog());
            SetReportCreatorPropertiesBySettings();

            if (userRole == UserRole.Debug)
                statusLblUser.Text = "Отладка";

            if (userRole == UserRole.Moderator)
                statusLblUser.Text = "Редактор";
        }

        /// <summary>
        /// Задает параметры сохранения отчетов по сохраненным настройкам.
        /// </summary>
        public void SetReportCreatorPropertiesBySettings()
        {
            reportCreator.IsRequirePath = Properties.Settings.Default.isRquirePath;
            reportCreator.SaveFolderPath = Properties.Settings.Default.reportPath;
            reportCreator.OpenAfterCreate = Properties.Settings.Default.openAfterCreate;
            reportCreator.CreateSubfolder = Properties.Settings.Default.createSubfolder;
        }

        // Возвращает сущность из активного дочернего MDI окна.
        private T GetEntityFromActiveChild<T>() where T : class, IIdentifiable
        {
            T entity = null;

            if (ActiveMdiChild is DataViewForm<T>)
                entity = (ActiveMdiChild as DataViewForm<T>).GetSelectedEntity();

            if (ActiveMdiChild is MDIChild<T> && (ActiveMdiChild as MDIChild<T>).Type != ActionType.Add)
                entity = (ActiveMdiChild as MDIChild<T>).Entity;

            return entity;
        }
        
        #region Управление видимостью и получение дочерних окон.         

        // Возвращает дочернее MDI окно в зависимости от значений переданных параметров.
        private T GetMDIChild<U, T>(U entity, ActionType type, IDataListView<U> viewContext) where U : class, IIdentifiable where T : MDIChild<U>
        {
            var form = this.MdiChildren.FirstOrDefault(i => (i is MDIChild<U>) && (i as MDIChild<U>).Type == type && (i as MDIChild<U>).ID == entity?.ID) as T;

            if (form == null)
                form = Activator.CreateInstance(typeof(T), new object[] { type, entity, viewContext }) as T;

            return form;
        }

        // Отображает окно, переданное через параметр, как дочернее MDI окно.
        private void ShowFormAsMDIChild(Form form)
        {
            form.MdiParent = this;
            form.Show();
            form.Activate();
        }

        // Отображает дочернее MDI окно для редактирования / добавления записей.
        public void ShowMDIChild<U, T>(U entity, ActionType type, IDataListView<U> viewContext) where U : class, IIdentifiable where T : MDIChild<U>
        {
            var form = GetMDIChild<U, T>(entity, type, viewContext);
            ShowFormAsMDIChild(form);
        }

        // Отображает диалоговое окно для просмотра данных.
        public void ShowViewDialog<U, T>(U entity) where U : class, IIdentifiable where T : MDIChild<U>
        {
            var form = Activator.CreateInstance(typeof(T), new object[] { this, entity }) as T;
            form.ShowDialog(this);
        }

        // Отображает форму в зависимости от типа действия (ActionType).
        public void ShowFormByActionType<U,T>(ActionType type, U entity, IDataListView<U> viewContext) where U : class, IIdentifiable where T : MDIChild<U>
        {
            if (type == ActionType.View)
            {
                ShowViewDialog<U, T>(entity);
                return;
            }

            ShowMDIChild<U, T>(entity, type, viewContext);
        }

        // Отображает форму в зависимости от типа сущности и типа действия (ActionType).
        public void ShowFormByType<U>(ActionType type, U entity, IDataListView<U> viewContext = null) where U : class, IIdentifiable
        {
            var nullEntity = typeof(U);

            if (entity is IVacancy || nullEntity == typeof(IVacancy))
            { 
                ShowFormByActionType<IVacancy, VacancyForm>(type, entity as IVacancy, viewContext as IDataListView<IVacancy>);
                return;
            }

            if (entity is IStudent || nullEntity == typeof(IStudent))
            {
                ShowFormByActionType<IStudent, StudentForm>(type, entity as IStudent, viewContext as IDataListView<IStudent>);
                return;
            }  

            if (entity is ICompany || nullEntity == typeof(ICompany))
            {
                ShowFormByActionType<ICompany, CompanyForm>(type, entity as ICompany, viewContext as IDataListView<ICompany>);
                return;
            }
           
            if (entity is IStudentCompany || nullEntity == typeof(IStudentCompany))
            {
                ShowFormByActionType<IStudentCompany, StudentCompanyForm>(type, entity as IStudentCompany, viewContext as IDataListView<IStudentCompany>);
                return;
            }

            if (entity is ISpecialization || typeof(ISpecialization).IsAssignableFrom(nullEntity))
            {
                var form = new SpecializationForm(this,type, entity as ISpecialization, viewContext as IDataListView<ISpecialization>);
                form.ShowDialog();
                return;
            }   

            if (entity is IFaculty || typeof(IFaculty).IsAssignableFrom(nullEntity))
            {
                var form = new FacultyForm(this, type, entity as IFaculty, viewContext as IDataListView<IFaculty>);
                form.ShowDialog();
                return;
            }

            if (entity is IPreferentialCategory || typeof(IPreferentialCategory).IsAssignableFrom(nullEntity))
            {
                var form = new PreferentialCategoryForm(this, type, entity as IPreferentialCategory, viewContext as IDataListView<IPreferentialCategory>);
                form.ShowDialog();
                return;
            }
        }

        // Отображает форму добавления для сущности.
        public void ShowAddForm<T>() where T: class, IIdentifiable, new()
        {
            ShowFormByType(ActionType.Add, new T(), null);
        }
                                                  
        public DataViewForm<T> GetDataViewForm<T>() where T: class,IIdentifiable
        {
            return MdiChildren.FirstOrDefault(i=> i is DataViewForm<T>) as DataViewForm<T>;
        }

        // Возвращает экземпляр окна, если окно с такими же данными уже открыто.
        private DataViewForm<T> GetDataViewForm<T>(string text, IEnumerable<T> data) where T : class, IIdentifiable
        {
            if (!(typeof(T) is IFaculty) && (typeof(T) is ISpecialization) && (typeof(T) is IPreferentialCategory))
                return null;

            return this.MdiChildren.FirstOrDefault(i => (i is DataViewForm<T>) && (i as DataViewForm<T>).Type == ViewType.Edit 
                                                        && ((i as DataViewForm<T>).Data.SequenceEqual(data) 
                                                        && i.Text == text)) as DataViewForm<T>;
        }

        // Отображает дочернее окно со списком сущностей.
        public void ShowEditDataViewForm<T>(string text, IEnumerable<T> data) where T : class, IIdentifiable
        {
            var form = GetDataViewForm<T>(text, data);
            form = form == null ? new DataViewForm<T>(text,ViewType.Edit, data) : form;
            ShowFormAsMDIChild(form);
        }

        #endregion

        #region Управление видимостью навигации.

        // Задает видимость элементов навигации в зависимости от флага isVisible. 
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

        // Задает видимость элементов навигации и пунктов меню навигации в зависимости от активного дочернего окна.
        private void SetNavigator()
        {
            var active = this.ActiveMdiChild;

            SetNavigationTSItemsVisible(active is IDataView && (active as IDataView).ItemsCount != 0);

            if (!(active is IDataView) && !(active is IDataSourceView))
                return;

            toolStrip.BindingSource = (active as IDataSourceView)?.DataSource;
        }

        #endregion

        #region Пункты меню и элементы панели инструментов.

        #region Пункт меню "Файл".

        // Управление видимостью пункта меню "Файл - Экспорт".
        private void ShowExportMI()
        {
            var active = ActiveMdiChild as IDataView;

            if (active == null)
            {
                exportMI.Visible = false;
                return;
            }

            exportMI.Visible = active.ItemsCount != 0;
        }

        // Экспорт.
        private void exportMI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "Лист .xls|*.xls";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var active = ActiveMdiChild as IDataSourceView;

                    if (active == null)
                        return;

                    active.Export(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Экспорт данных.
        private void exortDataMI_Click(object sender, EventArgs e)
        {
            new ExportForm(this.DataBase.Export).ShowDialog(this);
        }

        // Выход.
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Пункт меню "Данные"

        // Управляет видимостью пункта меню "Данные" в зависимости от активного дочернего окна.
        private void SetDataMIByActiveChild()
        {
            var istudent = GetEntityFromActiveChild<IStudent>();
            var icompany = GetEntityFromActiveChild<ICompany>();

            // Пункты меню для определенных сущностей
            showStudentCompaniesMI.Visible = istudent != null;
            showVacanciesByCompanyMI.Visible = icompany != null;
            showStudentsByCompanyMI.Visible = icompany != null;
            entityTempItemsSeparator.Visible = istudent != null || icompany != null;
        }

        #region Временнные пункты меню. Активны только при необходимых выбранных сущностях.

        // Показать места работы студента. Обработка события нажатия на пункт меню.
        private void tsShowStudentCompanies_Click(object sender, EventArgs e)
        {
            IStudent entity = GetEntityFromActiveChild<IStudent>();

            if (entity == null)
                return;

            ShowEditDataViewForm($"{entity.Surname} {entity.Name} {entity.Patronymic.Trim()}. Места работы", Entities.GetCompaniesByStudentID(entity.ID));
        }

        // Показать вакансии на выбранном предприятии. Обработка события нажатия на пункт меню.
        private void tsShowVacanciesByCompany_Click(object sender, EventArgs e)
        {
            ICompany entity = GetEntityFromActiveChild<ICompany>();

            if (entity == null)
                return;

            ShowEditDataViewForm($"Вакансии на предприятии: «{entity.Name}»", Entities.GetVacanciesByCompanyID(entity.ID));
        }

        // Показать студентов на предприятии. Обработка события нажатия на пункт меню.
        private void tsShowStudentsByCompany_Click(object sender, EventArgs e)
        {
            ICompany entity = GetEntityFromActiveChild<ICompany>();

            if (entity == null)
                return;

            ShowEditDataViewForm($"Cтуденты на предприятии: «{entity.Name}»", Entities.GetSudentsByCompanyID(entity.ID));
        }

        #endregion

        // Показать список вакансий. Обработка события нажатия на пункт меню.
        private void dataVacanciesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm<IVacancy>("Вакансии", Entities.GetEntities<Vacancy>());
        }

        // Показать список предприятий. Обработка события нажатия на пункт меню.
        private void dataCompaniesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm<ICompany>("Предприятия", Entities.GetEntities<Company>());
        }

        // Показать список работы студентов. Обработка события нажатия на пункт меню.
        private void dataStudentCompaniesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm<IStudentCompany>("Места работы студентов", Entities.GetEntities<StudentCompany>());
        }

        // Показать список студентов. Обработка события нажатия на пункт меню.
        private void dataStudentsMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm<IStudent>("Студенты", Entities.GetEntities<Student>());
        }

        #region Пункт меню "Данные - Служебные".

        private void dataFacultiesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm<IFaculty>("Факультеты", Entities.GetEntities<Faculty>());
        }

        private void dataSpecializationsMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm<ISpecialization>("Профили подготовки", Entities.GetEntities<Specialization>());
        }

        private void dataPreferentialCategoriesMI_Click(object sender, EventArgs e)
        {
            ShowEditDataViewForm<IPreferentialCategory>("Льготные категории", Entities.GetEntities<PreferentialCategory>());
        }

        #endregion

        #endregion

        #region Пункт меню "Добавить".

        // Добавить студента. Обработка события нажатия на пункт меню. 
        private void addStudentMI_Click(object sender, EventArgs e)
        {
            ShowAddForm<Student>();
        }

        // Добавить место работы студента. Обработка события нажатия на пункт меню.
        private void addStudentCompanyMI_Click(object sender, EventArgs e)
        {
            if (!(ActiveMdiChild is IDataView) || (ActiveMdiChild as IDataView).ItemsCount == 0)
            {
                ShowAddForm<StudentCompany>();
                return;
            }

            var form = GetMDIChild<IStudentCompany, StudentCompanyForm>(null, ActionType.Add, null);

            var student = GetEntityFromActiveChild<IStudent>();
            var vacancy = GetEntityFromActiveChild<IVacancy>();

            ShowFormAsMDIChild(form);
            form.LinkStudent = student;
            form.LinkVacancy = vacancy;
        }

        // Добавить предприятие. Обработка события нажатия на пункт меню.
        private void addCompanyMI_Click(object sender, EventArgs e)
        {
            ShowAddForm<Company>();
        }

        // Добавить вакансию. Обработка события нажатия на пункт меню.
        private void addVacancyMI_Click(object sender, EventArgs e)
        {
            var form = GetMDIChild<IVacancy, VacancyForm>(null, ActionType.Add, null);

            var company = GetEntityFromActiveChild<ICompany>();

            ShowFormAsMDIChild(form);
            form.LinkCompany = company;
        }

        #endregion
        
        #region Пункт меню "Правка".

        // Управление видимостью пункта меню и элементов на панели инструментов.
        private void SetEditMIByActiveChild()
        {
            var active = this.ActiveMdiChild;

            var isDataView = active is IDataView;

            editMI.Visible = isDataView || (active is IEditable);

            // Пункты меню для просмотра списка элементов.

            entityViewMI.Visible = isDataView && (active as IDataView).ItemsCount != 0;
            tsViewItem.Visible = tsViewItemSeparator.Visible = isDataView && (active as IDataView).ItemsCount != 0;

            entityInserMI.Visible = tsAddNewItem.Visible = isDataView;
            entityEditMI.Visible = tsEditItem.Visible = isDataView && (active as IDataView).ItemsCount != 0;
            entityEditSeparatorMI.Visible = (isDataView && (active as IDataView).ItemsCount != 0) && (userRole == UserRole.Administrator || userRole == UserRole.Debug);
            entityAddNEditSeparatorMI.Visible = isDataView && (active as IDataView).ItemsCount != 0;

            // Пункты меню для добавления / редактирования элементов.
            var isIEditable = active is IEditable;

            saveMI.Visible = tsSaveChanges.Visible = saveSeparatorMI.Visible = isIEditable && ((active as IEditable).Type == ActionType.Edit || (active as IEditable).Type == ActionType.Add);
            setDefaultValueMI.Visible = setDefaultValueSeparatorMI.Visible = saveSeparatorMI.Visible = isIEditable && ((active as IEditable).Type == ActionType.Edit);
            entityRemoveMI.Visible = tsDeleteItem.Visible = entityRemoveMI.Enabled = ((isIEditable && ((active as IEditable).Type == ActionType.Edit)) || (isDataView && (active as IDataView).ItemsCount != 0)) && (userRole == UserRole.Administrator || userRole == UserRole.Debug);
            tsNavigationSeparator.Visible = tsDeleteItem.Visible;
            setDefaultValueSeparatorMI.Visible = setDefaultValueSeparatorMI.Visible && (userRole == UserRole.Administrator || userRole == UserRole.Debug);

        }

        // Нажатие на пункт меню "Правка - Просмотреть". Обработка события.
        private void entityViewMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IDataView)?.View();
        }

        // Нажатие на пункт меню "Правка - Добавить". Обработка события.
        private void entityInserMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IDataView)?.Insert();
        }

        // Нажатие на пункт меню "Правка - Редактировать". Обработка события.
        private void entityEditMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IDataView)?.Edit();
        }

        // Нажатие на пункт меню "Правка - Удалить". Обработка события.
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

        // Нажатие на пункт меню "Правка - Установить значение по умолчанию". Обработка события.
        private void setDefaultValueMI_Click(object sender, EventArgs e)
        {
            (ActiveMdiChild as IEditable)?.SetDefaultValues();
        }

        // Нажатие на пункт меню "Правка - Сохранить". Обработка события.
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

        #endregion

        #region Пункт меню "Поиск".

        // Устанавливает видимость пункта меню и панели поиска.
        private void SetSearch()
        {
            var isStudents = this.ActiveMdiChild is DataViewForm<IStudent>;
            var isCompanies = this.ActiveMdiChild is DataViewForm<ICompany>;

            tsSearchPanel.Visible = (isStudents || isCompanies) && isSearchShown;
            searchMI.Visible = isStudents || isCompanies;

            if (!isStudents && !isCompanies)
                return;

            cmbSearchFilter.Visible = isStudents;
            lblSearchDescription.Visible = !isStudents;
        }

        // Возвращает фильтр поиска в зависимости от выбранных параметров.
        // Предназначено для создания строки фильтрации студентов.
        private string BuildStudentSearchString()
        {
            if (cmbSearchFilter.SelectedIndex == 0)
            {
                var name = tbSearch.Text.Trim().Split(' ');

                if (name.Count() == 0)
                    return "";

                if (name.Count() == 1)
                    return $"([Фамилия] LIKE '%{name[0]}%') OR ([Имя] LIKE '%{name[0]}%')";

                if (name.Count() == 2)
                    return $"([Фамилия] LIKE '%{name[0]}%') AND ([Имя] LIKE '%{name[1]}%')";

                cmbSearchFilter.SelectedIndex = 1;
            }

            var filter = cmbSearchFilter.SelectedIndex == 1 ? "[Фамилия]" : "[Имя]";

            return $"({filter} LIKE '%{tbSearch.Text.Trim()}%')";
        }

        // Поиск. Обработка события нажатия на кнопку.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            IDataSourceView students = this.ActiveMdiChild as DataViewForm<IStudent>;
            IDataSourceView companies = this.ActiveMdiChild as DataViewForm<ICompany>;

            IDataSourceView form = students ?? companies;

            if (form == null)
                return;

            var filter = students == null ? $"([Название] LIKE '%{tbSearch.Text}%')" : BuildStudentSearchString();

            form.SetDataSourceFilter(filter);
        }

        // Отменить поиск. Обработка события нажатия на кнопку.
        private void tbSearchCancel_Click(object sender, EventArgs e)
        {
            IDataSourceView students = this.ActiveMdiChild as DataViewForm<IStudent>;
            IDataSourceView companies = this.ActiveMdiChild as DataViewForm<ICompany>;

            IDataSourceView form = students ?? companies;

            if (form == null)
                return;

            form.SetDataSourceFilter(null);
        }    

        // Поиск. Обработка события нажатия на пункт меню.
        private void searchMI_Click(object sender, EventArgs e)
        {
            if (!this.isSearchShown)
            {
                tsSearchPanel.Visible = true;
                this.isSearchShown = true;
            }

            tbSearch.Focus();
            tbSearch.SelectAll();
        }

        // Закрыть панель поиска. Обработка события нажатия на кнопку.
        private void btnCloseSearchPanel_Click(object sender, EventArgs e)
        {
            this.isSearchShown = false;
            tsSearchPanel.Visible = false;
        }

        // Поиск по нажатию на клавишу ENTER. Обработка события нажатия на клавишу.
        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                return;

            btnSearch_Click(sender, e);
        }

        #endregion

        #region Пункт меню "Отчет". 
        // Управление видимостью пункта меню.
        private void SetReportMIVisible()
        {
            var student = GetEntityFromActiveChild<IStudent>();
            var studentCompany = GetEntityFromActiveChild<IStudentCompany>();

            var isVisible = ((IIdentifiable)student ?? studentCompany) != null;

            selfEmploymentMI.Visible = reportConfirmationOfArrivalSelfMI.Visible = isVisible;
            reportConfirmationOfArrivalMI.Visible = reportCertificateMI.Visible = reportNotificationMI.Visible  = isVisible;
            reportSeparatorMI.Visible = selfEmploymentSeparator.Visible = isVisible;
        }

        // Сохранение отчета в зависимости от выбранного пункта меню.
        private void SaveReport(ReportCreator.SaveReport saveMethod)
        {
            try
            {
                IStudent student;

                var studentCompany = GetEntityFromActiveChild<IStudentCompany>();

                if (studentCompany != null)
                    student = Entities.GetSingle<Student>(studentCompany.Student);
                else
                    student = GetEntityFromActiveChild<IStudent>();

                if (student == null)
                    return;

                saveMethod(student, Entities.GetSingle<Specialization>(student.FieldOfStudy), studentCompany);
            }
            catch (FileNotFoundException)
            {
                // Не выбрана папка в открывшемся диалоговом окне - отмена действия.
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Справка о самостоятельном трудоустройстве. Обработка события нажатия на пункт меню.
        private void selfEmploymentMI_Click(object sender, EventArgs e)
        {
            SaveReport(reportCreator.SaveSelfEmployment);
        }

        // Подтверждение прибытия к справке о самостоятельном трудоустройстве. Обработка события нажатия на пункт меню.
        private void reportConfirmationOfArrivalSelfMI_Click(object sender, EventArgs e)
        {
            SaveReport(reportCreator.SaveSelfEmploymentConfirmationOfArrival);
        }

        // Свидетельство о направлении на работу. Обработка события нажатия на пункт меню.
        private void reportCertificateMI_Click(object sender, EventArgs e)
        {
            SaveReport(reportCreator.SaveCertificate);
        }

        // Уведомление к свидетельству о направлении. Обработка события нажатия на пункт меню.
        private void reportNotificationMI_Click(object sender, EventArgs e)
        {
            SaveReport(reportCreator.SaveNotification);
        }

        // Подтверждение прибытия к свидетельству о направлении. Обработка события нажатия на пункт меню.
        private void reportConfirmationOfArrivalMI_Click(object sender, EventArgs e)
        {
            SaveReport(reportCreator.SaveConfirmationOfArrival);
        }

        // Ведомость распределения выпускников. Обработка события нажатия на пункт меню.
        private void reportStatementMI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "Документ MS Word .docx|*.docx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    new StatementReportForm(this.DataBase.Export, saveFileDialog.FileName).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Ведомость персонального учета выпускников. Обработка события нажатия на пункт меню.
        private void personalAccountOfGraduatesMI_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Документ MS Word .docx|*.docx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                new PersonalAccountReportForm(this, saveFileDialog.FileName).ShowDialog();
            }
        }

        // Параметры. Обработка события нажатия на пункт меню.
        private void reportSettingsMI_Click(object sender, EventArgs e)
        {
            new ReportSettingsForm(this).ShowDialog();
        }

        #endregion

        #region Пункт меню "Окно".

        // Выровнять окна каскадом. Обработка события нажатия на пункт меню.
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        // Выровнять окна слева направо. Обработка события нажатия на пункт меню.
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        // Выровнять окна слева вниз. Обработка события нажатия на пункт меню.
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        // Закрыть дочерние окна. Обработка события нажатия на пункт меню.
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
                childForm.Close();
        }

        #endregion

        #region Пункт меню "Справка".
        
        private void aboutMI_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }
        
        private void helpMI_Click(object sender, EventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"help.chm");
            }
            catch (Exception)
            {
                MessageBox.Show("Файл справки не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateMI_Click(object sender, EventArgs e)
        {
            updater.DoUpdate();
        }

        #endregion

        #endregion

        // Обработка события загрузки окна.
        private void MainMDIForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Фильтр в выпадающем списке для поиска.
            cmbSearchFilter.SelectedIndex = 0;      
        }

        // Обработка события смены активного дочернего окна.
        private void MainMDIForm_MdiChildActivate(object sender, EventArgs e)
        {
            // Видимсоть навигации.
            SetNavigator();

            // Видимость элементов меню "Правка".
            SetEditMIByActiveChild();

            // Видимость пункта меню "Файл-Экспорт".
            ShowExportMI();

            // Видимость элемента меню "Данные".
            SetDataMIByActiveChild();

            // Видимость элемента меню "Поиск".
            SetSearch();

            // Видимость пунктов меню "Отчет".
            SetReportMIVisible();
        }

        // Проверка корректности ввода данных в текстовое поле при навигиции по данным. Разрешен ввод только цифр.
        private void tsPositionItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '.' || !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete);
        }

        // Обработка события, происходящего после закрытия формы.
        private void MainMDIForm_FormClosed(object sender, FormClosedEventArgs e)
        {   
            if(isUnlogin)
                (this.Owner as Form)?.Show();

            if (e.CloseReason != CloseReason.FormOwnerClosing)
                if(this.Owner is Form && !(this.Owner as Form).Visible)
                    (this.Owner as Form)?.Close();
        }

        // Нажатие на роль пользователя в строке состояния. Обработка события.
        private void statusLblUser_Click(object sender, EventArgs e)
        {
            // Сброс сохраненных параметров подключения.
            Properties.Settings.Default.connection = null;
            Properties.Settings.Default.Save();

            // Закрываем текущее окно.
            isUnlogin = true;
            this.Close();
        }
    }
}
