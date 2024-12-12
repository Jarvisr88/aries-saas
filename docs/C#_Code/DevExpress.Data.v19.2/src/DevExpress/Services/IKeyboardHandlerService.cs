namespace DevExpress.Services
{
    using System;
    using System.Windows.Forms;

    public interface IKeyboardHandlerService
    {
        void OnKeyDown(KeyEventArgs e);
        void OnKeyPress(KeyPressEventArgs e);
        void OnKeyUp(KeyEventArgs e);
    }
}

