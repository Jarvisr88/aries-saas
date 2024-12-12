namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering.Helpers;
    using System;

    internal class DataSourcePropertyEvaluatorPropertyHandler : IEvaluatorPropertyHandler
    {
        public const string Name = "DataSource";
        public const string RowCount = "RowCount";
        public const string CurrentRowIndex = "CurrentRowIndex";
        public const string CurrentRowHierarchyLevel = "CurrentRowHierarchyLevel";
        private static readonly string[] knownNames;
        private readonly DataContext dataContext;
        private readonly object dataSource;
        private readonly string dataMember;

        static DataSourcePropertyEvaluatorPropertyHandler();
        public DataSourcePropertyEvaluatorPropertyHandler(DataContext dataContext, object dataSource, string dataMember);
        public object GetValue(EvaluatorProperty property);
        private object GetValueCore(string name);
        public static bool IsKnownName(string name);
        public static bool IsKnownPostfix(string bindingPath);
    }
}

