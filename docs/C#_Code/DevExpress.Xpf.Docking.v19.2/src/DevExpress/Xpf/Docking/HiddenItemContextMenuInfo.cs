namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.Customization;
    using System;
    using System.Runtime.CompilerServices;

    public class HiddenItemContextMenuInfo : ItemContextMenuInfoBase
    {
        public HiddenItemContextMenuInfo(HiddenItemContextMenu menu, BaseLayoutItem item) : base(menu, item)
        {
        }

        protected override void CreateItems()
        {
            base.Menu.Container.CustomizationController.CustomizationRoot = base.Item.GetRoot();
            BaseLayoutItem[] items = new BaseLayoutItem[] { base.Item };
            this.CreateBarButtonItem(MenuItems.RestoreItem, false, LayoutControllerHelper.CreateCommand<RestoreItemCommand>(base.Menu.Container, items));
        }

        public HiddenItemContextMenu HiddenItemMenu =>
            base.Menu as HiddenItemContextMenu;

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
                    func3 = <>c.<>9__5_2 = x => x.HiddenItemsMenuController;
                }
                return base.Menu.With<BaseLayoutElementMenu, DockLayoutManager>(evaluator).With<DockLayoutManager, ICustomizationController>(func2).With<ICustomizationController, BarManagerMenuController>(func3);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HiddenItemContextMenuInfo.<>c <>9 = new HiddenItemContextMenuInfo.<>c();
            public static Func<BaseLayoutElementMenu, DockLayoutManager> <>9__5_0;
            public static Func<DockLayoutManager, ICustomizationController> <>9__5_1;
            public static Func<ICustomizationController, BarManagerMenuController> <>9__5_2;

            internal DockLayoutManager <get_MenuController>b__5_0(BaseLayoutElementMenu x) => 
                x.Container;

            internal ICustomizationController <get_MenuController>b__5_1(DockLayoutManager x) => 
                x.CustomizationController;

            internal BarManagerMenuController <get_MenuController>b__5_2(ICustomizationController x) => 
                x.HiddenItemsMenuController;
        }
    }
}

