namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class HeaderItemControl : Button
    {
        public static readonly DependencyProperty ShowBorderProperty = DependencyProperty.Register("ShowBorder", typeof(bool), typeof(HeaderItemControl), new PropertyMetadata(true));
        public static readonly DependencyProperty TemplateProviderProperty = DependencyProperty.Register("TemplateProvider", typeof(HeaderItemControlTemplateProvider), typeof(HeaderItemControl), new PropertyMetadata(null));
        public static readonly DependencyProperty CloseTemplateProviderProperty = DependencyProperty.RegisterAttached("CloseTemplateProvider", typeof(CloseHeaderItemControlTemplateProvider), typeof(HeaderItemControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, null, new CoerceValueCallback(HeaderItemControl.CoerceCloseTemplateProvider)));
        public static readonly DependencyProperty CommonTemplateProviderProperty = DependencyProperty.RegisterAttached("CommonTemplateProvider", typeof(CommonHeaderItemControlTemplateProvider), typeof(HeaderItemControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, null, new CoerceValueCallback(HeaderItemControl.CoerceCommonTemplateProvider)));
        public static readonly DependencyProperty ExtendedCloseTemplateProviderProperty = DependencyProperty.RegisterAttached("ExtendedCloseTemplateProvider", typeof(CloseHeaderItemControlTemplateProvider), typeof(HeaderItemControl), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(HeaderItemControl.OnExtendedProviderPropertiesChanged)));
        public static readonly DependencyProperty ExtendedCommonTemplateProviderProperty = DependencyProperty.RegisterAttached("ExtendedCommonTemplateProvider", typeof(CommonHeaderItemControlTemplateProvider), typeof(HeaderItemControl), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(HeaderItemControl.OnExtendedProviderPropertiesChanged)));
        public static readonly DependencyProperty UseExtendedTemplateProvidersProperty = DependencyProperty.RegisterAttached("UseExtendedTemplateProviders", typeof(bool), typeof(HeaderItemControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(HeaderItemControl.OnExtendedProviderPropertiesChanged)));

        static HeaderItemControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderItemControl), new FrameworkPropertyMetadata(typeof(HeaderItemControl)));
        }

        private static object CoerceCloseTemplateProvider(DependencyObject d, object baseValue) => 
            !GetUseExtendedTemplateProviders(d) ? baseValue : (GetExtendedCloseTemplateProvider(d) ?? baseValue);

        private static object CoerceCommonTemplateProvider(DependencyObject d, object baseValue) => 
            !GetUseExtendedTemplateProviders(d) ? baseValue : (GetExtendedCommonTemplateProvider(d) ?? baseValue);

        public static CloseHeaderItemControlTemplateProvider GetCloseTemplateProvider(DependencyObject obj) => 
            (CloseHeaderItemControlTemplateProvider) obj.GetValue(CloseTemplateProviderProperty);

        public static CommonHeaderItemControlTemplateProvider GetCommonTemplateProvider(DependencyObject obj) => 
            (CommonHeaderItemControlTemplateProvider) obj.GetValue(CommonTemplateProviderProperty);

        public static CloseHeaderItemControlTemplateProvider GetExtendedCloseTemplateProvider(DependencyObject obj) => 
            (CloseHeaderItemControlTemplateProvider) obj.GetValue(ExtendedCloseTemplateProviderProperty);

        public static CommonHeaderItemControlTemplateProvider GetExtendedCommonTemplateProvider(DependencyObject obj) => 
            (CommonHeaderItemControlTemplateProvider) obj.GetValue(ExtendedCommonTemplateProviderProperty);

        public static bool GetUseExtendedTemplateProviders(DependencyObject obj) => 
            (bool) obj.GetValue(UseExtendedTemplateProvidersProperty);

        protected override void OnClick()
        {
            if (this.ShowBorder)
            {
                base.OnClick();
            }
        }

        private static void OnExtendedProviderPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(CloseTemplateProviderProperty);
            d.CoerceValue(CommonTemplateProviderProperty);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnKeyUp(e);
            }
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnLostKeyboardFocus(e);
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnLostMouseCapture(e);
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnMouseLeave(e);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnMouseLeftButtonDown(e);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnMouseLeftButtonUp(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.ShowBorder)
            {
                base.OnMouseMove(e);
            }
        }

        public static void SetCloseTemplateProvider(DependencyObject obj, CloseHeaderItemControlTemplateProvider value)
        {
            obj.SetValue(CloseTemplateProviderProperty, value);
        }

        public static void SetCommonTemplateProvider(DependencyObject obj, CommonHeaderItemControlTemplateProvider value)
        {
            obj.SetValue(CommonTemplateProviderProperty, value);
        }

        public static void SetExtendedCloseTemplateProvider(DependencyObject obj, CloseHeaderItemControlTemplateProvider value)
        {
            obj.SetValue(ExtendedCloseTemplateProviderProperty, value);
        }

        public static void SetExtendedCommonTemplateProvider(DependencyObject obj, CommonHeaderItemControlTemplateProvider value)
        {
            obj.SetValue(ExtendedCommonTemplateProviderProperty, value);
        }

        public static void SetUseExtendedTemplateProviders(DependencyObject obj, bool value)
        {
            obj.SetValue(UseExtendedTemplateProvidersProperty, value);
        }

        public bool ShowBorder
        {
            get => 
                (bool) base.GetValue(ShowBorderProperty);
            set => 
                base.SetValue(ShowBorderProperty, value);
        }

        public HeaderItemControlTemplateProvider TemplateProvider
        {
            get => 
                (HeaderItemControlTemplateProvider) base.GetValue(TemplateProviderProperty);
            set => 
                base.SetValue(TemplateProviderProperty, value);
        }
    }
}

