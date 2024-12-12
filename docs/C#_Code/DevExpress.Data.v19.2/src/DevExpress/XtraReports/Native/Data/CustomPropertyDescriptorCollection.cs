namespace DevExpress.XtraReports.Native.Data
{
    using System;
    using System.ComponentModel;

    internal class CustomPropertyDescriptorCollection : PropertyDescriptorCollection
    {
        private PropertyDescriptorCollection source;

        public CustomPropertyDescriptorCollection(PropertyDescriptorCollection source, PropertyDescriptor[] properties);
        public override PropertyDescriptor Find(string name, bool ignoreCase);
    }
}

