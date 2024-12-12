namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class SizeHelperBase
    {
        protected SizeHelperBase()
        {
        }

        public abstract Point CreatePoint(double definePoint, double secondaryPoint);
        public abstract Size CreateSize(double defineSize, double secondarySize);
        public abstract double GetDefinePoint(Point point);
        public abstract double GetDefineSize(FrameworkElement elem);
        public abstract double GetDefineSize(Size size);
        public static SizeHelperBase GetDefineSizeHelper(Orientation orientation) => 
            (orientation == Orientation.Vertical) ? VerticalSizeHelper.Instance : HorizontalSizeHelper.Instance;

        public abstract double GetMarginSpace(Thickness thickness);
        public abstract double GetSecondaryPoint(Point point);
        public abstract double GetSecondarySize(Size size);
        public static SizeHelperBase GetSecondarySizeHelper(Orientation orientation) => 
            (orientation == Orientation.Vertical) ? HorizontalSizeHelper.Instance : VerticalSizeHelper.Instance;

        public abstract void SetDefinePoint(ref Point point, double value);
        public abstract void SetDefineSize(ref Size size, double value);
        public abstract void SetSecondaryPoint(ref Point point, double value);
        public abstract void SetSecondarySize(ref Size size, double value);
    }
}

