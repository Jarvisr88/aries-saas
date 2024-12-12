namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class InnerShadowEffect : IDrawingEffect
    {
        private readonly DrawingColor color;
        private readonly OffsetShadowInfo info;
        private readonly long blurRadius;

        public InnerShadowEffect(DrawingColor color, OffsetShadowInfo info, long blurRadius)
        {
            Guard.ArgumentNotNull(color, "InnerShadowColor");
            Guard.ArgumentNotNull(info, "OffsetShadowInfo");
            this.color = color;
            this.info = info;
            this.blurRadius = blurRadius;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new InnerShadowEffect(this.color.CloneTo(documentModel), this.info.Clone(), this.blurRadius);

        public override bool Equals(object obj)
        {
            InnerShadowEffect effect = obj as InnerShadowEffect;
            return ((effect != null) ? (this.color.Equals(effect.color) && ((this.blurRadius == effect.blurRadius) && this.info.Equals(effect.info))) : false);
        }

        public override int GetHashCode() => 
            ((base.GetType().GetHashCode() ^ this.color.GetHashCode()) ^ this.blurRadius.GetHashCode()) ^ this.info.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor Color =>
            this.color;

        public OffsetShadowInfo OffsetShadow =>
            this.info;

        public long BlurRadius =>
            this.blurRadius;
    }
}

