namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class CollectionBindingHelper
    {
        private static void AssertCollections<TTarget, TSource>(TTarget target, TSource source) where TTarget: class where TSource: class
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
        }

        public static IDisposable Bind<TTarget, TSource>(IList<TTarget> target, Func<TSource, TTarget> itemConverter, IList<TSource> source, Func<TTarget, TSource> itemBackConverter, bool reverse = false, bool useStrongReferences = false)
        {
            AssertCollections<IList<TTarget>, IList<TSource>>(target, source);
            return BindCore<TTarget, TSource>(target, itemConverter, source, itemBackConverter, reverse, useStrongReferences);
        }

        public static IDisposable Bind<TTarget, TSource>(IList<TTarget> target, Func<TSource, TTarget> itemConverter, Func<TTarget, TSource> itemBackConverter, IList source, bool reverse = false, bool useStrongReferences = false)
        {
            AssertCollections<IList<TTarget>, IList>(target, source);
            return BindCore<TTarget, TSource>(target, itemConverter, source, itemBackConverter, reverse, useStrongReferences);
        }

        public static IDisposable Bind<TTarget, TSource>(Func<TSource, TTarget> itemConverter, IList target, IList<TSource> source, Func<TTarget, TSource> itemBackConverter, bool reverse = false, bool useStrongReferences = false)
        {
            AssertCollections<IList, IList<TSource>>(target, source);
            return BindCore<TTarget, TSource>(target, itemConverter, source, itemBackConverter, reverse, useStrongReferences);
        }

        public static IDisposable Bind<TTarget, TSource>(Func<TSource, TTarget> itemConverter, IList target, Func<TTarget, TSource> itemBackConverter, IList source, bool reverse = false, bool useStrongReferences = false)
        {
            AssertCollections<IList, IList>(target, source);
            return BindCore<TTarget, TSource>(target, itemConverter, source, itemBackConverter, reverse, useStrongReferences);
        }

        private static IDisposable BindCore<TTarget, TSource>(IEnumerable target, Func<TSource, TTarget> itemConverter, IEnumerable source, Func<TTarget, TSource> itemBackConverter, bool reverse, bool useStrongReferences)
        {
            CollectionTwoWayBinding<TTarget, TSource> binding = new CollectionTwoWayBinding<TTarget, TSource>(target, itemConverter, source, itemBackConverter, reverse, useStrongReferences);
            binding.Reset();
            return binding;
        }

        public static IDisposable BindOneWay<TTarget, TSource>(IList<TTarget> target, Func<TSource, TTarget> itemConverter, IEnumerable source, bool reverse = false, bool useStrongReferences = false)
        {
            AssertCollections<IList<TTarget>, IEnumerable>(target, source);
            return BindOneWayCore<TTarget, TSource>(target, itemConverter, source, reverse, useStrongReferences);
        }

        public static IDisposable BindOneWay<TTarget, TSource>(Func<TSource, TTarget> itemConverter, IList target, IList<TSource> source, bool reverse = false, bool useStrongReferences = false)
        {
            AssertCollections<IList, IList<TSource>>(target, source);
            return BindOneWayCore<TTarget, TSource>(target, itemConverter, source, reverse, useStrongReferences);
        }

        private static IDisposable BindOneWayCore<TTarget, TSource>(IEnumerable target, Func<TSource, TTarget> itemConverter, IEnumerable source, bool reverse, bool useStrongReferences)
        {
            CollectionOneWayBinding<TTarget, TSource> binding = new CollectionOneWayBinding<TTarget, TSource>(target, itemConverter, source, new CollectionLocker(), new CollectionLocker(), reverse, useStrongReferences);
            binding.Reset();
            return binding;
        }

        private sealed class CollectionLocker
        {
            private bool locked;

            public void DoIfNotLocked(Action action)
            {
                if (!this.locked)
                {
                    action();
                }
            }

            public void DoLockedAction(Action action)
            {
                this.locked = true;
                try
                {
                    action();
                }
                finally
                {
                    this.locked = false;
                }
            }

            public void DoLockedActionIfNotLocked(CollectionBindingHelper.CollectionLocker possibleLocked, Action action)
            {
                possibleLocked.DoIfNotLocked(() => this.DoLockedAction(action));
            }
        }

        private sealed class CollectionOneWayBinding<TTarget, TSource> : IDisposable
        {
            private CollectionBindingHelper.CollectionLocker doNotProcessSourceCollectionChanged;
            private CollectionBindingHelper.CollectionLocker doNotProcessTargetCollectionChanged;
            private Func<TSource, TTarget> itemConverter;
            private bool reverse;
            private readonly bool useStrongReferences;
            private object sourceRef;
            private object targetRef;

            public CollectionOneWayBinding(IEnumerable target, Func<TSource, TTarget> itemConverter, IEnumerable source, CollectionBindingHelper.CollectionLocker doNotProcessSourceCollectionChanged, CollectionBindingHelper.CollectionLocker doNotProcessTargetCollectionChanged, bool reverse, bool useStrongReferences)
            {
                source = this.GetListObject(source);
                this.reverse = reverse;
                this.useStrongReferences = useStrongReferences;
                this.doNotProcessSourceCollectionChanged = doNotProcessSourceCollectionChanged;
                this.doNotProcessTargetCollectionChanged = doNotProcessTargetCollectionChanged;
                this.sourceRef = useStrongReferences ? ((object) source) : ((object) new WeakReference(source));
                this.targetRef = useStrongReferences ? ((object) target) : ((object) new WeakReference(target));
                this.itemConverter = itemConverter;
                INotifyCollectionChanged changed = source as INotifyCollectionChanged;
                if (changed != null)
                {
                    changed.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnSourceCollectionChanged);
                }
            }

            private void Add(NotifyCollectionChangedEventArgs e)
            {
                this.doNotProcessTargetCollectionChanged.DoLockedActionIfNotLocked(this.doNotProcessSourceCollectionChanged, delegate {
                    IList<TSource> source = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).GetSource();
                    IList<TTarget> target = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).GetTarget();
                    if (((source != null) && (target != null)) && (source.Count != target.Count))
                    {
                        ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).AddCore(e, target);
                    }
                });
            }

            private void AddCore(NotifyCollectionChangedEventArgs e, IList<TTarget> target)
            {
                int index = this.reverse ? (((target.Count + 1) - e.NewStartingIndex) - e.NewItems.Count) : e.NewStartingIndex;
                foreach (TSource local in this.ReverseIfNeeded<TSource>(e.NewItems.Cast<TSource>()))
                {
                    target.Insert(index, this.itemConverter(local));
                    index++;
                }
            }

            public void Dispose()
            {
                INotifyCollectionChanged changed = (this.useStrongReferences ? ((INotifyCollectionChanged) this.sourceRef) : ((INotifyCollectionChanged) ((WeakReference) this.sourceRef).Target)) as INotifyCollectionChanged;
                if (changed != null)
                {
                    changed.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnSourceCollectionChanged);
                }
            }

            private IList<T> GetList<T>(object listRef)
            {
                if (listRef == null)
                {
                    return null;
                }
                IList<T> list = listRef as IList<T>;
                return ((list != null) ? list : ListAdapter<T>.FromObjectList((IList) listRef));
            }

            private IEnumerable GetListObject(IEnumerable source)
            {
                IList<TSource> list = source as IList<TSource>;
                if (list != null)
                {
                    return list;
                }
                IList list2 = source as IList;
                return ((list2 == null) ? source.Cast<TSource>().ToList<TSource>() : list2);
            }

            private IList<TSource> GetSource() => 
                this.GetList<TSource>(this.useStrongReferences ? this.sourceRef : ((WeakReference) this.sourceRef).Target);

            private IList<TTarget> GetTarget() => 
                this.GetList<TTarget>(this.useStrongReferences ? this.targetRef : ((WeakReference) this.targetRef).Target);

            private void Move(NotifyCollectionChangedEventArgs e)
            {
                this.doNotProcessTargetCollectionChanged.DoLockedActionIfNotLocked(this.doNotProcessSourceCollectionChanged, delegate {
                    IList<TTarget> target = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).GetTarget();
                    if (target != null)
                    {
                        ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).RemoveCore(e, target);
                        ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).AddCore(e, target);
                    }
                });
            }

            private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                IList<TSource> source = this.GetSource();
                if (this.GetTarget() != null)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this.Add(e);
                            return;

                        case NotifyCollectionChangedAction.Remove:
                            this.Remove(e);
                            return;

                        case NotifyCollectionChangedAction.Replace:
                            this.Replace(e);
                            return;

                        case NotifyCollectionChangedAction.Move:
                            this.Move(e);
                            return;
                    }
                    this.Reset();
                }
            }

            private void Remove(NotifyCollectionChangedEventArgs e)
            {
                this.doNotProcessTargetCollectionChanged.DoLockedActionIfNotLocked(this.doNotProcessSourceCollectionChanged, delegate {
                    IList<TSource> source = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).GetSource();
                    IList<TTarget> target = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).GetTarget();
                    if (((source != null) && (target != null)) && (source.Count != target.Count))
                    {
                        ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).RemoveCore(e, target);
                    }
                });
            }

            private void RemoveCore(NotifyCollectionChangedEventArgs e, IList<TTarget> target)
            {
                int index = this.reverse ? ((target.Count - e.OldStartingIndex) - 1) : ((e.OldStartingIndex + e.OldItems.Count) - 1);
                int count = e.OldItems.Count;
                while (--count >= 0)
                {
                    target.RemoveAt(index);
                    index--;
                }
            }

            private void Replace(NotifyCollectionChangedEventArgs e)
            {
                this.doNotProcessTargetCollectionChanged.DoLockedActionIfNotLocked(this.doNotProcessSourceCollectionChanged, delegate {
                    IList<TTarget> target = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).GetTarget();
                    if ((((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).GetSource() != null) && (target != null))
                    {
                        int num = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).reverse ? ((target.Count - e.NewStartingIndex) - e.NewItems.Count) : e.NewStartingIndex;
                        foreach (TSource local in ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).ReverseIfNeeded<TSource>(e.NewItems.Cast<TSource>()))
                        {
                            target[num] = ((CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>) this).itemConverter(local);
                            num++;
                        }
                    }
                });
            }

            public void Reset()
            {
                this.doNotProcessTargetCollectionChanged.DoLockedActionIfNotLocked(this.doNotProcessSourceCollectionChanged, delegate {
                    IList<TSource> source = base.GetSource();
                    IList<TTarget> target = base.GetTarget();
                    if ((source != null) && (target != null))
                    {
                        target.Clear();
                        foreach (TSource local in base.ReverseIfNeeded<TSource>(source))
                        {
                            target.Add(base.itemConverter(local));
                        }
                    }
                });
            }

            private IEnumerable<T> ReverseIfNeeded<T>(IEnumerable<T> items) => 
                this.reverse ? items.Reverse<T>() : items;
        }

        private sealed class CollectionTwoWayBinding<TTarget, TSource> : IDisposable
        {
            private CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource> sourceToTarget;
            private CollectionBindingHelper.CollectionOneWayBinding<TSource, TTarget> targetToSource;

            public CollectionTwoWayBinding(IEnumerable target, Func<TSource, TTarget> itemConverter, IEnumerable source, Func<TTarget, TSource> itemBackConverter, bool reverse, bool useStrongReferences)
            {
                CollectionBindingHelper.CollectionLocker doNotProcessTargetCollectionChanged = new CollectionBindingHelper.CollectionLocker();
                CollectionBindingHelper.CollectionLocker doNotProcessSourceCollectionChanged = new CollectionBindingHelper.CollectionLocker();
                this.targetToSource = new CollectionBindingHelper.CollectionOneWayBinding<TSource, TTarget>(source, itemBackConverter, target, doNotProcessSourceCollectionChanged, doNotProcessTargetCollectionChanged, reverse, useStrongReferences);
                this.sourceToTarget = new CollectionBindingHelper.CollectionOneWayBinding<TTarget, TSource>(target, itemConverter, source, doNotProcessTargetCollectionChanged, doNotProcessSourceCollectionChanged, reverse, useStrongReferences);
            }

            public void Dispose()
            {
                this.sourceToTarget.Dispose();
                this.sourceToTarget = null;
                this.targetToSource.Dispose();
                this.targetToSource = null;
            }

            public void Reset()
            {
                this.sourceToTarget.Reset();
            }
        }
    }
}

