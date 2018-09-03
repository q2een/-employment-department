namespace EmploymentDepartment
{
    /// <summary>
    /// Предоставляет интерфейс укзывающий, что реализующий его класс, предназначен для данных из источника данных.
    /// </summary>
    public interface IDataSourceView
    {
        /// <summary>
        /// Возвращает источник данных.
        /// </summary>
        System.Windows.Forms.BindingSource DataSource { get; }

        /// <summary>
        /// Экспортирует данные в файл.
        /// </summary>
        /// <param name="fileName">Полный путь к файлу</param>
        void Export(string fileName);

        void SetDataSourceFilter(string filterText);
    }
}
