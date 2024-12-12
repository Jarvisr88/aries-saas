namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Windows;
    using System.Windows.Media;

    public class BezierSegmentBuilder : PathSegmentBuilder<BezierSegment>
    {
        internal BezierSegmentBuilder(BezierSegment segment, System.Windows.Media.Matrix transform) : base(segment, transform)
        {
        }

        public override void GenerateData(List<Point> points, List<PathPointType> pointTypes)
        {
            points.Add(base.GetPoint(base.Segment.Point1));
            points.Add(base.GetPoint(base.Segment.Point2));
            points.Add(base.GetPoint(base.Segment.Point3));
            pointTypes.Add(PathPointType.Bezier);
            pointTypes.Add(PathPointType.Bezier);
            pointTypes.Add(PathPointType.Bezier);
        }

        public override Point GetLastPoint() => 
            base.Segment.Point3;
    }
}

