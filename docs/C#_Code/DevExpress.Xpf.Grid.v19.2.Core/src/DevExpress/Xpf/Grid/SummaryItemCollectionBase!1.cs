namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class SummaryItemCollectionBase<T> : ObservableCollectionCore<T>, ISummaryItemOwner, IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase>, IEnumerable, INotifyCollectionChanged, IList, ICollection, ISupportGetCachedIndex<DevExpress.Xpf.Grid.SummaryItemBase> where T: DevExpress.Xpf.Grid.SummaryItemBase, new()
    {
        private readonly DataControlBase dataControl;
        private readonly SummaryItemCollectionType collectionType;
        private Locker originationElementCollectionChangedLocker;

        protected SummaryItemCollectionBase(DataControlBase dataControl, SummaryItemCollectionType collectionType)
        {
            this.originationElementCollectionChangedLocker = new Locker();
            this.dataControl = dataControl;
            this.collectionType = collectionType;
        }

        public T Add(SummaryItemType summaryType, string fieldName)
        {
            T item = Activator.CreateInstance<T>();
            item.SummaryType = summaryType;
            item.FieldName = fieldName;
            base.Add(item);
            return item;
        }

        int ISupportGetCachedIndex<DevExpress.Xpf.Grid.SummaryItemBase>.GetCachedIndex(DevExpress.Xpf.Grid.SummaryItemBase item) => 
            base.GetCachedIndex((T) item);

        void ISummaryItemOwner.Add(DevExpress.Xpf.Grid.SummaryItemBase item)
        {
            base.Add((T) item);
        }

        void ISummaryItemOwner.OnSummaryChanged(DevExpress.Xpf.Grid.SummaryItemBase summaryItem, DependencyPropertyChangedEventArgs e)
        {
            this.dataControl.GetDataControlOriginationElement().NotifyPropertyChanged(this.dataControl, e.Property, (Func<DataControlBase, DependencyObject>) (dc => CloneDetailHelper.SafeGetDependentCollectionItem<DevExpress.Xpf.Grid.SummaryItemBase>(summaryItem, (SummaryItemCollectionBase<T>) this, (((SummaryItemCollectionBase<T>) this).collectionType == SummaryItemCollectionType.Group) ? dc.GroupSummaryCore : dc.TotalSummaryCore)), typeof(DevExpress.Xpf.Grid.SummaryItemBase));
            this.originationElementCollectionChangedLocker.DoLockedAction(() => this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)));
        }

        void ISummaryItemOwner.Remove(DevExpress.Xpf.Grid.SummaryItemBase item)
        {
            base.Remove((T) item);
        }

        protected internal List<T> GetActiveItems()
        {
            List<T> list = new List<T>();
            foreach (T local in this)
            {
                if (local.SummaryType != SummaryItemType.None)
                {
                    list.Add(local);
                }
            }
            return list;
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.Collection = this;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            <>c__DisplayClass10_0<T> class_;
            this.originationElementCollectionChangedLocker.DoIfNotLocked(delegate {
                Func<object, object> convertAction = <>c<T>.<>9__10_2;
                if (<>c<T>.<>9__10_2 == null)
                {
                    Func<object, object> local1 = <>c<T>.<>9__10_2;
                    convertAction = <>c<T>.<>9__10_2 = summaryItem => CloneDetailHelper.CloneElement<DevExpress.Xpf.Grid.SummaryItemBase>((DevExpress.Xpf.Grid.SummaryItemBase) summaryItem, (Action<DevExpress.Xpf.Grid.SummaryItemBase>) null, (Func<DevExpress.Xpf.Grid.SummaryItemBase, Locker>) null, (object[]) null);
                }
                ((SummaryItemCollectionBase<T>) this).dataControl.GetDataControlOriginationElement().NotifyCollectionChanged(((SummaryItemCollectionBase<T>) this).dataControl, dc => (class_.collectionType == SummaryItemCollectionType.Total) ? dc.TotalSummaryCore : dc.GroupSummaryCore, convertAction, e);
            });
            base.OnCollectionChanged(e);
        }

        protected override void RemoveItem(int index)
        {
            base[index].Collection = null;
            base.RemoveItem(index);
        }

        [IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<DevExpress-Xpf-Grid-SummaryItemBase>-GetEnumerator>d__14))]
        IEnumerator<DevExpress.Xpf.Grid.SummaryItemBase> IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase>.GetEnumerator()
        {
            <System-Collections-Generic-IEnumerable<DevExpress-Xpf-Grid-SummaryItemBase>-GetEnumerator>d__14<T> d__1 = new <System-Collections-Generic-IEnumerable<DevExpress-Xpf-Grid-SummaryItemBase>-GetEnumerator>d__14<T>(0);
            d__1.<>4__this = (SummaryItemCollectionBase<T>) this;
            return d__1;
        }

        public override string ToString() => 
            string.Empty;

        DevExpress.Xpf.Grid.SummaryItemBase ISummaryItemOwner.this[int index] =>
            base[index];

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SummaryItemCollectionBase<T>.<>c <>9;
            public static Func<object, object> <>9__10_2;

            static <>c()
            {
                SummaryItemCollectionBase<T>.<>c.<>9 = new SummaryItemCollectionBase<T>.<>c();
            }

            internal object <OnCollectionChanged>b__10_2(object summaryItem) => 
                CloneDetailHelper.CloneElement<DevExpress.Xpf.Grid.SummaryItemBase>((DevExpress.Xpf.Grid.SummaryItemBase) summaryItem, (Action<DevExpress.Xpf.Grid.SummaryItemBase>) null, (Func<DevExpress.Xpf.Grid.SummaryItemBase, Locker>) null, (object[]) null);
        }

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<DevExpress-Xpf-Grid-SummaryItemBase>-GetEnumerator>d__14 : IEnumerator<DevExpress.Xpf.Grid.SummaryItemBase>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DevExpress.Xpf.Grid.SummaryItemBase <>2__current;
            public SummaryItemCollectionBase<T> <>4__this;
            private IEnumerator<T> <>7__wrap1;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<DevExpress-Xpf-Grid-SummaryItemBase>-GetEnumerator>d__14(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        DevExpress.Xpf.Grid.SummaryItemBase current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            DevExpress.Xpf.Grid.SummaryItemBase IEnumerator<DevExpress.Xpf.Grid.SummaryItemBase>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

