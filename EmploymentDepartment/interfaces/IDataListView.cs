using System.Collections;

namespace EmploymentDepartment
{
    public interface IDataListView<T> : IDataView, IDataSourceView where T: BL.IIdentifiable
    {
        System.Collections.Generic.IEnumerable<T> Data { get; }

        void SetDataTableRow(T entity);
        void RemoveDataTableRow(T entity);
        T GetSelectedEntity();  
    }
}
