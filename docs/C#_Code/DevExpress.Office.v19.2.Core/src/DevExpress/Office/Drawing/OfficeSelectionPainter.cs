namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class OfficeSelectionPainter
    {
        protected static readonly Color HotZoneGradientColor = Color.FromArgb(0xff, 0xca, 0xea, 0xed);
        protected static readonly Color HotZoneRotationGradientColor = Color.FromArgb(0xff, 0x88, 0xe4, 0x3a);
        protected static readonly Color HotZoneBorderColor = Color.Gray;
        protected static readonly Color ShapeBorderColor = Color.FromArgb(0xff, 90, 0x93, 0xd3);
        protected static readonly int ShapeBorderWidth = 1;
        private readonly IGraphicsCache cache;
        private readonly Stack<Matrix> transformsStack = new Stack<Matrix>();

        protected OfficeSelectionPainter(IGraphicsCache cache)
        {
            Guard.ArgumentNotNull(cache, "cache");
            this.cache = cache;
        }

        protected internal virtual Brush CreateFillBrush(Rectangle bounds, Color color)
        {
            LinearGradientBrush brush = new LinearGradientBrush(bounds, Color.White, color, LinearGradientMode.Vertical);
            brush.SetSigmaBellShape(0.5f, 1f);
            return brush;
        }

        protected virtual void DrawActiveRectangle(Rectangle bounds)
        {
            this.DrawRectangle(bounds);
        }

        protected internal virtual void DrawEllipticHotZone(Rectangle bounds, Color color)
        {
            this.FillInOptionalQuality(delegate (System.Drawing.Graphics graphics) {
                graphics.FillEllipse(this.CreateFillBrush(bounds, color), bounds);
                graphics.DrawEllipse(this.cache.GetPen(HotZoneBorderColor), bounds);
            });
        }

        protected void DrawInHighQuality(Action<System.Drawing.Graphics> draw)
        {
            System.Drawing.Graphics graphics = this.Cache.Graphics;
            SmoothingMode smoothingMode = graphics.SmoothingMode;
            try
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                draw(graphics);
            }
            finally
            {
                graphics.SmoothingMode = smoothingMode;
            }
        }

        protected abstract void DrawLine(Point from, Point to);
        protected abstract void DrawRectangle(Rectangle bounds);
        protected internal virtual void DrawRectangularHotZone(Rectangle bounds, Color color)
        {
            this.FillInOptionalQuality(delegate (System.Drawing.Graphics graphics) {
                graphics.FillRectangle(this.CreateFillBrush(bounds, color), bounds);
                graphics.DrawRectangle(this.cache.GetPen(HotZoneBorderColor), bounds);
            });
        }

        protected void FillInOptionalQuality(Action<System.Drawing.Graphics> fill)
        {
            System.Drawing.Graphics graphics = this.Cache.Graphics;
            SmoothingMode smoothingMode = graphics.SmoothingMode;
            PixelOffsetMode pixelOffsetMode = graphics.PixelOffsetMode;
            try
            {
                graphics.SmoothingMode = this.FillSmoothingMode;
                graphics.PixelOffsetMode = this.FillPixelOffsetMode;
                fill(graphics);
            }
            finally
            {
                graphics.SmoothingMode = smoothingMode;
                graphics.PixelOffsetMode = pixelOffsetMode;
            }
        }

        protected abstract void FillRectangle(Rectangle bounds);
        public void PopTransform()
        {
            using (Matrix matrix = this.transformsStack.Pop())
            {
                this.Graphics.Transform = matrix;
            }
        }

        protected void PushFlipTransform(Rectangle bounds, float angleInDegrees, bool flipH, bool flipV)
        {
            this.transformsStack.Push(this.Graphics.Transform);
            using (Matrix matrix = TransformMatrixExtensions.CreateFlipTransformUnsafe(bounds, flipH, flipV, angleInDegrees))
            {
                this.Graphics.MultiplyTransform(matrix);
            }
        }

        public void PushRotationTransform(Point center, float angleInDegrees)
        {
            using (Matrix matrix = this.Graphics.Transform)
            {
                this.transformsStack.Push(matrix.Clone());
                matrix.RotateAt(angleInDegrees, (PointF) center);
                this.Graphics.Transform = matrix;
            }
        }

        public bool TryPushFlipTransform(Rectangle bounds, float angleInDegrees, bool flipH, bool flipV)
        {
            if (((angleInDegrees % 360f) == 0f) && (!flipH && !flipV))
            {
                return false;
            }
            this.PushFlipTransform(bounds, angleInDegrees, flipH, flipV);
            return true;
        }

        public bool TryPushRotationTransform(Point center, float angleInDegrees)
        {
            if ((angleInDegrees % 360f) == 0f)
            {
                return false;
            }
            this.PushRotationTransform(center, angleInDegrees);
            return true;
        }

        public IGraphicsCache Cache =>
            this.cache;

        public System.Drawing.Graphics Graphics =>
            this.cache.Graphics;

        public virtual SmoothingMode FillSmoothingMode =>
            SmoothingMode.HighQuality;

        public virtual PixelOffsetMode FillPixelOffsetMode =>
            PixelOffsetMode.HighQuality;
    }
}

