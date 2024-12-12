namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;

    public class SimpleEnumerator : IEnumerator
    {
        private IEnumerator en;
        private int index = -1;

        public SimpleEnumerator(IEnumerator en)
        {
            this.en = en;
        }

        public virtual bool MoveNext()
        {
            this.index++;
            return this.en.MoveNext();
        }

        public virtual void Reset()
        {
            this.en.Reset();
            this.index = -1;
        }

        public object Current =>
            this.en?.Current;

        internal int CurrentIndex =>
            this.index;
    }
}

