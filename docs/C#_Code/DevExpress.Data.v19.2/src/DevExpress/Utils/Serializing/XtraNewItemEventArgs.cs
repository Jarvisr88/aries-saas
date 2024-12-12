namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class XtraNewItemEventArgs : XtraItemEventArgs
    {
        public XtraNewItemEventArgs(object rootObject, object owner, object collection, XtraPropertyInfo item) : base(rootObject, owner, collection, item)
        {
        }

        public bool NewItem { get; set; }
    }
}

