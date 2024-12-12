namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class SoftEdgeEffect : IDrawingEffect
    {
        private readonly long radius;

        public SoftEdgeEffect(long radius)
        {
            this.radius = radius;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new SoftEdgeEffect(this.radius);

        public override bool Equals(object obj)
        {
            SoftEdgeEffect effect = obj as SoftEdgeEffect;
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

