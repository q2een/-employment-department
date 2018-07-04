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

        private int childFormNumber = 0;

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

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Окно " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

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

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void getStudentsMI_Click(object sender, EventArgs e)
        {
            var form = new StudentForm(ActionType.Add);
            form.MdiParent = this;
            form.Show();
        }

        private void addStudentMI_Click(object sender, EventArgs e)
        {
            IStudent student = EntGetter.GetStudents()[0];
            var form = new StudentForm(ActionType.Edit, student);
            form.MdiParent = this;
            form.Show();
        }

        private void MainMDIForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void addCompanyMI_Click(object sender, EventArgs e)
        {
            var company = EntGetter.GetCompanies()[0];
            var form = new CompanyFrom(ActionType.Edit, company);
            form.MdiParent = this;
            form.Show();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!(this.ActiveMdiChild is IUpdateble))
                return;

            var active = this.ActiveMdiChild as IUpdateble;

            active.Save();
        }

        private void specializationMI_Click(object sender, EventArgs e)
        {
            var spec = EntGetter.GetSpecializations()[0];
            var form = new SpecializationForm(this, ActionType.Edit, spec);
            form.ShowDialog();
        }

        private void факультетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var spec = Faculties[0];
            var form = new FacultyForm(this, ActionType.Edit, spec);
            form.ShowDialog();
        }

        private void вакансияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var company = EntGetter.GetCompanies()[0];
            var form = new StudentCompanyForm(this);
            form.MdiParent = this;
            form.Show();
        }

        private void предприятияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var student = EntGetter.GetStudents();
            var form = new DataViewForm<Student>(ViewType.Edit, student);
            form.MdiParent = this;
            form.Show();
        }

        private void ShowDialog(Form form)
        {
            form.ShowDialog(this);
        }

        
        private void ShowMdiChild<T,U>(T form, U entity) where T:Form, IEditable<U>, U where U:IIdentifiable
        {
            var temp = this.MdiChildren.FirstOrDefault(i => (i is T) && form.ID == entity.ID && (i as T).Type == form.Type);

            if (temp != null)
            {
                form.Dispose();
                temp.Show();
                temp.Activate();
                return;
            }

            form.MdiParent = this;
            form.Show();
        }

        private void ShowStudentFormByType(ActionType view, IStudent student)
        {
            switch (view)
            {
                case ActionType.View:
                    ShowDialog(new StudentForm(this, student));
                    break;
                case ActionType.Edit:
                case ActionType.Add:
                    ShowMdiChild(new StudentForm(view, student), student);
                    break;
            }
        }

        public void ShowFormByType(ActionType view, object type)
        {
            if(type is IStudent)
            {
                var student = type as IStudent;
                ShowStudentFormByType(view, student);
            }
        }
    }
}
