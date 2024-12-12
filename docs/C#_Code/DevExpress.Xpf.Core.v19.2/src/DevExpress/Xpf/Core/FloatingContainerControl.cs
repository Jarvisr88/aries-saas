namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Content")]
    public class FloatingContainerControl : Control
    {
        private static Type ownerType = typeof(FloatingContainerControl);
        public static readonly DependencyProperty FloatingModeProperty = DependencyProperty.Register("FloatingMode", typeof(DevExpress.Xpf.Core.FloatingMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Core.FloatingMode.Adorner, new PropertyChangedCallback(FloatingContainerControl.OnFloatingModeChanged)));
        public static readonly DependencyProperty AdornerTemplateProperty = DependencyProperty.Register("AdornerTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(FloatingContainerControl.OnTemplateChanged)));
        public static readonly DependencyProperty WindowTemplateProperty = DependencyProperty.Register("WindowTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(FloatingContainerControl.OnTemplateChanged)));
        public static readonly DependencyProperty ContentOffsetProperty = DependencyProperty.RegisterAttached("ContentOffset", typeof(Thickness), ownerType, new FrameworkPropertyMetadata(new Thickness(-1.0), FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty OwnerProperty = BaseFloatingContainer.OwnerProperty.AddOwner(ownerType);
        public static readonly DependencyProperty FloatLocationProperty = DependencyProperty.Register("FloatLocation", typeof(Point), ownerType);
        public static readonly DependencyProperty FloatSizeProperty = DependencyProperty.Register("FloatSize", typeof(Size), ownerType);
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), ownerType);
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), ownerType);

        static FloatingContainerControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
        }

        public static Thickness GetContentOffset(DependencyObject obj) => 
            (Thickness) obj.GetValue(ContentOffsetProperty);

        private static void OnFloatingModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((FloatingContainerControl) sender).UpdateTemplate();
        }

        private static void OnTemplateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((FloatingContainerControl) sender).UpdateTemplate();
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            this.UpdateTemplate();
        }

        public static void SetContentOffset(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ContentOffsetProperty, value);
        }

        protected void UpdateTemplate()
        {
            this.Owner ??= ((FrameworkElement) VisualTreeHelper.GetParent(this));
            if (this.Owner == null)
            {
                base.Template = null;
            }
            else
            {
                DevExpress.Xpf.Core.FloatingMode actualFloatingMode = this.ActualFloatingMode;
                if (actualFloatingMode == DevExpress.Xpf.Core.FloatingMode.Adorner)
                {
                    base.Template = this.AdornerTemplate;
                }
                else if (actualFloatingMode == DevExpress.Xpf.Core.FloatingMode.Window)
                {
                    base.Template = this.WindowTemplate;
                }
            }
        }

        public FrameworkElement Owner
        {
            get => 
                (FrameworkElement) base.GetValue(OwnerProperty);
            set => 
                base.SetValue(OwnerProperty, value);
        }

        public Point FloatLocation
        {
            get => 
                (Point) base.GetValue(FloatLocationProperty);
            set => 
                base.SetValue(FloatLocationProperty, value);
        }

        public Size FloatSize
        {
            get => 
                (Size) base.GetValue(FloatSizeProperty);
            set => 
                base.SetValue(FloatSizeProperty, value);
        }

        public bool IsOpen
        {
            get => 
                (bool) base.GetValue(IsOpenProperty);
            set => 
                base.SetValue(IsOpenProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public DevExpress.Xpf.Core.FloatingMode FloatingMode
        {
            get => 
                (DevExpress.Xpf.Core.FloatingMode) base.GetValue(FloatingModeProperty);
            set => 
                base.SetValue(FloatingModeProperty, value);
        }

        public ControlTemplate AdornerTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(AdornerTemplateProperty);
            set => 
                base.SetValue(AdornerTemplateProperty, value);
        }

        public ControlTemplate WindowTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(WindowTemplateProperty);
            set => 
                base.SetValue(WindowTemplateProperty, value);
        }

        public DevExpress.Xpf.Core.FloatingMode ActualFloatingMode =>
            !BrowserInteropHelper.IsBrowserHosted ? this.FloatingMode : DevExpress.Xpf.Core.FloatingMode.Adorner;
    }
}

