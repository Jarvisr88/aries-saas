namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class XtraOldItemEventArgs : XtraItemEventArgs
    {
        public XtraOldItemEventArgs(object rootObject, object owner, object collection, XtraPropertyInfo item) : base(rootObject, owner, collection, item)
        {
        }

        public bool OldItem { get; set; }
    }
}

