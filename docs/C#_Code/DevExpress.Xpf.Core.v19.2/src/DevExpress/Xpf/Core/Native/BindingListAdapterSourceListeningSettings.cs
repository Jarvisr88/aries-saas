namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct BindingListAdapterSourceListeningSettings
    {
        private readonly DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode itemPropertyNotificationMode;
        private readonly bool forceRaiseCollectionChange;
        private readonly bool listenToComplexProperties;
        private readonly bool useSlidingSubscription;
        public DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode ItemPropertyNotificationMode { get; }
        public bool ForceRaiseCollectionChange { get; }
        public bool ListenToComplexProperties { get; }
        public bool UseSlidingSubscription { get; }
        public BindingListAdapterSourceListeningSettings(DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode itemPropertyNotificationMode, bool forceRaiseCollectionChange, bool listenToComplexProperties, bool useSlidingSubscription);
        public bool ShouldSubscribePropertyChanged();
        public bool ShouldSubscribePropertyChanging();
        public bool ShouldSubscribePropertiesChanged();
        public bool ShouldSubscribeDependencyProperties();
        internal bool UseListSubscriber();
    }
}

