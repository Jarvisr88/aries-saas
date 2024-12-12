namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class AlphaModulateEffect : IDrawingEffect
    {
        private readonly ContainerEffect container;

        public AlphaModulateEffect(ContainerEffect container)
        {
            Guard.ArgumentNotNull(container, "ContainerEffect");
            this.container = container;
        }

        public AlphaModulateEffect(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.container = new ContainerEffect(documentModel);
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new AlphaModulateEffect((ContainerEffect) this.container.CloneTo(documentModel));

        public override bool Equals(object obj)
        {
            AlphaModulateEffect effect = obj as AlphaModulateEffect;
            return ((effect != null) ? this.container.Equals(effect.container) : false);
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ this.container.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ContainerEffect Container =>
            this.container;
    }
}

