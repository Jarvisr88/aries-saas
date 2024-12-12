namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class SyncCollectionHelper
    {
        private static void AddItem(object item, IList target, Func<object, object> convertItemAction)
        {
            object obj2 = convertItemAction(item);
            if (obj2 != null)
            {
                target.Add(obj2);
            }
        }

        private static void BeginPopulate(IList target, ISupportInitialize supportInitialize)
        {
            if (supportInitialize != null)
            {
                supportInitialize.BeginInit();
            }
            ILockable lockable = target as ILockable;
            if (lockable != null)
            {
                lockable.BeginUpdate();
            }
        }

        private static void DoAction(IEnumerable list, Action<object> action)
        {
            foreach (object obj2 in list)
            {
                action(obj2);
            }
        }

        private static void EndPopulate(IList target, ISupportInitialize supportInitialize)
        {
            ILockable lockable = target as ILockable;
            if (lockable != null)
            {
                lockable.EndUpdate();
            }
            if (supportInitialize != null)
            {
                supportInitialize.EndInit();
            }
        }

        private static void InsertItem(object item, IList target, IList source, Func<object, object> convertItemAction, Action<int, object> insertItemAction)
        {
            object obj2 = convertItemAction(item);
            if (obj2 != null)
            {
                int index = source.IndexOf(item);
                if (insertItemAction != null)
                {
                    insertItemAction(index, obj2);
                }
                else
                {
                    target.Insert(index, obj2);
                }
            }
        }

        public static void PopulateCore(IList target, IEnumerable source, Func<object, object> convertItemAction, ISupportInitialize supportInitialize = null, Action<object> clearItemAction = null)
        {
            if (target != null)
            {
                BeginPopulate(target, supportInitialize);
                try
                {
                    List<object> list = target.OfType<object>().ToList<object>();
                    target.Clear();
                    if (clearItemAction != null)
                    {
                        list.ForEach(clearItemAction);
                    }
                    if (source != null)
                    {
                        DoAction(source, item => AddItem(item, target, convertItemAction));
                    }
                }
                finally
                {
                    EndPopulate(target, supportInitialize);
                }
            }
        }

        public static void PopulateCore(IList target, IList source, Func<object, object> convertItemAction, Action<int, object> insertItemAction = null, ISupportInitialize supportInitialize = null, Action<object> clearItemAction = null)
        {
            if (target != null)
            {
                BeginPopulate(target, supportInitialize);
                try
                {
                    List<object> list = target.OfType<object>().ToList<object>();
                    target.Clear();
                    if (clearItemAction != null)
                    {
                        list.ForEach(clearItemAction);
                    }
                    if (source != null)
                    {
                        if (insertItemAction == null)
                        {
                            DoAction(source, item => AddItem(item, target, convertItemAction));
                        }
                        else
                        {
                            DoAction(source, item => InsertItem(item, target, source, convertItemAction, insertItemAction));
                        }
                    }
                }
                finally
                {
                    EndPopulate(target, supportInitialize);
                }
            }
        }

        private static void RemoveItem(int index, IList target, Action<object> clearItemAction)
        {
            if ((index >= 0) && (index < target.Count))
            {
                object obj2 = target[index];
                target.RemoveAt(index);
                if (clearItemAction != null)
                {
                    clearItemAction(obj2);
                }
            }
        }

        public static void SyncCollection(NotifyCollectionChangedEventArgs e, IList target, IList source, Func<object, object> convertItemAction, Action<int, object> insertItemAction = null, ISupportInitialize supportInitialize = null, Action<object> clearItemAction = null)
        {
            GuardHelper.ArgumentNotNull(target, "target");
            GuardHelper.ArgumentNotNull(source, "source");
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    DoAction(e.NewItems, item => InsertItem(item, target, source, convertItemAction, insertItemAction));
                    return;

                case NotifyCollectionChangedAction.Remove:
                    DoAction(e.OldItems, item => RemoveItem(e.OldStartingIndex, target, clearItemAction));
                    return;

                case NotifyCollectionChangedAction.Replace:
                    RemoveItem(e.NewStartingIndex, target, clearItemAction);
                    InsertItem(e.NewItems[0], target, source, convertItemAction, insertItemAction);
                    return;

                case NotifyCollectionChangedAction.Move:
                {
                    object obj2 = target[e.OldStartingIndex];
                    target.RemoveAt(e.OldStartingIndex);
                    target.Insert(e.NewStartingIndex, obj2);
                    return;
                }
                case NotifyCollectionChangedAction.Reset:
                    PopulateCore(target, source, convertItemAction, insertItemAction, supportInitialize, null);
                    return;
            }
        }

        public static IDisposable TwoWayBind<TSource, TTarget>(IList<TTarget> target, IList<TSource> source, Func<TSource, TTarget> itemConverter, Func<TTarget, TSource> itemBackConverter) => 
            CollectionBindingHelper.Bind<TTarget, TSource>(target, itemConverter, source, itemBackConverter, false, false);
    }
}

