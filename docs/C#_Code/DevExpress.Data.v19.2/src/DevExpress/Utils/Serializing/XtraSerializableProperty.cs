namespace DevExpress.Utils.Serializing
{
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class XtraSerializableProperty : Attribute
    {
        private readonly XtraSerializationVisibility? visibility;
        private readonly XtraSerializationFlags flags;
        private bool clearCollection;
        private bool useFindItem;
        private bool useCreateItem;
        private bool mergeCollection;
        private int order;

        public XtraSerializableProperty() : this(nullable)
        {
        }

        public XtraSerializableProperty(XtraSerializationFlags flags) : this(nullable, flags, 0)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility) : this(new XtraSerializationVisibility?(visibility))
        {
        }

        public XtraSerializableProperty(int order) : this(nullable, XtraSerializationFlags.None, order)
        {
        }

        private XtraSerializableProperty(XtraSerializationVisibility? visibility)
        {
            this.visibility = visibility;
        }

        public XtraSerializableProperty(XtraSerializationFlags flags, int order) : this(nullable, flags, order)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, XtraSerializationFlags flags) : this(new XtraSerializationVisibility?(visibility), flags, 0)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, bool useCreateItem) : this(visibility, useCreateItem, false, false)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, int order) : this(visibility, XtraSerializationFlags.None, order)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, XtraSerializationFlags flags, int order) : this(new XtraSerializationVisibility?(visibility), flags, order)
        {
        }

        public XtraSerializableProperty(bool useCreateItem, bool useFindItem, bool clearCollection) : this(XtraSerializationVisibility.Collection, useCreateItem, useFindItem, clearCollection)
        {
        }

        private XtraSerializableProperty(XtraSerializationVisibility? visibility, XtraSerializationFlags flags, int order)
        {
            this.visibility = visibility;
            this.flags = flags;
            this.order = order;
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, bool useCreateItem, bool useFindItem, bool clearCollection) : this(visibility, useCreateItem, useFindItem, clearCollection, 0)
        {
        }

        public XtraSerializableProperty(bool useCreateItem, bool useFindItem, bool clearCollection, int order) : this(XtraSerializationVisibility.Collection, useCreateItem, useFindItem, clearCollection, order)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, bool useCreateItem, bool useFindItem, bool clearCollection, int order) : this(visibility, useCreateItem, useFindItem, clearCollection, order, XtraSerializationFlags.None)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, bool useCreateItem, bool useFindItem, bool clearCollection, int order, XtraSerializationFlags flags) : this(visibility, useCreateItem, useFindItem, clearCollection, false, order, flags)
        {
        }

        public XtraSerializableProperty(XtraSerializationVisibility visibility, bool useCreateItem, bool useFindItem, bool clearCollection, bool mergeCollection, int order, XtraSerializationFlags flags) : this(new XtraSerializationVisibility?(visibility), flags, order)
        {
            this.clearCollection = clearCollection;
            this.mergeCollection = mergeCollection;
            this.useFindItem = useFindItem;
            this.useCreateItem = useCreateItem;
        }

        public XtraSerializationFlags Flags =>
            this.flags;

        public int Order =>
            this.order;

        public bool ClearCollection =>
            this.clearCollection;

        public bool MergeCollection =>
            this.mergeCollection;

        public bool UseFindItem =>
            this.useFindItem;

        public bool UseCreateItem =>
            this.useCreateItem;

        public bool Serialize =>
            this.Visibility != XtraSerializationVisibility.Hidden;

        public bool IsCachedProperty =>
            (this.flags & XtraSerializationFlags.Cached) > XtraSerializationFlags.None;

        public bool DeserializeCollectionItemBeforeCallSetIndex =>
            (this.flags & XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex) > XtraSerializationFlags.None;

        public bool SupressDefaultValue =>
            ((this.flags & XtraSerializationFlags.SuppressDefaultValue) > XtraSerializationFlags.None) || this.IsAutoScale;

        public bool IsAutoScale =>
            (this.flags & XtraSerializationFlags.AutoScale) > XtraSerializationFlags.None;

        public bool IsAutoScaleIgnoreDefault =>
            (this.flags & XtraSerializationFlags.AutoScaleIgnoreDefault) > XtraSerializationFlags.None;

        public bool IsLoadOnly =>
            (this.flags & XtraSerializationFlags.LoadOnly) > XtraSerializationFlags.None;

        public bool IsCollectionContent =>
            (this.flags & XtraSerializationFlags.CollectionContent) > XtraSerializationFlags.None;

        public bool SerializeCollection
        {
            get
            {
                XtraSerializationVisibility valueOrDefault = this.visibility.GetValueOrDefault(XtraSerializationVisibility.Visible);
                return ((valueOrDefault == XtraSerializationVisibility.Collection) || ((valueOrDefault == XtraSerializationVisibility.SimpleCollection) || (valueOrDefault == XtraSerializationVisibility.NameCollection)));
            }
        }

        public XtraSerializationVisibility Visibility =>
            this.visibility.GetValueOrDefault(XtraSerializationVisibility.Visible);

        protected internal bool IsExactVisible =>
            (this.visibility != null) && (((XtraSerializationVisibility) this.visibility.Value) == XtraSerializationVisibility.Visible);
    }
}

