namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IContextActionService : IUIService, IDisposable
    {
        bool CanExecute(IView view, ILayoutElement element, ContextAction action);
        bool Execute(IView view, ILayoutElement element, ContextAction action);
    }
}

