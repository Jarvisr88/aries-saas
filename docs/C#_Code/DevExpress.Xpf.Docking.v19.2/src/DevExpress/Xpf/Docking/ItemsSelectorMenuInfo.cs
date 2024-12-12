namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class ItemsSelectorMenuInfo : BaseLayoutElementMenuInfo
    {
        public ItemsSelectorMenuInfo(ItemsSelectorMenu menu, BaseLayoutItem[] items) : base(menu)
        {
            this.Items = items;
        }

        protected override void CreateItems()
        {
            DependencyObject targetElement = base.TargetElement as DependencyObject;
            if (targetElement != null)
            {
                LayoutGroup layoutItem = DockLayoutManager.GetLayoutItem(targetElement) as LayoutGroup;
                bool isInLayout = false;
                if (layoutItem == null)
                {
                    this.CreateMenuItemsForElement(targetElement);
                }
                else
                {
                    isInLayout = layoutItem.GetRoot().IsLayoutRoot;
                    this.CreateMenuItemsForGroup(layoutItem, isInLayout);
                }
                if ((base.Menu.Container.CustomizationController.ClosedPanelsBarVisibility != ClosedPanelsBarVisibility.Never) && (!isInLayout && ((base.Menu.Container.ClosedPanels.Count > 0) || base.Menu.Container.CustomizationController.IsClosedPanelsVisible)))
                {
                    this.CreateBarCheckItem(MenuItems.ClosedPanels, new bool?(base.Menu.Container.CustomizationController.IsClosedPanelsVisible), base.Menu.ItemLinks.Count > 0, CustomizationControllerHelper.CreateCommand(this.ItemsMenu.Container));
                }
            }
        }

        private void CreateMenuItems(IEnumerable<BaseLayoutItem> items)
        {
            Func<BaseLayoutItem, bool> predicate = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c.<>9__10_0;
                predicate = <>c.<>9__10_0 = x => x.IsVisibleCore;
            }
            List<BaseLayoutItem> list = items.Where<BaseLayoutItem>(predicate).ToList<BaseLayoutItem>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseLayoutItem item = list[i];
                BarCheckItem target = this.CreateBarCheckItem(item.CustomizationCaption, new bool?(item.IsSelectedItem), (i == 0) && (this.ItemsMenu.ItemLinks.Count > 0), DockControllerHelper.CreateCommand<ActivateCommand>(this.ItemsMenu.Container, item));
                target.SetCurrentValue(FrameworkContentElement.TagProperty, item);
                BindingHelper.SetBinding(target, BarItem.ContentProperty, item, BaseLayoutItem.ActualCustomizationCaptionProperty, BindingMode.OneWay);
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(ItemsSelectorMenuContentPresenter));
                Binding binding1 = new Binding();
                binding1.Source = item;
                factory.SetValue(DockLayoutManager.LayoutItemProperty, binding1);
                factory.SetValue(ContentPresenter.ContentProperty, new Binding());
                Binding binding2 = new Binding();
                binding2.Path = new PropertyPath(BaseLayoutItem.ActualCustomizationCaptionTemplateProperty);
                binding2.Source = item;
                factory.SetValue(ContentPresenter.ContentTemplateProperty, binding2);
                Binding binding3 = new Binding();
                binding3.Path = new PropertyPath(BaseLayoutItem.ActualCustomizationCaptionTemplateSelectorProperty);
                binding3.Source = item;
                factory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, binding3);
                DataTemplate template1 = new DataTemplate();
                template1.VisualTree = factory;
                DataTemplate template = template1;
                template.Seal();
                target.ContentTemplate = template;
            }
        }

        private void CreateMenuItemsForElement(DependencyObject targetElement)
        {
            if (targetElement is AutoHideTray)
            {
                this.CreateMenuItems(this.Items);
            }
        }

        private void CreateMenuItemsForGroup(LayoutGroup group, bool isInLayout)
        {
            if (group != null)
            {
                bool flag = ReferenceEquals(group.Parent, null);
                int num = 0;
                if (isInLayout)
                {
                    if (!base.Menu.Container.IsCustomization)
                    {
                        base.CreateBeginCustomizationMenuItem(false);
                    }
                    else
                    {
                        base.CreateStandardCustomizationMenuItems(false);
                        if (!flag)
                        {
                            BaseLayoutItem[] items = base.Menu.Container.LayoutController.Selection.ToArray();
                            bool? isChecked = LayoutControllerHelper.GetSameValue(BaseLayoutItem.ShowCaptionProperty, items, new Comparison<object>(LayoutControllerHelper.BoolComparer)) as bool?;
                            bool? nullable2 = LayoutControllerHelper.GetSameValue(BaseLayoutItem.HasCaptionProperty, items, new Comparison<object>(LayoutControllerHelper.BoolComparer)) as bool?;
                            if ((isChecked != null) && ((nullable2 != null) && nullable2.Value))
                            {
                                this.CreateBarCheckItem(MenuItems.ShowCaption, isChecked, num++ == 0, LayoutControllerHelper.CreateCommand<ShowCaptionCommand>(base.Menu.Container, items));
                            }
                            if (group.GroupBorderStyle == GroupBorderStyle.Tabbed)
                            {
                                BarSubItem barSubItem = base.CreateBarSubItem(MenuItems.CaptionLocation, null, num++ == 0);
                                CaptionLocation captionLocation = group.CaptionLocation;
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.Left, new bool?(captionLocation == CaptionLocation.Left), LayoutControllerHelper.CreateCommand<CaptionLocationLeftCommand>(base.Menu.Container, items));
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.Right, new bool?(captionLocation == CaptionLocation.Right), LayoutControllerHelper.CreateCommand<CaptionLocationRightCommand>(base.Menu.Container, items));
                                this.CreateBarCheckSubItem(barSubItem, MenuItems.Top, new bool?((captionLocation == CaptionLocation.Top) || (captionLocation == CaptionLocation.Default)), LayoutControllerHelper.CreateCommand<CaptionLocationTopCommand>(base.Menu.Container, items));
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.Bottom, new bool?(captionLocation == CaptionLocation.Bottom), LayoutControllerHelper.CreateCommand<CaptionLocationBottomCommand>(base.Menu.Container, items));
                            }
                            object obj2 = LayoutControllerHelper.GetSameValue(LayoutGroup.OrientationProperty, items, new Comparison<object>(LayoutControllerHelper.CompareOrientation));
                            bool flag3 = LayoutControllerHelper.HasOnlyGroups(items);
                            if (flag3 && (obj2 != null))
                            {
                                BarSubItem barSubItem = base.CreateBarSubItem(MenuItems.GroupOrientation, null, num++ == 0);
                                Orientation orientation = (Orientation) obj2;
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.Horizontal, new bool?(orientation == Orientation.Horizontal), LayoutControllerHelper.CreateCommand<GroupOrientationHorizontalCommand>(base.Menu.Container, items));
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.Vertical, new bool?(orientation == Orientation.Vertical), LayoutControllerHelper.CreateCommand<GroupOrientationVerticalCommand>(base.Menu.Container, items));
                            }
                            obj2 = LayoutControllerHelper.GetSameValue(LayoutGroup.GroupBorderStyleProperty, items, new Comparison<object>(LayoutControllerHelper.CompareGroupBorderStyle));
                            if (flag3 && (obj2 != null))
                            {
                                BarSubItem barSubItem = base.CreateBarSubItem(MenuItems.Style, null, num++ == 0);
                                GroupBorderStyle style = (GroupBorderStyle) obj2;
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.StyleNoBorder, new bool?(style == GroupBorderStyle.NoBorder), LayoutControllerHelper.CreateCommand<SetStyleNoBorderCommand>(base.Menu.Container, items));
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.StyleGroup, new bool?(style == GroupBorderStyle.Group), LayoutControllerHelper.CreateCommand<SetStyleGroupCommand>(base.Menu.Container, items));
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.StyleGroupBox, new bool?(style == GroupBorderStyle.GroupBox), LayoutControllerHelper.CreateCommand<SetStyleGroupBoxCommand>(base.Menu.Container, items));
                                base.CreateBarCheckSubItem(barSubItem, MenuItems.StyleTabbed, new bool?(style == GroupBorderStyle.Tabbed), LayoutControllerHelper.CreateCommand<SetStyleTabbedCommand>(base.Menu.Container, items));
                            }
                            this.CreateBarButtonItem(MenuItems.HideItem, true, LayoutControllerHelper.CreateCommand<HideItemCommand>(base.Menu.Container, items));
                            if (LayoutItemsHelper.AreInSameGroup(items))
                            {
                                this.CreateBarButtonItem(MenuItems.GroupItems, false, LayoutControllerHelper.CreateCommand<GroupCommand>(base.Menu.Container, items));
                            }
                            if (items.Length == 1)
                            {
                                this.CreateBarButtonItem(MenuItems.Ungroup, false, LayoutControllerHelper.CreateCommand<UngroupCommand>(base.Menu.Container, items));
                            }
                            if (base.Menu.Container.RenameHelper.CanRename(group))
                            {
                                BaseLayoutItem[] itemArray1 = new BaseLayoutItem[] { group };
                                this.CreateBarButtonItem(MenuItems.Rename, false, LayoutControllerHelper.CreateCommand<RenameCommand>(base.Menu.Container, itemArray1));
                            }
                        }
                    }
                }
                if (!flag && ((group.ItemType == LayoutItemType.Group) && (group.GroupBorderStyle == GroupBorderStyle.GroupBox)))
                {
                    this.CreateBarCheckItem(MenuItems.ExpandGroup, new bool?(group.IsExpanded), false, DockControllerHelper.CreateCommand<ExpandCommand>(this.ItemsMenu.Container, group));
                }
                if (group.CanShowItemsInSelectorMenu())
                {
                    this.CreateMenuItems(this.Items);
                }
            }
        }

        public override void Uninitialize()
        {
            Action<BarItem> action = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Action<BarItem> local1 = <>c.<>9__13_0;
                action = <>c.<>9__13_0 = delegate (BarItem x) {
                    x.ClearValue(BarItem.ContentProperty);
                    x.ClearValue(BarItem.ContentTemplateProperty);
                };
            }
            base.Menu.Items.OfType<BarItem>().ForEach<BarItem>(action);
            base.Uninitialize();
            this.Items = null;
        }

        public BaseLayoutItem[] Items { get; private set; }

        public ItemsSelectorMenu ItemsMenu =>
            base.Menu as ItemsSelectorMenu;

        public override BarManagerMenuController MenuController
        {
            get
            {
                Func<BaseLayoutElementMenu, DockLayoutManager> evaluator = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<BaseLayoutElementMenu, DockLayoutManager> local1 = <>c.<>9__12_0;
                    evaluator = <>c.<>9__12_0 = x => x.Container;
                }
                Func<DockLayoutManager, ICustomizationController> func2 = <>c.<>9__12_1;
                if (<>c.<>9__12_1 == null)
                {
                    Func<DockLayoutManager, ICustomizationController> local2 = <>c.<>9__12_1;
                    func2 = <>c.<>9__12_1 = x => x.CustomizationController;
                }
                Func<ICustomizationController, BarManagerMenuController> func3 = <>c.<>9__12_2;
                if (<>c.<>9__12_2 == null)
                {
                    Func<ICustomizationController, BarManagerMenuController> local3 = <>c.<>9__12_2;
                    func3 = <>c.<>9__12_2 = x => x.ItemsSelectorMenuController;
                }
                return base.Menu.With<BaseLayoutElementMenu, DockLayoutManager>(evaluator).With<DockLayoutManager, ICustomizationController>(func2).With<ICustomizationController, BarManagerMenuController>(func3);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsSelectorMenuInfo.<>c <>9 = new ItemsSelectorMenuInfo.<>c();
            public static Func<BaseLayoutItem, bool> <>9__10_0;
            public static Func<BaseLayoutElementMenu, DockLayoutManager> <>9__12_0;
            public static Func<DockLayoutManager, ICustomizationController> <>9__12_1;
            public static Func<ICustomizationController, BarManagerMenuController> <>9__12_2;
            public static Action<BarItem> <>9__13_0;

            internal bool <CreateMenuItems>b__10_0(BaseLayoutItem x) => 
                x.IsVisibleCore;

            internal DockLayoutManager <get_MenuController>b__12_0(BaseLayoutElementMenu x) => 
                x.Container;

            internal ICustomizationController <get_MenuController>b__12_1(DockLayoutManager x) => 
                x.CustomizationController;

            internal BarManagerMenuController <get_MenuController>b__12_2(ICustomizationController x) => 
                x.ItemsSelectorMenuController;

            internal void <Uninitialize>b__13_0(BarItem x)
            {
                x.ClearValue(BarItem.ContentProperty);
                x.ClearValue(BarItem.ContentTemplateProperty);
            }
        }
    }
}

