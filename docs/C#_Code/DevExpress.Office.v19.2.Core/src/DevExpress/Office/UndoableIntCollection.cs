namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableIntCollection : UndoableCollection<int>
    {
        public UndoableIntCollection(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public override int DeserializeItem(IHistoryReader reader) => 
            reader.ReadInt32();

        protected internal override void SerializeItem(IHistoryWriter writer, int item)
        {
            writer.Write(item);
        }

        protected internal override bool SupportsHistoryObjects =>
            false;
    }
}

