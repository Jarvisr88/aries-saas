namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Reflection;

    internal class StreamingItemsCollection : IStreamingItemsCollection, IStreamingPropertyCollection, IXtraPropertyCollection, ICollection, IEnumerable
    {
        private CollectionItemSerializationStrategy itemSerializationStrategy;
        private SerializablePropertyDescriptorPair pair;
        private StreamingSerializeHelper helper;
        private OptionsLayoutBase options;
        private ICollection collection;
        private int startIndex = 1;

        public StreamingItemsCollection(ICollection collection, object owner, SerializablePropertyDescriptorPair pair, StreamingPropertyInfo root, OptionsLayoutBase options, StreamingSerializeHelper helper)
        {
            this.collection = collection;
            this.options = options;
            this.helper = helper;
            this.pair = pair;
            XtraSerializableProperty attribute = pair.Attribute;
            this.itemSerializationStrategy = helper.CreateCollectionItemSerializationStrategy(attribute, root.Name, collection, owner, attribute.Flags, options);
        }

        void IXtraPropertyCollection.Add(XtraPropertyInfo prop)
        {
            throw new NotSupportedException();
        }

        void IXtraPropertyCollection.AddRange(ICollection props)
        {
            throw new NotSupportedException();
        }

        void IStreamingItemsCollection.IncreaseStartIndex(int offset)
        {
            this.startIndex += offset;
        }

        void IStreamingItemsCollection.SetItemsSource(ICollection collection)
        {
            this.collection = collection;
        }

        void IStreamingPropertyCollection.AssignCachedContentIndex(int index)
        {
            throw new NotSupportedException();
        }

        public IEnumerator GetEnumerator() => 
            new StreamingItemsEnumerator(this.collection, this.startIndex, this.itemSerializationStrategy);

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        public XtraPropertyInfo this[string name]
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public XtraPropertyInfo this[int index] =>
            this.itemSerializationStrategy.SerializeCollectionItem(index + this.startIndex, ((IList) this.collection)[index]);

        public bool IsSinglePass =>
            false;

        public int Count =>
            this.collection.Count;

        public object SyncRoot =>
            null;

        public bool IsSynchronized =>
            false;

        object IStreamingPropertyCollection.Owner =>
            this.collection;

        bool IStreamingPropertyCollection.HasCachedContentIndex =>
            false;

        int IStreamingPropertyCollection.CachedContentIndex
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}

