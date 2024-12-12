namespace DevExpress.Printing
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class GdiGraphicsWrapperBase : IGraphicsBase
    {
        private System.Drawing.Graphics graphics;
        private TransformStack transformStack = new TransformStack();

        public GdiGraphicsWrapperBase(System.Drawing.Graphics gr)
        {
            this.graphics = gr;
        }

        public void ApplyTransformState(MatrixOrder order, bool removeState)
        {
            Matrix matrix = removeState ? this.transformStack.Pop() : this.transformStack.Peek();
            this.MultiplyTransform(matrix, order);
        }

        public virtual void DrawCheckBox(RectangleF rect, CheckState state)
        {
        }

        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            Pen pen2 = pen;
            lock (pen2)
            {
                this.graphics.DrawEllipse(pen, x, y, width, height);
            }
        }

        public void DrawImage(Image image, Point position)
        {
            Image image2 = image;
            lock (image2)
            {
                this.graphics.DrawImage(image, position);
            }
        }

        public void DrawImage(Image image, RectangleF bounds)
        {
            Image image2 = image;
            lock (image2)
            {
                this.graphics.DrawImage(image, bounds);
            }
        }

        public void DrawImage(Image image, RectangleF bounds, Color underlyingColor)
        {
            this.DrawImage(image, bounds);
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            this.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            Pen pen2 = pen;
            lock (pen2)
            {
                this.graphics.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
            Pen pen2 = pen;
            lock (pen2)
            {
                this.graphics.DrawLines(pen, points);
            }
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            Pen pen2 = pen;
            lock (pen2)
            {
                GraphicsPath path2 = path;
                lock (path2)
                {
                    this.graphics.DrawPath(pen, path);
                }
            }
        }

        public void DrawRectangle(Pen pen, RectangleF rect)
        {
            Pen pen2 = pen;
            lock (pen2)
            {
                this.graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), null);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF bounds)
        {
            this.DrawString(s, font, brush, bounds, null);
        }

        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), format);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF bounds, StringFormat format)
        {
            this.graphics.DrawString(s, font, brush, bounds, format);
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            Brush brush2 = brush;
            lock (brush2)
            {
                this.graphics.FillEllipse(brush, x, y, width, height);
            }
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            Brush brush2 = brush;
            lock (brush2)
            {
                GraphicsPath path2 = path;
                lock (path2)
                {
                    this.graphics.FillPath(brush, path);
                }
            }
        }

        public void FillRectangle(Brush brush, RectangleF bounds)
        {
            this.graphics.FillRectangle(brush, bounds);
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            this.graphics.FillRectangle(brush, new RectangleF(new PointF(x, y), new SizeF(width, height)));
        }

        public Brush GetBrush(Color color) => 
            new SolidBrush(color);

        public void IntersectClip(GraphicsPath path)
        {
            this.graphics.SetClip(path, CombineMode.Intersect);
        }

        public SizeF MeasureString(string text, Font font, GraphicsUnit graphicsUnit) => 
            this.MeasureString(text, font, new SizeF(0f, 0f), null, graphicsUnit);

        public SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit graphicsUnit)
        {
            this.graphics.PageUnit = graphicsUnit;
            return this.graphics.MeasureString(text, font, location, stringFormat);
        }

        public SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit graphicsUnit)
        {
            this.graphics.PageUnit = graphicsUnit;
            return this.graphics.MeasureString(text, font, size, stringFormat);
        }

        public SizeF MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            this.MeasureString(text, font, new SizeF(width, 999999f), stringFormat, graphicsUnit);

        public void MultiplyTransform(Matrix matrix)
        {
            this.graphics.MultiplyTransform(matrix, MatrixOrder.Prepend);
        }

        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            Matrix matrix2 = matrix;
            lock (matrix2)
            {
                this.graphics.MultiplyTransform(matrix, order);
            }
        }

        public void ResetTransform()
        {
            this.graphics.ResetTransform();
        }

        public void Restore(IGraphicsState gstate)
        {
            GdiGraphicsStateContainer container = gstate as GdiGraphicsStateContainer;
            if (container != null)
            {
                this.graphics.Restore(container.GraphicsState);
            }
        }

        public void RotateTransform(float angle)
        {
            this.RotateTransform(angle, MatrixOrder.Prepend);
        }

        public void RotateTransform(float angle, MatrixOrder order)
        {
            this.graphics.RotateTransform(angle, order);
        }

        public IGraphicsState Save() => 
            new GdiGraphicsStateContainer(this.graphics.Save());

        public void SaveTransformState()
        {
            this.transformStack.Push(this.graphics.Transform);
        }

        public void ScaleTransform(float sx, float sy)
        {
            this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
        }

        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            this.graphics.ScaleTransform(sx, sy, order);
        }

        public void TranslateTransform(float dx, float dy)
        {
            this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
        }

        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            this.graphics.TranslateTransform(dx, dy, order);
        }

        protected System.Drawing.Graphics Graphics =>
            this.graphics;

        public RectangleF ClipBounds
        {
            get => 
                this.graphics.ClipBounds;
            set => 
                this.graphics.SetClip(value);
        }

        public GraphicsUnit PageUnit
        {
            get => 
                this.graphics.PageUnit;
            set => 
                this.graphics.PageUnit = value;
        }

        public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            get => 
                this.graphics.SmoothingMode;
            set => 
                this.graphics.SmoothingMode = value;
        }

        public Matrix Transform
        {
            get => 
                this.graphics.Transform;
            set => 
                this.graphics.Transform = value;
        }

        private class GdiGraphicsStateContainer : IGraphicsState
        {
            public GdiGraphicsStateContainer(System.Drawing.Drawing2D.GraphicsState gstate)
            {
                this.GraphicsState = gstate;
            }

            public System.Drawing.Drawing2D.GraphicsState GraphicsState { get; private set; }
        }

        private class TransformStack
        {
            private Stack stack = new Stack();

            public Matrix Peek() => 
                (Matrix) this.stack.Peek();

            public Matrix Pop() => 
                (Matrix) this.stack.Pop();

            public void Push(Matrix transform)
            {
                this.stack.Push(transform);
            }
        }
    }
}

