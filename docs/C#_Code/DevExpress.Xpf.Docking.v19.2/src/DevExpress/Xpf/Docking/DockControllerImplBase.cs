namespace DevExpress.Xpf.Docking
{
    using DevExpress.Data;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Internal;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    internal class DockControllerImplBase : IActiveItemOwner, ILockOwner
    {
        public static readonly DependencyProperty IsRestoringProperty;
        private ActiveItemHelper ActivationHelper;
        private BaseLayoutItem activeItemCore;
        private DockLayoutManager containerCore;
        private int lockActivateCounter;

        static DockControllerImplBase()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockControllerImpl>.New().RegisterAttached<DependencyObject, bool>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, bool>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DockControllerImplBase.GetIsRestoring), arguments), parameters), out IsRestoringProperty, false, frameworkOptions);
        }

        public DockControllerImplBase(DockLayoutManager container)
        {
            this.containerCore = container;
            this.ActivationHelper = new ActiveItemHelper(this);
        }

        public void Activate(BaseLayoutItem item)
        {
            this.Activate(item, true);
        }

        public void Activate(BaseLayoutItem item, bool focus)
        {
            if (((this.lockActivateCounter <= 0) && ((item != null) && (item.AllowActivate && (!item.IsClosed && item.IsVisibleCore)))) && (!(item is LayoutGroup) || !((LayoutGroup) item).IsUngroupped))
            {
                this.lockActivateCounter++;
                try
                {
                    if (!ReferenceEquals(this.ActiveItem, item) && this.RaiseItemCancelEvent(item, DockLayoutManager.DockItemActivatingEvent))
                    {
                        item.InvokeCancelActivation(this.ActiveItem);
                    }
                    else
                    {
                        this.ActivationHelper.ActivateItem(item, focus);
                    }
                }
                finally
                {
                    this.lockActivateCounter--;
                }
            }
        }

        public DocumentGroup AddDocumentGroup(DockType type)
        {
            DocumentGroup item = this.Container.GenerateGroup<DocumentGroup>();
            if ((type != DockType.Fill) && (type != DockType.None))
            {
                this.DockInExistingGroup(item, this.Container.LayoutRoot, type);
                this.Container.Update();
            }
            return item;
        }

        public DocumentPanel AddDocumentPanel(DocumentGroup group)
        {
            if (group == null)
            {
                return null;
            }
            DocumentPanel item = this.Container.CreateDocumentPanel();
            group.Items.Add(item);
            this.CheckMDIState(group, item);
            return item;
        }

        public DocumentPanel AddDocumentPanel(DocumentGroup group, Uri uri)
        {
            if (group == null)
            {
                return null;
            }
            DocumentPanel item = this.Container.CreateDocumentPanel();
            item.Content = uri;
            group.Items.Add(item);
            this.CheckMDIState(group, item);
            return item;
        }

        public DocumentPanel AddDocumentPanel(Point floatLocation, Size floatSize)
        {
            DocumentPanel item = this.Container.CreateDocumentPanel();
            item.FloatSize = floatSize;
            FloatGroup group = DockControllerHelper.BoxIntoFloatGroup(item, this.Container);
            group.FloatLocation = floatLocation;
            this.Container.FloatGroups.Add(group);
            return item;
        }

        public DocumentPanel AddDocumentPanel(Point floatLocation, Size floatSize, Uri uri)
        {
            DocumentPanel item = this.Container.CreateDocumentPanel();
            item.Content = uri;
            item.FloatSize = floatSize;
            FloatGroup group = DockControllerHelper.BoxIntoFloatGroup(item, this.Container);
            group.FloatLocation = floatLocation;
            this.Container.FloatGroups.Add(group);
            return item;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void AddItem(BaseLayoutItem item, BaseLayoutItem target, DockType type)
        {
            if ((item != null) && ((target != null) && (type != DockType.None)))
            {
                using (new NotificationBatch(this.Container))
                {
                    if (type != DockType.Fill)
                    {
                        if (target.Parent != null)
                        {
                            LayoutGroup parent = target.Parent;
                            int index = parent.Items.IndexOf(target);
                            if ((parent.Orientation != type.ToOrientation()) && !parent.IgnoreOrientation)
                            {
                                this.DockInNewGroup(item, target, type);
                            }
                            else
                            {
                                if (type.ToInsertType() == InsertType.After)
                                {
                                    index++;
                                }
                                if (!(parent is DocumentGroup) && (item is DocumentPanel))
                                {
                                    item = DockControllerHelper.BoxIntoDocumentGroup(item, this.Container);
                                }
                                this.InsertCore(parent, item, index);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    if ((type == DockType.Fill) && (target is LayoutGroup))
                    {
                        if (!(target is DocumentGroup) && (item is DocumentPanel))
                        {
                            item = DockControllerHelper.BoxIntoDocumentGroup(item, this.Container);
                        }
                        ((LayoutGroup) target).Add(item);
                    }
                    this.Container.Update();
                    NotificationBatch.Action(this.Container, item.GetRoot(), null);
                }
            }
        }

        public LayoutPanel AddPanel(DockType type)
        {
            LayoutPanel item = this.Container.CreateLayoutPanel();
            if ((type != DockType.Fill) && (type != DockType.None))
            {
                this.DockInExistingGroup(item, this.Container.LayoutRoot, type);
                this.Container.Update();
            }
            return item;
        }

        public LayoutPanel AddPanel(Point floatLocation, Size floatSize)
        {
            LayoutPanel item = this.Container.CreateLayoutPanel();
            item.FloatSize = floatSize;
            FloatGroup group = DockControllerHelper.BoxIntoFloatGroup(item, this.Container);
            group.FloatLocation = floatLocation;
            this.Container.FloatGroups.Add(group);
            this.Container.Update();
            return item;
        }

        private void AddPlaceHolder(BaseLayoutItem item, LayoutGroup group)
        {
            if (group.PlaceHolderHelper.Contains(item))
            {
                if (item.IsFloating)
                {
                    LayoutGroup placeHolderRoot = PlaceHolderHelper.GetPlaceHolderRoot(item, PlaceHolderState.Floating);
                    if (ReferenceEquals(group, placeHolderRoot))
                    {
                        return;
                    }
                }
                group.PlaceHolderHelper.AddPlaceHolderForItem(item);
            }
        }

        protected void BoxGroupItemsInNewGroup(LayoutGroup target, Orientation neededOrientation, DockLayoutManager manager)
        {
            LayoutGroup group = manager.GenerateGroup<LayoutGroup>();
            group.BeginInit();
            group.Orientation = target.Orientation;
            if (target.IsPropertyAssigned(LayoutGroup.AllowSplittersProperty))
            {
                group.AllowSplitters = target.AllowSplitters;
            }
            target.MoveItemsTo(group);
            group.EndInit();
            target.Orientation = neededOrientation;
            target.Add(group);
            this.Container.Update();
        }

        protected virtual FloatGroup BoxToFloatGroupCore(BaseLayoutItem item)
        {
            Rect? initialFloatBounds = item.InitialFloatBounds;
            Rect floatingBounds = (initialFloatBounds != null) ? initialFloatBounds.GetValueOrDefault() : new Rect(MathHelper.IsEmpty(item.FloatSize) ? item.GetSize() : item.FloatSize);
            return this.BoxToFloatGroupCore(item, floatingBounds);
        }

        internal virtual FloatGroup BoxToFloatGroupCore(BaseLayoutItem item, Rect floatingBounds) => 
            DockControllerHelper.BoxIntoFloatGroup(this.Container, item, floatingBounds);

        private AutoHideType CalcAutoHideType(BaseLayoutItem item, AutoHideType actualAutoHideType)
        {
            DockSituation lastDockSituation = item.GetLastDockSituation();
            return ((actualAutoHideType != AutoHideType.Default) ? actualAutoHideType : ((lastDockSituation != null) ? lastDockSituation.AutoHideType : AutoHideType.Default));
        }

        protected bool CanRestoreDockSituation(DockSituation situation) => 
            (situation != null) && ((situation.DockTarget != null) && (ReferenceEquals(situation.Root, situation.DockTarget.GetRoot()) && (!(situation.Root is FloatGroup) ? (!(situation.Root is AutoHideGroup) || this.Container.AutoHideGroups.Contains((AutoHideGroup) situation.Root)) : this.Container.FloatGroups.Contains((FloatGroup) situation.Root))));

        protected bool CheckHideView(BaseLayoutItem item) => 
            DockControllerHelper.CheckHideView(this.Container.GetActualViewOwner(item), item.GetRoot());

        private void CheckMDIState(DocumentGroup dGroup, DocumentPanel panel)
        {
            if (!dGroup.IsTabbed && dGroup.IsMaximized)
            {
                MDIStateHelper.SetMDIState(panel, MDIState.Maximized);
            }
        }

        protected void ClearPlaceHolder(BaseLayoutItem item)
        {
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            item.Accept(new VisitDelegate<BaseLayoutItem>(list.Add));
            foreach (BaseLayoutItem item2 in list)
            {
                LayoutGroup[] affectedGroups = PlaceHolderHelper.GetAffectedGroups(item2);
                PlaceHolderHelper.ClearPlaceHolder(item2);
                Array.ForEach<LayoutGroup>(affectedGroups, group => this.TryUngroupGroup(group));
            }
        }

        protected void ClearPlaceHolder(BaseLayoutItem item, BaseLayoutItem dockTarget)
        {
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            item.Accept(new VisitDelegate<BaseLayoutItem>(list.Add));
            foreach (BaseLayoutItem item2 in list)
            {
                PlaceHolderState state = dockTarget.IsFloating ? PlaceHolderState.Floating : PlaceHolderState.Docked;
                LayoutGroup affectedGroup = PlaceHolderHelper.GetAffectedGroup(item2, state);
                if (state == PlaceHolderState.Docked)
                {
                    PlaceHolderHelper.ClearPlaceHolder(item2, item);
                }
                else
                {
                    PlaceHolderHelper.ClearPlaceHolder(item2, PlaceHolderState.Floating);
                }
                if (!ReferenceEquals(affectedGroup, dockTarget))
                {
                    BaseLayoutItem[] items = new BaseLayoutItem[] { affectedGroup };
                    using (new LogicalTreeLocker(this.Container, items))
                    {
                        this.TryUngroupGroup(affectedGroup);
                    }
                }
            }
        }

        public bool Close(BaseLayoutItem item) => 
            this.CloseCore(item, false);

        public bool CloseAllButThis(BaseLayoutItem item)
        {
            if ((item == null) || ((item.Parent == null) || (item.Parent.Items.Count <= 1)))
            {
                return false;
            }
            BaseLayoutItem[] array = new BaseLayoutItem[item.Parent.Items.Count];
            item.Parent.Items.CopyTo(array, 0);
            bool flag = true;
            foreach (BaseLayoutItem item2 in array)
            {
                if (!ReferenceEquals(item2, item))
                {
                    flag &= this.Container.DockController.CloseEx(item2);
                }
            }
            return flag;
        }

        private bool CloseAutoHideGroup(AutoHideGroup autoHideGroup)
        {
            AutoHideGroup[] second = new AutoHideGroup[] { autoHideGroup };
            if (this.Container.AutoHideGroups.Except<AutoHideGroup>(second).All<AutoHideGroup>(x => x.DockType != autoHideGroup.DockType))
            {
                this.Container.InvalidateView(autoHideGroup);
            }
            bool flag2 = this.Container.AutoHideGroups.Contains(autoHideGroup) && this.Container.AutoHideGroups.Remove(autoHideGroup);
            if (flag2)
            {
                if (autoHideGroup.HasItems)
                {
                    this.AddPlaceHolder(autoHideGroup[0], autoHideGroup);
                }
                this.SetClosed(autoHideGroup);
            }
            return flag2;
        }

        private bool CloseCore(BaseLayoutItem item, bool remove = false)
        {
            bool flag2;
            if ((item == null) || (item.IsClosed || !(item.AllowClose | remove)))
            {
                return false;
            }
            if (!remove && (this.RaiseItemCancelEvent(item, DockLayoutManager.DockItemClosingEvent) || this.OnDockOperationStarting(DockOperation.Close, item, null)))
            {
                return false;
            }
            if ((item is IClosable) && !((IClosable) item).CanClose())
            {
                return false;
            }
            try
            {
                item.IsClosing = true;
                if (item.IsAutoHidden)
                {
                    this.CheckHideView(item);
                }
                IEnumerable<BaseLayoutItem> affectedItems = DockControllerHelper.GetAffectedItems(item);
                bool flag = false;
                BaseLayoutItem[] second = new BaseLayoutItem[] { item.Parent };
                using (new LogicalTreeLocker(this.Container, affectedItems.Concat<BaseLayoutItem>(second).ToArray<BaseLayoutItem>()))
                {
                    using (new NotificationBatch(this.Container))
                    {
                        NotificationBatch.Action(this.Container, item.GetRoot(), null);
                        if (item is FloatGroup)
                        {
                            flag = this.CloseFloatGroup(item as FloatGroup);
                        }
                        if (item is AutoHideGroup)
                        {
                            flag = this.CloseAutoHideGroup(item as AutoHideGroup);
                        }
                        if (!flag)
                        {
                            LayoutGroup parent = item.Parent;
                            if (parent != null)
                            {
                                LayoutGroup root = item.GetRoot();
                                if (parent.Items.Count == 1)
                                {
                                    FloatGroup floatGroup = parent as FloatGroup;
                                    if (floatGroup != null)
                                    {
                                        flag = parent.DestroyOnClosingChildren && this.CloseFloatGroup(floatGroup);
                                    }
                                    AutoHideGroup autoHideGroup = parent as AutoHideGroup;
                                    if (autoHideGroup != null)
                                    {
                                        flag = (!autoHideGroup.HasPersistentGroups && parent.DestroyOnClosingChildren) && this.CloseAutoHideGroup(autoHideGroup);
                                    }
                                }
                                flag = flag || this.CloseItem(item, parent);
                                DockControllerHelper.CheckUpdateView(this.Container, root);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if (flag)
                        {
                            this.Container.RaiseEvent(new DockItemClosedEventArgs(item, affectedItems));
                            this.OnDockOperationComplete(item, DockOperation.Close);
                            if (item is IClosable)
                            {
                                ((IClosable) item).OnClosed();
                            }
                        }
                        this.Container.Update();
                        NotificationBatch.Action(this.Container, item, null);
                        flag2 = flag;
                    }
                }
            }
            finally
            {
                item.IsClosing = false;
            }
            return flag2;
        }

        private bool CloseFloatGroup(FloatGroup floatGroup)
        {
            bool flag2;
            BaseLayoutItem[] items = new BaseLayoutItem[] { floatGroup };
            using (new LogicalTreeLocker(this.Container, items))
            {
                DockLayoutManager linkedManager = this.GetLinkedManager(floatGroup);
                if (!ReferenceEquals(linkedManager, this.Container))
                {
                    flag2 = linkedManager.DockController.Close(floatGroup);
                }
                else
                {
                    bool flag = this.Container.FloatGroups.Contains(floatGroup) && this.Container.FloatGroups.Remove(floatGroup);
                    if (flag)
                    {
                        IEnumerable<BaseLayoutItem> nestedItems = floatGroup.GetNestedItems();
                        List<LayoutPanel> source = this.Container.GetItems().Except<BaseLayoutItem>(nestedItems).OfType<LayoutPanel>().ToList<LayoutPanel>();
                        Func<LayoutPanel, bool> predicate = <>c.<>9__68_0;
                        if (<>c.<>9__68_0 == null)
                        {
                            Func<LayoutPanel, bool> local1 = <>c.<>9__68_0;
                            predicate = <>c.<>9__68_0 = x => x.IsFloating;
                        }
                        Func<LayoutPanel, DateTime> keySelector = <>c.<>9__68_1;
                        if (<>c.<>9__68_1 == null)
                        {
                            Func<LayoutPanel, DateTime> local2 = <>c.<>9__68_1;
                            keySelector = <>c.<>9__68_1 = x => x.LastActivationDateTime;
                        }
                        BaseLayoutItem selectedItem = source.Where<LayoutPanel>(predicate).OrderBy<LayoutPanel, DateTime>(keySelector, ListSortDirection.Descending).FirstOrDefault<LayoutPanel>();
                        if (selectedItem == null)
                        {
                            Func<LayoutPanel, bool> func3 = <>c.<>9__68_2;
                            if (<>c.<>9__68_2 == null)
                            {
                                Func<LayoutPanel, bool> local3 = <>c.<>9__68_2;
                                func3 = <>c.<>9__68_2 = x => x.DockItemState == DockItemState.Docked;
                            }
                            Func<LayoutPanel, DateTime> func4 = <>c.<>9__68_3;
                            if (<>c.<>9__68_3 == null)
                            {
                                Func<LayoutPanel, DateTime> local4 = <>c.<>9__68_3;
                                func4 = <>c.<>9__68_3 = x => x.LastActivationDateTime;
                            }
                            selectedItem = source.Where<LayoutPanel>(func3).OrderBy<LayoutPanel, DateTime>(func4, ListSortDirection.Descending).FirstOrDefault<LayoutPanel>();
                        }
                        if ((selectedItem != null) && (selectedItem.IsTabPage && (!selectedItem.IsSelectedItem && (selectedItem.Parent.SelectedItem != null))))
                        {
                            selectedItem = selectedItem.Parent.SelectedItem;
                        }
                        this.SetClosed(floatGroup);
                        this.Container.Activate(selectedItem);
                    }
                    flag2 = flag;
                }
            }
            return flag2;
        }

        private bool CloseItem(BaseLayoutItem item, LayoutGroup parentGroup)
        {
            bool local6;
            if (parentGroup == null)
            {
                return false;
            }
            DockSituation dockSituation = DockSituation.GetDockSituation(item);
            BaseLayoutItem firstOtherItem = this.GetFirstOtherItem(item, parentGroup.GetItems());
            BaseLayoutItem selectedItem = parentGroup.SelectedItem;
            BaseLayoutItem item4 = ((selectedItem == null) || ReferenceEquals(selectedItem, item)) ? this.GetNextItem(item, parentGroup.GetItems()) : selectedItem;
            if (item.IsActive)
            {
                local6 = true;
            }
            else
            {
                Func<LayoutGroup, bool> evaluator = <>c.<>9__69_0;
                if (<>c.<>9__69_0 == null)
                {
                    Func<LayoutGroup, bool> local1 = <>c.<>9__69_0;
                    evaluator = <>c.<>9__69_0 = delegate (LayoutGroup x) {
                        Func<BaseLayoutItem, bool> predicate = <>c.<>9__69_1;
                        if (<>c.<>9__69_1 == null)
                        {
                            Func<BaseLayoutItem, bool> local1 = <>c.<>9__69_1;
                            predicate = <>c.<>9__69_1 = y => y.IsActive;
                        }
                        return x.GetNestedItems().Any<BaseLayoutItem>(predicate);
                    };
                }
                local6 = (item as LayoutGroup).Return<LayoutGroup, bool>(evaluator, <>c.<>9__69_2 ??= () => false);
            }
            bool flag = local6;
            if ((item4 == null) && item.IsFloating)
            {
                List<BaseLayoutItem> list1 = new List<BaseLayoutItem>();
                list1.Add(item);
                List<BaseLayoutItem> second = list1;
                Func<BaseLayoutItem, bool> predicate = <>c.<>9__69_3;
                if (<>c.<>9__69_3 == null)
                {
                    Func<BaseLayoutItem, bool> local3 = <>c.<>9__69_3;
                    predicate = <>c.<>9__69_3 = x => (x is LayoutPanel) && x.IsFloating;
                }
                Func<BaseLayoutItem, DateTime> keySelector = <>c.<>9__69_4;
                if (<>c.<>9__69_4 == null)
                {
                    Func<BaseLayoutItem, DateTime> local4 = <>c.<>9__69_4;
                    keySelector = <>c.<>9__69_4 = x => ((LayoutPanel) x).LastActivationDateTime;
                }
                item4 = this.Container.GetItems().Except<BaseLayoutItem>(second).Where<BaseLayoutItem>(predicate).OrderBy<BaseLayoutItem, DateTime>(keySelector, ListSortDirection.Descending).FirstOrDefault<BaseLayoutItem>();
            }
            bool flag2 = parentGroup.Items.Contains(item);
            if (flag2 && (item is LayoutPanel))
            {
                this.AddPlaceHolder(item, parentGroup);
            }
            if (flag2)
            {
                ClosingBehavior actualClosingBehavior = DockControllerHelper.GetActualClosingBehavior(this.Container, item);
                DockControllerHelper.SetClosingBehavior(item, actualClosingBehavior);
                if (parentGroup.Remove(item))
                {
                    this.SetClosed(item, parentGroup);
                    BaseLayoutItem dockTarget = parentGroup;
                    if (!ReferenceEquals(this.TryUngroupGroup(parentGroup), parentGroup))
                    {
                        dockTarget = firstOtherItem;
                    }
                    if (item.IsClosed)
                    {
                        item.UpdateDockSituation(dockSituation, dockTarget, null);
                    }
                }
                if (flag && !(parentGroup is AutoHideGroup))
                {
                    BaseLayoutItem item1 = parentGroup.SelectedItem;
                    BaseLayoutItem item6 = item1;
                    if (item1 == null)
                    {
                        BaseLayoutItem local5 = item1;
                        item6 = item4;
                    }
                    this.Container.Activate(item6);
                }
            }
            return flag2;
        }

        public bool CreateNewDocumentGroup(DocumentPanel document, Orientation orientation) => 
            this.CreateNewDocumentGroup((LayoutPanel) document, orientation);

        public bool CreateNewDocumentGroup(LayoutPanel document, Orientation orientation)
        {
            bool flag;
            if ((document == null) || (!(document.Parent is DocumentGroup) || (document.Parent.Items.Count <= 1)))
            {
                return false;
            }
            DocumentGroup parent = (DocumentGroup) document.Parent;
            using (new UpdateBatch(this.Container))
            {
                BaseLayoutItem[] items = new BaseLayoutItem[] { document, parent };
                using (new LogicalTreeLocker(this.Container, items))
                {
                    DocumentGroup group2 = DockControllerHelper.BoxIntoDocumentGroup(document, this.Container);
                    DockControllerHelper.SetForceSizeUpdate(group2, true);
                    try
                    {
                        flag = this.DockCore(group2, parent, (orientation == Orientation.Horizontal) ? DockType.Right : DockType.Bottom, false);
                    }
                    finally
                    {
                        group2.ClearValue(DockControllerHelper.ForceSizeUpdateProperty);
                    }
                }
            }
            return flag;
        }

        private static void DestroyPanelContent(LayoutPanel panel)
        {
            if (DockLayoutManagerParameters.DisposePanelContentAfterRemovingPanel)
            {
                DisposeHelper.DisposeVisualTree(panel.Content as DependencyObject);
            }
        }

        void ILockOwner.Lock()
        {
            this.LockActivate();
        }

        void ILockOwner.Unlock()
        {
            this.UnlockActivate();
        }

        public bool Dock(BaseLayoutItem item)
        {
            if ((item == null) || !item.AllowDock)
            {
                return false;
            }
            DockLayoutManager linkedManager = this.GetLinkedManager(item);
            if (!ReferenceEquals(linkedManager, this.Container))
            {
                return linkedManager.DockController.Dock(item);
            }
            if (item is LayoutGroup)
            {
                LayoutGroup ownerGroup = LayoutGroup.GetOwnerGroup(item);
                if (ownerGroup is AutoHideGroup)
                {
                    item = ownerGroup;
                }
            }
            if (this.OnDockOperationStarting(DockOperation.Dock, item, null))
            {
                return false;
            }
            DockSituation lastDockSituation = item.GetLastDockSituation();
            LayoutGroup root = item.GetRoot();
            DockType dockTypeInContainer = DockControllerHelper.GetDockTypeInContainer(this.Container, item);
            BaseLayoutItem layoutRoot = this.Container.LayoutRoot;
            BaseLayoutItem itemToDock = this.GetItemToDock(item);
            LayoutGroup objB = null;
            PlaceHolder holder = (itemToDock != null) ? PlaceHolderHelper.GetPlaceHolder(itemToDock, PlaceHolderState.Docked) : null;
            if ((holder != null) && PlaceHolderHelper.CanRestoreLayoutHierarchy(itemToDock, PlaceHolderState.Docked))
            {
                dockTypeInContainer = DockType.None;
                objB = holder.Parent.GetRoot();
            }
            else if ((lastDockSituation != null) && (lastDockSituation.DockTarget != null))
            {
                objB = lastDockSituation.DockTarget.GetRoot();
                bool flag = ReferenceEquals(lastDockSituation.Root, objB) && this.Container.IsViewCreated(objB);
                if (!ReferenceEquals(objB, root) & flag)
                {
                    if (lastDockSituation.Type != DockType.None)
                    {
                        dockTypeInContainer = lastDockSituation.Type;
                    }
                    layoutRoot = lastDockSituation.DockTarget;
                }
            }
            if ((root is FloatGroup) && ((lastDockSituation != null) && ReferenceEquals(lastDockSituation.Root, objB)))
            {
                if (lastDockSituation.Width.IsAuto)
                {
                    item.ItemWidth = GridLength.Auto;
                }
                if (lastDockSituation.Height.IsAuto)
                {
                    item.ItemHeight = GridLength.Auto;
                }
            }
            return this.DockCore(item, layoutRoot, dockTypeInContainer, true);
        }

        public bool Dock(BaseLayoutItem item, BaseLayoutItem target, DockType type)
        {
            if ((item == null) || (target == null))
            {
                return false;
            }
            if (!target.GetAllowDockToCurrentItem() && (type == DockType.Fill))
            {
                return false;
            }
            DockLayoutManager linkedManager = this.GetLinkedManager(target);
            return (ReferenceEquals(linkedManager, this.Container) ? (!this.OnDockOperationStarting(DockOperation.Dock, item, target) ? this.DockCore(item, target, type, true) : false) : linkedManager.DockController.Dock(item, target, type));
        }

        public bool DockAsDocument(BaseLayoutItem item, BaseLayoutItem target, DockType type)
        {
            bool flag;
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
            using (new LogicalTreeLocker(this.Container, items))
            {
                using (new ActivateBatch(this.Container))
                {
                    LayoutGroup parent = item.Parent;
                    LayoutGroup root = item.GetRoot();
                    DocumentGroup group3 = DockControllerHelper.BoxIntoDocumentGroup(item, this.Container);
                    this.ClearPlaceHolder(item);
                    using (target.LockDockingTarget())
                    {
                        this.TryUngroupGroup(parent);
                        this.TryUngroupGroup(root);
                    }
                    flag = this.Dock(group3, target, type);
                }
            }
            return flag;
        }

        private bool DockCore(BaseLayoutItem item, BaseLayoutItem target, DockType type, bool shouldRaiseCompleted = true)
        {
            if (!item.AllowDock)
            {
                return false;
            }
            LayoutGroup group = (target != null) ? LayoutGroup.GetOwnerGroup(target) : null;
            if (group != null)
            {
                target = group;
            }
            if (target == null)
            {
                this.Container.EnsureLayoutRoot();
                target = this.Container.LayoutRoot;
            }
            using (new NotificationBatch(this.Container))
            {
                bool flag;
                using (new UpdateBatch(this.Container))
                {
                    using (new ActivateBatch(this.Container))
                    {
                        BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
                        using (new LogicalTreeLocker(this.Container, items))
                        {
                            FloatGroup group3;
                            AutoHideGroup group4;
                            this.EnsureDockItemState(item);
                            LayoutGroup parent = item.Parent;
                            BaseLayoutItem item2 = this.GetItemToDock(item, out group3, out group4);
                            LayoutGroup root = target.GetRoot();
                            LayoutGroup targetParent = target.Parent;
                            LayoutGroup affectedGroup = PlaceHolderHelper.GetAffectedGroup(item, PlaceHolderState.Docked);
                            if (type != DockType.None)
                            {
                                using (target.LockDockingTarget())
                                {
                                    this.ClearPlaceHolder(item2, target);
                                }
                            }
                            BaseLayoutItem focusedItem = this.GetFocusedItem(item2);
                            bool wasHidden = this.CheckHideView(item);
                            BaseLayoutItem actualSelectedItem = DockControllerHelper.GetActualSelectedItem(item);
                            if (type == DockType.None)
                            {
                                flag = this.DockToPlaceHolder(item2);
                            }
                            else if (type == DockType.Fill)
                            {
                                flag = this.DockToFillCore(item2, target);
                            }
                            else
                            {
                                this.UpdateDockTarget(type, target, targetParent);
                                flag = this.DockToSideCore(item2, target, type);
                            }
                            if (flag)
                            {
                                if (type == DockType.Fill)
                                {
                                    this.UpdateDockTarget(type, target, targetParent);
                                }
                                if (actualSelectedItem.Parent != null)
                                {
                                    this.ActivationHelper.SelectInGroup(actualSelectedItem, actualSelectedItem.Parent);
                                }
                                NotificationBatch.Action(this.Container, parent.GetRoot(), null);
                                this.UpdateDecomposedItems();
                                if (!ReferenceEquals(target, parent))
                                {
                                    this.TryUngroupGroup(parent);
                                }
                                if (!ReferenceEquals(group3, root))
                                {
                                    this.TryUngroupGroup(group3);
                                }
                                if (group4 != null)
                                {
                                    this.TryUngroupGroup(group4);
                                    LayoutGroup.SetOwnerGroup(item2, null);
                                    if (item2 is LayoutGroup)
                                    {
                                        this.Container.DecomposedItems.Remove((LayoutGroup) item2);
                                    }
                                }
                                if (affectedGroup != null)
                                {
                                    this.TryUngroupGroup(affectedGroup);
                                }
                                this.OnDockComplete(item2, type, wasHidden);
                                if (focusedItem != null)
                                {
                                    this.Container.FocusItem(focusedItem, false);
                                }
                                DockControllerHelper.CheckUpdateView(this.Container, root);
                                this.Container.Update();
                                if (shouldRaiseCompleted)
                                {
                                    this.OnDockOperationComplete(item2, DockOperation.Dock);
                                }
                                NotificationBatch.Action(this.Container, item2.GetRoot(), null);
                            }
                        }
                    }
                }
                return flag;
            }
        }

        protected bool DockInExistingGroup(BaseLayoutItem item, LayoutGroup target, DockType type)
        {
            if (target == null)
            {
                return false;
            }
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
            using (new LogicalTreeLocker(this.Container, items))
            {
                Orientation neededOrientation = type.ToOrientation();
                int index = (type.ToInsertType() == InsertType.Before) ? 0 : 1;
                bool flag = target.GetIsDocumentHost() && target.IsRoot();
                if ((!target.IgnoreOrientation && ((target.Orientation != neededOrientation) && ((target.Items.Count > 1) || target.HasPlaceHolders))) || (target.IsControlItemsHost || flag))
                {
                    this.BoxGroupItemsInNewGroup(target, neededOrientation, this.Container);
                }
                else
                {
                    if (target.Orientation != neededOrientation)
                    {
                        target.Orientation = neededOrientation;
                    }
                    index = (type.ToInsertType() == InsertType.Before) ? 0 : target.Items.Count;
                }
                if (item is DocumentPanel)
                {
                    item = DockControllerHelper.BoxIntoDocumentGroup(item, this.Container);
                }
                return this.InsertItemInGroup(target, item, index, false);
            }
        }

        private bool DockInExistingGroup(BaseLayoutItem item, LayoutGroup target, int targetPosition)
        {
            if (target == null)
            {
                return false;
            }
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
            using (new LogicalTreeLocker(this.Container, items))
            {
                if ((item is DocumentPanel) && ((target.ItemType != LayoutItemType.DocumentPanelGroup) && !(target is FloatGroup)))
                {
                    item = DockControllerHelper.BoxIntoDocumentGroup(item, this.Container);
                }
                return this.InsertItemInGroup(target, item, targetPosition, false);
            }
        }

        protected bool DockInNewGroup(BaseLayoutItem item, BaseLayoutItem target, DockType type)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
            using (new LogicalTreeLocker(this.Container, items))
            {
                LayoutGroup parent = target.Parent;
                PlaceHolderHelper.StorePositionAndRemove(parent, target);
                LayoutGroup group = DockControllerHelper.BoxIntoGroup(target, type.ToOrientation(), parent.IsSplittersEnabled, this.Container);
                if (item is DocumentPanel)
                {
                    item = DockControllerHelper.BoxIntoDocumentGroup(item, this.Container);
                }
                bool flag = this.InsertItemInGroup(group, item, (type.ToInsertType() == InsertType.Before) ? 0 : 1, false);
                PlaceHolderHelper.InsertItemToPosition(parent, target, group);
                return flag;
            }
        }

        protected virtual bool DockToFillCore(BaseLayoutItem itemToDock, BaseLayoutItem target)
        {
            LayoutGroup parent = target as LayoutGroup;
            if ((parent == null) && (target.Parent is TabbedGroup))
            {
                parent = target.Parent;
            }
            return (((parent == null) || (!(parent is TabbedGroup) && parent.IsControlItemsHost)) ? this.FillNewTabbedGroup(itemToDock, target) : this.FillExistingGroup(itemToDock, parent));
        }

        protected virtual bool DockToPlaceHolder(BaseLayoutItem itemToDock)
        {
            bool flag = this.RestoreToPlaceHolder(itemToDock, false);
            if (flag)
            {
                this.ClearPlaceHolder(itemToDock);
            }
            return flag;
        }

        protected virtual bool DockToSideCore(BaseLayoutItem itemToDock, BaseLayoutItem target, DockType type)
        {
            LayoutGroup parent = target as LayoutGroup;
            if ((parent == null) && ((target.Parent.Orientation == type.ToOrientation()) && (target.Parent.Items.Count == 1)))
            {
                parent = target.Parent;
            }
            return (((parent == null) || (parent is TabbedGroup)) ? this.DockInNewGroup(itemToDock, target, type) : this.DockInExistingGroup(itemToDock, parent, type));
        }

        private void EnsureDockItemState(BaseLayoutItem item)
        {
            if (item.Closed && !this.Container.ClosedPanels.Contains<BaseLayoutItem>(item))
            {
                item.SetCurrentValue(BaseLayoutItem.ClosedProperty, false);
            }
            LayoutPanel panel = item as LayoutPanel;
            if ((panel != null) && (panel.AutoHidden && !panel.IsAutoHidden))
            {
                panel.AutoHidden = false;
            }
        }

        private bool FillExistingGroup(BaseLayoutItem item, LayoutGroup target)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
            using (new LogicalTreeLocker(this.Container, items))
            {
                LayoutGroup group = item as LayoutGroup;
                if ((group != null) && ((group.Parent != null) && group.ShouldStayInTree()))
                {
                    this.AddPlaceHolder(group, group.Parent);
                }
                bool flag = (item.Parent == null) || item.Parent.Remove(item);
                if (flag)
                {
                    if ((item is DocumentPanel) && (target.ItemType == LayoutItemType.Group))
                    {
                        item = DockControllerHelper.BoxIntoDocumentGroup(item, this.Container);
                        target.Add(item);
                    }
                    else if (target is TabbedGroup)
                    {
                        target.AddRange(DockControllerHelper.Decompose(item));
                    }
                    else
                    {
                        DockSituation lastDockSituation = item.GetLastDockSituation();
                        if (lastDockSituation != null)
                        {
                            if (lastDockSituation.Width.IsAbsolute)
                            {
                                item.ItemWidth = lastDockSituation.Width;
                            }
                            if (lastDockSituation.Height.IsAbsolute)
                            {
                                item.ItemHeight = lastDockSituation.Height;
                            }
                        }
                        target.Add(item);
                    }
                }
                return flag;
            }
        }

        private bool FillExistingGroup(LayoutGroup group, LayoutGroup target, int index)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { group, target };
            using (new LogicalTreeLocker(this.Container, items))
            {
                bool flag = (group.Parent == null) || group.Parent.Remove(group);
                if (flag)
                {
                    BaseLayoutItem selectedItem = group.SelectedItem;
                    BaseLayoutItem[] itemArray2 = DockControllerHelper.Decompose(group);
                    int num = 0;
                    while (true)
                    {
                        if (num >= itemArray2.Length)
                        {
                            if (selectedItem != null)
                            {
                                this.ActivationHelper.SelectInGroup(selectedItem, target);
                            }
                            break;
                        }
                        BaseLayoutItem item = itemArray2[num];
                        target.Insert(index++, item);
                        num++;
                    }
                }
                return flag;
            }
        }

        private bool FillNewTabbedGroup(BaseLayoutItem item, BaseLayoutItem target)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
            using (new LogicalTreeLocker(this.Container, items))
            {
                bool flag = (item.Parent == null) || item.Parent.Remove(item);
                if (flag)
                {
                    IEnumerable<LayoutGroup> enumerable;
                    LayoutGroup parent = target.Parent;
                    PlaceHolderHelper.StorePositionAndRemove(parent, target);
                    TabbedGroup newGroup = (target.ItemType == LayoutItemType.Document) ? DockControllerHelper.BoxIntoDocumentGroup(target, this.Container) : DockControllerHelper.BoxIntoTabbedGroup(target, this.Container);
                    PlaceHolderHelper.DecomposeTo(item, newGroup, out enumerable);
                    enumerable.ForEach<LayoutGroup>(delegate (LayoutGroup x) {
                        if (x.ShouldStayInTree())
                        {
                            this.Container.DecomposedItems.AddOnce(x);
                        }
                    });
                    PlaceHolderHelper.InsertItemToPosition(parent, target, newGroup);
                    this.ActivationHelper.SelectInGroup(item, newGroup);
                }
                return flag;
            }
        }

        public FloatGroup Float(BaseLayoutItem item)
        {
            FloatGroup group3;
            if ((item == null) || ((item.Parent == null) || !item.AllowFloat))
            {
                return null;
            }
            if (this.OnDockOperationStarting(DockOperation.Float, item, null))
            {
                return null;
            }
            if (item.IsAutoHidden)
            {
                this.CheckHideView(item);
            }
            DockSituation dockSituation = DockSituation.GetDockSituation(item);
            LayoutGroup parent = item.Parent;
            BaseLayoutItem firstOtherItem = this.GetFirstOtherItem(item, parent.GetItems());
            FloatGroup group2 = null;
            BaseLayoutItem layout = this.GetLayout(item);
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, parent, layout };
            using (new LogicalTreeLocker(this.Container, items))
            {
                using (new NotificationBatch(this.Container))
                {
                    using (new UpdateBatch(this.Container))
                    {
                        using (new ActivateBatch(this.Container))
                        {
                            item.BeginFloating();
                            bool flag = this.HasFocus(item);
                            int startingIndex = parent.IndexFromItem(item);
                            bool allowFloat = item.AllowFloat;
                            if (allowFloat && !parent.IsFloating)
                            {
                                this.AddPlaceHolder(item, parent);
                            }
                            if (allowFloat && parent.Remove(item))
                            {
                                BaseLayoutItem itemToFocus = this.GetItemToFocus(parent, startingIndex);
                                if (flag && (itemToFocus != null))
                                {
                                    this.Container.LockItemActivationOnFocus();
                                    this.Container.FocusItem(itemToFocus, false);
                                }
                                BaseLayoutItem item5 = parent;
                                NotificationBatch.Action(this.Container, item5.GetRoot(), null);
                                if (!ReferenceEquals(this.TryUngroupGroup(parent), parent) && !(item5 is AutoHideGroup))
                                {
                                    item5 = firstOtherItem;
                                }
                                item.UpdateDockSituation(dockSituation, item5, null);
                                DockControllerHelper.CheckUpdateView(this.Container, dockSituation.Root);
                                group2 = this.BoxToFloatGroupCore(item);
                                group2.Manager = this.Container;
                                this.Container.FloatGroups.Add(group2);
                                if (flag)
                                {
                                    this.Container.FocusItem(item, true);
                                }
                                item.EndFloating();
                                this.Container.Update();
                                this.OnDockOperationComplete(item, DockOperation.Float);
                                NotificationBatch.Action(this.Container, item.GetRoot(), null);
                            }
                            group3 = group2;
                        }
                    }
                }
            }
            return group3;
        }

        private DockType GetDockType(BaseLayoutItem item, LayoutGroup parentGroup)
        {
            if (parentGroup is TabbedGroup)
            {
                return DockType.Fill;
            }
            bool flag2 = parentGroup.Orientation == Orientation.Horizontal;
            return ((parentGroup.Items.IndexOf(item) < (parentGroup.Items.Count * 0.5)) ? (flag2 ? DockType.Left : DockType.Top) : (flag2 ? DockType.Right : DockType.Bottom));
        }

        private BaseLayoutItem GetFirstOtherItem(BaseLayoutItem item, BaseLayoutItem[] itemsBeforeUngroup) => 
            Array.Find<BaseLayoutItem>(itemsBeforeUngroup, i => !ReferenceEquals(i, item));

        private BaseLayoutItem GetFloatGroupContent(BaseLayoutItem item)
        {
            FloatGroup target = (item.IsFloatingRootItem ? ((FloatGroup) item.GetRoot()) : ((FloatGroup) item)) as FloatGroup;
            if (target != null)
            {
                if ((target.Items.Count > 1) || target.HasPlaceHolders)
                {
                    this.BoxGroupItemsInNewGroup(target, target.Orientation, this.Container);
                }
                item = target.Items[0];
            }
            return item;
        }

        private BaseLayoutItem GetFocusedItem(BaseLayoutItem item)
        {
            if (this.HasFocus(item))
            {
                return item;
            }
            LayoutGroup group = item as LayoutGroup;
            if (group != null)
            {
                using (IEnumerator<BaseLayoutItem> enumerator = group.GetNestedItems().GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        BaseLayoutItem current = enumerator.Current;
                        if (this.HasFocus(current))
                        {
                            return current;
                        }
                    }
                }
            }
            return null;
        }

        public static bool GetIsRestoring(DependencyObject d) => 
            (bool) d.GetValue(IsRestoringProperty);

        private BaseLayoutItem GetItemToDock(BaseLayoutItem item)
        {
            BaseLayoutItem item2 = item;
            FloatGroup group = item as FloatGroup;
            if ((group != null) && (group.Items.Count == 1))
            {
                item2 = group.Items[0];
            }
            AutoHideGroup group2 = item as AutoHideGroup;
            if (group2 != null)
            {
                BaseLayoutItem[] items = group2.GetItems();
                BaseLayoutItem originalItemForAutoHideGroup = this.GetOriginalItemForAutoHideGroup(group2);
                if (originalItemForAutoHideGroup is TabbedGroup)
                {
                    item2 = originalItemForAutoHideGroup;
                }
                else if (items.Length == 1)
                {
                    item2 = items[0];
                }
            }
            return item2;
        }

        private BaseLayoutItem GetItemToDock(BaseLayoutItem item, out FloatGroup floatRoot, out AutoHideGroup autoHideRoot)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { item };
            using (new LogicalTreeLocker(this.Container, items))
            {
                BaseLayoutItem itemToDock = item;
                floatRoot = item.GetRoot() as FloatGroup;
                if (floatRoot != null)
                {
                    itemToDock = this.GetFloatGroupContent(item);
                    this.SaveFloatSize(floatRoot, itemToDock);
                }
                AutoHideGroup group1 = item as AutoHideGroup;
                AutoHideGroup ownerGroup = group1;
                if (group1 == null)
                {
                    AutoHideGroup local1 = group1;
                    ownerGroup = LayoutGroup.GetOwnerGroup(item) as AutoHideGroup;
                }
                autoHideRoot = ownerGroup;
                if (autoHideRoot != null)
                {
                    BaseLayoutItem[] itemArray = autoHideRoot.GetItems();
                    BaseLayoutItem originalItemForAutoHideGroup = this.GetOriginalItemForAutoHideGroup(autoHideRoot);
                    bool flag = !(originalItemForAutoHideGroup is TabbedGroup);
                    if (((itemArray.Length == 1) & flag) && !autoHideRoot.HasPlaceHolders)
                    {
                        itemToDock = itemArray[0];
                    }
                    else
                    {
                        int selectedTabIndex = autoHideRoot.SelectedTabIndex;
                        this.CheckHideView(autoHideRoot);
                        TabbedGroup newGroup = flag ? this.Container.GenerateGroup<TabbedGroup>() : ((TabbedGroup) originalItemForAutoHideGroup);
                        if (flag)
                        {
                            newGroup.BeginInit();
                        }
                        newGroup.Manager = this.Container;
                        PlaceHolderHelper.DecomposeTo(autoHideRoot, newGroup);
                        newGroup.SelectedTabIndex = selectedTabIndex;
                        newGroup.UpdateDockSituation(autoHideRoot.GetLastDockSituation(), this.Container.LayoutRoot, null);
                        AutoHideGroup.SetAutoHideType(newGroup, AutoHideGroup.GetAutoHideType(autoHideRoot));
                        if (flag)
                        {
                            newGroup.EndInit();
                        }
                        itemToDock = newGroup;
                        PlaceHolderHelper.ClearPlaceHolder(autoHideRoot);
                        autoHideRoot.HasPersistentGroups = false;
                    }
                }
                return itemToDock;
            }
        }

        private BaseLayoutItem GetItemToFocus(LayoutGroup group, int startingIndex)
        {
            if (group.IsTabHost && (group.SelectedItem != null))
            {
                return group.SelectedItem;
            }
            BaseLayoutItem itemToFocus = this.GetItemToFocus(group.Items, startingIndex);
            if (itemToFocus == null)
            {
                BaseLayoutItem[] items = this.Container.GetItems();
                itemToFocus = this.GetItemToFocus(items, Array.IndexOf<BaseLayoutItem>(items, group));
                if ((itemToFocus != null) && ((itemToFocus.Parent != null) && itemToFocus.Parent.IsTabHost))
                {
                    itemToFocus = itemToFocus.Parent.SelectedItem;
                }
            }
            return itemToFocus;
        }

        private BaseLayoutItem GetItemToFocus(IEnumerable<BaseLayoutItem> items, int startingIndex)
        {
            Func<BaseLayoutItem, bool> predicate = <>c.<>9__83_0;
            if (<>c.<>9__83_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c.<>9__83_0;
                predicate = <>c.<>9__83_0 = x => (x is LayoutPanel) && !x.IsAutoHidden;
            }
            BaseLayoutItem item = items.Skip<BaseLayoutItem>(startingIndex).FirstOrDefault<BaseLayoutItem>(predicate);
            if (item == null)
            {
                Func<BaseLayoutItem, bool> func2 = <>c.<>9__83_1;
                if (<>c.<>9__83_1 == null)
                {
                    Func<BaseLayoutItem, bool> local2 = <>c.<>9__83_1;
                    func2 = <>c.<>9__83_1 = x => (x is LayoutPanel) && !x.IsAutoHidden;
                }
                item = items.Take<BaseLayoutItem>(startingIndex).Reverse<BaseLayoutItem>().FirstOrDefault<BaseLayoutItem>(func2);
            }
            return item;
        }

        private BaseLayoutItem GetLayout(BaseLayoutItem item)
        {
            BaseLayoutItem panelLayout = this.GetPanelLayout(item as LayoutPanel);
            LayoutGroup group = item as LayoutGroup;
            if (group != null)
            {
                panelLayout = this.GetPanelLayout(group.SelectedItem as LayoutPanel);
            }
            return panelLayout;
        }

        private DockLayoutManager GetLinkedManager(BaseLayoutItem target)
        {
            Func<BaseLayoutItem, DockLayoutManager> evaluator = <>c.<>9__85_0;
            if (<>c.<>9__85_0 == null)
            {
                Func<BaseLayoutItem, DockLayoutManager> local1 = <>c.<>9__85_0;
                evaluator = <>c.<>9__85_0 = x => x.Manager;
            }
            DockLayoutManager manager = target.Return<BaseLayoutItem, DockLayoutManager>(evaluator, <>c.<>9__85_1 ??= ((Func<DockLayoutManager>) (() => null)));
            return (((manager == null) || manager.IsDisposing) ? this.Container : manager);
        }

        private BaseLayoutItem GetNextItem(BaseLayoutItem item, BaseLayoutItem[] items)
        {
            BaseLayoutItem item2 = null;
            BaseLayoutItem item3 = null;
            int index = Array.IndexOf<BaseLayoutItem>(items, item);
            int num2 = index + 1;
            int num3 = index - 1;
            if (items.IsValidIndex<BaseLayoutItem>(num2))
            {
                item2 = items[num2];
            }
            if (items.IsValidIndex<BaseLayoutItem>(num3))
            {
                item3 = items[num3];
            }
            return (item3 ?? item2);
        }

        private BaseLayoutItem GetOriginalItemForAutoHideGroup(AutoHideGroup autoHideRoot)
        {
            PlaceHolder placeHolder = PlaceHolderHelper.GetPlaceHolder(autoHideRoot, PlaceHolderState.Unset);
            return placeHolder?.Parent;
        }

        private LayoutGroup GetPanelLayout(LayoutPanel panel) => 
            ((panel == null) || !panel.IsControlItemsHost) ? null : panel.Layout;

        private bool HasFocus(BaseLayoutItem item)
        {
            LayoutPanel panel = item as LayoutPanel;
            if (panel == null)
            {
                return false;
            }
            UIElement element = panel.Control ?? panel.ContentPresenter;
            return ((element != null) && (KeyboardFocusHelper.IsKeyboardFocused(element) || KeyboardFocusHelper.IsKeyboardFocusWithin(element)));
        }

        public bool Hide(BaseLayoutItem item) => 
            this.HideCore(item, System.Windows.Controls.Dock.Left, true);

        public bool Hide(BaseLayoutItem item, AutoHideGroup target)
        {
            DockLayoutManager linkedManager = this.GetLinkedManager(target);
            return (ReferenceEquals(linkedManager, this.Container) ? ((target == null) ? this.Hide(item) : this.HideCore(item, target, target.DockType, false)) : linkedManager.DockController.Hide(item, target));
        }

        public bool Hide(BaseLayoutItem item, System.Windows.Controls.Dock dock) => 
            this.HideCore(item, dock, false);

        protected bool HideCore(BaseLayoutItem item, System.Windows.Controls.Dock dock, bool calcDock) => 
            this.HideCore(item, null, dock, calcDock);

        protected bool HideCore(BaseLayoutItem item, AutoHideGroup target, System.Windows.Controls.Dock dock, bool calcDock)
        {
            if ((((item == null) || (item.IsAutoHidden && ReferenceEquals(item.Parent, target))) || !item.AllowHide) || (item is AutoHideGroup))
            {
                return false;
            }
            if (item.IsClosed && !this.Restore(item))
            {
                return false;
            }
            if (item is FloatGroup)
            {
                FloatGroup group2 = (FloatGroup) item;
                item = this.GetFloatGroupContent(item);
                item.FloatSize = group2.FloatSize;
            }
            if (!item.AllowHide || (this.RaiseItemCancelEvent(item, DockLayoutManager.DockItemHidingEvent) || this.OnDockOperationStarting(DockOperation.Hide, item, null)))
            {
                return false;
            }
            DockSituation situation = calcDock ? DockSituation.GetDockSituation(item) : DockSituation.GetDockSituation(item, dock);
            LayoutGroup parent = item.Parent;
            BaseLayoutItem firstOtherItem = this.GetFirstOtherItem(item, parent.GetItems());
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, parent };
            using (new LogicalTreeLocker(this.Container, items))
            {
                using (new NotificationBatch(this.Container))
                {
                    using (new ActivateBatch(this.Container))
                    {
                        using (new UpdateBatch(this.Container))
                        {
                            if (calcDock)
                            {
                                this.AddPlaceHolder(item, parent);
                            }
                            else
                            {
                                this.ClearPlaceHolder(item);
                            }
                            AutoHideType autoHideType = AutoHideGroup.GetAutoHideType(item);
                            if (parent.Remove(item))
                            {
                                BaseLayoutItem item3 = parent;
                                NotificationBatch.Action(this.Container, item3.GetRoot(), null);
                                if (!ReferenceEquals(this.TryUngroupGroup(parent), parent))
                                {
                                    item3 = firstOtherItem;
                                }
                                item.UpdateDockSituation(situation, item3, null);
                                if (!calcDock)
                                {
                                    item.UpdateAutoHideSituation(dock.ToAutoHideType());
                                }
                                AutoHideType type = calcDock ? this.CalcAutoHideType(item, autoHideType) : AutoHideType.Default;
                                System.Windows.Controls.Dock dock2 = (type != AutoHideType.Default) ? type.ToDock() : situation.DesiredDock;
                                AutoHideGroup group3 = target;
                                if ((group3 != null) && !group3.IsUngroupped)
                                {
                                    group3.Add(item);
                                }
                                else
                                {
                                    group3 = DockControllerHelper.BoxIntoAutoHideGroup(item, dock2, this.Container);
                                    group3.UpdateDockSituation(situation, item3, item);
                                    TabbedGroup group4 = item as TabbedGroup;
                                    if (group4 != null)
                                    {
                                        LayoutGroup.SetOwnerGroup(group4, group3);
                                        this.Container.DecomposedItems.AddOnce(group4);
                                        PlaceHolderHelper.AddFakePlaceHolderForItem(group4, group3, PlaceHolderState.Unset);
                                        group3.HasPersistentGroups = !group4.DestroyOnClosingChildren;
                                    }
                                    this.Container.AutoHideGroups.Add(group3);
                                }
                                DockControllerHelper.CheckUpdateView(this.Container, situation.Root);
                                DockControllerHelper.CheckUpdateView(this.Container, group3.GetRoot());
                                this.RaiseItemEvent(item, DockLayoutManager.DockItemHiddenEvent);
                                this.Container.Update();
                                this.OnDockOperationComplete(item, DockOperation.Hide);
                                NotificationBatch.Action(this.Container, group3, null);
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        public bool Insert(LayoutGroup group, BaseLayoutItem item, int index) => 
            this.InsertCore(group, item, index);

        protected bool InsertCore(LayoutGroup group, BaseLayoutItem item, int index)
        {
            bool flag3;
            if ((item == null) || ((group == null) || (index == -1)))
            {
                return false;
            }
            LayoutGroup ownerGroup = LayoutGroup.GetOwnerGroup(group);
            if (ownerGroup != null)
            {
                group = ownerGroup;
            }
            LayoutGroup parent = item.Parent;
            LayoutGroup root = group.GetRoot();
            using (new UpdateBatch(this.Container))
            {
                using (new ActivateBatch(this.Container))
                {
                    if (!ReferenceEquals(parent, group))
                    {
                        FloatGroup group5;
                        AutoHideGroup group6;
                        if (item.ItemType == LayoutItemType.ControlItem)
                        {
                            if (!item.AllowMove)
                            {
                                return false;
                            }
                        }
                        else if (!item.AllowDock)
                        {
                            return false;
                        }
                        BaseLayoutItem item2 = this.GetItemToDock(item, out group5, out group6);
                        this.CheckHideView(parent);
                        if (!group.IsFloating)
                        {
                            this.ClearPlaceHolder(item2, group);
                        }
                        bool flag = (!(group is TabbedGroup) || !(item2 is LayoutGroup)) ? this.InsertItemInGroup(group, item2, index, false) : this.FillExistingGroup((LayoutGroup) item2, group, index);
                        if (flag)
                        {
                            this.TryUngroupGroup(parent);
                            if (group5 != null)
                            {
                                this.TryUngroupGroup(group5);
                            }
                            if (group6 != null)
                            {
                                this.TryUngroupGroup(group6);
                            }
                            this.Container.InvalidateView(root);
                            this.Container.Update();
                        }
                        flag3 = flag;
                    }
                    else
                    {
                        bool flag2 = group.MoveItem(index, item);
                        if (flag2)
                        {
                            this.Container.InvalidateView(root);
                            this.Container.Update();
                        }
                        flag3 = flag2;
                    }
                }
            }
            return flag3;
        }

        protected bool InsertItemInGroup(LayoutGroup group, BaseLayoutItem item, int index, bool forceSizeUpdate = false)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, group };
            using (new LogicalTreeLocker(this.Container, items))
            {
                return DockControllerHelper.InsertItemInGroup(group, item, index, forceSizeUpdate);
            }
        }

        private void LockActivate()
        {
            this.lockActivateCounter++;
        }

        public bool MoveToDocumentGroup(DocumentPanel document, bool next) => 
            this.MoveToDocumentGroup((LayoutPanel) document, next);

        public bool MoveToDocumentGroup(LayoutPanel document, bool next)
        {
            if (document == null)
            {
                return false;
            }
            DocumentGroup target = next ? DockControllerHelper.GetNextNotEmptyDocumentGroup(document.Parent) : DockControllerHelper.GetPreviousNotEmptyDocumentGroup(document.Parent);
            if (target == null)
            {
                return false;
            }
            this.Dock(document, target, DockType.Fill);
            DockControllerHelper.CheckUpdateView(this.Container, document.GetRoot());
            this.Container.Update();
            return true;
        }

        protected void OnDisposing()
        {
            Ref.Dispose<ActiveItemHelper>(ref this.ActivationHelper);
            this.containerCore = null;
            this.activeItemCore = null;
        }

        protected void OnDockComplete(BaseLayoutItem item, DockType type, bool wasHidden)
        {
            if (item.IsClosed)
            {
                BaseLayoutItem[] items = new BaseLayoutItem[] { item };
                using (new LogicalTreeLocker(this.Container, items))
                {
                    this.Container.ClosedPanels.Remove((LayoutPanel) item);
                }
            }
            item.UpdateDockSituation(item.Parent, type);
            if (!wasHidden)
            {
                item.UpdateAutoHideSituation();
            }
        }

        private void OnDockOperationComplete(BaseLayoutItem itemToDock, DockOperation dockOperation)
        {
            this.Container.RaiseDockOperationCompletedEvent(dockOperation, itemToDock);
        }

        private bool OnDockOperationStarting(DockOperation dockOperation, BaseLayoutItem item, BaseLayoutItem target = null) => 
            this.Container.RaiseDockOperationStartingEvent(dockOperation, item, target);

        private void RaiseActiveItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            this.Container.RaiseDockItemActivatedEvent(item, oldItem);
        }

        private bool RaiseItemCancelEvent(BaseLayoutItem item, RoutedEvent routedEvent) => 
            this.Container.RaiseItemCancelEvent(item, routedEvent);

        private void RaiseItemEvent(BaseLayoutItem item, RoutedEvent routedEvent)
        {
            this.Container.RaiseItemEvent(item, routedEvent);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveItem(BaseLayoutItem item)
        {
            if (item != null)
            {
                using (new NotificationBatch(this.Container))
                {
                    object target = item;
                    LayoutGroup parent = item.Parent;
                    if (parent != null)
                    {
                        target = item.GetRoot();
                        parent.Remove(item);
                        this.TryUngroupGroup(parent);
                    }
                    else
                    {
                        LayoutGroup group2 = item as LayoutGroup;
                        if ((group2 != null) && (group2.ParentPanel != null))
                        {
                            target = group2.ParentPanel.GetRoot();
                            group2.ParentPanel.Content = null;
                        }
                        else
                        {
                            if (ReferenceEquals(item, this.Container.LayoutRoot))
                            {
                                target = this.Container;
                                this.Container.LayoutRoot = null;
                            }
                            if (item.IsClosed)
                            {
                                this.Container.ClosedPanels.Remove(item as LayoutPanel);
                            }
                            if (item is AutoHideGroup)
                            {
                                this.Container.AutoHideGroups.Remove(item as AutoHideGroup);
                            }
                            if (item is FloatGroup)
                            {
                                this.Container.FloatGroups.Remove(item as FloatGroup);
                            }
                        }
                    }
                    NotificationBatch.Action(this.Container, target, null);
                    this.Container.Update();
                }
            }
        }

        public void RemovePanel(LayoutPanel panel)
        {
            if (panel != null)
            {
                using (new NotificationBatch(this.Container))
                {
                    LayoutGroup root = panel.GetRoot();
                    DockControllerHelper.SetClosingBehavior(panel, ClosingBehavior.ImmediatelyRemove);
                    try
                    {
                        if (!panel.IsClosed)
                        {
                            this.CloseCore(panel, true);
                        }
                        this.RemovePanelCore(panel);
                    }
                    finally
                    {
                        panel.ClearValue(DockControllerHelper.ClosingBehaviorProperty);
                    }
                    NotificationBatch.Action(this.Container, root, null);
                }
            }
        }

        private void RemovePanelCore(LayoutPanel panel)
        {
            this.Container.ClosedPanels.Remove(panel);
            DockLayoutManager.RemoveLogicalChild(this.Container, panel);
            panel.Manager = null;
            this.Container.Dispatcher.BeginInvoke(() => DestroyPanelContent(panel), DispatcherPriority.Render, new object[0]);
            this.ClearPlaceHolder(panel);
        }

        public bool Rename(BaseLayoutItem item) => 
            this.Container.RenameHelper.Rename(item);

        public bool Restore(BaseLayoutItem item)
        {
            bool flag4;
            bool isRestoring = GetIsRestoring(item);
            bool flag2 = ((item.IsClosed | isRestoring) && (item is LayoutPanel)) && item.AllowRestore;
            if (!flag2)
            {
                return false;
            }
            if (this.RaiseItemCancelEvent(item, DockLayoutManager.DockItemRestoringEvent) || this.OnDockOperationStarting(DockOperation.Restore, item, null))
            {
                return false;
            }
            BaseLayoutItem[] items = new BaseLayoutItem[] { item };
            using (new LogicalTreeLocker(this.Container, items))
            {
                using (new UpdateBatch(this.Container))
                {
                    if (this.Container.ClosedPanels.Remove((LayoutPanel) item) || GetIsRestoring(item))
                    {
                        if (!this.RestoreToPlaceHolder(item, true))
                        {
                            DockSituation lastDockSituation = item.GetLastDockSituation();
                            if (((lastDockSituation != null) && this.CanRestoreDockSituation(lastDockSituation)) && (lastDockSituation.Type != DockType.None))
                            {
                                BaseLayoutItem dockTarget = lastDockSituation.DockTarget;
                                flag2 = this.Dock(item, dockTarget, lastDockSituation.Type);
                            }
                            else
                            {
                                LayoutPanel panel = (LayoutPanel) item;
                                Size size = MathHelper.IsEmpty(panel.FloatSizeBeforeClose) ? (MathHelper.IsEmpty(item.FloatSize) ? item.GetSize() : item.FloatSize) : panel.FloatSizeBeforeClose;
                                FloatGroup floatGroup = this.BoxToFloatGroupCore(item, new Rect(panel.FloatLocationBeforeClose, size));
                                this.Container.CheckFloatGroupRestoreBounds(floatGroup, panel.HasFloatOffsetBeforeClose ? panel.FloatOffsetBeforeClose : this.Container.GetRestoreOffset());
                                this.Container.FloatGroups.Add(floatGroup);
                                PlaceHolderHelper.ClearPlaceHolder(item, PlaceHolderState.Floating);
                            }
                        }
                        this.RaiseItemEvent(item, DockLayoutManager.DockItemRestoredEvent);
                        this.Container.Update();
                        this.OnDockOperationComplete(item, DockOperation.Restore);
                        NotificationBatch.Action(this.Container, item.GetRoot(), null);
                    }
                    flag4 = flag2;
                }
            }
            return flag4;
        }

        public bool RestoreGroupAndInsertItem(int index, BaseLayoutItem item, LayoutGroup target)
        {
            bool flag;
            if ((item == null) || ((item.Parent != null) || (target == null)))
            {
                return false;
            }
            using (new UpdateBatch(this.Container))
            {
                BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
                using (new LogicalTreeLocker(this.Container, items))
                {
                    SetIsRestoring(item, true);
                    try
                    {
                        item.SetValue(DockLayoutManager.LayoutItemProperty, item);
                        PlaceHolder holder = target.DockInfo.LastOrDefault();
                        if (holder != null)
                        {
                            PlaceHolderHelper.AddFakePlaceHolderForItem(target, item, index, holder.DockState);
                        }
                        flag = this.Restore(item);
                    }
                    finally
                    {
                        item.ClearValue(IsRestoringProperty);
                    }
                }
            }
            return flag;
        }

        private void RestorePlaceHoldersRelations(IEnumerable<LayoutGroup> restoredGroups)
        {
            List<LayoutGroup> list = restoredGroups.ToList<LayoutGroup>();
            Func<LayoutGroup, bool> predicate = <>c.<>9__97_0;
            if (<>c.<>9__97_0 == null)
            {
                Func<LayoutGroup, bool> local1 = <>c.<>9__97_0;
                predicate = <>c.<>9__97_0 = x => x.Parent is FloatGroup;
            }
            LayoutGroup group = restoredGroups.FirstOrDefault<LayoutGroup>(predicate);
            if (group != null)
            {
                LayoutGroup root = group.GetRoot();
                foreach (PlaceHolder holder in group.PlaceHolderHelper.GetPlaceHolders().ToArray<PlaceHolder>())
                {
                    LayoutGroup owner = holder.Owner as LayoutGroup;
                    if (owner != null)
                    {
                        Func<PlaceHolder, BaseLayoutItem> selector = <>c.<>9__97_1;
                        if (<>c.<>9__97_1 == null)
                        {
                            Func<PlaceHolder, BaseLayoutItem> local2 = <>c.<>9__97_1;
                            selector = <>c.<>9__97_1 = x => x.Owner;
                        }
                        if (owner.PlaceHolderHelper.GetPlaceHolders().Select<PlaceHolder, BaseLayoutItem>(selector).Intersect<BaseLayoutItem>(root.Items).Any<BaseLayoutItem>())
                        {
                            this.DockInExistingGroup(owner, group, PlaceHolderHelper.GetDockIndex(holder));
                            PlaceHolderHelper.ClearPlaceHolder(owner, holder);
                            list.Add(owner);
                        }
                    }
                }
            }
            foreach (LayoutGroup group4 in list)
            {
                PlaceHolder[] holderArray3 = group4.PlaceHolderHelper.GetPlaceHolders().ToArray<PlaceHolder>();
                foreach (PlaceHolder holder2 in holderArray3)
                {
                    BaseLayoutItem owner = holder2.Owner;
                    if ((owner.Parent != null) && ((owner.IsFloating && ReferenceEquals(owner.Parent, group4.GetRoot())) || Equals(group4.Parent, owner.Parent)))
                    {
                        this.DockInExistingGroup(owner, group4, PlaceHolderHelper.GetDockIndex(holder2));
                        PlaceHolderHelper.ClearPlaceHolder(owner, holder2);
                    }
                }
            }
        }

        private bool RestoreToFloatingPlaceHolder(BaseLayoutItem item, PlaceHolder placeHolder)
        {
            bool flag = true;
            using (new LogicalTreeLocker(this.Container, PlaceHolderHelper.GetLayoutHierarchy(placeHolder).ToArray<LayoutGroup>()))
            {
                LayoutGroup group;
                LayoutGroup[] restoredGroups = PlaceHolderHelper.RestoreLayoutHierarchy(this.Container, placeHolder, out group).ToArray<LayoutGroup>();
                FloatGroup objA = (group as FloatGroup) ?? (group.GetRoot() as FloatGroup);
                bool flag2 = ReferenceEquals(objA, null);
                if (flag2)
                {
                    objA = this.Container.GenerateGroup<FloatGroup>();
                }
                bool flag3 = false;
                if (!this.Container.FloatGroups.Contains(objA))
                {
                    flag3 = true;
                    this.Container.DecomposedItems.Remove(objA);
                    LayoutPanel panel = (LayoutPanel) item;
                    if (flag2)
                    {
                        objA.BeginInit();
                    }
                    if (panel.HasFloatLocationBeforeClose)
                    {
                        objA.FloatLocation = panel.FloatLocationBeforeClose;
                    }
                    if (!MathHelper.IsEmpty(panel.FloatSizeBeforeClose))
                    {
                        objA.FloatSize = panel.FloatSizeBeforeClose;
                    }
                    if (flag2)
                    {
                        objA.EndInit();
                    }
                    if (panel.HasFloatOffsetBeforeClose)
                    {
                        this.Container.CheckFloatGroupRestoreBounds(objA, panel.FloatOffsetBeforeClose);
                    }
                    else
                    {
                        this.Container.CheckFloatGroupRestoreBounds(objA, objA.FloatOffsetBeforeClose);
                    }
                    this.Container.FloatGroups.Add(objA);
                }
                this.RestorePlaceHoldersRelations(restoredGroups);
                LayoutGroup group3 = restoredGroups.FirstOrDefault<LayoutGroup>();
                if (flag2 && (group3 == null))
                {
                    if ((item is DocumentPanel) && (this.Container.FloatingDocumentContainer == FloatingDocumentContainer.DocumentHost))
                    {
                        item = DockControllerHelper.BoxIntoDocumentHost(item, this.Container);
                    }
                    objA.Add(item);
                }
                else
                {
                    if (flag2)
                    {
                        objA.Add(group3);
                    }
                    flag = this.RestoreToPlaceHolder(item, placeHolder);
                }
                if ((flag & flag3) && (objA.FloatState == FloatState.Minimized))
                {
                    objA.SetFloatState(FloatState.Normal);
                }
                PlaceHolderHelper.ClearPlaceHolder(item, placeHolder);
                if (flag)
                {
                    using (item.GetRoot().LockDockingTarget())
                    {
                        restoredGroups.Reverse<LayoutGroup>().ForEach<LayoutGroup>(delegate (LayoutGroup x) {
                            this.TryUngroupGroup(x);
                        });
                    }
                }
                return flag;
            }
        }

        private bool RestoreToPlaceHolder(BaseLayoutItem item, PlaceHolder placeHolder)
        {
            using (new LogicalTreeLocker(this.Container, PlaceHolderHelper.GetLayoutHierarchy(placeHolder).ToArray<LayoutGroup>()))
            {
                LayoutGroup group;
                IEnumerable<LayoutGroup> restoredGroups = PlaceHolderHelper.RestoreLayoutHierarchy(this.Container, placeHolder, out group);
                FloatGroup document = group as FloatGroup;
                if ((document != null) && (document.FloatState == FloatState.Minimized))
                {
                    this.Container.MDIController.Restore(document);
                }
                bool flag = this.DockInExistingGroup(item, group, PlaceHolderHelper.GetDockIndex(item, placeHolder));
                if (flag)
                {
                    LayoutGroup root = item.GetRoot();
                    AutoHideGroup group4 = root as AutoHideGroup;
                    if ((group4 != null) && !this.Container.AutoHideGroups.Contains(group4))
                    {
                        this.Container.AutoHideGroups.Add(group4);
                        this.Container.DecomposedItems.Remove(group4);
                    }
                    FloatGroup group5 = root as FloatGroup;
                    if ((group5 != null) && !this.Container.FloatGroups.Contains(group5))
                    {
                        this.Container.FloatGroups.Add(group5);
                        this.Container.DecomposedItems.Remove(group5);
                    }
                    item.UpdateDockSituation(item.Parent, DockType.None);
                }
                this.RestorePlaceHoldersRelations(restoredGroups);
                PlaceHolderHelper.ClearPlaceHolder(item, placeHolder);
                return flag;
            }
        }

        private bool RestoreToPlaceHolder(BaseLayoutItem item, bool isRestore = false)
        {
            PlaceHolder placeHolderForDockOperation = PlaceHolderHelper.GetPlaceHolderForDockOperation(item, isRestore);
            if (placeHolderForDockOperation == null)
            {
                return false;
            }
            PlaceHolder placeHolderRoot = PlaceHolderHelper.GetPlaceHolderRoot(placeHolderForDockOperation);
            return ((!placeHolderForDockOperation.Parent.IsInTree() && (((placeHolderRoot.Parent == null) || !placeHolderRoot.Parent.IsInTree()) ? placeHolderRoot.IsFloating : placeHolderRoot.Parent.IsFloating)) ? this.RestoreToFloatingPlaceHolder(item, placeHolderForDockOperation) : this.RestoreToPlaceHolder(item, placeHolderForDockOperation));
        }

        private void SaveFloatSize(FloatGroup floatRoot, BaseLayoutItem itemToDock)
        {
            Thickness floatingMargin = new Thickness(0.0);
            FloatPanePresenter uIElement = this.Container.GetUIElement(floatRoot) as FloatPanePresenter;
            if (uIElement != null)
            {
                floatingMargin = uIElement.GetFloatingMargin();
            }
            itemToDock.FloatSize = new Size(Math.Max((double) 1.0, (double) (floatRoot.FloatSize.Width - (floatingMargin.Left + floatingMargin.Right))), Math.Max((double) 1.0, (double) (floatRoot.FloatSize.Height - (floatingMargin.Top + floatingMargin.Bottom))));
        }

        private void SetActive(bool value)
        {
            if (this.ActiveItem != null)
            {
                this.ActiveItem.SetActive(value);
                if (value && (this.lockActivateCounter == 0))
                {
                    this.ActivationHelper.SelectInGroup(this.ActiveItem);
                }
            }
        }

        private void SetActiveItemCore(BaseLayoutItem value)
        {
            this.SetActive(false);
            BaseLayoutItem activeItemCore = this.activeItemCore;
            this.activeItemCore = value;
            this.SetActive(true);
            DockLayoutManager container = this.Container;
            container.isDockItemActivation++;
            this.Container.ActiveDockItem = this.ActiveItem;
            DockLayoutManager manager2 = this.Container;
            manager2.isDockItemActivation--;
            this.RaiseActiveItemChanged(value, activeItemCore);
        }

        private void SetClosed(BaseLayoutItem item)
        {
            this.SetClosed(item, item as LayoutGroup);
        }

        private void SetClosed(BaseLayoutItem item, LayoutGroup parent)
        {
            FloatGroup group = (item as FloatGroup) ?? (parent.GetRoot() as FloatGroup);
            FloatGroup group2 = (item as FloatGroup) ?? (parent as FloatGroup);
            List<BaseLayoutItem> panels = new List<BaseLayoutItem>();
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            item.Accept(delegate (BaseLayoutItem i) {
                items.Add(i);
                if (i is LayoutPanel)
                {
                    panels.Add(i);
                }
            });
            if (group2 != null)
            {
                foreach (BaseLayoutItem item2 in items)
                {
                    if (item2.Parent != null)
                    {
                        this.AddPlaceHolder(item2, item2.Parent);
                    }
                }
            }
            Point restoreOffset = this.Container.GetRestoreOffset();
            foreach (BaseLayoutItem item3 in items)
            {
                LayoutPanel panel = item3 as LayoutPanel;
                bool flag = panel != null;
                if ((panel != null) && (group != null))
                {
                    panel.UpdateDockSituation(null, null, null);
                    panel.FloatLocationBeforeClose = group.FloatLocation;
                    panel.FloatSizeBeforeClose = group.FloatSize;
                    panel.FloatOffsetBeforeClose = restoreOffset;
                    flag = !panel.ExecuteCloseCommand();
                }
                if (item3.Parent != null)
                {
                    item3.Parent.Remove(item3);
                }
                if (flag)
                {
                    if (DockControllerHelper.GetActualClosingBehavior(this.Container, panel) == ClosingBehavior.ImmediatelyRemove)
                    {
                        this.RemovePanelCore(panel);
                    }
                    else
                    {
                        this.Container.ClosedPanels.Add(panel);
                    }
                }
            }
            if (group != null)
            {
                group.FloatOffsetBeforeClose = restoreOffset;
            }
            Action<LayoutGroup> action = <>c.<>9__105_1;
            if (<>c.<>9__105_1 == null)
            {
                Action<LayoutGroup> local3 = <>c.<>9__105_1;
                action = <>c.<>9__105_1 = x => x.IsUngroupped = true;
            }
            (item as LayoutGroup).Do<LayoutGroup>(action);
            foreach (BaseLayoutItem item4 in items)
            {
                LayoutGroup group3 = item4 as LayoutGroup;
                if (group3 != null)
                {
                    this.Container.DecomposedItems.AddOnce(group3);
                }
            }
            if (panels.Contains(this.Container.ActiveDockItem))
            {
                this.Container.ActiveDockItem = null;
            }
            if (panels.Contains(this.Container.ActiveMDIItem))
            {
                this.Container.ActiveMDIItem = null;
            }
        }

        public static void SetIsRestoring(DependencyObject d, bool value)
        {
            d.SetValue(IsRestoringProperty, value);
        }

        private LayoutGroup TryUngroupCore(LayoutGroup group)
        {
            LayoutGroup group4;
            if (group == null)
            {
                return null;
            }
            if (group.IsPermanent)
            {
                return group;
            }
            BaseLayoutItem[] items = new BaseLayoutItem[] { group };
            using (new LogicalTreeLocker(this.Container, items))
            {
                LayoutGroup parentGroup = group.Parent;
                int count = group.Items.Count;
                if (count == 0)
                {
                    AutoHideGroup autoHideGroup = group as AutoHideGroup;
                    if (autoHideGroup == null)
                    {
                        if (!(group is FloatGroup))
                        {
                            bool flag = (parentGroup != null) && group.ShouldStayInTree();
                            if (flag)
                            {
                                this.AddPlaceHolder(group, parentGroup);
                                group.IsUngroupped = true;
                            }
                            if (!this.CloseItem(group, parentGroup))
                            {
                                if (ReferenceEquals(group.Parent, null) & flag)
                                {
                                    this.Container.DecomposedItems.AddOnce(group);
                                }
                                goto TR_0006;
                            }
                        }
                        else
                        {
                            Action method = delegate {
                                this.CloseFloatGroup(group as FloatGroup);
                            };
                            if (this.Container.ViewAdapter.IsInEvent && this.Container.IsFloating)
                            {
                                this.Container.DelayedExecuteEnqueue(method);
                            }
                            else
                            {
                                method();
                            }
                        }
                    }
                    else if (!autoHideGroup.HasPersistentGroups)
                    {
                        this.CloseAutoHideGroup(autoHideGroup);
                    }
                    else
                    {
                        return group;
                    }
                    return parentGroup;
                }
                else if (count == 1)
                {
                    if (!DockControllerHelper.CanUnboxGroupWithSingleItem(group, this.Container, group.HasPlaceHolders))
                    {
                        group4 = group;
                    }
                    else
                    {
                        BaseLayoutItem item = group.Items[0];
                        if (parentGroup != null)
                        {
                            if (!parentGroup.IgnoreOrientation && (parentGroup.Orientation != Orientation.Horizontal))
                            {
                                if (!item.ItemHeight.IsAbsolute)
                                {
                                    item.ItemHeight = group.ItemHeight;
                                }
                            }
                            else if (!item.ItemWidth.IsAbsolute)
                            {
                                item.ItemWidth = group.ItemWidth;
                            }
                        }
                        if (item.ItemType != LayoutItemType.TabPanelGroup)
                        {
                            this.TryUngroupGroup(item as LayoutGroup);
                        }
                        Action<LayoutGroup> beforeUnboxing = delegate (LayoutGroup x) {
                            if ((parentGroup != null) && x.ShouldStayInTree())
                            {
                                this.AddPlaceHolder(x, parentGroup);
                                this.AddPlaceHolder(x[0], x);
                                x.IsUngroupped = true;
                            }
                        };
                        LayoutGroup group3 = DockControllerHelper.Unbox(this.Container, group, beforeUnboxing, delegate (LayoutGroup x) {
                            if (x.ShouldStayInTree() && (x.Parent == null))
                            {
                                this.Container.DecomposedItems.AddOnce(x);
                            }
                        });
                        if ((group3 is FloatGroup) && ((group3.Items.Count > 0) && (group3[0] is LayoutGroup)))
                        {
                            this.TryUngroupGroup((LayoutGroup) group3[0]);
                        }
                        group4 = group3;
                    }
                    return group4;
                }
            TR_0006:
                group4 = group;
            }
            return group4;
        }

        protected LayoutGroup TryUngroupGroup(LayoutGroup group)
        {
            if ((group != null) && !group.IsPermanent)
            {
                if (!this.Container.IsLayoutLocked)
                {
                    return this.TryUngroupCore(group);
                }
                this.Container.ExecuteActionOnLayoutUnlocked((PendingActionCallback) (x => this.TryUngroupCore(x as LayoutGroup)), group);
            }
            return group;
        }

        private void UnlockActivate()
        {
            this.lockActivateCounter--;
        }

        private void UpdateDecomposedItems()
        {
            Func<LayoutGroup, LayoutGroup> keySelector = <>c.<>9__108_0;
            if (<>c.<>9__108_0 == null)
            {
                Func<LayoutGroup, LayoutGroup> local1 = <>c.<>9__108_0;
                keySelector = <>c.<>9__108_0 = x => x;
            }
            Dictionary<LayoutGroup, LayoutGroup[]> dictionary = this.Container.DecomposedItems.ToDictionary<LayoutGroup, LayoutGroup, LayoutGroup[]>(keySelector, <>c.<>9__108_1 ??= x => PlaceHolderHelper.GetAffectedGroups(x));
            this.Container.DecomposedItems.Purge();
            foreach (KeyValuePair<LayoutGroup, LayoutGroup[]> pair in dictionary)
            {
                if (!this.Container.DecomposedItems.Contains(pair.Key))
                {
                    foreach (LayoutGroup group in pair.Value)
                    {
                        this.TryUngroupGroup(group);
                    }
                }
            }
        }

        private void UpdateDockTarget(DockType dockType, BaseLayoutItem dockTarget, BaseLayoutItem targetParent)
        {
            if ((dockType != DockType.None) && (dockTarget.IsFloating && dockTarget.IsInTree()))
            {
                PlaceHolder placeHolder = PlaceHolderHelper.GetPlaceHolder(dockTarget, PlaceHolderState.Floating);
                if ((placeHolder != null) && ((placeHolder.Parent != null) && !placeHolder.Parent.IsInTree()))
                {
                    LayoutGroup parent = placeHolder.Parent;
                    LayoutGroup target = dockTarget.Parent;
                    if (dockType != DockType.Fill)
                    {
                        this.DockInExistingGroup(parent, target, target.IndexFromItem(dockTarget));
                        this.Container.DecomposedItems.Remove(parent);
                        PlaceHolderHelper.ClearPlaceHolder(parent);
                        this.RestoreToPlaceHolder(dockTarget, placeHolder);
                    }
                    else if (!ReferenceEquals(targetParent, dockTarget.Parent))
                    {
                        PlaceHolderHelper.ReplacePlaceHolderOwner(parent, placeHolder, target);
                    }
                }
            }
        }

        public BaseLayoutItem ActiveItem
        {
            get => 
                this.activeItemCore;
            set
            {
                if (!ReferenceEquals(this.ActiveItem, value))
                {
                    this.SetActiveItemCore(value);
                }
            }
        }

        public DockLayoutManager Container =>
            this.containerCore;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockControllerImplBase.<>c <>9 = new DockControllerImplBase.<>c();
            public static Func<LayoutPanel, bool> <>9__68_0;
            public static Func<LayoutPanel, DateTime> <>9__68_1;
            public static Func<LayoutPanel, bool> <>9__68_2;
            public static Func<LayoutPanel, DateTime> <>9__68_3;
            public static Func<BaseLayoutItem, bool> <>9__69_1;
            public static Func<LayoutGroup, bool> <>9__69_0;
            public static Func<bool> <>9__69_2;
            public static Func<BaseLayoutItem, bool> <>9__69_3;
            public static Func<BaseLayoutItem, DateTime> <>9__69_4;
            public static Func<BaseLayoutItem, bool> <>9__83_0;
            public static Func<BaseLayoutItem, bool> <>9__83_1;
            public static Func<BaseLayoutItem, DockLayoutManager> <>9__85_0;
            public static Func<DockLayoutManager> <>9__85_1;
            public static Func<LayoutGroup, bool> <>9__97_0;
            public static Func<PlaceHolder, BaseLayoutItem> <>9__97_1;
            public static Action<LayoutGroup> <>9__105_1;
            public static Func<LayoutGroup, LayoutGroup> <>9__108_0;
            public static Func<LayoutGroup, LayoutGroup[]> <>9__108_1;

            internal bool <CloseFloatGroup>b__68_0(LayoutPanel x) => 
                x.IsFloating;

            internal DateTime <CloseFloatGroup>b__68_1(LayoutPanel x) => 
                x.LastActivationDateTime;

            internal bool <CloseFloatGroup>b__68_2(LayoutPanel x) => 
                x.DockItemState == DockItemState.Docked;

            internal DateTime <CloseFloatGroup>b__68_3(LayoutPanel x) => 
                x.LastActivationDateTime;

            internal bool <CloseItem>b__69_0(LayoutGroup x)
            {
                Func<BaseLayoutItem, bool> predicate = <>9__69_1;
                if (<>9__69_1 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>9__69_1;
                    predicate = <>9__69_1 = y => y.IsActive;
                }
                return x.GetNestedItems().Any<BaseLayoutItem>(predicate);
            }

            internal bool <CloseItem>b__69_1(BaseLayoutItem y) => 
                y.IsActive;

            internal bool <CloseItem>b__69_2() => 
                false;

            internal bool <CloseItem>b__69_3(BaseLayoutItem x) => 
                (x is LayoutPanel) && x.IsFloating;

            internal DateTime <CloseItem>b__69_4(BaseLayoutItem x) => 
                ((LayoutPanel) x).LastActivationDateTime;

            internal bool <GetItemToFocus>b__83_0(BaseLayoutItem x) => 
                (x is LayoutPanel) && !x.IsAutoHidden;

            internal bool <GetItemToFocus>b__83_1(BaseLayoutItem x) => 
                (x is LayoutPanel) && !x.IsAutoHidden;

            internal DockLayoutManager <GetLinkedManager>b__85_0(BaseLayoutItem x) => 
                x.Manager;

            internal DockLayoutManager <GetLinkedManager>b__85_1() => 
                null;

            internal bool <RestorePlaceHoldersRelations>b__97_0(LayoutGroup x) => 
                x.Parent is FloatGroup;

            internal BaseLayoutItem <RestorePlaceHoldersRelations>b__97_1(PlaceHolder x) => 
                x.Owner;

            internal void <SetClosed>b__105_1(LayoutGroup x)
            {
                x.IsUngroupped = true;
            }

            internal LayoutGroup <UpdateDecomposedItems>b__108_0(LayoutGroup x) => 
                x;

            internal LayoutGroup[] <UpdateDecomposedItems>b__108_1(LayoutGroup x) => 
                PlaceHolderHelper.GetAffectedGroups(x);
        }
    }
}

