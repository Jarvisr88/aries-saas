namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(DependencyObject sender, NotificationType notification)
        {
            this.Notification = notification;
            this.Sender = sender;
        }

        public NotificationType Notification { get; private set; }

        public DependencyObject Sender { get; private set; }
    }
}

