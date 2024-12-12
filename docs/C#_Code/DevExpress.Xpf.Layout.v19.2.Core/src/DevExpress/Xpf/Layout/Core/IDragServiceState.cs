namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public interface IDragServiceState
    {
        void ProcessCancel(IView view);
        void ProcessComplete(IView view);
        void ProcessKeyDown(IView view, Key key);
        void ProcessKeyUp(IView view, Key key);
        void ProcessMouseDown(IView view, Point point);
        void ProcessMouseMove(IView view, Point point);
        void ProcessMouseUp(IView view, Point point);

        DevExpress.Xpf.Layout.Core.OperationType OperationType { get; }
    }
}

