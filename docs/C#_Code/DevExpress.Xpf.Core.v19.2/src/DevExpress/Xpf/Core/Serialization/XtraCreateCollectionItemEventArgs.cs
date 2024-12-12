namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class XtraCreateCollectionItemEventArgs : XtraItemRoutedEventArgs
    {
        public XtraCreateCollectionItemEventArgs(object source, string propertyName, XtraItemEventArgs e) : base(DXSerializer.CreateCollectionItemEvent, source, e)
        {
            this.CollectionName = propertyName;
        }

        public object CollectionItem { get; set; }

        public string CollectionName { get; private set; }
    }
}

