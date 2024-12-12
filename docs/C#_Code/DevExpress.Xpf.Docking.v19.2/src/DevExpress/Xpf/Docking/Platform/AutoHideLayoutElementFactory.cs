namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class AutoHideLayoutElementFactory : LayoutElementFactory
    {
        protected override void InitializeFactory()
        {
            base.InitializeFactory();
            LayoutElementFactory.CreateInstance instance1 = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                LayoutElementFactory.CreateInstance local1 = <>c.<>9__0_0;
                instance1 = <>c.<>9__0_0 = (LayoutElementFactory.CreateInstance) ((element, view) => new AutoHideTrayElement(element, view));
            }
            base.Initializers[typeof(AutoHideTray)] = instance1;
            LayoutElementFactory.CreateInstance instance2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                LayoutElementFactory.CreateInstance local2 = <>c.<>9__0_1;
                instance2 = <>c.<>9__0_1 = (LayoutElementFactory.CreateInstance) ((element, view) => new AutoHidePaneElement(element, view));
            }
            base.Initializers[typeof(AutoHidePane)] = instance2;
            LayoutElementFactory.CreateInstance instance3 = <>c.<>9__0_2;
            if (<>c.<>9__0_2 == null)
            {
                LayoutElementFactory.CreateInstance local3 = <>c.<>9__0_2;
                instance3 = <>c.<>9__0_2 = (LayoutElementFactory.CreateInstance) ((element, view) => new AutoHideTrayHeadersGroupElement(element, view));
            }
            base.Initializers[typeof(AutoHideGroup)] = instance3;
            LayoutElementFactory.CreateInstance instance4 = <>c.<>9__0_3;
            if (<>c.<>9__0_3 == null)
            {
                LayoutElementFactory.CreateInstance local4 = <>c.<>9__0_3;
                instance4 = <>c.<>9__0_3 = (LayoutElementFactory.CreateInstance) ((element, view) => new AutoHidePaneHeaderItemElement(element, view));
            }
            base.Initializers[typeof(AutoHidePaneHeaderItem)] = instance4;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHideLayoutElementFactory.<>c <>9 = new AutoHideLayoutElementFactory.<>c();
            public static LayoutElementFactory.CreateInstance <>9__0_0;
            public static LayoutElementFactory.CreateInstance <>9__0_1;
            public static LayoutElementFactory.CreateInstance <>9__0_2;
            public static LayoutElementFactory.CreateInstance <>9__0_3;

            internal ILayoutElement <InitializeFactory>b__0_0(UIElement element, UIElement view) => 
                new AutoHideTrayElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_1(UIElement element, UIElement view) => 
                new AutoHidePaneElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_2(UIElement element, UIElement view) => 
                new AutoHideTrayHeadersGroupElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_3(UIElement element, UIElement view) => 
                new AutoHidePaneHeaderItemElement(element, view);
        }
    }
}

