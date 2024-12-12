namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class GlowEffect : IDrawingEffect
    {
        private readonly long radius;
        private readonly DrawingColor color;

        public GlowEffect(DrawingColor color, long radius)
        {
            Guard.ArgumentNotNull(color, "Glow Color");
            this.color = color;
            this.radius = radius;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new GlowEffect(this.color.CloneTo(documentModel), this.radius);

        public override bool Equals(object obj)
        {
            GlowEffect effect = obj as GlowEffect;
            return ((effect != null) ? ((this.radius == effect.radius) && this.color.Equals(effect.color)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.radius.GetHashCode()) ^ this.color.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor Color =>
            this.color;

        public long Radius =>
            this.radius;
    }
}

