using EmploymentDepartment.BL;
using System;

namespace EmploymentDepartment
{
    public class BasePreferentialCategory : MDIChild<IPreferentialCategory>, IPreferentialCategory
    {
        private BasePreferentialCategory() 
        {
        }

        public BasePreferentialCategory(ActionType type, IPreferentialCategory entity = null) : base(type, entity)
        {
        }

        public BasePreferentialCategory(ActionType type, IPreferentialCategory entity, IDataListView<IPreferentialCategory> viewContext):base(type,entity,viewContext)
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
            var msg = $"Информация о льготной категории обновлена";

            if (this.UpdateFormEntityInDataBase<BasePreferentialCategory, IPreferentialCategory>(main.DataBase, msg, "ID"))
            {
                SetFormText();
                main.UpdatePreferentialCategories();

                ViewContext?.SetDataTableRow(this as IPreferentialCategory);

                this.Close();
            }
        }

        public override void AddNewItem()
        {
            var msg = $"Льготная категория добавлена в базу";
            if (this.InsertFormEntityToDataBase<BasePreferentialCategory, IPreferentialCategory>(main.DataBase, msg, "ID"))
            {
                main.UpdatePreferentialCategories();

                ViewContext?.SetDataTableRow(this as IPreferentialCategory);

                this.Close();
            }
        }

        #endregion
    }
}
