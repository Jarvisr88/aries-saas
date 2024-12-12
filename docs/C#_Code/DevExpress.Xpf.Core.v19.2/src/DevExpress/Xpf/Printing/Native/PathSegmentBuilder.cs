namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    public abstract class PathSegmentBuilder
    {
        protected PathSegmentBuilder()
        {
        }

        public static PathSegmentBuilder CreateSegmentBuilder(PathSegment segment, Matrix transform)
        {
            switch (segment)
            {
                case (LineSegment _):
                    return new LineSegmentBuilder((LineSegment) segment, transform);
                    break;
            }
            if (segment is PolyLineSegment)
            {
                return new PolylineSegmentBuilder((PolyLineSegment) segment, transform);
            }
            if (segment is BezierSegment)
            {
                return new BezierSegmentBuilder((BezierSegment) segment, transform);
            }
            if (segment is PolyBezierSegment)
            {
                return new PolyBezierSegmentBuilder((PolyBezierSegment) segment, transform);
            }
            if (segment is QuadraticBezierSegment)
            {
                return new QuadraticBezierSegmentBuilder((QuadraticBezierSegment) segment, transform);
            }
            if (segment is PolyQuadraticBezierSegment)
            {
                return new PolyQuadraticBezierSegmentBuilder((PolyQuadraticBezierSegment) segment, transform);
            }
            if (!(segment is ArcSegment))
            {
                throw new Exception("Unknow segment");
            }
            return new ArcSegmentBuilder((ArcSegment) segment, transform);
        }

        public abstract void GenerateData(List<Point> points, List<PathPointType> pointTypes);
        public abstract Point GetLastPoint();
    }
}

