namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class AlphaInverseEffect : IDrawingEffect
    {
        private readonly DrawingColor color;

        public AlphaInverseEffect(DrawingColor color)
        {
            Guard.ArgumentNotNull(color, "AlphaInverseColor");
            this.color = color;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new AlphaInverseEffect(this.color.CloneTo(documentModel));

        public override bool Equals(object obj)
        {
            AlphaInverseEffect effect = obj as AlphaInverseEffect;
            return ((effect != null) ? this.color.Equals(effect.color) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.color.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor Color =>
            this.color;
    }
}

