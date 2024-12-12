namespace DevExpress.XtraReports.Native.Data
{
    using DevExpress.XtraReports.Native;
    using System;
    using System.ComponentModel;

    public class AggregatedPropertyDescriptor : PropertyDescriptorWrapper
    {
        private Type propertyType;
        private string displayName;
        private string name;

        public AggregatedPropertyDescriptor(PropertyDescriptor oldPropertyDescriptor, Type propertyType);
        public AggregatedPropertyDescriptor(PropertyDescriptor oldPropertyDescriptor, Type propertyType, string name);

        public override string Name { get; }

        public override string DisplayName { get; }

        public override Type PropertyType { get; }
    }
}

