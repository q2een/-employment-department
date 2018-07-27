using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public class BaseStudentForm : MDIChild<IStudent>, IStudent
    {
        protected BaseStudentForm() : base()
        {
        }

        public BaseStudentForm(ActionType type, IStudent entity = null) : base(type, entity)
        {
        }

        public BaseStudentForm(ActionType type, IStudent entity, IDataListView<IStudent> viewContext) : base(type, entity, viewContext)
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

        protected override string[] IngnoreProperties
        {
            get
            {
                return new string[] { "ID", "LevelOfEducation", "Faculty", "GenderName", "MartialStatusString", "FacultyName", "EducationLevel", "Specialization", "SelfEmploymentText", "PreferentialCategoryText" };
            }
        }

        #region IStudent

        public string ApplicationFormNumber { get; set; }
        public string Surname { get; set; }
        string IStudent.Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DOB { get; set; }
        public bool IsMale { get; set; }
        public bool MaritalStatus { get; set; }
        public int YearOfGraduation { get; set; }
        public long LevelOfEducation { get; set; }
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

        public string GenderName { get; }
        public string MartialStatusString { get; }
        public string FacultyName { get; }
        public string EducationLevel { get; }
        public string SelfEmploymentText { get;}
        public string Specialization { get; }
        public string PreferentialCategoryText { get; }

        #endregion

        #region IEditable implementation.

        public override bool Save()
        {
            var msg = $"Информация о студенте обновлена\nФИО студента: {((IStudent)this).Surname} {((IStudent)this).Name} {((IStudent)this).Patronymic}";

            if (this.UpdateFormEntityInDataBase<BaseStudentForm, IStudent>(Main.DataBase, msg, IngnoreProperties))
            {
                SetFormText();
                ViewContext?.SetDataTableRow(this as IStudent);

                return true;
            }

            return false;
        }

        public override void AddNewItem()
        {
            var msg = $"Студент {Surname} {((IStudent)this).Surname} {((IStudent)this).Name} {((IStudent)this).Patronymic}\nдобавлен в базу";
            if(this.InsertFormEntityToDataBase<BaseStudentForm, IStudent>(Main.DataBase, msg, IngnoreProperties))
            {
                var viewForm = ViewContext ?? Main.GetDataViewForm<IStudent>();

                viewForm?.SetDataTableRow(this as IStudent);

                this.Close();
            }
        }

        #endregion
    }
}
