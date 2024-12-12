namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionFloatHistoryItem : ActionHistoryItem<float>
    {
        public ActionFloatHistoryItem(IDocumentModelPart documentModelPart, float oldValue, float newValue, Action<float> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
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

