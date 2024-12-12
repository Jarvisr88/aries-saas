namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PathInfo : BaseRenderInfo
    {
        private double thickness;
        private Brush fill;
        private string data;
        private Brush stroke;
        private Geometry frozenGeometry;

        public PathInfo();
        protected override void RenderOverride(DrawingContext dc, Rect bounds);
        private void UpdatePen();

        public Brush Stroke { get; set; }

        public string Data { get; set; }

        public Brush Fill { get; set; }

        public double Thickness { get; set; }

        public System.Windows.Media.Pen Pen { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PathInfo.<>c <>9;
            public static Func<Brush, Brush> <>9__7_0;
            public static Func<Brush, Brush> <>9__13_0;

            static <>c();
            internal Brush <set_Fill>b__13_0(Brush x);
            internal Brush <set_Stroke>b__7_0(Brush x);
        }
    }
}

