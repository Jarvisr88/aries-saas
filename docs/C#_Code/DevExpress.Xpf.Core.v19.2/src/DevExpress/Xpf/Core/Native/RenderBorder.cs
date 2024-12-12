namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class RenderBorder : RenderDecorator
    {
        private Brush background;
        private Brush borderBrush;
        private Thickness borderThickness;
        private Thickness padding;
        private System.Windows.CornerRadius cornerRadius;

        private static bool AreUniformCorners(System.Windows.CornerRadius borderRadii);
        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        private static Rect DeflateRect(Rect rt, Thickness thick);
        protected void DrawGeometry(RenderBorderContext context, DrawingContext dc, Brush brush, Pen pen, Geometry geometry);
        protected void DrawLine(RenderBorderContext context, DrawingContext dc, Pen pen, Point point0, Point point1);
        protected void DrawRectangle(RenderBorderContext context, DrawingContext dc, Brush brush, Pen pen, Rect rectangle);
        protected void DrawRoundedRectangle(RenderBorderContext context, DrawingContext dc, Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY);
        private static void GenerateGeometry(StreamGeometryContext ctx, Rect rect, RenderBorder.Radii radii);
        private static bool IsUniformThickness(Thickness th);
        private static bool IsZeroThickness(Thickness th);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected override void RenderOverride(DrawingContext dc, IFrameworkRenderElementContext context);

        public Brush Background { get; set; }

        public Brush BorderBrush { get; set; }

        public Thickness BorderThickness { get; set; }

        public Thickness Padding { get; set; }

        public System.Windows.CornerRadius CornerRadius { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderBorder.<>c <>9;
            public static Func<Brush, Brush> <>9__8_0;
            public static Func<Brush, Brush> <>9__11_0;

            static <>c();
            internal Brush <set_Background>b__8_0(Brush x);
            internal Brush <set_BorderBrush>b__11_0(Brush x);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Radii
        {
            internal readonly double LeftTop;
            internal readonly double TopLeft;
            internal readonly double TopRight;
            internal readonly double RightTop;
            internal readonly double RightBottom;
            internal readonly double BottomRight;
            internal readonly double BottomLeft;
            internal readonly double LeftBottom;
            internal Radii(CornerRadius radii, Thickness borders, bool outer);
        }
    }
}

