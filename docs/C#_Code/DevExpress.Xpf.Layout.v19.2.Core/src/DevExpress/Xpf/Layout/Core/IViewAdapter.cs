namespace DevExpress.Xpf.Layout.Core
{
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public interface IViewAdapter : ILayoutElementHostAdapter, IDisposable
    {
        IView GetBehindView(IView sourceView, Point screenPoint);
        Point GetBehindViewPoint(IView source, IView behindView, Point point);
        IView GetView(ILayoutElement element);
        IView GetView(object rootKey);
        IView GetView(Point screenPoint);
        void ProcessAction(ViewAction action);
        void ProcessAction(IView view, ViewAction action);
        void ProcessKey(IView view, KeyEventType eventype, Key key);
        void ProcessMouseEvent(IView view, MouseEventType eventype, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea);

        ViewCollection Views { get; }

        object NotificationSource { get; }

        IDragService DragService { get; }

        ISelectionService SelectionService { get; }

        IUIInteractionService UIInteractionService { get; }

        IActionService ActionService { get; }

        IContextActionService ContextActionService { get; }

        bool IsInEvent { get; }
    }
}

