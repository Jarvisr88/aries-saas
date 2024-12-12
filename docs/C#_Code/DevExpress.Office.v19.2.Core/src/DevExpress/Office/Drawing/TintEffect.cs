namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class TintEffect : IDrawingEffect
    {
        private int amount;
        private int hue;

        public TintEffect(int hue, int amount)
        {
            this.amount = amount;
            this.hue = hue;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new TintEffect(this.hue, this.amount);

        public override bool Equals(object obj)
        {
            TintEffect effect = obj as TintEffect;
            return ((effect != null) ? ((this.amount == effect.amount) && (this.hue == effect.hue)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.amount.GetHashCode()) ^ this.hue.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Amount =>
            this.amount;

        public int Hue =>
            this.hue;
    }
}

