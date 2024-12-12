namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public static class FrameworkElementHelper
    {
        public static readonly DependencyProperty IsMouseOverOverrideProperty;
        public static readonly DependencyProperty EnableIsMouseOverOverrideProperty;
        public static readonly DependencyProperty ClipCornerRadiusProperty;
        public static readonly DependencyProperty IsClippedProperty;
        public static readonly DependencyProperty IsVisibleProperty;
        private static readonly Action<FrameworkElement, bool> SetBypassLayoutPoliciesHandler;

        static FrameworkElementHelper()
        {
            IsMouseOverOverrideProperty = DependencyProperty.RegisterAttached("IsMouseOverOverride", typeof(bool), typeof(FrameworkElementHelper), new PropertyMetadata((o, e) => OnIsMouseOverOverrideChanged((FrameworkElement) o)));
            EnableIsMouseOverOverrideProperty = DependencyProperty.RegisterAttached("EnableIsMouseOverOverride", typeof(bool), typeof(FrameworkElementHelper), new PropertyMetadata((o, e) => OnEnableIsMouseOverOverrideChanged((FrameworkElement) o, (bool) e.NewValue)));
            ClipCornerRadiusProperty = DependencyProperty.RegisterAttached("ClipCornerRadius", typeof(double), typeof(FrameworkElementHelper), new PropertyMetadata((o, e) => OnClipCornerRadiusChanged((FrameworkElement) o)));
            IsClippedProperty = DependencyProperty.RegisterAttached("IsClipped", typeof(bool), typeof(FrameworkElementHelper), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                FrameworkElement element = o as FrameworkElement;
                if (element != null)
                {
                    if ((bool) e.NewValue)
                    {
                        element.SizeChanged += new SizeChangedEventHandler(FrameworkElementHelper.OnElementSizeChanged);
                    }
                    else
                    {
                        element.SizeChanged -= new SizeChangedEventHandler(FrameworkElementHelper.OnElementSizeChanged);
                    }
                }
            }));
            IsVisibleProperty = DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(FrameworkElementHelper), new PropertyMetadata(true, (o, e) => ((FrameworkElement) o).SetVisible((bool) e.NewValue)));
            int? parametersCount = null;
            SetBypassLayoutPoliciesHandler = ReflectionHelper.CreateInstanceMethodHandler<FrameworkElement, Action<FrameworkElement, bool>>(null, "set_BypassLayoutPolicies", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
        }

        private static void ElementClipCornerRadiusSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement) sender;
            double clipCornerRadius = GetClipCornerRadius(element);
            RectangleGeometry geometry1 = new RectangleGeometry();
            geometry1.Rect = RectHelper.New(new Size(element.ActualWidth, element.ActualHeight));
            geometry1.RadiusX = clipCornerRadius;
            geometry1.RadiusY = clipCornerRadius;
            element.Clip = geometry1;
        }

        public static bool GetAllowDrop(FrameworkElement element) => 
            element.AllowDrop;

        public static double GetClipCornerRadius(FrameworkElement element) => 
            (double) element.GetValue(ClipCornerRadiusProperty);

        public static bool GetEnableIsMouseOverOverride(FrameworkElement element) => 
            (bool) element.GetValue(EnableIsMouseOverOverrideProperty);

        public static bool GetIsClipped(FrameworkElement element) => 
            (bool) element.GetValue(IsClippedProperty);

        public static bool GetIsLoaded(FrameworkContentElement element) => 
            element.IsLoaded;

        public static bool GetIsLoaded(FrameworkElement element) => 
            element.IsLoaded;

        public static bool GetIsMouseOverOverride(FrameworkElement element) => 
            (bool) element.GetValue(IsMouseOverOverrideProperty);

        public static bool GetIsVisible(FrameworkElement element) => 
            element.GetVisible();

        public static object GetToolTip(FrameworkElement element) => 
            element.ToolTip;

        private static void OnClipCornerRadiusChanged(FrameworkElement element)
        {
            element.SizeChanged -= new SizeChangedEventHandler(FrameworkElementHelper.ElementClipCornerRadiusSizeChanged);
            element.SizeChanged += new SizeChangedEventHandler(FrameworkElementHelper.ElementClipCornerRadiusSizeChanged);
            ElementClipCornerRadiusSizeChanged(element, null);
        }

        private static void OnElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((FrameworkElement) sender).ClipToBounds();
        }

        private static void OnEnableIsMouseOverOverrideChanged(FrameworkElement frameworkElement, bool value)
        {
            MouseEventHandler handler = (d, e) => frameworkElement.SetValue(IsMouseOverOverrideProperty, true);
            MouseEventHandler handler2 = (d, e) => frameworkElement.SetValue(IsMouseOverOverrideProperty, false);
            if (value)
            {
                frameworkElement.MouseEnter += handler;
                frameworkElement.MouseLeave += handler2;
            }
            else
            {
                frameworkElement.MouseEnter -= handler;
                frameworkElement.MouseLeave -= handler2;
            }
        }

        private static void OnIsMouseOverOverrideChanged(FrameworkElement frameworkElement)
        {
        }

        public static void SetAllowDrop(FrameworkElement element, bool allowDrop)
        {
            element.AllowDrop = allowDrop;
        }

        public static void SetBypassLayoutPolicies(this FrameworkElement element, bool value)
        {
            SetBypassLayoutPoliciesHandler(element, value);
        }

        public static void SetClipCornerRadius(FrameworkElement element, double value)
        {
            element.SetValue(ClipCornerRadiusProperty, value);
        }

        public static void SetEnableIsMouseOverOverride(FrameworkElement element, bool value)
        {
            element.SetValue(EnableIsMouseOverOverrideProperty, value);
        }

        public static void SetIsClipped(FrameworkElement element, bool value)
        {
            element.SetValue(IsClippedProperty, value);
        }

        public static void SetIsLoaded(FrameworkContentElement element, bool isLoaded)
        {
        }

        public static void SetIsLoaded(FrameworkElement element, bool isLoaded)
        {
        }

        public static void SetIsMouseOverOverride(FrameworkElement element, bool value)
        {
            element.SetValue(IsMouseOverOverrideProperty, value);
        }

        public static void SetIsVisible(FrameworkElement element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static void SetToolTip(FrameworkElement element, object value)
        {
            element.ToolTip = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FrameworkElementHelper.<>c <>9 = new FrameworkElementHelper.<>c();

            internal void <.cctor>b__29_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                FrameworkElementHelper.OnIsMouseOverOverrideChanged((FrameworkElement) o);
            }

            internal void <.cctor>b__29_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                FrameworkElementHelper.OnEnableIsMouseOverOverrideChanged((FrameworkElement) o, (bool) e.NewValue);
            }

            internal void <.cctor>b__29_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                FrameworkElementHelper.OnClipCornerRadiusChanged((FrameworkElement) o);
            }

            internal void <.cctor>b__29_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                FrameworkElement element = o as FrameworkElement;
                if (element != null)
                {
                    if ((bool) e.NewValue)
                    {
                        element.SizeChanged += new SizeChangedEventHandler(FrameworkElementHelper.OnElementSizeChanged);
                    }
                    else
                    {
                        element.SizeChanged -= new SizeChangedEventHandler(FrameworkElementHelper.OnElementSizeChanged);
                    }
                }
            }

            internal void <.cctor>b__29_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FrameworkElement) o).SetVisible((bool) e.NewValue);
            }
        }
    }
}

