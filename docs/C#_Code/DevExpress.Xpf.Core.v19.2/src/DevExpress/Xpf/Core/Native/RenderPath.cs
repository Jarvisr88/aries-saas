namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class RenderPath : FrameworkRenderElement
    {
        private Geometry data;
        private System.Windows.Media.Stretch stretch;
        private Brush fill;
        private Brush stroke;
        private double strokeThickness;
        private PenLineCap strokeStartLineCap;
        private PenLineCap strokeEndLineCap;
        private PenLineCap strokeDashCap;
        private PenLineJoin strokeLineJoin;
        private double strokeMiterLimit;
        private double strokeDashOffset;
        private DoubleCollection strokeDashArray;

        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        private void EnsureRenderedGeometry(RenderPathContext context);
        [IteratorStateMachine(typeof(RenderPath.<GetChildren>d__57))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren();
        private Size GetNaturalSize(RenderPathContext context);
        private Pen GetPen(RenderPathContext context);
        internal Size GetStretchedRenderSize(System.Windows.Media.Stretch mode, double strokeThickness, Size availableSize, Rect geometryBounds);
        internal Size GetStretchedRenderSizeAndSetStretchMatrix(RenderPathContext context, System.Windows.Media.Stretch mode, double strokeThickness, Size availableSize, Rect geometryBounds);
        private void GetStretchMetrics(System.Windows.Media.Stretch mode, double strokeThickness, Size availableSize, Rect geometryBounds, out double xScale, out double yScale, out double dX, out double dY, out Size stretchedSize);
        private double GetStrokeThickness(RenderPathContext context);
        private bool IsPenNoOp(RenderPathContext context);
        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected override void RenderOverride(DrawingContext dc, IFrameworkRenderElementContext context);
        internal bool SizeIsInvalidOrEmpty(Size size);

        public System.Windows.Media.Stretch Stretch { get; set; }

        public Geometry Data { get; set; }

        public Brush Fill { get; set; }

        public Brush Stroke { get; set; }

        public double StrokeThickness { get; set; }

        public PenLineCap StrokeStartLineCap { get; set; }

        public PenLineCap StrokeEndLineCap { get; set; }

        public PenLineCap StrokeDashCap { get; set; }

        public PenLineJoin StrokeLineJoin { get; set; }

        public double StrokeMiterLimit { get; set; }

        public double StrokeDashOffset { get; set; }

        public DoubleCollection StrokeDashArray { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderPath.<>c <>9;
            public static Func<Geometry, Geometry> <>9__17_0;
            public static Func<Brush, Brush> <>9__20_0;
            public static Func<Brush, Brush> <>9__23_0;

            static <>c();
            internal Geometry <set_Data>b__17_0(Geometry x);
            internal Brush <set_Fill>b__20_0(Brush x);
            internal Brush <set_Stroke>b__23_0(Brush x);
        }

        [CompilerGenerated]
        private sealed class <GetChildren>d__57 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetChildren>d__57(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<IFrameworkRenderElement> IEnumerable<IFrameworkRenderElement>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            IFrameworkRenderElement IEnumerator<IFrameworkRenderElement>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

