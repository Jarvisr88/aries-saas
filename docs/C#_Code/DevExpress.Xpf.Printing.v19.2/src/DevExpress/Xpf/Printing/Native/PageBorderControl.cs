namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class PageBorderControl : ContentControl
    {
        public static readonly DependencyProperty IsPageShadowVisibleProperty = DependencyPropertyManager.Register("IsPageShadowVisible", typeof(bool), typeof(PageBorderControl), new PropertyMetadata(true));

        static PageBorderControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PageBorderControl), new FrameworkPropertyMetadata(typeof(PageBorderControl)));
        }

        public PageBorderControl()
        {
            base.Focusable = false;
        }

        public bool IsPageShadowVisible
        {
            get => 
                (bool) base.GetValue(IsPageShadowVisibleProperty);
            set => 
                base.SetValue(IsPageShadowVisibleProperty, value);
        }
    }
}

