namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class Painter : IPatternLinePaintingSupport, IDisposable
    {
        private bool isDisposed;
        private Color textForeColor;
        private bool allowChangeTextForeColor = true;

        protected Painter()
        {
        }

        public virtual RectangleF ApplyClipBounds(RectangleF clipBounds)
        {
            RectangleF ef = this.ClipBounds;
            this.SetClipBounds(clipBounds);
            return ef;
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.isDisposed = true;
        }

        public abstract void DrawBrick(PrintingSystemBase ps, VisualBrick brick, Rectangle bounds);
        public virtual void DrawEllipse(Pen pen, Rectangle bounds)
        {
        }

        public virtual void DrawGrayedImage(OfficeImage img, Rectangle bounds)
        {
            this.DrawImage(img, bounds);
            this.GrayContent(bounds);
        }

        public abstract void DrawImage(OfficeImage img, Rectangle bounds);
        public virtual void DrawImage(OfficeImage img, Rectangle bounds, Size imgActualSizeInLayoutUnits, ImageSizeMode sizing)
        {
        }

        public virtual void DrawImageWithAdorner(OfficeImage img, Rectangle bounds, Action<RectangleF> drawAdorner)
        {
            this.DrawImage(img, bounds);
            if (drawAdorner != null)
            {
                drawAdorner(bounds);
            }
        }

        public abstract void DrawLine(Pen pen, float x1, float y1, float x2, float y2);
        public abstract void DrawLines(Pen pen, PointF[] points);
        public abstract void DrawRectangle(Pen pen, Rectangle bounds);
        public abstract void DrawSpacesString(string text, FontInfo fontInfo, Rectangle rectangle);
        public virtual void DrawSpacesString(string text, FontInfo fontInfo, Color foreColor, Rectangle rectangle)
        {
            this.TextForeColor = foreColor;
            this.DrawSpacesString(text, fontInfo, rectangle);
        }

        public abstract void DrawString(string text, FontInfo fontInfo, Rectangle rectangle);
        public void DrawString(string text, FontInfo fontInfo, Color foreColor, Rectangle rectangle)
        {
            this.TextForeColor = foreColor;
            this.DrawString(text, fontInfo, rectangle);
        }

        public abstract void DrawString(string text, FontInfo fontInfo, Rectangle rectangle, StringFormat stringFormat, IWordBreakProvider wordBreakProvider);
        public abstract void DrawString(string text, Brush brush, Font font, float x, float y);
        public void DrawString(string text, FontInfo fontInfo, Color foreColor, Rectangle rectangle, StringFormat stringFormat, IWordBreakProvider workBreakProvider)
        {
            this.TextForeColor = foreColor;
            this.DrawString(text, fontInfo, rectangle, stringFormat, workBreakProvider);
        }

        public abstract void ExcludeCellBounds(Rectangle rect, Rectangle rowBounds);
        public abstract void FillEllipse(Brush brush, Rectangle bounds);
        public virtual void FillEllipse(Brush brush, RectangleF bounds)
        {
            this.FillEllipse(brush, RectFToRect(bounds));
        }

        public virtual void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            this.FillEllipse(brush, new Rectangle((int) x, (int) y, (int) width, (int) height));
        }

        public abstract void FillPolygon(Brush brush, PointF[] points);
        public abstract void FillRectangle(Brush brush, Rectangle bounds);
        public virtual void FillRectangle(Brush brush, RectangleF bounds)
        {
            this.FillRectangle(brush, RectFToRect(bounds));
        }

        public abstract void FillRectangle(Color color, Rectangle bounds);
        public virtual void FillRectangle(Color color, RectangleF bounds)
        {
            this.FillRectangle(color, RectFToRect(bounds));
        }

        public virtual void FinishPaint()
        {
        }

        public abstract Brush GetBrush(Color color);
        public abstract Pen GetPen(Color color);
        public abstract Pen GetPen(Color color, float thickness);
        public virtual PointF GetSnappedPoint(PointF point)
        {
            PointF[] points = new PointF[] { point };
            this.TransformToPixels(points);
            points[0].X = (int) Math.Round((double) points[0].X);
            points[0].Y = (int) Math.Round((double) points[0].Y);
            this.TransformToLayoutUnits(points);
            return points[0];
        }

        public void GrayContent(Rectangle bounds)
        {
            this.FillRectangle(DXColor.FromArgb(0x80, 0xff, 0xff, 0xff), bounds);
        }

        public void GrayContent(RectangleF bounds)
        {
            this.GrayContent(RectFToRect(bounds));
        }

        public abstract SizeF MeasureString(string text, Font font);
        public abstract void PopPixelOffsetMode();
        public abstract void PopSmoothingMode();
        public abstract void PopTransform();
        public abstract void PushPixelOffsetMode(bool highQualtity);
        public abstract void PushRotationTransform(Point center, float angleInDegrees);
        public abstract void PushSmoothingMode(bool highQuality);
        public abstract bool PushTransform(Matrix transformMatrix);
        private static Rectangle RectFToRect(RectangleF bounds) => 
            new Rectangle((int) bounds.X, (int) bounds.Y, (int) bounds.Width, (int) bounds.Height);

        public abstract void ReleaseBrush(Brush brush);
        public abstract void ReleasePen(Pen pen);
        public abstract void ResetCellBoundsClip();
        public virtual void RestoreClipBounds(RectangleF clipBounds)
        {
            this.SetClipBounds(clipBounds);
        }

        protected abstract void SetClipBounds(RectangleF bounds);
        public virtual void SetUriArea(string uri, RectangleF bounds)
        {
        }

        public virtual void SnapHeights(float[] heights)
        {
            PointF[] points = new PointF[] { new PointF(0f, 0f) };
            for (int i = 0; i < heights.Length; i++)
            {
                points[i + 1] = new PointF(0f, heights[i]);
            }
            this.TransformToPixels(points);
            for (int j = heights.Length; j > 0; j--)
            {
                points[j].Y = (heights[j - 1] != 0f) ? ((float) Math.Max((int) ((points[j].Y - points[0].Y) + 0.5), 1)) : ((float) 0);
            }
            points[0].Y = 0f;
            this.TransformToLayoutUnits(points);
            for (int k = heights.Length; k > 0; k--)
            {
                heights[k - 1] = points[k].Y - points[0].Y;
            }
        }

        public virtual void SnapWidths(float[] widths)
        {
            PointF[] points = new PointF[] { new PointF(0f, 0f) };
            for (int i = 0; i < widths.Length; i++)
            {
                points[i + 1] = new PointF(widths[i], 0f);
            }
            this.TransformToPixels(points);
            for (int j = widths.Length; j > 0; j--)
            {
                points[j].X = (widths[j - 1] != 0f) ? ((float) Math.Max((int) ((points[j].X - points[0].X) + 0.5), 1)) : ((float) 0);
            }
            points[0].X = 0f;
            this.TransformToLayoutUnits(points);
            for (int k = widths.Length; k > 0; k--)
            {
                widths[k - 1] = points[k].X - points[0].X;
            }
        }

        protected abstract PointF[] TransformToLayoutUnits(PointF[] points);
        protected abstract PointF[] TransformToPixels(PointF[] points);
        public bool TryPushRotationTransform(Point center, float angleInDegrees)
        {
            if ((angleInDegrees % 360f) == 0f)
            {
                return false;
            }
            this.PushRotationTransform(center, angleInDegrees);
            return true;
        }

        public RectangleF ClipBounds
        {
            get => 
                this.RectangularClipBounds;
            set => 
                this.SetClipBounds(value);
        }

        public virtual bool HyperlinksSupported =>
            false;

        protected abstract RectangleF RectangularClipBounds { get; }

        public abstract int DpiY { get; }

        public Color TextForeColor
        {
            get => 
                this.textForeColor;
            set
            {
                if (this.allowChangeTextForeColor)
                {
                    this.textForeColor = value;
                }
            }
        }

        public bool AllowChangeTextForeColor
        {
            get => 
                this.allowChangeTextForeColor;
            set => 
                this.allowChangeTextForeColor = value;
        }

        public bool IsDisposed =>
            this.isDisposed;
    }
}

