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
    public partial class StudentForm : Form
    {
        public Student Student { get; private set; }
        public ActionType Type { get; private set; }

        public StudentForm(ActionType type, Student student = null)
        {
            if (type == ActionType.Edit && student == null)
                throw new ArgumentNullException();

            InitializeComponent();
            this.Student = student;
            this.Type = type;

            if(type == ActionType.Edit)
            {
                btnRemove.Visible = false;
                btnApply.Text = "Сохранить";
                this.Text = $"Редактирование информации о студенте [{student.Surname} {student.Name} {student.Patronymic}]" ;
            }

            /*vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Scroll += (sender1, e1) => { mainPanel.VerticalScroll.Value = vScrollBar1.Value; };
            mainPanel.Controls.Add(vScrollBar1);      */
        }

        private void StudentForm_ResizeEnd(object sender, EventArgs e)
        {
            mainPanel.AutoScroll = this.Size.Height < 630;
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            new EditModalForm((sender as Label).Tag as TextBox).ShowDialog();
        }

        private void lblEdit_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.RoyalBlue;
        }

        private void lblEdit_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = SystemColors.ControlText;
        }

        private void SetMartialStatusByGender(GenderType type)
        {
            cmbMaritalStatus.Items.Clear();
            switch(type)
            {
                case GenderType.Female:
                    cmbMaritalStatus.Items.Add("Не замужем");
                    cmbMaritalStatus.Items.Add("Замужем");
                    break;
                default:
                    cmbMaritalStatus.Items.Add("Женат");
                    cmbMaritalStatus.Items.Add("Не женат");
                    break;
            }

            cmbMaritalStatus.SelectedIndex = 0;
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMartialStatusByGender((GenderType)cmbGender.SelectedIndex + 1);
        }
    }

    public enum ActionType
    {
        Add,
        Edit
    }
}
