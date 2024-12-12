namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LayoutItemsControl : psvItemsControl
    {
        public static readonly DependencyProperty OrientationProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty LastChildFillProperty;

        static LayoutItemsControl()
        {
            DependencyPropertyRegistrator<LayoutItemsControl> registrator = new DependencyPropertyRegistrator<LayoutItemsControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<System.Windows.Controls.Orientation>("Orientation", ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, null, null);
            registrator.Register<bool>("LastChildFill", ref LastChildFillProperty, true, null, null);
        }

        protected override void ClearContainer(DependencyObject element)
        {
            MultiTemplateControl control = element as MultiTemplateControl;
            if (control != null)
            {
                control.LayoutItem = null;
                control.Dispose();
            }
            Action<Splitter> action = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Action<Splitter> local1 = <>c.<>9__8_0;
                action = <>c.<>9__8_0 = x => x.Deactivate();
            }
            (element as Splitter).Do<Splitter>(action);
            Action<BaseLayoutItem> action2 = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                Action<BaseLayoutItem> local2 = <>c.<>9__8_1;
                action2 = <>c.<>9__8_1 = x => x.ClearTemplate();
            }
            (element as BaseLayoutItem).Do<BaseLayoutItem>(action2);
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            if (this.PartGroupPanel != null)
            {
                this.PartGroupPanel.InvalidateLayout();
            }
        }

        protected override void EnsureItemsPanelCore(Panel itemsPanel)
        {
            base.EnsureItemsPanelCore(itemsPanel);
            this.PartGroupPanel = itemsPanel as GroupPanel;
        }

        protected void EnsureVisualTreeAffinity(UIElement element)
        {
            GroupPanel visualParent = LayoutItemsHelper.GetVisualParent(element) as GroupPanel;
            if (visualParent != null)
            {
                LayoutItemsControl templateParent = LayoutItemsHelper.GetTemplateParent<LayoutItemsControl>(visualParent);
                if ((templateParent != null) && !ReferenceEquals(templateParent, this))
                {
                    visualParent.Children.Remove(element);
                }
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            (item is Splitter) || (item is BaseLayoutItem);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.OwnerPresenter = LayoutItemsHelper.GetTemplateParent<GroupPaneContentPresenter>(this);
            if (this.OwnerPresenter != null)
            {
                psvContentControlBase.EnsureContentElement<GroupPane>(this, this.OwnerPresenter);
            }
        }

        protected override void OnDispose()
        {
            this.OwnerPresenter = null;
            if (this.PartGroupPanel != null)
            {
                this.PartGroupPanel.Dispose();
                this.PartGroupPanel = null;
            }
            base.OnDispose();
        }

        protected override void PrepareContainer(DependencyObject element, object item)
        {
            MultiTemplateControl control = element as MultiTemplateControl;
            if (control != null)
            {
                control.BeginPrepareContainer();
                control.LayoutItem = (BaseLayoutItem) item;
                control.EndPrepareContainer();
            }
            Action<Splitter> action = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Action<Splitter> local1 = <>c.<>9__7_0;
                action = <>c.<>9__7_0 = x => x.Activate();
            }
            (element as Splitter).Do<Splitter>(action);
            Action<BaseLayoutItem> action2 = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Action<BaseLayoutItem> local2 = <>c.<>9__7_1;
                action2 = <>c.<>9__7_1 = x => x.SelectTemplate();
            }
            (element as BaseLayoutItem).Do<BaseLayoutItem>(action2);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (this.PartGroupPanel != null)
            {
                this.PartGroupPanel.InvalidateLayout();
            }
        }

        internal virtual void PrepareForWindowClosing()
        {
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        protected internal GroupPaneContentPresenter OwnerPresenter { get; set; }

        protected internal GroupPanel PartGroupPanel { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemsControl.<>c <>9 = new LayoutItemsControl.<>c();
            public static Action<Splitter> <>9__7_0;
            public static Action<BaseLayoutItem> <>9__7_1;
            public static Action<Splitter> <>9__8_0;
            public static Action<BaseLayoutItem> <>9__8_1;

            internal void <ClearContainer>b__8_0(Splitter x)
            {
                x.Deactivate();
            }

            internal void <ClearContainer>b__8_1(BaseLayoutItem x)
            {
                x.ClearTemplate();
            }

            internal void <PrepareContainer>b__7_0(Splitter x)
            {
                x.Activate();
            }

            internal void <PrepareContainer>b__7_1(BaseLayoutItem x)
            {
                x.SelectTemplate();
            }
        }
    }
}

