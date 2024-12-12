namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class DocumentBandCollection : Collection<DocumentBand>, IListWrapper<DocumentBand>, IEnumerable<DocumentBand>, IEnumerable
    {
        internal DocumentBand owner;

        public DocumentBandCollection(DocumentBand owner);
        public DocumentBandCollection(DocumentBand owner, List<DocumentBand> bands);
        private IIndexedEnumerator CreateEnumerator(DocumentBandKind kind);
        int IListWrapper<DocumentBand>.IndexOf(DocumentBand item);
        void IListWrapper<DocumentBand>.Insert(DocumentBand item, int index);
        public int FastIndexOf(DocumentBand band);
        public DocumentBand FindBand(DocumentBandKind kind, Predicate<DocumentBand> predicate);
        private DocumentBand FindBandCore(out int bandIndex, DocumentBandKind kind, Predicate<DocumentBand> predicate);
        private DocumentBandCollection.IndexedEnumerator GetIndexedEnumerator();
        protected override void InsertItem(int index, DocumentBand value);
        private void UpdateIndices(int startIndex);

        public DocumentBand Last { get; }

        public DocumentBand First { get; }

        public DocumentBand this[DocumentBandKind kind] { get; }

        public virtual float TotalHeight { get; }

        private class DetailBreakEnumerator : IIndexedEnumerator, IEnumerator
        {
            private IIndexedEnumerator en;

            public DetailBreakEnumerator(IIndexedEnumerator en);
            bool IEnumerator.MoveNext();
            void IEnumerator.Reset();

            public object Current { get; }

            public int RealIndex { get; }
        }

        private class IndexedEnumerator : IIndexedEnumerator, IEnumerator
        {
            private IEnumerator en;
            private int realIndex;

            public IndexedEnumerator(IEnumerator en);
            private void ResetIndex();
            bool IEnumerator.MoveNext();
            void IEnumerator.Reset();

            public object Current { get; }

            public int RealIndex { get; }
        }
    }
}

