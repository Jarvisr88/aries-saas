namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    internal class ReservedSpaceCalculator : DependencyObject
    {
        public static readonly DependencyProperty ReservedSpaceProperty;
        public static readonly DependencyProperty ThumbLengthProperty;
        public static readonly DependencyProperty LeftThumbLengthProperty;
        public static readonly DependencyProperty RightThumbLengthProperty;

        static ReservedSpaceCalculator()
        {
            Type ownerType = typeof(ReservedSpaceCalculator);
            ReservedSpaceProperty = DependencyProperty.Register("ReservedSpace", typeof(double), ownerType, new PropertyMetadata(0.0));
            ThumbLengthProperty = DependencyProperty.Register("ThumbLength", typeof(double), ownerType, new PropertyMetadata(0.0, new PropertyChangedCallback(ReservedSpaceCalculator.PropertyChanged)));
            LeftThumbLengthProperty = DependencyProperty.Register("LeftThumbLength", typeof(double), ownerType, new PropertyMetadata(0.0, new PropertyChangedCallback(ReservedSpaceCalculator.PropertyChanged)));
            RightThumbLengthProperty = DependencyProperty.Register("RightThumbLength", typeof(double), ownerType, new PropertyMetadata(0.0, new PropertyChangedCallback(ReservedSpaceCalculator.PropertyChanged)));
        }

        public double GetReservedSpace() => 
            (this.ThumbLength + this.LeftThumbLength) + this.RightThumbLength;

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReservedSpaceCalculator calculator = (ReservedSpaceCalculator) d;
            calculator.ReservedSpace = (calculator.ThumbLength + calculator.LeftThumbLength) + calculator.RightThumbLength;
        }

        public double ThumbLength
        {
            get => 
                (double) base.GetValue(ThumbLengthProperty);
            set => 
                base.SetValue(ThumbLengthProperty, value);
        }

        public double ReservedSpace
        {
            get => 
                (double) base.GetValue(ReservedSpaceProperty);
            set => 
                base.SetValue(ReservedSpaceProperty, value);
        }

        public double LeftThumbLength
        {
            get => 
                (double) base.GetValue(LeftThumbLengthProperty);
            set => 
                base.SetValue(LeftThumbLengthProperty, value);
        }

        public double RightThumbLength
        {
            get => 
                (double) base.GetValue(RightThumbLengthProperty);
            set => 
                base.SetValue(RightThumbLengthProperty, value);
        }
    }
}

