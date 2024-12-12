namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Interop;

    public class Win32WindowWrapper : System.Windows.Forms.IWin32Window
    {
        public Win32WindowWrapper(IntPtr handle)
        {
            this.Handle = handle;
        }

        public Win32WindowWrapper(Window window)
        {
            if (window.IsLoaded)
            {
                this.Handle = new WindowInteropHelper(window).Handle;
            }
        }

        public IntPtr Handle { get; private set; }
    }
}

