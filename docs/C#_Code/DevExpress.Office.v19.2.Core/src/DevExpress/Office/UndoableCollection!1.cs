namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class UndoableCollection<T> : SimpleCollection<T>
    {
        private readonly IDocumentModelPart documentModelPart;
        private readonly bool supportsHistoryObjects;
        private UndoableCollectionAddEventHandler onAdd;
        private UndoableCollectionRemoveAtEventHandler onRemoveAt;
        private UndoableCollectionInsertEventHandler onInsert;
        private UndoableCollectionClearEventHandler onClear;
        private UndoableCollectionAddRangeEventHandler onAddRange;
        private UndoableCollectionMoveEventHandler onMove;
        private EventHandler onModified;

        public event EventHandler Modified
        {
            add
            {
                this.onModified += value;
            }
            remove
            {
                this.onModified -= value;
            }
        }

        public event UndoableCollectionAddEventHandler OnAdd
        {
            add
            {
                this.onAdd += value;
            }
            remove
            {
                this.onAdd -= value;
            }
        }

        public event UndoableCollectionAddRangeEventHandler OnAddRange
        {
            add
            {
                this.onAddRange += value;
            }
            remove
            {
                this.onAddRange -= value;
            }
        }

        public event UndoableCollectionClearEventHandler OnClear
        {
            add
            {
                this.onClear += value;
            }
            remove
            {
                this.onClear -= value;
            }
        }

        public event UndoableCollectionInsertEventHandler OnInsert
        {
            add
            {
                this.onInsert += value;
            }
            remove
            {
                this.onInsert -= value;
            }
        }

        public event UndoableCollectionMoveEventHandler OnMove
        {
            add
            {
                this.onMove += value;
            }
            remove
            {
                this.onMove -= value;
            }
        }

        public event UndoableCollectionRemoveAtEventHandler OnRemoveAt
        {
            add
            {
                this.onRemoveAt += value;
            }
            remove
            {
                this.onRemoveAt -= value;
            }
        }

        public UndoableCollection(IDocumentModelPart documentModelPart)
        {
            this.documentModelPart = documentModelPart;
            Type type = typeof(T);
            this.supportsHistoryObjects = type.IsClass || type.IsInterface;
        }

        public override int Add(T item)
        {
            Guard.ArgumentNotNull(item, "item");
            UndoableCollectionAddHistoryItem<T> item2 = new UndoableCollectionAddHistoryItem<T>(this.DocumentModel, (UndoableCollection<T>) this, item);
            this.DocumentModel.History.Add(item2);
            item2.Execute();
            return (base.InnerList.Count - 1);
        }

        public virtual int AddCore(T item)
        {
            this.RaiseAdd(item);
            return this.AddInternal(item);
        }

        public virtual int AddInternal(T item) => 
            base.Add(item);

        public virtual void AddRangeCore(IEnumerable<T> collection)
        {
            this.RaiseAddRange(collection);
            base.InnerList.AddRange(collection);
        }

        public virtual int AddWithoutHistoryAndNotifications(T item) => 
            this.AddWithoutNotification(item);

        public override void Clear()
        {
            if (base.Count != 0)
            {
                UndoableCollectionClearHistoryItem<T> item = new UndoableCollectionClearHistoryItem<T>(this.DocumentModel, (UndoableCollection<T>) this);
                this.DocumentModel.History.Add(item);
                item.Execute();
            }
        }

        public virtual void ClearCore()
        {
            this.RaiseClear();
            base.Clear();
        }

        protected virtual EventArgs CreateAddEventArgs(T item) => 
            new UndoableCollectionAddEventArgs<T>(item);

        protected virtual EventArgs CreateAddRangeEventArgs(IEnumerable<T> collection) => 
            new UndoableCollectionAddRangeEventArgs<T>(collection);

        protected virtual EventArgs CreateInsertEventArgs(int index, T item) => 
            new UndoableCollectionInsertEventArgs<T>(index, item);

        public virtual T DeserializeItem(IHistoryReader reader) => 
            default(T);

        public override void Insert(int index, T item)
        {
            Guard.ArgumentNotNull(item, "item");
            ValueChecker.CheckValue(index, 0, base.Count, "index");
            UndoableCollectionInsertHistoryItem<T> item2 = new UndoableCollectionInsertHistoryItem<T>(this.DocumentModel, (UndoableCollection<T>) this, index, item);
            this.DocumentModel.History.Add(item2);
            item2.Execute();
        }

        protected internal virtual void InsertCore(int index, T item)
        {
            this.RaiseInsert(index, item);
            this.InsertInternal(index, item);
        }

        protected internal virtual void InsertInternal(int index, T item)
        {
            base.Insert(index, item);
        }

        public void Move(int sourceIndex, int targetIndex)
        {
            if (base.Count == 0)
            {
                throw new ArgumentOutOfRangeException("index value out of range");
            }
            ValueChecker.CheckValue(sourceIndex, 0, base.Count - 1, "sourceIndex");
            ValueChecker.CheckValue(targetIndex, 0, base.Count - 1, "targetIndex");
            if (sourceIndex != targetIndex)
            {
                UndoableCollectionMoveHistoryItem<T> item = new UndoableCollectionMoveHistoryItem<T>(this.DocumentModel, (UndoableCollection<T>) this, sourceIndex, targetIndex);
                this.DocumentModel.History.Add(item);
                item.Execute();
            }
        }

        public virtual void MoveCore(int sourceIndex, int targetIndex)
        {
            this.RaiseMove(sourceIndex, targetIndex);
            T item = base.InnerList[sourceIndex];
            this.RemoveAtInternal(sourceIndex);
            this.InsertInternal(targetIndex, item);
        }

        protected override void OnModified()
        {
            base.OnModified();
            this.RaiseModified();
        }

        protected internal void RaiseAdd(T item)
        {
            if (this.onAdd != null)
            {
                this.onAdd(this, this.CreateAddEventArgs(item));
            }
        }

        protected internal void RaiseAddRange(IEnumerable<T> collection)
        {
            if (this.onAddRange != null)
            {
                this.onAddRange(this, this.CreateAddRangeEventArgs(collection));
            }
        }

        protected internal void RaiseClear()
        {
            if (this.onClear != null)
            {
                this.onClear(this);
            }
        }

        protected internal void RaiseInsert(int index, T item)
        {
            if (this.onInsert != null)
            {
                this.onInsert(this, this.CreateInsertEventArgs(index, item));
            }
        }

        public virtual void RaiseModified()
        {
            if (this.onModified != null)
            {
                this.onModified(this, EventArgs.Empty);
            }
        }

        protected internal void RaiseMove(int sourceIndex, int targetIndex)
        {
            if (this.onMove != null)
            {
                this.onMove(this, new UndoableCollectionMoveEventArgs(sourceIndex, targetIndex));
            }
        }

        protected internal void RaiseRemoveAt(int index)
        {
            if (this.onRemoveAt != null)
            {
                this.onRemoveAt(this, new UndoableCollectionRemoveAtEventArgs(index));
            }
        }

        protected internal virtual void RegisterItem(IHistoryWriter writer, T item)
        {
            writer.RegisterObject(item);
        }

        public override void RemoveAt(int index)
        {
            if (base.Count == 0)
            {
                throw new ArgumentOutOfRangeException("index value out of range");
            }
            ValueChecker.CheckValue(index, 0, base.Count - 1, "index");
            UndoableCollectionRemoveAtHistoryItem<T> item = new UndoableCollectionRemoveAtHistoryItem<T>(this.DocumentModel, (UndoableCollection<T>) this, index);
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        public virtual void RemoveAtCore(int index)
        {
            this.RaiseRemoveAt(index);
            this.RemoveAtInternal(index);
        }

        protected internal virtual void RemoveAtInternal(int index)
        {
            base.RemoveAt(index);
        }

        protected internal virtual void SerializeItem(IHistoryWriter writer, T item)
        {
            if (this.SupportsHistoryObjects)
            {
                writer.Write(writer.GetObjectId(item));
            }
        }

        public virtual void SetItem(int index, T value)
        {
            UndoableCollectionSetItemHistoryItem<T> item = new UndoableCollectionSetItemHistoryItem<T>(this.DocumentModel, (UndoableCollection<T>) this, index, value);
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected internal void SetItemCore(int index, T value)
        {
            base.InnerList[index] = value;
        }

        public T this[int index]
        {
            get => 
                base[index];
            set => 
                this.SetItem(index, value);
        }

        public IDocumentModel DocumentModel =>
            this.documentModelPart.DocumentModel;

        public IDocumentModelPart DocumentModelPart =>
            this.documentModelPart;

        protected internal virtual bool SupportsHistoryObjects =>
            this.supportsHistoryObjects;
    }
}

