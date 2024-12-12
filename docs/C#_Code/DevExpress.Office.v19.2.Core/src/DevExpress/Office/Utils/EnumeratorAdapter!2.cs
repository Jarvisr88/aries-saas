namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class EnumeratorAdapter<T, U> : IEnumerator<T>, IDisposable, IEnumerator where U: T
    {
        private IEnumerator<U> innerEnumerator;

        public EnumeratorAdapter(IEnumerator<U> enumerator)
        {
            this.innerEnumerator = enumerator;
        }

        public void Dispose()
        {
            this.innerEnumerator.Dispose();
        }

        public bool MoveNext() => 
            this.innerEnumerator.MoveNext();

        public void Reset()
        {
            this.innerEnumerator.Reset();
        }

        public T Current =>
            this.innerEnumerator.Current;

        object IEnumerator.Current =>
            this.Current;
    }
}

