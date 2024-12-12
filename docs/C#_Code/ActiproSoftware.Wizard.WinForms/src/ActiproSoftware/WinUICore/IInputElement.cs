namespace ActiproSoftware.WinUICore
{
    using System;
    using System.Windows.Forms;

    public interface IInputElement
    {
        void CaptureMouse();
        void RaiseClickEvent(MouseEventArgs e);
        void RaiseDoubleClickEvent(MouseEventArgs e);
        void RaiseMouseDownEvent(MouseEventArgs e);
        void RaiseMouseEnterEvent(MouseEventArgs e);
        void RaiseMouseHoverEvent(MouseEventArgs e);
        void RaiseMouseLeaveEvent(MouseEventArgs e);
        void RaiseMouseMoveEvent(MouseEventArgs e);
        void RaiseMouseUpEvent(MouseEventArgs e);
        void RaiseMouseWheelEvent(MouseEventArgs e);
        void ReleaseMouseCapture();

        bool IsMouseCaptured { get; }

        bool IsMouseDirectlyOver { get; }
    }
}

