namespace DevExpress.Xpf.Layout.Core.Selection
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class SelectionListener : ISelectionServiceListener, IUIServiceListener
    {
        public virtual SelectionMode CheckMode(ILayoutElement item) => 
            SelectionMode.SingleItem;

        public virtual void OnProcessSelection(ILayoutElement element)
        {
        }

        public virtual ILayoutElement[] OnRequestSelectionRange(ILayoutElement first, ILayoutElement last)
        {
            ILayoutElement element = ((first == null) || (first.Parent == null)) ? last.Parent : first.Parent;
            if (element != null)
            {
                return SelectionHelper.GetSelectionRange<ILayoutElement>(element.Nodes, first, last);
            }
            return new ILayoutElement[] { last };
        }

        public virtual void OnSelectionChanged(ILayoutElement element, bool selected)
        {
        }

        public virtual bool OnSelectionChanging(ILayoutElement element, bool selected) => 
            true;

        public IUIServiceProvider ServiceProvider { get; set; }

        public object Key =>
            typeof(ISelectionServiceListener);
    }
}

