namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class ConnectionShapeEffectBuildHelper : ShapeEffectBuildHelper
    {
        public ConnectionShapeEffectBuildHelper(ShapeLayoutInfo shapeLayoutInfo) : base(shapeLayoutInfo)
        {
        }

        public override Rectangle CalculateReflectionTransformBounds(bool rotateWithShape)
        {
            using (GraphicsPath path = this.GetSourceFigureWithCapBounds(!rotateWithShape))
            {
                return ((path != null) ? path.GetBoundsExt() : Rectangle.Empty);
            }
        }

        public override Rectangle CalculateShadowTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            Rectangle empty;
            using (GraphicsPath path = this.GetSourceFigure(!rotateWithShape))
            {
                if (path == null)
                {
                    empty = Rectangle.Empty;
                }
                else
                {
                    this.WidenFigure(path, additionalSize);
                    empty = path.GetBoundsExt();
                }
            }
            return empty;
        }

        public override void DrawGlow(Graphics graphics, Color color, EffectAdditionalSize additionalSize, Matrix shapeTransform)
        {
            using (GraphicsPath path = this.GetSourceFigure(false))
            {
                if (path != null)
                {
                    additionalSize.AddSize(base.ShapeLayoutInfo);
                    using (Matrix matrix = graphics.Transform)
                    {
                        using (Region region = graphics.Clip)
                        {
                            if (shapeTransform != null)
                            {
                                graphics.MultiplyTransform(shapeTransform);
                            }
                            base.ShapeLayoutInfo.SetGlowClip(graphics, additionalSize.GlowSize);
                            using (Brush brush = new SolidBrush(color))
                            {
                                using (Pen pen = new Pen(color, additionalSize.TotalSize))
                                {
                                    pen.LineJoin = LineJoin.Round;
                                    graphics.FillPath(brush, path);
                                    graphics.DrawPath(pen, path);
                                }
                            }
                            graphics.Clip = region;
                        }
                        graphics.Transform = matrix;
                    }
                }
            }
        }

        public override Rectangle GetBlurEffectBounds(Matrix parentTransform, bool applyTransform, EffectAdditionalSize additionalSize)
        {
            Rectangle empty;
            using (GraphicsPath path = this.GetSourceFigure(applyTransform))
            {
                if (path == null)
                {
                    empty = Rectangle.Empty;
                }
                else
                {
                    this.WidenFigure(path, additionalSize);
                    empty = path.GetBoundsExt(parentTransform);
                }
            }
            return empty;
        }

        public override Rectangle GetGlowBounds(EffectAdditionalSize additionalSize, Matrix parentTransform)
        {
            Rectangle empty;
            using (GraphicsPath path = this.GetSourceFigure(false))
            {
                if (path == null)
                {
                    empty = Rectangle.Empty;
                }
                else
                {
                    this.WidenFigure(path, additionalSize);
                    empty = path.GetBoundsExt(parentTransform);
                }
            }
            return empty;
        }

        public override GraphicsPath GetPreparedForReflectionFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            GraphicsPath sourceFigureWithCapBounds = this.GetSourceFigureWithCapBounds(!rotateWithShape);
            if (sourceFigureWithCapBounds == null)
            {
                return null;
            }
            this.WidenFigure(sourceFigureWithCapBounds, additionalSize);
            base.ShapeLayoutInfo.AddShadowToReflection(sourceFigureWithCapBounds);
            sourceFigureWithCapBounds.Transform(shadowTransform);
            return sourceFigureWithCapBounds;
        }

        public override GraphicsPath GetPreparedForShadowFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            GraphicsPath sourceFigure = this.GetSourceFigure(!rotateWithShape);
            if (sourceFigure == null)
            {
                return null;
            }
            this.WidenFigure(sourceFigure, additionalSize);
            sourceFigure.Transform(shadowTransform);
            return sourceFigure;
        }

        public override Rectangle GetSoftEdgeBounds()
        {
            using (GraphicsPath path = this.GetSourceFigure(false))
            {
                return ((path != null) ? path.GetBoundsExt() : Rectangle.Empty);
            }
        }

        private GraphicsPath GetSourceFigure(bool applyTransform)
        {
            float penWidth = base.ShapeLayoutInfo.PenWidth;
            return ((penWidth != 0f) ? this.GetSourceFigureCore(applyTransform, new Action<float>(base.ShapeLayoutInfo.PenInfo.ApplyCaps), penWidth, false) : null);
        }

        private GraphicsPath GetSourceFigureCore(bool applyTransform, Action<float> applyCaps, float penWidth, bool applyBoundingCaps)
        {
            GraphicsPath path2;
            PenInfo penInfo = base.ShapeLayoutInfo.PenInfo;
            Pen pen = penInfo.Pen;
            float scaleFactor = base.ShapeLayoutInfo.ScaleFactor;
            if (penInfo.HasCaps)
            {
                applyCaps(scaleFactor);
            }
            try
            {
                GraphicsPath graphicsPath = (GraphicsPath) base.ShapeLayoutInfo.Paths.GetPathInfo<ConnectionShapePathInfo>().GraphicsPath.Clone();
                using (Matrix matrix = new Matrix(1f / scaleFactor, 0f, 0f, 1f / scaleFactor, 0f, 0f))
                {
                    Matrix shapeTransform = base.ShapeLayoutInfo.ShapeTransform;
                    if (applyTransform && (shapeTransform != null))
                    {
                        graphicsPath.Transform(shapeTransform);
                        graphicsPath.Transform(matrix);
                    }
                    matrix.Invert();
                    float num2 = penInfo.ConvertToEmuSafe(1f);
                    pen.Width = penInfo.ConvertPenWidth(penWidth, scaleFactor);
                    using (Matrix matrix3 = new Matrix(num2, 0f, 0f, num2, 0f, 0f))
                    {
                        graphicsPath.Transform(matrix3);
                        if (GdipExtensions.AllowPathWidening(graphicsPath))
                        {
                            WidenHelper.Apply(graphicsPath, penInfo, applyBoundingCaps, applyTransform ? matrix : null);
                        }
                        matrix3.Invert();
                        graphicsPath.Transform(matrix3);
                        path2 = graphicsPath;
                    }
                }
            }
            finally
            {
                pen.Width = penWidth;
                if (penInfo.HasCaps)
                {
                    penInfo.ResetCaps();
                }
            }
            return path2;
        }

        private GraphicsPath GetSourceFigureWithCapBounds(bool applyTransform)
        {
            float penWidth = base.ShapeLayoutInfo.PenWidth;
            return ((penWidth != 0f) ? this.GetSourceFigureCore(applyTransform, new Action<float>(base.ShapeLayoutInfo.PenInfo.ApplyBoundingCaps), penWidth, true) : null);
        }

        protected override void WidenFigure(GraphicsPath figure, EffectAdditionalSize additionalSize)
        {
            additionalSize.AddSize(base.ShapeLayoutInfo);
            float totalSize = additionalSize.TotalSize;
            if ((totalSize > 0f) && GdipExtensions.AllowPathWidening(figure))
            {
                using (Pen pen = new Pen(Color.Empty, totalSize))
                {
                    pen.LineJoin = LineJoin.Round;
                    WidenHelper.Apply(figure, pen);
                }
            }
        }
    }
}

