namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class PartiallyDeserializedPageListEnumerator : IEnumerator<Page>, IDisposable, IEnumerator
    {
        private int currentIndex = -1;
        private IList<Page> list;

        public PartiallyDeserializedPageListEnumerator(IList<Page> list)
        {
            this.list = list;
        }

        public void Dispose()
        {
            this.list = null;
        }

        public bool MoveNext()
        {
            int num = this.currentIndex + 1;
            this.currentIndex = num;
            return (num < this.list.Count);
        }

        public void Reset()
        {
            this.currentIndex = -1;
        }

        public Page Current =>
            this.list[this.currentIndex];

        object IEnumerator.Current =>
            this.list[this.currentIndex];
    }
}

