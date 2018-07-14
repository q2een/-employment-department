using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    public partial class MainMDIForm : Form
    {
        public IDataBase DataBase { get; set; }
        public IEntityGetter Entities { get; set; }
        
        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Specialization> Specializations { get; set; }
        public IEnumerable<PreferentialCategory> PreferentialCategories { get; set; }

        private readonly UserRole userRole;
        private bool isUnlogin = false;

        public MainMDIForm(IDataBase db, UserRole userRole)
        {
            InitializeComponent();
            this.DataBase = db;
            this.Entities = new MySqlGetter(DataBase);
            this.userRole = userRole;

            if (userRole == UserRole.Moderator)
                statusLblUser.Text = "Редактор";

            UpdateFaculties();
            UpdateSpecializations();
            UpdatePreferentialCategories();
        }

        public MainMDIForm()
        {
            InitializeComponent();
            this.DataBase = new MySqlDB();
            this.Entities = new MySqlGetter(DataBase);
            this.userRole = UserRole.Administrator;

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
            this.Faculties = Entities.GetEntities<Faculty>(); 
        }

        /// <summary>
        /// Обновляет коллекцию с данными о профилях подготовки из БД.
        /// </summary>
        public void UpdateSpecializations()
        {
            this.Specializations = Entities.GetEntities<Specialization>();
        }

        /// <summary>
        /// Обновляет коллекцию с данными о льготных категориях из БД.
        /// </summary>
        public void UpdatePreferentialCategories()
        {
            this.PreferentialCategories = Entities.GetEntities<PreferentialCategory>();
        }

        #endregion
       
        private T GetEntityFromActiveChild<T>() where T : class, IIdentifiable
        {
            T entity = null;

            if (ActiveMdiChild is DataViewForm<T>)
            {
                entity = (ActiveMdiChild as DataViewForm<T>).GetSelectedEntity();
            }

            if (ActiveMdiChild is MDIChild<T>)
            {
                if ((ActiveMdiChild as MDIChild<T>).Type == ActionType.Add)
                    return entity;

                entity = (ActiveMdiChild as MDIChild<T>).Entity;
            }

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
            if (entity is IVacancy)
            { 
                ShowFormByActionType<IVacancy, VacancyForm>(type, entity as IVacancy, viewContext as IDataListView<IVacancy>);
            }

            if (entity is IStudent)
            {
                ShowFormByActionType<IStudent, StudentForm>(type, entity as IStudent, viewContext as IDataListView<IStudent>);
            }  

            if (entity is ICompany)
            {
                ShowFormByActionType<ICompany, CompanyForm>(type, entity as ICompany, viewContext as IDataListView<ICompany>);
            }
           
            if (entity is IStudentCompany)
            {
                ShowFormByActionType<IStudentCompany, StudentCompanyForm>(type, entity as IStudentCompany, viewContext as IDataListView<IStudentCompany>);
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
            new ExportForm(this.DataBase).ShowDialog(this);
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
            // Пункты меню для определенных сущностей
            showStudentCompaniesMI.Visible = GetEntityFromActiveChild<IStudent>() != null;
            showVacanciesByCompanyMI.Visible = GetEntityFromActiveChild<ICompany>() != null;
            showStudentsByCompanyMI.Visible = GetEntityFromActiveChild<ICompany>() != null;
            entityTempItemsSeparator.Visible = GetEntityFromActiveChild<IStudent>() != null || GetEntityFromActiveChild<ICompany>() != null;
        }

        #region Временнные пункты меню. Активны только при необходимых выбранных сущностях.

        // Показать места работы студента. Обработка события нажатия на пункт меню.
        private void tsShowStudentCompanies_Click(object sender, EventArgs e)
        {
            IStudent entity = GetEntityFromActiveChild<IStudent>();

            if (entity == null)
                return;

            ShowEditDataViewForm($"{entity.Surname} {entity.Name} {entity.Patronymic}. Места работы", Entities.GetCompaniesByStudentID(entity.ID));
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

        #region Пункт меню "Добавить".

        // Возвращает эксемпляр сущности, выделенной в активном окне DataViewFrom.
        private T GetActiveIDataListViewSelectedEntity<T>() where T : class, IIdentifiable
        {
            var active = ActiveMdiChild as IDataListView<T>;

            if (active == null || active.ItemsCount == 0)
                return null;

            return active.GetSelectedEntity();
        }

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

            var student = GetActiveIDataListViewSelectedEntity<IStudent>();
            var vacancy = GetActiveIDataListViewSelectedEntity<IVacancy>();

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

            var company = GetActiveIDataListViewSelectedEntity<ICompany>();

            ShowFormAsMDIChild(form);
            form.LinkCompany = company;
        }

        #endregion
        
        #region Пункт меню "Правка".

        // Управление видимостью пункта меню и элементов на панели инструментов.
        private void SetEditMIByActiveChild()
        {
            var active = this.ActiveMdiChild;

            var isDataView = active is IDataView && (active as IDataView).ItemsCount != 0;

            editMI.Visible = isDataView || (active is IEditable);

            // Пункты меню для просмотра списка элементов.

            entityViewMI.Visible = isDataView;
            tsViewItem.Visible = tsViewItemSeparator.Visible = isDataView;

            entityInserMI.Visible = tsAddNewItem.Visible = isDataView;
            entityEditMI.Visible = tsEditItem.Visible = isDataView;
            entityEditSeparatorMI.Visible = isDataView && userRole == UserRole.Administrator;
            entityAddNEditSeparatorMI.Visible = isDataView;

            // Пункты меню для добавления / редактирования элементов.
            var isIEditable = active is IEditable;

            saveMI.Visible = tsSaveChanges.Visible = saveSeparatorMI.Visible = isIEditable && ((active as IEditable).Type == ActionType.Edit || (active as IEditable).Type == ActionType.Add);
            setDefaultValueMI.Visible = setDefaultValueSeparatorMI.Visible = saveSeparatorMI.Visible = isIEditable && ((active as IEditable).Type == ActionType.Edit);
            entityRemoveMI.Visible = tsDeleteItem.Visible = ((isIEditable && ((active as IEditable).Type == ActionType.Edit)) || isDataView) && userRole == UserRole.Administrator;
            tsNavigationSeparator.Visible = tsDeleteItem.Visible;
            setDefaultValueSeparatorMI.Visible = setDefaultValueSeparatorMI.Visible && userRole == UserRole.Administrator;

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

        #region Пункт меню "Окно".

        // ВЫровнять окна каскадом. Обработка события нажатия на пункт меню.
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
            {
                childForm.Close();
            }
        }

        #endregion
        
        #endregion

        private void экспортДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*var student = EntGetter.GetStudent(1);

            var doc = new WordFile(@"E:\it's my\EmploymentDepartment\EmploymentDepartment\bin\Debug\1.docx");
            doc.ReplaceWordText("{surname}", student.Surname);
            doc.ReplaceWordText("{name}", student.Name);
            doc.ReplaceWordText("{patronymic}", student.Patronymic);

            doc.Save(@"E:\it's my\EmploymentDepartment\EmploymentDepartment\bin\Debug\2.docx");*/

        }
         
        // Обработка события загрузки окна.
        private void MainMDIForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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
        }

        // Проверка корректности ввода данных в текстовое поле при навигиции по данным. Разрешен ввод только цифр.
        private void tsPositionItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '.' || !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete);
        }

        // TODO : проверить работу анлогина через флаг.
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

        private void selfEmploymentMI_Click(object sender, EventArgs e)
        {
            try
            {
                var student = GetEntityFromActiveChild<Student>();

                if (student == null)
                    return;

                saveFileDialog.Filter = "Документ MS Word .docx|*.docx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var doc = new WordFile(Directory.GetCurrentDirectory() + @"\templates\selfEmployment.docx");

                    doc.ReplaceWordText("{surname}", student.Surname);
                    doc.ReplaceWordText("{name}", student.Name);
                    doc.ReplaceWordText("{patronymic}", student.Patronymic);
                    doc.ReplaceWordText("{year}", student.YearOfGraduation.ToString());
                    doc.ReplaceWordText("{specialization}", student.Specialization);

                    doc.Save(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportStatementMI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "Документ MS Word .docx|*.docx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    new StatementReportForm(DataBase, saveFileDialog.FileName).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void reportCertificateMI_Click(object sender, EventArgs e)
        {
            try
            {
                var studentCompany = GetEntityFromActiveChild<StudentCompany>();

                if (studentCompany == null)
                    return;

                var student = Entities.GetSingle<Student>(studentCompany.Student);

                if (student == null)
                    return;

                saveFileDialog.Filter = "Документ MS Word .docx|*.docx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var doc = new WordFile(Directory.GetCurrentDirectory() + @"\templates\certificate.docx");

                    doc.ReplaceWordText("{surname}", student.Surname);
                    doc.ReplaceWordText("{name}", student.Name);
                    doc.ReplaceWordText("{patronymic}", student.Patronymic);
                    doc.ReplaceWordText("{specialization}", student.Specialization);
                    doc.ReplaceWordText("{year}", studentCompany.YearOfEmployment.ToString());
                    doc.ReplaceWordText("{salary}", studentCompany.Salary.ToString());
                    doc.ReplaceWordText("{company}", studentCompany.NameOfCompany.ToString());

                    doc.Save(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void reportConfirmationOfArrivalMI_Click(object sender, EventArgs e)
        {
            try
            {
                var studentCompany = GetEntityFromActiveChild<IStudentCompany>();

                if (studentCompany == null)
                    return;

                var student = Entities.GetSingle<Student>(studentCompany.Student);

                if (student == null)
                    return;

                saveFileDialog.Filter = "Документ MS Word .docx|*.docx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var template = student.SelfEmployment ? "confirmationOfArrivalSelf" : "confirmationOfArrival";
                    var doc = new WordFile(Directory.GetCurrentDirectory() + $@"\templates\{template}.docx");

                    doc.ReplaceWordText("{surname}", student.Surname);
                    doc.ReplaceWordText("{name}", student.Name);
                    doc.ReplaceWordText("{patronymic}", student.Patronymic);
                    doc.ReplaceWordText("{specialization}", student.Specialization);
                    doc.ReplaceWordText("{year}", studentCompany.YearOfEmployment.ToString());
                    doc.ReplaceWordText("{salary}", studentCompany.Salary.ToString());
                    doc.ReplaceWordText("{company}", studentCompany.NameOfCompany.ToString());

                    doc.Save(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
