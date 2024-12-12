namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class FillOverlayEffect : IDrawingEffect
    {
        private readonly IDrawingFill fill;
        private readonly DevExpress.Office.Drawing.BlendMode blendMode;

        public FillOverlayEffect(IDrawingFill fill, DevExpress.Office.Drawing.BlendMode blendMode)
        {
            Guard.ArgumentNotNull(fill, "drawingFill");
            this.fill = fill;
            this.blendMode = blendMode;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new FillOverlayEffect(this.fill.CloneTo(documentModel), this.blendMode);

        public override bool Equals(object obj)
        {
            FillOverlayEffect effect = obj as FillOverlayEffect;
            return ((effect != null) ? (this.fill.Equals(effect.Fill) && (this.blendMode == effect.blendMode)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.fill.GetHashCode()) ^ this.blendMode.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DevExpress.Office.Drawing.BlendMode BlendMode =>
            this.blendMode;

        public IDrawingFill Fill =>
            this.fill;
    }
}

