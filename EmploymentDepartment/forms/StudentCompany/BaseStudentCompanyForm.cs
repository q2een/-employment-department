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

        protected override string[] IngnoreProperties
        {
            get
            {
                return new string[] { "ID", "Name", "StudentFullName", "StatusText", "VacancyNumber" };
            }
        }

        #region IStudentCompany 
        public int Student { get; set; }
        public string NameOfCompany { get; set; }
        public bool Status { get; set; }
        public int? Vacancy { get; set; }
        public string Post { get; set; }
        public string Note { get; set; }
        public decimal? Salary { get; set; }
        public string NameOfStateDepartment { get; set; }
        public int YearOfEmployment { get; set; }

        public string StudentFullName { get; }
        public string StatusText { get; }
        public string VacancyNumber { get; }
        #endregion

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());


        public override void SetDefaultValues()
        {
            this.SetPropertiesValue<IStudentCompany>(Entity, "StudentFullName", "StatusText", "VacancyNumber");
            Extentions.ValidateControls(this, GetErrorProvider());
        }

        public override bool Save()
        {
            var msg = $"Информация о месте работы студента обновлена";
            if (this.UpdateFormEntityInDataBase<BaseStudentCompanyForm, IStudentCompany>(Main.DataBase, msg, IngnoreProperties))
            {
                SetFormText();
                ViewContext?.SetDataTableRow(this as IStudentCompany);

                return true;
            }

            return false;
        }

        public override void AddNewItem()
        {
            var msg = $"Информация о месте работы студента добавлена в базу";

            if (this.InsertFormEntityToDataBase<BaseStudentCompanyForm, IStudentCompany>(Main.DataBase, msg, IngnoreProperties))
            {
                var viewForm = ViewContext ?? Main.GetDataViewForm<IStudentCompany>();

                viewForm?.SetDataTableRow(this as IStudentCompany);

                var vacancy = (this as IStudentCompany).Vacancy;

                if (vacancy != null)
                    Main.GetDataViewForm<IVacancy>()?.RemoveDataTableRow(Main.Entities.GetSingle<Vacancy>((int)vacancy));

                this.Close();
            }
        }
        #endregion
    }
}
