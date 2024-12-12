namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class ListEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private int index;
        private IList<T> list;

        public ListEnumerator(IList<T> list)
        {
            this.index = -1;
            this.list = list;
        }

        bool IEnumerator.MoveNext()
        {
            this.index++;
            return (this.index < this.list.Count);
        }

        void IEnumerator.Reset()
        {
            this.index = -1;
        }

        void IDisposable.Dispose()
        {
            this.list = null;
        }

        object IEnumerator.Current =>
            this.Current;

        T IEnumerator<T>.Current =>
            this.list[this.index];
    }
}

