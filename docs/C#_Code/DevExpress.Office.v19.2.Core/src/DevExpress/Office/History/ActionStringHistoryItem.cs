namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionStringHistoryItem : ActionHistoryItem<string>
    {
        public ActionStringHistoryItem(IDocumentModelPart documentModelPart, string oldValue, string newValue, Action<string> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
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

