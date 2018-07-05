using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class Specialization : IIdentifiable, ISpecialization
    {
        [Browsable(false)]
        public int ID { get; set; }

        [Browsable(false)]
        public int Faculty { get; set; }

        [Browsable(false)]
        public int LevelOfEducation { get; set; }

        [DisplayName("Факультет")]
        public string FacultyName { get; set; }

        [DisplayName("Уровень образования")]
        public string LevelOfEducationName { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

    }
}
