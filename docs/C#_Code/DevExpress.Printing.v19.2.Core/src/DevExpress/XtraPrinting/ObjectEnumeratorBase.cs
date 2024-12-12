namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;

    public class ObjectEnumeratorBase : IEnumerator
    {
        private IList objs;
        private IEnumerator en;

        public ObjectEnumeratorBase(IList objs)
        {
            this.objs = objs;
            this.en = objs.GetEnumerator();
        }

        public virtual bool MoveNext() => 
            this.en.MoveNext();

        public virtual void Reset()
        {
            this.en.Reset();
        }

        public object Current =>
            this.en.Current;
    }
}

