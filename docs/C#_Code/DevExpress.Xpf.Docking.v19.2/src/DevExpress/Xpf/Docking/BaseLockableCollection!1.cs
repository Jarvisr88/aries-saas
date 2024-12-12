namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    public abstract class BaseLockableCollection<T> : ObservableCollection<T>, ILockable
    {
        private int lockCount;
        private List<T> lockedItems;

        protected BaseLockableCollection()
        {
        }

        public virtual void BeginUpdate()
        {
            this.lockCount++;
            this.lockedItems = this.ToList<T>();
        }

        protected internal void CancelUpdate()
        {
            this.lockCount--;
        }

        public virtual void EndUpdate()
        {
            this.CancelUpdate();
            if (!this.IsLockUpdate)
            {
                if ((this.lockedItems != null) && !this.lockedItems.SequenceEqual<T>(this))
                {
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
                this.lockedItems = null;
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!this.IsLockUpdate)
            {
                this.OnCollectionChangedOverride(e);
            }
        }

        protected virtual void OnCollectionChangedOverride(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }

        public bool IsLockUpdate =>
            this.lockCount != 0;
    }
}

