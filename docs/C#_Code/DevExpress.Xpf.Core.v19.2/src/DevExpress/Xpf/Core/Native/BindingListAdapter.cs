namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.ChunkList;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class BindingListAdapter : BindingListAdapterBase, ICancelAddNew, IListWrapper, IRefreshable, IListChanging
    {
        private static readonly PropertyChangedEventArgs EmptyEventArgs;
        private PropertyChangingWeakEventHandler<BindingListAdapter> propertyChangingHandler;
        private ObservablePropertySchemeNode[] observablePropertyScheme;
        private bool isNewItemRowEditing;
        private readonly BindingListAdapterSourceListeningSettings sourceListeningSettings;
        private ListSubscriber listSubscriptionManager;
        private readonly IBindingList innerBindingList;

        public event AllowSubscribeDependecyPropertyNotificationEventHandler AllowSubscribeDependecyPropertyNotification;

        internal event EventHandler<NotifyCollectionChangedEventArgs> BindingListAdapterDirectlyChanged;

        public event EventHandler<ErrorsChangedEventArgs> ErrorsChanged;

        public event ListChangingEventHandler ListChanging;

        static BindingListAdapter();
        internal BindingListAdapter(IList source, BindingListAdapterSourceListeningSettings sourceListeningSettings);
        internal BindingListAdapter(IList source, ItemPropertyNotificationMode itemPropertyNotificationMode = 1, bool forceRaiseCollectionChange = false);
        protected override void AddInternal(object obj);
        protected override void AddNewInternal();
        public void CancelNew(int itemIndex);
        protected override void ClearInternal();
        internal static ItemPropertyNotificationMode ConvertMode(ItemPropertyNotificationMode mode);
        internal static BindingListAdapter CreateFromList(IList list, BindingListAdapterSourceListeningSettings settings);
        public static BindingListAdapter CreateFromList(IList list, ItemPropertyNotificationMode itemPropertyNotificationMode = 1, bool forceRaiseCollectionChange = false);
        void IRefreshable.Refresh();
        public void EndNew(int itemIndex);
        private void EnumerateDependencyPropertyDescriptors(object item, Action<DependencyPropertyDescriptor> descriptorAction);
        private static Func<BindingListAdapterSourceListeningSettings, BindingListAdapter> GetBindingListAdapterCreator(IList list);
        protected override void InsertInternal(int index, object obj);
        protected override bool IsItemLoaded(int index);
        protected override void NotifyOnItemRemove(int oldIndex, int newIndex, object item);
        protected override void NotifyOnItemReplace(int startingIndex, object oldItem);
        protected override void OnIndexAccessed(int index);
        private void OnObjectDependencyPropertyChanged(object sender, EventArgs e);
        protected virtual void OnObjectPropertyChanging(object sender, PropertyChangingEventArgs e);
        private void OnObservablePropertySchemeChanged();
        protected override void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        private void RaiseBindingListAdapterDirectlyChanged(NotifyCollectionChangedEventArgs args);
        internal void RaiseErrorsChanged(ErrorsChangedEventArgs args);
        internal void RaiseItemChangedInternal(ListChangedEventArgs e);
        internal void RaiseItemChangingInternal(ListChangingEventArgs e);
        private void RaiseListChanging(ListChangingEventArgs args);
        protected override void RemoveInternal(object obj);
        protected override void RemovingAtInternal(int index);
        protected override void SubscribeItemPropertyChangedEvent(object item);
        protected override void UnsubscribeItemPropertyChangedEvent(object item);

        private PropertyChangingWeakEventHandler<BindingListAdapter> PropertyChangingHandler { get; }

        public ObservablePropertySchemeNode[] ObservablePropertyScheme { get; set; }

        private bool ShouldSubscribePropertyChanging { get; }

        protected override bool ShouldSubscribePropertiesChanged { get; }

        private bool ShouldSubscribeDependencyProperties { get; }

        private bool ForceRaiseCollectionChange { get; }

        Type IListWrapper.WrappedListType { get; }

        public override bool SupportsChangeNotification { get; }

        public override bool AllowNew { get; }

        public override bool AllowRemove { get; }

        public override bool AllowEdit { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BindingListAdapter.<>c <>9;
            public static Action<BindingListAdapter, object, PropertyChangingEventArgs> <>9__3_0;
            public static Action<IRefreshable> <>9__60_0;

            static <>c();
            internal void <DevExpress.Data.Helpers.IRefreshable.Refresh>b__60_0(IRefreshable x);
            internal void <get_PropertyChangingHandler>b__3_0(BindingListAdapter owner, object o, PropertyChangingEventArgs e);
        }
    }
}

