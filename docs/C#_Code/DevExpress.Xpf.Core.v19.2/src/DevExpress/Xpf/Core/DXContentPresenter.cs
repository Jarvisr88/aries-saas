namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    [DXToolboxBrowsable(false), ContentProperty("Content")]
    public class DXContentPresenter : Control
    {
        private const string DefaultTemplateXAML = "<ControlTemplate TargetType='local:DXContentPresenter' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:local='clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2'><ContentPresenter Content='{TemplateBinding Content}' ContentTemplate='{TemplateBinding ContentTemplate}'/></ControlTemplate>";
        private static ControlTemplate _DefaultTemplate;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;

        static DXContentPresenter()
        {
            ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(DXContentPresenter), new PropertyMetadata((o, e) => ((DXContentPresenter) o).OnContentChanged(e.OldValue, e.NewValue)));
            ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(DXContentPresenter), null);
        }

        public DXContentPresenter()
        {
            base.Focusable = false;
            base.IsTabStop = false;
            base.Template = DefaultTemplate;
        }

        protected virtual void OnContentChanged(object oldValue, object newValue)
        {
            UIElement reference = newValue as UIElement;
            if (reference != null)
            {
                ContentPresenter parent = VisualTreeHelper.GetParent(reference) as ContentPresenter;
                if (parent != null)
                {
                    parent.Content = null;
                }
            }
        }

        private static ControlTemplate DefaultTemplate
        {
            get
            {
                _DefaultTemplate ??= ((ControlTemplate) XamlReader.Parse("<ControlTemplate TargetType='local:DXContentPresenter' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:local='clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2'><ContentPresenter Content='{TemplateBinding Content}' ContentTemplate='{TemplateBinding ContentTemplate}'/></ControlTemplate>"));
                return _DefaultTemplate;
            }
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXContentPresenter.<>c <>9 = new DXContentPresenter.<>c();

            internal void <.cctor>b__14_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DXContentPresenter) o).OnContentChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

