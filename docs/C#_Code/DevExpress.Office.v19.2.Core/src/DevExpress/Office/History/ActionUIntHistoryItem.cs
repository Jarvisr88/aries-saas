namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    [CLSCompliant(false)]
    public class ActionUIntHistoryItem : ActionHistoryItem<uint>
    {
        public ActionUIntHistoryItem(IDocumentModelPart documentModelPart, uint oldValue, uint newValue, Action<uint> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write((long) ((ulong) base.OldValue));
            writer.Write((long) ((ulong) base.NewValue));
        }
    }
}

