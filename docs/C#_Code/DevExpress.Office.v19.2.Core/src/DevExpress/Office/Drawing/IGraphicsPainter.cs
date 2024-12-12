namespace DevExpress.Office.Drawing
{
    using DevExpress.Data.Utils;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class IGraphicsPainter : GdiPlusPainterBase
    {
        private readonly IGraphicsBase graphics;
        private int transformLevel;
        private RectangleF actualClipBounds;

        public IGraphicsPainter(IGraphicsBase graphics)
        {
            Guard.ArgumentNotNull(graphics, "graphics");
            this.graphics = graphics;
            this.actualClipBounds = this.IGraphics.ClipBounds;
        }

        public override void DrawBrick(PrintingSystemBase ps, VisualBrick brick, Rectangle bounds)
        {
            VisualBrickExporter exporter = (VisualBrickExporter) ExportersFactory.CreateExporter(brick);
            DevExpress.XtraPrinting.IGraphics iGraphics = this.IGraphics as DevExpress.XtraPrinting.IGraphics;
            if (iGraphics != null)
            {
                exporter.Draw(iGraphics, bounds, bounds);
            }
        }

        public override void DrawImage(OfficeImage img, Rectangle bounds)
        {
            this.IGraphics.DrawImage(img.NativeImage, bounds);
        }

        public override void DrawImage(OfficeImage img, Rectangle bounds, Size imgActualSizeInLayoutUnits, ImageSizeMode sizing)
        {
            RectangleF clipBounds = this.IGraphics.ClipBounds;
            Rectangle rectangle = Rectangle.Round(ImageTool.CalculateImageRectCore(bounds, (SizeF) imgActualSizeInLayoutUnits, sizing));
            GdiGraphics iGraphics = this.IGraphics as GdiGraphics;
            if (iGraphics == null)
            {
                this.IGraphics.ClipBounds = Rectangle.Intersect(bounds, Rectangle.Round(clipBounds));
            }
            else
            {
                RectangleF a = bounds;
                PointF[] pts = new PointF[] { (PointF) bounds.Location };
                iGraphics.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World, pts);
                pts[0] = new PointF((float) Math.Ceiling((double) pts[0].X), (float) Math.Ceiling((double) pts[0].Y));
                iGraphics.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);
                a.Location = pts[0];
                this.IGraphics.ClipBounds = RectangleF.Intersect(a, clipBounds);
                rectangle = Rectangle.Round(ImageTool.CalculateImageRectCore(a, (SizeF) imgActualSizeInLayoutUnits, sizing));
            }
            this.DrawImage(img, rectangle);
            this.IGraphics.ClipBounds = clipBounds;
        }

        public override void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            this.IGraphics.DrawLine(pen, x1, y1, x2, y2);
        }

        public override void DrawLines(Pen pen, PointF[] points)
        {
            this.IGraphics.DrawLines(pen, points);
        }

        public override void DrawRectangle(Pen pen, Rectangle bounds)
        {
            this.IGraphics.DrawRectangle(pen, bounds);
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle rectangle)
        {
            this.DrawStringCore(text, fontInfo, rectangle, base.StringFormat);
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle rectangle, StringFormat stringFormat, IWordBreakProvider workBreakProvider)
        {
            this.DrawStringCore(text, fontInfo, rectangle, stringFormat);
        }

        public override void DrawString(string text, Brush brush, Font font, float x, float y)
        {
            this.IGraphics.DrawString(text, font, brush, new PointF(x, y), base.StringFormat);
        }

        private void DrawStringCore(string text, FontInfo fontInfo, Rectangle rectangle, StringFormat stringFormat)
        {
            Brush brush = this.GetBrush(base.TextForeColor);
            this.IGraphics.DrawString(text, fontInfo.Font, brush, this.CorrectTextDrawingBounds(fontInfo, rectangle), stringFormat);
            this.ReleaseBrush(brush);
        }

        public override void ExcludeCellBounds(Rectangle rect, Rectangle rowBounds)
        {
        }

        public override void FillEllipse(Brush brush, Rectangle bounds)
        {
            this.IGraphics.FillEllipse(brush, bounds);
        }

        public override void FillPolygon(Brush brush, PointF[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);
            this.IGraphics.FillPath(brush, path);
        }

        public override void FillRectangle(Brush brush, Rectangle bounds)
        {
            this.IGraphics.FillRectangle(brush, bounds);
        }

        public override void FillRectangle(Brush brush, RectangleF bounds)
        {
            this.IGraphics.FillRectangle(brush, bounds);
        }

        public override void FillRectangle(Color color, Rectangle bounds)
        {
            Brush brush = this.GetBrush(color);
            this.IGraphics.FillRectangle(brush, bounds);
            this.ReleaseBrush(brush);
        }

        public override void FillRectangle(Color color, RectangleF bounds)
        {
            Brush brush = this.GetBrush(color);
            this.IGraphics.FillRectangle(brush, bounds);
            this.ReleaseBrush(brush);
        }

        public override Brush GetBrush(Color color) => 
            new SolidBrush(color);

        public override Pen GetPen(Color color) => 
            new Pen(color);

        public override Pen GetPen(Color color, float thickness) => 
            new Pen(color, thickness);

        public override PointF GetSnappedPoint(PointF point) => 
            point;

        public override SizeF MeasureString(string text, Font font) => 
            this.IGraphics.MeasureString(text, font, float.MaxValue, base.StringFormat, GraphicsUnit.Pixel);

        public override void PopPixelOffsetMode()
        {
        }

        public override void PopSmoothingMode()
        {
        }

        public override void PopTransform()
        {
            this.transformLevel--;
            this.IGraphics.ResetTransform();
            this.IGraphics.ApplyTransformState(MatrixOrder.Prepend, true);
        }

        public override void PushPixelOffsetMode(bool highQualtity)
        {
        }

        public override void PushRotationTransform(Point center, float angleInDegrees)
        {
            this.transformLevel++;
            this.IGraphics.SaveTransformState();
            this.IGraphics.ResetTransform();
            this.IGraphics.ApplyTransformState(MatrixOrder.Append, false);
            this.IGraphics.TranslateTransform((float) center.X, (float) center.Y);
            this.IGraphics.RotateTransform(angleInDegrees);
            this.IGraphics.TranslateTransform((float) -center.X, (float) -center.Y);
        }

        public override void PushSmoothingMode(bool highQuality)
        {
        }

        public override bool PushTransform(Matrix transformMatrix)
        {
            this.transformLevel++;
            this.IGraphics.SaveTransformState();
            this.IGraphics.ResetTransform();
            this.IGraphics.ApplyTransformState(MatrixOrder.Append, false);
            this.IGraphics.Transform = transformMatrix;
            return true;
        }

        public override void ReleaseBrush(Brush brush)
        {
            brush.Dispose();
        }

        public override void ReleasePen(Pen pen)
        {
            pen.Dispose();
        }

        public override void ResetCellBoundsClip()
        {
        }

        protected internal void SetActualClipBounds(RectangleF bounds)
        {
            this.actualClipBounds = bounds;
        }

        protected override void SetClipBounds(RectangleF bounds)
        {
            this.IGraphics.ClipBounds = bounds;
            this.SetActualClipBounds(this.IGraphics.ClipBounds);
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

        public IGraphicsBase IGraphics =>
            this.graphics;

        protected override RectangleF RectangularClipBounds =>
            this.actualClipBounds;

        public bool HasTransform =>
            this.transformLevel != 0;

        public override int DpiY =>
            300;
    }
}

