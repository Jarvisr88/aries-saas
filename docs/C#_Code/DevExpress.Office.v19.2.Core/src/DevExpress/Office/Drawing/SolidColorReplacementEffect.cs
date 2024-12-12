namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class SolidColorReplacementEffect : IDrawingEffect
    {
        private readonly DrawingColor color;

        public SolidColorReplacementEffect(DrawingColor color)
        {
            Guard.ArgumentNotNull(color, "ClrRepl Color");
            this.color = color;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new SolidColorReplacementEffect(this.color.CloneTo(documentModel));

        public override bool Equals(object obj)
        {
            SolidColorReplacementEffect effect = obj as SolidColorReplacementEffect;
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

