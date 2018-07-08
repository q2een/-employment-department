﻿using EmploymentDepartment.BL;
using System;

namespace EmploymentDepartment
{
    public class BaseSpecializationForm : MDIChild<ISpecialization>, ISpecialization
    {
        #region CTOR
        private BaseSpecializationForm()
        {

        }

        public BaseSpecializationForm(ActionType type, ISpecialization entity, IDataListView<ISpecialization> viewContext):base(type,entity,viewContext)
        {
        }

        public BaseSpecializationForm(MainMDIForm mainForm, ISpecialization entity) : base(mainForm, entity)
        {
        }
        #endregion

        protected override void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о профиле подготовки [{Entity.Name}]";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление профиля подготовки";
                    break;
                case ActionType.View:
                    this.Text = $"[{Entity.Name}] - Просмотр информации о профиле подготовки";
                    break;
            }
        }

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());

        public override void Save()
        {
            var msg = $"Информация о профиле подготовки обновлена\nНаименование профиля: {((ISpecialization)this).Name}";

            if (this.UpdateFormEntityInDataBase<BaseSpecializationForm, ISpecialization>(main.DBGetter, msg, "ID", "FacultyName", "LevelOfEducationName"))
            {
                SetFormText();
                main.UpdateFaculties();
                main.UpdateSpecializations();

                ViewContext?.SetDataTableRow(this as ISpecialization);

                this.Close();
            }
            
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            var msg = $"Профиль подготовки добавлен в базу.\nНаименование профиля: {((ISpecialization)this).Name}";

            if (this.InsertFormEntityToDataBase<BaseSpecializationForm, ISpecialization>(main.DBGetter, msg, "ID", "FacultyName", "LevelOfEducationName"))
            {
                SetFormText();
                main.UpdateFaculties();
                main.UpdateSpecializations();

                ViewContext?.SetDataTableRow(this as ISpecialization);

                this.Close();
            }
        }

        #endregion
         
        #region ISpecialization
        public int Faculty { get; set; }
        public int LevelOfEducation { get; set; }
        string ISpecialization.Name { get; set; }
        public string FacultyName { get; }
        public string LevelOfEducationName { get; }
        #endregion

    }
}
