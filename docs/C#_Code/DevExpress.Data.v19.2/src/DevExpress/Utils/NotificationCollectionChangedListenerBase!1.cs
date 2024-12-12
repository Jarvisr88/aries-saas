namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;

    public abstract class NotificationCollectionChangedListenerBase<T> : IDisposable
    {
        private NotificationCollection<T> collection;
        private bool isDisposed;
        private EventHandler onChanged;
        private CancelEventHandler onChanging;

        public event EventHandler Changed
        {
            add
            {
                this.onChanged += value;
            }
            remove
            {
                this.onChanged -= value;
            }
        }

        public event CancelEventHandler Changing
        {
            add
            {
                this.onChanging += value;
            }
            remove
            {
                this.onChanging -= value;
            }
        }

        protected NotificationCollectionChangedListenerBase(NotificationCollection<T> collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
            this.SubscribeCollectionEvents();
            this.SubscribeExistingObjectsEvents();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.collection != null))
            {
                this.UnsubscribeExistingObjectsEvents();
                this.UnsubscribeCollectionEvents();
                this.collection = null;
            }
            this.isDisposed = true;
        }

        protected internal virtual void OnCollectionBeginBatchUpdate(object sender, EventArgs e)
        {
            this.UnsubscribeExistingObjectsEvents();
        }

        protected internal virtual void OnCollectionCancelBatchUpdate(object sender, EventArgs e)
        {
            this.SubscribeExistingObjectsEvents();
        }

        protected internal virtual void OnCollectionChanged(object sender, CollectionChangedEventArgs<T> e)
        {
            switch (e.Action)
            {
                case CollectionChangedAction.Add:
                    this.SubscribeObjectEvents(e.Element);
                    this.RaiseChanged();
                    return;

                case CollectionChangedAction.Remove:
                    this.UnsubscribeObjectEvents(e.Element);
                    this.RaiseChanged();
                    return;

                case CollectionChangedAction.Changed:
                    break;

                case CollectionChangedAction.Clear:
                    this.RaiseChanged();
                    break;

                default:
                    return;
            }
        }

        protected internal virtual void OnCollectionChanging(object sender, CollectionChangingEventArgs<T> e)
        {
            e.Cancel = this.RaiseChanging();
            if (!e.Cancel && (e.Action == CollectionChangedAction.Clear))
            {
                this.UnsubscribeExistingObjectsEvents();
            }
        }

        protected internal virtual void OnCollectionEndBatchUpdate(object sender, EventArgs e)
        {
            this.SubscribeExistingObjectsEvents();
            this.RaiseChanged();
        }

        protected internal virtual void RaiseChanged()
        {
            if (this.onChanged != null)
            {
                this.onChanged(this, EventArgs.Empty);
            }
        }

        protected internal virtual bool RaiseChanging()
        {
            if (this.onChanging == null)
            {
                return false;
            }
            CancelEventArgs e = new CancelEventArgs();
            this.onChanging(this, e);
            return e.Cancel;
        }

        protected internal virtual void SubscribeCollectionEvents()
        {
            this.collection.CollectionChanging += new CollectionChangingEventHandler<T>(this.OnCollectionChanging);
            this.collection.CollectionChanged += new CollectionChangedEventHandler<T>(this.OnCollectionChanged);
            this.collection.BeginBatchUpdate += new EventHandler(this.OnCollectionBeginBatchUpdate);
            this.collection.EndBatchUpdate += new EventHandler(this.OnCollectionEndBatchUpdate);
            this.collection.CancelBatchUpdate += new EventHandler(this.OnCollectionCancelBatchUpdate);
        }

        protected internal virtual void SubscribeExistingObjectsEvents()
        {
            int count = this.collection.Count;
            for (int i = 0; i < count; i++)
            {
                this.SubscribeObjectEvents(this.collection.List[i]);
            }
        }

        protected abstract void SubscribeObjectEvents(T obj);
        protected internal virtual void UnsubscribeCollectionEvents()
        {
            this.collection.CollectionChanging -= new CollectionChangingEventHandler<T>(this.OnCollectionChanging);
            this.collection.CollectionChanged -= new CollectionChangedEventHandler<T>(this.OnCollectionChanged);
            this.collection.BeginBatchUpdate -= new EventHandler(this.OnCollectionBeginBatchUpdate);
            this.collection.EndBatchUpdate -= new EventHandler(this.OnCollectionEndBatchUpdate);
            this.collection.CancelBatchUpdate -= new EventHandler(this.OnCollectionCancelBatchUpdate);
        }

        protected internal virtual void UnsubscribeExistingObjectsEvents()
        {
            int count = this.collection.Count;
            for (int i = 0; i < count; i++)
            {
                this.UnsubscribeObjectEvents(this.collection.List[i]);
            }
        }

        protected abstract void UnsubscribeObjectEvents(T obj);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal bool IsDisposed =>
            this.isDisposed;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected internal NotificationCollection<T> Collection =>
            this.collection;
    }
}

