namespace EmploymentDepartment
{
    public interface IDataView
    {           
        ViewType Type { get; set; }
        void View();
        void Insert();
        void Edit();
        void Remove();
    }
}
