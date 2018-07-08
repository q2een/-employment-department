using EmploymentDepartment.BL;
using FastMember;
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
    public partial class DataViewForm<T>: Form, IDataListView<T> where T:class,IIdentifiable
    {
        private MainMDIForm main;
        private DataTable dataTable = null;
        private DataSet dataSet = null;

        private readonly ILinkPickable selectParent;
        
        public DataViewForm( ViewType type, List<T> data)
        {
            if (data == null)
                throw new ArgumentNullException();

            InitializeComponent();
            this.Data = data;
            this.Type = type;

            dataTable = new DataTable();
            dataSet = new DataSet();

            bindingSource.DataSource = this.dataSet;

            mainDgv.DataSource = bindingSource;

            SetData();
        }

        public DataViewForm(List<T> data, MainMDIForm mainForm, ILinkPickable parent) :this(ViewType.Select,data)
        {
            this.selectParent = parent;
            this.main = mainForm;
        }

        public IEnumerable<T> Data { get; set; }
        public ViewType Type { get; set; }

        private void DataViewForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            mainDgv.DoubleBuffered(true);
            mainDgv.StretchLastColumn();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;
        }

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

        public void SetSelected()
        {
            if (Type == ViewType.Select)
            {
                selectParent.SetLinkValue<T>(GetSelectedEntity());
                this.Close();
            }
        }

        // TODO : GET ENTITY BY ID
        private T GetSelectedEntity()
        {
            int id;

            if (!Int32.TryParse(mainDgv.SelectedRows[0].Cells[0].Value.ToString(), out id))
                return null;
             
            return Data.FirstOrDefault(entity => entity.ID == id);
        }

        private void ShowOperationForm(ActionType type)
        { 
            main.ShowFormByType(type, GetSelectedEntity(), this);
        }

        public void SetDataTableRow<T>(T entity) where T:IIdentifiable
        {
            var row = this.dataTable.Rows.Find(entity.ID);

            if (row == null)
                row = this.dataTable.NewRow();

            row.ItemArray = entity.GetDisplayedPropertiesValue();

            if (this.dataTable.Rows.IndexOf(row) == -1)
                this.dataTable.Rows.Add(row);
            
        }

        private void mainDgv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetSelected();
                return;
            }

            if ((e.KeyCode != Keys.Space && e.KeyCode != Keys.F2 && e.KeyCode != Keys.Insert) || mainDgv.RowCount <= 0)
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
            }

            ShowOperationForm(type);          
        }

        private void mainDgv_DoubleClick(object sender, EventArgs e)
        {
            SetSelected();
        }

        private void mainDgv_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource.Filter = mainDgv.FilterString;
        }

        private void mainDgv_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource.Sort = mainDgv.SortString;
        }

        void IDataView.View() => ShowOperationForm(ActionType.View);

        void IDataView.Insert() => ShowOperationForm(ActionType.Add);

        void IDataView.Edit() => ShowOperationForm(ActionType.Edit);

        void IDataView.Remove()
        {
            throw new NotImplementedException();
        }
    }
}
