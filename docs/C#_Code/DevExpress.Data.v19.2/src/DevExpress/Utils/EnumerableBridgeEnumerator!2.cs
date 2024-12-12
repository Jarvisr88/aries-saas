namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class EnumerableBridgeEnumerator<T, Key> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private IEnumerator<Key> keysEnumerator;
        private Func<Key, T> cast;

        public EnumerableBridgeEnumerator(IEnumerator<Key> keysEnumerator, Func<Key, T> cast)
        {
            this.keysEnumerator = keysEnumerator;
            this.cast = cast;
        }

        public void Dispose()
        {
            this.keysEnumerator.Dispose();
        }

        public bool MoveNext() => 
            this.keysEnumerator.MoveNext();

        public void Reset()
        {
            this.keysEnumerator.Reset();
        }

        public T Current =>
            this.cast(this.keysEnumerator.Current);

        object IEnumerator.Current =>
            this.keysEnumerator.Current;
    }
}

