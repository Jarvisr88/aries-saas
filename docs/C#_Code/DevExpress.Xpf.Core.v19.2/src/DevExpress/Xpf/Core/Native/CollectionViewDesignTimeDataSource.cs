namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class CollectionViewDesignTimeDataSource : BaseGridDesignTimeDataSource
    {
        public CollectionViewDesignTimeDataSource(Type dataObjectType, int rowCount, bool useDistinctValues, bool flattenHierarchy);
        public CollectionViewDesignTimeDataSource(Type dataObjectType, int rowCount, bool useDistinctValues, object originalDataSource = null, IEnumerable<DesignTimePropertyInfo> defaultColumns = null);
        protected override DesignTimeDataSource.DesignTimePropertyDescriptor CreatePropertyDescriptor(bool useDistinctValues, DesignTimePropertyInfo propertyInfo);

        private class CollectionViewDesignTimePropertyDescriptor : DesignTimeDataSource.DesignTimePropertyDescriptor
        {
            public CollectionViewDesignTimePropertyDescriptor(string name, Type propertyType, bool useDistinctValues, bool isReadonly);
            protected override object[][] CreateDistinctValues();
            protected override object[] CreateValues();
        }
    }
}

