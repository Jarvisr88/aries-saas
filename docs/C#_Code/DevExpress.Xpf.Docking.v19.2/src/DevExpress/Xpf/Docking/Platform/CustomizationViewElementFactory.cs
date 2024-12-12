namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomizationViewElementFactory : LayoutElementFactory
    {
        protected override void InitializeFactory()
        {
            LayoutElementFactory.CreateInstance instance1 = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                LayoutElementFactory.CreateInstance local1 = <>c.<>9__0_0;
                instance1 = <>c.<>9__0_0 = (LayoutElementFactory.CreateInstance) ((element, view) => new CustomizationControlElement(element, view));
            }
            base.Initializers[typeof(CustomizationControl)] = instance1;
            LayoutElementFactory.CreateInstance instance2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                LayoutElementFactory.CreateInstance local2 = <>c.<>9__0_1;
                instance2 = <>c.<>9__0_1 = (LayoutElementFactory.CreateInstance) ((element, view) => new HiddenItemElement(element, view));
            }
            base.Initializers[typeof(HiddenItem)] = instance2;
            LayoutElementFactory.CreateInstance instance3 = <>c.<>9__0_2;
            if (<>c.<>9__0_2 == null)
            {
                LayoutElementFactory.CreateInstance local3 = <>c.<>9__0_2;
                instance3 = <>c.<>9__0_2 = (LayoutElementFactory.CreateInstance) ((element, view) => new TreeItemElement(element, view));
            }
            base.Initializers[typeof(TreeItem)] = instance3;
            LayoutElementFactory.CreateInstance instance4 = <>c.<>9__0_3;
            if (<>c.<>9__0_3 == null)
            {
                LayoutElementFactory.CreateInstance local4 = <>c.<>9__0_3;
                instance4 = <>c.<>9__0_3 = (LayoutElementFactory.CreateInstance) ((element, view) => new HiddenItemsListElement(element, view));
            }
            base.Initializers[typeof(HiddenItemsPanel)] = instance4;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomizationViewElementFactory.<>c <>9 = new CustomizationViewElementFactory.<>c();
            public static LayoutElementFactory.CreateInstance <>9__0_0;
            public static LayoutElementFactory.CreateInstance <>9__0_1;
            public static LayoutElementFactory.CreateInstance <>9__0_2;
            public static LayoutElementFactory.CreateInstance <>9__0_3;

            internal ILayoutElement <InitializeFactory>b__0_0(UIElement element, UIElement view) => 
                new CustomizationControlElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_1(UIElement element, UIElement view) => 
                new HiddenItemElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_2(UIElement element, UIElement view) => 
                new TreeItemElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_3(UIElement element, UIElement view) => 
                new HiddenItemsListElement(element, view);
        }
    }
}

