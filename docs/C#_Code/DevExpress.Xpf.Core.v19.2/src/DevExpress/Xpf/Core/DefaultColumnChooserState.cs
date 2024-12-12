namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Windows;

    public class DefaultColumnChooserState : DependencyObject, IColumnChooserState
    {
        public static readonly DependencyProperty LocationProperty = DependencyPropertyManager.Register("Location", typeof(Point), typeof(DefaultColumnChooserState), new UIPropertyMetadata(new Point(double.NaN, double.NaN)));
        public static readonly DependencyProperty SizeProperty = DependencyPropertyManager.Register("Size", typeof(System.Windows.Size), typeof(DefaultColumnChooserState), new UIPropertyMetadata(new System.Windows.Size(220.0, 250.0)));
        public static readonly DependencyProperty MinWidthProperty = DependencyPropertyManager.Register("MinWidth", typeof(double), typeof(DefaultColumnChooserState), new UIPropertyMetadata(200.0));
        public static readonly DependencyProperty MaxWidthProperty = DependencyPropertyManager.Register("MaxWidth", typeof(double), typeof(DefaultColumnChooserState), new UIPropertyMetadata(1.7976931348623157E+308));
        public static readonly DependencyProperty MinHeightProperty = DependencyPropertyManager.Register("MinHeight", typeof(double), typeof(DefaultColumnChooserState), new UIPropertyMetadata(200.0));
        public static readonly DependencyProperty MaxHeightProperty = DependencyPropertyManager.Register("MaxHeight", typeof(double), typeof(DefaultColumnChooserState), new UIPropertyMetadata(1.7976931348623157E+308));
        [ThreadStatic]
        private static DefaultColumnChooserState defaultState;

        public static DefaultColumnChooserState DefaultState
        {
            get
            {
                defaultState ??= new DefaultColumnChooserState();
                return defaultState;
            }
        }

        [XtraSerializableProperty]
        public Point Location
        {
            get => 
                (Point) base.GetValue(LocationProperty);
            set => 
                base.SetValue(LocationProperty, value);
        }

        [XtraSerializableProperty]
        public System.Windows.Size Size
        {
            get => 
                (System.Windows.Size) base.GetValue(SizeProperty);
            set => 
                base.SetValue(SizeProperty, value);
        }

        public double MinWidth
        {
            get => 
                (double) base.GetValue(MinWidthProperty);
            set => 
                base.SetValue(MinWidthProperty, value);
        }

        public double MaxWidth
        {
            get => 
                (double) base.GetValue(MaxWidthProperty);
            set => 
                base.SetValue(MaxWidthProperty, value);
        }

        public double MinHeight
        {
            get => 
                (double) base.GetValue(MinHeightProperty);
            set => 
                base.SetValue(MinHeightProperty, value);
        }

        public double MaxHeight
        {
            get => 
                (double) base.GetValue(MaxHeightProperty);
            set => 
                base.SetValue(MaxHeightProperty, value);
        }
    }
}

