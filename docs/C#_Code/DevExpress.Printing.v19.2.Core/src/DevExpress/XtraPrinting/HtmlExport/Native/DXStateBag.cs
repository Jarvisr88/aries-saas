namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public sealed class DXStateBag : IDictionary, ICollection, IEnumerable
    {
        private IDictionary bag = CreateBag();
        private bool marked = false;

        public StateItem Add(string key, object value)
        {
            Guard.ArgumentNotNull(key, "key");
            StateItem item = this.bag[key] as StateItem;
            if (item == null)
            {
                if ((value != null) || this.marked)
                {
                    item = new StateItem(value);
                    this.bag.Add(key, item);
                }
            }
            else if ((value == null) && !this.marked)
            {
                this.bag.Remove(key);
            }
            else
            {
                item.Value = value;
            }
            if ((item != null) && this.marked)
            {
                item.IsDirty = true;
            }
            return item;
        }

        public void Clear()
        {
            this.bag.Clear();
        }

        private static IDictionary CreateBag() => 
            new Dictionary<string, object>();

        public IDictionaryEnumerator GetEnumerator() => 
            this.bag.GetEnumerator();

        public bool IsItemDirty(string key)
        {
            StateItem item = this.bag[key] as StateItem;
            return ((item != null) && item.IsDirty);
        }

        public void Remove(string key)
        {
            this.bag.Remove(key);
        }

        public void SetDirty(bool dirty)
        {
            if (this.bag.Count != 0)
            {
                foreach (StateItem item in this.bag.Values)
                {
                    item.IsDirty = dirty;
                }
            }
        }

        public void SetItemDirty(string key, bool dirty)
        {
            StateItem item = this.bag[key] as StateItem;
            if (item != null)
            {
                item.IsDirty = dirty;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.Values.CopyTo(array, index);
        }

        void IDictionary.Add(object key, object value)
        {
            this.Add((string) key, value);
        }

        bool IDictionary.Contains(object key) => 
            this.bag.Contains((string) key);

        void IDictionary.Remove(object key)
        {
            this.Remove((string) key);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        internal void TrackViewState()
        {
            this.marked = true;
        }

        public int Count =>
            this.bag.Count;

        internal bool IsTrackingViewState =>
            this.marked;

        public object this[string key]
        {
            get
            {
                Guard.ArgumentNotNull(key, "key");
                StateItem item = this.bag[key] as StateItem;
                return item?.Value;
            }
            set => 
                this.Add(key, value);
        }

        public ICollection Keys =>
            this.bag.Keys;

        bool ICollection.IsSynchronized =>
            false;

        object ICollection.SyncRoot =>
            this;

        bool IDictionary.IsFixedSize =>
            false;

        bool IDictionary.IsReadOnly =>
            false;

        object IDictionary.this[object key]
        {
            get => 
                this[(string) key];
            set => 
                this[(string) key] = value;
        }

        public ICollection Values =>
            this.bag.Values;
    }
}

