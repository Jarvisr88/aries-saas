namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class AlphaModulateFixedEffect : IDrawingEffect
    {
        private readonly int amount;

        public AlphaModulateFixedEffect(int amount)
        {
            this.amount = amount;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new AlphaModulateFixedEffect(this.amount);

        public override bool Equals(object obj)
        {
            AlphaModulateFixedEffect effect = obj as AlphaModulateFixedEffect;
            return ((effect != null) ? (this.amount == effect.amount) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.amount.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Amount =>
            this.amount;
    }
}

