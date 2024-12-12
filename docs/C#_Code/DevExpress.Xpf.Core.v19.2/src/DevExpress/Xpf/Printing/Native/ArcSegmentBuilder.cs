namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Media;

    public class ArcSegmentBuilder : PathSegmentBuilder<ArcSegment>
    {
        internal ArcSegmentBuilder(ArcSegment segment, System.Windows.Media.Matrix transform) : base(segment, transform)
        {
        }

        [SecurityCritical, DllImport("wpfgfx_v0400.dll", EntryPoint="MilUtility_ArcToBezier")]
        private static extern void ConvertArcSegmentToBezier(Point startPoint, Size radius, double angle, bool isLargeArc, SweepDirection sweepDirection, Point endPoint, ref System.Windows.Media.Matrix transform, ref Point points, out int segmentCount);
        public override void GenerateData(List<Point> points, List<PathPointType> pointTypes)
        {
            this.GenerateDataInternal(points, pointTypes);
        }

        [SecuritySafeCritical]
        private void GenerateDataInternal(List<Point> points, List<PathPointType> pointTypes)
        {
            int num;
            Point[] pointArray = new Point[12];
            System.Windows.Media.Matrix identity = System.Windows.Media.Matrix.Identity;
            ConvertArcSegmentToBezier(points.Last<Point>(), base.Segment.Size, base.Segment.RotationAngle, base.Segment.IsLargeArc, base.Segment.SweepDirection, base.GetPoint(base.Segment.Point), ref identity, ref pointArray[0], out num);
            if (num <= 0)
            {
                if (num == 0)
                {
                    points.Add(pointArray[0]);
                    pointTypes.Add(PathPointType.Line);
                }
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    points.Add(pointArray[3 * i]);
                    points.Add(pointArray[(3 * i) + 1]);
                    points.Add(pointArray[(3 * i) + 2]);
                    pointTypes.Add(PathPointType.Bezier);
                    pointTypes.Add(PathPointType.Bezier);
                    pointTypes.Add(PathPointType.Bezier);
                }
            }
        }

        public override Point GetLastPoint() => 
            base.Segment.Point;
    }
}

