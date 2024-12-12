namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class DocumentHistory : IDisposable
    {
        public const int ForceModifiedIndex = -2;
        private bool isDisposed;
        private List<HistoryItem> items = new List<HistoryItem>();
        private int unmodifiedIndex = -1;
        private int currentIndex = -1;
        private IDocumentModel documentModel;
        private CompositeHistoryItem transaction;
        private NotificationIdGenerator idGenerator;
        private int transactionLevel;
        private int disableCount;
        private bool suppressRaiseOperationComplete;
        private bool previousModifiedValue;
        private EventHandler onOperationCompleted;
        private EventHandler onModifiedChanged;

        public event EventHandler ModifiedChanged
        {
            add
            {
                this.onModifiedChanged += value;
            }
            remove
            {
                this.onModifiedChanged -= value;
            }
        }

        public event EventHandler OperationCompleted
        {
            add
            {
                this.onOperationCompleted += value;
            }
            remove
            {
                this.onOperationCompleted -= value;
            }
        }

        public DocumentHistory(IDocumentModel documentModel)
        {
            this.documentModel = documentModel;
            this.idGenerator = this.CreateIdGenerator();
        }

        public virtual void Add(HistoryItem item)
        {
            if (this.TransactionLevel != 0)
            {
                this.Transaction.AddItem(item);
            }
            else
            {
                this.InternalAdd(item);
            }
        }

        public virtual void AddEmptyOperation()
        {
        }

        public virtual HistoryItem BeginSyntaxHighlight() => 
            this.transaction;

        protected internal virtual void BeginTrackModifiedChanged()
        {
            this.previousModifiedValue = this.Modified;
        }

        public virtual HistoryItem BeginTransaction()
        {
            if (this.transactionLevel == 0)
            {
                this.transaction = this.CreateCompositeHistoryItem();
            }
            this.transactionLevel++;
            return this.transaction;
        }

        protected internal virtual void BeginUndoCurrent()
        {
        }

        private bool CheckIsTransactionChangeModifiedBackward(int index, int startInnerIndex)
        {
            if (index >= 0)
            {
                CompositeHistoryItem item = this.Items[index] as CompositeHistoryItem;
                if (item == null)
                {
                    return false;
                }
                IList<HistoryItem> items = item.Items;
                for (int i = startInnerIndex; i >= 0; i--)
                {
                    if (items[i].ChangeModified)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckIsTransactionChangeModifiedForward(int index, int startInnerIndex)
        {
            if (index >= 0)
            {
                CompositeHistoryItem item = this.Items[index] as CompositeHistoryItem;
                if (item == null)
                {
                    return false;
                }
                IList<HistoryItem> items = item.Items;
                int count = items.Count;
                for (int i = startInnerIndex; i < count; i++)
                {
                    if (items[i].ChangeModified)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Clear()
        {
            this.ClearCore(false);
        }

        private void ClearCore(bool disposeOnlyCutOffItems)
        {
            this.DisposeContent(disposeOnlyCutOffItems);
            if (this.transactionLevel > 0)
            {
                this.transaction = this.CreateCompositeHistoryItem();
            }
            this.items.Clear();
            this.BeginTrackModifiedChanged();
            try
            {
                this.currentIndex = -1;
                this.unmodifiedIndex = -1;
            }
            finally
            {
                this.EndTrackModifiedChanged();
            }
        }

        protected internal virtual HistoryItem CommitAsSingleItem() => 
            this.CommitAsSingleItemCore(this.transaction[0]);

        protected internal virtual HistoryItem CommitAsSingleItemCore(HistoryItem singleItem)
        {
            this.Add(singleItem);
            return singleItem;
        }

        public virtual HistoryItem CommitTransaction()
        {
            int count = this.transaction.Count;
            if (count > 0)
            {
                if (count == 1)
                {
                    HistoryItem item = this.CommitAsSingleItem();
                    if (item != null)
                    {
                        return item;
                    }
                }
                this.Add(this.transaction);
            }
            return this.transaction;
        }

        protected internal virtual CompositeHistoryItem CreateCompositeHistoryItem() => 
            new CompositeHistoryItem(this.documentModel.MainPart);

        protected virtual NotificationIdGenerator CreateIdGenerator() => 
            new NotificationIdGenerator();

        internal void CutOffHistory()
        {
            int index = this.currentIndex + 1;
            if (index < this.Count)
            {
                this.OnCutOffHistory();
            }
            while (index < this.Count)
            {
                this[index].Dispose();
                this.items.RemoveAt(index);
            }
            if (this.unmodifiedIndex > this.currentIndex)
            {
                this.unmodifiedIndex = -2;
            }
        }

        public void DisableHistory()
        {
            this.disableCount++;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeContent(false);
            }
            this.isDisposed = true;
        }

        private void DisposeContent(bool cutOffItemsOnly)
        {
            if (this.transaction != null)
            {
                this.transaction.Dispose();
            }
            if (cutOffItemsOnly)
            {
                this.CutOffHistory();
            }
            else
            {
                int count = this.Count;
                for (int i = 0; i < count; i++)
                {
                    this[i].Dispose();
                }
            }
        }

        public void EnableHistory()
        {
            if (this.disableCount > 0)
            {
                this.disableCount--;
            }
        }

        public virtual void EndSyntaxHighlight()
        {
        }

        protected internal virtual void EndTrackModifiedChanged()
        {
            if (this.previousModifiedValue != this.Modified)
            {
                this.RaiseModifiedChanged();
            }
        }

        public virtual HistoryItem EndTransaction()
        {
            if (this.transactionLevel > 0)
            {
                this.transactionLevel--;
                if (this.transactionLevel == 0)
                {
                    HistoryItem item = this.CommitTransaction();
                    this.transaction = null;
                    return item;
                }
            }
            return this.transaction;
        }

        protected internal virtual void EndUndoCurrent()
        {
        }

        public int GetNotificationId() => 
            this.idGenerator.GenerateId();

        public virtual bool HasChangesInCurrentTransaction() => 
            (this.transactionLevel > 0) ? (this.transaction.Count > 0) : false;

        private void InternalAdd(HistoryItem item)
        {
            if (!this.IsHistoryDisabled)
            {
                this.CutOffHistory();
                this.items.Add(item);
                this.BeginTrackModifiedChanged();
                try
                {
                    this.currentIndex++;
                }
                finally
                {
                    this.EndTrackModifiedChanged();
                }
            }
            if (!this.suppressRaiseOperationComplete)
            {
                this.RaiseOperationCompleted();
            }
        }

        public bool IsModified(int unmodifiedIndex) => 
            this.IsModified(unmodifiedIndex, -1);

        public bool IsModified(int unmodifiedIndex, int unmodifiedTransactionIndex)
        {
            if (this.CurrentIndex == unmodifiedIndex)
            {
                return ((unmodifiedTransactionIndex >= 0) && this.CheckIsTransactionChangeModifiedBackward(this.CurrentIndex, unmodifiedTransactionIndex));
            }
            if (unmodifiedIndex < -1)
            {
                return true;
            }
            if (unmodifiedIndex >= this.CurrentIndex)
            {
                if (unmodifiedTransactionIndex >= 0)
                {
                    this.CheckIsTransactionChangeModifiedBackward(unmodifiedIndex, unmodifiedTransactionIndex);
                    unmodifiedIndex--;
                }
                for (int i = this.CurrentIndex + 1; (i <= unmodifiedIndex) && (i < this.Count); i++)
                {
                    if (this.items[i].ChangeModified)
                    {
                        return true;
                    }
                }
            }
            else
            {
                unmodifiedIndex++;
                if (unmodifiedTransactionIndex >= 0)
                {
                    if (this.CheckIsTransactionChangeModifiedForward(unmodifiedIndex, unmodifiedTransactionIndex + 1))
                    {
                        return true;
                    }
                    unmodifiedIndex++;
                }
                for (int i = unmodifiedIndex; (i <= this.CurrentIndex) && (i < this.Count); i++)
                {
                    if (this.items[i].ChangeModified)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected virtual void OnCutOffHistory()
        {
        }

        protected internal virtual void OnEndUndoCore()
        {
        }

        public virtual void RaiseModifiedChanged()
        {
            if (this.onModifiedChanged != null)
            {
                this.onModifiedChanged(this, EventArgs.Empty);
            }
        }

        public virtual void RaiseOperationCompleted()
        {
            if (this.onOperationCompleted != null)
            {
                this.onOperationCompleted(this, EventArgs.Empty);
            }
        }

        public void Redo()
        {
            if (this.CanRedo)
            {
                this.DisableHistory();
                try
                {
                    this.documentModel.BeginUpdate();
                    try
                    {
                        this.BeginTrackModifiedChanged();
                        try
                        {
                            this.RedoCore();
                        }
                        finally
                        {
                            this.EndTrackModifiedChanged();
                        }
                    }
                    finally
                    {
                        this.documentModel.EndUpdate();
                    }
                }
                finally
                {
                    this.EnableHistory();
                }
            }
        }

        protected internal virtual void RedoCore()
        {
            this.currentIndex++;
            this.Current.Redo();
            this.RaiseOperationCompleted();
        }

        protected internal virtual void SetModifiedTextAppended(bool forceRaiseModifiedChanged)
        {
            if (!this.Modified)
            {
                this.unmodifiedIndex--;
                this.RaiseModifiedChanged();
            }
        }

        protected internal void SetTransaction(CompositeHistoryItem value)
        {
            this.transaction = value;
        }

        protected internal void SetTransactionLevel(int value)
        {
            this.transactionLevel = value;
        }

        public void SmartClear()
        {
            this.ClearCore(true);
        }

        public void Undo()
        {
            if (this.CanUndo)
            {
                this.DisableHistory();
                try
                {
                    this.documentModel.BeginUpdate();
                    try
                    {
                        this.BeginTrackModifiedChanged();
                        try
                        {
                            this.UndoCore();
                        }
                        finally
                        {
                            this.EndTrackModifiedChanged();
                        }
                    }
                    finally
                    {
                        this.documentModel.EndUpdate();
                    }
                }
                finally
                {
                    this.EnableHistory();
                }
            }
        }

        protected internal virtual void UndoCore()
        {
            this.BeginUndoCurrent();
            this.Current.Undo();
            this.currentIndex--;
            this.EndUndoCurrent();
            this.OnEndUndoCore();
            this.RaiseOperationCompleted();
        }

        public bool IsDisposed =>
            this.isDisposed;

        public List<HistoryItem> Items =>
            this.items;

        internal bool IsHistoryDisabled =>
            this.disableCount > 0;

        public HistoryItem this[int index] =>
            this.items[index];

        public int Count =>
            this.items.Count;

        public int CurrentIndex
        {
            get => 
                this.currentIndex;
            set => 
                this.currentIndex = value;
        }

        public int UnmodifiedIndex
        {
            get => 
                this.unmodifiedIndex;
            set => 
                this.unmodifiedIndex = value;
        }

        public HistoryItem Current =>
            ((this.CurrentIndex < 0) || (this.CurrentIndex >= this.Count)) ? null : this[this.CurrentIndex];

        public int TransactionLevel =>
            this.transactionLevel;

        public bool CanUndo =>
            this.CurrentIndex >= 0;

        public bool CanRedo =>
            (this.Count > 0) && (this.CurrentIndex < (this.Count - 1));

        public CompositeHistoryItem Transaction =>
            this.transaction;

        public virtual bool Modified
        {
            get => 
                this.IsModified(this.unmodifiedIndex);
            set
            {
                if (value != this.Modified)
                {
                    this.unmodifiedIndex = !value ? this.CurrentIndex : -2;
                    this.RaiseModifiedChanged();
                }
            }
        }

        public bool SuppressRaiseOperationComplete
        {
            get => 
                this.suppressRaiseOperationComplete;
            set => 
                this.suppressRaiseOperationComplete = value;
        }

        protected IDocumentModel DocumentModel =>
            this.documentModel;
    }
}

