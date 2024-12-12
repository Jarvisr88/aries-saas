namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class BarItemLinksAsLogicalChildrenEnumerator : IEnumerator
    {
        public BarItemLinksAsLogicalChildrenEnumerator(ILinksHolder holder);
        bool IEnumerator.MoveNext();
        void IEnumerator.Reset();

        protected ILinksHolder Holder { get; set; }

        protected IEnumerator Enumerator { get; set; }

        object IEnumerator.Current { get; }
    }
}

