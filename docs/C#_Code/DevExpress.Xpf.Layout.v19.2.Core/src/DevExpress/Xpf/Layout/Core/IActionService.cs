namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IActionService : IUIService, IDisposable
    {
        void Collapse(IView view);
        void Expand(IView view);
        void Hide(IView view, bool immediately);
        void HideSelection(IView view);
        void ShowSelection(IView view);
    }
}

