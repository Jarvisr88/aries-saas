namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class OuterShadowEffectInfo : ICloneable<OuterShadowEffectInfo>
    {
        private readonly OffsetShadowInfo offsetShadowInfo;
        private readonly ScalingFactorInfo scalingFactorInfo;
        private readonly SkewAnglesInfo skewAnglesInfo;
        private readonly RectangleAlignType shadowAlignment;
        private readonly long blurRadius;
        private readonly bool rotateWithShape;

        public OuterShadowEffectInfo(OffsetShadowInfo offsetShadowInfo, ScalingFactorInfo scalingFactorInfo, SkewAnglesInfo skewAnglesInfo, RectangleAlignType shadowAlignment, long blurRadius, bool rotateWithShape)
        {
            Guard.ArgumentNotNull(offsetShadowInfo, "offsetShadowInfo");
            Guard.ArgumentNotNull(scalingFactorInfo, "scalingFactorInfo");
            Guard.ArgumentNotNull(skewAnglesInfo, "skewAnglesInfo");
            this.offsetShadowInfo = offsetShadowInfo;
            this.scalingFactorInfo = scalingFactorInfo;
            this.shadowAlignment = shadowAlignment;
            this.skewAnglesInfo = skewAnglesInfo;
            this.blurRadius = blurRadius;
            this.rotateWithShape = rotateWithShape;
        }

        public OuterShadowEffectInfo Clone() => 
            new OuterShadowEffectInfo(this.offsetShadowInfo.Clone(), this.scalingFactorInfo.Clone(), this.skewAnglesInfo.Clone(), this.shadowAlignment, this.blurRadius, this.rotateWithShape);

        public static OuterShadowEffectInfo Create(long blurRadius, long dist, int dir, int sx, int sy, int kx, int ky, RectangleAlignType align, bool rotWithShape) => 
            new OuterShadowEffectInfo(new OffsetShadowInfo(dist, dir), new ScalingFactorInfo(sx, sy), new SkewAnglesInfo(kx, ky), align, blurRadius, rotWithShape);

        public override bool Equals(object obj)
        {
            OuterShadowEffectInfo info = obj as OuterShadowEffectInfo;
            return ((info != null) ? (this.offsetShadowInfo.Equals(info.offsetShadowInfo) && (this.scalingFactorInfo.Equals(info.scalingFactorInfo) && (this.skewAnglesInfo.Equals(info.skewAnglesInfo) && ((this.shadowAlignment == info.shadowAlignment) && ((this.blurRadius == info.blurRadius) && (this.rotateWithShape == info.rotateWithShape)))))) : false);
        }

        public override int GetHashCode() => 
            (((((base.GetType().GetHashCode() ^ this.offsetShadowInfo.GetHashCode()) ^ this.scalingFactorInfo.GetHashCode()) ^ this.skewAnglesInfo.GetHashCode()) ^ this.shadowAlignment.GetHashCode()) ^ this.blurRadius.GetHashCode()) ^ this.rotateWithShape.GetHashCode();

        public OffsetShadowInfo OffsetShadow =>
            this.offsetShadowInfo;

        public ScalingFactorInfo ScalingFactor =>
            this.scalingFactorInfo;

        public SkewAnglesInfo SkewAngles =>
            this.skewAnglesInfo;

        public RectangleAlignType ShadowAlignment =>
            this.shadowAlignment;

        public long BlurRadius =>
            this.blurRadius;

        public bool RotateWithShape =>
            this.rotateWithShape;
    }
}

