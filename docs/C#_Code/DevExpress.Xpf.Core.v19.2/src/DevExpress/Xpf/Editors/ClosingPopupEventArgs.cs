namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ClosingPopupEventArgs : ClosePopupEventArgs
    {
        public ClosingPopupEventArgs(RoutedEvent closeEvent, PopupCloseMode mode, object value) : base(closeEvent, mode, value)
        {
        }

        public bool Cancel { get; set; }
    }
}

