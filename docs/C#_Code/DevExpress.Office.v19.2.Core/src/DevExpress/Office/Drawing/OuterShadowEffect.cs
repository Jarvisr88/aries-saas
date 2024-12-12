namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class OuterShadowEffect : IDrawingEffect
    {
        private readonly DrawingColor color;
        private readonly OuterShadowEffectInfo info;

        public OuterShadowEffect(DrawingColor color, OuterShadowEffectInfo info)
        {
            Guard.ArgumentNotNull(color, "OuterShadowColor");
            Guard.ArgumentNotNull(info, "OuterShadowEffectInfo");
            this.color = color;
            this.info = info;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new OuterShadowEffect(this.color.CloneTo(documentModel), this.info.Clone());

        public override bool Equals(object obj)
        {
            OuterShadowEffect effect = obj as OuterShadowEffect;
            return ((effect != null) ? (this.color.Equals(effect.color) && this.info.Equals(effect.info)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.color.GetHashCode()) ^ this.info.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor Color =>
            this.color;

        public OuterShadowEffectInfo Info =>
            this.info;
    }
}

