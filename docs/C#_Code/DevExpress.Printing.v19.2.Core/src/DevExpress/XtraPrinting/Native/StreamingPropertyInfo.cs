namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;

    internal class StreamingPropertyInfo : XtraPropertyInfo
    {
        private OptionsLayoutBase options;
        private StreamingSerializeHelper helper;
        private SerializablePropertyDescriptorPair pair;

        public StreamingPropertyInfo(XtraObjectInfo info, OptionsLayoutBase options, StreamingSerializeHelper helper) : base(info, null)
        {
            this.helper = helper;
            this.options = options;
            base.ChildProperties = new StreamingPropertyCollection(info.Instance, options, helper);
        }

        public StreamingPropertyInfo(object instance, SerializablePropertyDescriptorPair pair, OptionsLayoutBase options, StreamingSerializeHelper helper) : this(pair.Property.Name, pair.Property.PropertyType, pair.Property.GetValue(instance), pair.ShouldSerializeAsCollection() || pair.ShouldSerializeAsContent())
        {
            this.pair = pair;
            this.options = options;
            this.helper = helper;
            if (base.IsKey)
            {
                base.ChildProperties = this.CreateXtraPropertyCollection();
            }
        }

        public StreamingPropertyInfo(string name, OptionsLayoutBase options, StreamingSerializeHelper helper, bool isKey) : base(name, null, null, isKey)
        {
            this.helper = helper;
            this.options = options;
            if (base.IsKey)
            {
                base.ChildProperties = this.CreateXtraPropertyCollection();
            }
        }

        public StreamingPropertyInfo(string name, StreamingSerializeHelper helper, Type propertyType, object value, bool isKey) : base(name, propertyType, value, isKey)
        {
            this.helper = helper;
            this.options = null;
            if (base.IsKey)
            {
                base.ChildProperties = this.CreateXtraPropertyCollection();
            }
        }

        public StreamingPropertyInfo(string name, ICollection collection, object owner, SerializablePropertyDescriptorPair pair, OptionsLayoutBase options, StreamingSerializeHelper helper) : this(name, collection?.GetType(), collection, true)
        {
            this.helper = helper;
            this.options = options;
            this.pair = pair;
            base.ChildProperties = new StreamingItemsCollection(collection, owner, pair, this, options, helper);
        }

        public void AssignCachedContentIndex(int index)
        {
            IStreamingPropertyCollection childProperties = base.ChildProperties as IStreamingPropertyCollection;
            if (childProperties != null)
            {
                childProperties.AssignCachedContentIndex(index);
            }
        }

        protected override IXtraPropertyCollection CreateXtraPropertyCollection() => 
            new StreamingPropertyCollection(base.Value, this.options, this.helper);

        public void SetValue(object value)
        {
            base.Value = value;
            this.PropertyType = value?.GetType();
            base.ChildProperties = this.CreateXtraPropertyCollection();
        }

        public bool CachedProperty
        {
            get
            {
                IStreamingPropertyCollection childProperties = base.ChildProperties as IStreamingPropertyCollection;
                return ((childProperties != null) ? childProperties.HasCachedContentIndex : false);
            }
        }
    }
}

