namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class EditorsCompositeCollection : IList, ICollection, IEnumerable, INotifyCollectionChanged, IDisposable
    {
        private NotifyCollectionChangedEventHandler handler;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                this.handler += value;
            }
            remove
            {
                this.handler += value;
            }
        }

        public EditorsCompositeCollection()
        {
            this.ContentCollection = new CollectionContainer();
            this.CustomCollection = new CollectionContainer();
            this.CustomCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnContainedCollectionChanged);
            this.ContentCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnContainedCollectionChanged);
        }

        public bool Contains(object value) => 
            this.IndexOf(value) > -1;

        public void CopyTo(Array array, int index)
        {
            for (int i = index; i < this.Count; i++)
            {
                array.SetValue(this[i], i);
            }
        }

        public void Dispose()
        {
            this.CustomCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnContainedCollectionChanged);
            this.ContentCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnContainedCollectionChanged);
            this.ContentCollection = null;
            this.CustomCollection = null;
        }

        private int GetContentCollectionCount()
        {
            Func<IList, int> evaluator = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                Func<IList, int> local1 = <>c.<>9__49_0;
                evaluator = <>c.<>9__49_0 = x => x.Count;
            }
            return this.ContentList.Return<IList, int>(evaluator, (<>c.<>9__49_1 ??= () => 0));
        }

        private int GetCountInternal() => 
            this.GetCustomCollectionCount() + this.GetContentCollectionCount();

        internal int GetCustomCollectionCount()
        {
            Func<IList, int> evaluator = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                Func<IList, int> local1 = <>c.<>9__50_0;
                evaluator = <>c.<>9__50_0 = x => x.Count;
            }
            return this.CustomList.Return<IList, int>(evaluator, (<>c.<>9__50_1 ??= () => 0));
        }

        public IEnumerator GetEnumerator()
        {
            // Unresolved stack state at '00000079'
        }

        private object IndexerGetInternal(int index)
        {
            if (this.Count <= index)
            {
                return null;
            }
            int customCollectionCount = this.GetCustomCollectionCount();
            return ((customCollectionCount <= index) ? this.ContentList[index - customCollectionCount] : this.CustomList[index]);
        }

        public int IndexOf(object value)
        {
            if (this.IsCustomCollectionContains(value))
            {
                return this.CustomList.IndexOf(value);
            }
            if (this.ContentList == null)
            {
                return -1;
            }
            int index = this.ContentList.IndexOf(value);
            return ((index > -1) ? (index + this.GetCustomCollectionCount()) : index);
        }

        private bool IsCustomCollectionContains(object value)
        {
            Func<bool> fallback = <>c.<>9__51_1;
            if (<>c.<>9__51_1 == null)
            {
                Func<bool> local1 = <>c.<>9__51_1;
                fallback = <>c.<>9__51_1 = () => false;
            }
            return this.CustomList.Return<IList, bool>(x => x.Contains(value), fallback);
        }

        private void OnContainedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int num = ReferenceEquals(((CollectionContainer) sender).Collection, this.CustomList) ? 0 : this.CustomList.Count;
            int startingIndex = (e.OldStartingIndex >= 0) ? (e.OldStartingIndex + num) : e.OldStartingIndex;
            int index = (e.NewStartingIndex >= 0) ? (e.NewStartingIndex + num) : e.NewStartingIndex;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e = new NotifyCollectionChangedEventArgs(e.Action, e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    e = new NotifyCollectionChangedEventArgs(e.Action, e.OldItems, startingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    e = new NotifyCollectionChangedEventArgs(e.Action, e.NewItems, e.OldItems, startingIndex);
                    break;

                case NotifyCollectionChangedAction.Move:
                    e = new NotifyCollectionChangedEventArgs(e.Action, e.OldItems, index, startingIndex);
                    break;

                default:
                    break;
            }
            this.RaiseCollectionChanged(e);
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.handler != null)
            {
                this.handler(this, e);
            }
        }

        public void SetContentCollection(IList contentItemsSource)
        {
            this.ContentCollection.Collection = contentItemsSource;
        }

        public void SetCustomCollection(IList customItemsSource)
        {
            this.CustomCollection.Collection = customItemsSource;
        }

        int IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Clear()
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get => 
                this.IndexerGetInternal(index);
            set => 
                this[index] = value;
        }

        public int Count =>
            this.GetCountInternal();

        public bool IsFixedSize { get; private set; }

        public bool IsReadOnly { get; private set; }

        public object SyncRoot { get; private set; }

        public bool IsSynchronized { get; private set; }

        private IList CustomList =>
            this.CustomCollection.Collection as IList;

        private IList ContentList =>
            this.ContentCollection.Collection as IList;

        private CollectionContainer ContentCollection { get; set; }

        private CollectionContainer CustomCollection { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorsCompositeCollection.<>c <>9 = new EditorsCompositeCollection.<>c();
            public static Func<IList, IEnumerator> <>9__40_0;
            public static Func<IEnumerator> <>9__40_1;
            public static Func<IList, IEnumerator> <>9__40_2;
            public static Func<IEnumerator> <>9__40_3;
            public static Func<IList, int> <>9__49_0;
            public static Func<int> <>9__49_1;
            public static Func<IList, int> <>9__50_0;
            public static Func<int> <>9__50_1;
            public static Func<bool> <>9__51_1;

            internal int <GetContentCollectionCount>b__49_0(IList x) => 
                x.Count;

            internal int <GetContentCollectionCount>b__49_1() => 
                0;

            internal int <GetCustomCollectionCount>b__50_0(IList x) => 
                x.Count;

            internal int <GetCustomCollectionCount>b__50_1() => 
                0;

            internal IEnumerator <GetEnumerator>b__40_0(IList x) => 
                x.GetEnumerator();

            internal IEnumerator <GetEnumerator>b__40_1() => 
                null;

            internal IEnumerator <GetEnumerator>b__40_2(IList x) => 
                x.GetEnumerator();

            internal IEnumerator <GetEnumerator>b__40_3() => 
                null;

            internal bool <IsCustomCollectionContains>b__51_1() => 
                false;
        }
    }
}

