namespace DevExpress.Utils.Svg
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    public sealed class SvgGraphicsBase : ISvgGraphics, IDisposable
    {
        private IGraphicsBase graphicsCore;

        public SvgGraphicsBase(IGraphicsBase graphics)
        {
            this.graphicsCore = graphics;
        }

        public void Dispose()
        {
            this.graphicsCore = null;
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            this.graphicsCore.DrawLine(pen, x1, y1, x2, y2);
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            this.graphicsCore.DrawPath(pen, path);
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            this.graphicsCore.FillPath(brush, path);
        }

        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat) => 
            this.graphicsCore.MeasureString(text, font, origin, stringFormat, GraphicsUnit.Pixel);

        public void Restore(object graphicsState)
        {
            this.graphicsCore.Restore(graphicsState as IGraphicsState);
        }

        public object Save() => 
            this.graphicsCore.Save();

        public void ScaleTransform(float sx, float sy, MatrixOrder order = 0)
        {
            this.graphicsCore.ScaleTransform(sx, sy, order);
        }

        public void SetClip(GraphicsPath path, CombineMode combineMode = 0)
        {
            this.graphicsCore.IntersectClip(path);
        }

        public void TranslateTransform(float dx, float dy, MatrixOrder order = 0)
        {
            this.graphicsCore.TranslateTransform(dx, dy, order);
        }

        public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            get => 
                this.graphicsCore.SmoothingMode;
            set => 
                this.graphicsCore.SmoothingMode = value;
        }

        public Matrix Transform
        {
            get => 
                this.graphicsCore.Transform;
            set => 
                this.graphicsCore.Transform = value;
        }
    }
}

