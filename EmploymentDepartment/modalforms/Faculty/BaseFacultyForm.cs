using EmploymentDepartment.BL;

namespace EmploymentDepartment
{
    public class BaseFacultyForm : MDIChild<IFaculty>, IFaculty
    {
        private BaseFacultyForm() : base()
        {
        }

        public BaseFacultyForm(ActionType type, IFaculty entity = null) : base(type, entity)
        {
        }

        public BaseFacultyForm(ActionType type, IFaculty entity, IDataListView<IFaculty> viewContext) : base(type, entity, viewContext)
        {
        }

        public BaseFacultyForm(MainMDIForm mainForm, IFaculty entity) : base(mainForm, entity)
        {
        }

        protected override void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о факультете [{Entity.Name}]";
                    break;
                case ActionType.Add:
                    this.Text = $"Укажите наименование факультета";
                    break;
                case ActionType.View:
                    this.Text = $"[{Entity.Name}] - Просмотр информации о факультете";
                    break;
            }
        }

        protected override string[] IngnoreProperties
        {
            get
            {
                return new string[] { "ID" };
            }
        }

        #region IFaculty
        string IFaculty.Name { get; set; }
        #endregion

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());

        public override void SetDefaultValues() => this.SetPropertiesValue<IFaculty>(Entity, "");

        public override bool Save()
        {
            var msg = $"Информация о факультете\nНаименование факультета: {((IFaculty)this).Name}";

            if (this.UpdateFormEntityInDataBase<BaseFacultyForm, IFaculty>(main.DataBase, msg, IngnoreProperties))
            {
                SetFormText();

                ViewContext?.SetDataTableRow(this as IFaculty);
                this.Close();
                return true;
            }

            return false;
        }

        public override void AddNewItem()
        {
            var msg = $"Факультет добавлен в базу.\nНаименование факультета: {((IFaculty)this).Name}";

            if (this.InsertFormEntityToDataBase<BaseFacultyForm, IFaculty>(main.DataBase, msg, IngnoreProperties))
            {
                SetFormText();

                var viewForm = ViewContext ?? main.GetDataViewForm<IFaculty>();
                viewForm?.SetDataTableRow(this as IFaculty);

                this.Close();
            }
        }

        #endregion
    }
}
