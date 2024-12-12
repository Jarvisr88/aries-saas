namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ListItemCollection : CollectionBase, INotifyCollectionChanged
    {
        private int lockUpdate;

        private event NotifyCollectionChangedEventHandler CollectionChanged;

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
                this.CollectionChanged += value;
            }
            remove
            {
                this.CollectionChanged -= value;
            }
        }

        internal ListItemCollection(IListNotificationOwner listNotification)
        {
            this.Owner = listNotification;
        }

        public int Add(object item) => 
            base.List.Add(item);

        private void AddNotifyOwner(object item)
        {
            ListBoxEditItem item2 = item as ListBoxEditItem;
            if (item2 != null)
            {
                item2.SubscribeToSelection();
                item2.NotifyOwner = this.Owner;
            }
        }

        public void AddRange(object[] range)
        {
            this.BeginUpdate();
            try
            {
                foreach (object obj2 in range)
                {
                    this.Add(obj2);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        internal void Assign(ListItemCollection items)
        {
            base.InnerList.Clear();
            this.AddRange(items.InnerList.ToArray());
        }

        public void BeginUpdate()
        {
            this.lockUpdate++;
        }

        public bool Contains(object item) => 
            base.List.Contains(item);

        public void EndUpdate()
        {
            int num = this.lockUpdate - 1;
            this.lockUpdate = num;
            if (num == 0)
            {
                this.NotifyOwner(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public int IndexOf(object item) => 
            base.List.IndexOf(item);

        public void Insert(int index, object item)
        {
            base.List.Insert(index, item);
        }

        protected virtual void NotifyOwner(NotifyCollectionChangedEventArgs e)
        {
            if (!this.IsLockUpdate && (this.CollectionChanged != null))
            {
                this.CollectionChanged(this, e);
            }
        }

        protected override void OnClear()
        {
            base.InnerList.Clear();
            this.NotifyOwner(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            this.NotifyOwner(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));
            this.AddNotifyOwner(value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            this.NotifyOwner(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value, index));
            this.RemoveNotifyOwner(value);
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            base.OnSetComplete(index, oldValue, newValue);
            this.NotifyOwner(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldValue, newValue, index));
        }

        public void Remove(object item)
        {
            base.List.Remove(item);
        }

        private void RemoveNotifyOwner(object item)
        {
            ListBoxEditItem item2 = item as ListBoxEditItem;
            if (item2 != null)
            {
                item2.NotifyOwner = null;
                item2.UnsubscribeFromSelection();
            }
        }

        internal void UpdateSelection(object selectedItems)
        {
            IEnumerable<object> enumerable1;
            if (selectedItems is IEnumerable<object>)
            {
                enumerable1 = (IEnumerable<object>) selectedItems;
            }
            else
            {
                List<object> list1 = new List<object>();
                list1.Add(selectedItems);
                enumerable1 = list1;
            }
            List<object> list = enumerable1.ToList<object>();
            foreach (object obj2 in base.InnerList)
            {
                ListBoxEditItem item = obj2 as ListBoxEditItem;
                if (item != null)
                {
                    item.ChangeSelectionWithLock(list.Contains(obj2));
                }
            }
        }

        private IListNotificationOwner Owner { get; set; }

        internal bool IsLockUpdate =>
            this.lockUpdate > 0;

        public object this[int index]
        {
            get => 
                base.List[index];
            set => 
                base.List[index] = value;
        }
    }
}

