namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Windows;

    public class DXTabControlSerializationProvider : SerializationProvider
    {
        protected internal override object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e);
        protected internal override void OnEndDeserializing(DependencyObject dObj, string restoredVersion);
        protected internal override void OnEndSerializing(DependencyObject dObj);
        protected internal override void OnStartDeserializing(DependencyObject dObj, LayoutAllowEventArgs ea);
        protected internal override void OnStartSerializing(DependencyObject dObj);
    }
}

