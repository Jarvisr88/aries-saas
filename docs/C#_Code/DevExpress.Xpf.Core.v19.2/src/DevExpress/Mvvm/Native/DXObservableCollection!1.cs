namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class DXObservableCollection<T> : ObservableCollection<T>
    {
        [CompilerGenerated]
        private EventHandler BeforeClear;

        public event EventHandler BeforeClear
        {
            [CompilerGenerated] add
            {
                EventHandler beforeClear = this.BeforeClear;
                while (true)
                {
                    EventHandler comparand = beforeClear;
                    EventHandler handler3 = comparand + value;
                    beforeClear = Interlocked.CompareExchange<EventHandler>(ref this.BeforeClear, handler3, comparand);
                    if (ReferenceEquals(beforeClear, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler beforeClear = this.BeforeClear;
                while (true)
                {
                    EventHandler comparand = beforeClear;
                    EventHandler handler3 = comparand - value;
                    beforeClear = Interlocked.CompareExchange<EventHandler>(ref this.BeforeClear, handler3, comparand);
                    if (ReferenceEquals(beforeClear, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public DXObservableCollection()
        {
        }

        public DXObservableCollection(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this.Items.Capacity = DXObservableCollection<T>.GetCapacity(collection);
            this.CopyFrom(collection);
        }

        public DXObservableCollection(List<T> list) : base(list)
        {
        }

        public DXObservableCollection(int capacity)
        {
            this.Items.Capacity = capacity;
        }

        public void AddRange(IEnumerable<T> items)
        {
            List<T> collection = new List<T>(items);
            if (collection.Count != 0)
            {
                this.Items.AddRange(collection);
                this.OnCollectionChangedCore(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection, this.Items.Count));
            }
        }

        public int BinarySearch(T item) => 
            this.Items.BinarySearch(item);

        public int BinarySearch(T item, IComparer<T> comparer) => 
            this.Items.BinarySearch(item, comparer);

        public int BinarySearch(T item, Func<T, T, int> comparer) => 
            this.Items.BinarySearch(item, new DelegateComparer<T>(comparer));

        protected override void ClearItems()
        {
            if (this.Items.Count != 0)
            {
                this.OnCollectionClearing();
                base.ClearItems();
            }
        }

        private void CopyFrom(IEnumerable<T> collection)
        {
            IList<T> items = this.Items;
            if ((collection != null) && (items != null))
            {
                using (IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        items.Add(enumerator.Current);
                    }
                }
            }
        }

        public int FindIndex(Predicate<T> match) => 
            this.Items.FindIndex(match);

        public int FindIndex(int startIndex, Predicate<T> match) => 
            this.Items.FindIndex(startIndex, match);

        public int FindIndex(int startIndex, int count, Predicate<T> match) => 
            this.Items.FindIndex(startIndex, count, match);

        public void ForEach(Action<T> action, bool parallel = false)
        {
            if (!parallel)
            {
                this.Items.ForEach(action);
            }
            else
            {
                Parallel.For(0, base.Count, (Action<int>) (i => action(((DXObservableCollection<T>) this)[i])));
            }
        }

        private static int GetCapacity(IEnumerable<T> collection) => 
            !(collection is ICollection) ? (!(collection is IList) ? 3 : ((IList) collection).Count) : ((ICollection) collection).Count;

        public void InsertRange(int index, IEnumerable<T> items)
        {
            List<T> collection = new List<T>(items);
            if (collection.Count != 0)
            {
                this.Items.InsertRange(index, collection);
                this.OnCollectionChangedCore(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection, index));
            }
        }

        private void OnCollectionChangedCore(NotifyCollectionChangedEventArgs e)
        {
            if ((e.Action != NotifyCollectionChangedAction.Move) && (e.Action != NotifyCollectionChangedAction.Replace))
            {
                this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            }
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(e);
        }

        protected virtual void OnCollectionClearing()
        {
            this.RaiseBeforeClear();
        }

        private void RaiseBeforeClear()
        {
            if (this.BeforeClear != null)
            {
                this.BeforeClear(this, EventArgs.Empty);
            }
        }

        public void RemoveRange(int index, int count)
        {
            if (count != 0)
            {
                List<T> changedItems = new List<T>(count);
                for (int i = index; i < count; i++)
                {
                    changedItems.Add(this.Items[i]);
                }
                this.Items.RemoveRange(index, count);
                this.OnCollectionChangedCore(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, changedItems, index));
            }
        }

        protected List<T> Items =>
            (List<T>) base.Items;
    }
}

