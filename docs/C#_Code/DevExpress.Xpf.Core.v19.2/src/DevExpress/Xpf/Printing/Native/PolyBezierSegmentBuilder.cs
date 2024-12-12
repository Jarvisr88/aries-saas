namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class PolyBezierSegmentBuilder : PathSegmentBuilder<PolyBezierSegment>
    {
        internal PolyBezierSegmentBuilder(PolyBezierSegment segment, System.Windows.Media.Matrix transform) : base(segment, transform)
        {
        }

        public override void GenerateData(List<Point> points, List<PathPointType> pointTypes)
        {
            foreach (Point point in base.Segment.Points)
            {
                points.Add(base.GetPoint(point));
                pointTypes.Add(PathPointType.Bezier);
            }
        }

        public override Point GetLastPoint() => 
            base.Segment.Points.Last<Point>();
    }
}

