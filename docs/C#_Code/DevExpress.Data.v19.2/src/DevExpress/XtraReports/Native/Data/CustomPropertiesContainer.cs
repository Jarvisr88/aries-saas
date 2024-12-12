namespace DevExpress.XtraReports.Native.Data
{
    using System;
    using System.ComponentModel;

    public class CustomPropertiesContainer
    {
        private PropertyDescriptor[] customProperties;

        public CustomPropertiesContainer(PropertyDescriptor[] customProperties);
        public PropertyDescriptorCollection MergeProperties(PropertyDescriptorCollection properties);
    }
}

