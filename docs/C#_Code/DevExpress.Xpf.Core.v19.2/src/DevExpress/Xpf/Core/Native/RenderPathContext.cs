namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class RenderPathContext : FrameworkRenderElementContext
    {
        private Geometry data;
        private System.Windows.Media.Stretch? stretch;
        private Brush fill;
        private Brush stroke;
        private double? strokeThickness;
        private PenLineCap? strokeStartLineCap;
        private PenLineCap? strokeEndLineCap;
        private PenLineCap? strokeDashCap;
        private PenLineJoin? strokeLineJoin;
        private double? strokeMiterLimit;
        private double? strokeDashOffset;
        private DoubleCollection strokeDashArray;

        public RenderPathContext(RenderPath factory);

        public Geometry Data { get; set; }

        public System.Windows.Media.Stretch? Stretch { get; set; }

        public Brush Fill { get; set; }

        public Brush Stroke { get; set; }

        public double? StrokeThickness { get; set; }

        public PenLineCap? StrokeStartLineCap { get; set; }

        public PenLineCap? StrokeEndLineCap { get; set; }

        public PenLineCap? StrokeDashCap { get; set; }

        public PenLineJoin? StrokeLineJoin { get; set; }

        public double? StrokeMiterLimit { get; set; }

        public double? StrokeDashOffset { get; set; }

        public DoubleCollection StrokeDashArray { get; set; }

        public Geometry ActualData { get; }

        public System.Windows.Media.Stretch ActualStretch { get; }

        public virtual Brush ActualFill { get; }

        public Brush ActualStroke { get; }

        public double ActualStrokeThickness { get; }

        public PenLineCap ActualStrokeStartLineCap { get; }

        public PenLineCap ActualStrokeEndLineCap { get; }

        public PenLineCap ActualStrokeDashCap { get; }

        public PenLineJoin ActualStrokeLineJoin { get; }

        public double ActualStrokeMiterLimit { get; }

        public double ActualStrokeDashOffset { get; }

        public DoubleCollection ActualStrokeDashArray { get; }

        public RenderPath Factory { get; }

        public System.Windows.Media.Pen Pen { get; internal set; }

        public Geometry RenderedGeometry { get; internal set; }

        public Matrix? StretchMatrix { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderPathContext.<>c <>9;
            public static Func<Geometry, Geometry> <>9__14_0;
            public static Func<Brush, Brush> <>9__20_0;
            public static Func<Brush, Brush> <>9__23_0;

            static <>c();
            internal Geometry <set_Data>b__14_0(Geometry x);
            internal Brush <set_Fill>b__20_0(Brush x);
            internal Brush <set_Stroke>b__23_0(Brush x);
        }
    }
}

