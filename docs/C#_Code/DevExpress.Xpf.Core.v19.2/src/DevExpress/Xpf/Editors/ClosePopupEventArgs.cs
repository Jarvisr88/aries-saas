namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ClosePopupEventArgs : RoutedEventArgs
    {
        public ClosePopupEventArgs(RoutedEvent closeEvent, PopupCloseMode mode, object value)
        {
            base.RoutedEvent = closeEvent;
            this.CloseMode = mode;
            this.EditValue = value;
        }

        public PopupCloseMode CloseMode { get; internal set; }

        public object EditValue { get; internal set; }
    }
}

