namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class LineSparklinePainter : BaseSparklinePainter
    {
        private void DrawBezierFigure(StreamGeometryContext ctx, PathFigure figure)
        {
            ctx.BeginFigure(figure.StartPoint, figure.IsFilled, figure.IsClosed);
            foreach (BezierSegment segment in figure.Segments.OfType<BezierSegment>())
            {
                ctx.BezierTo(segment.Point1, segment.Point2, segment.Point3, segment.IsStroked, segment.IsSmoothJoin);
            }
        }

        protected override void DrawInternal(DrawingContext drawingContext)
        {
            List<Point> points = new List<Point>();
            List<int> pointsIndexes = new List<int>();
            Point item = new Point();
            int num = -1;
            for (int i = 0; i < base.Data.Count; i++)
            {
                double num3 = base.Data[i].Value;
                if (!SparklineMathUtils.IsValidDouble(num3))
                {
                    this.DrawWholeGeometry(drawingContext, base.View.ActualBrush, points);
                    this.DrawMarkers(drawingContext, points, pointsIndexes);
                    points.Clear();
                    pointsIndexes.Clear();
                }
                else
                {
                    double x = base.Mapping.MapXValueToScreen(base.Data[i].Argument);
                    double y = base.Mapping.MapYValueToScreen(num3);
                    if (!base.Mapping.IsArgumentVisible(base.Data[i].Argument) && (points.Count == 0))
                    {
                        item = new Point(x, y);
                        num = i;
                    }
                    else
                    {
                        if (num >= 0)
                        {
                            points.Add(item);
                            pointsIndexes.Add(num);
                            num = -1;
                        }
                        points.Add(new Point(x, y));
                        pointsIndexes.Add(i);
                        if (!base.Mapping.IsArgumentVisible(base.Data[i].Argument))
                        {
                            break;
                        }
                    }
                }
            }
            this.DrawWholeGeometry(drawingContext, base.View.ActualBrush, points);
            this.DrawMarkers(drawingContext, points, pointsIndexes);
        }

        private void DrawMarkers(DrawingContext drawingContext, List<Point> points, List<int> pointsIndexes)
        {
            if (this.LineView.ActualShowMarkers)
            {
                PathGeometry geometry = new PathGeometry();
                float num = this.GetMarkerSize(BaseSparklinePainter.PointPresentationType.SimplePoint) * 0.5f;
                SolidColorBrush pointBrush = this.GetPointBrush(BaseSparklinePainter.PointPresentationType.SimplePoint);
                Pen pen = base.GetPen(pointBrush, 1);
                int num2 = 0;
                while (true)
                {
                    if (num2 >= points.Count)
                    {
                        drawingContext.DrawGeometry(pointBrush, pen, geometry);
                        break;
                    }
                    EllipseGeometry geometry2 = new EllipseGeometry(points[num2], (double) num, (double) num);
                    geometry.AddGeometry(geometry2);
                    num2++;
                }
            }
            for (int i = 0; i < points.Count; i++)
            {
                BaseSparklinePainter.PointPresentationType pointPresentationType = base.GetPointPresentationType(pointsIndexes[i]);
                if (pointPresentationType != BaseSparklinePainter.PointPresentationType.SimplePoint)
                {
                    float num5 = 0.5f * this.GetMarkerSize(pointPresentationType);
                    SolidColorBrush pointBrush = this.GetPointBrush(pointPresentationType);
                    drawingContext.DrawEllipse(pointBrush, base.GetPen(pointBrush, 1), points[i], (double) num5, (double) num5);
                }
            }
        }

        protected virtual void DrawWholeGeometry(DrawingContext drawingContext, SolidColorBrush brush, List<Point> points)
        {
            if (points.Count > 1)
            {
                Pen pen = base.GetPen(brush, this.LineView.ActualLineWidth);
                StreamGeometry geometry = new StreamGeometry();
                using (StreamGeometryContext context = geometry.Open())
                {
                    context.BeginFigure(points[0], false, false);
                    int x = (int) points[0].X;
                    double maxValue = double.MaxValue;
                    double minValue = double.MinValue;
                    for (int i = 1; i < points.Count; i++)
                    {
                        maxValue = Math.Min(maxValue, points[i].Y);
                        minValue = Math.Max(minValue, points[i].Y);
                        bool flag = i == (points.Count - 1);
                        if ((((int) points[i].X) != x) | flag)
                        {
                            x = (int) points[i].X;
                            if (flag)
                            {
                                maxValue = points[i].Y;
                            }
                            context.LineTo(new Point((double) x, maxValue), true, false);
                            if (maxValue != minValue)
                            {
                                Point point = points[i];
                                if (Math.Abs((double) (x - point.X)) < 2.0)
                                {
                                    context.LineTo(new Point((double) x, minValue), true, false);
                                }
                            }
                            maxValue = double.MaxValue;
                            minValue = double.MinValue;
                        }
                    }
                }
                geometry.Freeze();
                drawingContext.DrawGeometry(null, pen, geometry);
            }
        }

        private float GetMarkerSize(BaseSparklinePainter.PointPresentationType pointType)
        {
            switch (pointType)
            {
                case BaseSparklinePainter.PointPresentationType.HighPoint:
                    return (float) this.LineView.ActualMaxPointMarkerSize;

                case BaseSparklinePainter.PointPresentationType.LowPoint:
                    return (float) this.LineView.ActualMinPointMarkerSize;

                case BaseSparklinePainter.PointPresentationType.StartPoint:
                    return (float) this.LineView.ActualStartPointMarkerSize;

                case BaseSparklinePainter.PointPresentationType.EndPoint:
                    return (float) this.LineView.ActualEndPointMarkerSize;

                case BaseSparklinePainter.PointPresentationType.NegativePoint:
                    return (float) this.LineView.ActualNegativePointMarkerSize;
            }
            return (float) this.LineView.ActualMarkerSize;
        }

        protected override SolidColorBrush GetPointBrush(BaseSparklinePainter.PointPresentationType pointType) => 
            (pointType != BaseSparklinePainter.PointPresentationType.SimplePoint) ? base.GetPointBrush(pointType) : this.LineView.ActualMarkerBrush;

        private LineSparklineControl LineView =>
            (LineSparklineControl) base.View;

        protected override bool EnableAntialiasing =>
            true;

        public override SparklineViewType SparklineType =>
            SparklineViewType.Line;
    }
}

