namespace EmploymentDepartment
{
    public interface IDataListView<T> : IDataView
    {
        System.Collections.Generic.List<T> Data { get; set; }         
    }
}
