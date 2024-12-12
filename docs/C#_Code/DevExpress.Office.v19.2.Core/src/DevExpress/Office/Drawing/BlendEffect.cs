namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class BlendEffect : IDrawingEffect
    {
        private readonly DevExpress.Office.Drawing.BlendMode blendMode;
        private readonly ContainerEffect container;

        public BlendEffect(ContainerEffect container, DevExpress.Office.Drawing.BlendMode blendMode)
        {
            Guard.ArgumentNotNull(container, "DrawingEffectContainer");
            this.container = container;
            this.blendMode = blendMode;
        }

        public BlendEffect(IDocumentModel documentModel, DevExpress.Office.Drawing.BlendMode blendMode)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.container = new ContainerEffect(documentModel);
            this.blendMode = blendMode;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new BlendEffect((ContainerEffect) this.container.CloneTo(documentModel), this.blendMode);

        public override bool Equals(object obj)
        {
            BlendEffect effect = obj as BlendEffect;
            return ((effect != null) ? ((this.blendMode == effect.blendMode) && this.container.Equals(effect.container)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.blendMode.GetHashCode()) ^ this.container.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DevExpress.Office.Drawing.BlendMode BlendMode =>
            this.blendMode;

        public ContainerEffect Container =>
            this.container;
    }
}

