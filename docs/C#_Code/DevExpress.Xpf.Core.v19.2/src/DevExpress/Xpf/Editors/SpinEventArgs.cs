namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SpinEventArgs : RoutedEventArgs
    {
        public SpinEventArgs(bool isSpinUp)
        {
            base.RoutedEvent = TextEdit.SpinEvent;
            this.IsSpinUp = isSpinUp;
        }

        public bool IsSpinUp { get; set; }
    }
}

