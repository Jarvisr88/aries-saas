namespace DevExpress.Data.Browsing
{
    using System;
    using System.ComponentModel;

    public class DataContextUtils
    {
        private DataContext dataContext;

        public DataContextUtils(DataContext dataContext);
        protected virtual PropertyDescriptor[] FilterProperties(PropertyDescriptorCollection properties, object dataSource, string dataMember, Func<PropertyDescriptor, bool> predicate);
        public PropertyDescriptor[] GetDisplayedProperties(object dataSource, string dataMember, Func<PropertyDescriptor, bool> predicate);
        private string GetPropertyDisplayName(object dataSource, string dataMember, PropertyDescriptor property);
    }
}

