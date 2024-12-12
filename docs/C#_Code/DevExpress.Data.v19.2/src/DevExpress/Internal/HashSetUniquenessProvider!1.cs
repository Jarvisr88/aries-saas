namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class HashSetUniquenessProvider<T> : DXCollectionUniquenessProvider<T>
    {
        private HashSet<T> hashSet;
        private DXCollectionBase<T> collection;

        public HashSetUniquenessProvider(DXCollectionBase<T> collection)
        {
            this.hashSet = new HashSet<T>();
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
        }

        public override bool LookupObject(T value) => 
            this.HashSet.Contains(value);

        public override int LookupObjectIndex(T value) => 
            this.LookupObject(value) ? this.Collection.IndexOf(value) : -1;

        public override void OnClearComplete()
        {
            this.HashSet.Clear();
        }

        public override void OnInsertComplete(T value)
        {
            this.HashSet.Add(value);
        }

        public override void OnRemoveComplete(T value)
        {
            this.HashSet.Remove(value);
        }

        public override void OnSetComplete(T oldValue, T newValue)
        {
            this.HashSet.Remove(oldValue);
            this.HashSet.Add(newValue);
        }

        public override DXCollectionUniquenessProviderType Type =>
            DXCollectionUniquenessProviderType.MaxPerformanceMinMemory;

        protected internal HashSet<T> HashSet =>
            this.hashSet;

        protected internal DXCollectionBase<T> Collection =>
            this.collection;
    }
}

