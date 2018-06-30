using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EmploymentDepartment.BL;
using System.Reflection;

namespace EmploymentDepartment
{
    public partial class MainForm : Form
    {
        List<Faculty> fac;
        List<Specialization> spec;

        public MainForm()
        {
            InitializeComponent();
            dataGridView1.DoubleBuffered(true);
        }
        EntitiesGetter ent;
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlGetter db = new MySqlGetter();

            ent = new EntitiesGetter(db);
            var a = ent.GetStudents();

            fac = ent.GetFaculties();
            spec = ent.GetSpecializations();

            dataGridView1.DataSource = new BindingList<Student>(a);
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {             

        }
    }
}
