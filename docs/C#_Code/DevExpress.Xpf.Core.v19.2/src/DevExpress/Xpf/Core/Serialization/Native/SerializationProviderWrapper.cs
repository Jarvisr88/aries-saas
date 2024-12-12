namespace DevExpress.Xpf.Core.Serialization.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal sealed class SerializationProviderWrapper : IDXSerializable, IXtraSerializable
    {
        public const string PathSeparator = "$";
        private readonly DependencyObject SourceObject;
        private readonly SerializationProvider Provider;
        private readonly string pathCore;

        public SerializationProviderWrapper(DependencyObject dObj, SerializationProvider provider, IEnumerable<string> parentIDs)
        {
            this.SourceObject = dObj;
            this.Provider = provider;
            string[] second = new string[] { this.GetSerializationID() };
            IEnumerable<string> source = parentIDs.Concat<string>(second);
            this.pathCore = string.Join("$", source.ToArray<string>());
        }

        public bool AllowProperty(object obj, PropertyDescriptor prop, int id, bool isSerializing) => 
            this.Provider.OnAllowProperty(new AllowPropertyEventArgs(id, isSerializing, prop, obj));

        public bool CustomDeserializeProperty(object obj, PropertyDescriptor property, XtraPropertyInfo propertyInfo) => 
            this.Provider.CustomDeserializeProperty(new XtraPropertyInfoEventArgs(DXSerializer.DeserializePropertyEvent, obj, property, propertyInfo));

        public bool? CustomShouldSerializeProperty(object obj, PropertyDescriptor prop) => 
            this.Provider.OnCustomShouldSerializeProperty(new CustomShouldSerializePropertyEventArgs(prop, obj));

        void IXtraSerializable.OnEndDeserializing(string restoredVersion)
        {
            this.Provider.OnEndDeserializing(this.Provider.GetEventTarget(this.EventTarget), restoredVersion);
        }

        void IXtraSerializable.OnEndSerializing()
        {
            this.Provider.OnEndSerializing(this.Provider.GetEventTarget(this.EventTarget));
        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs ea)
        {
            this.Provider.OnStartDeserializing(this.Provider.GetEventTarget(this.EventTarget), ea);
        }

        void IXtraSerializable.OnStartSerializing()
        {
            this.Provider.OnStartSerializing(this.Provider.GetEventTarget(this.EventTarget));
        }

        private string GetSerializationID() => 
            this.Provider.GetSerializationID(this.SourceObject);

        public void OnClearCollection(XtraItemEventArgs e)
        {
            this.Provider.OnClearCollection(new XtraClearCollectionEventArgs(this.Provider.GetEventTarget(this.EventTarget), e));
        }

        public object OnCreateCollectionItem(string propertyName, XtraItemEventArgs e) => 
            this.Provider.OnCreateCollectionItem(new XtraCreateCollectionItemEventArgs(this.Provider.GetEventTarget(this.EventTarget), propertyName, e));

        public object OnCreateContentPropertyValue(PropertyDescriptor propertyDescriptor, XtraItemEventArgs e) => 
            this.Provider.OnCreateContentPropertyValue(new XtraCreateContentPropertyValueEventArgs(this.Provider.GetEventTarget(this.EventTarget), propertyDescriptor, e));

        public object OnFindCollectionItem(string propertyName, XtraItemEventArgs e) => 
            this.Provider.OnFindCollectionItem(new XtraFindCollectionItemEventArgs(this.Provider.GetEventTarget(this.EventTarget), propertyName, e));

        public bool OnShouldSerailizaCollectionItem(XtraItemEventArgs e, object item) => 
            this.Provider.OnShouldSerializeCollectionItem(new XtraShouldSerailizeCollectionItemEventArgs(this.Provider.GetEventTarget(this.EventTarget), e, item));

        public bool OnShouldSerializeProperty(object obj, PropertyDescriptor prop, XtraSerializableProperty xtraSerializableProperty) => 
            this.Provider.OnShouldSerializeProperty(obj, prop, xtraSerializableProperty);

        public void ResetProperty(object obj, PropertyDescriptor prop, XtraSerializableProperty attr)
        {
            XtraResetPropertyAttribute defaultInstance = prop.Attributes[typeof(XtraResetPropertyAttribute)] as XtraResetPropertyAttribute;
            if ((defaultInstance == null) && (attr.Visibility == XtraSerializationVisibility.Visible))
            {
                defaultInstance = XtraResetPropertyAttribute.DefaultInstance;
            }
            if (defaultInstance != null)
            {
                this.Provider.OnResetProperty(new ResetPropertyEventArgs(defaultInstance.PropertyResetType, prop, obj));
            }
        }

        DependencyObject IDXSerializable.Source =>
            this.Provider.GetSource(this.SourceObject);

        string IDXSerializable.FullPath =>
            this.pathCore;

        public object EventTarget { get; set; }
    }
}

