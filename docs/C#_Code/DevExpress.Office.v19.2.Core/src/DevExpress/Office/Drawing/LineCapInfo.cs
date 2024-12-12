namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class LineCapInfo : IDisposable
    {
        public LineCapInfo(OutlineHeadTailType type, System.Drawing.Drawing2D.GraphicsPath graphicsPath, System.Drawing.Drawing2D.GraphicsPath boundingPath, bool filled, float inset)
        {
            this.Type = type;
            this.GraphicsPath = graphicsPath;
            this.BoundingPath = boundingPath;
            this.Filled = filled;
            this.Inset = inset;
        }

        public CustomLineCap CreateBoundingCap(float scale)
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = (System.Drawing.Drawing2D.GraphicsPath) this.BoundingPath.Clone())
            {
                this.TransformPath(path, scale);
                return new CustomLineCap(path, null, LineCap.Round, this.Inset * scale);
            }
        }

        public CustomLineCap CreateCap(float scale)
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = (System.Drawing.Drawing2D.GraphicsPath) this.GraphicsPath.Clone())
            {
                this.TransformPath(path, scale);
                return new CustomLineCap(this.Filled ? path : null, this.Filled ? null : path, LineCap.Round, this.Inset * scale);
            }
        }

        public void Dispose()
        {
            if (this.GraphicsPath != null)
            {
                this.GraphicsPath.Dispose();
                this.GraphicsPath = null;
            }
        }

        private void TransformPath(System.Drawing.Drawing2D.GraphicsPath path, float scale)
        {
            if (scale != 1f)
            {
                using (Matrix matrix = new Matrix(scale, 0f, 0f, scale, 0f, 0f))
                {
                    path.Transform(matrix);
                }
            }
        }

        public OutlineHeadTailType Type { get; private set; }

        public System.Drawing.Drawing2D.GraphicsPath GraphicsPath { get; private set; }

        public System.Drawing.Drawing2D.GraphicsPath BoundingPath { get; private set; }

        public bool Filled { get; private set; }

        public float Inset { get; private set; }
    }
}

