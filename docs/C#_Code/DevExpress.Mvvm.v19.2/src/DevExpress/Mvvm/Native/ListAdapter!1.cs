namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ListAdapter<T>
    {
        public static IList<T> FromObject(Func<T> getValue, Action<T> setValue) => 
            new SingletonList<T>(getValue, setValue, false, default(T));

        public static IList<T> FromObject(Func<T> getValue, Action<T> setValue, T emptyValue) => 
            new SingletonList<T>(getValue, setValue, true, emptyValue);

        public static IList<T> FromObjectList(IList objectList) => 
            new ObjectList<T>(objectList);

        public static IList<T> FromTwoLists<T1, T2>(IList<T1> list1, IList<T2> list2, Func<T, bool> addItemToFirstList = null) where T1: T where T2: T
        {
            Func<T, bool> func1 = addItemToFirstList;
            if (addItemToFirstList == null)
            {
                Func<T, bool> local1 = addItemToFirstList;
                func1 = <>c__5<T, T1, T2>.<>9__5_0;
                if (<>c__5<T, T1, T2>.<>9__5_0 == null)
                {
                    Func<T, bool> local2 = <>c__5<T, T1, T2>.<>9__5_0;
                    func1 = <>c__5<T, T1, T2>.<>9__5_0 = x => x is T1;
                }
            }
            return new CombinedList<T, T1, T2>(list1, list2, func1);
        }

        public static IList<T> FromTwoObjectLists<T1, T2>(IList objectList1, IList objectlist2, Func<T, bool> addItemToFirstList = null) where T1: T where T2: T => 
            ListAdapter<T>.FromTwoLists<T1, T2>(ListAdapter<T1>.FromObjectList(objectList1), ListAdapter<T2>.FromObjectList(objectlist2), addItemToFirstList);

        public static IList<T> FromUnsortedList(IList<T> list, Func<T, int> getInsertIndex) => 
            new ResortedList<T>(list, getInsertIndex);

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<T1, T2> where T1: T where T2: T
        {
            public static readonly ListAdapter<T>.<>c__5<T1, T2> <>9;
            public static Func<T, bool> <>9__5_0;

            static <>c__5()
            {
                ListAdapter<T>.<>c__5<T1, T2>.<>9 = new ListAdapter<T>.<>c__5<T1, T2>();
            }

            internal bool <FromTwoLists>b__5_0(T x) => 
                x is T1;
        }

        private sealed class CombinedList<T1, T2> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IMappedList where T1: T where T2: T
        {
            private readonly IList<T1> list1;
            private readonly IList<T2> list2;
            private readonly Func<T, bool> addItemToFirstList;
            private List<int> publicIndexes;
            private List<int> innerIndexes;

            public CombinedList(IList<T1> list1, IList<T2> list2, Func<T, bool> addItemToFirstList)
            {
                this.list1 = list1;
                this.list2 = list2;
                this.addItemToFirstList = addItemToFirstList;
            }

            public void Add(T item)
            {
                this.Insert(this.Count, item);
            }

            public void Clear()
            {
                this.list1.Clear();
                this.list2.Clear();
                this.publicIndexes.Clear();
                this.innerIndexes.Clear();
            }

            public bool Contains(T item)
            {
                if (item != null)
                {
                    return ((!(item is T1) || !this.list1.Contains((T1) item)) ? ((item is T2) && this.list2.Contains((T2) item)) : true);
                }
                T1 local = default(T1);
                if (this.list1.Contains(local))
                {
                    return true;
                }
                T2 local2 = default(T2);
                return this.list2.Contains(local2);
            }

            [IteratorStateMachine(typeof(<GetEnumerator>d__31<,,>))]
            public IEnumerator<T> GetEnumerator()
            {
                <GetEnumerator>d__31<T, T1, T2> d__1 = new <GetEnumerator>d__31<T, T1, T2>(0);
                d__1.<>4__this = (ListAdapter<T>.CombinedList<T1, T2>) this;
                return d__1;
            }

            private T GetItemCore(int index)
            {
                int num = this.innerIndexes[index];
                return ((num >= this.list1.Count) ? this.list2[num - this.list1.Count] : this.list1[num]);
            }

            public int IndexOf(T item)
            {
                this.UpdateIndexMap();
                if (item == null)
                {
                    T1 local = default(T1);
                    int index = this.list1.IndexOf(local);
                    if (index >= 0)
                    {
                        return this.publicIndexes[index];
                    }
                    T2 local2 = default(T2);
                    int num2 = this.list2.IndexOf(local2);
                    if (num2 >= 0)
                    {
                        return this.publicIndexes[num2 + this.list1.Count];
                    }
                }
                if (item is T1)
                {
                    int index = this.list1.IndexOf((T1) item);
                    if (index >= 0)
                    {
                        return this.publicIndexes[index];
                    }
                }
                if (item is T2)
                {
                    int index = this.list2.IndexOf((T2) item);
                    if (index >= 0)
                    {
                        return this.publicIndexes[index + this.list1.Count];
                    }
                }
                return -1;
            }

            private int InnerIndex(int publicIndex) => 
                this.innerIndexes[publicIndex];

            public void Insert(int publicIndex, T item)
            {
                int num;
                if ((publicIndex < 0) || (publicIndex > this.Count))
                {
                    throw new ArgumentException("", "publicIndex");
                }
                this.UpdateIndexMap();
                if (this.addItemToFirstList(item))
                {
                    int num2 = this.publicIndexes.Take<int>(this.list1.Count).Reverse<int>().TakeWhile<int>(p => (p >= publicIndex)).Count<int>();
                    num = this.list1.Count - num2;
                    this.list1.Insert(num, (T1) item);
                }
                else
                {
                    int num3 = this.publicIndexes.Skip<int>(this.list1.Count).Reverse<int>().TakeWhile<int>(p => (p >= publicIndex)).Count<int>();
                    num = this.list1.Count + (this.list2.Count - num3);
                    this.list2.Insert(num - this.list1.Count, (T2) item);
                }
                for (int i = num; i < this.publicIndexes.Count; i++)
                {
                    int num5 = this.publicIndexes[i];
                    int num6 = this.innerIndexes[num5] + 1;
                    this.innerIndexes[num5] = num6;
                }
                this.innerIndexes.Add(num);
                this.publicIndexes.Insert(num, this.innerIndexes.Count - 1);
                this.MoveItemVirtual(this.innerIndexes.Count - 1, publicIndex);
            }

            private void MoveItemVirtual(int oldPublicIndex, int newPublicIndex)
            {
                if (oldPublicIndex < newPublicIndex)
                {
                    for (int i = oldPublicIndex; i < newPublicIndex; i++)
                    {
                        this.SwapItemsVirtual(i, i + 1);
                    }
                }
                else
                {
                    for (int i = oldPublicIndex; i > newPublicIndex; i--)
                    {
                        this.SwapItemsVirtual(i, i - 1);
                    }
                }
            }

            private int PublicIndex(int innerIndex) => 
                this.publicIndexes[innerIndex];

            public bool Remove(T item)
            {
                int index = this.IndexOf(item);
                if (index < 0)
                {
                    return false;
                }
                this.RemoveAt(index);
                return true;
            }

            public void RemoveAt(int publicIndex)
            {
                this.UpdateIndexMap();
                int index = this.innerIndexes[publicIndex];
                this.MoveItemVirtual(publicIndex, this.innerIndexes.Count - 1);
                this.publicIndexes.RemoveAt(index);
                this.innerIndexes.RemoveAt(this.innerIndexes.Count - 1);
                for (int i = index; i < this.publicIndexes.Count; i++)
                {
                    int num3 = this.publicIndexes[i];
                    int num4 = this.innerIndexes[num3] - 1;
                    this.innerIndexes[num3] = num4;
                }
                if (index < this.list1.Count)
                {
                    this.list1.RemoveAt(index);
                }
                else
                {
                    this.list2.RemoveAt(index - this.list1.Count);
                }
            }

            private int SetSubMap<U>(IList<U> sublist, IEnumerable<int> map)
            {
                IMappedList list = sublist as IMappedList;
                if (list == null)
                {
                    return 0;
                }
                list.Map = map.ToList<int>();
                return sublist.Count;
            }

            private void SwapItemsVirtual(int publicIndex1, int publicIndex2)
            {
                int num = this.innerIndexes[publicIndex1];
                int num2 = this.innerIndexes[publicIndex2];
                this.publicIndexes[num] = publicIndex2;
                this.publicIndexes[num2] = publicIndex1;
                this.innerIndexes[publicIndex1] = num2;
                this.innerIndexes[publicIndex2] = num;
            }

            void ICollection<T>.CopyTo(T[] array, int arrayIndex)
            {
                foreach (T local in this)
                {
                    array[arrayIndex++] = local;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            private void UpdateIndexMap()
            {
                int count = this.Count;
                if (this.publicIndexes == null)
                {
                    this.publicIndexes = Enumerable.Range(0, count).ToList<int>();
                    this.innerIndexes = Enumerable.Range(0, count).ToList<int>();
                }
                else if (this.publicIndexes.Count != count)
                {
                    throw new InvalidOperationException();
                }
            }

            IList<int> IMappedList.Map
            {
                get
                {
                    // Unresolved stack state at '0000007F'
                }
                set
                {
                    this.UpdateIndexMap();
                    this.publicIndexes = value.Take<int>(this.Count).ToList<int>();
                    this.innerIndexes = this.publicIndexes.ToList<int>();
                    for (int i = 0; i < this.innerIndexes.Count; i++)
                    {
                        this.innerIndexes[this.publicIndexes[i]] = i;
                    }
                    int num = this.SetSubMap<T1>(this.list1, value.Skip<int>(this.Count));
                    this.SetSubMap<T2>(this.list2, value.Skip<int>(this.Count + num));
                }
            }

            public T this[int index]
            {
                get
                {
                    this.UpdateIndexMap();
                    return this.GetItemCore(index);
                }
                set
                {
                    this.UpdateIndexMap();
                    int num = this.innerIndexes[index];
                    if (num < this.list1.Count)
                    {
                        this.list1[num] = (T1) value;
                    }
                    else
                    {
                        this.list2[num - this.list1.Count] = (T2) value;
                    }
                }
            }

            public int Count =>
                this.list1.Count + this.list2.Count;

            bool ICollection<T>.IsReadOnly =>
                this.list1.IsReadOnly || this.list2.IsReadOnly;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ListAdapter<T>.CombinedList<T1, T2>.<>c <>9;
                public static Func<IMappedList, IList<int>> <>9__12_0;
                public static Func<IList<int>> <>9__12_1;
                public static Func<IMappedList, IList<int>> <>9__12_2;
                public static Func<IList<int>> <>9__12_3;

                static <>c()
                {
                    ListAdapter<T>.CombinedList<T1, T2>.<>c.<>9 = new ListAdapter<T>.CombinedList<T1, T2>.<>c();
                }

                internal IList<int> <DevExpress.Mvvm.Native.IMappedList.get_Map>b__12_0(IMappedList x) => 
                    x.Map;

                internal IList<int> <DevExpress.Mvvm.Native.IMappedList.get_Map>b__12_1() => 
                    new List<int>();

                internal IList<int> <DevExpress.Mvvm.Native.IMappedList.get_Map>b__12_2(IMappedList x) => 
                    x.Map;

                internal IList<int> <DevExpress.Mvvm.Native.IMappedList.get_Map>b__12_3() => 
                    new List<int>();
            }

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__31 : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public ListAdapter<T>.CombinedList<T1, T2> <>4__this;
                private int <i>5__1;
                private int <count>5__2;

                [DebuggerHidden]
                public <GetEnumerator>d__31(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>4__this.UpdateIndexMap();
                        this.<count>5__2 = this.<>4__this.publicIndexes.Count;
                        this.<i>5__1 = 0;
                    }
                    else
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                        int num2 = this.<i>5__1 + 1;
                        this.<i>5__1 = num2;
                    }
                    if (this.<i>5__1 >= this.<count>5__2)
                    {
                        return false;
                    }
                    this.<>2__current = this.<>4__this.GetItemCore(this.<i>5__1);
                    this.<>1__state = 1;
                    return true;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        private sealed class ObjectList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
        {
            private readonly IList innerList;

            public ObjectList(IList innerList)
            {
                this.innerList = innerList;
            }

            [IteratorStateMachine(typeof(<GetEnumerator>d__13<>))]
            public IEnumerator<T> GetEnumerator()
            {
                <GetEnumerator>d__13<T> d__1 = new <GetEnumerator>d__13<T>(0);
                d__1.<>4__this = (ListAdapter<T>.ObjectList) this;
                return d__1;
            }

            void ICollection<T>.Add(T item)
            {
                this.innerList.Add(item);
            }

            void ICollection<T>.Clear()
            {
                this.innerList.Clear();
            }

            bool ICollection<T>.Contains(T item) => 
                this.innerList.Contains(item);

            void ICollection<T>.CopyTo(T[] array, int arrayIndex)
            {
                this.innerList.CopyTo(array, arrayIndex);
            }

            bool ICollection<T>.Remove(T item)
            {
                int index = this.innerList.IndexOf(item);
                if (index < 0)
                {
                    return false;
                }
                this.innerList.RemoveAt(index);
                return true;
            }

            int IList<T>.IndexOf(T item) => 
                this.innerList.IndexOf(item);

            void IList<T>.Insert(int index, T item)
            {
                this.innerList.Insert(index, item);
            }

            void IList<T>.RemoveAt(int index)
            {
                this.innerList.RemoveAt(index);
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            T IList<T>.this[int index]
            {
                get => 
                    (T) this.innerList[index];
                set => 
                    this.innerList[index] = value;
            }

            int ICollection<T>.Count =>
                this.innerList.Count;

            bool ICollection<T>.IsReadOnly =>
                this.innerList.IsReadOnly;

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__13 : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public ListAdapter<T>.ObjectList <>4__this;
                private IEnumerator <>7__wrap1;

                [DebuggerHidden]
                public <GetEnumerator>d__13(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    IDisposable disposable = this.<>7__wrap1 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
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
                            this.<>7__wrap1 = this.<>4__this.innerList.GetEnumerator();
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
                            T current = (T) this.<>7__wrap1.Current;
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

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        private sealed class ResortedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IMappedList
        {
            private readonly IList<T> list;
            private readonly Func<T, int> getInsertIndexFunc;
            private List<int> publicIndexes;
            private List<int> innerIndexes;

            public ResortedList(IList<T> list, Func<T, int> getInsertIndexFunc)
            {
                this.list = list;
                this.getInsertIndexFunc = getInsertIndexFunc;
            }

            public void Add(T item)
            {
                this.Insert(this.Count, item);
            }

            public void Clear()
            {
                this.list.Clear();
                this.publicIndexes.Clear();
                this.innerIndexes.Clear();
            }

            public bool Contains(T item) => 
                this.list.Contains(item);

            [IteratorStateMachine(typeof(<GetEnumerator>d__30<>))]
            public IEnumerator<T> GetEnumerator()
            {
                <GetEnumerator>d__30<T> d__1 = new <GetEnumerator>d__30<T>(0);
                d__1.<>4__this = (ListAdapter<T>.ResortedList) this;
                return d__1;
            }

            private T GetItemCore(int index)
            {
                int num = this.innerIndexes[index];
                return this.list[num];
            }

            public int IndexOf(T item)
            {
                this.UpdateIndexMap();
                int index = this.list.IndexOf(item);
                return this.publicIndexes[index];
            }

            private int InnerIndex(int publicIndex) => 
                this.innerIndexes[publicIndex];

            public void Insert(int publicIndex, T item)
            {
                this.UpdateIndexMap();
                this.list.Add(item);
                int num = this.list.Count - 1;
                for (int i = num; i < this.publicIndexes.Count; i++)
                {
                    int num3 = this.publicIndexes[i];
                    int num4 = this.innerIndexes[num3] + 1;
                    this.innerIndexes[num3] = num4;
                }
                this.innerIndexes.Add(num);
                this.publicIndexes.Insert(num, this.innerIndexes.Count - 1);
                this.MoveItemVirtual(this.innerIndexes.Count - 1, publicIndex);
            }

            private void MoveItemVirtual(int oldPublicIndex, int newPublicIndex)
            {
                if (oldPublicIndex < newPublicIndex)
                {
                    for (int i = oldPublicIndex; i < newPublicIndex; i++)
                    {
                        this.SwapItemsVirtual(i, i + 1);
                    }
                }
                else
                {
                    for (int i = oldPublicIndex; i > newPublicIndex; i--)
                    {
                        this.SwapItemsVirtual(i, i - 1);
                    }
                }
            }

            private int PublicIndex(int innerIndex) => 
                this.publicIndexes[innerIndex];

            public bool Remove(T item)
            {
                int index = this.IndexOf(item);
                if (index < 0)
                {
                    return false;
                }
                this.RemoveAt(index);
                return true;
            }

            public void RemoveAt(int publicIndex)
            {
                this.UpdateIndexMap();
                int index = this.InnerIndex(publicIndex);
                this.MoveItemVirtual(publicIndex, this.innerIndexes.Count - 1);
                this.publicIndexes.RemoveAt(index);
                this.innerIndexes.RemoveAt(this.innerIndexes.Count - 1);
                for (int i = index; i < this.publicIndexes.Count; i++)
                {
                    int num3 = this.publicIndexes[i];
                    int num4 = this.innerIndexes[num3] - 1;
                    this.innerIndexes[num3] = num4;
                }
                this.list.RemoveAt(index);
            }

            private int SetSubMap<U>(IList<U> sublist, IEnumerable<int> map)
            {
                IMappedList list = sublist as IMappedList;
                if (list == null)
                {
                    return 0;
                }
                list.Map = map.ToList<int>();
                return sublist.Count;
            }

            private void SwapItemsVirtual(int publicIndex1, int publicIndex2)
            {
                int num = this.innerIndexes[publicIndex1];
                int num2 = this.innerIndexes[publicIndex2];
                this.publicIndexes[num] = publicIndex2;
                this.publicIndexes[num2] = publicIndex1;
                this.innerIndexes[publicIndex1] = num2;
                this.innerIndexes[publicIndex2] = num;
            }

            void ICollection<T>.CopyTo(T[] array, int arrayIndex)
            {
                foreach (T local in this)
                {
                    array[arrayIndex++] = local;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            private void UpdateIndexMap()
            {
                int count = this.list.Count;
                if ((this.publicIndexes == null) || (this.publicIndexes.Count != count))
                {
                    this.publicIndexes = Enumerable.Range(0, count).ToList<int>();
                    this.innerIndexes = Enumerable.Range(0, count).ToList<int>();
                    for (int i = 0; i < count; i++)
                    {
                        int num3 = this.getInsertIndexFunc(this.list[i]);
                        this.publicIndexes[i] = num3;
                        this.innerIndexes[num3] = i;
                    }
                }
            }

            public T this[int index]
            {
                get
                {
                    this.UpdateIndexMap();
                    return this.GetItemCore(index);
                }
                set
                {
                    this.UpdateIndexMap();
                    int num = this.innerIndexes[index];
                    this.list[num] = value;
                }
            }

            public int Count =>
                this.list.Count;

            bool ICollection<T>.IsReadOnly =>
                this.list.IsReadOnly;

            IList<int> IMappedList.Map
            {
                get
                {
                    this.UpdateIndexMap();
                    Func<IMappedList, IList<int>> evaluator = <>c<T>.<>9__22_0;
                    if (<>c<T>.<>9__22_0 == null)
                    {
                        Func<IMappedList, IList<int>> local1 = <>c<T>.<>9__22_0;
                        evaluator = <>c<T>.<>9__22_0 = x => x.Map;
                    }
                    IList<int> second = (this.list as IMappedList).Return<IMappedList, IList<int>>(evaluator, <>c<T>.<>9__22_1 ??= () => new List<int>());
                    return this.publicIndexes.Concat<int>(second).ToList<int>();
                }
                set
                {
                    this.UpdateIndexMap();
                    this.publicIndexes = value.Take<int>(this.Count).ToList<int>();
                    this.innerIndexes = this.publicIndexes.ToList<int>();
                    for (int i = 0; i < this.innerIndexes.Count; i++)
                    {
                        this.innerIndexes[this.publicIndexes[i]] = i;
                    }
                    this.SetSubMap<T>(this.list, value.Skip<int>(this.Count));
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ListAdapter<T>.ResortedList.<>c <>9;
                public static Func<IMappedList, IList<int>> <>9__22_0;
                public static Func<IList<int>> <>9__22_1;

                static <>c()
                {
                    ListAdapter<T>.ResortedList.<>c.<>9 = new ListAdapter<T>.ResortedList.<>c();
                }

                internal IList<int> <DevExpress.Mvvm.Native.IMappedList.get_Map>b__22_0(IMappedList x) => 
                    x.Map;

                internal IList<int> <DevExpress.Mvvm.Native.IMappedList.get_Map>b__22_1() => 
                    new List<int>();
            }

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__30 : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public ListAdapter<T>.ResortedList <>4__this;
                private int <i>5__1;
                private int <count>5__2;

                [DebuggerHidden]
                public <GetEnumerator>d__30(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>4__this.UpdateIndexMap();
                        this.<count>5__2 = this.<>4__this.publicIndexes.Count;
                        this.<i>5__1 = 0;
                    }
                    else
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                        int num2 = this.<i>5__1 + 1;
                        this.<i>5__1 = num2;
                    }
                    if (this.<i>5__1 >= this.<count>5__2)
                    {
                        return false;
                    }
                    this.<>2__current = this.<>4__this.GetItemCore(this.<i>5__1);
                    this.<>1__state = 1;
                    return true;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        private sealed class SingletonList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
        {
            private readonly bool hasEmptyValue;
            private readonly T emptyValue;
            private readonly Func<T> getValue;
            private readonly Action<T> setValue;

            public SingletonList(Func<T> getValue, Action<T> setValue, bool hasEmptyValue, T emptyValue)
            {
                this.getValue = getValue;
                this.setValue = setValue;
                this.hasEmptyValue = hasEmptyValue;
                this.emptyValue = emptyValue;
            }

            private bool Contains(T item) => 
                !this.IsEmpty && Equals(item, this.getValue());

            [IteratorStateMachine(typeof(<GetEnumerator>d__20<>))]
            public IEnumerator<T> GetEnumerator()
            {
                <GetEnumerator>d__20<T> d__1 = new <GetEnumerator>d__20<T>(0);
                d__1.<>4__this = (ListAdapter<T>.SingletonList) this;
                return d__1;
            }

            void ICollection<T>.Add(T item)
            {
                if (!this.IsEmpty)
                {
                    throw new NotSupportedException();
                }
                this.setValue(item);
            }

            void ICollection<T>.Clear()
            {
                if (!this.hasEmptyValue)
                {
                    throw new NotSupportedException();
                }
                this.setValue(this.emptyValue);
            }

            bool ICollection<T>.Contains(T item) => 
                this.Contains(item);

            void ICollection<T>.CopyTo(T[] array, int arrayIndex)
            {
                if (!this.IsEmpty)
                {
                    array[arrayIndex] = this.getValue();
                }
            }

            bool ICollection<T>.Remove(T item)
            {
                if (!this.Contains(item))
                {
                    return false;
                }
                if (!this.hasEmptyValue)
                {
                    throw new NotSupportedException();
                }
                this.setValue(this.emptyValue);
                return true;
            }

            int IList<T>.IndexOf(T item) => 
                this.Contains(item) ? 0 : -1;

            void IList<T>.Insert(int index, T item)
            {
                if (!this.IsEmpty)
                {
                    throw new NotSupportedException();
                }
                if (index != 0)
                {
                    throw new InvalidOperationException();
                }
                this.setValue(item);
            }

            void IList<T>.RemoveAt(int index)
            {
                if (this.IsEmpty || (index != 0))
                {
                    throw new InvalidOperationException();
                }
                if (!this.hasEmptyValue)
                {
                    throw new NotSupportedException();
                }
                this.setValue(this.emptyValue);
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            T IList<T>.this[int index]
            {
                get => 
                    this.getValue();
                set => 
                    this.setValue(value);
            }

            private bool IsEmpty =>
                this.hasEmptyValue && Equals(this.getValue(), this.emptyValue);

            int ICollection<T>.Count =>
                this.IsEmpty ? 0 : 1;

            bool ICollection<T>.IsReadOnly =>
                false;

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__20 : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public ListAdapter<T>.SingletonList <>4__this;

                [DebuggerHidden]
                public <GetEnumerator>d__20(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num != 0)
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                    }
                    else
                    {
                        this.<>1__state = -1;
                        if (!this.<>4__this.IsEmpty)
                        {
                            this.<>2__current = this.<>4__this.getValue();
                            this.<>1__state = 1;
                            return true;
                        }
                    }
                    return false;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }
    }
}

