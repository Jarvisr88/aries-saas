namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class NotificationCollection<T> : DXCollection<T>, IBatchUpdateable, IBatchUpdateHandler
    {
        private readonly BatchUpdateHelper batchUpdateHelper;
        private bool changed;
        private EventHandler onBeginBatchUpdate;
        private EventHandler onEndBatchUpdate;
        private EventHandler onCancelBatchUpdate;
        private CollectionChangingEventHandler<T> onCollectionChanging;
        private CollectionChangedEventHandler<T> onCollectionChanged;

        internal event EventHandler BeginBatchUpdate
        {
            add
            {
                this.onBeginBatchUpdate += value;
            }
            remove
            {
                this.onBeginBatchUpdate -= value;
            }
        }

        internal event EventHandler CancelBatchUpdate
        {
            add
            {
                this.onCancelBatchUpdate += value;
            }
            remove
            {
                this.onCancelBatchUpdate -= value;
            }
        }

        [Browsable(false)]
        public event CollectionChangedEventHandler<T> CollectionChanged
        {
            add
            {
                this.onCollectionChanged += value;
            }
            remove
            {
                this.onCollectionChanged -= value;
            }
        }

        [Browsable(false)]
        public event CollectionChangingEventHandler<T> CollectionChanging
        {
            add
            {
                this.onCollectionChanging += value;
            }
            remove
            {
                this.onCollectionChanging -= value;
            }
        }

        internal event EventHandler EndBatchUpdate
        {
            add
            {
                this.onEndBatchUpdate += value;
            }
            remove
            {
                this.onEndBatchUpdate -= value;
            }
        }

        public NotificationCollection()
        {
            this.batchUpdateHelper = new BatchUpdateHelper(this);
        }

        protected NotificationCollection(DXCollectionUniquenessProviderType uniquenessProviderType) : base(uniquenessProviderType)
        {
            this.batchUpdateHelper = new BatchUpdateHelper(this);
        }

        protected NotificationCollection(int capacity, DXCollectionUniquenessProviderType uniquenessProviderType) : base(capacity, uniquenessProviderType)
        {
            this.batchUpdateHelper = new BatchUpdateHelper(this);
        }

        public override void AddRange(ICollection collection)
        {
            if ((collection != null) && (collection.Count > 0))
            {
                this.BeginUpdate();
                try
                {
                    this.AddRangeCore(collection);
                }
                finally
                {
                    this.EndUpdate();
                }
            }
        }

        public void BeginUpdate()
        {
            this.batchUpdateHelper.BeginUpdate();
        }

        public void CancelUpdate()
        {
            this.batchUpdateHelper.CancelUpdate();
        }

        void IBatchUpdateHandler.OnBeginUpdate()
        {
        }

        void IBatchUpdateHandler.OnCancelUpdate()
        {
        }

        void IBatchUpdateHandler.OnEndUpdate()
        {
        }

        void IBatchUpdateHandler.OnFirstBeginUpdate()
        {
            this.OnFirstBeginUpdate();
        }

        void IBatchUpdateHandler.OnLastCancelUpdate()
        {
            this.OnLastCancelUpdate();
        }

        void IBatchUpdateHandler.OnLastEndUpdate()
        {
            this.OnLastEndUpdate();
        }

        public void EndUpdate()
        {
            this.batchUpdateHelper.EndUpdate();
        }

        protected override bool OnClear()
        {
            if (base.Count <= 0)
            {
                return false;
            }
            T element = default(T);
            CollectionChangingEventArgs<T> e = new CollectionChangingEventArgs<T>(CollectionChangedAction.Clear, element);
            this.OnCollectionChanging(e);
            return (!e.Cancel ? base.OnClear() : false);
        }

        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            T element = default(T);
            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(CollectionChangedAction.Clear, element));
        }

        protected internal virtual void OnCollectionChanged(CollectionChangedEventArgs<T> e)
        {
            if (!this.IsUpdateLocked)
            {
                this.RaiseCollectionChanged(e);
            }
        }

        protected internal virtual void OnCollectionChanging(CollectionChangingEventArgs<T> e)
        {
            if (!this.IsUpdateLocked)
            {
                this.RaiseCollectionChanging(e);
            }
            else
            {
                this.RaiseCollectionChanging(e);
                if (!e.Cancel)
                {
                    this.changed = true;
                }
            }
        }

        protected internal void OnFirstBeginUpdate()
        {
            this.RaiseBeginBatchUpdate();
            this.changed = false;
        }

        protected override bool OnInsert(int index, T value)
        {
            CollectionChangingEventArgs<T> e = new CollectionChangingEventArgs<T>(CollectionChangedAction.Add, value);
            this.OnCollectionChanging(e);
            return (!e.Cancel ? base.OnInsert(index, value) : false);
        }

        protected override void OnInsertComplete(int index, T value)
        {
            base.OnInsertComplete(index, value);
            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(CollectionChangedAction.Add, value));
        }

        protected internal virtual void OnLastCancelUpdate()
        {
            this.RaiseCancelBatchUpdate();
        }

        protected virtual void OnLastEndUpdate()
        {
            this.RaiseEndBatchUpdate();
            if (this.changed)
            {
                T element = default(T);
                this.OnCollectionChanged(new CollectionChangedEventArgs<T>(CollectionChangedAction.EndBatchUpdate, element));
            }
        }

        protected override bool OnRemove(int index, T value)
        {
            CollectionChangingEventArgs<T> e = new CollectionChangingEventArgs<T>(CollectionChangedAction.Remove, value);
            this.OnCollectionChanging(e);
            return (!e.Cancel ? base.OnRemove(index, value) : false);
        }

        protected override void OnRemoveComplete(int index, T value)
        {
            base.OnRemoveComplete(index, value);
            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(CollectionChangedAction.Remove, value));
        }

        protected override void OnSetComplete(int index, T oldValue, T newValue)
        {
            base.OnSetComplete(index, oldValue, newValue);
            if (this.NotifySet)
            {
                this.OnCollectionChanged(new CollectionChangedEventArgs<T>(CollectionChangedAction.Changed, newValue));
            }
        }

        protected internal virtual void RaiseBeginBatchUpdate()
        {
            if (this.onBeginBatchUpdate != null)
            {
                this.onBeginBatchUpdate(this, EventArgs.Empty);
            }
        }

        protected internal virtual void RaiseCancelBatchUpdate()
        {
            if (this.onCancelBatchUpdate != null)
            {
                this.onCancelBatchUpdate(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseCollectionChanged(CollectionChangedEventArgs<T> e)
        {
            if (this.onCollectionChanged != null)
            {
                this.onCollectionChanged(this, e);
            }
        }

        protected virtual void RaiseCollectionChanging(CollectionChangingEventArgs<T> e)
        {
            if (this.onCollectionChanging != null)
            {
                this.onCollectionChanging(this, e);
            }
        }

        protected internal virtual void RaiseEndBatchUpdate()
        {
            if (this.onEndBatchUpdate != null)
            {
                this.onEndBatchUpdate(this, EventArgs.Empty);
            }
        }

        protected virtual bool NotifySet =>
            false;

        BatchUpdateHelper IBatchUpdateable.BatchUpdateHelper =>
            this.batchUpdateHelper;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUpdateLocked =>
            this.batchUpdateHelper.IsUpdateLocked;
    }
}

