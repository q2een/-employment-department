using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public interface ISpecialization : IIdentifiable
    {
        [DisplayName("ID")]
        int ID { get; set; }

        int Faculty { get; set; }

        [Browsable(false)]
        int LevelOfEducation { get; set; }

        [DisplayName("Факультет")]
        string FacultyName { get; }

        [DisplayName("Уровень образования")]
        string LevelOfEducationName { get; }

        [DisplayName("Наименование")]
        string Name { get; set; }

    }
}
