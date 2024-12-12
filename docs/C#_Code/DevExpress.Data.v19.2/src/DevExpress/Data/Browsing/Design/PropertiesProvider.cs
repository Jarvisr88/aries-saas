namespace DevExpress.Data.Browsing.Design
{
    using DevExpress.Data.Browsing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PropertiesProvider : IPropertiesProvider, IDisposable
    {
        private DevExpress.Data.Browsing.DataContext dataContext;
        private bool disposeDataContext;
        private ITypeSpecificsService serv;

        public PropertiesProvider();
        public PropertiesProvider(DevExpress.Data.Browsing.DataContext dataContext, ITypeSpecificsService serv);
        protected virtual bool CanProcessProperty(IPropertyDescriptor property);
        protected virtual GetPropertiesEventArgs CreatePropertiesEventArgs(IPropertyDescriptor[] args);
        public virtual void Dispose();
        protected virtual PropertyDescriptor[] FilterProperties(ICollection properties, object dataSource, string dataMember);
        public virtual void GetDataSourceDisplayName(object dataSource, string dataMember, EventHandler<GetDataSourceDisplayNameEventArgs> callback);
        public static string GetFullName(string dataMember, string name);
        public virtual void GetItemProperties(object dataSource, string dataMember, EventHandler<GetPropertiesEventArgs> action);
        public virtual void GetListItemProperties(object dataSource, string dataMember, EventHandler<GetPropertiesEventArgs> action);
        protected static PropertyDescriptor GetProperty(IPropertyDescriptor property);
        private string GetPropertyDisplayName(object dataSource, string dataMember, PropertyDescriptor property);
        protected static Type GetPropertyType(IPropertyDescriptor property);
        private bool IsComplexProperty(PropertyDescriptor property, object dataSource, string dataMember);
        protected List<IPropertyDescriptor> PostFilterProperties(ICollection<IPropertyDescriptor> properties);
        private IPropertyDescriptor[] ProcessProperties(PropertyDescriptorCollection properties, object dataSource, string dataMember);
        protected virtual void SortProperties(IPropertyDescriptor[] properties);
        public FakedPropertyDescriptor[] ToFakedProperties(object dataSource, string dataMember, PropertyDescriptor[] properties);
        private static string ToString(IPropertyDescriptor property);

        private ITypeSpecificsService Service { get; }

        public DevExpress.Data.Browsing.DataContext DataContext { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertiesProvider.<>c <>9;
            public static Comparison<IPropertyDescriptor> <>9__19_0;

            static <>c();
            internal int <SortProperties>b__19_0(IPropertyDescriptor x, IPropertyDescriptor y);
        }
    }
}

