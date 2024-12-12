namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using System;

    public abstract class DrawingUndoableIndexBasedObject<T> : UndoableIndexBasedObject<T, DocumentModelChangeActions> where T: ICloneable<T>, ISupportsCopyFrom<T>, ISupportsSizeOf
    {
        private InvalidateProxy innerParent;

        protected DrawingUndoableIndexBasedObject(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
            this.innerParent = new InvalidateProxy();
        }

        protected internal override void ApplyChanges(DocumentModelChangeActions changeActions)
        {
            this.InvalidateParent();
        }

        public void AssignInfo(T info)
        {
            base.SetIndexInitial(this.GetCache(base.DocumentModel).AddItem(info));
        }

        public virtual void CopyFrom(DrawingUndoableIndexBasedObject<T> sourceItem)
        {
            if (ReferenceEquals(sourceItem.DocumentModel, base.DocumentModel))
            {
                base.CopyFrom(sourceItem);
            }
            else
            {
                base.BeginUpdate();
                sourceItem.BeginUpdate();
                try
                {
                    base.Info.CopyFrom(sourceItem.Info);
                }
                finally
                {
                    base.EndUpdate();
                    sourceItem.EndUpdate();
                }
            }
        }

        public override DocumentModelChangeActions GetBatchUpdateChangeActions() => 
            DocumentModelChangeActions.None;

        protected void InvalidateParent()
        {
            this.innerParent.Invalidate();
        }

        protected internal IDrawingCache DrawingCache =>
            base.DocumentModel.DrawingCache;

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        protected InvalidateProxy InnerParent =>
            this.innerParent;
    }
}

