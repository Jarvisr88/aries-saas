namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;
    using System.Collections.Generic;

    public class UndoableCollectionClearHistoryItem<T> : HistoryItem
    {
        private readonly UndoableCollection<T> collection;
        private List<T> itemList;

        public UndoableCollectionClearHistoryItem(IDocumentModel documentModel, UndoableCollection<T> collection) : base(documentModel.MainPart)
        {
            this.collection = collection;
            this.itemList = new List<T>();
            this.itemList.AddRange(this.collection.InnerList);
        }

        public override object GetTargetObject() => 
            this.collection;

        public override void Read(IHistoryReader reader)
        {
            base.Read(reader);
            this.itemList.Clear();
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                this.itemList.Add(this.collection.DeserializeItem(reader));
            }
        }

        protected override void RedoCore()
        {
            this.collection.ClearCore();
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            base.Register(writer, undone);
            if (this.collection.SupportsHistoryObjects)
            {
                foreach (T local in this.itemList)
                {
                    this.collection.RegisterItem(writer, local);
                }
            }
        }

        protected override void UndoCore()
        {
            this.collection.AddRangeCore(this.itemList);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.itemList.Count);
            foreach (T local in this.itemList)
            {
                this.collection.SerializeItem(writer, local);
            }
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            if (!undone && this.collection.SupportsHistoryObjects)
            {
                foreach (T local in this.itemList)
                {
                    writer.WriteObject(local);
                }
            }
        }
    }
}

