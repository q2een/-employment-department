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
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBGetter db = new DBGetter();

            var a = db.GetCollection("SELECT s.ID, s.ApplicationFormNumber, s.Name, s.Surname, s.Patronymic, s.DOB, s.Gender + 0 AS Gender,"+
  "s.MaritalStatus, s.YearOfGraduation, d.LevelOfEducation, d.Faculty, d.Specialization, s.StudyGroup," +
  "s.Rating, s.PreferentialCategory, s.SelfEmployment, s.City, s.Region, s.District, s.Address, s.RegCity," +
  "s.RegRegion, s.RegDistrict, s.RegAddress, s.Phone, s.Email FROM student s INNER JOIN" +
  "(SELECT sp.LevelOfEducation + 0 as LevelOfEducation, sp.Name AS Specialization, f.Name AS Faculty, " +
  "sp.ID FROM specialization sp INNER JOIN faculty f ON sp.Faculty = f.ID) AS d ON s.FieldOfStudy = d.ID");
            var Student = new Student();
            SetProperties(Student,a[0]);
        }

        private void SetProperties<T>(T obj, Dictionary<string, object> dict)
        {
            if (dict == null)
                return;

            var properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (dict.ContainsKey(property.Name))
                {
                    try
                    {
                        property.SetValue(obj, dict[property.Name], null);
                    }
                    catch (ArgumentException)
                    {
                        // if(propInf.GetType() == Type.E)
                       // property.SetValue(obj, 1, null);
                    }
                }
            }
        }
    }
}
