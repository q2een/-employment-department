using EmploymentDepartment.BL;
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

        protected override string[] IngnoreProperties
        {
            get
            {
                return new string[] { "ID", "FacultyName", "LevelOfEducationName" };
            }
        }

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());

        public override bool Save()
        {
            var msg = $"Информация о профиле подготовки обновлена\nНаименование профиля: {((ISpecialization)this).Name}";

            if (this.UpdateFormEntityInDataBase<BaseSpecializationForm, ISpecialization>(Main.DataBase, msg, IngnoreProperties))
            {
                SetFormText();

                ViewContext?.SetDataTableRow(this as ISpecialization);

                this.Close();

                return true;
            }

            return false;
        }

        public override void AddNewItem()
        {
            var msg = $"Профиль подготовки добавлен в базу.\nНаименование профиля: {((ISpecialization)this).Name}";

            if (this.InsertFormEntityToDataBase<BaseSpecializationForm, ISpecialization>(Main.DataBase, msg, IngnoreProperties))
            {
                SetFormText();

                var viewForm = ViewContext ?? Main.GetDataViewForm<ISpecialization>();
                viewForm?.SetDataTableRow(this as ISpecialization);

                this.Close();
            }
        }

        #endregion
         
        #region ISpecialization
        public int Faculty { get; set; }
        public long LevelOfEducation { get; set; }
        string ISpecialization.Name { get; set; }
        public string FacultyName { get; }
        public string LevelOfEducationName { get; }
        public string SpecialtyCode { get; set; }
        public string SpecialtyName { get; set; }
        public string SpecialtyProfileName { get; set; }
        public string Cipher { get; set; }
        #endregion

    }
}
