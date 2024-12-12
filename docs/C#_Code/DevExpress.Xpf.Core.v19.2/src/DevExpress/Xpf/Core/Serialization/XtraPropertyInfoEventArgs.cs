namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class XtraPropertyInfoEventArgs : PropertyEventArgs
    {
        private readonly XtraPropertyInfo info;

        public XtraPropertyInfoEventArgs(RoutedEvent routedEvent, object source, PropertyDescriptor property, XtraPropertyInfo info) : base(property, source, routedEvent)
        {
            this.info = info;
        }

        public XtraPropertyInfo Info =>
            this.info;
    }
}

