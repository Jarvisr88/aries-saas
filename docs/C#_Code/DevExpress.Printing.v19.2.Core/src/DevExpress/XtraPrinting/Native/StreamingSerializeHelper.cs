namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class StreamingSerializeHelper : SerializeHelper
    {
        private StreamingSerializationContext streamingSerializationContext;

        public StreamingSerializeHelper() : this(null, new StreamingSerializationContext())
        {
        }

        public StreamingSerializeHelper(object rootObject, StreamingSerializationContext context) : base(rootObject, context)
        {
            this.streamingSerializationContext = context;
        }

        protected internal override CollectionItemSerializationStrategy CreateCollectionItemSerializationStrategy(XtraSerializableProperty attr, string name, ICollection collection, object owner, XtraSerializationFlags parentFlags, OptionsLayoutBase options)
        {
            XtraSerializationVisibility visibility = attr.Visibility;
            return ((visibility == XtraSerializationVisibility.Collection) ? new StreamingStrategyCollection(this, name, collection, owner, parentFlags, options, attr) : ((visibility != XtraSerializationVisibility.NameCollection) ? base.CreateCollectionItemSerializationStrategy(attr, name, collection, owner, parentFlags, options) : new StreamingStrategyName(this, name, options, collection, owner)));
        }

        protected override XtraPropertyInfo CreateXtraPropertyInfo(PropertyDescriptor prop, object value, bool isKey) => 
            new StreamingPropertyInfo(prop.Name, this, prop.PropertyType, value, isKey);

        public IList<SerializablePropertyDescriptorPair> GetSerializableProperties(object instance)
        {
            List<SerializablePropertyDescriptorPair> list2 = new List<SerializablePropertyDescriptorPair>();
            foreach (SerializablePropertyDescriptorPair pair in this.streamingSerializationContext.GetSortedProperties(instance))
            {
                XtraSerializableProperty attribute = pair.Attribute;
                if (this.CheckNeedSerialize(instance, pair.Property, attribute, (attribute != null) ? attribute.Flags : XtraSerializationFlags.None))
                {
                    list2.Add(pair);
                }
            }
            return list2;
        }

        protected internal override XtraPropertyInfo GetSerializedPropertyAsContent(object obj, PropertyDescriptor prop, OptionsLayoutBase options, XtraSerializableProperty attr)
        {
            SerializationInfo info;
            XtraPropertyInfo info2;
            if (this.TrySerializeCachedValue(obj, prop, attr, out info2, out info))
            {
                return info2;
            }
            StreamingPropertyInfo info3 = base.GetSerializedPropertyAsContent(obj, prop, options, attr) as StreamingPropertyInfo;
            if ((info3 != null) && (info3.Value == null))
            {
                info3.SetValue(prop.GetValue(obj));
            }
            if (info != null)
            {
                info3.AssignCachedContentIndex(info.Index);
            }
            return info3;
        }

        protected internal override IXtraPropertyCollection SerializeObjectCore(object obj, XtraSerializationFlags parentFlags, OptionsLayoutBase options) => 
            new StreamingPropertyCollection(obj, options, this);

        private bool TrySerializeCachedValue(object obj, PropertyDescriptor prop, XtraSerializableProperty attr, out XtraPropertyInfo result, out SerializationInfo info)
        {
            result = null;
            info = null;
            if (base.ShouldNotTryCache(attr))
            {
                return false;
            }
            object obj2 = prop.GetValue(obj);
            if (obj2 == null)
            {
                return false;
            }
            info = base.RootSerializationObject.GetIndexByObject(prop.Name, obj2);
            if (info == null)
            {
                return false;
            }
            if (!info.Serialized)
            {
                info.Serialized = true;
                return false;
            }
            result = new XtraPropertyInfo(prop.Name, typeof(int), info.Index);
            return true;
        }

        private class StreamingStrategyCollection : CollectionItemSerializationStrategyCollection
        {
            private OptionsLayoutBase options;

            public StreamingStrategyCollection(SerializeHelper helper, string name, ICollection collection, object owner, XtraSerializationFlags parentFlags, OptionsLayoutBase options, XtraSerializableProperty attr) : base(helper, name, collection, owner, parentFlags, options, attr)
            {
                this.options = options;
            }

            protected internal override bool AssignItemPropertyValue(XtraPropertyInfo itemProperty, object item)
            {
                ((StreamingPropertyInfo) itemProperty).SetValue(item);
                return true;
            }

            protected internal override XtraPropertyInfo CreateItemPropertyInfoCore(int index, bool isSimpleCollection) => 
                new StreamingPropertyInfo("Item" + index.ToString(), this.options, (StreamingSerializeHelper) base.Helper, !isSimpleCollection);
        }

        private class StreamingStrategyName : CollectionItemSerializationStrategyName
        {
            private OptionsLayoutBase options;

            public StreamingStrategyName(SerializeHelper helper, string name, OptionsLayoutBase options, ICollection collection, object owner) : base(helper, name, collection, owner)
            {
                this.options = options;
            }

            protected internal override bool AssignItemPropertyValue(XtraPropertyInfo itemProperty, object item)
            {
                ((StreamingPropertyInfo) itemProperty).SetValue(item);
                return true;
            }

            protected internal override XtraPropertyInfo CreateItemPropertyInfoCore(int index, bool isSimpleCollection) => 
                new StreamingPropertyInfo("Item" + index.ToString(), this.options, (StreamingSerializeHelper) base.Helper, !isSimpleCollection);
        }
    }
}

