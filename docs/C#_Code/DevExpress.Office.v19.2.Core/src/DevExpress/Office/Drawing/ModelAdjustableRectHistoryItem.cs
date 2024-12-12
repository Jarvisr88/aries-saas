namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class ModelAdjustableRectHistoryItem : DrawingHistoryItem<ModelAdjustableRect, AdjustableCoordinate>
    {
        private readonly ModelAdjustableRect rect;
        private readonly AdjustableCoordinate newValue;
        private readonly AdjustableCoordinate oldValue;
        private readonly int valueIndex;

        public ModelAdjustableRectHistoryItem(ModelAdjustableRect rect, int valueIndex, AdjustableCoordinate oldValue, AdjustableCoordinate newValue) : base(rect.DocumentModelPart, rect, oldValue, newValue)
        {
            this.rect = rect;
            this.valueIndex = valueIndex;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        protected override void RedoCore()
        {
            this.rect.SetCoordinate(this.valueIndex, this.newValue);
        }

        protected override void UndoCore()
        {
            this.rect.SetCoordinate(this.valueIndex, this.oldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.valueIndex);
            writer.Write(this.oldValue.ToString());
            writer.Write(this.newValue.ToString());
        }
    }
}

