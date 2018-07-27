using EmploymentDepartment.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment
{
    public class BaseVacancyForm : MDIChild<IVacancy>, IVacancy
    {
        protected BaseVacancyForm() : base()
        {

        }

        public BaseVacancyForm(ActionType type, IVacancy entity = null) : base (type, entity)
        {
        }

        public BaseVacancyForm(ActionType type, IVacancy entity, IDataListView<IVacancy> viewContext) : base(type, entity, viewContext)
        {
        }

        public BaseVacancyForm(MainMDIForm mainForm, IVacancy entity) : base(mainForm, entity)
        {
        }

        protected override void SetFormText()
        {
            switch (Type)
            {
                case ActionType.Edit:
                    this.Text = $"Редактирование информации о вакансии";
                    break;
                case ActionType.Add:
                    this.Text = $"Добавление вакансии";
                    break;
                case ActionType.View:
                    this.Text = $"Просмотр вакансии";
                    break;
            }
        }

        protected override string[] IngnoreProperties
        {
            get
            {
                return new string[] { "ID", "Name", "CompanyName", "GenderName" };
            }
        }
         
        #region IVacancy
        public string VacancyNumber { get; set; }
        public string Post { get; set; }
        public int Employer { get; set; }
        public string WorkArea { get; set; }
        public decimal Salary { get; set; }
        public bool? IsActive { get; set; }
        public string SalaryNote { get; set; }
        public long Gender { get; set; }
        public string Features { get; set; }

        string IVacancy.CompanyName { get; }
        public string GenderName { get; }
        #endregion

        #region IEditable implementation.

        public override bool Save()
        {
            var msg = $"Информация о вакансии обновлена";
            if (this.UpdateFormEntityInDataBase<BaseVacancyForm, IVacancy>(Main.DataBase, msg, IngnoreProperties))
            {
                SetFormText();
                ViewContext?.SetDataTableRow(this as IVacancy);

                return true;
            }

            return false;
        }

        public override bool ValidateFields() => Extentions.ValidateFields(this, GetErrorProvider());

        public override void SetDefaultValues()
        {
            this.SetPropertiesValue<IVacancy>(Entity, "CompanyName", "GenderName");
            Extentions.ValidateControls(this, GetErrorProvider());
        }

        #endregion
    }
}
