namespace DevExpress.XtraReports.Native.Data
{
    using DevExpress.Data.Browsing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PropertyAggregator
    {
        public const string ReferenceAsObjectTail = "!";
        public const string ReferenceAsKeyTail = "!Key";
        private DataContext dataContext;

        public PropertyAggregator();
        public PropertyAggregator(DataContext dataContext);
        public static PropertyDescriptor[] Aggregate(ICollection properties);
        public PropertyDescriptor[] Aggregate(ICollection properties, object dataSource, string dataMember);
        protected virtual PropertyDescriptor GetAggregatedProperty(PropertyDescriptor property, List<PropertyDescriptor> list, object dataSource, string dataMember);
        protected string GetName(PropertyDescriptor property);
        private PropertyDescriptor GetProperty(List<PropertyDescriptor> list, object dataSource, string dataMember);
        protected virtual bool IsXpoDescriptor(PropertyDescriptor property);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyAggregator.<>c <>9;
            public static Predicate<PropertyDescriptor> <>9__9_0;
            public static Predicate<PropertyDescriptor> <>9__9_1;

            static <>c();
            internal bool <GetProperty>b__9_0(PropertyDescriptor prop);
            internal bool <GetProperty>b__9_1(PropertyDescriptor prop);
        }
    }
}

