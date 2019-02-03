using System.ComponentModel;

namespace EmploymentDepartment.BL
{
    public class Specialization : IIdentifiable, ISpecialization
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [Browsable(false)]
        public int Faculty { get; set; }

        [Browsable(false)]
        public long LevelOfEducation { get; set; }

        [DisplayName("Факультет")]
        public string FacultyName { get; set; }

        [DisplayName("Уровень образования")]
        public string LevelOfEducationName { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Код")] 
        public string SpecialtyCode { get; set; }

        // Наименование специальности.
        [Browsable(false)]
        public string SpecialtyName { get; set; }

        // Наименование профиля подготовки.
        [Browsable(false)]
        public string SpecialtyProfileName { get; set; }

        // Шифр.
        [Browsable(false)]
        public string Cipher { get; set; }
    }
}
