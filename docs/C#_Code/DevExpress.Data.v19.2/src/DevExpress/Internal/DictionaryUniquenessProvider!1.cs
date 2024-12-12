namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class DictionaryUniquenessProvider<T> : DXCollectionUniquenessProvider<T>
    {
        private readonly DXCollectionBase<T> collection;
        private readonly Dictionary<T, T> dictionary;

        public DictionaryUniquenessProvider(DXCollectionBase<T> collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
            this.dictionary = new Dictionary<T, T>();
        }

        public override bool LookupObject(T value)
        {
            T local;
            return this.dictionary.TryGetValue(value, out local);
        }

        public override int LookupObjectIndex(T value)
        {
            T local;
            return (!this.dictionary.TryGetValue(value, out local) ? -1 : this.collection.IndexOf(value));
        }

        public override void OnClearComplete()
        {
            this.dictionary.Clear();
        }

        public override void OnInsertComplete(T value)
        {
            this.dictionary.Add(value, value);
        }

        public override void OnRemoveComplete(T value)
        {
            this.dictionary.Remove(value);
        }

        public override void OnSetComplete(T oldValue, T newValue)
        {
            this.dictionary.Remove(oldValue);
            this.dictionary.Add(newValue, newValue);
        }

        protected internal DXCollectionBase<T> Collection =>
            this.collection;

        protected internal Dictionary<T, T> Dictionary =>
            this.dictionary;

        public override DXCollectionUniquenessProviderType Type =>
            DXCollectionUniquenessProviderType.MaximizePerformance;
    }
}

