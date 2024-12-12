namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Office.Model;
    using System;

    public class GradientFillRectPropertyChangedHistoryItem : DrawingHistoryItem<DrawingGradientFill, RectangleOffset>
    {
        public GradientFillRectPropertyChangedHistoryItem(IDocumentModelPart documentModelPart, DrawingGradientFill owner, RectangleOffset oldValue, RectangleOffset newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetFillRectCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetFillRectCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            base.OldValue.Write(writer);
            base.NewValue.Write(writer);
        }
    }
}

