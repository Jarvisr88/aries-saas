namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        public virtual void AddRange(IEnumerable<T> collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            if (collection.Any<T>())
            {
                base.CheckReentrancy();
                ((List<T>) base.Items).InsertRange(base.Count, collection);
                this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                List<T> changedItems = collection.ToList<T>();
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, base.Count));
            }
        }

        public void RaiseUpdateItems(IList items)
        {
            Guard.ArgumentNotNull(items, "item");
            if (items.Count != 0)
            {
                base.CheckReentrancy();
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, items, items));
            }
        }

        public void RemoveAll()
        {
            this.RemoveRange(new List<T>(base.Items));
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            if (collection.Any<T>() && (base.Count != 0))
            {
                List<T> list = collection.ToList<T>();
                if (list.Count == 1)
                {
                    base.Remove(list[0]);
                }
                else
                {
                    base.CheckReentrancy();
                    Dictionary<int, List<T>> source = new Dictionary<int, List<T>>();
                    int num = -1;
                    List<T> list2 = null;
                    collection = from a in collection
                        orderby base.IndexOf(a)
                        select a;
                    foreach (T local in collection)
                    {
                        int index = base.IndexOf(local);
                        if (index >= 0)
                        {
                            if (((index - num) == 1) && (list2 != null))
                            {
                                list2.Add(local);
                            }
                            else
                            {
                                List<T> list1 = new List<T>();
                                list1.Add(local);
                                source[index] = list2 = list1;
                            }
                            num = index;
                        }
                    }
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                    Func<KeyValuePair<int, List<T>>, int> keySelector = <>c<T>.<>9__1_1;
                    if (<>c<T>.<>9__1_1 == null)
                    {
                        Func<KeyValuePair<int, List<T>>, int> local1 = <>c<T>.<>9__1_1;
                        keySelector = <>c<T>.<>9__1_1 = a => a.Key;
                    }
                    IOrderedEnumerable<KeyValuePair<int, List<T>>> enumerable = source.OrderByDescending<KeyValuePair<int, List<T>>, int>(keySelector);
                    if (base.Count == 0)
                    {
                        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                    else
                    {
                        foreach (KeyValuePair<int, List<T>> pair in enumerable)
                        {
                            foreach (T local2 in pair.Value)
                            {
                                base.Items.RemoveAt(pair.Key);
                            }
                            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, pair.Value, pair.Key));
                        }
                    }
                }
            }
        }

        public T this[int index]
        {
            get
            {
                if (base.Count > index)
                {
                    return base[index];
                }
                return default(T);
            }
            set
            {
                if (base.Count <= index)
                {
                    throw new IndexOutOfRangeException();
                }
                base[index] = value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ObservableRangeCollection<T>.<>c <>9;
            public static Func<KeyValuePair<int, List<T>>, int> <>9__1_1;

            static <>c()
            {
                ObservableRangeCollection<T>.<>c.<>9 = new ObservableRangeCollection<T>.<>c();
            }

            internal int <RemoveRange>b__1_1(KeyValuePair<int, List<T>> a) => 
                a.Key;
        }
    }
}

