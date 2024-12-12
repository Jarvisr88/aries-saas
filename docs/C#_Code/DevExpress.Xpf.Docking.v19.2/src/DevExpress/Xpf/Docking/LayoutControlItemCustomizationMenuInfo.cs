namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.Customization;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutControlItemCustomizationMenuInfo : BaseLayoutElementMenuInfo
    {
        public LayoutControlItemCustomizationMenuInfo(LayoutControlItemCustomizationMenu menu, BaseLayoutItem[] items) : base(menu)
        {
            this.Items = items;
        }

        protected override void CreateItems()
        {
            base.CreateStandardCustomizationMenuItems(false);
            int num = 0;
            bool? isChecked = LayoutControllerHelper.GetSameValue(BaseLayoutItem.ShowCaptionProperty, this.Items, new Comparison<object>(LayoutControllerHelper.BoolComparer)) as bool?;
            bool? nullable2 = LayoutControllerHelper.GetSameValue(BaseLayoutItem.HasCaptionProperty, this.Items, new Comparison<object>(LayoutControllerHelper.BoolComparer)) as bool?;
            object obj2 = LayoutControllerHelper.GetSameValue(BaseLayoutItem.CaptionLocationProperty, this.Items, new Comparison<object>(LayoutControllerHelper.CompareCaptionLocation));
            bool? nullable3 = LayoutControllerHelper.GetSameValue(LayoutControlItem.ShowControlProperty, this.Items, new Comparison<object>(LayoutControllerHelper.BoolComparer)) as bool?;
            bool? nullable4 = LayoutControllerHelper.GetSameValue(LayoutControlItem.HasControlProperty, this.Items, new Comparison<object>(LayoutControllerHelper.BoolComparer)) as bool?;
            bool? nullable5 = (bool?) LayoutControllerHelper.GetSameValue(BaseLayoutItem.HasImageProperty, this.Items, new Comparison<object>(LayoutControllerHelper.BoolComparer));
            bool? nullable6 = (bool?) LayoutControllerHelper.GetSameValue(BaseLayoutItem.ShowCaptionImageProperty, this.Items, new Comparison<object>(LayoutControllerHelper.BoolComparer));
            object obj3 = LayoutControllerHelper.GetSameValue(BaseLayoutItem.CaptionImageLocationProperty, this.Items, new Comparison<object>(LayoutControllerHelper.CompareImageLocation));
            if ((isChecked != null) && ((nullable2 != null) && nullable2.Value))
            {
                this.CreateBarCheckItem(MenuItems.ShowCaption, isChecked, num++ == 0, LayoutControllerHelper.CreateCommand<ShowCaptionCommand>(this.LayoutControlItemMenu.Container, this.Items));
            }
            if ((nullable3 != null) && ((nullable4 != null) && nullable4.Value))
            {
                this.CreateBarCheckItem(MenuItems.ShowControl, nullable3, num++ == 0, LayoutControllerHelper.CreateCommand<ShowControlCommand>(this.LayoutControlItemMenu.Container, this.Items));
            }
            if ((nullable6 != null) && ((nullable5 != null) && nullable5.Value))
            {
                this.CreateBarCheckItem(MenuItems.ShowCaptionImage, nullable6, num++ == 0, LayoutControllerHelper.CreateCommand<ShowCaptionImageCommand>(this.LayoutControlItemMenu.Container, this.Items));
            }
            if ((obj2 != null) && ((nullable2 != null) && (nullable2.Value && ((isChecked != null) && isChecked.Value))))
            {
                BarSubItem barSubItem = base.CreateBarSubItem(MenuItems.CaptionLocation, null, num++ == 0);
                CaptionLocation location = (CaptionLocation) obj2;
                this.CreateBarCheckSubItem(barSubItem, MenuItems.Left, new bool?((location == CaptionLocation.Left) || (location == CaptionLocation.Default)), LayoutControllerHelper.CreateCommand<CaptionLocationLeftCommand>(base.Menu.Container, this.Items));
                base.CreateBarCheckSubItem(barSubItem, MenuItems.Right, new bool?(location == CaptionLocation.Right), LayoutControllerHelper.CreateCommand<CaptionLocationRightCommand>(base.Menu.Container, this.Items));
                base.CreateBarCheckSubItem(barSubItem, MenuItems.Top, new bool?(location == CaptionLocation.Top), LayoutControllerHelper.CreateCommand<CaptionLocationTopCommand>(base.Menu.Container, this.Items));
                base.CreateBarCheckSubItem(barSubItem, MenuItems.Bottom, new bool?(location == CaptionLocation.Bottom), LayoutControllerHelper.CreateCommand<CaptionLocationBottomCommand>(base.Menu.Container, this.Items));
            }
            if ((obj3 != null) && ((nullable5 != null) && (nullable5.Value && ((nullable6 != null) && nullable6.Value))))
            {
                BarSubItem barSubItem = base.CreateBarSubItem(MenuItems.CaptionImageLocation, null, num++ == 0);
                ImageLocation location2 = (ImageLocation) obj3;
                base.CreateBarCheckSubItem(barSubItem, MenuItems.BeforeText, new bool?(location2 != ImageLocation.AfterText), LayoutControllerHelper.CreateCommand<CaptionImageBeforeTextCommand>(base.Menu.Container, this.Items));
                base.CreateBarCheckSubItem(barSubItem, MenuItems.AfterText, new bool?(location2 == ImageLocation.AfterText), LayoutControllerHelper.CreateCommand<CaptionImageAfterTextCommand>(base.Menu.Container, this.Items));
            }
            this.CreateBarButtonItem(MenuItems.HideItem, true, LayoutControllerHelper.CreateCommand<HideItemCommand>(base.Menu.Container, this.Items));
            if (LayoutItemsHelper.AreInSameGroup(this.Items))
            {
                this.CreateBarButtonItem(MenuItems.GroupItems, false, LayoutControllerHelper.CreateCommand<GroupCommand>(base.Menu.Container, this.Items));
            }
            if (base.Menu.Container.RenameHelper.CanRename(base.Menu.Container.ActiveLayoutItem))
            {
                BaseLayoutItem[] items = new BaseLayoutItem[] { base.Menu.Container.ActiveLayoutItem };
                this.CreateBarButtonItem(MenuItems.Rename, false, LayoutControllerHelper.CreateCommand<RenameCommand>(base.Menu.Container, items));
            }
        }

        public BaseLayoutItem[] Items { private get; set; }

        public LayoutControlItemCustomizationMenu LayoutControlItemMenu =>
            base.Menu as LayoutControlItemCustomizationMenu;

        public override BarManagerMenuController MenuController
        {
            get
            {
                Func<BaseLayoutElementMenu, DockLayoutManager> evaluator = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<BaseLayoutElementMenu, DockLayoutManager> local1 = <>c.<>9__9_0;
                    evaluator = <>c.<>9__9_0 = x => x.Container;
                }
                Func<DockLayoutManager, ICustomizationController> func2 = <>c.<>9__9_1;
                if (<>c.<>9__9_1 == null)
                {
                    Func<DockLayoutManager, ICustomizationController> local2 = <>c.<>9__9_1;
                    func2 = <>c.<>9__9_1 = x => x.CustomizationController;
                }
                Func<ICustomizationController, BarManagerMenuController> func3 = <>c.<>9__9_2;
                if (<>c.<>9__9_2 == null)
                {
                    Func<ICustomizationController, BarManagerMenuController> local3 = <>c.<>9__9_2;
                    func3 = <>c.<>9__9_2 = x => x.LayoutControlItemCustomizationMenuController;
                }
                return base.Menu.With<BaseLayoutElementMenu, DockLayoutManager>(evaluator).With<DockLayoutManager, ICustomizationController>(func2).With<ICustomizationController, BarManagerMenuController>(func3);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutControlItemCustomizationMenuInfo.<>c <>9 = new LayoutControlItemCustomizationMenuInfo.<>c();
            public static Func<BaseLayoutElementMenu, DockLayoutManager> <>9__9_0;
            public static Func<DockLayoutManager, ICustomizationController> <>9__9_1;
            public static Func<ICustomizationController, BarManagerMenuController> <>9__9_2;

            internal DockLayoutManager <get_MenuController>b__9_0(BaseLayoutElementMenu x) => 
                x.Container;

            internal ICustomizationController <get_MenuController>b__9_1(DockLayoutManager x) => 
                x.CustomizationController;

            internal BarManagerMenuController <get_MenuController>b__9_2(ICustomizationController x) => 
                x.LayoutControlItemCustomizationMenuController;
        }
    }
}

