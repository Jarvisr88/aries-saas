namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ConvertEditValueEventArgs : RoutedEventArgs
    {
        public ConvertEditValueEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public System.Windows.Media.ImageSource ImageSource { get; internal set; }

        public object EditValue { get; set; }
    }
}

