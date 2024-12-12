namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class PolyQuadraticBezierSegmentBuilder : PathSegmentBuilder<PolyQuadraticBezierSegment>
    {
        internal PolyQuadraticBezierSegmentBuilder(PolyQuadraticBezierSegment segment, System.Windows.Media.Matrix transform) : base(segment, transform)
        {
        }

        public override void GenerateData(List<Point> points, List<PathPointType> pointTypes)
        {
            for (int i = 0; i < base.Segment.Points.Count; i += 2)
            {
                Point startPoint = points.Last<Point>();
                Point point = base.GetPoint(base.Segment.Points[i]);
                Point point3 = base.GetPoint(base.Segment.Points[i + 1]);
                points.Add(QuadraticBezierSegmentBuilder.GetPointPosition(startPoint, point));
                points.Add(QuadraticBezierSegmentBuilder.GetPointPosition(point3, point));
                points.Add(point3);
                pointTypes.Add(PathPointType.Bezier);
                pointTypes.Add(PathPointType.Bezier);
                pointTypes.Add(PathPointType.Bezier);
            }
        }

        public override Point GetLastPoint() => 
            base.Segment.Points.Last<Point>();
    }
}

