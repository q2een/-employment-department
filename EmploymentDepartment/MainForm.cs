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

        private void BindComboboxData<T>(ComboBox cmb, List<T> data) where T : IIdentifiable
        {
            var value = comboBox1.SelectedValue;
            int id;

            cmb.DataSource = null;
            cmb.Items.Clear();
            cmb.DataSource = data;
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "ID";

            // Выделить элемент (если он существует), который был активен до изменений.
            if (!Int32.TryParse(value + "", out id))
                return;

            var elem = data.FirstOrDefault(i => i.ID == id);

            if (elem == null)
                return;

            cmb.SelectedIndex = data.IndexOf(elem);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = comboBox1.SelectedValue;
            int id;

            if (!Int32.TryParse(value + "", out id))
                return;

            var sp = spec.Where(i => (int)i.LevelOfEducation == comboBox2.SelectedIndex + 1 && i.Faculty == id).ToList();
            BindComboboxData(comboBox3, sp);
        }

        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {             
            var fc = ent.GetFaculties((EducationLevelType)(comboBox2.SelectedIndex + 1));
            BindComboboxData(comboBox1, fc);
            var sp = spec.Where(i => (int)i.LevelOfEducation == comboBox2.SelectedIndex + 1 && i.Faculty == (int)comboBox1.SelectedValue).ToList();
            BindComboboxData(comboBox3, sp);
        }
    }
}
