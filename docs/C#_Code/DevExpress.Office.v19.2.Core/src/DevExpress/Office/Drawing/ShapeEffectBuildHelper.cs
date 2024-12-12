namespace DevExpress.Office.Drawing
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ShapeEffectBuildHelper
    {
        private readonly DevExpress.Office.Drawing.ShapeLayoutInfo shapeLayoutInfo;

        public ShapeEffectBuildHelper(DevExpress.Office.Drawing.ShapeLayoutInfo shapeLayoutInfo)
        {
            this.shapeLayoutInfo = shapeLayoutInfo;
        }

        public virtual Rectangle CalculateReflectionTransformBounds(bool rotateWithShape)
        {
            using (GraphicsPath path = this.ShapeLayoutInfo.Paths.CollectFigure(GraphicsPathCollectFlags.FigureWithoutEffects | GraphicsPathCollectFlags.ExcludeInvisiblePaths))
            {
                if ((this.ShapeLayoutInfo.PenWidth > 0f) && GdipExtensions.AllowPathWidening(path))
                {
                    Pen pen = this.ShapeLayoutInfo.PenInfo.Pen;
                    if (pen.Alignment != PenAlignment.Outset)
                    {
                        WidenHelper.Apply(path, pen);
                    }
                    else
                    {
                        using (Pen pen2 = (Pen) pen.Clone())
                        {
                            pen2.Width = this.ShapeLayoutInfo.PenWidth;
                            WidenHelper.Apply(path, pen2);
                        }
                    }
                }
                Matrix shapeTransform = this.ShapeLayoutInfo.ShapeTransform;
                if (!rotateWithShape && (shapeTransform != null))
                {
                    path.Transform(shapeTransform);
                }
                return path.GetBoundsExt();
            }
        }

        public virtual Rectangle CalculateShadowTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            GraphicsPathCollectFlags flags = GraphicsPathCollectFlags.FigureWithoutEffects | GraphicsPathCollectFlags.ExcludeInvisiblePaths;
            using (GraphicsPath path = this.GetTransformedFigure(flags, rotateWithShape, additionalSize))
            {
                return path.GetBoundsExt();
            }
        }

        private Pen CreateGradientPen(Pen pen, Brush gradientBrush, float width)
        {
            Pen pen2 = null;
            if (gradientBrush != null)
            {
                pen2 = (Pen) pen.Clone();
                pen2.Width = width;
                pen2.Brush = gradientBrush;
            }
            return pen2;
        }

        public virtual void DrawGlow(Graphics graphics, Color color, EffectAdditionalSize additionalSize, Matrix shapeTransform)
        {
            PathInfoBase[] pathInfos = this.ShapeLayoutInfo.GetPathInfos();
            additionalSize.AddSize(this.ShapeLayoutInfo);
            float penWidth = this.ShapeLayoutInfo.PenWidth;
            float width = penWidth + additionalSize.TotalSize;
            using (SolidBrush brush = new SolidBrush(color))
            {
                using (Pen pen = GlowEffectHelper.CreatePen(color, width, this.ShapeLayoutInfo.PenInfo))
                {
                    using (Matrix matrix = graphics.Transform)
                    {
                        if (shapeTransform != null)
                        {
                            graphics.MultiplyTransform(shapeTransform);
                        }
                        using (Region region = graphics.Clip)
                        {
                            this.ShapeLayoutInfo.SetGlowClip(graphics, additionalSize.GlowSize);
                            PathInfoBase[] baseArray2 = pathInfos;
                            int index = 0;
                            while (true)
                            {
                                if (index >= baseArray2.Length)
                                {
                                    graphics.Clip = region;
                                    break;
                                }
                                PathInfoBase pathInfo = baseArray2[index];
                                this.DrawGlow(graphics, pathInfo, brush, pen, penWidth);
                                index++;
                            }
                        }
                        graphics.Transform = matrix;
                    }
                }
            }
        }

        private void DrawGlow(Graphics graphics, PathInfoBase pathInfo, SolidBrush glowBrush, Pen glowPen, float penWidth)
        {
            GraphicsPath graphicsPath = pathInfo.GraphicsPath;
            Brush fill = pathInfo.Fill;
            if (pathInfo.Filled)
            {
                this.FillGlowPath(graphics, graphicsPath, fill, glowBrush);
            }
            if (pathInfo.ShouldDrawGlowPath())
            {
                if (penWidth > 0f)
                {
                    this.DrawGlowPath(graphics, graphicsPath, glowPen);
                }
                else if (pathInfo.Filled)
                {
                    graphics.DrawPath(glowPen, graphicsPath);
                }
            }
        }

        private void DrawGlowPath(Graphics graphics, GraphicsPath path, Pen glowPen)
        {
            PenInfo penInfo = this.shapeLayoutInfo.PenInfo;
            Pen pen = penInfo.Pen;
            PenType penType = pen.PenType;
            if ((penType == PenType.LinearGradient) || (penType == PenType.PathGradient))
            {
                using (Brush brush = GlowEffectHelper.ConvertGradientBrushWithTransparentColors(penInfo.Brush, glowPen.Color))
                {
                    using (Pen pen2 = this.CreateGradientPen(pen, brush, glowPen.Width))
                    {
                        if (pen2 != null)
                        {
                            graphics.DrawPath(pen2, path);
                            return;
                        }
                    }
                }
            }
            graphics.DrawPath(glowPen, path);
        }

        private void FillGlowPath(Graphics graphics, GraphicsPath path, Brush brush, SolidBrush glowBrush)
        {
            if (!(brush is SolidBrush) || (((SolidBrush) brush).Color.A != 0))
            {
                if ((brush is LinearGradientBrush) || (brush is PathGradientBrush))
                {
                    using (Brush brush2 = GlowEffectHelper.ConvertGradientBrushWithTransparentColors(brush, glowBrush.Color))
                    {
                        if (brush2 != null)
                        {
                            graphics.FillPath(brush2, path);
                            return;
                        }
                    }
                }
                graphics.FillPath(glowBrush, path);
            }
        }

        public virtual Rectangle GetBlurEffectBounds(Matrix parentTransform, bool applyTransform, EffectAdditionalSize additionalSize)
        {
            GraphicsPathCollectFlags flags = GraphicsPathCollectFlags.FigureWithoutEffects | GraphicsPathCollectFlags.ExcludeInvisiblePaths;
            using (GraphicsPath path = this.GetTransformedFigure(flags, !applyTransform, additionalSize))
            {
                return path.GetBoundsExt(parentTransform);
            }
        }

        public virtual Rectangle GetGlowBounds(EffectAdditionalSize additionalSize, Matrix parentTransform)
        {
            GraphicsPathCollectFlags flags = GraphicsPathCollectFlags.ExcludeInnerShadow | GraphicsPathCollectFlags.ExcludeInvisiblePaths;
            using (GraphicsPath path = this.GetTransformedFigure(flags, true, additionalSize))
            {
                return path.GetBoundsExt(parentTransform);
            }
        }

        public virtual GraphicsPath GetPreparedForReflectionFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            GraphicsPathCollectFlags flags = GraphicsPathCollectFlags.ExcludeReflection | GraphicsPathCollectFlags.ExcludeInnerShadow | GraphicsPathCollectFlags.ExcludeOuterShadow;
            GraphicsPath reflection = this.GetTransformedFigure(flags, rotateWithShape, additionalSize);
            this.ShapeLayoutInfo.AddShadowToReflection(reflection);
            reflection.Transform(shadowTransform);
            return reflection;
        }

        public virtual GraphicsPath GetPreparedForShadowFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            GraphicsPathCollectFlags flags = GraphicsPathCollectFlags.ExcludeReflection | GraphicsPathCollectFlags.ExcludeInnerShadow | GraphicsPathCollectFlags.ExcludeOuterShadow;
            GraphicsPath path = this.GetTransformedFigure(flags, rotateWithShape, additionalSize);
            path.Transform(shadowTransform);
            return path;
        }

        public virtual Rectangle GetSoftEdgeBounds()
        {
            GraphicsPathCollectFlags flags = GraphicsPathCollectFlags.FigureWithoutEffects | GraphicsPathCollectFlags.ExcludeInvisiblePaths;
            using (GraphicsPath path = this.GetTransformedFigure(flags, false, EffectAdditionalSize.Default))
            {
                return path.GetBoundsExt();
            }
        }

        protected GraphicsPath GetTransformedFigure(GraphicsPathCollectFlags flags, bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            GraphicsPath figure = this.ShapeLayoutInfo.Paths.CollectFigure(flags);
            this.WidenFigure(figure, additionalSize);
            Matrix shapeTransform = this.ShapeLayoutInfo.ShapeTransform;
            if (!rotateWithShape && (shapeTransform != null))
            {
                figure.Transform(shapeTransform);
            }
            return figure;
        }

        protected virtual void WidenFigure(GraphicsPath figure, EffectAdditionalSize additionalSize)
        {
            additionalSize.AddSize(this.ShapeLayoutInfo);
            float width = this.ShapeLayoutInfo.PenWidth + additionalSize.TotalSize;
            if ((width > 0f) && GdipExtensions.AllowPathWidening(figure))
            {
                using (Pen pen = new Pen(Color.Empty, width))
                {
                    pen.LineJoin = LineJoin.Round;
                    WidenHelper.Apply(figure, pen);
                }
            }
        }

        protected DevExpress.Office.Drawing.ShapeLayoutInfo ShapeLayoutInfo =>
            this.shapeLayoutInfo;
    }
}

