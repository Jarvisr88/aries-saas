namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class DXCollection<T> : DXCollectionBase<T>
    {
        public DXCollection()
        {
        }

        protected DXCollection(DXCollectionUniquenessProviderType uniquenessProviderType) : base(uniquenessProviderType)
        {
        }

        protected internal DXCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        protected DXCollection(int capacity, DXCollectionUniquenessProviderType uniquenessProviderType) : base(capacity, uniquenessProviderType)
        {
        }

        protected override void SetItem(int index, T value)
        {
            if (!this.CanSet)
            {
                throw new InvalidOperationException("Can't set item for this collection. This collection is read-only.");
            }
            base.SetItem(index, value);
        }

        public virtual T this[int index] =>
            this.GetItem(index);

        protected virtual bool CanSet =>
            false;
    }
}

