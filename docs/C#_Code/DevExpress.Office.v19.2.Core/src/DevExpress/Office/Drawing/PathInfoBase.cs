namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public abstract class PathInfoBase : IDisposable
    {
        private System.Drawing.Drawing2D.GraphicsPath graphicsPath;
        private Brush fill;

        protected PathInfoBase(System.Drawing.Drawing2D.GraphicsPath graphicsPath, Brush fill, bool stroke)
        {
            this.graphicsPath = graphicsPath;
            this.fill = fill;
            this.Stroke = stroke;
        }

        protected PathInfoBase(System.Drawing.Drawing2D.GraphicsPath graphicsPath, Brush fill, bool stroke, bool hasPermanentFill) : this(graphicsPath, fill, stroke)
        {
            this.HasPermanentFill = hasPermanentFill;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.fill != null)
            {
                this.fill.Dispose();
                this.fill = null;
            }
            if (this.graphicsPath != null)
            {
                this.graphicsPath.Dispose();
                this.graphicsPath = null;
            }
        }

        public virtual void Draw(Graphics graphics, PenInfo penInfo, Matrix shapeTransform)
        {
            if ((this.fill != null) || (this.Stroke && (penInfo != null)))
            {
                this.DrawCore(graphics, penInfo, shapeTransform);
            }
        }

        internal virtual void DrawCore(Graphics graphics, PenInfo penInfo)
        {
            if (this.fill != null)
            {
                graphics.FillPath(this.fill, this.GraphicsPath);
            }
            if (this.Stroke && (penInfo != null))
            {
                this.DrawPath(graphics, penInfo);
            }
        }

        protected virtual void DrawCore(Graphics graphics, PenInfo penInfo, Matrix shapeTransform)
        {
            if (shapeTransform == null)
            {
                this.DrawCore(graphics, penInfo);
            }
            else
            {
                bool hasPermanentStroke = (penInfo != null) && penInfo.HasPermanentFill;
                bool hasPermanentFill = this.HasPermanentFill;
                if (hasPermanentFill | hasPermanentStroke)
                {
                    this.DrawWithoutGraphicsTransform(graphics, penInfo, shapeTransform, hasPermanentFill, hasPermanentStroke);
                }
                else
                {
                    this.DrawWithGraphicsTransform(graphics, penInfo, shapeTransform);
                }
            }
        }

        protected virtual void DrawPath(Graphics graphics, PenInfo penInfo)
        {
            Pen pen = penInfo?.Pen;
            if (pen != null)
            {
                if (pen.Alignment != PenAlignment.Outset)
                {
                    graphics.DrawPath(pen, this.GraphicsPath);
                }
                else
                {
                    using (System.Drawing.Drawing2D.GraphicsPath path = GdipExtensions.TransformGraphicsPathForOutsetPen(this.GraphicsPath, pen))
                    {
                        graphics.DrawPath(pen, path);
                    }
                }
            }
        }

        protected void DrawWithGraphicsTransform(Graphics graphics, PenInfo penInfo, Matrix shapeTransform)
        {
            Matrix transform = graphics.Transform;
            try
            {
                graphics.MultiplyTransform(shapeTransform);
                this.DrawCore(graphics, penInfo);
            }
            finally
            {
                graphics.Transform = transform;
                transform.Dispose();
            }
        }

        protected void DrawWithoutGraphicsTransform(Graphics graphics, PenInfo penInfo, Matrix shapeTransform, bool hasPermanentFill, bool hasPermanentStroke)
        {
            Matrix transform = null;
            Matrix matrix2 = null;
            Pen pen = penInfo?.Pen;
            try
            {
                this.GraphicsPath.Transform(shapeTransform);
                if (!hasPermanentFill && (this.Fill != null))
                {
                    transform = this.Fill.ApplyTransform(shapeTransform, MatrixOrder.Append);
                }
                if (!hasPermanentStroke && (penInfo != null))
                {
                    matrix2 = pen.ApplyTransform(shapeTransform, MatrixOrder.Append);
                }
                this.DrawCore(graphics, penInfo);
            }
            finally
            {
                if (shapeTransform.IsInvertible)
                {
                    Matrix matrix = shapeTransform.Clone();
                    matrix.Invert();
                    this.GraphicsPath.Transform(matrix);
                    matrix.Dispose();
                }
                if (transform != null)
                {
                    this.Fill.ReplaceTransform(transform);
                    transform.Dispose();
                }
                if (matrix2 != null)
                {
                    pen.ReplaceTransform(matrix2);
                    matrix2.Dispose();
                }
            }
        }

        public virtual Rectangle GetBounds() => 
            this.graphicsPath.GetBoundsExt();

        public virtual RectangleF GetRealBounds(Matrix transform, PenInfo penInfo) => 
            this.graphicsPath.GetBoundsExtF(transform, this.Stroke ? penInfo : null);

        public virtual bool HitTest(Point logicalPoint, Pen pen, Matrix invertedShapeTransform)
        {
            if (invertedShapeTransform != null)
            {
                logicalPoint = invertedShapeTransform.TransformPoint(logicalPoint);
            }
            return (((this.Fill == null) || !this.graphicsPath.IsVisible(logicalPoint)) ? (this.Stroke && ((pen != null) && this.graphicsPath.IsOutlineVisible(logicalPoint, pen))) : true);
        }

        protected internal virtual bool ShouldDrawGlowPath() => 
            this.Stroke;

        public System.Drawing.Drawing2D.GraphicsPath GraphicsPath =>
            this.graphicsPath;

        public Brush Fill =>
            this.fill;

        public bool HasPermanentFill { get; set; }

        public bool Stroke { get; set; }

        public bool SkipDrawing { get; set; }

        public virtual bool AllowHitTest =>
            false;

        public virtual bool Filled =>
            this.fill != null;
    }
}

