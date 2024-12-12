namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class ActionAdjustableAngleHistoryItem : ActionHistoryItem<AdjustableAngle>
    {
        public ActionAdjustableAngleHistoryItem(IDocumentModelPart documentModelPart, AdjustableAngle oldValue, AdjustableAngle newValue, Action<AdjustableAngle> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.WriteAdjustableAngle(writer, base.OldValue);
            this.WriteAdjustableAngle(writer, base.NewValue);
        }

        private void WriteAdjustableAngle(IHistoryWriter writer, AdjustableAngle coordinate)
        {
            writer.Write(coordinate.Value);
            writer.Write(coordinate.GuideName);
        }
    }
}

