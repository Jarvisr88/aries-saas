namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Docking.Internal;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SerializationController : ISerializationController, IDisposable
    {
        private bool isDisposingCore;
        private DockLayoutManager containerCore;
        public const string SpecialNameSignature = "$";
        public const string HiddenItemsSignature = "$HiddenItems";
        public const string ClosedPanelsSignature = "$ClosedPanels";
        public const string FloatGroupsSignature = "$FloatGroups";
        public const string AutoHideGroupsSignature = "$AutoHideGroups";
        public const string DecomposedItemsSignature = "$DecomposedItems";
        private int startDeserializing;
        public const string DefaultID = "DockLayoutManager";

        public SerializationController(DockLayoutManager container)
        {
            this.containerCore = container;
            this.SubscribeEvents();
        }

        protected void AssignUniqueName(ISerializableItem item, ICollection<string> names)
        {
            RequestUniqueNameEventArgs e = new RequestUniqueNameEventArgs(item, names);
            e.Source = this.Container;
            this.Container.RaiseEvent(e);
            string name = item.Name;
            if (string.IsNullOrEmpty(name) || names.Contains(name))
            {
                if (!string.IsNullOrEmpty(item.Name) && (item is ISupportOriginalSerializableName))
                {
                    ((ISupportOriginalSerializableName) item).OriginalName = item.Name;
                }
                item.Name = UniqueNameHelper.GetUniqueName("dockItem", names, 1);
            }
        }

        private void BeginDeserializeItems(List<ISerializableItem> items)
        {
            if (items != null)
            {
                foreach (ISerializableItem item in items)
                {
                    BaseLayoutItem item2 = item as BaseLayoutItem;
                    if (item2 != null)
                    {
                        item2.OnDeserializationStarted();
                    }
                }
            }
        }

        private void BeginInitItems(List<ISerializableItem> newItems)
        {
            if (newItems != null)
            {
                foreach (ISerializableItem item in newItems)
                {
                    if (item is ISupportInitialize)
                    {
                        ((ISupportInitialize) item).BeginInit();
                    }
                }
            }
        }

        internal void BeginRestoreLayout()
        {
            this.startDeserializing++;
            this.Container.PrepareLayoutForModification();
            this.CollectSerializableItems();
            this.Items.Clear();
        }

        internal void BeginSaveLayout()
        {
            this.CollectSerializableItems();
            this.SaveFloatPanelsRestoreOffset();
            this.BeginSerializeItems(this.Items);
        }

        private void BeginSerializeItems(List<ISerializableItem> items)
        {
            if (items != null)
            {
                foreach (ISerializableItem item in items)
                {
                    BaseLayoutItem item2 = item as BaseLayoutItem;
                    if (item2 != null)
                    {
                        item2.OnSerializationStarted();
                    }
                }
            }
        }

        protected virtual bool CanRemoveOldItem(XtraPropertyInfo info, string typeStr) => 
            !this.Container.IsInDesignTime ? (((typeStr == "LayoutPanel") || (typeStr == "DocumentPanel")) ? RestoreLayoutOptions.GetRemoveOldPanels(this.Container) : ((typeStr == "LayoutControlItem") ? RestoreLayoutOptions.GetRemoveOldLayoutControlItems(this.Container) : ((typeStr == "LayoutGroup") ? RestoreLayoutOptions.GetRemoveOldLayoutGroups(this.Container) : false))) : false;

        protected virtual void CheckRestoredItems(List<ISerializableItem> restored)
        {
            if (!this.Container.IsInDesignTime)
            {
                bool addNewPanels = RestoreLayoutOptions.GetAddNewPanels(this.Container);
                bool addNewLayoutControlItems = RestoreLayoutOptions.GetAddNewLayoutControlItems(this.Container);
                bool addNewLayoutGroups = RestoreLayoutOptions.GetAddNewLayoutGroups(this.Container);
                foreach (KeyValuePair<string, ISerializableItem> pair in this.NamedItems)
                {
                    LayoutPanel panel = pair.Value as LayoutPanel;
                    if ((panel != null) && !restored.Contains(panel))
                    {
                        if (!addNewPanels)
                        {
                            continue;
                        }
                        this.Container.ClosedPanels.Add(panel);
                        continue;
                    }
                    LayoutControlItem item = pair.Value as LayoutControlItem;
                    if ((item != null) && !restored.Contains(item))
                    {
                        if (!addNewLayoutControlItems)
                        {
                            continue;
                        }
                        this.Container.LayoutController.HiddenItems.Add(item);
                        continue;
                    }
                    LayoutGroup group = pair.Value as LayoutGroup;
                    if ((group != null) && !restored.Contains(group))
                    {
                        if (!addNewLayoutGroups)
                        {
                            group.IsUngroupped = true;
                            continue;
                        }
                        if (group.ItemType == LayoutItemType.Group)
                        {
                            this.Container.HiddenItems.Add(group);
                        }
                    }
                }
            }
        }

        protected void CollectControls()
        {
            this.NamedControls = new Dictionary<string, object>();
            foreach (ISerializableItem item in this.Items)
            {
                if (item is ILayoutContent)
                {
                    this.NamedControls.Add(item.Name, ((ILayoutContent) item).Control);
                }
            }
        }

        protected void CollectSerializableItems()
        {
            this.Items = new SerializableItemCollection();
            if (this.Container.LayoutRoot != null)
            {
                this.Container.LayoutRoot.Accept(this.GetPrepareParentCollectionName<BaseLayoutItem>(null));
            }
            this.Container.ClosedPanels.Accept<LayoutPanel>(this.GetPrepareParentCollectionName<LayoutPanel>("$ClosedPanels"));
            this.Container.LayoutController.HiddenItems.Accept<BaseLayoutItem>(this.GetPrepareHiddenItem("$HiddenItems"));
            this.Container.FloatGroups.Accept<FloatGroup>(this.GetPrepareGroupItems<FloatGroup>("$FloatGroups"));
            this.Container.AutoHideGroups.Accept<AutoHideGroup>(this.GetPrepareGroupItems<AutoHideGroup>("$AutoHideGroups"));
            this.Container.DecomposedItems.Accept<LayoutGroup>(this.GetPrepareDecomposedItem("$DecomposedItems"));
            this.PrepareSerializableItems();
        }

        public T CreateCommand<T>(object path) where T: SerializationControllerCommand, new()
        {
            T local1 = Activator.CreateInstance<T>();
            local1.Controller = this;
            local1.Path = path;
            return local1;
        }

        protected ISerializableItem CreateItem(XtraPropertyInfo info, XtraPropertyInfo infoType)
        {
            string typeStr = (string) infoType.Value;
            string str2 = "DevExpress.Xpf.Docking.";
            if (typeStr.StartsWith(str2))
            {
                typeStr = typeStr.Remove(0, str2.Length);
            }
            return (!this.CanRemoveOldItem(info, typeStr) ? this.CreateItemCore(info, typeStr) : null);
        }

        protected virtual BaseLayoutItem CreateItemByType(XtraPropertyInfo info, string typeStr)
        {
            BaseLayoutItem item = null;
            uint num = <PrivateImplementationDetails>.ComputeStringHash(typeStr);
            if (num <= 0x5f4f9272)
            {
                if (num <= 0x22802727)
                {
                    if (num == 0xe9510cc)
                    {
                        if (typeStr == "FloatGroup")
                        {
                            item = this.Container.GenerateGroup<FloatGroup>();
                        }
                    }
                    else if (num != 0x1ab4fe9f)
                    {
                        if ((num == 0x22802727) && (typeStr == "EmptySpaceItem"))
                        {
                            item = this.Container.CreateEmptySpaceItem();
                        }
                    }
                    else if (typeStr == "LayoutControlItem")
                    {
                        item = this.Container.CreateLayoutControlItem();
                    }
                }
                else if (num == 0x46b9a433)
                {
                    if (typeStr == "SeparatorItem")
                    {
                        item = this.Container.CreateSeparatorItem();
                    }
                }
                else if (num != 0x4b5c665e)
                {
                    if ((num == 0x5f4f9272) && (typeStr == "TabbedGroup"))
                    {
                        item = this.Container.GenerateGroup<TabbedGroup>();
                    }
                }
                else if (typeStr == "LabelItem")
                {
                    item = this.Container.CreateLabelItem();
                }
            }
            else if (num <= 0x93c33e29)
            {
                if (num == 0x8e947602)
                {
                    if (typeStr == "DocumentPanel")
                    {
                        item = this.Container.CreateDocumentPanel();
                    }
                }
                else if (num != 0x9008b628)
                {
                    if ((num == 0x93c33e29) && (typeStr == "AutoHideGroup"))
                    {
                        item = this.Container.GenerateGroup<AutoHideGroup>();
                    }
                }
                else if (typeStr == "LayoutSplitter")
                {
                    item = this.Container.CreateLayoutSplitter();
                }
            }
            else if (num == 0xa94856e6)
            {
                if (typeStr == "LayoutGroup")
                {
                    item = this.Container.GenerateGroup<LayoutGroup>();
                }
            }
            else if (num != 0xbafe7b27)
            {
                if ((num == 0xf8f2cb87) && (typeStr == "LayoutPanel"))
                {
                    item = this.Container.CreateLayoutPanel();
                }
            }
            else if (typeStr == "DocumentGroup")
            {
                item = this.Container.GenerateGroup<DocumentGroup>();
            }
            return item;
        }

        private BaseLayoutItem CreateItemCore(XtraPropertyInfo info, string typeStr)
        {
            BaseLayoutItem item = this.CreateItemByType(info, typeStr);
            if (item != null)
            {
                item.AttachedSerializationController = new WeakReference(this);
                item.Manager = this.Container;
                item.PrepareForModification(true);
            }
            return item;
        }

        private void EndDeserializeItems(List<ISerializableItem> items)
        {
            if (items != null)
            {
                foreach (ISerializableItem item in items)
                {
                    BaseLayoutItem item2 = item as BaseLayoutItem;
                    if (item2 != null)
                    {
                        item2.OnDeserializationComplete();
                        item2.AttachedSerializationController = null;
                    }
                }
            }
        }

        private void EndInitItems(List<ISerializableItem> newItems)
        {
            if (newItems != null)
            {
                foreach (ISerializableItem item in newItems)
                {
                    if (item is ISupportInitialize)
                    {
                        ((ISupportInitialize) item).EndInit();
                    }
                }
            }
        }

        internal void EndRestoreLayout()
        {
            using (new LogicalTreeLocker(this.Container, this.Container.GetItems()))
            {
                this.RestoreItems();
            }
            this.RestoreSelectedTabs(this.TabIndexes);
            this.startDeserializing--;
            this.UpdateContainer();
            this.ResetSerializableItems();
        }

        internal void EndSaveLayout()
        {
            this.EndSerializeItems(this.Items);
            this.ResetSerializableItems();
        }

        private void EndSerializeItems(List<ISerializableItem> items)
        {
            if (items != null)
            {
                foreach (ISerializableItem item in items)
                {
                    BaseLayoutItem item2 = item as BaseLayoutItem;
                    if (item2 != null)
                    {
                        item2.OnSerializationComplete();
                    }
                }
            }
        }

        protected void EnsureUniqueName(ISerializableItem item)
        {
            string name = item.Name;
            if (this.IsInvalidName(name) || this.FindItemName(item))
            {
                this.AssignUniqueName(item, this.NamedItems.Keys.Union<string>(this.Container.LinkedItemNames).ToList<string>());
            }
            this.NamedItems.Add(item.Name, item);
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

        private object FindItemInLinkedManagers(string name)
        {
            object obj2;
            using (IEnumerator<DockLayoutManager> enumerator = this.Container.Linked.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Func<BaseLayoutItem, bool> <>9__0;
                        BaseLayoutItem[] items = enumerator.Current.GetItems();
                        Func<BaseLayoutItem, bool> predicate = <>9__0;
                        if (<>9__0 == null)
                        {
                            Func<BaseLayoutItem, bool> local1 = <>9__0;
                            predicate = <>9__0 = x => x.Name == name;
                        }
                        BaseLayoutItem item = items.FirstOrDefault<BaseLayoutItem>(predicate);
                        if (item == null)
                        {
                            continue;
                        }
                        obj2 = item;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return obj2;
        }

        private bool FindItemName(ISerializableItem item) => 
            !this.NamedItems.ContainsKey(item.Name) ? this.Container.FindItemNameInLinked(item.Name) : true;

        protected virtual string GetAppName() => 
            "DockLayoutManager";

        private VisitDelegate<LayoutGroup> GetPrepareDecomposedItem(string collectionName) => 
            delegate (LayoutGroup item) {
                item.Accept(this.GetPrepareParentCollectionName<BaseLayoutItem>(null));
                item.ParentCollectionName = collectionName;
            };

        private VisitDelegate<T> GetPrepareGroupItems<T>(string collectionName) where T: LayoutGroup
        {
            VisitDelegate<BaseLayoutItem> visit = this.GetPrepareParentCollectionName<BaseLayoutItem>(collectionName);
            return delegate (T group) {
                group.Accept(visit);
            };
        }

        private VisitDelegate<BaseLayoutItem> GetPrepareHiddenItem(string collectionName) => 
            delegate (BaseLayoutItem item) {
                item.Accept(this.GetPrepareParentCollectionName<BaseLayoutItem>(null));
                item.ParentCollectionName = collectionName;
            };

        private VisitDelegate<T> GetPrepareParentCollectionName<T>(string collectionName) where T: class, ISerializableItem => 
            delegate (T item) {
                item.ParentCollectionName = collectionName;
                LayoutPanel panel = item as LayoutPanel;
                if ((panel != null) && (panel.Layout != null))
                {
                    panel.Layout.Accept(this.GetPrepareParentCollectionName<BaseLayoutItem>(null));
                }
                this.Items.Add(item);
            };

        private ISerializableItem GetSerializableParent(ISerializableItem item)
        {
            BaseLayoutItem item2 = item as BaseLayoutItem;
            LayoutGroup group = item as LayoutGroup;
            ISerializableItem parent = null;
            if (item2 != null)
            {
                parent = item2.Parent;
                if ((group != null) && ((parent == null) && (group.ParentPanel != null)))
                {
                    parent = group.ParentPanel;
                }
            }
            return parent;
        }

        protected void InvalidateLayoutItems()
        {
            foreach (ISerializableItem item in this.Items)
            {
                BaseLayoutItem itemToDock = item as BaseLayoutItem;
                if (itemToDock != null)
                {
                    itemToDock.ClearValue(BaseLayoutItem.HasDesiredCaptionWidthPropertyKey);
                    itemToDock.ClearValue(BaseLayoutItem.DesiredCaptionWidthPropertyKey);
                    LayoutControlItem item3 = item as LayoutControlItem;
                    if (item3 != null)
                    {
                        item3.CoerceValue(LayoutControlItem.CaptionToControlDistanceProperty);
                        item3.CoerceValue(LayoutControlItem.ActualCaptionMarginProperty);
                    }
                    LayoutGroup group = item as LayoutGroup;
                    if (group != null)
                    {
                        group.CoerceValue(LayoutGroup.ActualDockItemIntervalProperty);
                        group.CoerceValue(LayoutGroup.ActualLayoutItemIntervalProperty);
                        group.CoerceValue(LayoutGroup.ActualLayoutGroupIntervalProperty);
                        group.CoerceValue(LayoutGroup.TabHeaderLayoutTypeProperty);
                        group.CoerceValue(LayoutGroup.AllowSplittersProperty);
                    }
                    FloatGroup group2 = item as FloatGroup;
                    if (group2 != null)
                    {
                        group2.ScreenLocationBeforeClose = null;
                    }
                    LayoutPanel panel = item as LayoutPanel;
                    if ((panel != null) && (panel.AutoHidden && (!panel.IsAutoHidden && (panel.Parent != null))))
                    {
                        panel.SetCurrentValue(LayoutPanel.AutoHiddenProperty, false);
                    }
                    itemToDock.CoerceValue(BaseLayoutItem.CaptionFormatProperty);
                    itemToDock.CoerceValue(BaseLayoutItem.ActualCaptionProperty);
                    itemToDock.CoerceValue(BaseLayoutItem.TabCaptionProperty);
                    itemToDock.CoerceValue(BaseLayoutItem.AppearanceProperty);
                    PlaceHolderHelper.ClearPlaceHolder(itemToDock);
                }
            }
        }

        protected bool IsInvalidName(string name) => 
            string.IsNullOrEmpty(name) || name.StartsWith("$");

        private void LockItemsUpdate()
        {
            Action<LayoutGroup> action = <>c.<>9__57_0;
            if (<>c.<>9__57_0 == null)
            {
                Action<LayoutGroup> local1 = <>c.<>9__57_0;
                action = <>c.<>9__57_0 = x => x.Items.BeginUpdate();
            }
            this.Items.OfType<LayoutGroup>().ToList<LayoutGroup>().ForEach(action);
            this.Container.FloatGroups.BeginUpdate();
            this.Container.ClosedPanels.BeginUpdate();
        }

        public virtual void OnClearCollection(XtraItemRoutedEventArgs e)
        {
        }

        public virtual object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
        {
            ISerializableItem item = null;
            if (e.CollectionName == "SerializableDockSituation")
            {
                item = new PlaceHolder();
                this.Items.Add(item);
            }
            if (e.CollectionName == "Items")
            {
                XtraPropertyInfo info = e.Item.ChildProperties["Name"];
                XtraPropertyInfo infoType = e.Item.ChildProperties["TypeName"];
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
            XtraPropertyInfo info2 = e.Item.ChildProperties["TypeName"];
            if ((info == null) || (info2 == null))
            {
                return null;
            }
            ISerializableItem item = this.FindItem(this.NamedItems, (string) info.Value, (string) info2.Value);
            if (item != null)
            {
                this.Items.Add(item);
                return item;
            }
            XtraPropertyInfo info3 = e.Item.ChildProperties["Name"];
            BaseLayoutItem baseItem = this.FindItemInLinkedManagers(info3.Value.ToString()) as BaseLayoutItem;
            if (baseItem is LayoutGroup)
            {
                return null;
            }
            if (baseItem == null)
            {
                return item;
            }
            baseItem.GetDockLayoutManager().Do<DockLayoutManager>(delegate (DockLayoutManager x) {
                x.DockController.RemoveItem(baseItem);
            });
            this.Items.Add(baseItem);
            return baseItem;
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
            this.NamedItems = new Dictionary<string, ISerializableItem>();
            List<ISerializableItem> list = new List<ISerializableItem>();
            foreach (ISerializableItem item in this.Items)
            {
                if (!this.IsInvalidName(item.Name) && !this.FindItemName(item))
                {
                    this.NamedItems.Add(item.Name, item);
                    continue;
                }
                list.Add(item);
            }
            foreach (ISerializableItem item2 in list)
            {
                this.EnsureUniqueName(item2);
            }
            this.UpdateParentNames();
            this.CollectControls();
        }

        protected void ProcessNewlyCreatedItems(List<ISerializableItem> newItems)
        {
            if (newItems != null)
            {
                foreach (ISerializableItem item in newItems)
                {
                    this.EnsureUniqueName(item);
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
            this.NamedControls = null;
            this.NewItems = null;
            this.TabIndexes = null;
        }

        protected void RestoreControls()
        {
            foreach (ISerializableItem item in this.Items)
            {
                object obj2;
                ILayoutContent content = item as ILayoutContent;
                if ((content != null) && this.NamedControls.TryGetValue(item.Name, out obj2))
                {
                    UIElement objB = obj2 as UIElement;
                    if (!ReferenceEquals(content.Control, objB))
                    {
                        content.Content = objB;
                    }
                }
            }
        }

        protected void RestoreItems()
        {
            this.BeginDeserializeItems(this.Items);
            this.LockItemsUpdate();
            SerializationControllerHelper.ClearLayoutRoot(this.Container);
            SerializationControllerHelper.ClearItemsCollection<LayoutPanel>(this.Container.ClosedPanels);
            SerializationControllerHelper.ClearHiddenItemsCollection(this.Container.LayoutController.HiddenItems);
            SerializationControllerHelper.ClearGroupsCollection<FloatGroup>(this.Container.FloatGroups);
            SerializationControllerHelper.ClearGroupsCollection<AutoHideGroup>(this.Container.AutoHideGroups);
            SerializationControllerHelper.ClearGroupsCollection<LayoutGroup>(this.Container.DecomposedItems);
            this.BeginInitItems(this.NewItems);
            this.ProcessNewlyCreatedItems(this.NewItems);
            this.TabIndexes = this.SaveSelectedTabs();
            this.RestoreControls();
            this.RestoreLayoutRelations();
            this.UnlockItemsUpdate();
            this.InvalidateLayoutItems();
            this.RestorePlaceHolders();
            this.EndInitItems(this.NewItems);
            this.EndDeserializeItems(this.Items);
        }

        public void RestoreLayout(object path)
        {
            DXSerializer.DeserializeSingleObject(this.Container, path, this.GetAppName());
        }

        protected void RestoreLayoutRelations()
        {
            List<ISerializableItem> restored = new List<ISerializableItem>();
            List<ISerializableItem> notRestored = new List<ISerializableItem>();
            LayoutGroup group = null;
            foreach (ISerializableItem item in this.Items)
            {
                if ((item is FloatGroup) && (item.ParentCollectionName == "$FloatGroups"))
                {
                    this.Container.FloatGroups.Add((FloatGroup) item);
                    restored.Add(item);
                    continue;
                }
                if ((item is AutoHideGroup) && (item.ParentCollectionName == "$AutoHideGroups"))
                {
                    this.Container.AutoHideGroups.Add((AutoHideGroup) item);
                    restored.Add(item);
                    continue;
                }
                if ((item.ParentCollectionName == "$HiddenItems") && (item is BaseLayoutItem))
                {
                    ISerializableItem item;
                    if (!string.IsNullOrEmpty(item.ParentName) && this.NamedItems.TryGetValue(item.ParentName, out item))
                    {
                        this.Container.LayoutController.HiddenItems.Add((BaseLayoutItem) item, (LayoutGroup) item);
                    }
                    else
                    {
                        this.Container.LayoutController.HiddenItems.Add((BaseLayoutItem) item);
                    }
                    restored.Add(item);
                    continue;
                }
                if ((item.ParentCollectionName == "$ClosedPanels") && (item is LayoutPanel))
                {
                    this.Container.ClosedPanels.Add((LayoutPanel) item);
                    restored.Add(item);
                    continue;
                }
                if ((item.ParentCollectionName == "$DecomposedItems") && (item is LayoutGroup))
                {
                    this.Container.DecomposedItems.Add((LayoutGroup) item);
                    restored.Add(item);
                    continue;
                }
                if (string.IsNullOrEmpty(item.ParentCollectionName))
                {
                    if (string.IsNullOrEmpty(item.ParentName))
                    {
                        if ((group == null) && (item is LayoutGroup))
                        {
                            group = (LayoutGroup) item;
                            restored.Add(item);
                            continue;
                        }
                    }
                    else
                    {
                        ISerializableItem item2;
                        if (!this.NamedItems.TryGetValue(item.ParentName, out item2))
                        {
                            item2 = (ISerializableItem) this.Items.OfType<ISupportOriginalSerializableName>().FirstOrDefault<ISupportOriginalSerializableName>(x => (x.OriginalName == item.ParentName));
                        }
                        if (item2 != null)
                        {
                            if ((item2 is LayoutGroup) && (item is BaseLayoutItem))
                            {
                                ((LayoutGroup) item2).Add((BaseLayoutItem) item);
                                restored.Add(item);
                                continue;
                            }
                            if ((item2 is LayoutPanel) && (item is LayoutGroup))
                            {
                                ((LayoutPanel) item2).Content = item;
                                restored.Add(item);
                                continue;
                            }
                        }
                    }
                }
                notRestored.Add(item);
            }
            this.ProcessNotRestoredItems(notRestored);
            this.Container.LayoutRoot = group;
            this.CheckRestoredItems(restored);
        }

        private void RestoreOwnedGroupRelations(PlaceHolder ph)
        {
            TabbedGroup parent = ph.Parent as TabbedGroup;
            AutoHideGroup owner = ph.Owner as AutoHideGroup;
            if ((parent != null) && (owner != null))
            {
                LayoutGroup.SetOwnerGroup(parent, owner);
                owner.HasPersistentGroups = !parent.DestroyOnClosingChildren;
            }
        }

        private void RestorePlaceHolders()
        {
            Func<ISerializableItem, bool> predicate = <>c.<>9__60_0;
            if (<>c.<>9__60_0 == null)
            {
                Func<ISerializableItem, bool> local1 = <>c.<>9__60_0;
                predicate = <>c.<>9__60_0 = x => x is PlaceHolder;
            }
            Func<ISerializableItem, int> keySelector = <>c.<>9__60_1;
            if (<>c.<>9__60_1 == null)
            {
                Func<ISerializableItem, int> local2 = <>c.<>9__60_1;
                keySelector = <>c.<>9__60_1 = x => ((PlaceHolder) x).Index;
            }
            foreach (ISerializableItem item in this.Items.Where<ISerializableItem>(predicate).OrderBy<ISerializableItem, int>(keySelector))
            {
                PlaceHolder ph = item as PlaceHolder;
                ph.BeginInit();
                ISerializableItem item2 = null;
                ISerializableItem item3 = null;
                int index = ph.Index;
                if (!string.IsNullOrEmpty(ph.OwnerName))
                {
                    this.NamedItems.TryGetValue(ph.OwnerName, out item2);
                }
                if (!string.IsNullOrEmpty(ph.ParentName))
                {
                    this.NamedItems.TryGetValue(ph.ParentName, out item3);
                }
                if (item2 is BaseLayoutItem)
                {
                    ph.Owner = (BaseLayoutItem) item2;
                    if (item3 is LayoutGroup)
                    {
                        ph.Parent = (LayoutGroup) item3;
                    }
                    PlaceHolderHelper.AddPlaceHolderForItem(item3 as LayoutGroup, (BaseLayoutItem) item2, ph, index);
                    if (ph.DockState == PlaceHolderState.Unset)
                    {
                        this.RestoreOwnedGroupRelations(ph);
                    }
                }
                ph.EndInit();
            }
        }

        private void RestoreSelectedTabs(Dictionary<LayoutGroup, int> tabIndexes)
        {
            foreach (KeyValuePair<LayoutGroup, int> pair in tabIndexes)
            {
                if (pair.Key.IsTabHost)
                {
                    pair.Key.SelectedTabIndex = pair.Value;
                }
            }
        }

        private void SaveFloatPanelsRestoreOffset()
        {
            if (this.Container.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window)
            {
                this.SaveFloatPanelsRestoreOffsetCore();
            }
        }

        private void SaveFloatPanelsRestoreOffsetCore()
        {
            Point restoreOffset = this.Container.GetRestoreOffset();
            RestoreLayoutOptions.SetFloatPanelsRestoreOffset(this.Container, restoreOffset);
        }

        public void SaveLayout(object path)
        {
            DXSerializer.SerializeSingleObject(this.Container, path, this.GetAppName());
        }

        private Dictionary<LayoutGroup, int> SaveSelectedTabs()
        {
            Dictionary<LayoutGroup, int> tabIndexes = new Dictionary<LayoutGroup, int>();
            this.Items.Accept<ISerializableItem>(delegate (ISerializableItem item) {
                LayoutGroup key = item as LayoutGroup;
                if ((key != null) && key.IsTabHost)
                {
                    tabIndexes.Add(key, key.GetSerializableSelectedTabPageIndex());
                }
            });
            return tabIndexes;
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

        private void UnlockItemsUpdate()
        {
            List<LayoutGroup> list = this.Items.OfType<LayoutGroup>().ToList<LayoutGroup>();
            Action<LayoutGroup> action = <>c.<>9__58_0;
            if (<>c.<>9__58_0 == null)
            {
                Action<LayoutGroup> local1 = <>c.<>9__58_0;
                action = <>c.<>9__58_0 = x => x.Items.ProcessRemovedItems();
            }
            list.ForEach(action);
            Action<LayoutGroup> action2 = <>c.<>9__58_1;
            if (<>c.<>9__58_1 == null)
            {
                Action<LayoutGroup> local2 = <>c.<>9__58_1;
                action2 = <>c.<>9__58_1 = x => x.Items.EndUpdate();
            }
            list.ForEach(action2);
            this.Container.FloatGroups.EndUpdate();
            this.Container.ClosedPanels.EndUpdate();
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
            this.Container.OnLayoutRestored();
        }

        protected void UpdateParentNames()
        {
            foreach (ISerializableItem item in this.Items)
            {
                ISerializableItem serializableParent = this.GetSerializableParent(item);
                item.ParentName = serializableParent?.Name;
                if ((serializableParent != null) && (item.ParentCollectionName != "$HiddenItems"))
                {
                    item.ParentCollectionName = null;
                }
            }
        }

        protected bool IsDisposing =>
            this.isDisposingCore;

        public bool IsDeserializing =>
            this.startDeserializing > 0;

        public DockLayoutManager Container =>
            this.containerCore;

        public SerializableItemCollection Items { get; set; }

        internal Dictionary<string, ISerializableItem> NamedItems { get; private set; }

        internal Dictionary<string, object> NamedControls { get; private set; }

        internal List<ISerializableItem> NewItems { get; private set; }

        protected Dictionary<LayoutGroup, int> TabIndexes { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SerializationController.<>c <>9 = new SerializationController.<>c();
            public static Action<LayoutGroup> <>9__57_0;
            public static Action<LayoutGroup> <>9__58_0;
            public static Action<LayoutGroup> <>9__58_1;
            public static Func<ISerializableItem, bool> <>9__60_0;
            public static Func<ISerializableItem, int> <>9__60_1;

            internal void <LockItemsUpdate>b__57_0(LayoutGroup x)
            {
                x.Items.BeginUpdate();
            }

            internal bool <RestorePlaceHolders>b__60_0(ISerializableItem x) => 
                x is PlaceHolder;

            internal int <RestorePlaceHolders>b__60_1(ISerializableItem x) => 
                ((PlaceHolder) x).Index;

            internal void <UnlockItemsUpdate>b__58_0(LayoutGroup x)
            {
                x.Items.ProcessRemovedItems();
            }

            internal void <UnlockItemsUpdate>b__58_1(LayoutGroup x)
            {
                x.Items.EndUpdate();
            }
        }
    }
}

