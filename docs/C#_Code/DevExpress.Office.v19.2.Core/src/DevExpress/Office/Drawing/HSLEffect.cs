namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class HSLEffect : IDrawingEffect
    {
        private readonly int hue;
        private readonly int saturation;
        private readonly int luminance;

        public HSLEffect(int hue, int saturation, int luminance)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.luminance = luminance;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new HSLEffect(this.hue, this.saturation, this.luminance);

        public override bool Equals(object obj)
        {
            HSLEffect effect = obj as HSLEffect;
            return ((effect != null) ? ((this.hue == effect.hue) && ((this.saturation == effect.saturation) && (this.luminance == effect.luminance))) : false);
        }

        public override int GetHashCode() => 
            ((base.GetType().GetHashCode() ^ this.hue.GetHashCode()) ^ this.saturation.GetHashCode()) ^ this.luminance.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Hue =>
            this.hue;

        public int Saturation =>
            this.saturation;

        public int Luminance =>
            this.luminance;
    }
}

