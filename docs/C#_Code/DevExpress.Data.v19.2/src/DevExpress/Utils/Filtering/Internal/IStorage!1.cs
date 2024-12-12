namespace DevExpress.Utils.Filtering.Internal
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IStorage<T> : IStorage<string, T>, IEnumerable<T>, IEnumerable
    {
    }
}

