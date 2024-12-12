namespace DevExpress.ReportServer.IndexedCache
{
    using System;
    using System.Reflection;

    public interface ICache<T> : IDisposable
    {
        void Clear();
        bool EnlargeCapacity(int count);
        void Ensure(int[] indexes, Action<int[]> onCached);
        bool IsElementCached(int index);
        void RemoveAt(int index);

        T this[int index] { get; set; }

        int Capacity { get; }
    }
}

