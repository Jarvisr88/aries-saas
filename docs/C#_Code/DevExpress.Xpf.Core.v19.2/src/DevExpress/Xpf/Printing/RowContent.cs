namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public class RowContent : DependencyObject
    {
        public static readonly DependencyProperty IsEvenProperty = DependencyPropertyManager.Register("IsEven", typeof(bool), typeof(RowContent), new PropertyMetadata(false));
        public static readonly DependencyProperty UsablePageWidthProperty = DependencyPropertyManager.Register("UsablePageWidth", typeof(double), typeof(RowContent), new PropertyMetadata(0.0));
        public static readonly DependencyProperty UsablePageHeightProperty = DependencyPropertyManager.Register("UsablePageHeight", typeof(double), typeof(RowContent), new UIPropertyMetadata(0.0));
        public static readonly DependencyProperty ContentProperty = DependencyPropertyManager.Register("Content", typeof(object), typeof(RowContent), new PropertyMetadata(null));

        public bool IsEven
        {
            get => 
                (bool) base.GetValue(IsEvenProperty);
            set => 
                base.SetValue(IsEvenProperty, value);
        }

        public double UsablePageWidth
        {
            get => 
                (double) base.GetValue(UsablePageWidthProperty);
            set => 
                base.SetValue(UsablePageWidthProperty, value);
        }

        public double UsablePageHeight
        {
            get => 
                (double) base.GetValue(UsablePageHeightProperty);
            set => 
                base.SetValue(UsablePageHeightProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }
    }
}

