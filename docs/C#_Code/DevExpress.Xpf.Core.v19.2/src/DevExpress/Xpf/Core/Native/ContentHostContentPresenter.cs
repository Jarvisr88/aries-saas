namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ContentHostContentPresenter : ContentPresenter
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty IsTouchEnabledProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty IsTouchlineThemeProperty;

        static ContentHostContentPresenter();
        public ContentHostContentPresenter();
        private void OnIsTouchEnabledChanged();
        private void OnIsTouchlineThemeChanged();
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentHostContentPresenter.<>c <>9;

            static <>c();
            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__6_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

