namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class ItemContextMenuInfo : ItemContextMenuInfoBase
    {
        public ItemContextMenuInfo(ItemContextMenu menu, BaseLayoutItem item) : base(menu, item)
        {
        }

        protected override void CreateItems()
        {
            DockLayoutManager container = this.ItemMenu.Container;
            bool isFloating = base.Item.IsFloating;
            bool isAutoHidden = base.Item.IsAutoHidden;
            if (!LayoutItemsHelper.IsDataBound(base.Item) || !DockLayoutManagerParameters.CheckLayoutGroupIsUsingItemsSource)
            {
                ClosingBehavior actualClosingBehavior = DockControllerHelper.GetActualClosingBehavior(container, base.Item);
                this.CreateBarCheckItem(MenuItems.Dockable, new bool?(!isFloating && !isAutoHidden), false, DockControllerHelper.CreateCommand<DockCommand>(container, base.Item));
                this.CreateBarCheckItem(MenuItems.Floating, new bool?(isFloating), false, DockControllerHelper.CreateCommand<FloatCommand>(container, base.Item));
                this.CreateBarCheckItem(MenuItems.AutoHide, new bool?(isAutoHidden), false, DockControllerHelper.CreateCommand<HideCommand>(container, base.Item));
                this.CreateBarButtonItem((actualClosingBehavior == ClosingBehavior.ImmediatelyRemove) ? MenuItems.Close : MenuItems.Hide, false, DockControllerHelper.CreateCommand<CloseCommand>(container, base.Item));
                LayoutGroup parent = base.Item.Parent;
                if ((parent != null) && (parent.ItemType == LayoutItemType.DocumentPanelGroup))
                {
                    this.CreateBarButtonItem(MenuItems.CloseAllButThis, false, DockControllerHelper.CreateCommand<CloseAllButThisCommand>(container, base.Item));
                    base.CreateBarItemLinkSeparator(MenuItems.TabOperationsSeparator);
                    this.CreateBarCheckItem(MenuItems.PinTab, new bool?(DocumentGroup.GetPinned(base.Item)), false, DockControllerHelper.CreateCommand<PinTabCommand>(container, base.Item));
                    bool flag3 = DockControllerHelper.GetPreviousNotEmptyDocumentGroup(parent) != null;
                    bool flag4 = DockControllerHelper.GetNextNotEmptyDocumentGroup(parent) != null;
                    if ((flag4 | flag3) || (parent.Items.Count > 1))
                    {
                        base.CreateBarItemLinkSeparator(MenuItems.DocumentHostOperationsSeparator);
                    }
                    if (parent.Items.Count > 1)
                    {
                        bool flag5 = true;
                        bool flag6 = true;
                        if (flag3 | flag4)
                        {
                            flag5 = (parent.Parent != null) && (parent.Parent.Orientation == Orientation.Horizontal);
                            flag6 = !flag5;
                        }
                        if (flag6)
                        {
                            this.CreateBarButtonItem(MenuItems.NewHorizontalTabbedGroup, false, DockControllerHelper.CreateCommand<NewVerticalDocumentGroupCommand>(container, base.Item));
                        }
                        if (flag5)
                        {
                            this.CreateBarButtonItem(MenuItems.NewVerticalTabbedGroup, false, DockControllerHelper.CreateCommand<NewHorizontalDocumentGroupCommand>(container, base.Item));
                        }
                    }
                    if (flag3)
                    {
                        this.CreateBarButtonItem(MenuItems.MoveToPreviousTabGroup, false, DockControllerHelper.CreateCommand<MoveToPreviousDocumentGroupCommand>(container, base.Item));
                    }
                    if (flag4)
                    {
                        this.CreateBarButtonItem(MenuItems.MoveToNextTabGroup, false, DockControllerHelper.CreateCommand<MoveToNextDocumentGroupCommand>(container, base.Item));
                    }
                }
            }
            if ((base.Item is LayoutPanel) && (((LayoutPanel) base.Item).Content is LayoutGroup))
            {
                if (!container.IsCustomization)
                {
                    base.CreateBeginCustomizationMenuItem(true);
                }
                else
                {
                    base.CreateStandardCustomizationMenuItems(true);
                    if (container.RenameHelper.CanRename(base.Item))
                    {
                        BaseLayoutItem[] items = new BaseLayoutItem[] { base.Item };
                        this.CreateBarButtonItem(MenuItems.Rename, false, LayoutControllerHelper.CreateCommand<RenameCommand>(base.Menu.Container, items));
                    }
                }
            }
            if (base.Menu.Container.CustomizationController.ClosedPanelsBarVisibility != ClosedPanelsBarVisibility.Never)
            {
                base.CreateBarItemLinkSeparator(MenuItems.ClosedPanelsSeparator);
                if ((base.Menu.Container.ClosedPanels.Count > 0) || base.Menu.Container.CustomizationController.IsClosedPanelsVisible)
                {
                    this.CreateBarCheckItem(MenuItems.ClosedPanels, new bool?(base.Menu.Container.CustomizationController.IsClosedPanelsVisible), false, CustomizationControllerHelper.CreateCommand(this.ItemMenu.Container));
                }
            }
        }

        public ItemContextMenu ItemMenu =>
            base.Menu as ItemContextMenu;

        public override BarManagerMenuController MenuController
        {
            get
            {
                Func<BaseLayoutElementMenu, DockLayoutManager> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<BaseLayoutElementMenu, DockLayoutManager> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x.Container;
                }
                Func<DockLayoutManager, ICustomizationController> func2 = <>c.<>9__5_1;
                if (<>c.<>9__5_1 == null)
                {
                    Func<DockLayoutManager, ICustomizationController> local2 = <>c.<>9__5_1;
                    func2 = <>c.<>9__5_1 = x => x.CustomizationController;
                }
                Func<ICustomizationController, BarManagerMenuController> func3 = <>c.<>9__5_2;
                if (<>c.<>9__5_2 == null)
                {
                    Func<ICustomizationController, BarManagerMenuController> local3 = <>c.<>9__5_2;
                    func3 = <>c.<>9__5_2 = x => x.ItemContextMenuController;
                }
                return base.Menu.With<BaseLayoutElementMenu, DockLayoutManager>(evaluator).With<DockLayoutManager, ICustomizationController>(func2).With<ICustomizationController, BarManagerMenuController>(func3);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemContextMenuInfo.<>c <>9 = new ItemContextMenuInfo.<>c();
            public static Func<BaseLayoutElementMenu, DockLayoutManager> <>9__5_0;
            public static Func<DockLayoutManager, ICustomizationController> <>9__5_1;
            public static Func<ICustomizationController, BarManagerMenuController> <>9__5_2;

            internal DockLayoutManager <get_MenuController>b__5_0(BaseLayoutElementMenu x) => 
                x.Container;

            internal ICustomizationController <get_MenuController>b__5_1(DockLayoutManager x) => 
                x.CustomizationController;

            internal BarManagerMenuController <get_MenuController>b__5_2(ICustomizationController x) => 
                x.ItemContextMenuController;
        }
    }
}

