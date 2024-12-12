namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class ListSubscriber : IObjectListenerOwner
    {
        private const int SimultaneouslyChangedItemsThreshold = 1;
        private readonly IList source;
        private readonly ObjectListener rootListener;
        private readonly PropertyDescriptorProvider propertyDescriptorProvider;
        private readonly BindingListAdapter adapter;
        private readonly ItemPropertyNotificationMode itemPropertyNotificationMode;
        private readonly bool useSlidingSubscription;
        private ObservablePropertySchemeNode[] propertyScheme;
        private int slidingSubscriptionIndex;
        private CollectionChangedWeakEventHandler<ListSubscriber> collectionChangedHandler;
        private ListChangedWeakEventHandler<ListSubscriber> listChangedHandler;
        private ListSubscriber.PropertyChangeInfo lastPropertyChange;
        private bool lastPropertyChangeNotificationWasReset;

        public ListSubscriber(IList source, BindingListAdapter adapter, ItemPropertyNotificationMode itemPropertyNotificationMode, bool useSlidingSubscription);
        private static bool AreSamePropertyNodes(ObservablePropertySchemeNode first, ObservablePropertySchemeNode second);
        private static bool AreSamePropertySchemes(ObservablePropertySchemeNode[] first, ObservablePropertySchemeNode[] second);
        private void ClearCache();
        private ListSubscriber.PropertyChangeInfo CreatePropertyChangeInfo(object obj, string propertyName);
        private List<int> FindThresholdIndexes(ObjectListener listener, object obj);
        private PropertyDescriptor GetListChangeArgsPropertyDescriptor(ObjectListener listener, string propertyName);
        private void OnBindingListChanged(object sender, ListChangedEventArgs e);
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        public void OnErrorsChanged(ObjectListener listener, object obj, string propertyName);
        public void OnPropertyChanged(ObjectListener listener, object obj, string propertyName);
        public void OnPropertyChanging(ObjectListener listener, object obj, string propertyName);
        private void OnSimpleNotification(ObjectListener listener, object obj, string propertyName, Action<int> raiseNotification);
        private static bool OverflowsSimultaneouslyChangedItemsThreshold(List<int> indexes);
        private void ProcessPropertyChangedNotification(ObjectListener listener, object obj, string propertyName);
        private void ResetSlidingSubscriptionIndex();
        private void SubscribeToAllListItems();
        private void SubscribeToCollectionChangedEvent();
        private void SubscribeToNewValueOfChangedProperty(ObjectListener listener, object obj, string propertyName);
        private void SubscribeToRange(int startIndex, int endIndex);
        private void SubscribeToRealBindingList();
        private void UpdateSlidingSubscriptionIndex(NotifyCollectionChangedEventArgs e);
        public void UpdateSlidingSubscriptionIndex(int index);

        public ObservablePropertySchemeNode[] PropertyScheme { get; set; }

        private ObservablePropertySchemeNode[] ActualPropertyScheme { get; }

        private bool CanSubscribe { get; }

        private bool CanProcessItemChangeNotifications { get; }

        private CollectionChangedWeakEventHandler<ListSubscriber> CollectionChangedHandler { get; }

        private ListChangedWeakEventHandler<ListSubscriber> ListChangedHandler { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListSubscriber.<>c <>9;
            public static Action<ListSubscriber, object, NotifyCollectionChangedEventArgs> <>9__28_0;
            public static Action<ListSubscriber, object, ListChangedEventArgs> <>9__35_0;

            static <>c();
            internal void <get_CollectionChangedHandler>b__28_0(ListSubscriber owner, object o, NotifyCollectionChangedEventArgs e);
            internal void <get_ListChangedHandler>b__35_0(ListSubscriber owner, object o, ListChangedEventArgs e);
        }

        private class PropertyChangeInfo
        {
            private readonly object propertyOwner;
            private readonly string propertyName;
            private readonly object propertyValue;

            public PropertyChangeInfo(object obj, string propertyName, object propertyValue);
            public override bool Equals(object obj);
            public override int GetHashCode();

            public object PropertyOwner { get; }

            public string PropertyName { get; }

            public object PropertyValue { get; }
        }
    }
}

