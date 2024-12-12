namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class ModelAdjustableRect : AdjustableRect
    {
        private IDocumentModelPart documentModelPart;

        public ModelAdjustableRect(IDocumentModelPart documentModelPart)
        {
            this.documentModelPart = documentModelPart;
        }

        protected internal override void SetCoordinate(int index, AdjustableCoordinate value)
        {
            AdjustableCoordinate objA = base.Coordinates[index];
            if (!ReferenceEquals(objA, value))
            {
                base.SetCoordinate(index, value);
                ModelAdjustableRectHistoryItem item = new ModelAdjustableRectHistoryItem(this, index, objA, value);
                this.DocumentModelPart.DocumentModel.History.Add(item);
                item.Execute();
            }
        }

        public IDocumentModelPart DocumentModelPart =>
            this.documentModelPart;
    }
}

