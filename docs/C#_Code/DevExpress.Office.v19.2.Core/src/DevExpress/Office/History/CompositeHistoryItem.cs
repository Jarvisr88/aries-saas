namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeHistoryItem : HistoryItem
    {
        private readonly List<HistoryItem> items;

        public CompositeHistoryItem(IDocumentModelPart part) : base(part)
        {
            this.items = new List<HistoryItem>();
        }

        public void AddItem(HistoryItem item)
        {
            this.items.Add(item);
        }

        public void AddRange(CompositeHistoryItem item)
        {
            this.items.AddRange(item.items);
        }

        protected internal virtual void Clear()
        {
            this.items.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                int count = this.items.Count;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= count)
                    {
                        this.items.Clear();
                        break;
                    }
                    this.items[num2].Dispose();
                    this.items[num2] = null;
                    num2++;
                }
            }
            base.Dispose(disposing);
        }

        public override void Read(IHistoryReader reader)
        {
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                HistoryItem item = reader.CreateHistoryItem();
                if (item != null)
                {
                    item.Read(reader);
                    this.items.Add(item);
                }
            }
        }

        protected override void RedoCore()
        {
            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                this[i].Redo();
            }
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            foreach (HistoryItem item in this.items)
            {
                item.Register(writer, undone);
            }
        }

        public void Rollback()
        {
            base.Undo();
            this.Clear();
        }

        protected override void UndoCore()
        {
            for (int i = this.items.Count - 1; i >= 0; i--)
            {
                this[i].Undo();
            }
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            int count = this.items.Count;
            writer.Write(count);
            for (int i = 0; i < count; i++)
            {
                HistoryItem historyItem = this.items[i];
                int typeCode = writer.GetTypeCode(historyItem);
                writer.Write(typeCode);
                if (typeCode >= 0)
                {
                    historyItem.Write(writer);
                }
            }
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            foreach (HistoryItem item in this.items)
            {
                item.WriteObjects(writer, undone);
            }
        }

        public override bool ChangeModified
        {
            get
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this.items[i].ChangeModified)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public HistoryItem this[int index] =>
            this.items[index];

        public int Count =>
            this.items.Count;

        public IList<HistoryItem> Items =>
            this.items;
    }
}

