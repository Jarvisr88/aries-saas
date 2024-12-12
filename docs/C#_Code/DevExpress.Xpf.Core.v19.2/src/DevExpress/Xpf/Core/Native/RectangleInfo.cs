namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class RectangleInfo : BaseRenderInfo
    {
        private Brush background;
        private Brush borderBrush;
        private double thickness;

        protected virtual void OnBorderBrushChanged(Brush oldValue);
        protected virtual void OnThicknessChanged(double oldValue);
        protected override void RenderOverride(DrawingContext dc, Rect bounds);
        private void UpdeteBorderPen();

        public Brush Background { get; set; }

        public Brush BorderBrush { get; set; }

        public double RadiusX { get; set; }

        public double RadiusY { get; set; }

        public double Thickness { get; set; }

        public System.Windows.Media.Pen Pen { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RectangleInfo.<>c <>9;
            public static Func<Brush, Brush> <>9__5_0;
            public static Func<Brush, Brush> <>9__8_0;

            static <>c();
            internal Brush <set_Background>b__5_0(Brush x);
            internal Brush <set_BorderBrush>b__8_0(Brush x);
        }
    }
}

