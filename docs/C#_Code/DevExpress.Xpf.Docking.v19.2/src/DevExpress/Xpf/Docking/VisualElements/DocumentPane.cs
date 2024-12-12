namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_Content", Type=typeof(DocumentPaneContentPresenter))]
    public class DocumentPane : psvContentControl
    {
        public static readonly DependencyProperty TabbedTemplateProperty;
        public static readonly DependencyProperty MDITemplateProperty;

        static DocumentPane()
        {
            DependencyPropertyRegistrator<DocumentPane> registrator = new DependencyPropertyRegistrator<DocumentPane>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("TabbedTemplate", ref TabbedTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("MDITemplate", ref MDITemplateProperty, null, null, null);
        }

        public IUIElement GetRootUIScope() => 
            (base.LayoutItem != null) ? base.LayoutItem.GetRootUIScope() : null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartContent != null) && !LayoutItemsHelper.IsTemplateChild<DocumentPane>(this.PartContent, this))
            {
                this.PartContent.Dispose();
            }
            this.PartContent = base.GetTemplateChild("PART_Content") as DocumentPaneContentPresenter;
            if (this.PartContent != null)
            {
                this.PartContent.EnsureOwner(this);
            }
        }

        protected override void OnDispose()
        {
            base.ClearValue(TabbedTemplateProperty);
            base.ClearValue(MDITemplateProperty);
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

        public DataTemplate MDITemplate
        {
            get => 
                (DataTemplate) base.GetValue(MDITemplateProperty);
            set => 
                base.SetValue(MDITemplateProperty, value);
        }

        public DocumentPaneContentPresenter PartContent { get; private set; }
    }
}

