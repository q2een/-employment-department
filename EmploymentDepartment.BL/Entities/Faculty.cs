using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class Faculty : IIdentifiable, IFaculty
    {
        [Browsable(false)]
        public int ID { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }
    }
}
