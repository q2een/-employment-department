using System.ComponentModel;


namespace EmploymentDepartment.BL
{
    public class PreferentialCategory : IPreferentialCategory
    {
        [Browsable(false)]
        public int ID { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }
    }
}
