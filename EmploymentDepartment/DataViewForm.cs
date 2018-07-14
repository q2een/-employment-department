using EmploymentDepartment.BL;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет окно для просмотра данны в табличном виде с возможностью сортировки и фильтрации данных.
    /// </summary>
    public partial class DataViewForm<T>: Form, IDataListView<T> where T:class,IIdentifiable
    {
        /// <summary>
        /// Возвращает коллекцию отображаемых данных.
        /// </summary>
        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// Возвращает количество элементов в коллекции для отображения.
        /// </summary>
        public int ItemsCount
        {
            get
            {
                return Data.Count();
            }
        }

        /// <summary>
        /// Возвращает тип отображаения экземпляра формы.
        /// </summary>
        public ViewType Type { get; private set; }

        /// <summary>
        /// Возвращает источник данных для формы.
        /// </summary>
        public BindingSource DataSource
        {
            get
            {
                return bindingSource;
            }
        }

        private MainMDIForm main;
        private DataTable dataTable = null;
        private DataSet dataSet = null;
        private readonly ILinkPickable selectParent;
        private int columnsSize;

        /// <summary>
        /// Предоставляет окно для просмотра данных в табличном виде с возможностью сортировки и фильтрации данных.
        /// </summary>
        /// <param name="text">Текст, отображаемый в заголовке окна</param>
        /// <param name="type">Тип просмотра данного окна</param>
        /// <param name="data">Коллекция данных для отображения</param>
        public DataViewForm(string text, ViewType type, IEnumerable<T> data)
        {
            if (data == null)
                throw new ArgumentNullException();

            InitializeComponent();
            this.Data = data;
            this.Type = type;
            this.Text = text;

            dataTable = new DataTable();
            dataSet = new DataSet();

            bindingSource.DataSource = this.dataSet;

            mainDgv.DataSource = bindingSource;
            mainDgv.DoubleBuffered(true);

            Init();

        }

        /// <summary>
        /// Предоставляет окно для просмотра данных в табличном виде с возможностью сортировки, фильтрации и выбора данных.
        /// </summary>
        /// <param name="text">Текст, отображаемый в заголовке окна</param>
        /// <param name="data">Коллекция данных для отображения</param>
        /// <param name="mainForm">Экзземпляр главного MDI окна</param>
        /// <param name="parent">Экземпляр родительского окна, которое является инициатором вызова данной формы</param>
        public DataViewForm(string text, IEnumerable<T> data, MainMDIForm mainForm, ILinkPickable parent) :this(text,ViewType.Select,data)
        {
            this.selectParent = parent;
            this.main = mainForm;
        }
        
        // Отображает данные в сотрируемой таблице.
        // Данный элемент DataGridView не поддерживает сортировку для польлзовательских коллекций.
        // Для корректной работы сортировки и фильтрации не стоит менят логику привязки данных.
        private void SetData()
        { 
            dataTable = dataSet.Tables.Add("Entity");

            // Получаем наименования свойств объекта в формате "Имя свойства" - "Отображаемое имя свойства".
            var columns = Data.First().GetTypePropertiesNameDisplayName();

            if (columns?.Count == 0)
                return;

            // Заполняем таблицу.
            using (var reader = ObjectReader.Create(Data, columns.Keys.ToArray()))
            {
                dataTable.Load(reader);
            }

            if (!dataTable.Columns.Contains("ID"))
                throw new ArgumentException("Необходим уникальный идентификатор");

            // Установка уникального первичного ключа.
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["ID"] };
            dataTable.Columns["ID"].Unique = true;

            // Переименовываем имена колонок на "Отображаемое имя свойства".
            foreach (DataColumn column in dataTable.Columns)
            {
                column.ColumnName = columns[column.ColumnName] ?? column.ColumnName;
            }
            
            
            bindingSource.DataMember = dataTable.TableName;

            // Скрываем данные. Пользователь не должен видеть уникальный идентификатор.
            mainDgv.Columns["ID"].Visible = false;
        }

        /// <summary>
        /// Устанавливает выбранный элемент из таблицы в запрашиваемой форме.
        /// </summary>
        public void SetSelected()
        {
            if (Type == ViewType.Select)
            {
                selectParent.SetLinkValue<T>(GetSelectedEntity());
                this.Close();
            }
        }

        /// <summary>
        /// Возвращает выбранную в данный момент в таблице сущность.
        /// </summary>                   
        public T GetSelectedEntity()
        {
            int id;

            if (mainDgv.SelectedRows.Count == 0)
                return null;

            if (!Int32.TryParse(mainDgv.SelectedRows[0].Cells[0].Value.ToString(), out id))
                return null;

            var entity = Data.FirstOrDefault(i => i.ID == id);

            return entity ?? main.Entities.GetSingle<T>(id);
        }

        /// <summary>
        /// Устанавливает значения сущности <c>entity</c> в строку таблицы. 
        /// Если строка существует с идентификатором существует - меняет ее значения,
        /// в обратном случае создает новую строку.
        /// </summary>
        /// <param name="entity">Сущность, значениями которой необходимо заполнить строку</param>
        public void SetDataTableRow(T entity)
        {
            if (entity == null)
                return;

            if(ItemsCount == 0)
            {
                Data = Data.Add(entity);
                Init();
            }

            var row = this.dataTable.Rows.Find(entity.ID);

            if (row == null)
            {
                row = this.dataTable.NewRow();
                Data.Add(entity);
            }

            var newValues = entity.GetDisplayedPropertiesValue();

            for (int i = 0; i < newValues.Length; i++)
            {
                row[i] = newValues[i];
            }

            if (this.dataTable.Rows.IndexOf(row) == -1)
                this.dataTable.Rows.Add(row);
        }

        /// <summary>
        /// Удаляет строку с идентификатором сущности из таблицы.
        /// </summary>
        /// <param name="entity">Сущность, строку с которой необходимо удалить</param>
        public void RemoveDataTableRow(T entity)
        {
            var row = this.dataTable.Rows.Find(entity.ID);

            if (row == null)
                return;

            this.dataTable.Rows.Remove(row);
        }
        
        /// <summary>
        /// Производит экспорт данных в excel файл, сохраняя фильтрацию и сортировку.
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        public void Export(string fileName)
        {
            var dt = dataTable.Copy();
            dt.PrimaryKey = null;
            dt.Columns.Remove("ID");

            var excel = new ExcelFile(1);
            excel.AddSheet(dt, this.Text, mainDgv.FilterString, mainDgv.SortString);
            excel.Save(fileName);
        }
        
        private void Init()
        {
            if (Data.Count() == 0)
            {
                noDataBox.Visible = true;
                return;
            }

            SetData();

            // Получаем размер колонок для корректного отображения содержимого.
            foreach (DataGridViewColumn column in mainDgv.Columns)
            {
                columnsSize += column.Width;
            }

            // Если текущий размер таблицы больше растаягиваем колонки.
            if (columnsSize < mainDgv.Size.Width)
                mainDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            noDataBox.Visible = false;
        }

        // Отображает форму в зависимости от типа.
        private void ShowOperationForm(ActionType type)
        {
            main.ShowFormByType(type, GetSelectedEntity(), this);
        }

        // Обработка события загрузки формы.
        private void DataViewForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;
        }

        // Обработка события нажатия на кнопку.
        private void mainDgv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetSelected();
                return;
            }

            if ((e.KeyCode != Keys.Space && e.KeyCode != Keys.F2 && e.KeyCode != Keys.Insert && e.KeyCode != Keys.Delete) || mainDgv.RowCount <= 0)
                return;

            ActionType type = ActionType.View;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    type = ActionType.View;
                    break;
                case Keys.F2:
                    type = ActionType.Edit;
                    break;
                case Keys.Insert:
                    type = ActionType.Add;
                    break;
                case Keys.Delete:
                    ((IDataView)this).Remove();
                    return;
            }

            ShowOperationForm(type);          
        }

        // Обработка события двойного щелчка.
        private void mainDgv_DoubleClick(object sender, EventArgs e)
        {
            SetSelected();
        }
         
        // Обработка события изменения строки фильтрации.     
        private void mainDgv_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource.Filter = mainDgv.FilterString;
        }

        // Обработка события изменения строки сортировки.
        private void mainDgv_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource.Sort = mainDgv.SortString;
        }
        
        // Обработка события изменения размера окна.
        private void mainDgv_SizeChanged(object sender, EventArgs e)
        {
            if (columnsSize < mainDgv.Size.Width)
                mainDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            else
                mainDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        /// <summary>
        /// Отображает окно для просмотра данных.
        /// </summary>
        void IDataView.View() => ShowOperationForm(ActionType.View);

        /// <summary>
        /// Отображает окно для добавления данных.
        /// </summary>
        void IDataView.Insert() => ShowOperationForm(ActionType.Add);

        /// <summary>
        /// Отображает окно для редактирования данных.
        /// </summary>
        void IDataView.Edit() => ShowOperationForm(ActionType.Edit);

        /// <summary>
        /// Удаляет выбранную строку из БД.
        /// </summary>
        void IDataView.Remove()
        {
            var entity = GetSelectedEntity();

            if (entity.RemoveEntity(main.Entities))
            {
                RemoveDataTableRow(entity);
            }
        }
    }
}
