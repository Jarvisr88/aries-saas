namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ArrowControl : Control
    {
        public static readonly DependencyProperty LinkControlProperty;
        public static readonly DependencyProperty ArrowAlignmentProperty;
        public static readonly DependencyProperty LinkContainerTypeProperty;

        static ArrowControl();
        public ArrowControl();
        public override void OnApplyTemplate();
        protected virtual void OnArrowAlignmentChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnArrowAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnLinkContainerTypeChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnLinkContainerTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnLinkControlChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnLinkControlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected virtual void UpdateVisualStateByArrowAlignment();

        public BarItemLinkControl LinkControl { get; set; }

        public Dock ArrowAlignment { get; set; }

        public DevExpress.Xpf.Bars.LinkContainerType LinkContainerType { get; set; }
    }
}

