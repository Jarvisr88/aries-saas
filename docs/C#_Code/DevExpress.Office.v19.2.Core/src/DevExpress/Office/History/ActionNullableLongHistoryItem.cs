namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionNullableLongHistoryItem : ActionHistoryItem<long?>
    {
        public ActionNullableLongHistoryItem(IDocumentModelPart documentModelPart, long? oldValue, long? newValue, Action<long?> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.WriteNullableLong(writer, base.OldValue);
            this.WriteNullableLong(writer, base.NewValue);
        }

        private void WriteNullableLong(IHistoryWriter writer, long? value)
        {
            writer.Write(value != null);
            if (value != null)
            {
                writer.Write(value.Value);
            }
        }
    }
}

