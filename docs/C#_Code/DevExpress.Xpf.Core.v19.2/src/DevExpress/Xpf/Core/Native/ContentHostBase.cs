namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class ContentHostBase : Panel
    {
        private ContentHostPresenterBase ownerPresenter;
        private FrameworkElement child;

        public ContentHostBase();
        protected override Size ArrangeOverride(Size finalSize);
        protected abstract ContentHostPresenterBase FindContentHostPresenter();
        protected abstract string GetHostName();
        protected override Size MeasureOverride(Size availableSize);
        private void OnChildChanged(FrameworkElement oldValue, FrameworkElement newValue);
        private void OnLayoutUpdated(object sender, EventArgs e);
        private void OnLoaded(object sender, RoutedEventArgs e);
        private void OnOwnerPresenterChanged(ContentHostPresenterBase oldValue, ContentHostPresenterBase newValue);
        private void OnUnloaded(object sender, RoutedEventArgs e);
        protected override void OnVisualParentChanged(DependencyObject oldParent);

        public ContentHostPresenterBase OwnerPresenter { get; internal set; }

        public FrameworkElement Child { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentHostBase.<>c <>9;
            public static Func<FrameworkElement, Size> <>9__17_1;
            public static Func<Size> <>9__17_2;

            static <>c();
            internal Size <MeasureOverride>b__17_1(FrameworkElement x);
            internal Size <MeasureOverride>b__17_2();
        }
    }
}

