namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class WindowGlowChrome : Freezable
    {
        public static readonly DependencyProperty WindowGlowChromeProperty;
        public static readonly DependencyProperty ShowGlowProperty;
        public static readonly DependencyProperty UseGlowColorsProperty;

        internal event EventHandler RequestRepaint;

        static WindowGlowChrome();
        protected override Freezable CreateInstanceCore();
        public static bool GetShowGlow(DependencyObject obj);
        public static bool GetUseGlowColors(DependencyObject obj);
        public static WindowGlowChrome GetWindowGlowChrome(DependencyObject obj);
        private static void OnShowGlowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnUseGlowColorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnWindowGlowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private void RaiseRequestRepaint();
        internal static void RiseRepaint(DependencyObject d);
        public static void SetShowGlow(DependencyObject obj, bool value);
        public static void SetUseGlowColors(DependencyObject obj, bool value);
        public static void SetWindowGlowChrome(DependencyObject obj, WindowGlowChrome value);
    }
}

