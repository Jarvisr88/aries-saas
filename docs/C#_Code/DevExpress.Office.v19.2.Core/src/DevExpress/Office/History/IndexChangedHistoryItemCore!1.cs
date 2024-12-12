namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public abstract class IndexChangedHistoryItemCore<TActions> : HistoryItem where TActions: struct
    {
        private int oldIndex;
        private int newIndex;
        private TActions changeActions;

        protected IndexChangedHistoryItemCore(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public abstract IIndexBasedObject<TActions> GetObject();
        public override void Read(IHistoryReader reader)
        {
            this.OldIndex = reader.ReadInt32();
            this.NewIndex = reader.ReadInt32();
            if (this.ChangeActions is IConvertToInt<TActions>)
            {
                this.ChangeActions = ((IConvertToInt<TActions>) this.ChangeActions).FromInt(reader.ReadInt32());
            }
            else
            {
                this.ChangeActions = (TActions) Enum.ToObject(typeof(TActions), reader.ReadInt32());
            }
        }

        protected override void RedoCore()
        {
            this.GetObject().SetIndex(this.NewIndex, this.ChangeActions);
        }

        protected override void UndoCore()
        {
            this.GetObject().SetIndex(this.OldIndex, this.ChangeActions);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.OldIndex);
            writer.Write(this.NewIndex);
            if (this.ChangeActions is IConvertToInt<TActions>)
            {
                writer.Write(((IConvertToInt<TActions>) this.ChangeActions).ToInt());
            }
            else
            {
                writer.Write(Convert.ToInt32(this.ChangeActions));
            }
        }

        public int OldIndex
        {
            get => 
                this.oldIndex;
            set => 
                this.oldIndex = value;
        }

        public int NewIndex
        {
            get => 
                this.newIndex;
            set => 
                this.newIndex = value;
        }

        public TActions ChangeActions
        {
            get => 
                this.changeActions;
            set => 
                this.changeActions = value;
        }
    }
}

