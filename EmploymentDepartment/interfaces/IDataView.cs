namespace EmploymentDepartment
{
    public interface IDataView
    {
        int ItemsCount { get; }
        ViewType Type { get; set; }
        void View();
        void Insert();
        void Edit();
        void Remove();
    }
}
