namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableCollectionRemoveAtHistoryItem<T> : HistoryItem
    {
        private UndoableCollection<T> collection;
        private int index;
        private T item;

        public UndoableCollectionRemoveAtHistoryItem(IDocumentModel documentModel, UndoableCollection<T> collection, int index) : base(documentModel.MainPart)
        {
            this.collection = collection;
            this.index = index;
        }

        public override object GetTargetObject() => 
            this.collection;

        public override void Read(IHistoryReader reader)
        {
            base.Read(reader);
            this.item = this.collection.DeserializeItem(reader);
        }

        protected override void RedoCore()
        {
            this.item = this.collection[this.index];
            this.collection.RemoveAtCore(this.index);
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            base.Register(writer, undone);
            if (this.collection.SupportsHistoryObjects)
            {
                this.collection.RegisterItem(writer, this.item);
            }
        }

        protected override void UndoCore()
        {
            this.collection.InsertCore(this.index, this.item);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.index);
            this.collection.SerializeItem(writer, this.item);
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            if (!undone && this.collection.SupportsHistoryObjects)
            {
                writer.WriteObject(this.item);
            }
        }

        public T Item =>
            this.item;
    }
}

