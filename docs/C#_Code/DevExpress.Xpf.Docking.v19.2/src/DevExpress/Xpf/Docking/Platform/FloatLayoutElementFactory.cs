namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class FloatLayoutElementFactory : LayoutElementFactory
    {
        protected override bool CanUseCustomInitializer(UIElement uiElement)
        {
            DocumentPanel panel = uiElement as DocumentPanel;
            return (((panel == null) || !panel.IsFloatingRootItem) ? base.CanUseCustomInitializer(uiElement) : true);
        }

        protected override void InitializeFactory()
        {
            base.InitializeFactory();
            LayoutElementFactory.CreateInstance instance1 = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                LayoutElementFactory.CreateInstance local1 = <>c.<>9__0_0;
                instance1 = <>c.<>9__0_0 = (LayoutElementFactory.CreateInstance) ((element, view) => new FloatPanePresenterElement(element, view));
            }
            base.Initializers[typeof(FloatingAdornerPresenter)] = instance1;
            LayoutElementFactory.CreateInstance instance2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                LayoutElementFactory.CreateInstance local2 = <>c.<>9__0_1;
                instance2 = <>c.<>9__0_1 = (LayoutElementFactory.CreateInstance) ((element, view) => new FloatPanePresenterElement(element, view));
            }
            base.Initializers[typeof(FloatingWindowPresenter)] = instance2;
            LayoutElementFactory.CreateInstance instance3 = <>c.<>9__0_2;
            if (<>c.<>9__0_2 == null)
            {
                LayoutElementFactory.CreateInstance local3 = <>c.<>9__0_2;
                instance3 = <>c.<>9__0_2 = (LayoutElementFactory.CreateInstance) ((element, view) => new EmptyLayoutContainer());
            }
            base.Initializers[typeof(FloatGroup)] = instance3;
        }

        protected override bool TryGetCustomInitializer(UIElement uiElement, out LayoutElementFactory.CreateInstance createInstance)
        {
            DocumentPanel panel = uiElement as DocumentPanel;
            if ((panel == null) || !panel.IsFloatingRootItem)
            {
                base.TryGetCustomInitializer(uiElement, out createInstance);
            }
            else
            {
                LayoutElementFactory.CreateInstance instance1 = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    LayoutElementFactory.CreateInstance local1 = <>c.<>9__2_0;
                    instance1 = <>c.<>9__2_0 = (LayoutElementFactory.CreateInstance) ((element, view) => new FloatDocumentElement(element, view));
                }
                createInstance = instance1;
            }
            return (createInstance != null);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatLayoutElementFactory.<>c <>9 = new FloatLayoutElementFactory.<>c();
            public static LayoutElementFactory.CreateInstance <>9__0_0;
            public static LayoutElementFactory.CreateInstance <>9__0_1;
            public static LayoutElementFactory.CreateInstance <>9__0_2;
            public static LayoutElementFactory.CreateInstance <>9__2_0;

            internal ILayoutElement <InitializeFactory>b__0_0(UIElement element, UIElement view) => 
                new FloatPanePresenterElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_1(UIElement element, UIElement view) => 
                new FloatPanePresenterElement(element, view);

            internal ILayoutElement <InitializeFactory>b__0_2(UIElement element, UIElement view) => 
                new EmptyLayoutContainer();

            internal ILayoutElement <TryGetCustomInitializer>b__2_0(UIElement element, UIElement view) => 
                new FloatDocumentElement(element, view);
        }
    }
}

