namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class GridLengthAnimation : AnimationTimeline
    {
        public static readonly GridLength ZeroStar = new GridLength(0.0, GridUnitType.Star);
        public static readonly GridLength Zero = new GridLength(0.0, GridUnitType.Pixel);
        public static readonly DependencyProperty ToProperty;
        public static readonly DependencyProperty FromProperty;

        static GridLengthAnimation()
        {
            DependencyPropertyRegistrator<GridLengthAnimation> registrator = new DependencyPropertyRegistrator<GridLengthAnimation>();
            registrator.Register<GridLength>("To", ref ToProperty, ZeroStar, null, null);
            registrator.Register<GridLength>("From", ref FromProperty, ZeroStar, null, null);
        }

        internal static GridLength CalcCurrentValue(double from, double to, double progress, bool isStar)
        {
            double num3 = Math.Min(from, to);
            return new GridLength((((from > to) ? (1.0 - progress) : progress) * (Math.Max(from, to) - num3)) + num3, isStar ? GridUnitType.Star : GridUnitType.Pixel);
        }

        protected override Freezable CreateInstanceCore() => 
            new GridLengthAnimation();

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock) => 
            CalcCurrentValue(this.From.Value, this.To.Value, animationClock.CurrentProgress.Value, this.To.IsStar);

        public GridLength To
        {
            get => 
                (GridLength) base.GetValue(ToProperty);
            set => 
                base.SetValue(ToProperty, value);
        }

        public GridLength From
        {
            get => 
                (GridLength) base.GetValue(FromProperty);
            set => 
                base.SetValue(FromProperty, value);
        }

        public override Type TargetPropertyType =>
            typeof(GridLength);
    }
}

