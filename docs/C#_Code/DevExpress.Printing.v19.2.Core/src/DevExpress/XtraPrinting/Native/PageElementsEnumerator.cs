namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;

    public abstract class PageElementsEnumerator : IEnumerator
    {
        private IEnumerator pageEnumerator;
        private IEnumerator elementsEnumerator;

        protected PageElementsEnumerator(PageList pages);
        protected abstract IEnumerator GetPageElementsEnumerator();
        public bool MoveNext();
        private bool MoveNextPage();
        public void Reset();

        public DevExpress.XtraPrinting.Page Page { get; }

        public object Current { get; }
    }
}

