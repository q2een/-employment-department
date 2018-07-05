using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public class BaseSpecializationForm : MDIChild<ISpecialization>, ISpecialization
    {
        public BaseSpecializationForm() : base()
        {

        }

        public BaseSpecializationForm(ActionType type, ISpecialization entity = null) : base(type, entity)
        {
        }

        public BaseSpecializationForm(MainMDIForm mainForm, ISpecialization entity) : base(mainForm, entity)
        {
        }

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

        #region ISpecialization
        public int ID { get; set; }
        public int Faculty { get; set; }
        public int LevelOfEducation { get; set; }
        string ISpecialization.Name { get; set; }

        #endregion

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());

        public override void SetDefaultValues() => this.SetPropertiesValue<ISpecialization>(Entity, "");

        public override void Save()
        {
            var msg = $"Информация о профиле подготовки обновлена\nНаименование профиля: {((ISpecialization)this).Name}";

            if (this.UpdateFormEntityInDataBase<BaseSpecializationForm, ISpecialization>(main.DBGetter, msg, "ID"))
                SetFormText();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            var msg = $"Профиль подготовки добавлен в базу.\nНаименование профиля: {((ISpecialization)this).Name}";

            if (this.UpdateFormEntityInDataBase<BaseSpecializationForm, ISpecialization>(main.DBGetter, msg, "ID"))
                SetFormText();
        }

        #endregion
    }
}
