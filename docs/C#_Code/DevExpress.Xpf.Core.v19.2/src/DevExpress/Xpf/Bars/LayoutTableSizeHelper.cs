namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class LayoutTableSizeHelper
    {
        protected LayoutTableSizeHelper();
        public abstract TResult Do<TResult>(Func<TResult> horizontal, Func<TResult> vertical);
        public static LayoutTableSizeHelper Get(Orientation orientation);
        public abstract double GetHeight(Size size);
        public abstract double GetWidth(Size size);
        public abstract double GetX(Point point);
        public abstract double GetY(Point point);
        public abstract void SetHeight(ref Size size, double value);
        public abstract void SetWidth(ref Size size, double value);
        public abstract Point Translate(Point point);
        public abstract Size Translate(Size size);

        private class HorizontalLayoutTableSizeHelper : LayoutTableSizeHelper
        {
            public override TResult Do<TResult>(Func<TResult> horizontal, Func<TResult> vertical);
            public override double GetHeight(Size size);
            public override double GetWidth(Size size);
            public override double GetX(Point point);
            public override double GetY(Point point);
            public override void SetHeight(ref Size size, double value);
            public override void SetWidth(ref Size size, double value);
            public override Point Translate(Point point);
            public override Size Translate(Size size);
        }

        private class VerticalLayoutTableSizeHelper : LayoutTableSizeHelper
        {
            public override TResult Do<TResult>(Func<TResult> horizontal, Func<TResult> vertical);
            public override double GetHeight(Size size);
            public override double GetWidth(Size size);
            public override double GetX(Point point);
            public override double GetY(Point point);
            public override void SetHeight(ref Size size, double value);
            public override void SetWidth(ref Size size, double value);
            public override Point Translate(Point point);
            public override Size Translate(Size size);
        }
    }
}

