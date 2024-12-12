namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class PdfGeometry
    {
        private readonly System.Windows.Media.Matrix transform = System.Windows.Media.Matrix.Identity;
        private readonly bool nonZero;
        private readonly PointF[] points;
        private readonly byte[] pointTypes;

        public PdfGeometry(Geometry geometry, bool fill, bool stroke, bool applyTransform = false)
        {
            PathGeometry geometry2 = PathGeometry.CreateFromGeometry(geometry);
            this.transform = (!applyTransform || (geometry2.Transform == null)) ? System.Windows.Media.Matrix.Identity : geometry2.Transform.Value;
            this.nonZero = geometry2.FillRule == FillRule.Nonzero;
            List<System.Windows.Point> points = new List<System.Windows.Point>();
            List<PathPointType> pointTypes = new List<PathPointType>();
            foreach (PathFigure figure in geometry2.Figures)
            {
                if (!fill || figure.IsFilled)
                {
                    pointTypes.Add(PathPointType.Start);
                    points.Add(this.GetPoint(figure.StartPoint));
                    foreach (PathSegment segment in figure.Segments)
                    {
                        PathSegmentBuilder builder = PathSegmentBuilder.CreateSegmentBuilder(segment, this.transform);
                        if (!stroke || segment.IsStroked)
                        {
                            builder.GenerateData(points, pointTypes);
                            continue;
                        }
                        pointTypes.Add(PathPointType.Start);
                        points.Add(this.GetPoint(builder.GetLastPoint()));
                    }
                    if (figure.IsClosed)
                    {
                        List<PathPointType> list3 = pointTypes;
                        int num = pointTypes.Count - 1;
                        list3[num] = ((PathPointType) list3[num]) | PathPointType.CloseSubpath;
                    }
                }
            }
            Func<System.Windows.Point, PointF> selector = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<System.Windows.Point, PointF> local1 = <>c.<>9__10_0;
                selector = <>c.<>9__10_0 = x => x.ToPointF();
            }
            this.points = points.Select<System.Windows.Point, PointF>(selector).ToArray<PointF>();
            Func<PathPointType, byte> func2 = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Func<PathPointType, byte> local2 = <>c.<>9__10_1;
                func2 = <>c.<>9__10_1 = x => (byte) x;
            }
            this.pointTypes = pointTypes.Select<PathPointType, byte>(func2).ToArray<byte>();
        }

        private System.Windows.Point GetPoint(System.Windows.Point sourcePoint) => 
            sourcePoint.Transform(this.transform);

        public bool NonZero =>
            this.nonZero;

        public PointF[] Points =>
            this.points;

        public byte[] PointTypes =>
            this.pointTypes;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfGeometry.<>c <>9 = new PdfGeometry.<>c();
            public static Func<System.Windows.Point, PointF> <>9__10_0;
            public static Func<PathPointType, byte> <>9__10_1;

            internal PointF <.ctor>b__10_0(System.Windows.Point x) => 
                x.ToPointF();

            internal byte <.ctor>b__10_1(PathPointType x) => 
                (byte) x;
        }
    }
}

