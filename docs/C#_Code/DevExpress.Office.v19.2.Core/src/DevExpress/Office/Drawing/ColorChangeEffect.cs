namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class ColorChangeEffect : IDrawingEffect
    {
        private readonly DrawingColor colorFrom;
        private readonly DrawingColor colorTo;
        private bool useAlpha;

        public ColorChangeEffect(DrawingColor colorFrom, DrawingColor colorTo) : this(colorFrom, colorTo, true)
        {
        }

        public ColorChangeEffect(DrawingColor colorFrom, DrawingColor colorTo, bool useAlpha)
        {
            Guard.ArgumentNotNull(colorFrom, "ColorChange ColorFrom");
            Guard.ArgumentNotNull(colorTo, "ColorChange ColorTo");
            this.colorFrom = colorFrom;
            this.colorTo = colorTo;
            this.useAlpha = useAlpha;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new ColorChangeEffect(this.colorFrom.CloneTo(documentModel), this.colorTo.CloneTo(documentModel), this.useAlpha);

        public override bool Equals(object obj)
        {
            ColorChangeEffect effect = obj as ColorChangeEffect;
            return ((effect != null) ? ((this.useAlpha == effect.useAlpha) && (this.colorFrom.Equals(effect.colorFrom) && this.colorTo.Equals(effect.colorTo))) : false);
        }

        public override int GetHashCode() => 
            ((base.GetType().GetHashCode() ^ this.useAlpha.GetHashCode()) ^ this.colorFrom.GetHashCode()) ^ this.colorTo.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor ColorFrom =>
            this.colorFrom;

        public DrawingColor ColorTo =>
            this.colorTo;

        public bool UseAlpha =>
            this.useAlpha;
    }
}

