using System;

namespace EmploymentDepartment.BL
{
    public class Vacancy: IVacancy
    {
        public int ID { get; set; }
        public string VacancyNumber { get; set; }
        public string  Post { get; set; }
        public int Employer { get; set; }
        public string  WorkArea { get; set; }
        public decimal Salary { get; set; }
        public bool? IsActive { get; set; }
        public string SalaryNote { get; set; }
        public int Gender { get; set; }
        public string Features { get; set; }
        string IIdentifiable.Name
        {
            get
            {
                return VacancyNumber;
            } 
            set
            {
                
            }
        }
    }
}
