namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class ReflectionEffect : IDrawingEffect
    {
        private readonly DevExpress.Office.Drawing.OuterShadowEffectInfo outerShadowEffectInfo;
        private readonly ReflectionOpacityInfo reflectionOpacityInfo;

        public ReflectionEffect(ReflectionOpacityInfo reflectionOpacityInfo, DevExpress.Office.Drawing.OuterShadowEffectInfo outerShadowEffectInfo)
        {
            Guard.ArgumentNotNull(reflectionOpacityInfo, "reflectionOpacityInfo");
            Guard.ArgumentNotNull(outerShadowEffectInfo, "OuterShadowEffectInfo");
            this.outerShadowEffectInfo = outerShadowEffectInfo;
            this.reflectionOpacityInfo = reflectionOpacityInfo;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new ReflectionEffect(this.reflectionOpacityInfo.Clone(), this.outerShadowEffectInfo.Clone());

        public override bool Equals(object obj)
        {
            ReflectionEffect effect = obj as ReflectionEffect;
            return ((effect != null) ? (this.outerShadowEffectInfo.Equals(effect.outerShadowEffectInfo) && this.reflectionOpacityInfo.Equals(effect.reflectionOpacityInfo)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.outerShadowEffectInfo.GetHashCode()) ^ this.reflectionOpacityInfo.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DevExpress.Office.Drawing.OuterShadowEffectInfo OuterShadowEffectInfo =>
            this.outerShadowEffectInfo;

        public ReflectionOpacityInfo ReflectionOpacity =>
            this.reflectionOpacityInfo;
    }
}

