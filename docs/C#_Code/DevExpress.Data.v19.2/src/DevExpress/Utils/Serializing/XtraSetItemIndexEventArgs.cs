namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    public class XtraSetItemIndexEventArgs : XtraItemEventArgs
    {
        private int newIndex;

        public XtraSetItemIndexEventArgs(object rootObject, object owner, object collection, XtraPropertyInfo item, int newIndex) : base(rootObject, owner, collection, item)
        {
            this.newIndex = newIndex;
        }

        public virtual int NewIndex =>
            this.newIndex;
    }
}

