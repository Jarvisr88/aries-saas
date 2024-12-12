namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct StreamingItemsEnumerator : IEnumerator<XtraPropertyInfo>, IDisposable, IEnumerator
    {
        private ICollection collection;
        private IEnumerator en;
        private CollectionItemSerializationStrategy itemSerializationStrategy;
        private int index;
        private int startIndex;
        public StreamingItemsEnumerator(ICollection collection, int startIndex, CollectionItemSerializationStrategy itemSerializationStrategy)
        {
            this.collection = collection;
            this.itemSerializationStrategy = itemSerializationStrategy;
            this.startIndex = startIndex - 1;
            this.en = collection.GetEnumerator();
            this.index = 0;
        }

        public XtraPropertyInfo Current =>
            this.itemSerializationStrategy.SerializeCollectionItem(this.index + this.startIndex, this.en.Current);
        object IEnumerator.Current =>
            this.Current;
        public bool MoveNext()
        {
            this.index++;
            return this.en.MoveNext();
        }

        public void Reset()
        {
            this.index = 0;
            this.en.Reset();
        }

        public void Dispose()
        {
            this.collection = null;
            this.en = null;
            this.itemSerializationStrategy = null;
        }
    }
}

