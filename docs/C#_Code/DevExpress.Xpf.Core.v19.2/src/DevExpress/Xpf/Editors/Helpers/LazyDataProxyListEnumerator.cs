namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LazyDataProxyListEnumerator : IEnumerator<DataProxy>, IDisposable, IEnumerator
    {
        private readonly LazyDataProxyListWrapper wrapper;
        private int index;
        private DataProxy current;

        public LazyDataProxyListEnumerator(LazyDataProxyListWrapper wrapper)
        {
            this.wrapper = wrapper;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if ((this.index + 1) > this.wrapper.Count)
            {
                return false;
            }
            this.current = this.wrapper[this.index];
            this.index++;
            return true;
        }

        public void Reset()
        {
            this.current = null;
            this.index = 0;
        }

        object IEnumerator.Current =>
            this.current;

        public DataProxy Current =>
            this.current;
    }
}

