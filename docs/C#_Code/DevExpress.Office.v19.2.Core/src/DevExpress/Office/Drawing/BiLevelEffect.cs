namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class BiLevelEffect : IDrawingEffect
    {
        private readonly int thresh;

        public BiLevelEffect(int thresh)
        {
            this.thresh = thresh;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new BiLevelEffect(this.thresh);

        public override bool Equals(object obj)
        {
            BiLevelEffect effect = obj as BiLevelEffect;
            return ((effect != null) ? (this.thresh == effect.thresh) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.thresh.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Thresh =>
            this.thresh;
    }
}

