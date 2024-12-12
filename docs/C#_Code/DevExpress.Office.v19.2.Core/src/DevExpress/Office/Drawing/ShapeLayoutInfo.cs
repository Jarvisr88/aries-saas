namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class ShapeLayoutInfo : ShapeLayoutInfoBase
    {
        private ShapeEffectBuildHelper effectBuildHelper;
        private float softEdgesSize;

        public ShapeLayoutInfo() : this(true)
        {
        }

        public ShapeLayoutInfo(bool useForGroupEffects) : base(useForGroupEffects)
        {
        }

        public override Rectangle CalculateReflectionTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize) => 
            this.EffectBuildHelper.CalculateReflectionTransformBounds(rotateWithShape);

        public override Rectangle CalculateShadowTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize) => 
            this.EffectBuildHelper.CalculateShadowTransformBounds(rotateWithShape, additionalSize);

        public override HitTestInfoCollection CreateReflectionHitTestInfos(Matrix shadowTransform, bool rotateWithShape)
        {
            HitTestInfoCollection hitTestInfos = this.CreateHitTestInfosCore(shadowTransform, rotateWithShape);
            base.PopulateReflectionHitTestInfosFromShadow(hitTestInfos, shadowTransform, rotateWithShape);
            return hitTestInfos;
        }

        public override HitTestInfoCollection CreateShadowHitTestInfos(Matrix shadowTransform, bool rotateWithShape) => 
            this.CreateHitTestInfosCore(shadowTransform, rotateWithShape);

        public override void DrawBlurEffect(Graphics graphics)
        {
            base.DrawBackgroundBitmap(graphics, GraphicsPathCollectFlags.ExcludeBlur);
        }

        public override void DrawGlow(Graphics graphics, Color color, EffectAdditionalSize additionalSize, Matrix shapeTransform)
        {
            this.EffectBuildHelper.DrawGlow(graphics, color, additionalSize, shapeTransform);
        }

        public override void DrawReflection(Graphics graphics, bool applyTransform)
        {
            base.DrawCore(graphics, applyTransform, GraphicsPathCollectFlags.ExcludeReflection);
        }

        public override void DrawShadow(Graphics graphics, bool applyTransform)
        {
            base.DrawCore(graphics, applyTransform, GraphicsPathCollectFlags.ExcludeTextRectangle | GraphicsPathCollectFlags.ExcludeReflection | GraphicsPathCollectFlags.ExcludeOuterShadow);
        }

        public override Rectangle GetBlurEffectBounds(Matrix parentTransform, bool applyTransform, EffectAdditionalSize additionalSize) => 
            this.EffectBuildHelper.GetBlurEffectBounds(parentTransform, applyTransform, additionalSize);

        protected virtual ShapeEffectBuildHelper GetEffectBuilder() => 
            new ShapeEffectBuildHelper(this);

        public override Rectangle GetGlowBounds(EffectAdditionalSize additionalSize, Matrix parentTransform) => 
            this.EffectBuildHelper.GetGlowBounds(additionalSize, parentTransform);

        protected internal virtual PathInfoBase[] GetPathInfos() => 
            base.Paths.GetPathInfos<PathInfo>();

        public override GraphicsPath GetPreparedForReflectionFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize) => 
            this.EffectBuildHelper.GetPreparedForReflectionFigure(shadowTransform, rotateWithShape, additionalSize);

        public override GraphicsPath GetPreparedForShadowFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize) => 
            this.EffectBuildHelper.GetPreparedForShadowFigure(shadowTransform, rotateWithShape, additionalSize);

        public static ShapeLayoutInfo GetShapeLayoutInfo(ShapeProperties shapeProperties, bool preferBitmap) => 
            !shapeProperties.IsConnectionShape() ? (!(preferBitmap ? ShapeRenderHelper.RenderAsImage(shapeProperties) : ShapeRenderHelper.RenderAsMetafile(shapeProperties)) ? new ShapeLayoutInfo() : new PictureImageLayoutInfo()) : new ConnectionShapeLayoutInfo(true);

        public static ShapeLayoutInfo GetShapeLayoutInfo(bool useForGroupEffects, bool isConnectionShape) => 
            isConnectionShape ? new ConnectionShapeLayoutInfo(useForGroupEffects) : new ShapeLayoutInfo(useForGroupEffects);

        public Rectangle GetSoftEdgeBounds() => 
            this.EffectBuildHelper.GetSoftEdgeBounds();

        private float GetSoftEdgesSize()
        {
            if (this.softEdgesSize == 0f)
            {
                SoftEdgePathInfo pathInfo = base.Paths.GetPathInfo<SoftEdgePathInfo>();
                if (pathInfo != null)
                {
                    this.softEdgesSize = pathInfo.BlurRadiusInLayoutUnits;
                }
            }
            return this.softEdgesSize;
        }

        protected ShapeEffectBuildHelper EffectBuildHelper
        {
            get
            {
                ShapeEffectBuildHelper effectBuildHelper = this.effectBuildHelper;
                if (this.effectBuildHelper == null)
                {
                    ShapeEffectBuildHelper local1 = this.effectBuildHelper;
                    effectBuildHelper = this.effectBuildHelper = this.GetEffectBuilder();
                }
                return effectBuildHelper;
            }
        }

        public override DevExpress.Office.Drawing.PenInfo PenInfo { get; set; }

        public override float SoftEdgesSize =>
            this.GetSoftEdgesSize();
    }
}

