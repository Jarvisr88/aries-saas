namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;

    public class CollectionItemSerializationStrategyCollection : CollectionItemSerializationStrategy
    {
        private XtraSerializationFlags parentFlags;
        private OptionsLayoutBase options;
        private XtraSerializableProperty attr;
        private string name;

        public CollectionItemSerializationStrategyCollection(SerializeHelper helper, string name, ICollection collection, object owner, XtraSerializationFlags parentFlags, OptionsLayoutBase options, XtraSerializableProperty attr) : base(helper, name, collection, owner)
        {
            this.parentFlags = parentFlags;
            this.options = options;
            this.attr = attr;
            this.name = name;
        }

        protected internal override bool AssignItemPropertyValue(XtraPropertyInfo itemProperty, object item)
        {
            int index = -1;
            if (!base.Helper.TrySerializeCollectionItemCacheIndex(this.name, this.attr, itemProperty.ChildProperties, item, ref index))
            {
                itemProperty.ChildProperties.AddRange(base.Helper.SerializeObject(item, this.parentFlags, this.options));
                SerializeHelper.AddIndexPropertyInfo(itemProperty, index);
            }
            return true;
        }

        protected internal XtraSerializationFlags ParentFlags =>
            this.parentFlags;

        protected internal OptionsLayoutBase Options =>
            this.options;
    }
}

