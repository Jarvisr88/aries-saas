namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface ISelectionService : IUIService, IDisposable
    {
        void ClearSelection(IView view);
        bool ExtendSelectionToParent(IView view);
        bool GetSelectedState(ILayoutElement element);
        void Select(IView view, ILayoutElement element, SelectionMode mode);
    }
}

