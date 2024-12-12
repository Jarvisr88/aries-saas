namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class Effect : IDrawingEffect
    {
        private readonly string reference = string.Empty;

        public Effect(string reference)
        {
            this.reference = reference;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new Effect(this.reference);

        public override bool Equals(object obj)
        {
            Effect effect = obj as Effect;
            return ((effect != null) ? (StringExtensions.CompareInvariantCultureIgnoreCase(this.reference, effect.reference) == 0) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.reference.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Reference =>
            this.reference;
    }
}

