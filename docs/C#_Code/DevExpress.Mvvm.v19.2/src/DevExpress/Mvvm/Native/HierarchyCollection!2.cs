namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class HierarchyCollection<T, TParent> : ObservableCollection<T> where TParent: class
    {
        private readonly TParent owner;
        private readonly Action<T, TParent> attachAction;
        private readonly Action<T, TParent> detachAction;

        public HierarchyCollection(TParent owner, Action<T, TParent> attachAction, Action<T, TParent> detachAction, IEnumerable<T> collection = null, bool applyAttachActionForOldItems = true) : this(enumerable1)
        {
            IEnumerable<T> enumerable1 = collection;
            if (collection == null)
            {
                IEnumerable<T> local1 = collection;
                enumerable1 = Enumerable.Empty<T>();
            }
            this.owner = owner;
            this.attachAction = attachAction;
            this.detachAction = detachAction;
            if (applyAttachActionForOldItems)
            {
                foreach (T local in this)
                {
                    attachAction(local, owner);
                }
            }
        }

        protected override void ClearItems()
        {
            foreach (T local in this)
            {
                this.detachAction(local, this.owner);
            }
            this.ClearItemsCore();
        }

        protected virtual void ClearItemsCore()
        {
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            this.InsertItemCore(index, item);
            this.attachAction(item, this.owner);
        }

        protected virtual void InsertItemCore(int index, T item)
        {
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this.detachAction(base[index], this.owner);
            this.RemoveItemCore(index);
        }

        protected virtual void RemoveItemCore(int index)
        {
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            this.detachAction(base[index], this.owner);
            this.SetItemCore(index, item);
            this.attachAction(item, this.owner);
        }

        protected virtual void SetItemCore(int index, T item)
        {
            base.SetItem(index, item);
        }
    }
}

