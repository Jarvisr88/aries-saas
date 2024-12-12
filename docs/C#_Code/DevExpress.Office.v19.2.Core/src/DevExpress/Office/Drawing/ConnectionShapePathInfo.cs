namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ConnectionShapePathInfo : PathInfo
    {
        private readonly float scaleFactor;

        public ConnectionShapePathInfo(GraphicsPath graphicsPath, float scaleFactor) : base(graphicsPath, null, true)
        {
            this.scaleFactor = scaleFactor;
        }

        protected override void DrawPath(Graphics graphics, PenInfo penInfo)
        {
            if (penInfo != null)
            {
                Matrix transform = graphics.Transform;
                if (penInfo.HasCaps)
                {
                    penInfo.ApplyCaps(this.scaleFactor);
                }
                Pen pen = penInfo.Pen;
                float width = pen.Width;
                try
                {
                    float num2 = 1f / ((float) penInfo.ConvertToEmuSafe(1f));
                    using (Matrix matrix2 = new Matrix(num2, 0f, 0f, num2, 0f, 0f))
                    {
                        graphics.MultiplyTransform(matrix2);
                        pen.Width = penInfo.ConvertPenWidth(width, this.scaleFactor);
                        Matrix matrix3 = null;
                        bool flag = NeedPenBrushScale(pen);
                        try
                        {
                            matrix2.Invert();
                            if (flag)
                            {
                                matrix3 = pen.ApplyTransform(matrix2, MatrixOrder.Append);
                            }
                            using (GraphicsPath path = (GraphicsPath) base.GraphicsPath.Clone())
                            {
                                path.Transform(matrix2);
                                WidenHelper.DrawPath(path, penInfo, graphics);
                            }
                        }
                        finally
                        {
                            if (flag)
                            {
                                pen.ReplaceTransform(matrix3);
                            }
                        }
                    }
                }
                finally
                {
                    using (transform)
                    {
                        graphics.Transform = transform;
                    }
                    pen.Width = width;
                    if (penInfo.HasCaps)
                    {
                        penInfo.ResetCaps();
                    }
                }
            }
        }

        public override RectangleF GetRealBounds(Matrix transform, PenInfo penInfo)
        {
            RectangleF ef3;
            if (penInfo == null)
            {
                return base.GetRealBounds(transform, penInfo);
            }
            if (penInfo.HasCaps)
            {
                penInfo.ApplyCaps(this.scaleFactor);
            }
            Pen pen = penInfo.Pen;
            float width = pen.Width;
            try
            {
                using (GraphicsPath path = (GraphicsPath) base.GraphicsPath.Clone())
                {
                    if (transform != null)
                    {
                        path.Transform(transform);
                    }
                    pen.Width = penInfo.ConvertPenWidth(width, this.scaleFactor);
                    Matrix matrix = pen.Transform;
                    pen.ScaleTransform(this.scaleFactor, this.scaleFactor, MatrixOrder.Append);
                    try
                    {
                        float num2 = penInfo.ConvertToEmuSafe(1f);
                        using (Matrix matrix2 = new Matrix(num2, 0f, 0f, num2, 0f, 0f))
                        {
                            path.Transform(matrix2);
                        }
                        RectangleF ef = path.GetBoundsExtF(null, penInfo);
                        RectangleF ef2 = new RectangleF(ef.X / num2, ef.Y / num2, ef.Width / num2, ef.Height / num2);
                        ef2.Inflate(1f, 1f);
                        ef3 = ef2;
                    }
                    finally
                    {
                        pen.Transform = matrix;
                    }
                }
            }
            finally
            {
                pen.Width = width;
                if (penInfo.HasCaps)
                {
                    penInfo.ResetCaps();
                }
            }
            return ef3;
        }

        private static bool NeedPenBrushScale(Pen pen)
        {
            switch (pen.PenType)
            {
                case PenType.TextureFill:
                case PenType.PathGradient:
                case PenType.LinearGradient:
                    return true;
            }
            return false;
        }

        public override bool AllowHitTest =>
            true;
    }
}

