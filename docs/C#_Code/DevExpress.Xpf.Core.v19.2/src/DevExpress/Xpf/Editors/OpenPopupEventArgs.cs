namespace DevExpress.Xpf.Editors
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class OpenPopupEventArgs : RoutedEventArgs
    {
        public OpenPopupEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        [DefaultValue(false)]
        public bool Cancel { get; set; }
    }
}

