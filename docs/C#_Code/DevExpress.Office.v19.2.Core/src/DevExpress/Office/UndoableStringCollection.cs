namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableStringCollection : UndoableCollection<string>
    {
        public UndoableStringCollection(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public override string DeserializeItem(IHistoryReader reader) => 
            reader.ReadString();

        protected internal override void SerializeItem(IHistoryWriter writer, string item)
        {
            writer.Write(item);
        }

        protected internal override bool SupportsHistoryObjects =>
            false;
    }
}

