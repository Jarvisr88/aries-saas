namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class TransformEffect : IDrawingEffect
    {
        private readonly ScalingFactorInfo scalingFactorInfo;
        private readonly SkewAnglesInfo skewAnglesInfo;
        private readonly CoordinateShiftInfo coordinateShiftInfo;

        public TransformEffect(ScalingFactorInfo scalingFactorInfo, SkewAnglesInfo skewAnglesInfo, CoordinateShiftInfo coordinateShiftInfo)
        {
            Guard.ArgumentNotNull(scalingFactorInfo, "scalingFactorInfo");
            Guard.ArgumentNotNull(skewAnglesInfo, "skewAnglesInfo");
            Guard.ArgumentNotNull(coordinateShiftInfo, "coordinateShiftInfo");
            this.scalingFactorInfo = scalingFactorInfo;
            this.skewAnglesInfo = skewAnglesInfo;
            this.coordinateShiftInfo = coordinateShiftInfo;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new TransformEffect(this.scalingFactorInfo.Clone(), this.skewAnglesInfo.Clone(), this.coordinateShiftInfo.Clone());

        public override bool Equals(object obj)
        {
            TransformEffect effect = obj as TransformEffect;
            return ((effect != null) ? (this.scalingFactorInfo.Equals(effect.scalingFactorInfo) && (this.skewAnglesInfo.Equals(effect.skewAnglesInfo) && this.coordinateShiftInfo.Equals(effect.coordinateShiftInfo))) : false);
        }

        public override int GetHashCode() => 
            ((base.GetType().GetHashCode() ^ this.scalingFactorInfo.GetHashCode()) ^ this.skewAnglesInfo.GetHashCode()) ^ this.coordinateShiftInfo.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ScalingFactorInfo ScalingFactor =>
            this.scalingFactorInfo;

        public SkewAnglesInfo SkewAngles =>
            this.skewAnglesInfo;

        public CoordinateShiftInfo CoordinateShift =>
            this.coordinateShiftInfo;
    }
}

