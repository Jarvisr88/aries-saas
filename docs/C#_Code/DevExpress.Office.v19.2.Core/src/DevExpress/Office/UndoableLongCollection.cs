namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableLongCollection : UndoableCollection<long>
    {
        public UndoableLongCollection(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public override long DeserializeItem(IHistoryReader reader) => 
            reader.ReadInt64();

        protected internal override void SerializeItem(IHistoryWriter writer, long item)
        {
            writer.Write(item);
        }

        protected internal override bool SupportsHistoryObjects =>
            false;
    }
}

