namespace DevExpress.Services
{
    using System;
    using System.Windows.Forms;

    public interface IMouseHandlerServiceEx : IMouseHandlerService
    {
        void OnMouseDoubleClick(MouseEventArgs e);
    }
}

