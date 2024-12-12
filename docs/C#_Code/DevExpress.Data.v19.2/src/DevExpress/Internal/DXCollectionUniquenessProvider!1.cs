namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;

    public abstract class DXCollectionUniquenessProvider<T>
    {
        protected DXCollectionUniquenessProvider()
        {
        }

        public abstract bool LookupObject(T value);
        public abstract int LookupObjectIndex(T value);
        public abstract void OnClearComplete();
        public abstract void OnInsertComplete(T value);
        public abstract void OnRemoveComplete(T value);
        public abstract void OnSetComplete(T oldValue, T newValue);

        public abstract DXCollectionUniquenessProviderType Type { get; }
    }
}

