namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public abstract class PathGradientBrushCalculatorBase
    {
        private readonly System.Windows.Media.GradientStopCollection stops;
        private readonly DrawingGradientFill fill;
        private readonly List<Trapeze> trapezes;
        private Rect bounds;
        private Point center;
        private const float eps = 1E-12f;

        public PathGradientBrushCalculatorBase(Rect bounds, DrawingGradientFill fill, System.Windows.Media.GradientStopCollection stops, ShapePreset shapeType)
        {
            Guard.ArgumentNotNull(fill, "fill");
            Guard.ArgumentNotNull(stops, "stops");
            this.bounds = bounds;
            this.fill = fill;
            this.stops = stops;
            this.trapezes = new List<Trapeze>();
            this.center = this.GetCenter(shapeType);
        }

        protected void AddLineSegment(PathFigure figure, Point point)
        {
            figure.Segments.Add(new LineSegment(point, true));
        }

        protected void AddTrapeze(Point outerPoint1, Point outerPoint2, Point innerPoint1, Point innerPoint2)
        {
            this.trapezes.Add(new Trapeze(outerPoint1, outerPoint2, innerPoint1, innerPoint2));
        }

        protected void ChangeBounds(Point topLeft, Point bottomRight)
        {
            this.bounds = new Rect(topLeft, bottomRight);
        }

        public Brush CreateBrush()
        {
            PathGeometry innerGeometry = this.CreateInnerGeometry(this.fill.TileRect, this.fill.FillRect);
            if ((this.bounds.Width == 0.0) || (this.bounds.Height == 0.0))
            {
                return this.CreateFirstColorBrush();
            }
            DrawingVisual tile = new DrawingVisual {
                Clip = new RectangleGeometry(this.bounds)
            };
            this.DrawTile(tile, innerGeometry);
            return new VisualBrush(tile) { 
                Stretch = Stretch.None,
                TileMode = TileMode.FlipXY,
                Viewport = this.bounds,
                ViewportUnits = BrushMappingMode.Absolute
            };
        }

        protected SolidColorBrush CreateFirstColorBrush() => 
            new SolidColorBrush(this.stops[0].Color);

        protected abstract PathGeometry CreateInnerGeometry(RectangleOffset tileRect, RectangleOffset fillRect);
        private LinearGradientBrush CreateLinearGradientBrush(Point gradStartPoint, Point gradEndPoint)
        {
            LinearGradientBrush brush = new LinearGradientBrush(this.stops) {
                MappingMode = BrushMappingMode.Absolute,
                StartPoint = gradStartPoint,
                EndPoint = gradEndPoint
            };
            brush.Freeze();
            return brush;
        }

        private PathGeometry CreateTrapezeGeometry(Trapeze trapeze)
        {
            PathGeometry geometry = new PathGeometry();
            PathFigure figure = new PathFigure {
                StartPoint = trapeze.OuterPoint1
            };
            this.AddLineSegment(figure, trapeze.InnerPoint1);
            this.AddLineSegment(figure, trapeze.InnerPoint2);
            this.AddLineSegment(figure, trapeze.OuterPoint2);
            this.AddLineSegment(figure, trapeze.OuterPoint1);
            geometry.Figures.Add(figure);
            return geometry;
        }

        private void DrawBorder(DrawingContext dc, Trapeze trapeze)
        {
            Pen pen = new Pen(this.CreateLinearGradientBrush(trapeze.InnerPoint1, trapeze.OuterPoint1), 5.0);
            pen.Freeze();
            dc.DrawLine(pen, trapeze.InnerPoint1, trapeze.OuterPoint1);
        }

        private void DrawInnerGeometry(DrawingContext dc, PathGeometry innerGeometry)
        {
            Brush brush = this.CreateFirstColorBrush();
            brush.Freeze();
            Pen pen = new Pen(brush, 1.0);
            pen.Freeze();
            dc.DrawGeometry(brush, pen, innerGeometry);
        }

        private void DrawTile(DrawingVisual tile, PathGeometry innerGeometry)
        {
            using (DrawingContext context = tile.RenderOpen())
            {
                foreach (Trapeze trapeze in this.trapezes)
                {
                    this.DrawBorder(context, trapeze);
                }
                foreach (Trapeze trapeze2 in this.trapezes)
                {
                    this.DrawTrapeze(context, trapeze2);
                }
                this.DrawInnerGeometry(context, innerGeometry);
            }
        }

        private void DrawTrapeze(DrawingContext dc, Trapeze trapeze)
        {
            PathGeometry geometry = this.CreateTrapezeGeometry(trapeze);
            Point[] gradientPoints = trapeze.GetGradientPoints();
            LinearGradientBrush brush = this.CreateLinearGradientBrush(gradientPoints[0], gradientPoints[1]);
            dc.DrawGeometry(brush, null, geometry);
        }

        private Point GetCenter(ShapePreset shapeType)
        {
            double num;
            double num2;
            if (shapeType == ShapePreset.Arc)
            {
                num = (3.0 * this.Bounds.Width) / 4.0;
                num2 = this.Bounds.Height / 2.0;
            }
            else
            {
                num = this.Bounds.Width / 2.0;
                num2 = this.Bounds.Height / 2.0;
            }
            return new Point(num, num2);
        }

        protected Rect Bounds =>
            this.bounds;

        protected DrawingGradientFill Fill =>
            this.fill;

        protected Point Center =>
            this.center;

        [StructLayout(LayoutKind.Sequential)]
        protected struct Trapeze
        {
            public Trapeze(Point outerPoint1, Point outerPoint2, Point innerPoint1, Point innerPoint2)
            {
                this = new PathGradientBrushCalculatorBase.Trapeze();
                this.OuterPoint1 = outerPoint1;
                this.OuterPoint2 = outerPoint2;
                this.InnerPoint1 = innerPoint1;
                this.InnerPoint2 = innerPoint2;
            }

            public Point OuterPoint1 { get; private set; }
            public Point OuterPoint2 { get; private set; }
            public Point InnerPoint1 { get; private set; }
            public Point InnerPoint2 { get; private set; }
            public Point[] GetGradientPoints()
            {
                Point point = this.CalculateNormalPoint(this.InnerPoint1, this.OuterPoint1.X, this.OuterPoint1.Y, this.OuterPoint2.X, this.OuterPoint2.Y);
                return new Point[] { this.InnerPoint1, point };
            }

            private Point CalculateNormalPoint(Point center, double x1, double y1, double x2, double y2)
            {
                double x;
                double y;
                if (Math.Abs((double) (x2 - x1)) < 9.999999960041972E-13)
                {
                    x = x1;
                    y = center.Y;
                }
                else if (Math.Abs((double) (y2 - y1)) < 9.999999960041972E-13)
                {
                    x = center.X;
                    y = y1;
                }
                else
                {
                    double num3 = (y2 - y1) / (x2 - x1);
                    double num5 = -1.0 / num3;
                    double num6 = center.Y - (num5 * center.X);
                    x = ((y2 - (num3 * x2)) - num6) / (num5 - num3);
                    y = (num5 * x) + num6;
                }
                return new Point(x, y);
            }
        }
    }
}

