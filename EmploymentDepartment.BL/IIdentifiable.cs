using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public interface IIdentifiable
    {
        [DisplayName("ID")]
        int ID { get; set; }

        string Name { get; set; }
    }
}
