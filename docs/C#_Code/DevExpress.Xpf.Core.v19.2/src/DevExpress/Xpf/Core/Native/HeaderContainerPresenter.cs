namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("ActualChild")]
    public class HeaderContainerPresenter : Decorator
    {
        public static readonly DependencyProperty ViewInfoProperty;
        public static readonly DependencyProperty ChildMinHeightProperty;

        static HeaderContainerPresenter();
        public HeaderContainerPresenter();
        protected override Size ArrangeOverride(Size finalSize);
        protected override Size MeasureOverride(Size availableSize);
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e);

        public TabViewInfo ViewInfo { get; set; }

        public double ChildMinHeight { get; set; }

        public FrameworkElement ActualChild { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Border Child { get; private set; }
    }
}

