namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class LuminanceEffect : IDrawingEffect
    {
        private readonly int bright;
        private readonly int contrast;

        public LuminanceEffect(int bright, int contrast)
        {
            this.bright = bright;
            this.contrast = contrast;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new LuminanceEffect(this.bright, this.contrast);

        public override bool Equals(object obj)
        {
            LuminanceEffect effect = obj as LuminanceEffect;
            return ((effect != null) ? ((this.bright == effect.bright) && (this.contrast == effect.contrast)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.bright.GetHashCode()) ^ this.contrast.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Bright =>
            this.bright;

        public int Contrast =>
            this.contrast;
    }
}

