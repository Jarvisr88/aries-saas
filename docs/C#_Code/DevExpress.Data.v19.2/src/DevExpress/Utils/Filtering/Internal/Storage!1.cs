namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal sealed class Storage<T> : StorageBase<string, T>, IOrderedStorage<T>, IStorage<T>, IStorage<string, T>, IEnumerable<T>, IEnumerable
    {
        internal Storage(IEnumerable<T> elements, Func<T, int> getOrder);
        void IOrderedStorage<T>.ResetOrder(Func<T, int> getOrder);
        protected sealed override IEqualityComparer<string> GetPathComparer();
    }
}

