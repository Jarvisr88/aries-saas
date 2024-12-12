namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class AlphaReplaceEffect : IDrawingEffect
    {
        private readonly int alpha;

        public AlphaReplaceEffect(int alpha)
        {
            this.alpha = alpha;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new AlphaReplaceEffect(this.alpha);

        public override bool Equals(object obj)
        {
            AlphaReplaceEffect effect = obj as AlphaReplaceEffect;
            return ((effect != null) ? (this.alpha == effect.alpha) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.alpha.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Alpha =>
            this.alpha;
    }
}

