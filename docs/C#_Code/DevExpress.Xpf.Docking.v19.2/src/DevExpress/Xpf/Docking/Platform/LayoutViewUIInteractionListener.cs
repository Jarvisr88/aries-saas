namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using DevExpress.Xpf.Layout.Core.UIInteraction;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class LayoutViewUIInteractionListener : UIInteractionServiceListener
    {
        protected override bool CanFloatElementOnDoubleClick(ILayoutElement element) => 
            ((IDockLayoutElement) element).Item.FloatOnDoubleClick;

        protected virtual bool CanScrollNextCore(BaseLayoutItem scrollTarget)
        {
            LayoutGroup group = scrollTarget as LayoutGroup;
            return ((group != null) && group.TabHeaderCanScrollNext);
        }

        protected virtual bool CanScrollPrevCore(BaseLayoutItem scrollTarget)
        {
            LayoutGroup group = scrollTarget as LayoutGroup;
            return ((group != null) && group.TabHeaderCanScrollPrev);
        }

        protected virtual bool CloseItemCore(BaseLayoutItem itemToClose)
        {
            TabbedGroup group = itemToClose as TabbedGroup;
            if ((group != null) && ((group.ItemType != LayoutItemType.TabPanelGroup) || !group.IsFloatingRootItem))
            {
                itemToClose = group.SelectedItem;
            }
            if (itemToClose == null)
            {
                return false;
            }
            TabbedGroup parent = itemToClose.Parent as TabbedGroup;
            if (parent != null)
            {
                FloatGroup group3 = parent.Parent as FloatGroup;
                if ((group3 != null) && (group3.FloatState == FloatState.Minimized))
                {
                    itemToClose = group3;
                }
            }
            return this.View.Container.DockController.CloseEx(itemToClose);
        }

        protected override IFloatingHelper CreateFloatingHelper() => 
            new FloatingHelper(this.View);

        protected override bool DoControlItemDoubleClick(ILayoutElement element)
        {
            LayoutControlItem item = ((IDockLayoutElement) element).Item as LayoutControlItem;
            return ((item != null) && this.SelectAllInControl(item));
        }

        protected virtual bool ExpandItemCore(BaseLayoutItem item)
        {
            LayoutGroup group = item as LayoutGroup;
            LayoutPanel panel = item as LayoutPanel;
            if (group != null)
            {
                group.Expanded = !group.Expanded;
                this.View.AdornerHelper.InvalidateSelectionAdorner();
            }
            if (panel != null)
            {
                panel.AutoHideExpandState = (panel.AutoHideExpandState == AutoHideExpandState.Expanded) ? AutoHideExpandState.Visible : AutoHideExpandState.Expanded;
            }
            return ((group != null) || (panel != null));
        }

        protected static DocumentPanel GetDocument(BaseLayoutItem item)
        {
            DocumentPanel selectedItem = item as DocumentPanel;
            if (item is DocumentGroup)
            {
                selectedItem = ((DocumentGroup) item).SelectedItem as DocumentPanel;
            }
            if (item is FloatGroup)
            {
                selectedItem = ((FloatGroup) item)[0] as DocumentPanel;
            }
            return selectedItem;
        }

        protected virtual bool HideCore(BaseLayoutItem item)
        {
            LayoutPanel panel = item as LayoutPanel;
            if (panel != null)
            {
                panel.AutoHideExpandState = AutoHideExpandState.Hidden;
            }
            return (panel != null);
        }

        protected override bool IsControlItemElement(ILayoutElement element) => 
            element is ControlItemElement;

        protected override bool IsFloatingElement(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            return ((item is FloatGroup) || item.IsFloatingRootItem);
        }

        protected override bool IsMaximized(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            if (item is FloatGroup)
            {
                return ((FloatGroup) item).IsMaximized;
            }
            LayoutPanel panel = (GetDocument(((IDockLayoutElement) element).Item) ?? item) as LayoutPanel;
            return ((panel != null) && panel.IsMaximized);
        }

        protected override bool IsMDIDocument(ILayoutElement element) => 
            element is MDIDocumentElement;

        protected override bool MaximizeElementOnDoubleClick(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            DocumentPanel document = GetDocument(item);
            DocumentPanel panel2 = document;
            if (document == null)
            {
                DocumentPanel local1 = document;
                panel2 = (DocumentPanel) item;
            }
            return this.View.Container.MDIController.Maximize((BaseLayoutItem) panel2);
        }

        protected virtual bool MaximizeItemCore(BaseLayoutItem item)
        {
            DocumentPanel document = GetDocument(item);
            DocumentPanel panel2 = document;
            if (document == null)
            {
                DocumentPanel local1 = document;
                panel2 = (DocumentPanel) item;
            }
            return this.View.Container.MDIController.Maximize((BaseLayoutItem) panel2);
        }

        protected virtual bool MinimizeItemCore(BaseLayoutItem item) => 
            this.View.Container.MDIController.Minimize(item);

        public override void OnActivate()
        {
            Func<LayoutView, DockLayoutManager> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<LayoutView, DockLayoutManager> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => x.Container;
            }
            Func<DockLayoutManager, ICustomizationController> func2 = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<DockLayoutManager, ICustomizationController> local2 = <>c.<>9__2_1;
                func2 = <>c.<>9__2_1 = x => x.CustomizationController;
            }
            Action<ICustomizationController> action = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Action<ICustomizationController> local3 = <>c.<>9__2_2;
                action = <>c.<>9__2_2 = x => x.CloseMenu();
            }
            this.View.With<LayoutView, DockLayoutManager>(evaluator).With<DockLayoutManager, ICustomizationController>(func2).Do<ICustomizationController>(action);
        }

        public override bool OnActiveItemChanged(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            this.View.Container.Activate(item);
            LayoutGroup root = item.GetRoot();
            if (root != null)
            {
                this.View.Container.CustomizationController.CustomizationRoot = root;
            }
            return ((item != null) && item.IsActive);
        }

        public override bool OnActiveItemChanging(ILayoutElement element)
        {
            IDockLayoutElement element2 = (IDockLayoutElement) element;
            this.View.Container.RenameHelper.CancelRenamingAndResetClickedState();
            Func<DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs, System.Windows.Input.MouseEventArgs> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs, System.Windows.Input.MouseEventArgs> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.OriginalEvent as System.Windows.Input.MouseEventArgs;
            }
            System.Windows.Input.MouseEventArgs args = (this.View.LastProcessedEvent as DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs).With<DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs, System.Windows.Input.MouseEventArgs>(evaluator);
            return ((!element.IsTabHeader || ((args == null) || ((args.MiddleButton != MouseButtonState.Pressed) || DockLayoutManagerParameters.ActivateItemOnTabHeaderMiddleClick))) ? (element2.AllowActivate && element2.IsEnabled) : false);
        }

        public override bool OnClickAction(LayoutElementHitInfo clickInfo)
        {
            DockLayoutElementHitInfo info = clickInfo as DockLayoutElementHitInfo;
            if (info == null)
            {
                return false;
            }
            IDockLayoutElement item = info.Element as IDockLayoutElement;
            return (info.InHeader && this.RenameItemCore(item));
        }

        public override bool OnClickPreviewAction(LayoutElementHitInfo clickInfo) => 
            (!clickInfo.InHeader || (KeyHelper.IsCtrlPressed || KeyHelper.IsShiftPressed)) ? this.View.Container.RenameHelper.CancelRenamingAndResetClickedState() : false;

        public override bool OnMenuAction(LayoutElementHitInfo clickInfo)
        {
            this.View.Container.RenameHelper.CancelRenamingAndResetClickedState();
            if (!(clickInfo.Element is IDockLayoutElement))
            {
                return false;
            }
            BaseLayoutItem menuTarget = ((IDockLayoutElement) clickInfo.Element).Item;
            return this.ShowMenuCore(menuTarget);
        }

        public override bool OnMiddleButtonClickAction(LayoutElementHitInfo clickInfo)
        {
            if ((clickInfo.Element == null) || (!(clickInfo.Element is IDockLayoutElement) || !clickInfo.InHeader))
            {
                return false;
            }
            BaseLayoutItem clickTarget = ((IDockLayoutElement) clickInfo.Element).Item;
            return this.OnMiddleButtonClickActionCore(clickTarget);
        }

        protected virtual bool OnMiddleButtonClickActionCore(BaseLayoutItem clickTarget) => 
            (clickTarget != null) && (clickTarget.IsTabPage && (LayoutItemsHelper.IsDockItem(clickTarget) && this.CloseItemCore(clickTarget)));

        public override bool OnMouseDown(LayoutElementHitInfo clickInfo)
        {
            BaseLayoutItem item = ((IDockLayoutElement) clickInfo.Element).Item;
            if ((item == null) || !item.IsActive)
            {
                return false;
            }
            this.View.Container.FocusItem(item, false);
            return true;
        }

        protected bool PinDocument(BaseLayoutItem item) => 
            this.View.Container.DockController.ToggleItemPinStatus(item);

        protected bool PinItem(BaseLayoutItem item)
        {
            if (item.Parent is TabbedGroup)
            {
                item = item.Parent;
            }
            return ((!item.IsFloating || !this.View.Container.RaiseItemDockingEvent(DockLayoutManager.DockItemStartDockingEvent, item, CoordinateHelper.ZeroPoint, null, DockType.None, true)) ? this.View.Container.DockController.Hide(item) : false);
        }

        protected virtual bool PinItemCore(BaseLayoutItem item) => 
            (item != null) ? (!this.PinDocument(item) ? (item.IsAutoHidden ? this.UnPinItem(item) : this.PinItem(item)) : true) : false;

        protected virtual bool RenameItemCore(IDockLayoutElement item) => 
            !this.View.Container.RenameHelper.CancelRenaming() && this.View.Container.RenameHelper.RenameByClick(item);

        protected override bool RestoreElementOnDoubleClick(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            DocumentPanel document = GetDocument(item);
            DocumentPanel panel2 = document;
            if (document == null)
            {
                DocumentPanel local1 = document;
                panel2 = (DocumentPanel) item;
            }
            return this.View.Container.MDIController.Restore((BaseLayoutItem) panel2);
        }

        protected virtual bool RestoreItemCore(BaseLayoutItem item)
        {
            DocumentPanel document = GetDocument(item);
            DocumentPanel panel2 = document;
            if (document == null)
            {
                DocumentPanel local1 = document;
                panel2 = (DocumentPanel) item;
            }
            return this.View.Container.MDIController.Restore((BaseLayoutItem) panel2);
        }

        protected virtual bool ScrollNextCore(BaseLayoutItem scrollTarget)
        {
            LayoutGroup group = scrollTarget as LayoutGroup;
            return ((group != null) && group.ScrollNext());
        }

        protected virtual bool ScrollPrevCore(BaseLayoutItem scrollTarget)
        {
            LayoutGroup group = scrollTarget as LayoutGroup;
            return ((group != null) && group.ScrollPrev());
        }

        private bool SelectAllInControl(LayoutControlItem item)
        {
            RoutedCommand selectAll = ApplicationCommands.SelectAll;
            if (!selectAll.CanExecute(null, item.Control))
            {
                return false;
            }
            selectAll.Execute(null, item.Control);
            return true;
        }

        protected virtual void ShowItemsSelectorMenu(UIElement source, BaseLayoutItem[] items)
        {
            this.View.Container.CustomizationController.ShowItemSelectorMenu(source, items);
        }

        protected virtual bool ShowMenuCore(BaseLayoutItem menuTarget) => 
            this.ShowMenuCore(menuTarget, false);

        protected bool ShowMenuCore(BaseLayoutItem menuTarget, bool forceShow) => 
            MenuHelper.ShowMenu(this.View.Container, menuTarget, null, forceShow);

        protected bool UnPinItem(BaseLayoutItem item)
        {
            if (!item.AllowDock)
            {
                return (this.View.Container.DockController.Float(item) != null);
            }
            if (item.IsAutoHidden)
            {
                item = item.Parent;
            }
            return this.View.Container.DockController.Dock(item);
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutViewUIInteractionListener.<>c <>9 = new LayoutViewUIInteractionListener.<>c();
            public static Func<LayoutView, DockLayoutManager> <>9__2_0;
            public static Func<DockLayoutManager, ICustomizationController> <>9__2_1;
            public static Action<ICustomizationController> <>9__2_2;
            public static Func<DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs, System.Windows.Input.MouseEventArgs> <>9__3_0;

            internal DockLayoutManager <OnActivate>b__2_0(LayoutView x) => 
                x.Container;

            internal ICustomizationController <OnActivate>b__2_1(DockLayoutManager x) => 
                x.CustomizationController;

            internal void <OnActivate>b__2_2(ICustomizationController x)
            {
                x.CloseMenu();
            }

            internal System.Windows.Input.MouseEventArgs <OnActiveItemChanging>b__3_0(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs x) => 
                x.OriginalEvent as System.Windows.Input.MouseEventArgs;
        }
    }
}

