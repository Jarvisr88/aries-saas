namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class AlphaOutsetEffect : IDrawingEffect
    {
        private readonly long radius;

        public AlphaOutsetEffect()
        {
        }

        public AlphaOutsetEffect(long radius)
        {
            this.radius = radius;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new AlphaOutsetEffect(this.radius);

        public override bool Equals(object obj)
        {
            AlphaOutsetEffect effect = obj as AlphaOutsetEffect;
            return ((effect != null) ? (this.radius == effect.radius) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.radius.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public long Radius =>
            this.radius;
    }
}

