namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;

    public static class DXNotificationCollectionAccessor
    {
        public static void OnCollectionChanged<T>(NotificationCollection<T> collection, CollectionChangedEventArgs<T> e)
        {
            collection.OnCollectionChanged(e);
        }

        public static void OnCollectionChanging<T>(NotificationCollection<T> collection, CollectionChangingEventArgs<T> e)
        {
            collection.OnCollectionChanging(e);
        }
    }
}

