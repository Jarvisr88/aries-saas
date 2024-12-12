namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class ClipboardItemInfoCollectionBase<TKey, TValue> : IReadOnlyList<TValue>, IReadOnlyCollection<TValue>, IEnumerable<TValue>, IEnumerable where TValue: class
    {
        protected Dictionary<TKey, TValue> itemsInfo;

        protected ClipboardItemInfoCollectionBase()
        {
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            Dictionary<TKey, TValue>.ValueCollection.Enumerator? nullable1;
            if (this.itemsInfo != null)
            {
                nullable1 = new Dictionary<TKey, TValue>.ValueCollection.Enumerator?(this.itemsInfo.Values.GetEnumerator());
            }
            else
            {
                Dictionary<TKey, TValue> itemsInfo = this.itemsInfo;
                nullable1 = null;
            }
            return (IEnumerator<TValue>) nullable1;
        }

        protected abstract TValue GetValueByName(string name);
        IEnumerator IEnumerable.GetEnumerator()
        {
            Dictionary<TKey, TValue>.ValueCollection.Enumerator? nullable1;
            if (this.itemsInfo != null)
            {
                nullable1 = new Dictionary<TKey, TValue>.ValueCollection.Enumerator?(this.itemsInfo.Values.GetEnumerator());
            }
            else
            {
                Dictionary<TKey, TValue> itemsInfo = this.itemsInfo;
                nullable1 = null;
            }
            return (IEnumerator) nullable1;
        }

        public TValue this[int index]
        {
            get
            {
                if (this.itemsInfo != null)
                {
                    return this.itemsInfo.ElementAt<KeyValuePair<TKey, TValue>>(index).Value;
                }
                Dictionary<TKey, TValue> itemsInfo = this.itemsInfo;
                return default(TValue);
            }
        }

        public TValue this[string name] =>
            this.GetValueByName(name);

        public int Count
        {
            get
            {
                if (this.itemsInfo != null)
                {
                    return this.itemsInfo.Count;
                }
                Dictionary<TKey, TValue> itemsInfo = this.itemsInfo;
                return 0;
            }
        }
    }
}

