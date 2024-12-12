namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ObjectListener
    {
        private readonly IObjectListenerOwner owner;
        private readonly PropertyDescriptor originationDescriptor;
        private readonly PropertyDescriptor[] descriptorChain;
        private readonly string descriptorChainName;
        private readonly ItemPropertyNotificationMode itemPropertyNotificationMode;
        private readonly PropertyDescriptorProvider descriptorProvider;
        private List<ObjectListener> children;
        private PropertyChangedWeakEventHandler<ObjectListener> propertyChangedHandler;
        private PropertyChangingWeakEventHandler<ObjectListener> propertyChangingHandler;
        private NotifyDataErrorInfoWeakEventHandler<ObjectListener> errorsChangedEventHandler;
        private bool allowCurrentLevelSubscription;
        private ConditionalWeakTable<object, List<int>> indexCache;

        public ObjectListener(IObjectListenerOwner owner, ItemPropertyNotificationMode itemPropertyNotificationMode);
        public ObjectListener(IObjectListenerOwner owner, ItemPropertyNotificationMode itemPropertyNotificationMode, PropertyDescriptorProvider propertyDescriptorProvider);
        private ObjectListener(IObjectListenerOwner owner, PropertyDescriptor originationDescriptor, ObjectListener originationObjectListener, ItemPropertyNotificationMode itemPropertyNotificationMode, PropertyDescriptorProvider descriptorProvider);
        internal void AddCachedIndex(object obj, List<int> indexes);
        private static PropertyDescriptor[] CreateDescriptorChain(PropertyDescriptor originationDescriptor, ObjectListener originationObjectListener);
        private static string CreateDescriptorChainName(PropertyDescriptor[] descriptorChain);
        public ObjectListener FindChildListener(string propertyName);
        public ObservablePropertySchemeNode FindNode(ObservablePropertySchemeNode[] scheme, string propertyName);
        public ObservablePropertySchemeNode[] FindNodes(ObservablePropertySchemeNode[] scheme);
        public object FindObservedObjectByDirectAncestor(object directAncestor);
        public object FindObservedObjectByRoot(object root);
        private ObjectListener FindOrCreateChildListener(string propertyName, PropertyDescriptorCollection descriptors);
        private static ObservablePropertySchemeNode FlatFindNode(ObservablePropertySchemeNode[] nodes, string propertyName);
        private void FlatResubscribe(object obj);
        private void FlatUnsubscribe(object obj);
        public string GetDescriptorName(string propertyName);
        private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e);
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
        private void OnPropertyChanging(object sender, PropertyChangingEventArgs e);
        internal void ResetIndexCache();
        public void Subscribe(object obj, ObservablePropertySchemeNode[] scheme);
        internal List<int> TryGetCachedIndexes(object obj);
        public void Unsubscribe(object obj);

        private List<ObjectListener> Children { get; set; }

        private PropertyChangedWeakEventHandler<ObjectListener> PropertyChangedHandler { get; }

        private PropertyChangingWeakEventHandler<ObjectListener> PropertyChangingHandler { get; }

        private NotifyDataErrorInfoWeakEventHandler<ObjectListener> ErrorsChangedEventHandler { get; }

        public bool AllowCurrentLevelSubscription { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ObjectListener.<>c <>9;
            public static Action<ObjectListener, object, PropertyChangedEventArgs> <>9__12_0;
            public static Action<ObjectListener, object, PropertyChangingEventArgs> <>9__15_0;
            public static Action<ObjectListener, object, DataErrorsChangedEventArgs> <>9__18_0;

            static <>c();
            internal void <get_ErrorsChangedEventHandler>b__18_0(ObjectListener o, object s, DataErrorsChangedEventArgs e);
            internal void <get_PropertyChangedHandler>b__12_0(ObjectListener o, object s, PropertyChangedEventArgs e);
            internal void <get_PropertyChangingHandler>b__15_0(ObjectListener o, object s, PropertyChangingEventArgs e);
        }
    }
}

