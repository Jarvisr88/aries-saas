namespace DevExpress.Xpf.Layout.Core
{
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public interface IView : ILayoutElementHost, IBaseObject, IDisposable, IUIServiceProvider
    {
        bool CanHandleMouseDown();
        void InvalidateZOrder();
        void OnKeyDown(Key key);
        void OnKeyUp(Key key);
        void OnMouseCaptureLost();
        void OnMouseDown(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea);
        void OnMouseEvent(MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea = null);
        void OnMouseLeave();
        void OnMouseMove(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea);
        void OnMouseUp(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea);

        IViewAdapter Adapter { get; }

        int ZOrder { get; }
    }
}

