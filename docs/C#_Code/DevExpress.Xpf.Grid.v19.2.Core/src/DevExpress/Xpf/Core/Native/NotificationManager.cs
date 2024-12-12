namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class NotificationManager : DependencyObject, INotificationManager
    {
        private Delegate[] delegates = new Delegate[EnumHelper.GetEnumCount(typeof(NotificationType))];
        protected object owner;

        public NotificationManager(object owner)
        {
            this.owner = owner;
        }

        void INotificationManager.AcceptNotification(DependencyObject sender, NotificationType notification)
        {
            if (this.delegates[(int) notification] != null)
            {
                ((NotificationEventHandler) this.delegates[(int) notification])(this.owner, new NotificationEventArgs(sender, notification));
            }
        }

        void INotificationManager.SubscribeRequireMeasure(NotificationType notification, NotificationEventHandler eventHandler)
        {
            this.delegates[(int) notification] += eventHandler;
        }

        void INotificationManager.UnsubscribeRequireMeasure(NotificationType notification, NotificationEventHandler eventHandler)
        {
            this.delegates[(int) notification] -= eventHandler;
        }
    }
}

