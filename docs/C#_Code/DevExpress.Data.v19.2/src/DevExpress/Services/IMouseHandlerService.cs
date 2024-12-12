namespace DevExpress.Services
{
    using System;
    using System.Windows.Forms;

    public interface IMouseHandlerService
    {
        void OnMouseDown(MouseEventArgs e);
        void OnMouseMove(MouseEventArgs e);
        void OnMouseUp(MouseEventArgs e);
        void OnMouseWheel(MouseEventArgs e);
    }
}

