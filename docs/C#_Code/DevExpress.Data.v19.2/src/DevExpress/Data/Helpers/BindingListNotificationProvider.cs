namespace DevExpress.Data.Helpers
{
    using System;
    using System.ComponentModel;

    public class BindingListNotificationProvider : INotificationProvider, IDisposable
    {
        private IBindingList list;

        public INotificationProvider Clone(object list);
        bool INotificationProvider.IsSupportNotifications(object list);
        void INotificationProvider.SubscribeNotifications(ListChangedEventHandler handler);
        void INotificationProvider.UnsubscribeNotifications(ListChangedEventHandler handler);
        public virtual void Dispose();
    }
}

