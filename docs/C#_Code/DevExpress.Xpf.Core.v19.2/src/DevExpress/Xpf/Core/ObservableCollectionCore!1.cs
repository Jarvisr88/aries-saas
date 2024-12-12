namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ObservableCollectionCore<T> : ObservableCollection<T>, ILockable, ISupportGetCachedIndex<T>
    {
        private Lazy<Dictionary<T, int>> indexCacheValue;
        private int lockCount;

        public ObservableCollectionCore()
        {
            Func<Dictionary<T, int>> valueFactory = <>c<T>.<>9__2_0;
            if (<>c<T>.<>9__2_0 == null)
            {
                Func<Dictionary<T, int>> local1 = <>c<T>.<>9__2_0;
                valueFactory = <>c<T>.<>9__2_0 = () => new Dictionary<T, int>();
            }
            this.indexCacheValue = new Lazy<Dictionary<T, int>>(valueFactory);
        }

        public void Assign(IList<T> source)
        {
            if (this.NeedsRebuild(source))
            {
                this.BeginUpdate();
                base.Clear();
                foreach (T local in source)
                {
                    base.Add(local);
                }
                this.EndUpdate();
            }
        }

        protected virtual void BaseOnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }

        public virtual void BeginUpdate()
        {
            this.lockCount++;
        }

        protected internal void CancelUpdate()
        {
            this.lockCount--;
        }

        public virtual void EndUpdate()
        {
            this.CancelUpdate();
            if (!this.IsLockUpdate)
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public int GetCachedIndex(T item)
        {
            int num;
            if (!this.indexCacheValue.Value.TryGetValue(item, out num))
            {
                this.indexCacheValue.Value[item] = num = base.IndexOf(item);
            }
            return num;
        }

        public bool NeedsRebuild(IList<T> source) => 
            !ListHelper.AreEqual<T>(this, source);

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.indexCacheValue.IsValueCreated)
            {
                this.indexCacheValue.Value.Clear();
            }
            if (!this.IsLockUpdate)
            {
                this.BaseOnCollectionChanged(e);
            }
        }

        [Obsolete("Use Remove method instead"), EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveSafe(T item)
        {
            base.Remove(item);
        }

        public bool IsLockUpdate =>
            this.lockCount != 0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ObservableCollectionCore<T>.<>c <>9;
            public static Func<Dictionary<T, int>> <>9__2_0;

            static <>c()
            {
                ObservableCollectionCore<T>.<>c.<>9 = new ObservableCollectionCore<T>.<>c();
            }

            internal Dictionary<T, int> <.ctor>b__2_0() => 
                new Dictionary<T, int>();
        }
    }
}

