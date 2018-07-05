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
    public partial class DataViewForm<T>: Form, IDataListView<T>, IDataView where T:class,IIdentifiable
    {
        private MainMDIForm main;
        private readonly ILinkPickable selectParent; 

        public DataViewForm(ViewType type, List<T> data)
        {
            InitializeComponent();
            this.Data = data;
            this.Type = type;
        }

        public DataViewForm(List<T> data, MainMDIForm mainForm, ILinkPickable parent) :this(ViewType.Select,data)
        {
            this.selectParent = parent;
            this.main = mainForm;
        }

        public List<T> Data { get; set; }
        public ViewType Type { get; set; }

        private void DataViewForm_Load(object sender, EventArgs e)
        {
            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            mainDgv.DataSource = Data;
            mainDgv.DoubleBuffered(true);
            mainDgv.StretchLastColumn();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;
        }

        public void SetSelected()
        {
            if (Type == ViewType.Select)
            {
                selectParent.SetLinkValue<T>(Data[mainDgv.SelectedRows[0].Index]);
                this.Close();
            }
        }

        private void ShowOperationForm(ActionType type)
        { 
            main.ShowFormByType(type, Data[mainDgv.SelectedRows[0].Index]);
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

        void IDataView.View() => ShowOperationForm(ActionType.View);

        void IDataView.Insert() => ShowOperationForm(ActionType.Add);

        void IDataView.Edit() => ShowOperationForm(ActionType.Edit);

        void IDataView.Remove()
        {
            throw new NotImplementedException();
        }
    }
}
