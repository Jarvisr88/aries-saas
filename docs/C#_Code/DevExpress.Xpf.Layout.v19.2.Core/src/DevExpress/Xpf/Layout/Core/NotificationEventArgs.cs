namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(DevExpress.Xpf.Layout.Core.Notification notification)
        {
            this.Notification = notification;
        }

        public DevExpress.Xpf.Layout.Core.Notification Notification { get; private set; }

        public object ActionTarget { get; set; }

        public DependencyProperty Property { get; set; }
    }
}

