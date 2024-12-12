namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class BaseGridDesignTimeDataSource : DesignTimeDataSource, IDesignTimeDataSource
    {
        private readonly bool flattenHierarchy;

        public BaseGridDesignTimeDataSource(Type dataObjectType, int rowCount, bool useDistinctValues, bool flattenHierarchy);
        public BaseGridDesignTimeDataSource(Type dataObjectType, int rowCount, bool useDistinctValues, object originalDataSource = null, IEnumerable<DesignTimePropertyInfo> defaultColumns = null, List<DesignTimePropertyInfo> properties = null);
        protected bool IsReadonlyProperty(PropertyInfo property);
        protected override void PopulateColumns(DXGridDataController dataController);
        private void PopultateColumnsFromDataObjectType(DXGridDataController dataController);
        private void PopultateColumnsFromDesignProperties(DXGridDataController dataController);
    }
}

