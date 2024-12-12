namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public class UndoableCollectionMoveHistoryItem<T> : HistoryItem
    {
        private readonly UndoableCollection<T> collection;
        private int sourceIndex;
        private int targetIndex;

        public UndoableCollectionMoveHistoryItem(IDocumentModel documentModel, UndoableCollection<T> collection, int sourceIndex, int targetIndex) : base(documentModel.MainPart)
        {
            this.collection = collection;
            this.sourceIndex = sourceIndex;
            this.targetIndex = targetIndex;
        }

        public override object GetTargetObject() => 
            this.collection;

        protected override void RedoCore()
        {
            this.collection.MoveCore(this.sourceIndex, this.targetIndex);
        }

        protected override void UndoCore()
        {
            this.collection.MoveCore(this.targetIndex, this.sourceIndex);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.sourceIndex);
            writer.Write(this.targetIndex);
        }
    }
}

