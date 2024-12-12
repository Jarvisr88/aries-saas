namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections;

    public class LogicalElementsEnumerator : IEnumerator
    {
        private readonly IEnumerator LogicalEnumerator;

        public LogicalElementsEnumerator(DockLayoutManager manager)
        {
            object[] objArray1 = new object[] { manager.DockHintsContainer };
            this.LogicalEnumerator = objArray1.GetEnumerator();
        }

        bool IEnumerator.MoveNext() => 
            this.LogicalEnumerator.MoveNext();

        void IEnumerator.Reset()
        {
            this.LogicalEnumerator.Reset();
        }

        object IEnumerator.Current =>
            this.LogicalEnumerator.Current;
    }
}

