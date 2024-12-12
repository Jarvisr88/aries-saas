namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public interface INotificationManager
    {
        void AcceptNotification(DependencyObject sender, NotificationType notification);
        void SubscribeRequireMeasure(NotificationType notification, NotificationEventHandler eventHandler);
        void UnsubscribeRequireMeasure(NotificationType notification, NotificationEventHandler eventHandler);
    }
}

