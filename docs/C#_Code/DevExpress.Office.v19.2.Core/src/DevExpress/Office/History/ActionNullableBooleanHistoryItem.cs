namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionNullableBooleanHistoryItem : ActionHistoryItem<bool?>
    {
        public ActionNullableBooleanHistoryItem(IDocumentModelPart documentModelPart, bool? oldValue, bool? newValue, Action<bool?> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.WriteNullableBoolean(writer, base.OldValue);
            this.WriteNullableBoolean(writer, base.NewValue);
        }

        private void WriteNullableBoolean(IHistoryWriter writer, bool? value)
        {
            writer.Write(value != null);
            if (value != null)
            {
                writer.Write(value.Value);
            }
        }
    }
}

