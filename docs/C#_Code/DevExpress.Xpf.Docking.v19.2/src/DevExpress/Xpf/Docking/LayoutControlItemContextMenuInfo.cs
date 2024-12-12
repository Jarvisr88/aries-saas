namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutControlItemContextMenuInfo : ItemContextMenuInfoBase
    {
        public LayoutControlItemContextMenuInfo(LayoutControlItemContextMenu menu, BaseLayoutItem item) : base(menu, item)
        {
        }

        protected override void CreateItems()
        {
            base.CreateBeginCustomizationMenuItem(false);
        }

        public LayoutControlItemContextMenu LayoutControlItemMenu =>
            base.Menu as LayoutControlItemContextMenu;

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
                    func3 = <>c.<>9__5_2 = x => x.LayoutControlItemContextMenuController;
                }
                return base.Menu.With<BaseLayoutElementMenu, DockLayoutManager>(evaluator).With<DockLayoutManager, ICustomizationController>(func2).With<ICustomizationController, BarManagerMenuController>(func3);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutControlItemContextMenuInfo.<>c <>9 = new LayoutControlItemContextMenuInfo.<>c();
            public static Func<BaseLayoutElementMenu, DockLayoutManager> <>9__5_0;
            public static Func<DockLayoutManager, ICustomizationController> <>9__5_1;
            public static Func<ICustomizationController, BarManagerMenuController> <>9__5_2;

            internal DockLayoutManager <get_MenuController>b__5_0(BaseLayoutElementMenu x) => 
                x.Container;

            internal ICustomizationController <get_MenuController>b__5_1(DockLayoutManager x) => 
                x.CustomizationController;

            internal BarManagerMenuController <get_MenuController>b__5_2(ICustomizationController x) => 
                x.LayoutControlItemContextMenuController;
        }
    }
}

