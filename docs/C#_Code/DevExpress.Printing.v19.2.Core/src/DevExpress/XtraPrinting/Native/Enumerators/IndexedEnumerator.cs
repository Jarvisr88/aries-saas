namespace DevExpress.XtraPrinting.Native.Enumerators
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class IndexedEnumerator : IIndexedEnumerator, IEnumerator
    {
        protected IList items;

        public IndexedEnumerator(IList items);
        public virtual bool MoveNext();
        public void Reset();
        protected virtual void ResetCore();
        protected void UpdateCurrent();

        public int RealIndex { get; protected set; }

        public object Current { get; protected set; }
    }
}

