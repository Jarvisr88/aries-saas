namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public abstract class DrawingMultiIndexObject<TOwner, TActions> : MultiIndexObject<TOwner, TActions> where TOwner: DrawingMultiIndexObject<TOwner, TActions> where TActions: struct
    {
        private InvalidateProxy innerParent;
        private readonly IDocumentModel documentModel;

        protected DrawingMultiIndexObject(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.innerParent = new InvalidateProxy();
            this.documentModel = documentModel;
        }

        protected internal override void ApplyChanges(TActions actions)
        {
            this.InvalidateParent();
        }

        protected override IDocumentModel GetDocumentModel() => 
            this.documentModel;

        protected void InvalidateParent()
        {
            this.innerParent.Invalidate();
        }

        public IDrawingCache DrawingCache =>
            this.documentModel.DrawingCache;

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        protected ISupportsInvalidate InnerParent =>
            this.innerParent;
    }
}

