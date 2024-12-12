namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionBooleanHistoryItem : ActionHistoryItem<bool>
    {
        public ActionBooleanHistoryItem(IDocumentModelPart documentModelPart, bool oldValue, bool newValue, Action<bool> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
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

