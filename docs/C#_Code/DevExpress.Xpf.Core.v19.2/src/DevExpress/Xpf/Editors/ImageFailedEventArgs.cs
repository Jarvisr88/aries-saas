namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ImageFailedEventArgs : RoutedEventArgs
    {
        public ImageFailedEventArgs(RoutedEvent routedEvent, object sender, Exception errorException) : base(routedEvent, sender)
        {
            this.ErrorException = errorException;
        }

        public Exception ErrorException { get; protected set; }
    }
}

