namespace EmploymentDepartment.BL
{
    public class Vacancy
    {
        public int ID { get; set; }
        public string VacancyNumber { get; set; }
        public string  Post { get; set; }
        public int Employer { get; set; }
        public string  WorkArea { get; set; }
        public double Salary { get; set; }
        public bool? IsActive { get; set; }
        public string SalaryNote { get; set; }
        public GenderType Gender { get; set; }
        public string Features { get; set; }
    }
}
