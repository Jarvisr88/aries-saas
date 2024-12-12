namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class FillEffect : IDrawingEffect
    {
        private readonly IDrawingFill fill;

        public FillEffect(IDrawingFill fill)
        {
            Guard.ArgumentNotNull(fill, "drawingFill");
            this.fill = fill;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new FillEffect(this.fill.CloneTo(documentModel));

        public override bool Equals(object obj)
        {
            FillEffect effect = obj as FillEffect;
            return ((effect != null) ? this.fill.Equals(effect.fill) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.fill.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IDrawingFill Fill =>
            this.fill;
    }
}

