namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface ISelectionServiceListener : IUIServiceListener
    {
        SelectionMode CheckMode(ILayoutElement item);
        void OnProcessSelection(ILayoutElement element);
        ILayoutElement[] OnRequestSelectionRange(ILayoutElement first, ILayoutElement last);
        void OnSelectionChanged(ILayoutElement element, bool selected);
        bool OnSelectionChanging(ILayoutElement element, bool selected);
    }
}

