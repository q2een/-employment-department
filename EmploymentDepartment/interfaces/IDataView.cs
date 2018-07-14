namespace EmploymentDepartment
{
    public interface IDataView
    {
        int ItemsCount { get; }
        ViewType Type { get;}
        void View();
        void Insert();
        void Edit();
        void Remove();
    }
}
