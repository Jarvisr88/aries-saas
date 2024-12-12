namespace DevExpress.XtraPrinting.Native.Enumerators
{
    using System;
    using System.Collections;

    public class ReversedEnumerator : IndexedEnumerator
    {
        public ReversedEnumerator(IList items);
        public override bool MoveNext();
        protected override void ResetCore();
    }
}

