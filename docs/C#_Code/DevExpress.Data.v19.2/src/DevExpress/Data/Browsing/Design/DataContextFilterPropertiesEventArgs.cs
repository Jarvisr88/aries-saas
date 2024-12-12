namespace DevExpress.Data.Browsing.Design
{
    using DevExpress.Data.Browsing;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DataContextFilterPropertiesEventArgs : EventArgs
    {
        public DataContextFilterPropertiesEventArgs(IList<PropertyDescriptor> properties, object dataSource, string dataMember, DevExpress.Data.Browsing.DataContext dataContext);

        public IList<PropertyDescriptor> Properties { get; private set; }

        public object DataSource { get; private set; }

        public string DataMember { get; private set; }

        public DevExpress.Data.Browsing.DataContext DataContext { get; private set; }
    }
}

