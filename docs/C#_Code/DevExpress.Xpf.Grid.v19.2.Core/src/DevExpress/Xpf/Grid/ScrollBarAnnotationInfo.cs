namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ScrollBarAnnotationInfo : DependencyObject
    {
        public static readonly DependencyProperty AlignmentProperty;
        public static readonly DependencyProperty WidthProperty;
        public static readonly DependencyProperty MinHeightProperty;
        public static readonly DependencyProperty BrushProperty;

        static ScrollBarAnnotationInfo()
        {
            Type ownerType = typeof(ScrollBarAnnotationInfo);
            AlignmentProperty = DependencyProperty.Register("Alignment", typeof(ScrollBarAnnotationAlignment), ownerType, new PropertyMetadata(ScrollBarAnnotationAlignment.Full, (d, e) => ((ScrollBarAnnotationInfo) d).ChangedInfo()));
            WidthProperty = DependencyProperty.Register("Width", typeof(double), ownerType, new PropertyMetadata(1.0, (d, e) => ((ScrollBarAnnotationInfo) d).ChangedInfo()), new ValidateValueCallback(ScrollBarAnnotationInfo.ValidateSize));
            MinHeightProperty = DependencyProperty.Register("MinHeight", typeof(double), ownerType, new PropertyMetadata(1.0, (d, e) => ((ScrollBarAnnotationInfo) d).ChangedInfo()), new ValidateValueCallback(ScrollBarAnnotationInfo.ValidateSize));
            BrushProperty = DependencyProperty.Register("Brush", typeof(System.Windows.Media.Brush), ownerType, new PropertyMetadata(Brushes.Black, (d, e) => ((ScrollBarAnnotationInfo) d).ChangedInfo()), new ValidateValueCallback(ScrollBarAnnotationInfo.ValidateBrush));
        }

        public ScrollBarAnnotationInfo()
        {
        }

        public ScrollBarAnnotationInfo(System.Windows.Media.Brush brush, ScrollBarAnnotationAlignment alignment, double minHeight, double width)
        {
            this.Brush = brush;
            this.Alignment = alignment;
            this.MinHeight = minHeight;
            this.Width = width;
        }

        private void ChangedInfo()
        {
            ITableView view = ((this.Owner == null) || !this.Owner.IsAlive) ? null : (this.Owner.Target as ITableView);
            if ((view != null) && (this.Mode != null))
            {
                view.ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(this.Mode.Value, false);
            }
        }

        private static bool ValidateBrush(object value) => 
            value != null;

        private static bool ValidateSize(object value) => 
            ((double) value) > 0.0;

        internal WeakReference Owner { get; set; }

        internal ScrollBarAnnotationMode? Mode { get; set; }

        public ScrollBarAnnotationAlignment Alignment
        {
            get => 
                (ScrollBarAnnotationAlignment) base.GetValue(AlignmentProperty);
            set => 
                base.SetValue(AlignmentProperty, value);
        }

        public double Width
        {
            get => 
                (double) base.GetValue(WidthProperty);
            set => 
                base.SetValue(WidthProperty, value);
        }

        public double MinHeight
        {
            get => 
                (double) base.GetValue(MinHeightProperty);
            set => 
                base.SetValue(MinHeightProperty, value);
        }

        public System.Windows.Media.Brush Brush
        {
            get => 
                (System.Windows.Media.Brush) base.GetValue(BrushProperty);
            set => 
                base.SetValue(BrushProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollBarAnnotationInfo.<>c <>9 = new ScrollBarAnnotationInfo.<>c();

            internal void <.cctor>b__14_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationInfo) d).ChangedInfo();
            }

            internal void <.cctor>b__14_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationInfo) d).ChangedInfo();
            }

            internal void <.cctor>b__14_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationInfo) d).ChangedInfo();
            }

            internal void <.cctor>b__14_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollBarAnnotationInfo) d).ChangedInfo();
            }
        }
    }
}

