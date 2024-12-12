namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public abstract class ActionHistoryItem<T> : HistoryItem
    {
        private readonly T oldValue;
        private readonly T newValue;
        private readonly Action<T> setValueAction;

        protected ActionHistoryItem(IDocumentModelPart documentModelPart, T oldValue, T newValue, Action<T> setValueAction) : base(documentModelPart)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.setValueAction = setValueAction;
        }

        public override object GetTargetObject() => 
            this.setValueAction.Target;

        protected override void RedoCore()
        {
            this.setValueAction(this.NewValue);
        }

        protected override void UndoCore()
        {
            this.setValueAction(this.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.setValueAction.Method.Name);
        }

        public T OldValue =>
            this.oldValue;

        public T NewValue =>
            this.newValue;

        public Action<T> SetValueAction =>
            this.setValueAction;
    }
}

