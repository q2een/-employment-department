using EmploymentDepartment.BL;
using System;

namespace EmploymentDepartment
{
    public class BasePreferentialCategory : MDIChild<IPreferentialCategory>, IPreferentialCategory
    {
        public BasePreferentialCategory() : base()
        {
        }

        public BasePreferentialCategory(ActionType type, IPreferentialCategory entity = null) : base(type, entity)
        {
        }

        public BasePreferentialCategory(MainMDIForm mainForm, IPreferentialCategory entity) : base(mainForm, entity)
        {
        }

        protected override void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о льготной категории";
                    break;
                case ActionType.Add:
                    this.Text = $"Укажите текст льготной категории";
                    break;
                case ActionType.View:
                    this.Text = $"Просмотр информации о льготной категории";
                    break;
            }
        }

        #region IPreferentialCategory
        string IPreferentialCategory.Name { get; set; }
        #endregion

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());

        public override void SetDefaultValues() => this.SetPropertiesValue<IPreferentialCategory>(Entity, "");

        public override void Save()
        {
            var msg = $"Информация о льготной категории обновлена\nНаименование факультета: {((IPreferentialCategory)this).Name}";

            if (this.UpdateFormEntityInDataBase<BasePreferentialCategory, IPreferentialCategory>(main.DBGetter, msg, "ID"))
            {
                SetFormText();
                main.UpdatePreferentialCategories();
                this.Close();
            }
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            var msg = $"Льготная категория добавлена в базу.\nНаименование факультета: {((IPreferentialCategory)this).Name}";

            if (this.UpdateFormEntityInDataBase<BasePreferentialCategory, IPreferentialCategory>(main.DBGetter, msg, "ID"))
            {
                SetFormText();
                main.UpdatePreferentialCategories();
                this.Close();
            }
        }

        #endregion
    }
}
