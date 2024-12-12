namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class GdiGraphics : GraphicsBase, IGraphics, IGraphicsBase, IPrintingSystemContext, System.IServiceProvider, IDisposable
    {
        private static readonly object padlock = new object();
        private static System.Drawing.Image checkImage;
        private static System.Drawing.Image uncheckImage;
        private static System.Drawing.Image checkGrayImage;
        private System.Drawing.Graphics gr;
        private TransformStack transformStack;
        protected GraphicsModifier modifier;

        public GdiGraphics(System.Drawing.Graphics gr, PrintingSystemBase ps) : base(ps)
        {
            this.transformStack = new TransformStack();
            this.gr = gr;
            this.modifier = ((System.IServiceProvider) ps).GetService(typeof(GraphicsModifier)) as GraphicsModifier;
            this.SetPageUnit();
            ForceUpdateClipBounds(gr);
        }

        public void ApplyTransformState(MatrixOrder order, bool removeState)
        {
            Matrix matrix = removeState ? this.transformStack.Pop() : this.transformStack.Peek();
            this.MultiplyTransform(matrix, order);
        }

        void IGraphics.AddDrawingAction(DeferredAction action)
        {
            action.Execute(base.PrintingSystem, this);
        }

        public virtual void Dispose()
        {
            if (this.modifier != null)
            {
                this.modifier.OnGraphicsDispose();
                this.modifier = null;
            }
        }

        public void DrawCheckBox(RectangleF rect, CheckState state)
        {
            System.Drawing.Image image = ((state & CheckState.Checked) != CheckState.Unchecked) ? CheckImage : (((state & CheckState.Indeterminate) != CheckState.Unchecked) ? CheckGrayImage : UncheckImage);
            System.Drawing.Image image2 = image;
            lock (image2)
            {
                this.gr.DrawImageUnscaled(image, Rectangle.Round(rect).Location);
            }
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
                this.gr.DrawEllipse(pen, x, y, width, height);
            }
        }

        public virtual void DrawImage(System.Drawing.Image image, Point position)
        {
            System.Drawing.Image image2 = image;
            lock (image2)
            {
                this.modifier.DrawImage(this.gr, image, position);
            }
        }

        public virtual void DrawImage(System.Drawing.Image image, RectangleF bounds)
        {
            System.Drawing.Image image2 = image;
            lock (image2)
            {
                this.modifier.DrawImage(this.gr, image, bounds);
            }
        }

        public void DrawImage(System.Drawing.Image image, RectangleF bounds, Color underlyingColor)
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
                this.gr.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
            Pen pen2 = pen;
            lock (pen2)
            {
                this.gr.DrawLines(pen, points);
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
                    this.gr.DrawPath(pen, path);
                }
            }
        }

        public void DrawRectangle(Pen pen, RectangleF rect)
        {
            Pen pen2 = pen;
            lock (pen2)
            {
                this.gr.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
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

        public virtual void DrawString(string s, Font font, Brush brush, RectangleF bounds, StringFormat format)
        {
            EnsureStringFormat(font, bounds, this.gr.PageUnit, format, validFormat => this.modifier.DrawString(this.gr, s, font, brush, bounds, validFormat));
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
                this.gr.FillEllipse(brush, x, y, width, height);
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
                    this.gr.FillPath(brush, path);
                }
            }
        }

        public void FillRectangle(Brush brush, RectangleF bounds)
        {
            this.FillRectangle(brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            Brush brush2 = brush;
            lock (brush2)
            {
                this.gr.FillRectangle(brush, x, y, width, height);
            }
        }

        private static void ForceUpdateClipBounds(System.Drawing.Graphics gr)
        {
            Matrix transform = gr.Transform;
            gr.ResetTransform();
            gr.Transform = transform;
        }

        private static void InitCheckImages()
        {
            object padlock = GdiGraphics.padlock;
            lock (padlock)
            {
                checkImage = CheckBoxImageHelper.GetCheckBoxImage(CheckState.Checked);
                uncheckImage = CheckBoxImageHelper.GetCheckBoxImage(CheckState.Unchecked);
                checkGrayImage = CheckBoxImageHelper.GetCheckBoxImage(CheckState.Indeterminate);
            }
        }

        public void IntersectClip(GraphicsPath path)
        {
            this.gr.SetClip(path, CombineMode.Intersect);
        }

        public SizeF MeasureString(string text, Font font, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, graphicsUnit);

        public SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, location, stringFormat, graphicsUnit);

        public SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, size, stringFormat, graphicsUnit);

        public SizeF MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, width, stringFormat, graphicsUnit);

        public void MultiplyTransform(Matrix matrix)
        {
            this.gr.MultiplyTransform(matrix, MatrixOrder.Prepend);
        }

        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            Matrix matrix2 = matrix;
            lock (matrix2)
            {
                this.gr.MultiplyTransform(matrix, order);
            }
        }

        public void ResetTransform()
        {
            this.gr.ResetTransform();
        }

        public void Restore(IGraphicsState gstate)
        {
            GdiGraphicsStateContainer container = gstate as GdiGraphicsStateContainer;
            if (container != null)
            {
                this.gr.Restore(container.GraphicsState);
            }
        }

        public void RotateTransform(float angle)
        {
            this.RotateTransform(angle, MatrixOrder.Prepend);
        }

        public void RotateTransform(float angle, MatrixOrder order)
        {
            this.gr.RotateTransform(angle, order);
        }

        public IGraphicsState Save() => 
            new GdiGraphicsStateContainer(this.gr.Save());

        public void SaveTransformState()
        {
            this.transformStack.Push(this.gr.Transform);
        }

        public void ScaleTransform(float sx, float sy)
        {
            this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
        }

        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            this.modifier.ScaleTransform(this.gr, sx, sy, order);
        }

        protected virtual void SetPageUnit()
        {
            this.PageUnit = GraphicsUnit.Document;
        }

        public void TranslateTransform(float dx, float dy)
        {
            this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
        }

        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            this.gr.TranslateTransform(dx, dy, order);
        }

        private static System.Drawing.Image CheckImage
        {
            get
            {
                if (checkImage == null)
                {
                    InitCheckImages();
                }
                return checkImage;
            }
        }

        private static System.Drawing.Image UncheckImage
        {
            get
            {
                if (uncheckImage == null)
                {
                    InitCheckImages();
                }
                return uncheckImage;
            }
        }

        private static System.Drawing.Image CheckGrayImage
        {
            get
            {
                if (checkGrayImage == null)
                {
                    InitCheckImages();
                }
                return checkGrayImage;
            }
        }

        public System.Drawing.Graphics Graphics =>
            this.gr;

        public float Dpi =>
            this.gr.DpiX;

        public RectangleF ClipBounds
        {
            get => 
                this.gr.ClipBounds;
            set => 
                this.gr.SetClip(value);
        }

        public Region Clip
        {
            get => 
                this.gr.Clip;
            set => 
                this.gr.Clip = value;
        }

        public GraphicsUnit PageUnit
        {
            get => 
                this.gr.PageUnit;
            set => 
                this.modifier.SetPageUnit(this.gr, value);
        }

        public Matrix Transform
        {
            get => 
                this.gr.Transform;
            set => 
                this.gr.Transform = value;
        }

        public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            get => 
                this.gr.SmoothingMode;
            set => 
                this.gr.SmoothingMode = value;
        }

        public System.Drawing.Text.TextRenderingHint TextRenderingHint
        {
            get => 
                this.gr.TextRenderingHint;
            set => 
                this.gr.TextRenderingHint = value;
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

