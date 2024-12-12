namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableCollectionInsertHistoryItem<T> : HistoryItem
    {
        private UndoableCollection<T> collection;
        private int index;
        private T item;

        public UndoableCollectionInsertHistoryItem(IDocumentModel documentModel, UndoableCollection<T> collection, int index, T item) : base(documentModel.MainPart)
        {
            this.collection = collection;
            this.index = index;
            this.item = item;
        }

        public override object GetTargetObject() => 
            this.collection;

        protected override void RedoCore()
        {
            this.collection.InsertCore(this.index, this.item);
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
            this.collection.RemoveAtCore(this.index);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.index);
            this.collection.SerializeItem(writer, this.item);
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            if (this.collection.SupportsHistoryObjects)
            {
                writer.WriteObject(this.item);
            }
        }
    }
}

