namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public abstract class ShapeLayoutInfoBase : IShapeLayoutInfo, IDisposable
    {
        private PathInfoCollection paths = new PathInfoCollection();
        private bool useForGroupEffects;
        private float glowSize;
        private float blurSize;
        private Matrix invertedShapeTransform;
        private Lazy<Pen> hitTestPen;
        private GraphicsPath selectionPath;
        private int selectionMargin;

        protected ShapeLayoutInfoBase(bool useForGroupEffects)
        {
            this.useForGroupEffects = useForGroupEffects;
            this.ScaleFactor = 1f;
        }

        protected internal void AddShadowToReflection(GraphicsPath reflection)
        {
            OuterShadowPathInfo pathInfo = this.Paths.GetPathInfo<OuterShadowPathInfo>();
            if (pathInfo != null)
            {
                using (GraphicsPath path = (GraphicsPath) pathInfo.GraphicsPath.Clone())
                {
                    int blurRadius = pathInfo.BlurRadius;
                    if ((blurRadius > 0) && GdipExtensions.AllowPathWidening(path))
                    {
                        using (Pen pen = new Pen(Color.Empty, (float) blurRadius))
                        {
                            pen.LineJoin = LineJoin.Round;
                            WidenHelper.Apply(path, pen);
                        }
                    }
                    reflection.AddPath(path, false);
                }
            }
        }

        public abstract Rectangle CalculateReflectionTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize);
        public abstract Rectangle CalculateShadowTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize);
        protected virtual HitTestInfoCollection CreateHitTestInfosCore(Matrix shadowTransform, bool rotateWithShape)
        {
            HitTestInfoCollection infos = new HitTestInfoCollection();
            foreach (PathInfo info in this.Paths.GetPathInfosAndDerived<PathInfo>())
            {
                GraphicsPath graphicsPath = (GraphicsPath) info.GraphicsPath.Clone();
                if (!rotateWithShape && (this.ShapeTransform != null))
                {
                    graphicsPath.Transform(this.ShapeTransform);
                }
                graphicsPath.Transform(shadowTransform);
                infos.Add(new HitTestInfo(graphicsPath, info.Fill != null));
            }
            return infos;
        }

        public abstract HitTestInfoCollection CreateReflectionHitTestInfos(Matrix shadowTransform, bool rotateWithShape);
        public abstract HitTestInfoCollection CreateShadowHitTestInfos(Matrix shadowTransform, bool rotateWithShape);
        public void Dispose()
        {
            this.DisposeCore();
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeCore()
        {
            if (this.PenInfo != null)
            {
                this.PenInfo.Dispose();
                this.PenInfo = null;
            }
            if (this.paths != null)
            {
                foreach (PathInfoBase base2 in this.paths)
                {
                    base2.Dispose();
                }
                this.paths = null;
            }
            if (this.ShapeTransform != null)
            {
                this.ShapeTransform.Dispose();
                this.ShapeTransform = null;
            }
            if (this.invertedShapeTransform != null)
            {
                this.invertedShapeTransform.Dispose();
                this.invertedShapeTransform = null;
            }
            if (this.hitTestPen != null)
            {
                if (this.hitTestPen.IsValueCreated && (this.hitTestPen.Value != null))
                {
                    this.hitTestPen.Value.Dispose();
                }
                this.hitTestPen = null;
            }
            if (this.selectionPath != null)
            {
                this.selectionPath.Dispose();
                this.selectionPath = null;
            }
        }

        public virtual void Draw(Graphics graphics)
        {
            this.DrawCore(graphics, true);
        }

        protected void DrawAndSkip(Graphics graphics, bool applyTransform, GraphicsPathCollectFlags excludeFlags)
        {
            foreach (PathInfoBase base2 in this.paths)
            {
                if (!this.paths.ShouldSkipPathInfo(base2, excludeFlags) && !base2.SkipDrawing)
                {
                    base2.SkipDrawing = true;
                    base2.Draw(graphics, this.PenInfo, applyTransform ? this.ShapeTransform : null);
                }
            }
        }

        protected internal void DrawBackgroundBitmap(Graphics graphics, GraphicsPathCollectFlags excludeFlags)
        {
            excludeFlags |= GraphicsPathCollectFlags.ExcludeTextRectangle | GraphicsPathCollectFlags.ExcludeReflection | GraphicsPathCollectFlags.ExcludeGlow | GraphicsPathCollectFlags.ExcludeOuterShadow;
            this.DrawAndSkip(graphics, true, excludeFlags);
        }

        public abstract void DrawBlurEffect(Graphics graphics);
        protected void DrawCore(Graphics graphics, bool applyTransform)
        {
            foreach (PathInfoBase base2 in this.paths)
            {
                if (!base2.SkipDrawing)
                {
                    base2.Draw(graphics, this.PenInfo, applyTransform ? this.ShapeTransform : null);
                }
            }
        }

        protected void DrawCore(Graphics graphics, bool applyTransform, GraphicsPathCollectFlags excludeFlags)
        {
            foreach (PathInfoBase base2 in this.paths)
            {
                if (!this.paths.ShouldSkipPathInfo(base2, excludeFlags) && !base2.SkipDrawing)
                {
                    base2.Draw(graphics, this.PenInfo, applyTransform ? this.ShapeTransform : null);
                }
            }
        }

        public abstract void DrawGlow(Graphics graphics, Color color, EffectAdditionalSize additionalSize, Matrix shapeTransform);
        public abstract void DrawReflection(Graphics graphics, bool applyTransform);
        public abstract void DrawShadow(Graphics graphics, bool applyTransform);
        public abstract Rectangle GetBlurEffectBounds(Matrix parentTransform, bool applyTransform, EffectAdditionalSize additionalSize);
        public virtual Rectangle GetBounds()
        {
            bool flag1;
            if (this.Paths.Count == 0)
            {
                return Rectangle.Empty;
            }
            DevExpress.Office.Drawing.PenInfo penInfo = this.PenInfo;
            if (penInfo == null)
            {
                DevExpress.Office.Drawing.PenInfo local1 = penInfo;
                flag1 = false;
            }
            else
            {
                PenAlignment? nullable1;
                Pen pen = penInfo.Pen;
                if (pen != null)
                {
                    nullable1 = new PenAlignment?(pen.Alignment);
                }
                else
                {
                    Pen local2 = pen;
                    nullable1 = null;
                }
                PenAlignment? nullable = nullable1;
                PenAlignment outset = PenAlignment.Outset;
                flag1 = (((PenAlignment) nullable.GetValueOrDefault()) == outset) ? (nullable != null) : false;
            }
            bool flag = flag1;
            float width = 0f;
            if (flag)
            {
                Pen pen = this.PenInfo.Pen;
                width = pen.Width;
                pen.Width = Math.Max((float) 1f, (float) ((width * this.ScaleFactor) * 1.5f));
            }
            RectangleF realBounds = this.Paths[0].GetRealBounds(this.ShapeTransform, this.PenInfo);
            for (int i = 1; i < this.Paths.Count; i++)
            {
                RectangleF b = this.Paths[i].GetRealBounds(this.ShapeTransform, this.PenInfo);
                realBounds = RectangleF.Union(realBounds, b);
            }
            if (flag)
            {
                this.PenInfo.Pen.Width = width;
            }
            return Rectangle.FromLTRB((int) Math.Floor(this.GetValue(realBounds.X)), (int) Math.Floor(this.GetValue(realBounds.Y)), (int) Math.Ceiling(this.GetValue(realBounds.Right)), (int) Math.Ceiling(this.GetValue(realBounds.Bottom)));
        }

        public abstract Rectangle GetGlowBounds(EffectAdditionalSize additionalSize, Matrix parentTransform);
        protected Matrix GetInvertedShapeTransform()
        {
            if ((this.ShapeTransform == null) || !this.ShapeTransform.IsInvertible)
            {
                return null;
            }
            if (this.invertedShapeTransform == null)
            {
                this.invertedShapeTransform = this.ShapeTransform.Clone();
                this.invertedShapeTransform.Invert();
            }
            return this.invertedShapeTransform;
        }

        public abstract GraphicsPath GetPreparedForReflectionFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize);
        public abstract GraphicsPath GetPreparedForShadowFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize);
        private double GetValue(float value) => 
            (double) value;

        public virtual bool HitTest(Point logicalPoint)
        {
            bool flag;
            using (List<PathInfoBase>.Enumerator enumerator = this.Paths.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PathInfoBase current = enumerator.Current;
                        if (!current.AllowHitTest || !current.HitTest(logicalPoint, this.HitTestPen, this.GetInvertedShapeTransform()))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected void PopulateReflectionHitTestInfosFromShadow(HitTestInfoCollection hitTestInfos, Matrix shadowTransform, bool rotateWithShape)
        {
            OuterShadowPathInfo pathInfo = this.Paths.GetPathInfo<OuterShadowPathInfo>();
            if (pathInfo != null)
            {
                foreach (HitTestInfo info2 in pathInfo.HitTestInfos)
                {
                    GraphicsPath graphicsPath = (GraphicsPath) info2.GraphicsPath.Clone();
                    graphicsPath.Transform(shadowTransform);
                    hitTestInfos.Add(new HitTestInfo(graphicsPath, info2.HasFill));
                }
            }
        }

        protected void ResetSizes()
        {
            this.glowSize = 0f;
            this.blurSize = 0f;
        }

        public bool SelectionHitTest(Point logicalPoint)
        {
            Matrix invertedShapeTransform = this.GetInvertedShapeTransform();
            if (invertedShapeTransform != null)
            {
                logicalPoint = invertedShapeTransform.TransformPoint(logicalPoint);
            }
            return this.SelectionPath.IsOutlineVisible(logicalPoint, this.HitTestPen);
        }

        protected internal void SetGlowClip(Graphics graphics, float glowSize)
        {
            BlurPathInfo pathInfo = this.Paths.GetPathInfo<BlurPathInfo>();
            if ((pathInfo != null) && pathInfo.IsCropped)
            {
                Rectangle boundsWithoutTransform = pathInfo.BoundsWithoutTransform;
                int width = (int) (glowSize / 2f);
                boundsWithoutTransform.Inflate(width, width);
                graphics.SetClip(boundsWithoutTransform);
            }
        }

        public void SetSelectionMargin(int value)
        {
            this.selectionMargin = value;
        }

        public virtual void SetupHitTestPen(float extendedWidth)
        {
            Func<Pen> valueFactory = delegate {
                Pen pen;
                Pen pen2 = this.PenInfo?.Pen;
                if (pen2 == null)
                {
                    pen = new Pen(Color.Empty, extendedWidth);
                }
                else
                {
                    pen = (Pen) pen2.Clone();
                    pen.Width += extendedWidth;
                }
                return pen;
            };
            this.hitTestPen = new Lazy<Pen>(valueFactory);
        }

        private void UpdateBlurSize()
        {
            if (this.blurSize == 0f)
            {
                BlurPathInfo pathInfo = this.Paths.GetPathInfo<BlurPathInfo>();
                if (pathInfo != null)
                {
                    this.blurSize = 1.7f * pathInfo.BlurRadiusInLayoutUnits;
                }
            }
        }

        private void UpdateGlowSize()
        {
            if (this.glowSize == 0f)
            {
                GlowPathInfo pathInfo = this.Paths.GetPathInfo<GlowPathInfo>();
                if (pathInfo != null)
                {
                    this.glowSize = 2f * pathInfo.BlurRadiusInLayoutUnits;
                }
            }
        }

        public Rectangle InitialBounds { get; set; }

        public PathInfoCollection Paths
        {
            [DebuggerStepThrough]
            get => 
                this.paths;
            protected set => 
                this.paths = value;
        }

        public bool UseForGroupEffects
        {
            [DebuggerStepThrough]
            get => 
                this.useForGroupEffects;
            protected set => 
                this.useForGroupEffects = value;
        }

        public Matrix ShapeTransform { get; set; }

        public abstract DevExpress.Office.Drawing.PenInfo PenInfo { get; set; }

        public float PenWidth =>
            (this.PenInfo != null) ? this.PenInfo.PenWidth : 0f;

        public float ScaleFactor { get; set; }

        public float GlowSize
        {
            get
            {
                this.UpdateGlowSize();
                return this.glowSize;
            }
        }

        public float BlurSize
        {
            get
            {
                this.UpdateBlurSize();
                return this.blurSize;
            }
        }

        public virtual float SoftEdgesSize =>
            0f;

        protected Pen HitTestPen =>
            this.hitTestPen.Value;

        protected GraphicsPath SelectionPath
        {
            get
            {
                if (this.selectionPath == null)
                {
                    this.selectionPath = new GraphicsPath();
                    Rectangle initialBounds = this.InitialBounds;
                    initialBounds.Inflate(this.selectionMargin, this.selectionMargin);
                    this.selectionPath.AddRectangle(initialBounds);
                }
                return this.selectionPath;
            }
        }
    }
}

