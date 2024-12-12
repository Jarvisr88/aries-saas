namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class EmptyPainter : Painter
    {
        public override void DrawBrick(PrintingSystemBase ps, VisualBrick brick, Rectangle bounds)
        {
        }

        public override void DrawGrayedImage(OfficeImage img, Rectangle bounds)
        {
        }

        public override void DrawImage(OfficeImage img, Rectangle bounds)
        {
        }

        public override void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
        }

        public override void DrawLines(Pen pen, PointF[] points)
        {
        }

        public override void DrawRectangle(Pen pen, Rectangle bounds)
        {
        }

        public override void DrawSpacesString(string text, FontInfo fontInfo, Rectangle rectangle)
        {
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle rectangle)
        {
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle rectangle, StringFormat stringFormat, IWordBreakProvider wordBreakProvider)
        {
        }

        public override void DrawString(string text, Brush brush, Font font, float x, float y)
        {
        }

        public override void ExcludeCellBounds(Rectangle rect, Rectangle rowBounds)
        {
        }

        public override void FillEllipse(Brush brush, Rectangle bounds)
        {
        }

        public override void FillPolygon(Brush brush, PointF[] points)
        {
        }

        public override void FillRectangle(Brush brush, Rectangle bounds)
        {
        }

        public override void FillRectangle(Color color, Rectangle bounds)
        {
        }

        public override Brush GetBrush(Color color) => 
            Brushes.Black;

        public override Pen GetPen(Color color) => 
            new Pen(color);

        public override Pen GetPen(Color color, float thickness) => 
            new Pen(color, thickness);

        public override PointF GetSnappedPoint(PointF point) => 
            point;

        public override SizeF MeasureString(string text, Font font) => 
            SizeF.Empty;

        public override void PopPixelOffsetMode()
        {
        }

        public override void PopSmoothingMode()
        {
        }

        public override void PopTransform()
        {
        }

        public override void PushPixelOffsetMode(bool highQualtity)
        {
        }

        public override void PushRotationTransform(Point center, float angleInRadians)
        {
        }

        public override void PushSmoothingMode(bool highQuality)
        {
        }

        public override bool PushTransform(Matrix transformMatrix) => 
            true;

        public override void ReleaseBrush(Brush brush)
        {
        }

        public override void ReleasePen(Pen pen)
        {
        }

        public override void ResetCellBoundsClip()
        {
        }

        protected override void SetClipBounds(RectangleF bounds)
        {
        }

        public override void SnapHeights(float[] heights)
        {
        }

        public override void SnapWidths(float[] widths)
        {
        }

        protected override PointF[] TransformToLayoutUnits(PointF[] points) => 
            points;

        protected override PointF[] TransformToPixels(PointF[] points) => 
            points;

        protected override RectangleF RectangularClipBounds =>
            RectangleF.Empty;

        public override int DpiY =>
            0x60;
    }
}

