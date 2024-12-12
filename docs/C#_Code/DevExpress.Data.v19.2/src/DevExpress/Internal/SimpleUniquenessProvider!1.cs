namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;

    public class SimpleUniquenessProvider<T> : DXCollectionUniquenessProvider<T>
    {
        private readonly DXCollectionBase<T> collection;

        public SimpleUniquenessProvider(DXCollectionBase<T> collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
        }

        public override bool LookupObject(T value) => 
            this.collection.Contains(value);

        public override int LookupObjectIndex(T value) => 
            this.collection.IndexOf(value);

        public override void OnClearComplete()
        {
        }

        public override void OnInsertComplete(T value)
        {
        }

        public override void OnRemoveComplete(T value)
        {
        }

        public override void OnSetComplete(T oldValue, T newValue)
        {
        }

        protected internal DXCollectionBase<T> Collection =>
            this.collection;

        public override DXCollectionUniquenessProviderType Type =>
            DXCollectionUniquenessProviderType.MinimizeMemoryUsage;
    }
}

