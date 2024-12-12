namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ContentAndFooterHostPresenter : ContentHostPresenter
    {
        public static readonly DependencyProperty FooterCacheModeProperty;
        public static readonly DependencyProperty RegularFooterPresenterProperty;
        public static readonly DependencyProperty FastFooterPresenterProperty;

        static ContentAndFooterHostPresenter();
        public ContentAndFooterHostPresenter();
        private void UpdateContentHostChild();

        public TabContentCacheMode FooterCacheMode { get; set; }

        public object RegularFooterPresenter { get; set; }

        public object FastFooterPresenter { get; set; }

        public FrameworkElement FooterHostChild { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentAndFooterHostPresenter.<>c <>9;

            static <>c();
            internal void <.cctor>b__18_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__18_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

