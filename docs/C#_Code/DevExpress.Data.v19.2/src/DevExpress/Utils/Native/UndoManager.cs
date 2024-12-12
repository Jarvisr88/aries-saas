namespace DevExpress.Utils.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class UndoManager : DisposableObject, IDisposable
    {
        private const int DefaultQueuePosition = -1;
        private readonly List<IHistoryItem> commands = new List<IHistoryItem>();
        private readonly HashSet<IHistoryItem> lockedCommands = new HashSet<IHistoryItem>();
        private int queuePosition = -1;
        private CompositeHistoryItem transaction;
        private HistoryDisposingStrategy disposingStrategy = HistoryDisposingStrategy.UndoneItemsStrategy;
        private bool allowSaveActions = true;

        public event CommandExecutedEventHandler CommandExecuted;

        public void AddHistoryItem(HistoryItem item)
        {
            if (this.AllowSaveActions && (item != null))
            {
                this.lockedCommands.Add(item);
                if (this.transaction != null)
                {
                    this.PushToTransaction(item);
                }
                else
                {
                    this.PushItem(item);
                }
            }
            this.OnCommandExecuted(item, CommandAction.Execute);
        }

        public void BeginTransaction()
        {
            this.transaction = new CompositeHistoryItem();
        }

        public void CancelTransaction()
        {
            this.transaction = null;
            this.lockedCommands.Clear();
            this.Undo();
        }

        public bool CanDisposeNewValue(HistoryItem historyItem) => 
            this.disposingStrategy.CanDisposeNewValue && (historyItem.Command.CanDisposeNewValue && this.CanDisposeValue(historyItem, historyItem.NewValue));

        public bool CanDisposeOldValue(HistoryItem historyItem) => 
            this.disposingStrategy.CanDisposeOldValue && (historyItem.Command.CanDisposeOldValue && this.CanDisposeValue(historyItem, historyItem.OldValue));

        protected virtual bool CanDisposeValue(HistoryItem historyItem, object value) => 
            !this.lockedCommands.Contains(historyItem);

        public void ClearCommands()
        {
            this.queuePosition = -1;
            this.ClearCommandsCore();
        }

        private void ClearCommandsCore()
        {
            if (this.commands.Count > (this.queuePosition + 1))
            {
                for (int i = this.commands.Count - 1; i > this.queuePosition; i--)
                {
                    IHistoryItem item = this.commands[i];
                    this.commands.RemoveAt(i);
                    item.Dispose();
                }
            }
        }

        public void CommitTransaction()
        {
            if (this.transaction.HistoryItems.Count > 0)
            {
                this.PushItem(this.transaction);
            }
            this.transaction = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.disposingStrategy = HistoryDisposingStrategy.ExecutedItemsStrategy;
                int num = 0;
                while (true)
                {
                    if (num > this.queuePosition)
                    {
                        this.disposingStrategy = HistoryDisposingStrategy.UndoneItemsStrategy;
                        for (int i = this.queuePosition + 1; i < this.commands.Count; i++)
                        {
                            this.commands[i].Dispose();
                        }
                        break;
                    }
                    this.commands[num].Dispose();
                    num++;
                }
            }
            base.Dispose(disposing);
        }

        private IHistoryItem GetRedoItem()
        {
            IHistoryItem item = null;
            if (this.queuePosition < (this.commands.Count - 1))
            {
                this.queuePosition++;
                item = this.commands[this.queuePosition];
            }
            return item;
        }

        private IHistoryItem GetUndoItem()
        {
            IHistoryItem item = null;
            if (this.CanUndo)
            {
                item = this.commands[this.queuePosition];
                this.queuePosition--;
                if (this.queuePosition < -1)
                {
                    this.queuePosition = -1;
                }
            }
            return item;
        }

        private void OnCommandExecuted(IHistoryItem item, CommandAction commandAction)
        {
            if (this.CommandExecuted != null)
            {
                HistoryItem historyItem = item as HistoryItem;
                if (historyItem != null)
                {
                    this.CommandExecuted(new CommandExecutedEventArgs(item.ObjectToSelect, historyItem.Parameter, historyItem, commandAction));
                }
                else
                {
                    object objectToSelect = item?.ObjectToSelect;
                    this.CommandExecuted(new CommandExecutedEventArgs(objectToSelect));
                }
            }
        }

        private void PushItem(IHistoryItem info)
        {
            this.ClearCommandsCore();
            this.commands.Add(info);
            this.lockedCommands.Clear();
            this.queuePosition++;
        }

        private void PushToTransaction(IHistoryItem info)
        {
            this.transaction.HistoryItems.Add(info);
        }

        public void Redo()
        {
            IHistoryItem redoItem = this.GetRedoItem();
            if (redoItem != null)
            {
                redoItem.Redo();
                this.OnCommandExecuted(redoItem, CommandAction.Redo);
            }
        }

        public void Undo()
        {
            IHistoryItem undoItem = this.GetUndoItem();
            if (undoItem != null)
            {
                undoItem.Undo();
                this.OnCommandExecuted(undoItem, CommandAction.Undo);
            }
        }

        public bool CanUndo =>
            (this.commands.Count > 0) && (this.queuePosition > -1);

        public bool CanRedo =>
            (this.commands.Count > 0) && (this.queuePosition < (this.commands.Count - 1));

        public bool AllowSaveActions
        {
            get => 
                this.allowSaveActions;
            set => 
                this.allowSaveActions = value;
        }
    }
}

