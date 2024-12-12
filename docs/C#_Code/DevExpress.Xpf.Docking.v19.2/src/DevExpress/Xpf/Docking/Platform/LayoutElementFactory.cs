namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Base;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class LayoutElementFactory : UILayoutElementFactory
    {
        private IDictionary<Type, CreateInstance> initializers = new Dictionary<Type, CreateInstance>();
        private IDictionary<Type, CreateInstance> customInitializers = new Dictionary<Type, CreateInstance>();

        public LayoutElementFactory()
        {
            this.InitializeFactory();
        }

        protected virtual bool CanUseCustomInitializer(UIElement uiElement)
        {
            LayoutGroup group = uiElement as LayoutGroup;
            if (group != null)
            {
                return ((group.ItemType == LayoutItemType.Group) && group.IsTabHost);
            }
            DocumentPanel panel = uiElement as DocumentPanel;
            return ((panel != null) && panel.IsMDIChild);
        }

        protected sealed override ILayoutElement CreateElement(IUIElement uiKey)
        {
            UIElement uiElement = uiKey as UIElement;
            return this.Resolve(uiElement, uiKey.GetRootUIScope() as UIElement);
        }

        private Type GetInitializerType(IDictionary<Type, CreateInstance> initializers, Type elementType)
        {
            Type key = elementType;
            while ((key != null) && !initializers.ContainsKey(key))
            {
                key = key.BaseType;
            }
            return key;
        }

        protected sealed override IEnumerator<IUIElement> GetUIEnumerator(IUIElement rootKey) => 
            new IUIElementEnumerator(rootKey);

        protected virtual void InitializeFactory()
        {
            CreateInstance instance1 = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                CreateInstance local1 = <>c.<>9__8_0;
                instance1 = <>c.<>9__8_0 = (CreateInstance) ((element, view) => new DockPaneElement(element, view));
            }
            this.Initializers[typeof(LayoutPanel)] = instance1;
            CreateInstance instance2 = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                CreateInstance local2 = <>c.<>9__8_1;
                instance2 = <>c.<>9__8_1 = (CreateInstance) ((element, view) => new DocumentElement(element, view));
            }
            this.Initializers[typeof(DocumentPanel)] = instance2;
            CreateInstance instance3 = <>c.<>9__8_2;
            if (<>c.<>9__8_2 == null)
            {
                CreateInstance local3 = <>c.<>9__8_2;
                instance3 = <>c.<>9__8_2 = (CreateInstance) ((element, view) => new GroupPaneElement(element, view));
            }
            this.Initializers[typeof(LayoutGroup)] = instance3;
            CreateInstance instance4 = <>c.<>9__8_3;
            if (<>c.<>9__8_3 == null)
            {
                CreateInstance local4 = <>c.<>9__8_3;
                instance4 = <>c.<>9__8_3 = (CreateInstance) ((element, view) => new TabbedPaneElement(element, view));
            }
            this.Initializers[typeof(TabbedGroup)] = instance4;
            CreateInstance instance5 = <>c.<>9__8_4;
            if (<>c.<>9__8_4 == null)
            {
                CreateInstance local5 = <>c.<>9__8_4;
                instance5 = <>c.<>9__8_4 = (CreateInstance) ((element, view) => new TabbedPaneItemElement(element, view));
            }
            this.Initializers[typeof(TabbedPaneItem)] = instance5;
            CreateInstance instance6 = <>c.<>9__8_5;
            if (<>c.<>9__8_5 == null)
            {
                CreateInstance local6 = <>c.<>9__8_5;
                instance6 = <>c.<>9__8_5 = (CreateInstance) ((element, view) => new DocumentPaneElement(element, view));
            }
            this.Initializers[typeof(DocumentGroup)] = instance6;
            CreateInstance instance7 = <>c.<>9__8_6;
            if (<>c.<>9__8_6 == null)
            {
                CreateInstance local7 = <>c.<>9__8_6;
                instance7 = <>c.<>9__8_6 = (CreateInstance) ((element, view) => new DocumentPaneItemElement(element, view));
            }
            this.Initializers[typeof(DocumentPaneItem)] = instance7;
            CreateInstance instance8 = <>c.<>9__8_7;
            if (<>c.<>9__8_7 == null)
            {
                CreateInstance local8 = <>c.<>9__8_7;
                instance8 = <>c.<>9__8_7 = (CreateInstance) ((element, view) => new SplitterElement(element, view));
            }
            this.Initializers[typeof(Splitter)] = instance8;
            CreateInstance instance9 = <>c.<>9__8_8;
            if (<>c.<>9__8_8 == null)
            {
                CreateInstance local9 = <>c.<>9__8_8;
                instance9 = <>c.<>9__8_8 = (CreateInstance) ((element, view) => new TabbedLayoutGroupHeaderElement(element, view));
            }
            this.Initializers[typeof(TabbedLayoutGroupItem)] = instance9;
            CreateInstance instance10 = <>c.<>9__8_9;
            if (<>c.<>9__8_9 == null)
            {
                CreateInstance local10 = <>c.<>9__8_9;
                instance10 = <>c.<>9__8_9 = (CreateInstance) ((element, view) => new ControlItemElement(element, view));
            }
            this.Initializers[typeof(LayoutControlItem)] = instance10;
            CreateInstance instance11 = <>c.<>9__8_10;
            if (<>c.<>9__8_10 == null)
            {
                CreateInstance local11 = <>c.<>9__8_10;
                instance11 = <>c.<>9__8_10 = (CreateInstance) ((element, view) => new LayoutSplitterElement(element, view));
            }
            this.Initializers[typeof(LayoutSplitter)] = instance11;
            CreateInstance instance12 = <>c.<>9__8_11;
            if (<>c.<>9__8_11 == null)
            {
                CreateInstance local12 = <>c.<>9__8_11;
                instance12 = <>c.<>9__8_11 = (CreateInstance) ((element, view) => new FixedItemElement(element, view));
            }
            this.Initializers[typeof(EmptySpaceItem)] = instance12;
            CreateInstance instance13 = <>c.<>9__8_12;
            if (<>c.<>9__8_12 == null)
            {
                CreateInstance local13 = <>c.<>9__8_12;
                instance13 = <>c.<>9__8_12 = (CreateInstance) ((element, view) => new FixedItemElement(element, view));
            }
            this.Initializers[typeof(LabelItem)] = instance13;
            CreateInstance instance14 = <>c.<>9__8_13;
            if (<>c.<>9__8_13 == null)
            {
                CreateInstance local14 = <>c.<>9__8_13;
                instance14 = <>c.<>9__8_13 = (CreateInstance) ((element, view) => new FixedItemElement(element, view));
            }
            this.Initializers[typeof(SeparatorItem)] = instance14;
            CreateInstance instance15 = <>c.<>9__8_14;
            if (<>c.<>9__8_14 == null)
            {
                CreateInstance local15 = <>c.<>9__8_14;
                instance15 = <>c.<>9__8_14 = (CreateInstance) ((element, view) => new TabbedLayoutGroupElement(element, view));
            }
            this.CustomInitializers[typeof(LayoutGroup)] = instance15;
            CreateInstance instance16 = <>c.<>9__8_15;
            if (<>c.<>9__8_15 == null)
            {
                CreateInstance local16 = <>c.<>9__8_15;
                instance16 = <>c.<>9__8_15 = (CreateInstance) ((element, view) => new MDIDocumentElement(element, view));
            }
            this.CustomInitializers[typeof(DocumentPanel)] = instance16;
        }

        private ILayoutElement Resolve(UIElement uiElement, UIElement view)
        {
            CreateInstance createInstance = null;
            Type elementType = uiElement.GetType();
            if ((!this.CanUseCustomInitializer(uiElement) || !this.TryGetCustomInitializer(uiElement, out createInstance)) && !this.initializers.TryGetValue(this.GetInitializerType(this.initializers, elementType), out createInstance))
            {
                throw new AssertionException($"Could not resolve element for {elementType}.");
            }
            return createInstance(uiElement, view);
        }

        protected virtual bool TryGetCustomInitializer(UIElement uiElement, out CreateInstance createInstance)
        {
            Type elementType = uiElement.GetType();
            return this.CustomInitializers.TryGetValue(this.GetInitializerType(this.customInitializers, elementType), out createInstance);
        }

        protected IDictionary<Type, CreateInstance> Initializers =>
            this.initializers;

        protected IDictionary<Type, CreateInstance> CustomInitializers =>
            this.customInitializers;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutElementFactory.<>c <>9 = new LayoutElementFactory.<>c();
            public static LayoutElementFactory.CreateInstance <>9__8_0;
            public static LayoutElementFactory.CreateInstance <>9__8_1;
            public static LayoutElementFactory.CreateInstance <>9__8_2;
            public static LayoutElementFactory.CreateInstance <>9__8_3;
            public static LayoutElementFactory.CreateInstance <>9__8_4;
            public static LayoutElementFactory.CreateInstance <>9__8_5;
            public static LayoutElementFactory.CreateInstance <>9__8_6;
            public static LayoutElementFactory.CreateInstance <>9__8_7;
            public static LayoutElementFactory.CreateInstance <>9__8_8;
            public static LayoutElementFactory.CreateInstance <>9__8_9;
            public static LayoutElementFactory.CreateInstance <>9__8_10;
            public static LayoutElementFactory.CreateInstance <>9__8_11;
            public static LayoutElementFactory.CreateInstance <>9__8_12;
            public static LayoutElementFactory.CreateInstance <>9__8_13;
            public static LayoutElementFactory.CreateInstance <>9__8_14;
            public static LayoutElementFactory.CreateInstance <>9__8_15;

            internal ILayoutElement <InitializeFactory>b__8_0(UIElement element, UIElement view) => 
                new DockPaneElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_1(UIElement element, UIElement view) => 
                new DocumentElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_10(UIElement element, UIElement view) => 
                new LayoutSplitterElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_11(UIElement element, UIElement view) => 
                new FixedItemElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_12(UIElement element, UIElement view) => 
                new FixedItemElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_13(UIElement element, UIElement view) => 
                new FixedItemElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_14(UIElement element, UIElement view) => 
                new TabbedLayoutGroupElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_15(UIElement element, UIElement view) => 
                new MDIDocumentElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_2(UIElement element, UIElement view) => 
                new GroupPaneElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_3(UIElement element, UIElement view) => 
                new TabbedPaneElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_4(UIElement element, UIElement view) => 
                new TabbedPaneItemElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_5(UIElement element, UIElement view) => 
                new DocumentPaneElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_6(UIElement element, UIElement view) => 
                new DocumentPaneItemElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_7(UIElement element, UIElement view) => 
                new SplitterElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_8(UIElement element, UIElement view) => 
                new TabbedLayoutGroupHeaderElement(element, view);

            internal ILayoutElement <InitializeFactory>b__8_9(UIElement element, UIElement view) => 
                new ControlItemElement(element, view);
        }

        protected delegate ILayoutElement CreateInstance(UIElement uiElement, UIElement view);
    }
}

