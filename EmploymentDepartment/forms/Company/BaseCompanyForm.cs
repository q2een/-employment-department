using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public class BaseCompanyForm : MDIChild<ICompany>, ICompany
    {
        public BaseCompanyForm() : base()
        {

        }

        public BaseCompanyForm(ActionType type, ICompany entity = null) : base(type, entity)
        {
        }
        public BaseCompanyForm(MainMDIForm mainForm, ICompany entity) : base(mainForm, entity)
        {
        }

        protected override void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о предприятии [{Entity.Name}]";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление предприятия в базу";
                    break;
                case ActionType.View:
                    this.Text = $"[{Entity.Name}] - Просмотр информации о предприятии";
                    break;
            }
        }

        #region ICompany
        public int ID { get; set; }
        public string CompanyNumber { get; set; }
        public string Name { get; set; }
        public string NameOfStateDepartment { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string DirectorName { get; set; }
        public string DirectorSurname { get; set; }
        public string DirectorPatronymic { get; set; }
        public string ContactName { get; set; }
        public string ContactSurname { get; set; }
        public string ContactPatronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        #endregion

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());

        public override void SetDefaultValues() => this.SetPropertiesValue<ICompany>(Entity, "");

        public override void Save()
        {
            var msg = $"Информация о предприятии «{(this as ICompany).Name}» обновлена";

            if (this.UpdateFormEntityInDataBase<BaseCompanyForm, ICompany>(main.DBGetter, msg, "ID"))
                SetFormText();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            var msg = $"Предприятие «{(this as ICompany).Name}»\nдобавлено в базу";

            if (this.UpdateFormEntityInDataBase<BaseCompanyForm, ICompany>(main.DBGetter, msg, "ID"))
                SetFormText();
        }

        #endregion
    }
}
