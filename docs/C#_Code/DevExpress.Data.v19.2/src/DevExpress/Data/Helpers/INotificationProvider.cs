namespace DevExpress.Data.Helpers
{
    using System;
    using System.ComponentModel;

    public interface INotificationProvider : IDisposable
    {
        INotificationProvider Clone(object list);
        bool IsSupportNotifications(object list);
        void SubscribeNotifications(ListChangedEventHandler handler);
        void UnsubscribeNotifications(ListChangedEventHandler handler);
    }
}

