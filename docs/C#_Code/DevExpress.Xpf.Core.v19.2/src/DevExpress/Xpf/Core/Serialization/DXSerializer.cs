namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class DXSerializer
    {
        public static readonly DependencyProperty EnabledProperty;
        public static readonly DependencyProperty EnableSubtreeProperty;
        public static readonly DependencyProperty SerializationIDProperty;
        public static readonly DependencyProperty SerializationIDDefaultProperty;
        public static readonly DependencyProperty SerializationProviderProperty;
        public static readonly DependencyProperty StoreLayoutModeProperty;
        public static readonly DependencyProperty LayoutVersionProperty;
        private static readonly DependencyPropertyKey IsProcessingSingleObjectPropertyKey;
        public static readonly DependencyProperty IsProcessingSingleObjectProperty;
        private static readonly DependencyPropertyKey IsCustomSerializableChildPropertyKey;
        public static readonly DependencyProperty IsCustomSerializableChildProperty;
        public static readonly RoutedEvent StartSerializingEvent;
        public static readonly RoutedEvent EndSerializingEvent;
        public static readonly RoutedEvent StartDeserializingEvent;
        public static readonly RoutedEvent EndDeserializingEvent;
        public static readonly RoutedEvent CustomGetSerializableChildrenEvent;
        public static readonly RoutedEvent CustomGetSerializablePropertiesEvent;
        public static readonly RoutedEvent ClearCollectionEvent;
        public static readonly RoutedEvent CreateCollectionItemEvent;
        public static readonly RoutedEvent FindCollectionItemEvent;
        public static readonly RoutedEvent AllowPropertyEvent;
        public static readonly RoutedEvent CustomShouldSerializePropertyEvent;
        public static readonly RoutedEvent ResetPropertyEvent;
        public static readonly RoutedEvent DeserializePropertyEvent;
        public static readonly RoutedEvent CreateContentPropertyValueEvent;
        public static readonly RoutedEvent BeforeLoadLayoutEvent;
        public static readonly RoutedEvent LayoutUpgradeEvent;
        public static readonly RoutedEvent ShouldSerializeCollectionItemEvent;
        private static IList<DependencyObject> acceptedObjects = new List<DependencyObject>();
        private static IList<DependencyObject> customSerializableChildren = new List<DependencyObject>();

        static DXSerializer()
        {
            Type ownerType = typeof(DXSerializer);
            EnabledProperty = DependencyPropertyManager.RegisterAttached("Enabled", typeof(bool), ownerType, new UIPropertyMetadata(true));
            EnableSubtreeProperty = DependencyPropertyManager.RegisterAttached("EnableSubtree", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));
            SerializationIDProperty = DependencyPropertyManager.RegisterAttached("SerializationID", typeof(string), ownerType, new UIPropertyMetadata(null, new PropertyChangedCallback(DXSerializer.OnSerializationIDChanged)));
            SerializationIDDefaultProperty = DependencyPropertyManager.RegisterAttached("SerializationIDDefault", typeof(string), ownerType, new UIPropertyMetadata(null, new PropertyChangedCallback(DXSerializer.OnSerializationIDChanged)));
            SerializationProviderProperty = DependencyPropertyManager.RegisterAttached("SerializationProvider", typeof(SerializationProvider), ownerType, new UIPropertyMetadata(SerializationProvider.Instance));
            StoreLayoutModeProperty = DependencyPropertyManager.RegisterAttached("StoreLayoutMode", typeof(StoreLayoutMode), ownerType, new UIPropertyMetadata(StoreLayoutMode.UI));
            LayoutVersionProperty = DependencyPropertyManager.RegisterAttached("LayoutVersion", typeof(string), ownerType, new UIPropertyMetadata(null));
            IsProcessingSingleObjectPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("IsProcessingSingleObject", typeof(bool), ownerType, new UIPropertyMetadata(false));
            IsProcessingSingleObjectProperty = IsProcessingSingleObjectPropertyKey.DependencyProperty;
            IsCustomSerializableChildPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("IsCustomSerializableChild", typeof(bool), ownerType, new UIPropertyMetadata(false));
            IsCustomSerializableChildProperty = IsCustomSerializableChildPropertyKey.DependencyProperty;
            StartSerializingEvent = EventManager.RegisterRoutedEvent("StartSerializing", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            EndSerializingEvent = EventManager.RegisterRoutedEvent("EndSerializing", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            StartDeserializingEvent = EventManager.RegisterRoutedEvent("StartDeserializing", RoutingStrategy.Direct, typeof(StartDeserializingEventHandler), ownerType);
            EndDeserializingEvent = EventManager.RegisterRoutedEvent("EndDeserializing", RoutingStrategy.Direct, typeof(EndDeserializingEventHandler), ownerType);
            CustomGetSerializablePropertiesEvent = EventManager.RegisterRoutedEvent("CustomGetSerializableProperties", RoutingStrategy.Direct, typeof(CustomGetSerializablePropertiesEventHandler), ownerType);
            CustomGetSerializableChildrenEvent = EventManager.RegisterRoutedEvent("CustomGetSerializableChildren", RoutingStrategy.Direct, typeof(CustomGetSerializableChildrenEventHandler), ownerType);
            ClearCollectionEvent = EventManager.RegisterRoutedEvent("ClearCollection", RoutingStrategy.Direct, typeof(XtraItemRoutedEventHandler), ownerType);
            CreateCollectionItemEvent = EventManager.RegisterRoutedEvent("CreateCollectionItem", RoutingStrategy.Direct, typeof(XtraCreateCollectionItemEventHandler), ownerType);
            FindCollectionItemEvent = EventManager.RegisterRoutedEvent("FindCollectionItem", RoutingStrategy.Direct, typeof(XtraFindCollectionItemEventHandler), ownerType);
            AllowPropertyEvent = EventManager.RegisterRoutedEvent("AllowProperty", RoutingStrategy.Direct, typeof(AllowPropertyEventHandler), ownerType);
            CustomShouldSerializePropertyEvent = EventManager.RegisterRoutedEvent("CustomShouldSerializeProperty", RoutingStrategy.Direct, typeof(CustomShouldSerializePropertyEventHandler), ownerType);
            ResetPropertyEvent = EventManager.RegisterRoutedEvent("ResetProperty", RoutingStrategy.Direct, typeof(ResetPropertyEventHandler), ownerType);
            DeserializePropertyEvent = EventManager.RegisterRoutedEvent("DeserializeProperty", RoutingStrategy.Direct, typeof(XtraPropertyInfoEventHandler), ownerType);
            ShouldSerializeCollectionItemEvent = EventManager.RegisterRoutedEvent("ShouldSerializeCollectionItem", RoutingStrategy.Direct, typeof(XtraShouldSerializeCollectionItemEventHandler), ownerType);
            CreateContentPropertyValueEvent = EventManager.RegisterRoutedEvent("CreateContentPropertyValue", RoutingStrategy.Direct, typeof(XtraCreateContentPropertyValueEventHandler), ownerType);
            BeforeLoadLayoutEvent = EventManager.RegisterRoutedEvent("BeforeLoadLayout", RoutingStrategy.Direct, typeof(BeforeLoadLayoutEventHandler), ownerType);
            LayoutUpgradeEvent = EventManager.RegisterRoutedEvent("LayoutUpgrade", RoutingStrategy.Direct, typeof(LayoutUpgradeEventHandler), ownerType);
        }

        internal static void Accept(DependencyObject dObj, Action<IDXSerializable> visit)
        {
            Accept(dObj, AcceptNestedObjects.Default, visit);
        }

        internal static void Accept(DependencyObject dObj, AcceptNestedObjects acceptNested, Action<IDXSerializable> visit)
        {
            try
            {
                Func<DependencyObject, bool> nestedChilrenPredicate = (acceptNested != AcceptNestedObjects.IgnoreChildrenOfDisabledObjects) ? (<>c.<>9__99_1 ??= x => true) : x => (ReferenceEquals(x, dObj) || CanAccept(x));
                using (VisualTreeEnumerator enumerator = GetEnumerator(dObj, acceptNested, nestedChilrenPredicate))
                {
                    while (enumerator.MoveNext())
                    {
                        if (!CanAccept(enumerator.Current))
                        {
                            continue;
                        }
                        IEnumerable<string> parentIDs = GetParentIDs(enumerator.GetVisualParents());
                        BehaviorCollection behaviors = (BehaviorCollection) enumerator.Current.GetValue(Interaction.BehaviorsProperty);
                        if (behaviors != null)
                        {
                            foreach (Behavior behavior in behaviors)
                            {
                                AcceptCore(behavior, visit, parentIDs, nestedChilrenPredicate);
                            }
                        }
                        AcceptCore(enumerator.Current, visit, parentIDs, nestedChilrenPredicate);
                    }
                }
            }
            finally
            {
                acceptedObjects.Clear();
                customSerializableChildren.Clear();
            }
        }

        private static void AcceptChildVisualTree(DependencyObject child, Action<IDXSerializable> visit, DependencyObject parent, Func<DependencyObject, bool> nestedChilrenPredicate)
        {
            VisualTreeEnumerator enumerator = GetEnumerator(child, AcceptNestedObjects.VisualTreeOnly, nestedChilrenPredicate);
            while (enumerator.MoveNext())
            {
                DependencyObject current = enumerator.Current;
                if (GetEnabled(current) && GetEnableSubtree(current))
                {
                    DependencyObject[] first = new DependencyObject[] { parent };
                    IEnumerable<DependencyObject> visualParents = first.Concat<DependencyObject>(enumerator.GetVisualParents());
                    if (IsDXSerializable(current))
                    {
                        customSerializableChildren.Add(current);
                    }
                    AcceptCore(current, visit, GetParentIDs(visualParents), nestedChilrenPredicate);
                }
            }
        }

        private static void AcceptCore(DependencyObject dObj, Action<IDXSerializable> visit, IEnumerable<string> parentIDs, Func<DependencyObject, bool> nestedChilrenPredicate)
        {
            IDXSerializable iDXSerializable = GetIDXSerializable(dObj, parentIDs);
            if (iDXSerializable != null)
            {
                visit(iDXSerializable);
                if (!GetIsProcessingSingleObject(dObj))
                {
                    DependencyObject[] visualParents = new DependencyObject[] { dObj };
                    parentIDs = parentIDs.Concat<string>(GetParentIDs(visualParents));
                    CheckCustomSerializableChildren(dObj, visit, parentIDs, nestedChilrenPredicate);
                }
            }
        }

        public static void AddAllowPropertyHandler(DependencyObject dObj, AllowPropertyEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(AllowPropertyEvent, handler);
            }
        }

        public static void AddBeforeLoadLayoutHandler(DependencyObject dObj, BeforeLoadLayoutEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(BeforeLoadLayoutEvent, handler);
            }
        }

        public static void AddClearCollectionHandler(DependencyObject dObj, XtraItemRoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(ClearCollectionEvent, handler);
            }
        }

        public static void AddCreateCollectionItemEventHandler(DependencyObject dObj, XtraCreateCollectionItemEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(CreateCollectionItemEvent, handler);
            }
        }

        public static void AddCreateContentPropertyValueEventHandler(DependencyObject dObj, XtraCreateContentPropertyValueEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(CreateContentPropertyValueEvent, handler);
            }
        }

        public static void AddCustomGetSerializableChildrenHandler(DependencyObject dObj, CustomGetSerializableChildrenEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(CustomGetSerializableChildrenEvent, handler);
            }
        }

        public static void AddCustomGetSerializablePropertiesHandler(DependencyObject dObj, CustomGetSerializablePropertiesEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(CustomGetSerializablePropertiesEvent, handler);
            }
        }

        public static void AddCustomShouldSerializePropertyHandler(DependencyObject dObj, CustomShouldSerializePropertyEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(CustomShouldSerializePropertyEvent, handler);
            }
        }

        public static void AddDeserializePropertyHandler(DependencyObject dObj, XtraPropertyInfoEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(DeserializePropertyEvent, handler);
            }
        }

        public static void AddEndDeserializingHandler(DependencyObject dObj, EndDeserializingEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(EndDeserializingEvent, handler);
            }
        }

        public static void AddEndSerializingHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(EndSerializingEvent, handler);
            }
        }

        public static void AddFindCollectionItemEventHandler(DependencyObject dObj, XtraFindCollectionItemEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(FindCollectionItemEvent, handler);
            }
        }

        public static void AddLayoutUpgradeHandler(DependencyObject dObj, LayoutUpgradeEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(LayoutUpgradeEvent, handler);
            }
        }

        public static void AddResetPropertyHandler(DependencyObject dObj, ResetPropertyEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(ResetPropertyEvent, handler);
            }
        }

        public static void AddShouldSerializeCollectionItemHandler(DependencyObject dObj, XtraShouldSerializeCollectionItemEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(ShouldSerializeCollectionItemEvent, handler);
            }
        }

        public static void AddStartDeserializingHandler(DependencyObject dObj, StartDeserializingEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(StartDeserializingEvent, handler);
            }
        }

        public static void AddStartSerializingHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(StartSerializingEvent, handler);
            }
        }

        private static bool CanAccept(DependencyObject dObj) => 
            GetEnabled(dObj) && (GetEnableSubtree(dObj) && (!GetIsCustomSerializableChild(dObj) && !customSerializableChildren.Contains(dObj)));

        private static void CheckCustomSerializableChildren(DependencyObject dObj, Action<IDXSerializable> visit, IEnumerable<string> parentIDs, Func<DependencyObject, bool> nestedChilrenPredicate)
        {
            CustomGetSerializableChildrenEventArgs e = new CustomGetSerializableChildrenEventArgs(dObj);
            SerializationProvider serializationProvider = GetSerializationProvider(dObj);
            if (serializationProvider != null)
            {
                serializationProvider.OnCustomGetSerializableChildren(dObj, e);
                foreach (DependencyObject obj2 in e.Children.Where<DependencyObject>(nestedChilrenPredicate))
                {
                    AcceptChildVisualTree(obj2, visit, dObj, nestedChilrenPredicate);
                }
            }
        }

        public static void Deserialize(DependencyObject root, Stream stream, string appName, DXOptionsLayout options)
        {
            AcceptNestedObjects acceptNested = (options == null) ? AcceptNestedObjects.Default : options.AcceptNestedObjects;
            DeserializeCore(root, stream, appName, options, acceptNested);
        }

        public static void Deserialize(DependencyObject root, object path, string appName, DXOptionsLayout options)
        {
            AcceptNestedObjects acceptNested = (options == null) ? AcceptNestedObjects.Default : options.AcceptNestedObjects;
            DeserializeCore(root, path, appName, options, acceptNested);
        }

        public static void Deserialize(DependencyObject[] dObjects, object path, string appName, DXOptionsLayout options)
        {
            DeserializeCore(dObjects, path, appName, options);
        }

        private static void DeserializeCore(DependencyObject[] dObjects, object path, string appName, DXOptionsLayout options)
        {
            new DXXmlSerializer().DeserializeObject(new SerializationStore(dObjects), path, appName, options);
        }

        private static void DeserializeCore(DependencyObject root, Stream stream, string appName, DXOptionsLayout options, AcceptNestedObjects acceptNested)
        {
            new DXXmlSerializer().DeserializeObject(new SerializationStore(root, acceptNested), stream, appName, options);
        }

        private static void DeserializeCore(DependencyObject root, object path, string appName, DXOptionsLayout options, AcceptNestedObjects acceptNested)
        {
            new DXXmlSerializer().DeserializeObject(new SerializationStore(root, acceptNested), path, appName, options);
        }

        public static void DeserializeSingleObject(DependencyObject d, object path, string appName)
        {
            SetIsProcessingSingleObject(d, true);
            try
            {
                DeserializeCore(d, path, appName, null, AcceptNestedObjects.Nothing);
            }
            finally
            {
                SetIsProcessingSingleObject(d, false);
            }
        }

        public static bool GetEnabled(DependencyObject dObj) => 
            (bool) dObj.GetValue(EnabledProperty);

        public static bool GetEnableSubtree(DependencyObject dObj) => 
            (bool) dObj.GetValue(EnableSubtreeProperty);

        private static VisualTreeEnumerator GetEnumerator(DependencyObject dObj, AcceptNestedObjects acceptNested, Func<DependencyObject, bool> nestedChilrenPredicate)
        {
            VisualTreeEnumerator enumerator;
            switch (acceptNested)
            {
                case AcceptNestedObjects.AllTree:
                case AcceptNestedObjects.IgnoreChildrenOfDisabledObjects:
                    enumerator = new SerializationEnumerator(dObj, nestedChilrenPredicate);
                    break;

                case AcceptNestedObjects.Nothing:
                    enumerator = new SingleObjectEnumerator(dObj);
                    break;

                default:
                    enumerator = new SerializationVisualEnumerator(dObj, nestedChilrenPredicate);
                    break;
            }
            return enumerator;
        }

        private static IDXSerializable GetIDXSerializable(DependencyObject dObj, IEnumerable<string> parentIDs) => 
            IsDXSerializable(dObj) ? new SerializationProviderWrapper(dObj, GetSerializationProvider(dObj), parentIDs) : null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool GetIsCustomSerializableChild(DependencyObject dObj) => 
            (bool) dObj.GetValue(IsCustomSerializableChildProperty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool GetIsProcessingSingleObject(DependencyObject obj) => 
            (bool) obj.GetValue(IsProcessingSingleObjectProperty);

        public static string GetLayoutVersion(DependencyObject obj) => 
            (string) obj.GetValue(LayoutVersionProperty);

        private static IEnumerable<string> GetParentIDs(IEnumerable<DependencyObject> visualParents)
        {
            Func<DependencyObject, bool> predicate = <>c.<>9__107_0;
            if (<>c.<>9__107_0 == null)
            {
                Func<DependencyObject, bool> local1 = <>c.<>9__107_0;
                predicate = <>c.<>9__107_0 = obj => IsDXSerializable(obj);
            }
            Func<DependencyObject, string> selector = <>c.<>9__107_1;
            if (<>c.<>9__107_1 == null)
            {
                Func<DependencyObject, string> local2 = <>c.<>9__107_1;
                selector = <>c.<>9__107_1 = obj => GetSerializationProvider(obj).GetSerializationID(obj);
            }
            return visualParents.Where<DependencyObject>(predicate).Select<DependencyObject, string>(selector);
        }

        public static string GetSerializationID(DependencyObject obj) => 
            (string) obj.GetValue(SerializationIDProperty);

        public static string GetSerializationIDDefault(DependencyObject obj) => 
            (string) obj.GetValue(SerializationIDDefaultProperty);

        public static SerializationProvider GetSerializationProvider(DependencyObject obj) => 
            (SerializationProvider) obj.GetValue(SerializationProviderProperty);

        public static StoreLayoutMode GetStoreLayoutMode(DependencyObject obj) => 
            (StoreLayoutMode) obj.GetValue(StoreLayoutModeProperty);

        internal static bool IsDXSerializable(DependencyObject dObj) => 
            !string.IsNullOrEmpty(GetSerializationProvider(dObj).GetSerializationID(dObj));

        private static void OnSerializationIDChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
        {
            InvalidSerializationIDException.Assert((string) ea.NewValue);
        }

        public static void RemoveAllowPropertyHandler(DependencyObject dObj, AllowPropertyEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(AllowPropertyEvent, handler);
            }
        }

        public static void RemoveBeforeLoadLayoutHandler(DependencyObject dObj, BeforeLoadLayoutEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(BeforeLoadLayoutEvent, handler);
            }
        }

        public static void RemoveClearCollectionHandler(DependencyObject dObj, XtraItemRoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(ClearCollectionEvent, handler);
            }
        }

        public static void RemoveCreateCollectionItemEventHandler(DependencyObject dObj, XtraCreateCollectionItemEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(CreateCollectionItemEvent, handler);
            }
        }

        public static void RemoveCreateContentPropertyValueEventHandler(DependencyObject dObj, XtraCreateContentPropertyValueEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(CreateContentPropertyValueEvent, handler);
            }
        }

        public static void RemoveCustomGetSerializableChildrenHandler(DependencyObject dObj, CustomGetSerializableChildrenEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(CustomGetSerializableChildrenEvent, handler);
            }
        }

        public static void RemoveCustomGetSerializablePropertiesHandler(DependencyObject dObj, CustomGetSerializablePropertiesEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(CustomGetSerializablePropertiesEvent, handler);
            }
        }

        public static void RemoveCustomShouldSerializePropertyHandler(DependencyObject dObj, CustomShouldSerializePropertyEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(CustomShouldSerializePropertyEvent, handler);
            }
        }

        public static void RemoveDeserializePropertyHandler(DependencyObject dObj, XtraPropertyInfoEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(DeserializePropertyEvent, handler);
            }
        }

        public static void RemoveEndDeserializingHandler(DependencyObject dObj, EndDeserializingEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(EndDeserializingEvent, handler);
            }
        }

        public static void RemoveEndSerializingHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(EndSerializingEvent, handler);
            }
        }

        public static void RemoveFindCollectionItemEventHandler(DependencyObject dObj, XtraFindCollectionItemEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(FindCollectionItemEvent, handler);
            }
        }

        public static void RemoveLayoutUpgradeHandler(DependencyObject dObj, LayoutUpgradeEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(LayoutUpgradeEvent, handler);
            }
        }

        public static void RemoveResetPropertyHandler(DependencyObject dObj, ResetPropertyEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(ResetPropertyEvent, handler);
            }
        }

        public static void RemoveShouldSerializeCollectionItemHandler(DependencyObject dObj, XtraShouldSerializeCollectionItemEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(ShouldSerializeCollectionItemEvent, handler);
            }
        }

        public static void RemoveStartDeserializingHandler(DependencyObject dObj, StartDeserializingEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(StartDeserializingEvent, handler);
            }
        }

        public static void RemoveStartSerializingHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(StartSerializingEvent, handler);
            }
        }

        public static void Serialize(DependencyObject root, Stream stream, string appName, DXOptionsLayout options)
        {
            AcceptNestedObjects acceptNested = (options == null) ? AcceptNestedObjects.Default : options.AcceptNestedObjects;
            SerializeCore(root, stream, appName, options, acceptNested);
        }

        public static void Serialize(DependencyObject root, object path, string appName, DXOptionsLayout options)
        {
            AcceptNestedObjects acceptNested = (options == null) ? AcceptNestedObjects.Default : options.AcceptNestedObjects;
            SerializeCore(root, path, appName, options, acceptNested);
        }

        public static void Serialize(DependencyObject[] dObjects, object path, string appName, DXOptionsLayout options)
        {
            SerializeCore(dObjects, path, appName, options);
        }

        private static bool SerializeCore(DependencyObject[] dObjects, object path, string appName, DXOptionsLayout options) => 
            new DXXmlSerializer().SerializeObject(new SerializationStore(dObjects), path, appName, options);

        private static bool SerializeCore(DependencyObject root, Stream stream, string appName, DXOptionsLayout options, AcceptNestedObjects acceptNested) => 
            new DXXmlSerializer().SerializeObject(new SerializationStore(root, acceptNested), stream, appName, options);

        private static bool SerializeCore(DependencyObject root, object path, string appName, DXOptionsLayout options, AcceptNestedObjects acceptNested) => 
            new DXXmlSerializer().SerializeObject(new SerializationStore(root, acceptNested), path, appName, options);

        public static void SerializeSingleObject(DependencyObject d, object path, string appName)
        {
            SetIsProcessingSingleObject(d, true);
            try
            {
                SerializeCore(d, path, appName, null, AcceptNestedObjects.Nothing);
            }
            finally
            {
                SetIsProcessingSingleObject(d, false);
            }
        }

        public static void SetEnabled(DependencyObject dObj, bool value)
        {
            dObj.SetValue(EnabledProperty, value);
        }

        public static void SetEnableSubtree(DependencyObject dObj, bool value)
        {
            dObj.SetValue(EnableSubtreeProperty, value);
        }

        private static void SetIsCustomSerializableChild(DependencyObject dObj, bool value)
        {
            dObj.SetValue(IsCustomSerializableChildPropertyKey, value);
        }

        private static void SetIsProcessingSingleObject(DependencyObject obj, bool value)
        {
            obj.SetValue(IsProcessingSingleObjectPropertyKey, value);
        }

        public static void SetLayoutVersion(DependencyObject obj, string value)
        {
            obj.SetValue(LayoutVersionProperty, value);
        }

        public static void SetSerializationID(DependencyObject obj, string value)
        {
            obj.SetValue(SerializationIDProperty, value);
        }

        public static void SetSerializationIDDefault(DependencyObject obj, string value)
        {
            obj.SetValue(SerializationIDDefaultProperty, value);
        }

        public static void SetSerializationProvider(DependencyObject obj, SerializationProvider value)
        {
            obj.SetValue(SerializationProviderProperty, value);
        }

        public static void SetStoreLayoutMode(DependencyObject obj, StoreLayoutMode value)
        {
            obj.SetValue(StoreLayoutModeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXSerializer.<>c <>9 = new DXSerializer.<>c();
            public static Func<DependencyObject, bool> <>9__99_1;
            public static Func<DependencyObject, bool> <>9__107_0;
            public static Func<DependencyObject, string> <>9__107_1;

            internal bool <Accept>b__99_1(DependencyObject x) => 
                true;

            internal bool <GetParentIDs>b__107_0(DependencyObject obj) => 
                DXSerializer.IsDXSerializable(obj);

            internal string <GetParentIDs>b__107_1(DependencyObject obj) => 
                DXSerializer.GetSerializationProvider(obj).GetSerializationID(obj);
        }

        internal class DXDeserializeHelper : DeserializeHelper
        {
            protected readonly IDXSerializable DXObject;
            protected readonly string rootVersion;

            public DXDeserializeHelper(IDXSerializable dxObj, string rootVersion)
            {
                this.DXObject = dxObj;
                this.rootVersion = rootVersion;
            }

            protected override SerializationContext CreateSerializationContext() => 
                new DXSerializationContext();

            protected internal object GetDXObj(object obj)
            {
                this.DXObject.EventTarget = obj;
                return ((obj != this.DXObject) ? this.DXObject : obj);
            }

            protected override string GetRootVersion() => 
                this.rootVersion;

            protected override void RaiseEndDeserializing(object obj, string restoredLayoutVersion)
            {
                base.RaiseEndDeserializing(this.GetDXObj(obj), restoredLayoutVersion);
            }

            protected override bool RaiseStartDeserializing(object obj, string restoredLayoutVersion) => 
                base.RaiseStartDeserializing(this.GetDXObj(obj), restoredLayoutVersion);
        }

        internal class DXSerializeHelper : SerializeHelper
        {
            protected readonly IDXSerializable DXObject;

            public DXSerializeHelper(IDXSerializable dxObj)
            {
                this.DXObject = dxObj;
            }

            protected override SerializationContext CreateSerializationContext() => 
                new DXSerializationContext();

            protected override XtraPropertyInfo CreateXtraPropertyInfo(PropertyDescriptor prop, object value, bool isKey)
            {
                DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(prop);
                return (((descriptor == null) || !descriptor.IsAttached) ? base.CreateXtraPropertyInfo(prop, value, isKey) : new AttachedPropertyInfo(prop.Name, prop.PropertyType, descriptor.DependencyProperty.PropertyType, descriptor.DependencyProperty.OwnerType, value, isKey));
            }

            protected internal object GetDXObj(object obj)
            {
                this.DXObject.EventTarget = obj;
                return ((obj != this.DXObject) ? this.DXObject : obj);
            }

            protected override XtraPropertyInfo[] PerformManualSerialization(object obj) => 
                base.PerformManualSerialization(this.GetDXObj(obj));

            protected override void RaiseEndSerializing(object obj)
            {
                base.RaiseEndSerializing(this.GetDXObj(obj));
            }

            protected override void RaiseStartSerializing(object obj)
            {
                base.RaiseStartSerializing(this.GetDXObj(obj));
            }
        }

        private class SerializationStore : IXtraSerializable2, IXtraSerializableLayout
        {
            private string versionCore;

            public SerializationStore(DependencyObject[] dObjects)
            {
                if ((dObjects != null) && (dObjects.Length != 0))
                {
                    this.Items = new Dictionary<string, IDXSerializable>();
                    try
                    {
                        Array.ForEach<DependencyObject>(dObjects, delegate (DependencyObject dObj) {
                            IDXSerializable iDXSerializable = DXSerializer.GetIDXSerializable(dObj, new string[0]);
                            if (iDXSerializable != null)
                            {
                                this.CollectDXSerializableItem(iDXSerializable);
                            }
                        });
                    }
                    finally
                    {
                        DXSerializer.acceptedObjects.Clear();
                    }
                }
            }

            public SerializationStore(DependencyObject root, AcceptNestedObjects acceptNested)
            {
                this.versionCore = DXSerializer.GetLayoutVersion(root);
                this.Items = new Dictionary<string, IDXSerializable>();
                if (root != null)
                {
                    DXSerializer.Accept(root, acceptNested, new Action<IDXSerializable>(this.CollectDXSerializableItem));
                }
            }

            private void CollectDXSerializableItem(IDXSerializable dxObj)
            {
                if (!DXSerializer.acceptedObjects.Contains(dxObj.Source))
                {
                    IDXSerializable serializable;
                    DXSerializer.acceptedObjects.Add(dxObj.Source);
                    if (this.Items.TryGetValue(dxObj.FullPath, out serializable))
                    {
                        DuplicateSerializationIDException.Assert(dxObj, serializable);
                    }
                    else
                    {
                        this.Items.Add(dxObj.FullPath, dxObj);
                    }
                }
            }

            void IXtraSerializable2.Deserialize(IList props)
            {
                string layoutVersion = DeserializeHelper.GetLayoutVersion(props as IXtraPropertyCollection);
                foreach (IDXSerializable serializable in this.Items.Values)
                {
                    XtraPropertyInfo info;
                    if (this.TryGetXtraPropertyInfo(props as IXtraPropertyCollection, serializable, out info))
                    {
                        new DXSerializer.DXDeserializeHelper(serializable, layoutVersion).DeserializeObject(serializable.Source, info.ChildProperties, null);
                    }
                }
            }

            XtraPropertyInfo[] IXtraSerializable2.Serialize()
            {
                List<XtraPropertyInfo> list = new List<XtraPropertyInfo>(this.Items.Count);
                foreach (IDXSerializable serializable in this.Items.Values)
                {
                    XtraPropertyInfo item = new XtraPropertyInfo(new XtraObjectInfo(serializable.FullPath, serializable.Source));
                    item.ChildProperties.AddRange(new DXSerializer.DXSerializeHelper(serializable).SerializeObject(serializable.Source, null));
                    list.Add(item);
                }
                return list.ToArray();
            }

            private bool TryGetXtraPropertyInfo(IXtraPropertyCollection collection, IDXSerializable dxSerializable, out XtraPropertyInfo pInfo)
            {
                string str = XtraPropertyInfo.MakeXtraObjectInfoName(dxSerializable.FullPath);
                pInfo = collection[str];
                if (pInfo == null)
                {
                    str = XtraPropertyInfo.MakeXtraObjectInfoName(DXSerializer.GetSerializationID(dxSerializable.Source));
                    if (!string.IsNullOrEmpty(str))
                    {
                        pInfo = collection[str];
                    }
                }
                return (pInfo != null);
            }

            string IXtraSerializableLayout.LayoutVersion =>
                this.versionCore;

            public Dictionary<string, IDXSerializable> Items { get; private set; }
        }
    }
}

