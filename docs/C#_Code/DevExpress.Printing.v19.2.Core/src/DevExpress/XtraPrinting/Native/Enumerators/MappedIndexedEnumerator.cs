namespace DevExpress.XtraPrinting.Native.Enumerators
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MappedIndexedEnumerator : IndexedEnumerator
    {
        protected IList<MapItem> map;
        protected int mapIndex;

        public MappedIndexedEnumerator(IList<MapItem> map, IList items);
        public override bool MoveNext();
        protected virtual bool MoveNextMap();
        protected override void ResetCore();
        protected virtual void ResetMap();

        public Predicate<RectangleF> Predicate { get; set; }
    }
}

