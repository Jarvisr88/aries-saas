namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    public class AreaSparklinePainter : LineSparklinePainter
    {
        protected override void DrawWholeGeometry(DrawingContext drawingContext, SolidColorBrush brush, List<Point> points)
        {
            if (points.Count != 0)
            {
                Color color = Color.FromArgb(this.GetOpacity(), brush.Color.R, brush.Color.G, brush.Color.B);
                if (points.Count == 1)
                {
                    drawingContext.DrawLine(base.GetPen(color, this.AreaView.ActualLineWidth), points[0], new Point(points[0].X, base.Mapping.ScreenYZeroValue));
                }
                else
                {
                    if (points.Count > 1)
                    {
                        SolidColorBrush solidBrush = base.GetSolidBrush(color);
                        Pen pen = base.GetPen(color, 1);
                        double y = Math.Round(base.Mapping.ScreenYZeroValue) + 1.0;
                        double num2 = 1.0;
                        List<LineSegment> list = new List<LineSegment>();
                        double x = Math.Round(points[0].X) - num2;
                        list.Add(new LineSegment(new Point(x, y), false));
                        list.Add(new LineSegment(new Point(x, points[0].Y), false));
                        int num5 = 0;
                        while (true)
                        {
                            if (num5 >= points.Count)
                            {
                                double num4 = Math.Round(points[points.Count - 1].X) + num2;
                                list.Add(new LineSegment(new Point(num4, points[points.Count - 1].Y), false));
                                list.Add(new LineSegment(new Point(num4, y), false));
                                list.Add(new LineSegment(new Point(x, y), false));
                                PathGeometry geometry = new PathGeometry();
                                PathFigure figure1 = new PathFigure(new Point(x, y), (IEnumerable<PathSegment>) list, true);
                                figure1.IsFilled = true;
                                geometry.Figures.Add(figure1);
                                drawingContext.DrawGeometry(solidBrush, pen, geometry);
                                break;
                            }
                            list.Add(new LineSegment(points[num5], false));
                            num5++;
                        }
                    }
                    base.DrawWholeGeometry(drawingContext, brush, points);
                }
            }
        }

        private byte GetOpacity() => 
            (this.AreaView.ActualAreaOpacity <= 1.0) ? ((this.AreaView.ActualAreaOpacity >= 0.0) ? Convert.ToByte((double) (this.AreaView.ActualAreaOpacity * 255.0)) : 0) : 0xff;

        private AreaSparklineControl AreaView =>
            (AreaSparklineControl) base.View;

        public override SparklineViewType SparklineType =>
            SparklineViewType.Area;
    }
}

