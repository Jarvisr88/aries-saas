namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal interface IOrderedStorage<T> : IStorage<T>, IStorage<string, T>, IEnumerable<T>, IEnumerable
    {
        void ResetOrder(Func<T, int> getOrder);
    }
}

