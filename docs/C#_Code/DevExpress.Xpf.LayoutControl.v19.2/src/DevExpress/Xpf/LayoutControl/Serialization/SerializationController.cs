namespace DevExpress.Xpf.LayoutControl.Serialization
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class SerializationController : ISerializationController, IDisposable
    {
        private const string AvailableItemsCollectionName = "AvailableItems";
        private bool isDisposingCore;
        private DevExpress.Xpf.LayoutControl.LayoutControl containerCore;
        public const string SpecialNameSignature = "$";
        public const string AvailableItemsSignature = "$AvailableItems";
        public const string DefaultID = "LayoutControl";
        private List<string> originallyNamedItems;
        private int startDeserializing;

        public SerializationController(DevExpress.Xpf.LayoutControl.LayoutControl container)
        {
            this.containerCore = container;
            this.SubscribeEvents();
        }

        protected void AssignUniqueName(ISerializableItem item)
        {
            string name = item.Name;
            if (string.IsNullOrEmpty(name) || (this.NamedItems.Keys.Contains<string>(name) || this.originallyNamedItems.Contains(name)))
            {
                item.Name = this.GetUniqueName("_layoutItem", 1);
                item.Element.Name = item.Name;
            }
        }

        private void BeginInitItems(List<ISerializableItem> newItems)
        {
            if (newItems != null)
            {
                foreach (ISerializableItem item in newItems)
                {
                    ISupportInitialize input = item as ISupportInitialize;
                    Action<ISupportInitialize> action = <>c.<>9__60_0;
                    if (<>c.<>9__60_0 == null)
                    {
                        Action<ISupportInitialize> local1 = <>c.<>9__60_0;
                        action = <>c.<>9__60_0 = x => x.BeginInit();
                    }
                    input.Do<ISupportInitialize>(action);
                }
            }
        }

        internal void BeginRestoreLayout()
        {
            this.startDeserializing++;
            this.CollectSerializableItems();
            this.Items.Clear();
        }

        internal void BeginSaveLayout()
        {
            this.CollectSerializableItems();
        }

        protected virtual bool CanRemoveOldItem(XtraPropertyInfo info, string typeStr) => 
            ((typeStr == "LayoutItem") || (typeStr == "DataLayoutItem")) ? RestoreLayoutOptions.GetRemoveOldItems(this.Container) : false;

        protected virtual void CheckRestoredItems(List<ISerializableItem> restored)
        {
            bool addNewItems = RestoreLayoutOptions.GetAddNewItems(this.Container);
            foreach (KeyValuePair<string, ISerializableItem> pair in this.NamedItems)
            {
                ISerializableItem item = pair.Value;
                if ((item != null) && (!restored.Contains(item) && ((item.ParentCollectionName != "AvailableItems") && (addNewItems && (pair.Value.Element != null)))))
                {
                    this.Container.AvailableItems.Add(pair.Value.Element);
                }
            }
        }

        public static void ClearLayout(DevExpress.Xpf.LayoutControl.LayoutControl container)
        {
            FrameworkElement[] elementArray = container.GetLogicalChildren(false).ToArray<FrameworkElement>();
            for (int i = 0; i < elementArray.Length; i++)
            {
                ClearLayoutRecursively(elementArray[i]);
            }
            elementArray = container.AvailableItems.ToArray<FrameworkElement>();
            for (int j = 0; j < elementArray.Length; j++)
            {
                ClearLayoutRecursively(elementArray[j]);
            }
            container.AvailableItems.Clear();
        }

        private static void ClearLayoutRecursively(FrameworkElement item)
        {
            item.RemoveFromVisualTree();
            LayoutControlBase base2 = item as LayoutControlBase;
            if (base2 != null)
            {
                FrameworkElement[] elementArray = base2.GetLogicalChildren(false).ToArray<FrameworkElement>();
                for (int i = 0; i < elementArray.Length; i++)
                {
                    ClearLayoutRecursively(elementArray[i]);
                }
            }
        }

        protected void CollectElementName(FrameworkElement element)
        {
            if (!string.IsNullOrEmpty(element.Name))
            {
                this.originallyNamedItems.Add(element.Name);
            }
        }

        protected void CollectOriginallyNamedItems()
        {
            this.originallyNamedItems = new List<string>();
            this.CollectElementName(this.Container);
            this.CollectOriginallyNamedNestedItems(this.Container);
            foreach (FrameworkElement element in this.Container.AvailableItems)
            {
                this.CollectElementName(element);
                LayoutGroup nestedGroup = element as LayoutGroup;
                if (nestedGroup != null)
                {
                    this.CollectOriginallyNamedNestedItems(nestedGroup);
                }
            }
        }

        protected void CollectOriginallyNamedNestedItems(LayoutGroup nestedGroup)
        {
            foreach (FrameworkElement element in nestedGroup.GetLogicalChildren(false))
            {
                this.CollectElementName(element);
                LayoutGroup group = element as LayoutGroup;
                if (group != null)
                {
                    this.CollectOriginallyNamedNestedItems(group);
                }
            }
        }

        protected void CollectSerializableItems()
        {
            this.CollectOriginallyNamedItems();
            this.Items = new SerializableItemCollection();
            this.NamedItems = new Dictionary<string, ISerializableItem>();
            this.itemsWithInvalidNames = new List<ISerializableItem>();
            this.CollectSerializableNestedItems(this.Container);
            foreach (FrameworkElement element in this.Container.AvailableItems)
            {
                this.SerializeElement(element, "AvailableItems");
                LayoutGroup nestedGroup = element as LayoutGroup;
                if (nestedGroup != null)
                {
                    this.CollectSerializableNestedItems(nestedGroup);
                }
            }
            this.PrepareSerializableItems();
        }

        protected void CollectSerializableNestedItems(LayoutGroup nestedGroup)
        {
            foreach (FrameworkElement element in nestedGroup.GetLogicalChildren(false))
            {
                this.SerializeElement(element, null);
                LayoutGroup group = element as LayoutGroup;
                if (group != null)
                {
                    this.CollectSerializableNestedItems(group);
                }
            }
        }

        protected ISerializableItem CreateItem(XtraPropertyInfo info, XtraPropertyInfo infoType)
        {
            string typeStr = (string) infoType.Value;
            string str2 = "DevExpress.Xpf.LayoutControl.";
            if (typeStr.StartsWith(str2))
            {
                typeStr = typeStr.Remove(0, str2.Length);
            }
            return (!this.CanRemoveOldItem(info, typeStr) ? this.CreateItemByType(info, typeStr) : null);
        }

        protected virtual ISerializableItem CreateItemByType(XtraPropertyInfo info, string typeStr) => 
            (typeStr == "LayoutGroup") ? ((ISerializableItem) new LayoutGroup()) : ((typeStr == "LayoutItem") ? ((ISerializableItem) new LayoutItem()) : ((typeStr == "DataLayoutItem") ? ((ISerializableItem) new DataLayoutItem()) : null));

        private void EndInitItems()
        {
            foreach (ISerializableItem item in this.Items)
            {
                SerializableItem item2 = item as SerializableItem;
                if ((item2 != null) && (item2.Element != null))
                {
                    item2.Element.Height = item2.Height;
                    item2.Element.Width = item2.Width;
                    item2.Element.VerticalAlignment = item2.VerticalAlignment;
                    item2.Element.HorizontalAlignment = item2.HorizontalAlignment;
                }
                LayoutItem item3 = item as LayoutItem;
                if (item3 != null)
                {
                    item3.OnEndDeserializing();
                }
            }
        }

        private void EndInitItems(List<ISerializableItem> newItems)
        {
            if (newItems != null)
            {
                foreach (ISerializableItem item in newItems)
                {
                    ISupportInitialize input = item as ISupportInitialize;
                    Action<ISupportInitialize> action = <>c.<>9__61_0;
                    if (<>c.<>9__61_0 == null)
                    {
                        Action<ISupportInitialize> local1 = <>c.<>9__61_0;
                        action = <>c.<>9__61_0 = x => x.EndInit();
                    }
                    input.Do<ISupportInitialize>(action);
                }
            }
        }

        internal void EndRestoreLayout()
        {
            this.RestoreItems();
            this.startDeserializing--;
            this.UpdateContainer();
            this.ResetSerializableItems();
        }

        internal void EndSaveLayout()
        {
            this.ResetSerializableItems();
        }

        protected void EnsureUniqueName(Dictionary<string, ISerializableItem> namedItems, ISerializableItem item)
        {
            string name = item.Name;
            if (this.IsInvalidName(name) || (namedItems.ContainsKey(name) || this.originallyNamedItems.Contains(name)))
            {
                this.AssignUniqueName(item);
            }
            namedItems.Add(item.Name, item);
        }

        protected ISerializableItem FindItem(Dictionary<string, ISerializableItem> namedItems, string name, string type)
        {
            ISerializableItem item;
            if (namedItems.TryGetValue(name, out item) && (item.TypeName != type))
            {
                namedItems.Remove(name);
                item = null;
            }
            return item;
        }

        protected virtual string GetAppName() => 
            "LayoutControl";

        private static DevExpress.Xpf.LayoutControl.LayoutControl GetContainer(DependencyObject dObj) => 
            dObj as DevExpress.Xpf.LayoutControl.LayoutControl;

        private static ISerializationController GetSerializationController(DevExpress.Xpf.LayoutControl.LayoutControl container) => 
            container?.SerializationController;

        public static ISerializationController GetSerializationController(DependencyObject dObj)
        {
            ISerializationController serializationController = GetSerializationController(dObj as DevExpress.Xpf.LayoutControl.LayoutControl);
            return ((serializationController == null) ? GetSerializationController(GetContainer(dObj)) : serializationController);
        }

        private string GetUniqueName(string prefix, int initialValue)
        {
            int num = initialValue;
            while (true)
            {
                string str = prefix + num++;
                if (!this.NamedItems.Keys.Contains<string>(str) && !this.originallyNamedItems.Contains(str))
                {
                    return str;
                }
            }
        }

        protected bool IsInvalidName(string name) => 
            string.IsNullOrEmpty(name) || name.StartsWith("$");

        public virtual void OnClearCollection(XtraItemRoutedEventArgs e)
        {
        }

        public virtual object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
        {
            ISerializableItem item = null;
            if (e.CollectionName == "Items")
            {
                XtraPropertyInfo info = e.Item.ChildProperties["Name"];
                XtraPropertyInfo infoType = e.Item.ChildProperties["TypeName"] ?? e.Item.ChildProperties["SerializableItem.TypeName"];
                if ((info == null) || (infoType == null))
                {
                    return null;
                }
                item = this.CreateItem(info, infoType);
                if (item != null)
                {
                    this.NewItems ??= new List<ISerializableItem>();
                    this.NewItems.Add(item);
                    this.Items.Add(item);
                }
            }
            return item;
        }

        protected void OnDisposing()
        {
            this.UnSubscribeEvents();
            this.ResetSerializableItems();
            this.containerCore = null;
        }

        protected virtual void OnEndDeserializing(object sender, EndDeserializingEventArgs e)
        {
            this.EndRestoreLayout();
        }

        protected virtual void OnEndSerializing(object sender, RoutedEventArgs e)
        {
            this.EndSaveLayout();
        }

        public virtual object OnFindCollectionItem(XtraFindCollectionItemEventArgs e)
        {
            if (e.CollectionName != "Items")
            {
                return null;
            }
            XtraPropertyInfo info = e.Item.ChildProperties["Name"];
            XtraPropertyInfo info2 = e.Item.ChildProperties["TypeName"] ?? e.Item.ChildProperties["SerializableItem.TypeName"];
            if ((info == null) || (info2 == null))
            {
                return null;
            }
            ISerializableItem item = this.FindItem(this.NamedItems, (string) info.Value, (string) info2.Value);
            if (item != null)
            {
                this.Items.Add(item);
            }
            return item;
        }

        protected virtual void OnStartDeserializing(object sender, StartDeserializingEventArgs e)
        {
            this.BeginRestoreLayout();
        }

        protected virtual void OnStartSerializing(object sender, RoutedEventArgs e)
        {
            this.BeginSaveLayout();
        }

        protected void PrepareSerializableItems()
        {
            foreach (ISerializableItem item in this.Items)
            {
                if (this.IsInvalidName(item.Name))
                {
                    this.itemsWithInvalidNames.Add(item);
                    continue;
                }
                if (!this.NamedItems.ContainsKey(item.Name))
                {
                    this.NamedItems.Add(item.Name, item);
                }
            }
            foreach (ISerializableItem item2 in this.itemsWithInvalidNames)
            {
                this.EnsureUniqueName(this.NamedItems, item2);
            }
        }

        protected void ProcessNewlyCreatedItems(List<ISerializableItem> newItems)
        {
            if (newItems != null)
            {
                foreach (ISerializableItem item in newItems)
                {
                    this.EnsureUniqueName(this.NamedItems, item);
                }
            }
        }

        protected virtual void ProcessNotRestoredItems(List<ISerializableItem> notRestored)
        {
        }

        protected void ResetSerializableItems()
        {
            this.Items = null;
            this.NamedItems = null;
            this.NewItems = null;
            this.TabIndexes = null;
            this.itemsWithInvalidNames = null;
        }

        protected void RestoreItems()
        {
            ClearLayout(this.Container);
            this.Container.AvailableItems.Clear();
            this.BeginInitItems(this.NewItems);
            this.ProcessNewlyCreatedItems(this.NewItems);
            this.RestoreLayoutRelations();
            this.EndInitItems(this.NewItems);
            this.EndInitItems();
        }

        public void RestoreLayout(object path)
        {
            DXSerializer.DeserializeSingleObject(this.Container, path, this.GetAppName());
        }

        protected void RestoreLayoutRelations()
        {
            List<ISerializableItem> restored = new List<ISerializableItem>();
            List<ISerializableItem> notRestored = new List<ISerializableItem>();
            foreach (ISerializableItem item in this.Items)
            {
                ISerializableItem item2;
                if (item.Element == null)
                {
                    continue;
                }
                if (item.ParentCollectionName == "AvailableItems")
                {
                    this.Container.AvailableItems.Add(item.Element);
                    restored.Add(item);
                    continue;
                }
                if (item.ParentName == this.Container.Name)
                {
                    this.Container.Children.Add(item.Element);
                    restored.Add(item);
                    continue;
                }
                if (this.NamedItems.TryGetValue(item.ParentName, out item2))
                {
                    LayoutControlBase base2 = item2 as LayoutControlBase;
                    if (base2 != null)
                    {
                        base2.Children.Add(item.Element);
                        restored.Add(item);
                    }
                }
            }
            this.ProcessNotRestoredItems(notRestored);
            this.CheckRestoredItems(restored);
        }

        public void SaveLayout(object path)
        {
            DXSerializer.SerializeSingleObject(this.Container, path, this.GetAppName());
        }

        protected void SerializeElement(FrameworkElement element, string ParentCollectionName = null)
        {
            ISerializableItem item = element as ISerializableItem;
            if (item != null)
            {
                item.ParentCollectionName = ParentCollectionName;
                item.Name = element.Name;
                item.ParentName = ((FrameworkElement) element.Parent).Name;
                if (this.IsInvalidName(item.Name))
                {
                    this.EnsureUniqueName(this.NamedItems, item);
                }
            }
            else
            {
                SerializableItem item1 = new SerializableItem();
                item1.Name = element.Name;
                item1.TypeName = element.GetType().Name;
                item1.ParentName = ((FrameworkElement) element.Parent).Name;
                item1.ParentCollectionName = ParentCollectionName;
                item1.Height = element.Height;
                item1.Width = element.Width;
                item1.HorizontalAlignment = element.HorizontalAlignment;
                item1.VerticalAlignment = element.VerticalAlignment;
                item1.Element = element;
                item = item1;
                if (this.IsInvalidName(item.Name))
                {
                    this.EnsureUniqueName(this.NamedItems, item);
                }
            }
            this.Items.Add(item);
        }

        private void SubscribeEvents()
        {
            DXSerializer.AddStartSerializingHandler(this.Container, new RoutedEventHandler(this.OnStartSerializing));
            DXSerializer.AddEndSerializingHandler(this.Container, new RoutedEventHandler(this.OnEndSerializing));
            DXSerializer.AddStartDeserializingHandler(this.Container, new StartDeserializingEventHandler(this.OnStartDeserializing));
            DXSerializer.AddEndDeserializingHandler(this.Container, new EndDeserializingEventHandler(this.OnEndDeserializing));
        }

        void IDisposable.Dispose()
        {
            if (!this.IsDisposing)
            {
                this.isDisposingCore = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        private void UnSubscribeEvents()
        {
            DXSerializer.RemoveStartSerializingHandler(this.Container, new RoutedEventHandler(this.OnStartSerializing));
            DXSerializer.RemoveEndSerializingHandler(this.Container, new RoutedEventHandler(this.OnEndSerializing));
            DXSerializer.RemoveStartDeserializingHandler(this.Container, new StartDeserializingEventHandler(this.OnStartDeserializing));
            DXSerializer.RemoveEndDeserializingHandler(this.Container, new EndDeserializingEventHandler(this.OnEndDeserializing));
        }

        private void UpdateContainer()
        {
            this.Container.InvalidateVisual();
        }

        protected bool IsDisposing =>
            this.isDisposingCore;

        public bool IsDeserializing =>
            this.startDeserializing > 0;

        public DevExpress.Xpf.LayoutControl.LayoutControl Container =>
            this.containerCore;

        public SerializableItemCollection Items { get; set; }

        internal Dictionary<string, ISerializableItem> NamedItems { get; private set; }

        internal List<ISerializableItem> NewItems { get; private set; }

        internal List<ISerializableItem> itemsWithInvalidNames { get; private set; }

        protected Dictionary<LayoutGroup, int> TabIndexes { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SerializationController.<>c <>9 = new SerializationController.<>c();
            public static Action<ISupportInitialize> <>9__60_0;
            public static Action<ISupportInitialize> <>9__61_0;

            internal void <BeginInitItems>b__60_0(ISupportInitialize x)
            {
                x.BeginInit();
            }

            internal void <EndInitItems>b__61_0(ISupportInitialize x)
            {
                x.EndInit();
            }
        }
    }
}

