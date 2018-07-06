using System.ComponentModel;


namespace EmploymentDepartment.BL
{
    public class PreferentialCategory : IPreferentialCategory
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Льготная категория")]
        public string Name { get; set; }
    }
}
