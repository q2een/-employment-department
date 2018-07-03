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
            var student = EntGetter.GetStudents()[0];
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
            var active = this.ActiveMdiChild as IEditable;

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
            var form = new StudentCompanyForm(this);
            form.ShowDialog();
        }
    }
}
