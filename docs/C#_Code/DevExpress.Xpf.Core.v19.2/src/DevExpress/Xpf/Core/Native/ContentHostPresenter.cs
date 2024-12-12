namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ContentHostPresenter : ContentHostPresenterBase
    {
        public static readonly DependencyProperty ContentCacheModeProperty;
        public static readonly DependencyProperty RegularContentPresenterProperty;
        public static readonly DependencyProperty FastContentPresenterProperty;

        static ContentHostPresenter();
        public ContentHostPresenter();
        private void UpdateContentHostChild();

        public TabContentCacheMode ContentCacheMode { get; set; }

        public object RegularContentPresenter { get; set; }

        public object FastContentPresenter { get; set; }

        public FrameworkElement ContentHostChild { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentHostPresenter.<>c <>9;

            static <>c();
            internal void <.cctor>b__18_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__18_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

