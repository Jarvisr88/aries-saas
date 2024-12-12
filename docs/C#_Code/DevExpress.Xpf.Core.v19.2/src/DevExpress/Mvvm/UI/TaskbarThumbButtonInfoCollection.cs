namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Shell;

    public class TaskbarThumbButtonInfoCollection : IList<TaskbarThumbButtonInfo>, ICollection<TaskbarThumbButtonInfo>, IEnumerable<TaskbarThumbButtonInfo>, IEnumerable, IList, ICollection
    {
        private object syncRoot;

        public TaskbarThumbButtonInfoCollection() : this(new ThumbButtonInfoCollection())
        {
        }

        public TaskbarThumbButtonInfoCollection(ThumbButtonInfoCollection collection)
        {
            this.syncRoot = new object();
            GuardHelper.ArgumentNotNull(collection, "collection");
            this.InternalCollection = collection;
        }

        public void Add(TaskbarThumbButtonInfo item)
        {
            this.InternalCollection.Add(TaskbarThumbButtonInfoWrapper.Wrap(item));
        }

        public void Clear()
        {
            this.InternalCollection.Clear();
        }

        public bool Contains(TaskbarThumbButtonInfo item) => 
            this.InternalCollection.Contains(TaskbarThumbButtonInfoWrapper.Wrap(item));

        public void CopyTo(TaskbarThumbButtonInfo[] array, int arrayIndex)
        {
            foreach (TaskbarThumbButtonInfo info in this)
            {
                array[arrayIndex] = info;
                arrayIndex++;
            }
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__21))]
        public IEnumerator<TaskbarThumbButtonInfo> GetEnumerator()
        {
            <GetEnumerator>d__21 d__1 = new <GetEnumerator>d__21(0);
            d__1.<>4__this = this;
            return d__1;
        }

        public int IndexOf(TaskbarThumbButtonInfo item) => 
            this.InternalCollection.IndexOf(TaskbarThumbButtonInfoWrapper.Wrap(item));

        public void Insert(int index, TaskbarThumbButtonInfo item)
        {
            this.InternalCollection.Insert(index, TaskbarThumbButtonInfoWrapper.Wrap(item));
        }

        public bool Remove(TaskbarThumbButtonInfo item) => 
            this.InternalCollection.Remove(TaskbarThumbButtonInfoWrapper.Wrap(item));

        public void RemoveAt(int index)
        {
            this.InternalCollection.RemoveAt(index);
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CopyTo((TaskbarThumbButtonInfo[]) array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        int IList.Add(object value)
        {
            this.Add((TaskbarThumbButtonInfo) value);
            return (this.Count - 1);
        }

        bool IList.Contains(object value) => 
            this.Contains((TaskbarThumbButtonInfo) value);

        int IList.IndexOf(object value) => 
            this.IndexOf((TaskbarThumbButtonInfo) value);

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (TaskbarThumbButtonInfo) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((TaskbarThumbButtonInfo) value);
        }

        internal ThumbButtonInfoCollection InternalCollection { get; private set; }

        public TaskbarThumbButtonInfo this[int index]
        {
            get => 
                TaskbarThumbButtonInfoWrapper.UnWrap(this.InternalCollection[index]);
            set => 
                this.InternalCollection[index] = TaskbarThumbButtonInfoWrapper.Wrap(value);
        }

        public int Count =>
            this.InternalCollection.Count;

        public bool IsReadOnly =>
            false;

        bool IList.IsFixedSize =>
            false;

        object IList.this[int index]
        {
            get => 
                this[index];
            set => 
                this[index] = (TaskbarThumbButtonInfo) value;
        }

        bool ICollection.IsSynchronized =>
            false;

        object ICollection.SyncRoot =>
            this.syncRoot;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__21 : IEnumerator<TaskbarThumbButtonInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TaskbarThumbButtonInfo <>2__current;
            public TaskbarThumbButtonInfoCollection <>4__this;
            private FreezableCollection<ThumbButtonInfo>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__21(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                this.<>7__wrap1.Dispose();
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
                        this.<>7__wrap1 = this.<>4__this.InternalCollection.GetEnumerator();
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
                        this.<>7__wrap1 = new FreezableCollection<ThumbButtonInfo>.Enumerator();
                        flag = false;
                    }
                    else
                    {
                        ThumbButtonInfo current = this.<>7__wrap1.Current;
                        this.<>2__current = TaskbarThumbButtonInfoWrapper.UnWrap(current);
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

            TaskbarThumbButtonInfo IEnumerator<TaskbarThumbButtonInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

