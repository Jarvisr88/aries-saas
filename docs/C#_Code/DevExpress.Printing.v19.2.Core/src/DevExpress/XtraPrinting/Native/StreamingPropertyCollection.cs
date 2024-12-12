namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Data.Extensions;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class StreamingPropertyCollection : IStreamingPropertyCollection, IXtraPropertyCollection, ICollection, IEnumerable
    {
        private object instance;
        private OptionsLayoutBase options;
        private StreamingSerializeHelper helper;
        private IList<SerializablePropertyDescriptorPair> serializableProperties;
        private int cachedContentIndex = -1;
        private IList<XtraPropertyInfo> cachedProperties;

        public StreamingPropertyCollection(object instance, OptionsLayoutBase options, StreamingSerializeHelper helper)
        {
            this.instance = instance;
            this.options = options;
            this.helper = helper;
        }

        public void AssignCachedContentIndex(int index)
        {
            this.cachedContentIndex = index;
        }

        private XtraPropertyInfo CreateProperty(SerializablePropertyDescriptorPair pair)
        {
            if (pair == null)
            {
                return null;
            }
            if (pair.ShouldSerializeAsCollection())
            {
                ICollection collection = pair.Property.GetValue(this.instance) as ICollection;
                if (collection != null)
                {
                    return new StreamingPropertyInfo(pair.Property.Name, collection, this.instance, pair, this.options, this.helper);
                }
            }
            else if (pair.ShouldSerializeAsContent())
            {
                XtraPropertyInfo info = this.helper.GetSerializedPropertyAsContent(this.instance, pair.Property, this.options, pair.Attribute);
                if (info != null)
                {
                    return info;
                }
            }
            return new StreamingPropertyInfo(this.instance, pair, this.options, this.helper);
        }

        private IList<SerializablePropertyDescriptorPair> CreateSerializableProperties() => 
            (this.instance != null) ? this.helper.GetSerializableProperties(this.instance) : null;

        void IXtraPropertyCollection.Add(XtraPropertyInfo prop)
        {
        }

        void IXtraPropertyCollection.AddRange(ICollection props)
        {
        }

        private XtraPropertyInfo GetCachedProperty(int index) => 
            this.cachedProperties?[index];

        public IEnumerator GetEnumerator() => 
            new StreamingPropertyEnumerator(this);

        private XtraPropertyInfo GetOrCreateCachedProperty(int index) => 
            this.GetCachedProperty(index) ?? this.PushPropertyToCache(this.CreateProperty(this.SerializableProperties[index]), index);

        private XtraPropertyInfo PushPropertyToCache(XtraPropertyInfo info, int index)
        {
            StreamingPropertyInfo info2 = info as StreamingPropertyInfo;
            if ((info2 != null) && info2.CachedProperty)
            {
                if (this.cachedProperties == null)
                {
                    int count = this.SerializableProperties.Count;
                    List<XtraPropertyInfo> list = new List<XtraPropertyInfo>(count);
                    list.AddRange(Enumerable.Repeat<XtraPropertyInfo>(null, count));
                    this.cachedProperties = list;
                }
                this.cachedProperties[index] = info;
            }
            return info;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        protected IList<SerializablePropertyDescriptorPair> SerializableProperties
        {
            get
            {
                IList<SerializablePropertyDescriptorPair> serializableProperties = this.serializableProperties;
                if (this.serializableProperties == null)
                {
                    IList<SerializablePropertyDescriptorPair> local1 = this.serializableProperties;
                    serializableProperties = this.serializableProperties = this.CreateSerializableProperties();
                }
                return serializableProperties;
            }
        }

        public int Count
        {
            get
            {
                IList<SerializablePropertyDescriptorPair> serializableProperties = this.SerializableProperties;
                return (((serializableProperties != null) ? serializableProperties.Count : 0) + ((this.cachedContentIndex < 0) ? 0 : 1));
            }
        }

        public XtraPropertyInfo this[string name] =>
            ((name != "Index") || (this.cachedContentIndex < 0)) ? this.GetOrCreateCachedProperty(this.SerializableProperties.FindIndex<SerializablePropertyDescriptorPair>(p => p.Property.Name == name)) : new XtraPropertyInfo(name, typeof(int), this.cachedContentIndex);

        public XtraPropertyInfo this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                IList<SerializablePropertyDescriptorPair> serializableProperties = this.SerializableProperties;
                return (((serializableProperties == null) || (index >= serializableProperties.Count)) ? ((this.cachedContentIndex < 0) ? null : new XtraPropertyInfo("Index", typeof(int), this.cachedContentIndex)) : this.GetOrCreateCachedProperty(index));
            }
        }

        public bool IsSinglePass =>
            false;

        public object SyncRoot =>
            null;

        public bool IsSynchronized =>
            false;

        public int CachedContentIndex =>
            this.cachedContentIndex;

        public bool HasCachedContentIndex =>
            this.cachedContentIndex >= 0;

        object IStreamingPropertyCollection.Owner =>
            this.instance;
    }
}

