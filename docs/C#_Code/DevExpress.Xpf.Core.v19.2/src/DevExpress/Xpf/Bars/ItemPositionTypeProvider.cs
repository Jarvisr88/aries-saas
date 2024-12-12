namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public class ItemPositionTypeProvider : DependencyObject
    {
        public static readonly DependencyProperty HorizontalItemPositionProperty;

        static ItemPositionTypeProvider();
        public static HorizontalItemPositionType GetHorizontalItemPosition(DependencyObject d);
        protected static void OnHorizontalItemPositionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        public static void SetHorizontalItemPosition(DependencyObject d, HorizontalItemPositionType value);
    }
}

