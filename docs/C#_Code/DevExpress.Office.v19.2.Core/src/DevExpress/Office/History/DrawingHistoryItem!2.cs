namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public abstract class DrawingHistoryItem<TOwner, TValue> : HistoryItem
    {
        private int index;
        private TOwner owner;
        private TValue oldValue;
        private TValue newValue;

        protected DrawingHistoryItem(IDocumentModelPart documentModelPart, TOwner owner, TValue oldValue, TValue newValue) : base(documentModelPart)
        {
            this.owner = owner;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        protected DrawingHistoryItem(IDocumentModelPart documentModelPart, TOwner owner, int index, TValue oldValue, TValue newValue) : this(documentModelPart, owner, oldValue, newValue)
        {
            this.index = index;
        }

        public override object GetTargetObject() => 
            this.owner;

        protected int Index =>
            this.index;

        protected TOwner Owner =>
            this.owner;

        protected TValue OldValue =>
            this.oldValue;

        protected TValue NewValue =>
            this.newValue;
    }
}

