namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class DuotoneEffect : IDrawingEffect
    {
        private readonly DrawingColor firstColor;
        private readonly DrawingColor secondColor;

        public DuotoneEffect(DrawingColor firstColor, DrawingColor secondColor)
        {
            Guard.ArgumentNotNull(firstColor, "Duotone first color");
            Guard.ArgumentNotNull(secondColor, "Duotone second color");
            this.firstColor = firstColor;
            this.secondColor = secondColor;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new DuotoneEffect(this.firstColor.CloneTo(documentModel), this.secondColor.CloneTo(documentModel));

        public override bool Equals(object obj)
        {
            DuotoneEffect effect = obj as DuotoneEffect;
            return ((effect != null) ? (this.firstColor.Equals(effect.firstColor) && this.secondColor.Equals(effect.secondColor)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.firstColor.GetHashCode()) ^ this.secondColor.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor FirstColor =>
            this.firstColor;

        public DrawingColor SecondColor =>
            this.secondColor;
    }
}

