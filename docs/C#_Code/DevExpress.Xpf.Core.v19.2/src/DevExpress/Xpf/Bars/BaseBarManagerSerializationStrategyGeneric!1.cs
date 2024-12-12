namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class BaseBarManagerSerializationStrategyGeneric<TOwner> : BaseBarManagerSerializationStrategy where TOwner: DependencyObject
    {
        private readonly TOwner owner;

        public BaseBarManagerSerializationStrategyGeneric(TOwner owner);
        private void AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo = false);
        protected virtual void OnAllowProperty(AllowPropertyEventArgs e);
        private void OnAllowPropertyEvent(object sender, AllowPropertyEventArgs e);
        protected virtual void OnBeforeLoadLayoutEvent(BeforeLoadLayoutEventArgs e);
        private void OnBeforeLoadLayoutEvent(object sender, BeforeLoadLayoutEventArgs e);
        protected virtual void OnClearCollection(XtraItemRoutedEventArgs e);
        private void OnClearCollectionEvent(object sender, XtraItemRoutedEventArgs e);
        protected virtual void OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e);
        private void OnCreateCollectionItemEvent(object sender, XtraCreateCollectionItemEventArgs e);
        protected virtual void OnCreateContentPropertyValue(XtraCreateContentPropertyValueEventArgs e);
        private void OnCreateContentPropertyValueEvent(object sender, XtraCreateContentPropertyValueEventArgs e);
        protected virtual void OnCustomGetSerializableChildren(CustomGetSerializableChildrenEventArgs e);
        private void OnCustomGetSerializableChildrenEvent(object sender, CustomGetSerializableChildrenEventArgs e);
        protected virtual void OnCustomGetSerializableProperties(CustomGetSerializablePropertiesEventArgs e);
        private void OnCustomGetSerializablePropertiesEvent(object sender, CustomGetSerializablePropertiesEventArgs e);
        protected virtual void OnCustomShouldSerializeProperty(CustomShouldSerializePropertyEventArgs e);
        private void OnCustomShouldSerializePropertyEvent(object sender, CustomShouldSerializePropertyEventArgs e);
        protected virtual void OnDeserializeProperty(XtraPropertyInfoEventArgs e);
        private void OnDeserializePropertyEvent(object sender, XtraPropertyInfoEventArgs e);
        protected virtual void OnEndDeserializing(EndDeserializingEventArgs e);
        private void OnEndDeserializingEvent(object sender, EndDeserializingEventArgs e);
        protected virtual void OnEndSerializing(RoutedEventArgs e);
        private void OnEndSerializingEvent(object sender, RoutedEventArgs e);
        protected virtual void OnFindCollectionItem(XtraFindCollectionItemEventArgs e);
        private void OnFindCollectionItemEvent(object sender, XtraFindCollectionItemEventArgs e);
        protected virtual void OnLayoutUpgrade(LayoutUpgradeEventArgs e);
        private void OnLayoutUpgradeEvent(object sender, LayoutUpgradeEventArgs e);
        protected virtual void OnResetProperty(ResetPropertyEventArgs e);
        private void OnResetPropertyEvent(object sender, ResetPropertyEventArgs e);
        protected virtual void OnShouldSerializeCollectionItem(XtraShouldSerailizeCollectionItemEventArgs e);
        private void OnShouldSerializeCollectionItemEvent(object sender, XtraShouldSerailizeCollectionItemEventArgs e);
        protected virtual void OnStartDeserializing(StartDeserializingEventArgs e);
        private void OnStartDeserializingEvent(object sender, StartDeserializingEventArgs e);
        protected virtual void OnStartSerializing(RoutedEventArgs e);
        private void OnStartSerializingEvent(object sender, RoutedEventArgs e);
        private void SubscribeEvents();

        protected TOwner Owner { get; }
    }
}

