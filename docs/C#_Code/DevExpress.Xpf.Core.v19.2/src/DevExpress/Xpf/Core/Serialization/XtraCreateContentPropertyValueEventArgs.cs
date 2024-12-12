namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class XtraCreateContentPropertyValueEventArgs : XtraItemRoutedEventArgs
    {
        public XtraCreateContentPropertyValueEventArgs(object source, System.ComponentModel.PropertyDescriptor propertyDescriptor, XtraItemEventArgs e) : base(DXSerializer.CreateContentPropertyValueEvent, source, e)
        {
            this.PropertyDescriptor = propertyDescriptor;
        }

        public object PropertyValue { get; set; }

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor { get; private set; }
    }
}

