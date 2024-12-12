namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public class NotifyCollectionChangedProvider : INotificationProvider, IDisposable
    {
        private IList list;
        private bool supportPropertiesChanged;
        private List<NotifyCollectionChangedProvider.EventInfo> events;
        private int lastChangedIndex;
        private PropertyDescriptorCollection itemTypeProperties;

        public NotifyCollectionChangedProvider();
        private ListChangedType ConvertType(NotifyCollectionChangedAction action);
        INotificationProvider INotificationProvider.Clone(object list);
        bool INotificationProvider.IsSupportNotifications(object list);
        void INotificationProvider.SubscribeNotifications(ListChangedEventHandler handler);
        void INotificationProvider.UnsubscribeNotifications(ListChangedEventHandler handler);
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e);
        private void SubscribePropertiesChanged(IList items, bool unsubscribeOnly);
        private void SubscribePropertiesChanged(NotifyCollectionChangedEventArgs e, IList items);
        void IDisposable.Dispose();

        public bool SupportPropertiesChanged { get; set; }

        private class EventInfo
        {
            public NotifyCollectionChangedEventHandler CCHandler;
            public ListChangedEventHandler Handler;
        }
    }
}

