namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class VerticalSizeHelper : SizeHelperBase
    {
        public static readonly SizeHelperBase Instance = new VerticalSizeHelper();

        private VerticalSizeHelper()
        {
        }

        public override Point CreatePoint(double definePoint, double secondaryPoint) => 
            new Point(secondaryPoint, definePoint);

        public override Size CreateSize(double defineSize, double secondarySize) => 
            new Size(secondarySize, defineSize);

        public override double GetDefinePoint(Point point) => 
            point.Y;

        public override double GetDefineSize(FrameworkElement elem) => 
            (elem == null) ? 0.0 : elem.ActualHeight;

        public override double GetDefineSize(Size size) => 
            size.Height;

        public override double GetMarginSpace(Thickness thickness) => 
            thickness.Bottom + thickness.Top;

        public override double GetSecondaryPoint(Point point) => 
            point.X;

        public override double GetSecondarySize(Size size) => 
            size.Width;

        public override void SetDefinePoint(ref Point point, double value)
        {
            point.Y = value;
        }

        public override void SetDefineSize(ref Size size, double value)
        {
            size.Height = value;
        }

        public override void SetSecondaryPoint(ref Point point, double value)
        {
            point.X = value;
        }

        public override void SetSecondarySize(ref Size size, double value)
        {
            size.Width = value;
        }
    }
}

