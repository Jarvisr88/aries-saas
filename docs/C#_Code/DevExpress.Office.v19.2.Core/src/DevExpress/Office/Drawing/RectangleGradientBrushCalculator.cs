namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class RectangleGradientBrushCalculator : PathGradientBrushCalculatorBase
    {
        public RectangleGradientBrushCalculator(Rect bounds, DrawingGradientFill fill, System.Windows.Media.GradientStopCollection stops, ShapePreset shapeType) : base(bounds, fill, stops, shapeType)
        {
        }

        protected override PathGeometry CreateInnerGeometry(RectangleOffset tileRect, RectangleOffset fillRect)
        {
            Point[] pointArray = this.OffsetPoints(tileRect, base.Bounds);
            Point[] pointArray2 = this.OffsetPoints(fillRect, base.Bounds);
            base.ChangeBounds(pointArray[0], pointArray[2]);
            base.AddTrapeze(pointArray[0], pointArray[1], pointArray2[0], pointArray2[1]);
            base.AddTrapeze(pointArray[1], pointArray[2], pointArray2[1], pointArray2[2]);
            base.AddTrapeze(pointArray[2], pointArray[3], pointArray2[2], pointArray2[3]);
            base.AddTrapeze(pointArray[3], pointArray[0], pointArray2[3], pointArray2[0]);
            PathGeometry geometry = new PathGeometry();
            PathFigure figure = new PathFigure {
                StartPoint = pointArray2[0]
            };
            base.AddLineSegment(figure, pointArray2[1]);
            base.AddLineSegment(figure, pointArray2[2]);
            base.AddLineSegment(figure, pointArray2[3]);
            base.AddLineSegment(figure, pointArray2[0]);
            geometry.Figures.Add(figure);
            return geometry;
        }

        private Point[] OffsetPoints(RectangleOffset offset, Rect bounds)
        {
            double num = DrawingValueConverter.FromPercentage(offset.LeftOffset);
            double num2 = DrawingValueConverter.FromPercentage(offset.RightOffset);
            double num3 = DrawingValueConverter.FromPercentage(offset.TopOffset);
            double num4 = DrawingValueConverter.FromPercentage(offset.BottomOffset);
            num2 = (num2 > (1.0 - num)) ? num : (1.0 - num2);
            num4 = (num4 > (1.0 - num3)) ? num3 : (1.0 - num4);
            double x = bounds.X + (bounds.Width * num);
            double num6 = bounds.X + (bounds.Width * num2);
            double y = bounds.Y + (bounds.Height * num3);
            double num8 = bounds.Y + (bounds.Height * num4);
            return new Point[] { new Point(x, y), new Point(num6, y), new Point(num6, num8), new Point(x, num8) };
        }
    }
}

