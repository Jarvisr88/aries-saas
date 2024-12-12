namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Office.Model;
    using System;

    public class GradientTileRectPropertyChangedHistoryItem : DrawingHistoryItem<DrawingGradientFill, RectangleOffset>
    {
        public GradientTileRectPropertyChangedHistoryItem(IDocumentModelPart documentModelPart, DrawingGradientFill owner, RectangleOffset oldValue, RectangleOffset newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetTileRectCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetTileRectCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            base.OldValue.Write(writer);
            base.NewValue.Write(writer);
        }
    }
}

