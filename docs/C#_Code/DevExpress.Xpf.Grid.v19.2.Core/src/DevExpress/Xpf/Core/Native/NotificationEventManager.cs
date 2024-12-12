namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class NotificationEventManager : WeakEventManager
    {
        private NotificationType notification;

        private NotificationEventManager(NotificationType notification)
        {
            this.notification = notification;
        }

        public static bool AddListener(DependencyObject element, INotificationListener listener)
        {
            INotificationManager source = LayoutHelper.FindParentObject<INotificationManager>(element);
            if (source == null)
            {
                return false;
            }
            foreach (NotificationType type in listener.SupportedNotifications)
            {
                GetManager(type).ProtectedAddListener(source, listener);
            }
            return true;
        }

        private static NotificationEventManager GetManager(NotificationType notification)
        {
            Type type;
            if (notification == NotificationType.Data)
            {
                type = typeof(DataType);
            }
            else
            {
                if (notification != NotificationType.Layout)
                {
                    throw new NotImplementedException();
                }
                type = typeof(LayoutType);
            }
            NotificationEventManager currentManager = (NotificationEventManager) GetCurrentManager(type);
            if (currentManager == null)
            {
                currentManager = new NotificationEventManager(notification);
                SetCurrentManager(type, currentManager);
            }
            return currentManager;
        }

        private void OnRequireMeasure(object sender, EventArgs args)
        {
            base.DeliverEvent(sender, args);
        }

        public static void RemoveListener(INotificationManager manager, INotificationListener listener)
        {
            foreach (NotificationType type in listener.SupportedNotifications)
            {
                GetManager(type).ProtectedRemoveListener(manager, listener);
            }
        }

        protected override void StartListening(object source)
        {
            ((INotificationManager) source).SubscribeRequireMeasure(this.notification, new NotificationEventHandler(this.OnRequireMeasure));
        }

        protected override void StopListening(object source)
        {
            ((INotificationManager) source).UnsubscribeRequireMeasure(this.notification, new NotificationEventHandler(this.OnRequireMeasure));
        }

        private class DataType
        {
        }

        private class LayoutType
        {
        }
    }
}

