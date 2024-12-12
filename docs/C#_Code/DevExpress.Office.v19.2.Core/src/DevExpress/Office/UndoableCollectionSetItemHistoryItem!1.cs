namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableCollectionSetItemHistoryItem<T> : HistoryItem
    {
        private readonly UndoableCollection<T> collection;
        private readonly int index;
        private readonly T newItem;
        private T oldItem;

        public UndoableCollectionSetItemHistoryItem(IDocumentModel documentModel, UndoableCollection<T> collection, int index, T item) : base(documentModel.MainPart)
        {
            this.collection = collection;
            this.newItem = item;
            this.index = index;
        }

        public override object GetTargetObject() => 
            this.collection;

        public override void Read(IHistoryReader reader)
        {
            base.Read(reader);
            this.oldItem = this.collection.DeserializeItem(reader);
        }

        protected override void RedoCore()
        {
            this.oldItem = this.collection[this.index];
            this.collection.SetItemCore(this.index, this.newItem);
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            base.Register(writer, undone);
            if (this.collection.SupportsHistoryObjects)
            {
                this.collection.RegisterItem(writer, this.newItem);
                this.collection.RegisterItem(writer, this.oldItem);
            }
        }

        protected override void UndoCore()
        {
            this.collection.SetItemCore(this.index, this.oldItem);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.index);
            this.collection.SerializeItem(writer, this.newItem);
            this.collection.SerializeItem(writer, this.oldItem);
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            Type type = typeof(T);
            if (this.collection.SupportsHistoryObjects)
            {
                writer.WriteObject(undone ? this.newItem : this.oldItem);
            }
        }
    }
}

