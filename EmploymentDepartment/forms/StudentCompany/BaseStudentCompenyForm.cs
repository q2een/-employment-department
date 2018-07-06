using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public class BaseStudentCompanyForm : MDIChild<IStudentCompany>, IStudentCompany
    {
        public BaseStudentCompanyForm() : base()
        {

        }
        public BaseStudentCompanyForm(ActionType type, IStudentCompany entity = null) : base(type, entity)
        {
        }
        public BaseStudentCompanyForm(MainMDIForm mainForm, IStudentCompany entity) : base(mainForm, entity)
        {
        }
         
        #region IStudentCompany
        public int ID { get; set; }
        public int Student { get; set; }
        string IStudentCompany. CompanyName { get; set; }
        public bool Status { get; set; }
        public int? Vacancy { get; set; }
        public string Post { get; set; }
        public string Note { get; set; }
        #endregion

        #region IEditable implementation.

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());


        public override void SetDefaultValues() => this.SetPropertiesValue<IStudentCompany>(Entity, "");

        public override void Save()
        {
            var msg = $"Информация о месте работы студента обновлена";
            if (this.UpdateFormEntityInDataBase<BaseStudentCompanyForm, IStudentCompany>(main.DBGetter, msg, "ID", "Name"))
                SetFormText();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            var msg = $"Информация о месте работы студента добавлена в базу";
            this.InsertFormEntityToDataBase<BaseStudentCompanyForm, IStudentCompany>(main.DBGetter, msg, "ID", "Name");
        }
        #endregion
    }
}
