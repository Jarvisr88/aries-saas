namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class ActionAdjustablePointHistoryItem : ActionHistoryItem<AdjustablePoint>
    {
        public ActionAdjustablePointHistoryItem(IDocumentModelPart documentModelPart, AdjustablePoint oldValue, AdjustablePoint newValue, Action<AdjustablePoint> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.WriteAdjustablePoint(writer, base.OldValue);
            this.WriteAdjustablePoint(writer, base.NewValue);
        }

        private void WriteAdjustableCoordinate(IHistoryWriter writer, AdjustableCoordinate coordinate)
        {
            writer.Write(coordinate.ValueEMU);
            writer.Write(coordinate.GuideName);
        }

        private void WriteAdjustablePoint(IHistoryWriter writer, AdjustablePoint point)
        {
            this.WriteAdjustableCoordinate(writer, point.X);
            this.WriteAdjustableCoordinate(writer, point.Y);
        }
    }
}

