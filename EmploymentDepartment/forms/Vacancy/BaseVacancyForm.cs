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

        #region IVacancy
        public string VacancyNumber { get; set; }
        public string Post { get; set; }
        public int Employer { get; set; }
        public string WorkArea { get; set; }
        public decimal Salary { get; set; }
        public bool? IsActive { get; set; }
        public string SalaryNote { get; set; }
        public int Gender { get; set; }
        public string Features { get; set; }

        string IVacancy.CompanyName { get; }
        public string GenderName { get; }
        #endregion

        #region IEditable implementation.

        public override void Save()
        {
            var msg = $"Информация о вакансии обновлена";
            if (this.UpdateFormEntityInDataBase<BaseVacancyForm, IVacancy>(main.DataBase, msg, "ID", "Name", "CompanyName", "GenderName"))
            {
                SetFormText();
                ViewContext?.SetDataTableRow(this as IVacancy);
            }
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
