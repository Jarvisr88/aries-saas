namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public abstract class PatternLinePainter
    {
        private readonly IPatternLinePaintingSupport painter;
        private readonly DocumentLayoutUnitConverter unitConverter;

        protected PatternLinePainter(IPatternLinePaintingSupport painter, DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(painter, "painter");
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.painter = painter;
            this.unitConverter = unitConverter;
            this.UseFixedWidthPattern = true;
        }

        public void DrawDoubleSolidLine(RectangleF bounds, Color color)
        {
            bounds = this.RotateBounds(bounds);
            float height = this.RoundToPixels(bounds.Height / 4f, this.PixelGraphics.DpiY);
            if (height <= this.PixelsToUnits(1f, this.PixelGraphics.DpiY))
            {
                height = this.PixelsToUnits(1f, this.PixelGraphics.DpiY);
            }
            float num2 = height / 2f;
            RectangleF ef = new RectangleF(bounds.X, bounds.Y + num2, bounds.Width, height);
            RectangleF ef2 = new RectangleF(bounds.X, bounds.Bottom - num2, bounds.Width, height);
            float num3 = this.RoundToPixels(ef2.Y - ef.Bottom, this.PixelGraphics.DpiY);
            if (num3 <= this.PixelsToUnits(1f, this.PixelGraphics.DpiY))
            {
                num3 = this.PixelsToUnits(1f, this.PixelGraphics.DpiY);
            }
            ef2.Y = ef.Bottom + num3;
            this.DrawSolidLine(this.RotateBounds(ef), color);
            this.DrawSolidLine(this.RotateBounds(ef2), color);
        }

        protected internal void DrawDoubleSolidLine(PointF from, PointF to, Color color, float thickness)
        {
            thickness /= 3f;
            if (thickness <= this.PixelsToUnits(1f, this.PixelGraphics.DpiY))
            {
                thickness = this.PixelsToUnits(1f, this.PixelGraphics.DpiY);
            }
            double num = from.X - to.X;
            double num2 = from.Y - to.Y;
            double num3 = Math.Sqrt((num * num) + (num2 * num2));
            if (num3 != 0.0)
            {
                double num4 = num / num3;
                double num5 = num2 / num3;
                PointF tf = new PointF(from.X - ((float) (thickness * num5)), from.Y + ((float) (thickness * num4)));
                PointF tf2 = new PointF(to.X - ((float) (thickness * num5)), to.Y + ((float) (thickness * num4)));
                this.DrawSolidLine(tf, tf2, color, thickness);
                PointF tf3 = new PointF(from.X + ((float) (thickness * num5)), from.Y - ((float) (thickness * num4)));
                PointF tf4 = new PointF(to.X + ((float) (thickness * num5)), to.Y - ((float) (thickness * num4)));
                this.DrawSolidLine(tf3, tf4, color, thickness);
            }
        }

        protected virtual void DrawLine(Pen pen, RectangleF bounds)
        {
            this.Painter.DrawLine(pen, bounds.X, bounds.Y, bounds.Right, bounds.Y);
        }

        protected virtual void DrawLine(Pen pen, PointF from, PointF to)
        {
            this.Painter.DrawLine(pen, from.X, from.Y, to.X, to.Y);
        }

        public void DrawPatternLine(RectangleF bounds, Color color, float[] pattern)
        {
            bounds = this.RotateBounds(bounds);
            using (Pen pen = new Pen(color))
            {
                pen.Width = bounds.Height;
                pen.DashPattern = this.MakeFixedWidthPattern(pattern, bounds.Height);
                this.DrawLine(pen, this.RotateBounds(bounds));
            }
        }

        protected internal void DrawPatternLine(PointF from, PointF to, Color color, float thickness, float[] pattern)
        {
            using (Pen pen = new Pen(color, thickness))
            {
                pen.DashPattern = this.MakeFixedWidthPattern(pattern, thickness);
                this.DrawLine(pen, from, to);
            }
        }

        public void DrawSlantPatternLine(RectangleF bounds, Color color, float[] pattern)
        {
            RectangleF rotatedBounds = this.RotateBounds(bounds);
            using (Pen pen = new Pen(color))
            {
                pen.Width = rotatedBounds.Height;
                pen.DashPattern = this.MakeFixedWidthPattern(pattern, rotatedBounds.Height);
                pen.Transform = this.GetTransformMatrix(bounds, rotatedBounds);
                this.DrawLine(pen, bounds);
            }
        }

        protected internal void DrawSlantPatternLine(PointF from, PointF to, Color color, float thickness, float[] pattern)
        {
            using (Pen pen = new Pen(color, thickness))
            {
                pen.DashPattern = this.MakeFixedWidthPattern(pattern, thickness);
                pen.Transform = this.GetTransformMatrix(from, to);
                this.DrawLine(pen, from, to);
            }
        }

        protected void DrawSolidLine(RectangleF bounds, Color color)
        {
            Pen pen = this.painter.GetPen(color, Math.Min(bounds.Width, bounds.Height));
            try
            {
                this.DrawLine(pen, bounds);
            }
            finally
            {
                this.painter.ReleasePen(pen);
            }
        }

        protected void DrawSolidLine(PointF from, PointF to, Color color, float thickness)
        {
            Pen pen = this.painter.GetPen(color, thickness);
            try
            {
                this.DrawLine(pen, from, to);
            }
            finally
            {
                this.painter.ReleasePen(pen);
            }
        }

        public void DrawWaveUnderline(RectangleF bounds, Pen pen)
        {
            bounds = this.RotateBounds(bounds);
            float num = this.RoundToPixels(bounds.Height, this.PixelGraphics.DpiY);
            num = Math.Max(this.PixelStep, num);
            this.DrawWaveUnderline(this.RotateBounds(bounds), pen, num);
        }

        protected void DrawWaveUnderline(RectangleF bounds, Color color, float penWidth)
        {
            Pen pen = this.painter.GetPen(color, penWidth);
            try
            {
                this.DrawWaveUnderline(bounds, pen);
            }
            finally
            {
                this.painter.ReleasePen(pen);
            }
        }

        public void DrawWaveUnderline(RectangleF bounds, Pen pen, float step)
        {
            bounds = this.RotateBounds(bounds);
            int num = (int) Math.Ceiling((double) (bounds.Width / step));
            if (num > 1)
            {
                float x = bounds.X;
                float top = bounds.Top;
                float num4 = top + bounds.Bottom;
                PointF[] points = new PointF[num];
                int index = 0;
                while (index < num)
                {
                    points[index] = this.RotatePoint(new PointF(x, top));
                    top = num4 - top;
                    index++;
                    x += step;
                }
                this.Painter.DrawLines(pen, points);
            }
        }

        private Matrix GetDiagonalTransformMatrix(PointF from, PointF to)
        {
            Matrix matrix = new Matrix();
            if (to.Y > from.Y)
            {
                matrix.Shear(0.4f, 0.4f);
            }
            else
            {
                matrix.Shear(-0.4f, -0.4f);
            }
            return matrix;
        }

        private Matrix GetTransformMatrix(PointF from, PointF to)
        {
            bool isHorizontal = from.Y == to.Y;
            bool isVertical = from.X == to.X;
            return (!(isHorizontal | isVertical) ? this.GetDiagonalTransformMatrix(from, to) : this.GetTransformMatrixCore(isHorizontal, isVertical));
        }

        private Matrix GetTransformMatrix(RectangleF bounds, RectangleF rotatedBounds)
        {
            bool isHorizontal = bounds.Equals(rotatedBounds);
            return this.GetTransformMatrixCore(isHorizontal, !isHorizontal);
        }

        private Matrix GetTransformMatrixCore(bool isHorizontal, bool isVertical)
        {
            Matrix matrix = new Matrix();
            if (isHorizontal)
            {
                matrix.Shear(-1f, 0f);
            }
            else if (isVertical)
            {
                matrix.Shear(0f, -1f);
            }
            return matrix;
        }

        protected virtual RectangleF MakeBoundsAtLeast2PixelsHigh(RectangleF bounds)
        {
            if (this.RoundToPixels(bounds.Height, this.PixelGraphics.DpiY) <= this.PixelsToUnits(2f, this.PixelGraphics.DpiY))
            {
                float height = this.PixelsToUnits(2f, this.PixelGraphics.DpiY);
                bounds = new RectangleF(bounds.X, bounds.Y, bounds.Width, height);
            }
            return bounds;
        }

        protected virtual float[] MakeFixedWidthPattern(float[] pattern, float thickness)
        {
            if (!this.UseFixedWidthPattern)
            {
                thickness = this.PixelPenWidth;
            }
            int length = pattern.Length;
            float[] numArray = new float[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = pattern[i] / thickness;
            }
            return numArray;
        }

        protected virtual float PixelsToUnits(float val, float dpi) => 
            this.UnitConverter.PixelsToLayoutUnitsF(val, dpi);

        protected virtual RectangleF RotateBounds(RectangleF bounds) => 
            bounds;

        protected virtual PointF RotatePoint(PointF pointF) => 
            pointF;

        protected virtual float RoundToPixels(float val, float dpi)
        {
            float num = (float) Math.Round((double) this.UnitsToPixels(val, dpi));
            val = this.PixelsToUnits(num, dpi);
            return val;
        }

        protected virtual float UnitsToPixels(float val, float dpi) => 
            this.UnitConverter.LayoutUnitsToPixelsF(val, dpi);

        public IPatternLinePaintingSupport Painter =>
            this.painter;

        public DocumentLayoutUnitConverter UnitConverter =>
            this.unitConverter;

        protected virtual float[] DotPattern =>
            this.Parameters.DotPattern;

        protected virtual float[] DashPattern =>
            this.Parameters.DashPattern;

        protected virtual float[] DashSmallGapPattern =>
            this.Parameters.DashSmallGapPattern;

        protected virtual float[] DashDotPattern =>
            this.Parameters.DashDotPattern;

        protected virtual float[] DashDotDotPattern =>
            this.Parameters.DashDotDotPattern;

        protected virtual float[] LongDashPattern =>
            this.Parameters.LongDashPattern;

        protected virtual float PixelPenWidth =>
            this.Parameters.PixelPenWidth;

        protected virtual float PixelStep =>
            this.Parameters.PixelStep;

        public bool UseFixedWidthPattern { get; set; }

        protected abstract PatternLinePainterParameters Parameters { get; }

        protected abstract Graphics PixelGraphics { get; }
    }
}

