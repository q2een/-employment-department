namespace EmploymentDepartment
{
    public interface IDataListView<out T> : IDataView, IDataSourceView where T: BL.IIdentifiable
    {
        System.Collections.Generic.IEnumerable<T> Data { get; }        
        void SetDataTableRow<T>(T entity) where T : BL.IIdentifiable;
        void RemoveDataTableRow<T>(T entity) where T : BL.IIdentifiable;
        T GetSelectedEntity();        
    }
}
