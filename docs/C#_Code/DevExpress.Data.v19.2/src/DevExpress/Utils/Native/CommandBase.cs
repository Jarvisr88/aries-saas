namespace DevExpress.Utils.Native
{
    using System;

    public abstract class CommandBase
    {
        private readonly DevExpress.Utils.Native.UndoManager undoManager;

        protected CommandBase(DevExpress.Utils.Native.UndoManager undoManager)
        {
            this.undoManager = undoManager;
        }

        public abstract bool CanExecute(object parameter);
        public void Execute(object parameter)
        {
            if (this.CanExecute(parameter))
            {
                HistoryItem item = this.ExecuteInternal(parameter);
                this.undoManager.AddHistoryItem(item);
            }
        }

        protected abstract HistoryItem ExecuteInternal(object parameter);
        public void Redo(HistoryItem historyItem)
        {
            this.RedoInternal(historyItem);
        }

        protected abstract void RedoInternal(HistoryItem historyItem);
        public void Undo(HistoryItem historyItem)
        {
            this.UndoInternal(historyItem);
        }

        protected abstract void UndoInternal(HistoryItem historyItem);

        protected internal virtual bool CanDisposeOldValue =>
            false;

        protected internal virtual bool CanDisposeNewValue =>
            false;

        public DevExpress.Utils.Native.UndoManager UndoManager =>
            this.undoManager;
    }
}

