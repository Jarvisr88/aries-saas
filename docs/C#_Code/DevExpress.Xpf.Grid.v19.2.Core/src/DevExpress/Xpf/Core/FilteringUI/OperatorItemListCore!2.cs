namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    internal sealed class OperatorItemListCore<T, U> where T: OperatorItemBase where U: struct
    {
        private readonly IList<T> list;
        private readonly Func<T, U?> getKey;

        public OperatorItemListCore(IList<T> list, Func<T, U?> getKey)
        {
            Guard.ArgumentNotNull(list, "list");
            Guard.ArgumentNotNull(getKey, "getKey");
            this.list = list;
            this.getKey = getKey;
        }

        private int? FindIndex(U key)
        {
            int index = this.list.IndexOf<T>(delegate (T item) {
                U? nullable = ((OperatorItemListCore<T, U>) this).getKey(item);
                return (nullable != null) && nullable.Value.Equals(key);
            });
            if (index > -1)
            {
                return new int?(index);
            }
            return null;
        }

        internal T GetItem(U key)
        {
            int? nullable = this.FindIndex(key);
            if (nullable != null)
            {
                return this.list[nullable.Value];
            }
            return default(T);
        }

        internal bool Remove(U key)
        {
            int? nullable = this.FindIndex(key);
            if (nullable == null)
            {
                return false;
            }
            this.list.RemoveAt(nullable.Value);
            return true;
        }

        internal void SetItem(U key, T newItem)
        {
            int? nullable = this.FindIndex(key);
            if (nullable != null)
            {
                this.list[nullable.Value] = newItem;
            }
        }
    }
}

