namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BadgeProperties
    {
        public static readonly DependencyProperty BlendingModeProperty;
        public static readonly DependencyProperty VisibilityProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyPropertyKey IsVisiblePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsVisibleProperty;

        static BadgeProperties();
        public static BadgeBlendingMode GetBlendingMode(DependencyObject element);
        public static bool GetIsVisible(DependencyObject element);
        public static Visibility GetVisibility(DependencyObject element);
        public static void SetBlendingMode(DependencyObject element, BadgeBlendingMode value);
        public static void SetVisibility(DependencyObject element, Visibility value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BadgeProperties.<>c <>9;

            static <>c();
            internal void <.cctor>b__9_0(DependencyObject o, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__9_1(DependencyObject o, object value);
            internal void <.cctor>b__9_2(DependencyObject o, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__9_3(DependencyObject o, DependencyPropertyChangedEventArgs args);
        }
    }
}

