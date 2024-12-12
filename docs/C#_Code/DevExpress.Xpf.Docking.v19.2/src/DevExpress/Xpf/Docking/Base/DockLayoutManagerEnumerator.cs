namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections;

    public class DockLayoutManagerEnumerator : IEnumerator
    {
        private readonly IEnumerator PanelsEnumerator;

        public DockLayoutManagerEnumerator(DockLayoutManager manager)
        {
            this.PanelsEnumerator = Array.FindAll<BaseLayoutItem>(manager.GetItems(), new Predicate<BaseLayoutItem>(this.IsElementWithControl)).GetEnumerator();
        }

        private bool IsElementWithControl(BaseLayoutItem item)
        {
            ILayoutContent content = item as ILayoutContent;
            return ((content != null) && (content.Control != null));
        }

        bool IEnumerator.MoveNext() => 
            this.PanelsEnumerator.MoveNext();

        void IEnumerator.Reset()
        {
            this.PanelsEnumerator.Reset();
        }

        object IEnumerator.Current =>
            ((ILayoutContent) this.PanelsEnumerator.Current).Control;
    }
}

