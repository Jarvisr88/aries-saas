namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionDoubleHistoryItem : ActionHistoryItem<double>
    {
        public ActionDoubleHistoryItem(IDocumentModelPart documentModelPart, double oldValue, double newValue, Action<double> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

