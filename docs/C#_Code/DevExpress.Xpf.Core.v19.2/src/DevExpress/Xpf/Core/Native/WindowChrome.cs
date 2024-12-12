namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class WindowChrome : Freezable
    {
        public static readonly DependencyProperty WindowChromeProperty;
        public static readonly DependencyProperty IsHitTestVisibleInChromeProperty;
        public static readonly DependencyProperty ResizeGripDirectionProperty;
        public static readonly DependencyProperty ResizeBorderThicknessProperty;
        public static readonly DependencyProperty IsTouchModeProperty;
        public static readonly DependencyProperty CaptionHeightProperty;

        internal event EventHandler RequestRepaint;

        static WindowChrome();
        private static object CoerceCaptionHeightProperty(Window d, double baseValue);
        protected override Freezable CreateInstanceCore();
        public static double GetCaptionHeight(DependencyObject obj);
        public static bool GetIsHitTestVisibleInChrome(DependencyObject dObj);
        public static bool GetIsTouchMode(DependencyObject obj);
        public static Thickness GetResizeBorderThickness(DependencyObject obj);
        public static ResizeGripDirection GetResizeGripDirection(DependencyObject inputElement);
        public static WindowChrome GetWindowChrome(DependencyObject window);
        private static void OnCaptionHeightChanged(Window d, double oldValue, double newValue);
        private static void OnChromeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnIsTouchModeChanged(Window d, bool oldValue, bool newValue);
        private void RaiseRequestRepaint();
        public static void SetCaptionHeight(DependencyObject obj, double value);
        public static void SetIsHitTestVisibleInChrome(DependencyObject dObj, bool hitTestVisible);
        public static void SetIsTouchMode(DependencyObject obj, bool value);
        public static void SetResizeBorderThickness(DependencyObject obj, Thickness value);
        public static void SetResizeGripDirection(DependencyObject inputElement, ResizeGripDirection direction);
        public static void SetWindowChrome(DependencyObject window, WindowChrome chrome);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowChrome.<>c <>9;

            static <>c();
            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__6_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__6_2(DependencyObject d, object v);
        }
    }
}

