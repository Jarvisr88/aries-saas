namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class XtraShouldSerailizeCollectionItemEventArgs : XtraItemRoutedEventArgs
    {
        public XtraShouldSerailizeCollectionItemEventArgs(object source, XtraItemEventArgs e, object item) : base(DXSerializer.ShouldSerializeCollectionItemEvent, source, e)
        {
            this.ShouldSerailize = true;
            this.Value = item;
        }

        public bool ShouldSerailize { get; set; }

        public object Value { get; set; }
    }
}

