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
    public partial class DataViewForm<T>: Form, IDataListView<T>
    {
        private MainMDIForm main;

        public DataViewForm(ViewType type, List<T> data)
        {
            InitializeComponent();
            this.Data = data;
            this.Type = type;
        }

        public List<T> Data { get; set; }
        public ViewType Type { get; set; }

        private void DataViewForm_Load(object sender, EventArgs e)
        {
            mainDgv.DataSource = Data;
            mainDgv.DoubleBuffered(true);

            if (!(this.MdiParent is MainMDIForm) && main == null)
                throw new ArgumentNullException();

            this.main = main == null ? this.MdiParent as MainMDIForm : main;
        }

        private void mainDgv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
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

            main.ShowFormByType(type, Data[mainDgv.SelectedRows[0].Index]); 
                    
        }
    }
}
