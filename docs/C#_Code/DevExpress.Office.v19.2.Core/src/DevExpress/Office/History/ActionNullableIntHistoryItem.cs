namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionNullableIntHistoryItem : ActionHistoryItem<int?>
    {
        public ActionNullableIntHistoryItem(IDocumentModelPart documentModelPart, int? oldValue, int? newValue, Action<int?> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.WriteNullableInt(writer, base.OldValue);
            this.WriteNullableInt(writer, base.NewValue);
        }

        private void WriteNullableInt(IHistoryWriter writer, int? value)
        {
            writer.Write(value != null);
            if (value != null)
            {
                writer.Write(value.Value);
            }
        }
    }
}

