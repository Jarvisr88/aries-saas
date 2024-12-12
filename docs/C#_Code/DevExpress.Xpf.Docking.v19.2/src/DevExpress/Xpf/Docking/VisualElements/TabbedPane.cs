namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_Content", Type=typeof(TabbedPaneContentPresenter))]
    public class TabbedPane : psvContentControl
    {
        public static readonly DependencyProperty TabbedTemplateProperty;

        static TabbedPane()
        {
            DependencyPropertyRegistrator<TabbedPane> registrator = new DependencyPropertyRegistrator<TabbedPane>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("TabbedTemplate", ref TabbedTemplateProperty, null, null, null);
        }

        public IUIElement GetRootUIScope() => 
            (base.LayoutItem != null) ? base.LayoutItem.GetRootUIScope() : null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartContent != null) && !LayoutItemsHelper.IsTemplateChild<TabbedPane>(this.PartContent, this))
            {
                this.PartContent.Dispose();
            }
            this.PartContent = base.GetTemplateChild("PART_Content") as TabbedPaneContentPresenter;
            if (this.PartContent != null)
            {
                this.PartContent.EnsureOwner(this);
            }
        }

        protected override void OnDispose()
        {
            base.ClearValue(TabbedTemplateProperty);
            if (this.PartContent != null)
            {
                this.PartContent.Dispose();
                this.PartContent = null;
            }
            base.OnDispose();
        }

        public DataTemplate TabbedTemplate
        {
            get => 
                (DataTemplate) base.GetValue(TabbedTemplateProperty);
            set => 
                base.SetValue(TabbedTemplateProperty, value);
        }

        public TabbedPaneContentPresenter PartContent { get; private set; }
    }
}

