namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableCollectionAddHistoryItem<T> : HistoryItem
    {
        private readonly UndoableCollection<T> collection;
        private T item;

        public UndoableCollectionAddHistoryItem(IDocumentModel documentModel, UndoableCollection<T> collection, T item) : base(documentModel.MainPart)
        {
            this.collection = collection;
            this.item = item;
        }

        public override object GetTargetObject() => 
            this.collection;

        protected override void RedoCore()
        {
            this.collection.AddCore(this.item);
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
            int index = this.collection.Count - 1;
            this.collection.RemoveAtCore(index);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.collection.SerializeItem(writer, this.item);
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            if (undone && this.collection.SupportsHistoryObjects)
            {
                writer.WriteObject(this.item);
            }
        }
    }
}

