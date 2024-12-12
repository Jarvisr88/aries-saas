namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public abstract class BaseItemsCollection<T> : ObservableCollection<T>, IDisposable, IWeakEventListener
    {
        private bool isDisposing;
        private IEnumerable itemsSource;
        private ICollectionView collectionViewCore;

        protected BaseItemsCollection()
        {
        }

        protected virtual bool CheckItemCollectionMustBeEmpty() => 
            !this.IsUsingItemsSource && (base.Count > 0);

        public void ClearItemsSource()
        {
            if (this.IsUsingItemsSource)
            {
                this.UnsubscribeSource(this.itemsSource);
                this.itemsSource = null;
                this.InvalidateItemsSource();
            }
        }

        private void collectionView_CurrentChanged(object sender, EventArgs e)
        {
            this.OnCurrentChanged(sender);
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        protected virtual void InvalidateItemsSource()
        {
        }

        public void MoveCurrentTo(object item)
        {
            if (this.collectionViewCore != null)
            {
                this.collectionViewCore.MoveCurrentTo(item);
            }
        }

        protected abstract void OnAddToItemsSource(IEnumerable newItems, int startingIndex = 0);
        protected abstract void OnCurrentChanged(object sender);
        protected virtual void OnDisposing()
        {
            this.ClearItemsSource();
            this.ClearItems();
        }

        protected virtual void OnItemMovedInItemsSource(int oldStartingIndex, int newStartingIndex)
        {
        }

        protected virtual void OnItemReplacedInItemsSource(IList oldItems, IList newItems, int newStartingIndex)
        {
        }

        protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.OnAddToItemsSource(e.NewItems, e.NewStartingIndex);
                    return;

                case NotifyCollectionChangedAction.Remove:
                    this.OnRemoveFromItemsSource(e.OldItems);
                    return;

                case NotifyCollectionChangedAction.Replace:
                    this.OnItemReplacedInItemsSource(e.OldItems, e.NewItems, e.NewStartingIndex);
                    return;

                case NotifyCollectionChangedAction.Move:
                    this.OnItemMovedInItemsSource(e.OldStartingIndex, e.NewStartingIndex);
                    return;

                case NotifyCollectionChangedAction.Reset:
                    this.OnResetItemsSource();
                    return;
            }
        }

        protected abstract void OnRemoveFromItemsSource(IEnumerable oldItems);
        protected abstract void OnResetItemsSource();
        public void SetItemsSource(IEnumerable value)
        {
            if (this.CheckItemCollectionMustBeEmpty())
            {
                throw new InvalidOperationException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.ItemCollectionMustBeEmpty));
            }
            this.SetItemsSourceCore(value);
        }

        private void SetItemsSourceCore(IEnumerable value)
        {
            this.UnsubscribeSource(this.itemsSource);
            this.itemsSource = value;
            this.SubscribeSource(this.itemsSource);
            this.InvalidateItemsSource();
            this.OnAddToItemsSource(value, 0);
        }

        private void SubscribeSource(IEnumerable Source)
        {
            INotifyCollectionChanged source = Source as INotifyCollectionChanged;
            if (source != null)
            {
                CollectionChangedEventManager.AddListener(source, this);
            }
            this.collectionViewCore = CollectionViewSource.GetDefaultView(Source);
            if (this.collectionViewCore != null)
            {
                CurrentChangedEventManager.AddListener(this.collectionViewCore, this);
            }
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(CollectionChangedEventManager))
            {
                NotifyCollectionChangedEventArgs args = (NotifyCollectionChangedEventArgs) e;
                this.OnItemsSourceCollectionChanged(sender, args);
            }
            else
            {
                if (!(managerType == typeof(CurrentChangedEventManager)))
                {
                    return false;
                }
                this.collectionView_CurrentChanged(sender, e);
            }
            return true;
        }

        private void UnsubscribeSource(IEnumerable Source)
        {
            INotifyCollectionChanged source = Source as INotifyCollectionChanged;
            if (source != null)
            {
                CollectionChangedEventManager.RemoveListener(source, this);
            }
            if (this.collectionViewCore != null)
            {
                CurrentChangedEventManager.RemoveListener(this.collectionViewCore, this);
            }
        }

        protected IEnumerable ItemsSource =>
            this.itemsSource;

        internal bool IsUsingItemsSource =>
            this.itemsSource != null;

        public object CurrentItem =>
            this.collectionViewCore?.CurrentItem;
    }
}

