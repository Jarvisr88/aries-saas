namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class PathGradientBrushCalculator : PathGradientBrushCalculatorBase
    {
        private readonly PathGeometry geometry;

        public PathGradientBrushCalculator(PathGeometry geometry, Rect bounds, DrawingGradientFill fill, System.Windows.Media.GradientStopCollection stops, ShapePreset shapeType) : base(bounds, fill, stops, shapeType)
        {
            Guard.ArgumentNotNull(geometry, "geometry");
            this.geometry = geometry;
        }

        private void ConvertBezierCurveToTrapezes(Vector[] p, Point[] innerPoints)
        {
            int num2 = (int) Math.Max((double) 10.0, (double) (WpfDrawingShapeHelper.CalcBezierLength(p) / 20.0));
            Point point2 = new Point(p[0].X, p[0].Y);
            for (int i = 0; i < num2; i++)
            {
                Point point = WpfDrawingShapeHelper.CalcBezier(p, (i + 1.0) / ((double) num2));
                base.AddTrapeze(point2, point, innerPoints[0], innerPoints[1]);
                point2 = point;
            }
        }

        protected override PathGeometry CreateInnerGeometry(RectangleOffset tileRect, RectangleOffset fillRect)
        {
            PathGeometry geometry = this.geometry.Clone();
            Point[] innerPoints = new Point[] { base.Center, base.Center };
            PathFigureCollection figures = geometry.Figures;
            int num = figures.Count - 1;
            while (num >= 0)
            {
                PathFigure figure = figures[num];
                Point startPoint = figure.StartPoint;
                PathSegmentCollection segments = figure.Segments;
                int index = 0;
                while (true)
                {
                    Point point;
                    if (index >= segments.Count)
                    {
                        num--;
                        break;
                    }
                    LineSegment lineSegment = segments[index] as LineSegment;
                    if (lineSegment != null)
                    {
                        point = lineSegment.Point;
                        this.ProcessLineSegment(innerPoints, startPoint, point, figure, lineSegment, index);
                        startPoint = point;
                    }
                    else
                    {
                        BezierSegment bezierSegment = segments[index] as BezierSegment;
                        if (bezierSegment != null)
                        {
                            point = bezierSegment.Point3;
                            this.ProcessBezierSegment(innerPoints, startPoint, point, figure, bezierSegment, index);
                            startPoint = point;
                        }
                    }
                    index++;
                }
            }
            return geometry;
        }

        private void ProcessBezierSegment(Point[] innerPoints, Point start, Point end, PathFigure figure, BezierSegment bezierSegment, int index)
        {
            if (index == 0)
            {
                figure.StartPoint = innerPoints[0];
            }
            if (!start.Equals(end))
            {
                Vector[] p = WpfDrawingShapeHelper.GetBezierPoints(start, bezierSegment.Point1, bezierSegment.Point2, bezierSegment.Point3);
                this.ConvertBezierCurveToTrapezes(p, innerPoints);
            }
            bezierSegment.Point1 = innerPoints[0];
            bezierSegment.Point2 = innerPoints[0];
            bezierSegment.Point3 = innerPoints[1];
        }

        private void ProcessLineSegment(Point[] innerPoints, Point start, Point end, PathFigure figure, LineSegment lineSegment, int index)
        {
            if (index == 0)
            {
                figure.StartPoint = innerPoints[0];
            }
            lineSegment.Point = innerPoints[1];
            lineSegment.IsStroked = true;
            if (!start.Equals(end))
            {
                base.AddTrapeze(start, end, innerPoints[0], innerPoints[1]);
            }
        }
    }
}

