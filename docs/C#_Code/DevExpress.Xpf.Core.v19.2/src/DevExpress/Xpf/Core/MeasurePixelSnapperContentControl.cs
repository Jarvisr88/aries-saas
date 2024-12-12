namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class MeasurePixelSnapperContentControl : ContentControl
    {
        public static readonly DependencyProperty SnapperTypeProperty = DependencyPropertyManager.Register("SnapperType", typeof(DevExpress.Xpf.Core.SnapperType), typeof(MeasurePixelSnapperContentControl), new FrameworkPropertyMetadata(DevExpress.Xpf.Core.SnapperType.Ceil, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty TranslateContentOnRenderProperty = DependencyPropertyManager.Register("TranslateContentOnRender", typeof(bool), typeof(MeasurePixelSnapperContentControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));
        private const string DefaultTemplateXAML = "<ControlTemplate TargetType='local:MeasurePixelSnapperContentControl' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:local='clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2'><local:MeasurePixelSnapperContentPresenter Content='{TemplateBinding Content}' ContentTemplate='{TemplateBinding ContentTemplate}' Cursor='{TemplateBinding Cursor}' Margin='{TemplateBinding Padding}' HorizontalAlignment='{TemplateBinding HorizontalContentAlignment}' VerticalAlignment='{TemplateBinding VerticalContentAlignment}'/></ControlTemplate>";
        private static ControlTemplate defaultTemplate;
        private FrameworkElement topLevelVisual;

        public MeasurePixelSnapperContentControl()
        {
            base.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
            base.Template = DefaultTemplate;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.UpdateChildDataContext();
            return base.ArrangeOverride(arrangeBounds);
        }

        private DependencyObject GetChild()
        {
            DependencyObject obj2 = (VisualTreeHelper.GetChildrenCount(this) > 0) ? VisualTreeHelper.GetChild(this, 0) : null;
            MeasurePixelSnapperContentPresenter presenter = obj2 as MeasurePixelSnapperContentPresenter;
            if (presenter != null)
            {
                presenter.Snapper = this;
            }
            return obj2;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.UpdateChildDataContext();
            return MeasurePixelSnapperHelper.MeasureOverride(base.MeasureOverride(constraint), this.SnapperType);
        }

        public override void OnApplyTemplate()
        {
            this.UpdateChildDataContext();
            base.OnApplyTemplate();
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            FrameworkElement element = newContent as FrameworkElement;
            FrameworkContentElement element2 = newContent as FrameworkContentElement;
            if (((element != null) && (element.Parent != null)) || ((element2 != null) && (element2.Parent != null)))
            {
                base.RemoveLogicalChild(oldContent);
            }
            else
            {
                base.OnContentChanged(oldContent, newContent);
            }
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateChildDataContext();
        }

        private void UpdateChildDataContext()
        {
            ContentPresenter child = this.GetChild() as ContentPresenter;
            if ((child != null) && (base.ContentTemplate != null))
            {
                child.DataContext = base.DataContext;
            }
        }

        private static ControlTemplate DefaultTemplate
        {
            get
            {
                defaultTemplate ??= ((ControlTemplate) XamlReader.Parse("<ControlTemplate TargetType='local:MeasurePixelSnapperContentControl' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:local='clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2'><local:MeasurePixelSnapperContentPresenter Content='{TemplateBinding Content}' ContentTemplate='{TemplateBinding ContentTemplate}' Cursor='{TemplateBinding Cursor}' Margin='{TemplateBinding Padding}' HorizontalAlignment='{TemplateBinding HorizontalContentAlignment}' VerticalAlignment='{TemplateBinding VerticalContentAlignment}'/></ControlTemplate>"));
                return defaultTemplate;
            }
        }

        public DevExpress.Xpf.Core.SnapperType SnapperType
        {
            get => 
                (DevExpress.Xpf.Core.SnapperType) base.GetValue(SnapperTypeProperty);
            set => 
                base.SetValue(SnapperTypeProperty, value);
        }

        public bool TranslateContentOnRender
        {
            get => 
                (bool) base.GetValue(TranslateContentOnRenderProperty);
            set => 
                base.SetValue(TranslateContentOnRenderProperty, value);
        }

        private FrameworkElement TopLevelVisual
        {
            get
            {
                if ((this.topLevelVisual == null) || !this.topLevelVisual.IsAncestorOf(this))
                {
                    this.topLevelVisual = LayoutHelper.GetTopLevelVisual(this);
                }
                return this.topLevelVisual;
            }
        }
    }
}

