namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Customization;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class BaseLayoutElementMenu : CustomizablePopupMenuBase
    {
        public static readonly object RuntimeCreatedMenuItem = new object();

        static BaseLayoutElementMenu()
        {
            EventManager.RegisterClassHandler(typeof(FrameworkElement), Mouse.PreviewMouseDownOutsideCapturedElementEvent, new MouseButtonEventHandler(BaseLayoutElementMenu.OnClickThroughThunk));
            EventManager.RegisterClassHandler(typeof(BaseLayoutElementMenu), FrameworkElement.ContextMenuOpeningEvent, new ContextMenuEventHandler(BaseLayoutElementMenu.OnContextMenuOpening));
        }

        public BaseLayoutElementMenu(DockLayoutManager container) : base(container)
        {
            this.Container = container;
            base.Placement = PlacementMode.MousePoint;
            base.AllowMouseCapturing = true;
            BarManagerHelper.SetMDIChildHost(this, null);
        }

        public void AddItem(BarItem barItem, bool isBeginGroup)
        {
            barItem.Tag = RuntimeCreatedMenuItem;
            if (isBeginGroup)
            {
                base.ItemLinks.Add(new BarItemLinkSeparator());
            }
            base.ItemLinks.Add(barItem);
        }

        public override void ClearItems()
        {
            Func<FrameworkContentElement, DependencyObject> selector = <>c.<>9__22_0;
            if (<>c.<>9__22_0 == null)
            {
                Func<FrameworkContentElement, DependencyObject> local1 = <>c.<>9__22_0;
                selector = <>c.<>9__22_0 = x => x.Parent;
            }
            foreach (BarManagerActionContainer container in base.Items.OfType<FrameworkContentElement>().Select<FrameworkContentElement, DependencyObject>(selector).OfType<BarManagerActionContainer>().Distinct<BarManagerActionContainer>())
            {
                container.ClearValue(FrameworkElement.DataContextProperty);
            }
            base.ClearItems();
        }

        private void ClearPopupRoot()
        {
            DependencyObject obj2 = (base.Child != null) ? LayoutHelper.FindRoot(base.Child, false) : null;
            if (obj2 != null)
            {
                DockLayoutManager.SetLayoutItem(obj2, null);
            }
        }

        public void Close()
        {
            this.ClosePopup();
        }

        public BarButtonItem CreateBarButtonItem(MenuItems menuItem, ImageSource glyph, bool beginGroup)
        {
            BarButtonItem barItem = this.CreateBarButtonItemCore(menuItem, glyph);
            this.AddItem(barItem, beginGroup);
            return barItem;
        }

        public BarButtonItem CreateBarButtonItem(object content, ImageSource glyph, bool beginGroup)
        {
            BarButtonItem barItem = this.CreateBarButtonItemCore(content, glyph);
            this.AddItem(barItem, beginGroup);
            return barItem;
        }

        protected internal BarButtonItem CreateBarButtonItemCore(MenuItems menuItem, ImageSource glyph)
        {
            BarButtonItem item1 = new BarButtonItem();
            item1.IsPrivate = true;
            item1.Name = MenuItemHelper.GetUniqueName(menuItem);
            item1.Content = MenuItemHelper.GetContent(menuItem);
            item1.Glyph = glyph;
            return item1;
        }

        protected internal BarButtonItem CreateBarButtonItemCore(object content, ImageSource glyph)
        {
            BarButtonItem item1 = new BarButtonItem();
            item1.IsPrivate = true;
            item1.Name = CustomizationController.GetUniqueMenuItemName();
            item1.Content = content;
            item1.Glyph = glyph;
            return item1;
        }

        public BarCheckItem CreateBarCheckItem(MenuItems menuItem, bool? isChecked, bool beginGroup)
        {
            BarCheckItem barItem = this.CreateBarCheckItemCore(menuItem, isChecked);
            this.AddItem(barItem, beginGroup);
            return barItem;
        }

        public BarCheckItem CreateBarCheckItem(object content, bool? isChecked, bool beginGroup)
        {
            BarCheckItem barItem = this.CreateBarCheckItemCore(content, isChecked);
            this.AddItem(barItem, beginGroup);
            return barItem;
        }

        protected internal BarCheckItem CreateBarCheckItemCore(MenuItems menuItem, bool? isChecked)
        {
            BarCheckItem item1 = new BarCheckItem();
            item1.IsPrivate = true;
            item1.Name = MenuItemHelper.GetUniqueName(menuItem);
            item1.Content = MenuItemHelper.GetContent(menuItem);
            item1.IsChecked = isChecked;
            return item1;
        }

        protected internal BarCheckItem CreateBarCheckItemCore(object content, bool? isChecked)
        {
            BarCheckItem item1 = new BarCheckItem();
            item1.IsPrivate = true;
            item1.Name = CustomizationController.GetUniqueMenuItemName();
            item1.Content = content;
            item1.IsChecked = isChecked;
            return item1;
        }

        public BarSubItem CreateBarSubItem(MenuItems menuItem, ImageSource glyph, bool beginGroup)
        {
            BarSubItem barItem = this.CreateBarSubItemCore(menuItem, glyph);
            this.AddItem(barItem, beginGroup);
            return barItem;
        }

        public BarSubItem CreateBarSubItem(object content, ImageSource glyph, bool beginGroup)
        {
            BarSubItem barItem = this.CreateBarSubItemCore(content, glyph);
            this.AddItem(barItem, beginGroup);
            return barItem;
        }

        protected virtual BarSubItem CreateBarSubItemCore(MenuItems menuItem, ImageSource glyph)
        {
            BarSubItem item1 = new BarSubItem();
            item1.IsPrivate = true;
            item1.Name = MenuItemHelper.GetUniqueName(menuItem);
            item1.Content = MenuItemHelper.GetContent(menuItem);
            item1.Glyph = glyph;
            return item1;
        }

        protected virtual BarSubItem CreateBarSubItemCore(object content, ImageSource glyph)
        {
            BarSubItem item1 = new BarSubItem();
            item1.IsPrivate = true;
            item1.Name = CustomizationController.GetUniqueMenuItemName();
            item1.Content = content;
            item1.Glyph = glyph;
            return item1;
        }

        private static void GeneratePreviewouseDownEvent(DockLayoutManager manager, MouseButtonEventArgs e)
        {
            if (manager != null)
            {
                MouseButtonEventArgs args1 = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton, e.StylusDevice);
                args1.RoutedEvent = UIElement.PreviewMouseDownEvent;
                manager.RaiseEvent(args1);
            }
        }

        public ReadOnlyCollection<BarItem> GetItems()
        {
            List<BarItem> list = new List<BarItem>();
            foreach (BarItemLinkBase base2 in base.ItemLinks)
            {
                if ((base2 is BarItemLink) && (((BarItemLink) base2).Item != null))
                {
                    list.Add(((BarItemLink) base2).Item);
                }
                if (base2 is BarSubItemLink)
                {
                    foreach (BarItemLink link in ((BarSubItemLink) base2).ItemLinks)
                    {
                        list.Add(link.Item);
                    }
                }
            }
            return new ReadOnlyCollection<BarItem>(list);
        }

        public void MoveItem(BarItem item, int index)
        {
            List<BarItemLinkBase> list = new List<BarItemLinkBase>(base.ItemLinks);
            for (int i = 0; i < list.Count; i++)
            {
                BarItemLink link = list[i] as BarItemLink;
                if ((link != null) && ReferenceEquals(link.Item, item))
                {
                    base.ItemLinks.Move(i, index);
                }
            }
            BarDragProvider.RemoveUnnesessarySeparators(base.ItemLinks);
        }

        private static void OnClickThroughThunk(object sender, MouseButtonEventArgs e)
        {
            if ((sender is BaseLayoutElementMenu) && DockLayoutManagerHelper.IsPopupRoot(e.OriginalSource))
            {
                GeneratePreviewouseDownEvent(((BaseLayoutElementMenu) sender).Container, e);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (this.MenuInfo != null)
            {
                this.MenuInfo.Uninitialize();
            }
            base.PlacementTarget = null;
            base.ClearValue(DockLayoutManager.LayoutItemProperty);
            this.ClearPopupRoot();
            base.OnClosed(e);
        }

        private static void OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            BaseLayoutElementMenu menu = sender as BaseLayoutElementMenu;
            if (menu != null)
            {
                e.Handled = menu.IsOpen;
            }
        }

        protected override bool RaiseShowMenu()
        {
            ShowingMenuEventArgs args1 = new ShowingMenuEventArgs(this);
            args1.Source = this.Container;
            ShowingMenuEventArgs e = args1;
            this.Container.RaiseEvent(e);
            return e.Show;
        }

        public void RemoveItem(BarItem item)
        {
            base.Items.Remove(item);
            BarDragProvider.RemoveUnnesessarySeparators(base.ItemLinks);
        }

        public override void ShowPopup(UIElement control)
        {
            if (control != null)
            {
                base.ShowPopup(control);
            }
        }

        protected override void UpdateDataContext()
        {
            BaseLayoutItem item = (base.PlacementTarget != null) ? ((base.PlacementTarget as BaseLayoutItem) ?? DockLayoutManager.GetLayoutItem(base.PlacementTarget)) : null;
            if (item != null)
            {
                base.DataContext = item.DataContext;
            }
            else
            {
                base.UpdateDataContext();
            }
        }

        protected override bool ShouldClearItemsOnClose =>
            true;

        public DockLayoutManager Container { get; private set; }

        public BaseLayoutElementMenuInfo MenuInfo =>
            (BaseLayoutElementMenuInfo) base.MenuInfo;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseLayoutElementMenu.<>c <>9 = new BaseLayoutElementMenu.<>c();
            public static Func<FrameworkContentElement, DependencyObject> <>9__22_0;

            internal DependencyObject <ClearItems>b__22_0(FrameworkContentElement x) => 
                x.Parent;
        }
    }
}

