using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public interface IPreferentialCategory : IIdentifiable
    {
        [DisplayName("ID")]
        int ID { get; set; }

        [DisplayName("Льготная категория")]
        string Name { get; set; }
    }
}
