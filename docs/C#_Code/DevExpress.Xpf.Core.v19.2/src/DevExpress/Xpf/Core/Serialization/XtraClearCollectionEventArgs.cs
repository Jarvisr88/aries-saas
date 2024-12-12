namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;

    public class XtraClearCollectionEventArgs : XtraItemRoutedEventArgs
    {
        public XtraClearCollectionEventArgs(object source, XtraItemEventArgs e) : base(DXSerializer.ClearCollectionEvent, source, e)
        {
        }
    }
}

