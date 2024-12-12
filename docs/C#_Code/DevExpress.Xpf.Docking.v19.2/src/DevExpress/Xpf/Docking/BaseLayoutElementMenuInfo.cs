namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.Customization;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class BaseLayoutElementMenuInfo : CustomizablePopupMenuInfoBase
    {
        public BaseLayoutElementMenuInfo(BaseLayoutElementMenu menu) : base(menu)
        {
        }

        protected virtual BarButtonItem CreateBarButtonItem(MenuItems menuItem, bool beginGroup, ICommand command) => 
            base.CreateBarButtonItem(GetName(menuItem), GetContent(menuItem), beginGroup, null, command, null);

        protected virtual BarCheckItem CreateBarCheckItem(MenuItems menuItem, bool? isChecked, bool beginGroup, ICommand command) => 
            base.CreateBarCheckItem(GetName(menuItem), GetContent(menuItem), isChecked, beginGroup, null, command);

        protected virtual BarCheckItem CreateBarCheckItem(string content, bool? isChecked, bool beginGroup, ICommand command) => 
            base.CreateBarCheckItem(CustomizationController.GetUniqueMenuItemName(), content, isChecked, beginGroup, null, command);

        protected BarCheckItem CreateBarCheckSubItem(BarSubItem barSubItem, MenuItems menuItem, bool? isChecked, LayoutControllerCommand command) => 
            base.CreateBarCheckItem(barSubItem.ItemLinks, GetName(menuItem), GetContent(menuItem), isChecked, false, null, command);

        protected BarItemLinkSeparator CreateBarItemLinkSeparator(MenuItems menuItem)
        {
            BarItemLinkSeparator separator1 = new BarItemLinkSeparator();
            separator1.Name = MenuItemHelper.GetUniqueName(menuItem);
            BarItemLinkSeparator item = separator1;
            this.Menu.ItemLinks.Add(item);
            return item;
        }

        protected BarSubItem CreateBarSubItem(MenuItems menuItem, ImageSource glyph, bool beginGroup) => 
            base.CreateBarSubItem(GetName(menuItem), GetContent(menuItem), beginGroup, glyph, null);

        protected void CreateBeginCustomizationMenuItem(bool beginGroup)
        {
            if (this.Menu.Container.AllowCustomization)
            {
                if (beginGroup)
                {
                    this.CreateBarItemLinkSeparator(MenuItems.CustomizationOperationsSeparator);
                }
                this.CreateBarButtonItem(MenuItems.BeginCustomization, false, CustomizationControllerHelper.CreateCommand<BeginCustomizationCommand>(this.Menu.Container));
            }
        }

        protected void CreateStandardCustomizationMenuItems(bool beginGroup)
        {
            if (beginGroup)
            {
                this.CreateBarItemLinkSeparator(MenuItems.CustomizationOperationsSeparator);
            }
            this.CreateBarButtonItem(MenuItems.EndCustomization, false, CustomizationControllerHelper.CreateCommand<EndCustomizationCommand>(this.Menu.Container));
            if (this.Menu.Container.CustomizationController.IsCustomizationFormVisible)
            {
                this.CreateBarButtonItem(MenuItems.HideCustomizationWindow, false, CustomizationControllerHelper.CreateCommand<HideCustomizationFormCommand>(this.Menu.Container));
            }
            else
            {
                this.CreateBarButtonItem(MenuItems.ShowCustomizationWindow, false, CustomizationControllerHelper.CreateCommand<ShowCustomizationFormCommand>(this.Menu.Container));
            }
        }

        private void ExecuteItemCustomizations()
        {
            BaseLayoutItem item = (this.Menu.PlacementTarget != null) ? ((this.Menu.PlacementTarget as BaseLayoutItem) ?? DockLayoutManager.GetLayoutItem(this.Menu.PlacementTarget)) : null;
            if (item != null)
            {
                BarManagerHelper.SetLinksHolder(item.ContextMenuActionGroup, this.Menu);
                foreach (IControllerAction action in item.ContextMenuActionGroup.Actions)
                {
                    (action as DependencyObject).Do<DependencyObject>(x => BarManagerHelper.SetLinksHolder(x, this.Menu));
                }
                ((IControllerAction) item.ContextMenuActionGroup).Execute(this.Menu);
            }
        }

        protected override void ExecuteMenuController()
        {
            base.ExecuteMenuController();
            this.ExecuteItemCustomizations();
        }

        private static string GetContent(MenuItems menuItem) => 
            MenuItemHelper.GetContent(menuItem).ToString();

        private static string GetName(MenuItems menuItem) => 
            MenuItemHelper.GetUniqueName(menuItem);

        public override void Uninitialize()
        {
            this.UninitializeItemCustomizations();
            base.Uninitialize();
        }

        private void UninitializeItemCustomizations()
        {
            BaseLayoutItem item = (this.Menu.PlacementTarget != null) ? ((this.Menu.PlacementTarget as BaseLayoutItem) ?? DockLayoutManager.GetLayoutItem(this.Menu.PlacementTarget)) : null;
            if (item != null)
            {
                item.ContextMenuActionGroup.ClearValue(BarManagerHelper.LinksHolderProperty);
                foreach (IControllerAction action in item.ContextMenuActionGroup.Actions)
                {
                    Action<DependencyObject> action1 = <>c.<>9__18_0;
                    if (<>c.<>9__18_0 == null)
                    {
                        Action<DependencyObject> local2 = <>c.<>9__18_0;
                        action1 = <>c.<>9__18_0 = x => x.ClearValue(BarManagerHelper.LinksHolderProperty);
                    }
                    (action as DependencyObject).Do<DependencyObject>(action1);
                }
            }
        }

        public override bool CanCreateItems =>
            true;

        public BaseLayoutElementMenu Menu =>
            (BaseLayoutElementMenu) base.Menu;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseLayoutElementMenuInfo.<>c <>9 = new BaseLayoutElementMenuInfo.<>c();
            public static Action<DependencyObject> <>9__18_0;

            internal void <UninitializeItemCustomizations>b__18_0(DependencyObject x)
            {
                x.ClearValue(BarManagerHelper.LinksHolderProperty);
            }
        }
    }
}

