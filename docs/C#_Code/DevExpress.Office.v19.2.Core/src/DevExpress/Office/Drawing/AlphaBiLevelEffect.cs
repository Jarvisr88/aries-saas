namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class AlphaBiLevelEffect : IDrawingEffect
    {
        private readonly int thresh;

        public AlphaBiLevelEffect(int thresh)
        {
            this.thresh = thresh;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new AlphaBiLevelEffect(this.thresh);

        public override bool Equals(object obj)
        {
            AlphaBiLevelEffect effect = obj as AlphaBiLevelEffect;
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

