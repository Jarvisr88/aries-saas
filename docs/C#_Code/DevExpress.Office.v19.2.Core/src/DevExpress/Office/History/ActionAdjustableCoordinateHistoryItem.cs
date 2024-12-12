namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class ActionAdjustableCoordinateHistoryItem : ActionHistoryItem<AdjustableCoordinate>
    {
        public ActionAdjustableCoordinateHistoryItem(IDocumentModelPart documentModelPart, AdjustableCoordinate oldValue, AdjustableCoordinate newValue, Action<AdjustableCoordinate> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.WriteAdjustableCoordinate(writer, base.OldValue);
            this.WriteAdjustableCoordinate(writer, base.NewValue);
        }

        private void WriteAdjustableCoordinate(IHistoryWriter writer, AdjustableCoordinate coordinate)
        {
            writer.Write(coordinate.ValueEMU);
            writer.Write(coordinate.GuideName);
        }
    }
}

