namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Windows;
    using System.Windows.Media;

    public class LineSegmentBuilder : PathSegmentBuilder<LineSegment>
    {
        internal LineSegmentBuilder(LineSegment segment, System.Windows.Media.Matrix transform) : base(segment, transform)
        {
        }

        public override void GenerateData(List<Point> points, List<PathPointType> pointTypes)
        {
            points.Add(base.GetPoint(base.Segment.Point));
            pointTypes.Add(PathPointType.Line);
        }

        public override Point GetLastPoint() => 
            base.Segment.Point;
    }
}

