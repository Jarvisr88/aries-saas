namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class XtraFindCollectionItemEventArgs : XtraItemRoutedEventArgs
    {
        public XtraFindCollectionItemEventArgs(object source, string propertyName, XtraItemEventArgs e) : base(DXSerializer.FindCollectionItemEvent, source, e)
        {
            this.CollectionName = propertyName;
        }

        public object CollectionItem { get; set; }

        public string CollectionName { get; private set; }
    }
}

