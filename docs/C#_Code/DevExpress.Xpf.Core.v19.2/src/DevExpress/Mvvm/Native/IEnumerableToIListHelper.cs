namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    internal class IEnumerableToIListHelper
    {
        private static void Add<TSource, TTarget>(IList<TTarget> target, Func<TSource, TTarget> convert, NotifyCollectionChangedEventArgs e)
        {
            int newStartingIndex = e.NewStartingIndex;
            foreach (TSource local in e.NewItems)
            {
                target.Insert(newStartingIndex, convert(local));
                newStartingIndex++;
            }
        }

        private static void Add(IList target, Func<object, object> convert, NotifyCollectionChangedEventArgs e)
        {
            int newStartingIndex = e.NewStartingIndex;
            foreach (object obj2 in e.NewItems)
            {
                target.Insert(newStartingIndex, convert(obj2));
                newStartingIndex++;
            }
        }

        private static void Move<TTarget>(IList<TTarget> target, NotifyCollectionChangedEventArgs e)
        {
            if (target is ObservableCollection<TTarget>)
            {
                ((ObservableCollection<TTarget>) target).Move(e.OldStartingIndex, e.NewStartingIndex);
            }
            else
            {
                TTarget item = target[e.OldStartingIndex];
                target.RemoveAt(e.OldStartingIndex);
                target.Insert(e.NewStartingIndex, item);
            }
        }

        private static void Move(IList target, NotifyCollectionChangedEventArgs e)
        {
            object obj2 = target[e.OldStartingIndex];
            target.RemoveAt(e.OldStartingIndex);
            target.Insert(e.NewStartingIndex, obj2);
        }

        private static void Remove<TTarget>(IList<TTarget> target, NotifyCollectionChangedEventArgs e)
        {
            int index = (e.OldStartingIndex + e.OldItems.Count) - 1;
            int count = e.OldItems.Count;
            while (--count >= 0)
            {
                target.RemoveAt(index);
                index--;
            }
        }

        private static void Remove(IList target, NotifyCollectionChangedEventArgs e)
        {
            int index = (e.OldStartingIndex + e.OldItems.Count) - 1;
            int count = e.OldItems.Count;
            while (--count >= 0)
            {
                target.RemoveAt(index);
                index--;
            }
        }

        private static void Replace<TSource, TTarget>(IList<TTarget> target, Func<TSource, TTarget> convert, NotifyCollectionChangedEventArgs e)
        {
            target[e.NewStartingIndex] = convert((TSource) e.NewItems[0]);
        }

        private static void Replace(IList target, Func<object, object> convert, NotifyCollectionChangedEventArgs e)
        {
            target[e.NewStartingIndex] = convert(e.NewItems[0]);
        }

        public static void Reset<TSource, TTarget>(IEnumerable<TSource> source, IList<TTarget> target, Func<TSource, TTarget> convert)
        {
            target.Clear();
            foreach (TSource local in source)
            {
                target.Add(convert(local));
            }
        }

        public static void Reset(IEnumerable source, IList target, Func<object, object> convert)
        {
            target.Clear();
            foreach (object obj2 in source)
            {
                target.Add(convert(obj2));
            }
        }

        public static void Sync<TSource, TTarget>(IEnumerable<TSource> source, IList<TTarget> target, Func<TSource, TTarget> convert, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Add<TSource, TTarget>(target, convert, e);
                    return;

                case NotifyCollectionChangedAction.Remove:
                    Remove<TTarget>(target, e);
                    return;

                case NotifyCollectionChangedAction.Replace:
                    Replace<TSource, TTarget>(target, convert, e);
                    return;

                case NotifyCollectionChangedAction.Move:
                    Move<TTarget>(target, e);
                    return;
            }
            Reset<TSource, TTarget>(source, target, convert);
        }

        public static void Sync(IEnumerable source, IList target, Func<object, object> convert, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Add(target, convert, e);
                    return;

                case NotifyCollectionChangedAction.Remove:
                    Remove(target, e);
                    return;

                case NotifyCollectionChangedAction.Replace:
                    Replace(target, convert, e);
                    return;

                case NotifyCollectionChangedAction.Move:
                    Move(target, e);
                    return;
            }
            Reset(source, target, convert);
        }
    }
}

