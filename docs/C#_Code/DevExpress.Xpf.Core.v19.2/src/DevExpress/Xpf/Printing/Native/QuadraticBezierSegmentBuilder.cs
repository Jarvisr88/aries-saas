namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class QuadraticBezierSegmentBuilder : PathSegmentBuilder<QuadraticBezierSegment>
    {
        internal QuadraticBezierSegmentBuilder(QuadraticBezierSegment segment, System.Windows.Media.Matrix transform) : base(segment, transform)
        {
        }

        public override void GenerateData(List<Point> points, List<PathPointType> pointTypes)
        {
            Point startPoint = points.Last<Point>();
            points.Add(GetPointPosition(startPoint, base.GetPoint(base.Segment.Point1)));
            points.Add(GetPointPosition(base.GetPoint(base.Segment.Point2), base.GetPoint(base.Segment.Point1)));
            points.Add(base.GetPoint(base.Segment.Point2));
            pointTypes.Add(PathPointType.Bezier);
            pointTypes.Add(PathPointType.Bezier);
            pointTypes.Add(PathPointType.Bezier);
        }

        public override Point GetLastPoint() => 
            base.Segment.Point2;

        internal static Point GetPointPosition(Point startPoint, Point endPoint) => 
            startPoint + (((endPoint - startPoint) * 2.0) / 3.0);
    }
}

