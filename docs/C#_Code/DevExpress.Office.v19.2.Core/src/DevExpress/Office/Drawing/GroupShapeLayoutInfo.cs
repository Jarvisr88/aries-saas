namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class GroupShapeLayoutInfo : ShapeLayoutInfoBase, IDisposable
    {
        public GroupShapeLayoutInfo() : base(false)
        {
            this.ShapeLayoutInfos = new List<IShapeLayoutInfo>();
            this.InnerDrawingsOrder = new Dictionary<int, ShapeLayoutInfo>();
        }

        public GroupShapeLayoutInfo(Matrix shapeTransform) : this()
        {
            base.ShapeTransform = shapeTransform;
        }

        public void AddShapeLayoutInfo(IShapeLayoutInfo shapeLayoutInfo)
        {
            this.ShapeLayoutInfos.Add(shapeLayoutInfo);
            base.UseForGroupEffects |= shapeLayoutInfo.UseForGroupEffects;
        }

        public override Rectangle CalculateReflectionTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize) => 
            this.CalculateShadowTransformBounds(rotateWithShape, additionalSize);

        public override Rectangle CalculateShadowTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            additionalSize.ProcessGlowSize = true;
            GetBoundsStrategy getBounds = delegate (IShapeLayoutInfo shapeLayoutInfo) {
                if (shapeLayoutInfo is GroupShapeLayoutInfo)
                {
                    additionalSize.AddSize(shapeLayoutInfo);
                }
                return shapeLayoutInfo.CalculateShadowTransformBounds(rotateWithShape, additionalSize);
            };
            return this.GetTotalBounds(getBounds);
        }

        private Matrix CreateGlowChildTransform(Matrix parentTransform, Matrix shapeTransform)
        {
            Matrix matrix = (shapeTransform != null) ? shapeTransform.Clone() : new Matrix();
            if ((parentTransform != null) && parentTransform.IsInvertible)
            {
                using (Matrix matrix2 = parentTransform.Clone())
                {
                    matrix2.Invert();
                    matrix.Multiply(matrix2, MatrixOrder.Append);
                }
            }
            return matrix;
        }

        public override HitTestInfoCollection CreateReflectionHitTestInfos(Matrix shadowTransform, bool rotateWithShape)
        {
            HitTestInfoCollection hitTestInfos = new HitTestInfoCollection();
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                hitTestInfos.AddRange(info.CreateReflectionHitTestInfos(shadowTransform, rotateWithShape));
            }
            base.PopulateReflectionHitTestInfosFromShadow(hitTestInfos, shadowTransform, rotateWithShape);
            return hitTestInfos;
        }

        public override HitTestInfoCollection CreateShadowHitTestInfos(Matrix shadowTransform, bool rotateWithShape)
        {
            HitTestInfoCollection infos = new HitTestInfoCollection();
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                infos.AddRange(info.CreateShadowHitTestInfos(shadowTransform, rotateWithShape));
            }
            return infos;
        }

        protected override void DisposeCore()
        {
            base.DisposeCore();
            if (this.ShapeLayoutInfos != null)
            {
                foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
                {
                    info.Dispose();
                }
                this.ShapeLayoutInfos = null;
            }
        }

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                info.Draw(graphics);
            }
        }

        public override void DrawBlurEffect(Graphics graphics)
        {
            base.DrawBackgroundBitmap(graphics, GraphicsPathCollectFlags.ExcludeBlur);
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                if (info.UseForGroupEffects)
                {
                    info.DrawBlurEffect(graphics);
                }
            }
        }

        public override void DrawGlow(Graphics graphics, Color color, EffectAdditionalSize additionalSize, Matrix shapeTransform)
        {
            additionalSize.AddSize(this);
            additionalSize.ProcessGlowSize = true;
            using (Matrix matrix = graphics.Transform)
            {
                using (Region region = graphics.Clip)
                {
                    if (shapeTransform != null)
                    {
                        graphics.MultiplyTransform(shapeTransform);
                    }
                    base.SetGlowClip(graphics, additionalSize.GlowSize);
                    foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
                    {
                        if (info.UseForGroupEffects)
                        {
                            using (Matrix matrix2 = this.CreateGlowChildTransform(base.ShapeTransform, info.ShapeTransform))
                            {
                                info.DrawGlow(graphics, color, additionalSize, matrix2);
                            }
                        }
                    }
                    graphics.Clip = region;
                }
                graphics.Transform = matrix;
            }
        }

        private void DrawGroupPathInfos(Graphics graphics, GraphicsPathCollectFlags excludeFlags)
        {
            foreach (PathInfoBase base2 in base.Paths)
            {
                if (!base.Paths.ShouldSkipPathInfo(base2, excludeFlags) && !base2.SkipDrawing)
                {
                    bool flag = base2 is GlowPathInfo;
                    base2.Draw(graphics, this.PenInfo, flag ? base.ShapeTransform : null);
                }
            }
        }

        public override void DrawReflection(Graphics graphics, bool applyTransform)
        {
            this.DrawGroupPathInfos(graphics, GraphicsPathCollectFlags.ExcludeReflection);
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                if (info.UseForGroupEffects)
                {
                    info.DrawShadow(graphics, applyTransform);
                }
            }
        }

        public override void DrawShadow(Graphics graphics, bool applyTransform)
        {
            base.DrawCore(graphics, applyTransform, GraphicsPathCollectFlags.ExcludeReflection | GraphicsPathCollectFlags.ExcludeOuterShadow);
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                if (info.UseForGroupEffects)
                {
                    info.DrawShadow(graphics, applyTransform);
                }
            }
        }

        public override Rectangle GetBlurEffectBounds(Matrix parentTransform, bool applyTransform, EffectAdditionalSize additionalSize)
        {
            additionalSize.AddSize(this);
            if (!applyTransform)
            {
                parentTransform = this.GetInverseTransform();
            }
            GetBoundsStrategy getBounds = shapeLayoutInfo => shapeLayoutInfo.GetBlurEffectBounds(parentTransform, true, additionalSize);
            return this.GetTotalBounds(getBounds);
        }

        public override Rectangle GetBounds()
        {
            Rectangle bounds = base.GetBounds();
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                Rectangle b = info.GetBounds();
                if (!b.IsEmpty)
                {
                    bounds = !bounds.IsEmpty ? Rectangle.Union(bounds, b) : b;
                }
            }
            return bounds;
        }

        public override Rectangle GetGlowBounds(EffectAdditionalSize additionalSize, Matrix parentTransform)
        {
            additionalSize.AddSize(this);
            additionalSize.ProcessGlowSize = true;
            GetBoundsStrategy getBounds = delegate (IShapeLayoutInfo shapeLayoutInfo) {
                Matrix matrix1;
                if (parentTransform != null)
                {
                    matrix1 = parentTransform.Clone();
                }
                else
                {
                    Matrix local1 = parentTransform;
                    matrix1 = null;
                }
                Matrix local2 = matrix1;
                Matrix matrix3 = local2;
                if (local2 == null)
                {
                    Matrix local3 = local2;
                    matrix3 = new Matrix();
                }
                using (Matrix matrix = matrix3)
                {
                    using (Matrix matrix2 = this.CreateGlowChildTransform(this.ShapeTransform, shapeLayoutInfo.ShapeTransform))
                    {
                        if (matrix2 != null)
                        {
                            matrix.Multiply(matrix2);
                        }
                    }
                    return shapeLayoutInfo.GetGlowBounds(additionalSize, matrix);
                }
            };
            return this.GetTotalBounds(getBounds);
        }

        private Matrix GetInverseTransform()
        {
            if (base.ShapeTransform != null)
            {
                Matrix matrix = base.ShapeTransform.Clone();
                if (matrix.IsInvertible)
                {
                    matrix.Invert();
                    return matrix;
                }
            }
            return null;
        }

        public override GraphicsPath GetPreparedForReflectionFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            additionalSize.AddSize(this);
            GraphicsPath reflection = new GraphicsPath();
            base.AddShadowToReflection(reflection);
            reflection.Transform(shadowTransform);
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                if (info.UseForGroupEffects)
                {
                    using (GraphicsPath path2 = info.GetPreparedForShadowFigure(shadowTransform, rotateWithShape, additionalSize))
                    {
                        reflection.AddPath(path2, false);
                    }
                }
            }
            return reflection;
        }

        public override GraphicsPath GetPreparedForShadowFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize)
        {
            additionalSize.AddSize(this);
            GraphicsPath path = new GraphicsPath();
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                if (info.UseForGroupEffects)
                {
                    using (GraphicsPath path2 = info.GetPreparedForShadowFigure(shadowTransform, rotateWithShape, additionalSize))
                    {
                        path.AddPath(path2, false);
                    }
                }
            }
            return path;
        }

        private Rectangle GetTotalBounds(GetBoundsStrategy getBounds)
        {
            Rectangle empty = Rectangle.Empty;
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                if (info.UseForGroupEffects)
                {
                    Rectangle b = getBounds(info);
                    empty = empty.IsEmpty ? b : Rectangle.Union(empty, b);
                }
            }
            return empty;
        }

        public override bool HitTest(Point logicalPoint)
        {
            bool flag;
            using (List<IShapeLayoutInfo>.Enumerator enumerator = this.ShapeLayoutInfos.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IShapeLayoutInfo current = enumerator.Current;
                        if (!current.HitTest(logicalPoint))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return base.HitTest(logicalPoint);
                    }
                    break;
                }
            }
            return flag;
        }

        public override void SetupHitTestPen(float extendedWidth)
        {
            base.SetupHitTestPen(extendedWidth);
            foreach (IShapeLayoutInfo info in this.ShapeLayoutInfos)
            {
                info.SetupHitTestPen(extendedWidth);
            }
        }

        public Dictionary<int, ShapeLayoutInfo> InnerDrawingsOrder { get; private set; }

        public List<IShapeLayoutInfo> ShapeLayoutInfos { get; private set; }

        public override DevExpress.Office.Drawing.PenInfo PenInfo
        {
            get => 
                null;
            set
            {
            }
        }

        public bool HasMetafile { get; set; }

        private delegate Rectangle GetBoundsStrategy(IShapeLayoutInfo shapeLayoutInfo);
    }
}

