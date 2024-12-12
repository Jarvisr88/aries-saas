namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class EmptyEnumerable<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
    {
        public static readonly EmptyEnumerable<T> Instance;

        static EmptyEnumerable();
        private EmptyEnumerable();
        public void Dispose();
        public IEnumerator<T> GetEnumerator();
        public bool MoveNext();
        public void Reset();
        IEnumerator IEnumerable.GetEnumerator();

        public T Current { get; }

        object IEnumerator.Current { get; }
    }
}

