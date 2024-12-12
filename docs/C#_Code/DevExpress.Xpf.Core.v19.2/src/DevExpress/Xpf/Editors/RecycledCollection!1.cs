namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RecycledCollection<TItem> : IEnumerable<TItem>, IEnumerable where TItem: FrameworkElement
    {
        private readonly Dictionary<int, TItem> manipulationItems;
        private readonly Dictionary<int, TItem> items;

        public RecycledCollection()
        {
            this.items = new Dictionary<int, TItem>();
            this.manipulationItems = new Dictionary<int, TItem>();
        }

        public void AddManipulationItems(IDictionary<int, TItem> mItems)
        {
            this.ResetManipulationItems();
            this.manipulationItems.AddRange<int, TItem>(mItems);
        }

        public bool Contains(int index) => 
            !this.IsManipulating || this.items.ContainsKey(index);

        public IEnumerator<TItem> GetEnumerator() => 
            (IEnumerator<TItem>) this.items.Values.GetEnumerator();

        private TItem GetItem(int index)
        {
            TItem local;
            if (!this.items.Any<KeyValuePair<int, TItem>>())
            {
                return default(TItem);
            }
            int key = index;
            if (!this.items.TryGetValue(index, out local))
            {
                KeyValuePair<int, TItem> pair = this.items.First<KeyValuePair<int, TItem>>();
                local = pair.Value;
                key = pair.Key;
            }
            this.items.Remove(key);
            return local;
        }

        public bool IsManipulationItem(int index) => 
            this.manipulationItems.ContainsKey(index);

        public TItem Pop(int index)
        {
            TItem item = this.GetItem(index);
            if (item != null)
            {
                FrameworkElement element = item;
                if (element != null)
                {
                    element.Visibility = Visibility.Visible;
                }
            }
            return item;
        }

        public void Push(int index, TItem item)
        {
            item.Visibility = Visibility.Collapsed;
            this.items.Add(index, item);
        }

        public void Reset()
        {
            this.items.Clear();
        }

        public void ResetManipulationItems()
        {
            this.manipulationItems.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.items.Values.GetEnumerator();

        public bool IsManipulating { get; set; }

        public int Count =>
            this.items.Count;
    }
}

