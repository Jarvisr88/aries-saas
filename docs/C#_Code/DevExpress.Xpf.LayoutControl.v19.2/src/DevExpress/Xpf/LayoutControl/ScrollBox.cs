namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Utils;
    using DevExpress.Xpf;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Windows;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free), ToolboxTabName("DX.19.2: Navigation & Layout")]
    public class ScrollBox : LayoutControlBase
    {
        public static readonly DependencyProperty LeftProperty = DependencyProperty.RegisterAttached("Left", typeof(double), typeof(ScrollBox), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
        public static readonly DependencyProperty TopProperty = DependencyProperty.RegisterAttached("Top", typeof(double), typeof(ScrollBox), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));

        public ScrollBox()
        {
            About.CheckLicenseShowNagScreen(typeof(ScrollBox));
        }

        protected override LayoutProviderBase CreateLayoutProvider() => 
            new ScrollBoxLayoutProvider(this);

        public static double GetLeft(UIElement element) => 
            (double) element.GetValue(LeftProperty);

        public static double GetTop(UIElement element) => 
            (double) element.GetValue(TopProperty);

        protected override void OnAttachedPropertyChanged(FrameworkElement child, DependencyProperty property, object oldValue, object newValue)
        {
            base.OnAttachedPropertyChanged(child, property, oldValue, newValue);
            if (ReferenceEquals(property, LeftProperty) || ReferenceEquals(property, TopProperty))
            {
                base.Changed();
            }
        }

        public static void SetLeft(UIElement element, double value)
        {
            element.SetValue(LeftProperty, value);
        }

        public static void SetTop(UIElement element, double value)
        {
            element.SetValue(TopProperty, value);
        }
    }
}

