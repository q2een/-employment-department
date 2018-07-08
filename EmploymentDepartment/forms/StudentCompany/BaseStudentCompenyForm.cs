using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public class BaseStudentCompanyForm : MDIChild<IStudentCompany>, IStudentCompany
    {
        protected BaseStudentCompanyForm() : base()
        {

        }

        public BaseStudentCompanyForm(ActionType type, IStudentCompany entity = null) : base(type, entity)
        {
        }

        public BaseStudentCompanyForm(ActionType type, IStudentCompany entity, IDataListView<IStudentCompany> viewContext) : base(type, entity, viewContext)
        {
        }

        public BaseStudentCompanyForm(MainMDIForm mainForm, IStudentCompany entity) : base(mainForm, entity)
        {
        }
         
        #region IStudentCompany
        public int ID { get; set; }
        public int Student { get; set; }
        string IStudentCompany.CompanyName { get; set; }
        public bool Status { get; set; }
        public int? Vacancy { get; set; }
        public string Post { get; set; }
        public string Note { get; set; }

        public string StudentFullName { get; }
        public string StatusText { get; }
        public string VacancyNumber { get; }
        #endregion

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());


        public override void SetDefaultValues() => this.SetPropertiesValue<IStudentCompany>(Entity, "StudentFullName", "StatusText", "VacancyNumber");

        public override void Save()
        {
            var msg = $"Информация о месте работы студента обновлена";
            if (this.UpdateFormEntityInDataBase<BaseStudentCompanyForm, IStudentCompany>(main.DBGetter, msg, "ID", "Name", "StudentFullName", "StatusText", "VacancyNumber"))
            {
                SetFormText();
                ViewContext?.SetDataTableRow(this as IStudentCompany);
            }
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            var msg = $"Информация о месте работы студента добавлена в базу";
            if(this.InsertFormEntityToDataBase<BaseStudentCompanyForm, IStudentCompany>(main.DBGetter, msg, "ID", "Name", "StudentFullName", "StatusText", "VacancyNumber"))
            {
                ViewContext?.SetDataTableRow(this as IStudentCompany);
                this.Close();
            }
        }
        #endregion
    }
}
