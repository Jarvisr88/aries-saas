namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    public class XtraItemEventArgs : EventArgs
    {
        private OptionsLayoutBase options;
        private object owner;
        private object collection;
        private XtraPropertyInfo item;
        private object rootObject;
        private int index;

        public XtraItemEventArgs(object owner, object collection, XtraPropertyInfo item) : this(null, owner, collection, item)
        {
        }

        public XtraItemEventArgs(object rootObject, object owner, object collection, XtraPropertyInfo item) : this(rootObject, owner, collection, item, OptionsLayoutBase.FullLayout)
        {
        }

        public XtraItemEventArgs(object rootObject, object owner, object collection, XtraPropertyInfo item, OptionsLayoutBase options) : this(rootObject, owner, collection, item, options, -1)
        {
        }

        public XtraItemEventArgs(object rootObject, object owner, object collection, XtraPropertyInfo item, OptionsLayoutBase options, int index)
        {
            this.options = options;
            this.owner = owner;
            this.collection = collection;
            this.item = item;
            this.rootObject = rootObject;
            this.index = index;
        }

        public OptionsLayoutBase Options =>
            this.options;

        public object Owner =>
            this.owner;

        public object Collection =>
            this.collection;

        public object RootObject =>
            this.rootObject;

        public XtraPropertyInfo Item =>
            this.item;

        public int Index =>
            this.index;
    }
}

