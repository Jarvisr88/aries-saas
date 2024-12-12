namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class LayoutNotificationHelper : INotificationListener, IWeakEventListener
    {
        private bool subscribed;
        protected ILayoutNotificationHelperOwner owner;
        private DependencyObject manager;

        public LayoutNotificationHelper(ILayoutNotificationHelperOwner owner)
        {
            this.owner = owner;
        }

        protected virtual void ReceiveNotification(object sender, NotificationType notification)
        {
            if (notification == NotificationType.Layout)
            {
                this.owner.InvalidateMeasure();
                ItemsControlBase owner = this.owner as ItemsControlBase;
                if ((owner != null) && (owner.Panel != null))
                {
                    owner.Panel.InvalidateMeasure();
                }
            }
        }

        public void Subscribe()
        {
            if (!this.subscribed || !ReferenceEquals(this.manager, this.owner.NotificationManager))
            {
                this.manager = this.owner.NotificationManager;
                this.subscribed = NotificationEventManager.AddListener(this.manager, this);
            }
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(NotificationEventManager)))
            {
                return false;
            }
            NotificationEventArgs args = (NotificationEventArgs) e;
            this.ReceiveNotification(args.Sender, args.Notification);
            return true;
        }

        public void Unsubscribe()
        {
            NotificationEventManager.RemoveListener(this.manager as INotificationManager, this);
            this.subscribed = false;
        }

        IEnumerable<NotificationType> INotificationListener.SupportedNotifications =>
            this.SupportedNotificationsCore;

        protected virtual IEnumerable<NotificationType> SupportedNotificationsCore =>
            new NotificationType[] { NotificationType.Layout };
    }
}

