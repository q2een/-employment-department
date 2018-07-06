using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class Faculty : IIdentifiable, IFaculty
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }
    }
}
