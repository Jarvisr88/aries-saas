namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class RelativeOffsetEffect : IDrawingEffect
    {
        private readonly int offsetX;
        private readonly int offsetY;

        public RelativeOffsetEffect(int offsetX, int offsetY)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new RelativeOffsetEffect(this.offsetX, this.offsetY);

        public override bool Equals(object obj)
        {
            RelativeOffsetEffect effect = obj as RelativeOffsetEffect;
            return ((effect != null) ? ((this.offsetX == effect.offsetX) && (this.offsetY == effect.offsetY)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.offsetX.GetHashCode()) ^ this.offsetY.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int OffsetX =>
            this.offsetX;

        public int OffsetY =>
            this.offsetY;
    }
}

