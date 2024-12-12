namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class BlurEffect : IDrawingEffect
    {
        private readonly long radius;
        private readonly bool grow;

        public BlurEffect(long radius, bool grow)
        {
            this.radius = radius;
            this.grow = grow;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new BlurEffect(this.radius, this.grow);

        public override bool Equals(object obj)
        {
            BlurEffect effect = obj as BlurEffect;
            return ((effect != null) ? ((this.grow == effect.grow) && (this.radius == effect.radius)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.grow.GetHashCode()) ^ this.radius.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public long Radius =>
            this.radius;

        public bool Grow =>
            this.grow;
    }
}

