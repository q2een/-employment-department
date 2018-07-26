using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmploymentDepartment.BL
{
    public static class Tables
    { 
        public static Table GetTableNameByType<T>(T obj) where T : class, IIdentifiable
        {
            if (obj is IStudent)
                return Table.student;
            if (obj is ICompany)
                return Table.company;
            if (obj is IFaculty)
                return Table.faculty;
            if (obj is IPreferentialCategory)
                return Table.preferentialcategory;
            if (obj is ISpecialization)
                return Table.specialization;
            if (obj is IStudentCompany)
                return Table.studentcompany;
            if (obj is IVacancy)
                return Table.vacancy;

            throw new ArgumentException();
        }
    }

    public enum Table
    {
        student,
        company,
        faculty,
        specialization,
        vacancy,
        studentcompany,
        preferentialcategory
    }
}
