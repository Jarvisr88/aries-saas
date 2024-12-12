namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IUIInteractionService : IUIService, IDisposable
    {
        void Activate(IView view);
        void Deactivate(IView view);
        void SetActiveItem(IView view, ILayoutElement element);
    }
}

