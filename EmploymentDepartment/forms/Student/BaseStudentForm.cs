using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public class BaseStudentForm : MDIChild<IStudent>, IStudent
    {
        public BaseStudentForm() : base()
        {

        }

        public BaseStudentForm(ActionType type, IStudent entity = null) : base(type, entity)
        {
        }
        public BaseStudentForm(MainMDIForm mainForm, IStudent entity) : base(mainForm, entity)
        {
        }

        protected override void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о студенте [{Entity.Surname} {Entity.Name} {Entity.Patronymic}]";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление анкеты студента";
                    break;
                case ActionType.View:
                    this.Text = $"{Entity.Surname} {Entity.Name} {Entity.Patronymic} - Просмотр анкеты студента";
                    break;
            }
        }

        #region IStudent
       
        public string ApplicationFormNumber { get; set; }
        public string Surname { get; set; }
        string IStudent.Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DOB { get; set; }
        public int Gender { get; set; }
        public bool MaritalStatus { get; set; }
        public int YearOfGraduation { get; set; }
        public EducationLevelType LevelOfEducation { get; set; }
        public int Faculty { get; set; }
        public int FieldOfStudy { get; set; }
        public string StudyGroup { get; set; }
        public decimal Rating { get; set; }
        public int? PreferentialCategory { get; set; }
        public bool SelfEmployment { get; set; }
        public string City { get; set; }
        string IStudent.Region { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string RegCity { get; set; }
        public string RegRegion { get; set; }
        public string RegDistrict { get; set; }
        public string RegAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        #endregion

        #region IEditable implementation.

        public override void Save()
        {
            var msg = $"Информация о студенте обновлена\nФИО студента: {Surname} {((IStudent)this).Name} {Patronymic}";
            if (this.UpdateFormEntityInDataBase<BaseStudentForm, IStudent>(main.DBGetter, msg, "ID", "LevelOfEducation", "Faculty"))
                SetFormText();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            var msg = $"Студент {Surname} {((IStudent)this).Name} {Patronymic}\nдобавлен в базу";
            this.InsertFormEntityToDataBase<BaseStudentForm, IStudent>(main.DBGetter, msg, "ID", "LevelOfEducation", "Faculty");
        }

        #endregion
    }
}
