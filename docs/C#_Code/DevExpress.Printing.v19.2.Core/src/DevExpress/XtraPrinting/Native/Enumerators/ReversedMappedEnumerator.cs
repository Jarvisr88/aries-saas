namespace DevExpress.XtraPrinting.Native.Enumerators
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedMappedEnumerator : MappedIndexedEnumerator
    {
        public ReversedMappedEnumerator(IList<MapItem> map, IList items);
        public override bool MoveNext();
        protected override bool MoveNextMap();
        protected override void ResetMap();
    }
}

