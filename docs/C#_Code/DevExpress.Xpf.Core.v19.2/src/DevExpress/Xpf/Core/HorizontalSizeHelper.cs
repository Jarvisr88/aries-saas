namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class HorizontalSizeHelper : SizeHelperBase
    {
        public static readonly SizeHelperBase Instance = new HorizontalSizeHelper();

        private HorizontalSizeHelper()
        {
        }

        public override Point CreatePoint(double definePoint, double secondaryPoint) => 
            new Point(definePoint, secondaryPoint);

        public override Size CreateSize(double defineSize, double secondarySize) => 
            new Size(defineSize, secondarySize);

        public override double GetDefinePoint(Point point) => 
            point.X;

        public override double GetDefineSize(FrameworkElement elem) => 
            (elem == null) ? 0.0 : elem.ActualWidth;

        public override double GetDefineSize(Size size) => 
            size.Width;

        public override double GetMarginSpace(Thickness thickness) => 
            thickness.Left + thickness.Right;

        public override double GetSecondaryPoint(Point point) => 
            point.Y;

        public override double GetSecondarySize(Size size) => 
            size.Height;

        public override void SetDefinePoint(ref Point point, double value)
        {
            point.X = value;
        }

        public override void SetDefineSize(ref Size size, double value)
        {
            size.Width = value;
        }

        public override void SetSecondaryPoint(ref Point point, double value)
        {
            point.Y = value;
        }

        public override void SetSecondarySize(ref Size size, double value)
        {
            size.Height = value;
        }
    }
}

