namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class LohPooled
    {
        private static LohPooled.FixedSizeList<T> _CreateList<T>(IEnumerable<T> source, int? hintCount, bool longLiving);
        private static LohPooled.FixedSizeList<T> _CreateList<T>(IEnumerator<T> source, int? hintSize, bool longLiving);
        private static LohPooled.FixedSizeList<T> _CreateListFromChunkedIfLarge<T>(LohPooled.ChunkedList<T> innerList, bool longLiving);
        public static LohPooled.FixedSizeList<T> CreateFixedSizeList<T>(int sizeOfNewEmptyFixedSizeList);
        public static int GetCreateFixedSizeListTerminalSizeBetweenOrdinaryAndChunkedImpl<T>();
        public static LohPooled.EnumeratorWithCount<T> ToListAndDisposeToEnumeratorWithCount<T>(LohPooled.EnumeratorWithHintCount<T> source);
        public static LohPooled.EnumeratorWithCount<T> ToListAndDisposeToEnumeratorWithCount<T>(IEnumerable<T> source);
        public static LohPooled.EnumeratorWithCount<T> ToListAndDisposeToEnumeratorWithCount<T>(IEnumerator<T> source, int? hintSize = new int?());
        public static LohPooled.OnceEnumerableCollection<T> ToListAndDisposeToOnceEnumerableCollection<T>(LohPooled.EnumeratorWithHintCount<T> source);
        public static ICollection<T> ToListAndDisposeToOnceEnumerableCollection<T>(IEnumerable<T> source);
        public static LohPooled.OnceEnumerableCollection<T> ToListAndDisposeToOnceEnumerableCollection<T>(IEnumerator<T> source, int? hintSize = new int?());
        public static LohPooled.FixedSizeList<T> ToListForDispose<T>(LohPooled.EnumeratorWithHintCount<T> source);
        public static LohPooled.FixedSizeList<T> ToListForDispose<T>(IEnumerable<T> source, int? hintCount = new int?());
        public static LohPooled.FixedSizeList<T> ToListForDispose<T>(IEnumerator<T> source, int? hintSize = new int?());
        public static LohPooled.FixedSizeList<T> ToListForMultipleReads<T>(LohPooled.EnumeratorWithHintCount<T> source);
        public static LohPooled.FixedSizeList<T> ToListForMultipleReads<T>(IEnumerable<T> source, int? hintSize = new int?());
        public static LohPooled.FixedSizeList<T> ToListForMultipleReads<T>(IEnumerator<T> source, int? hintSize = new int?());

        public static class ArrayPool
        {
            public static T[] Empty<T>();
            public static T[] Get<T>(int minAcceptableSize);
            public static T[] Get<T>(int minAcceptableSize, int maxAcceptableSize, int createNewSize);
            public static T[] GetClear<T>(int minAcceptableSize);
            public static T[] GetClear<T>(int minAcceptableSize, int maxAcceptableSize, int createNewSize);
            public static int LohSize<T>();
            public static void Put<T>(IEnumerable<T[]> items);
            public static void Put<T>(T[] item);
            public static bool TryGet<T>(out T[] item, int minAcceptableSize, int maxAcceptableSize);

            private sealed class Core<T> : LohPooled.PoolForSizedItems<T[]>
            {
                public static readonly T[] Empty;
                public static readonly int LohSize;
                public static readonly LohPooled.ArrayPool.Core<T> Instance;
                private readonly int _LohSize;
                private int _SizeToPoolMin;

                static Core();
                public Core();
                protected override void BeforeReallyPut(T[] item);
                private static int CalcLohSize();
                protected override LohPooled.PoolForSizedItems<T[]>.DecideListResult DecideList(int size);
                protected override int GetCount(T[] item);
            }
        }

        public static class BitArraySimple
        {
            public const int BitsPerInt = 0x20;

            public static int CalcIntsToHoldBitsCount(int count);
            public static bool GetBit(int[] holder, int index);
            public static void ResetBit(int[] holder, int index);
            public static void SetBit(int[] holder, int index);
            public static void SetBit(int[] holder, int index, bool value);
        }

        private sealed class ChunkedList<T> : LohPooled.FinalizeSuppressableBase, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
        {
            private List<T[]> pages;
            private int version;
            private int size;
            public static readonly int PageSize;
            private int PageSizeToAvoidLdsfldOfValueClassJitObservationAndAllowInlining;
            public static readonly int MaxAcceptablePageSize;

            static ChunkedList();
            public ChunkedList();
            public ChunkedList(int initialEmptySize);
            public void Add(T item);
            private int AddCore(T item);
            public void AddRange(IEnumerable<T> items);
            public void AddRange(IEnumerator<T> en);
            private int calcOffsetFromIndex(int index);
            private int calcPageFromIndex(int index);
            private static int CalcPageSize();
            private void Clear();
            private bool Contains(T item);
            public void CopyTo(T[] array, int arrayIndex);
            public void CopyTo(Array array, int index);
            private T[] CreateClearPage();
            private T[] CreateDirtyPage();
            protected override void Dispose(bool disposing);
            public IEnumerator<T> DisposeToEnumerator();
            public LohPooled.EnumeratorWithCount<T> DisposeToEnumeratorWithCount();
            public IEnumerable<T> DisposeToOnceEnumerableWithCount();
            private T[] EnsurePageAvailable(int page);
            [IteratorStateMachine(typeof(LohPooled.ChunkedList<>.<GetEnumerator>d__42<>))]
            public IEnumerator<T> GetEnumerator();
            private T Indexer(int index);
            private int IndexOf(T item);
            private bool Remove(T item);
            private void Setter(int index, T value);
            void ICollection<T>.Clear();
            bool ICollection<T>.Contains(T item);
            bool ICollection<T>.Remove(T item);
            int IList<T>.IndexOf(T item);
            void IList<T>.Insert(int index, T item);
            void IList<T>.RemoveAt(int index);
            IEnumerator IEnumerable.GetEnumerator();
            int IList.Add(object value);
            void IList.Clear();
            bool IList.Contains(object value);
            int IList.IndexOf(object value);
            void IList.Insert(int index, object value);
            void IList.Remove(object value);
            void IList.RemoveAt(int index);

            public int Count { get; }

            public bool IsReadOnly { get; }

            public T this[int index] { get; set; }

            object IList.this[int index] { get; set; }

            bool ICollection.IsSynchronized { get; }

            object ICollection.SyncRoot { get; }

            public bool IsFixedSize { get; }

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__42 : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public LohPooled.ChunkedList<T> <>4__this;
                private T[] <page>5__1;
                private int <rememberedVersion>5__2;
                private int <toGo>5__3;
                private int <offset>5__4;
                private int <pageIndex>5__5;

                [DebuggerHidden]
                public <GetEnumerator>d__42(int <>1__state);
                private bool MoveNext();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                T IEnumerator<T>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }
        }

        private static class CreateListMemo<T>
        {
            public static readonly int TerminalSize;

            static CreateListMemo();
            private static int CalcTerminalSize();
        }

        private class DisposingEnumeratorPages<T> : IDisposable, IEnumerator<T>, IEnumerator
        {
            private List<T[]> Pages;
            private readonly int PageSize;
            private int toGo;
            private T current;
            private int pageIndex;
            private int offset;
            private T[] currentPage;

            public DisposingEnumeratorPages(List<T[]> pages, int pageSize, int length);
            public void Dispose();
            protected virtual void Dispose(bool disposing);
            protected override void Finalize();
            public bool MoveNext();
            public void Reset();

            public T Current { get; }

            object IEnumerator.Current { get; }
        }

        private class DisposingEnumeratorSinglePage<T> : IDisposable, IEnumerator<T>, IEnumerator
        {
            private T[] Page;
            private int length;
            private int currentIndex;
            private T current;

            public DisposingEnumeratorSinglePage(T[] _page, int _length);
            public void Dispose();
            protected virtual void Dispose(bool disposing);
            protected override void Finalize();
            public bool MoveNext();
            public void Reset();

            public T Current { get; }

            object IEnumerator.Current { get; }
        }

        private class EmptyEnumeratorEnumerableCollection<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable, ICollection<T>, ICollection
        {
            public static readonly LohPooled.EmptyEnumeratorEnumerableCollection<T> Instance;

            static EmptyEnumeratorEnumerableCollection();
            public void Add(T item);
            public void Clear();
            public bool Contains(T item);
            public void CopyTo(T[] array, int arrayIndex);
            public void CopyTo(Array array, int index);
            public void Dispose();
            public IEnumerator<T> GetEnumerator();
            public bool MoveNext();
            public bool Remove(T item);
            public void Reset();
            IEnumerator IEnumerable.GetEnumerator();

            public T Current { get; }

            object IEnumerator.Current { get; }

            public int Count { get; }

            public bool IsReadOnly { get; }

            public bool IsSynchronized { get; }

            public object SyncRoot { get; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EnumeratorWithCount<T>
        {
            public IEnumerator<T> Enumerator;
            public int Count;
            public EnumeratorWithCount(IEnumerator<T> enumerator, int count);
            public static implicit operator LohPooled.EnumeratorWithHintCount<T>(LohPooled.EnumeratorWithCount<T> me);
            public LohPooled.OnceEnumerableCollection<T> ToOnceEnumerableCollection();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EnumeratorWithHintCount<T>
        {
            public IEnumerator<T> Enumerator;
            public int? HintCount;
            public EnumeratorWithHintCount(IEnumerator<T> enumerator, int? hintCount);
        }

        public abstract class FinalizeSuppressableBase : IDisposable
        {
            public static bool WeakReferencesNotAvailable;
            private object holder;

            protected FinalizeSuppressableBase();
            [MethodImpl(MethodImplOptions.NoInlining)]
            private static void _HandleCore(LohPooled.FinalizeSuppressableBase reference);
            public void Dispose();
            protected abstract void Dispose(bool disposing);
            private void DoOptOutFromFinalize();
            protected override void Finalize();
            private static void Handle(LohPooled.FinalizeSuppressableBase reference);
            private void StoreThatWithinObjectItself(object somethingToHold);

            private class FinalizeSuppressorForTooOldForThis
            {
                private readonly WeakReference SubjectReference;
                private object generationPromoterHack;

                public FinalizeSuppressorForTooOldForThis(WeakReference subjectReference);
                protected override void Finalize();
            }
        }

        public abstract class FixedSizeList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IDisposable
        {
            private readonly bool isReadOnly;

            protected FixedSizeList(bool isReadOnly);
            private void Add(T item);
            private void Clear();
            private bool Contains(T item);
            public abstract void CopyTo(T[] array, int arrayIndex);
            public abstract void CopyTo(Array array, int index);
            public abstract void Dispose();
            public abstract IEnumerator<T> DisposeToEnumerator();
            public LohPooled.EnumeratorWithCount<T> DisposeToEnumeratorWithCount();
            public LohPooled.OnceEnumerableCollection<T> DisposeToOnceEnumerableCollection();
            public abstract IEnumerator<T> GetEnumerator();
            private int IndexOf(T item);
            private bool Remove(T item);
            void ICollection<T>.Add(T item);
            void ICollection<T>.Clear();
            bool ICollection<T>.Contains(T item);
            bool ICollection<T>.Remove(T value);
            int IList<T>.IndexOf(T item);
            void IList<T>.Insert(int index, T item);
            void IList<T>.RemoveAt(int index);
            IEnumerator IEnumerable.GetEnumerator();
            int IList.Add(object value);
            void IList.Clear();
            bool IList.Contains(object value);
            int IList.IndexOf(object value);
            void IList.Insert(int index, object value);
            void IList.Remove(object value);
            void IList.RemoveAt(int index);

            public abstract T this[int index] { get; set; }

            public abstract int Count { get; }

            public bool IsReadOnly { get; }

            public bool IsFixedSize { get; }

            object IList.this[int index] { get; set; }

            public bool IsSynchronized { get; }

            object ICollection.SyncRoot { get; }
        }

        private sealed class FixedSizeListChunkedImpl<T> : LohPooled.FixedSizeList<T>
        {
            private readonly LohPooled.ChunkedList<T> Nested;

            public FixedSizeListChunkedImpl(LohPooled.ChunkedList<T> nested, bool isReadOnly);
            public override void CopyTo(T[] array, int arrayIndex);
            public override void CopyTo(Array array, int index);
            public override void Dispose();
            public override IEnumerator<T> DisposeToEnumerator();
            public override IEnumerator<T> GetEnumerator();

            public override T this[int index] { get; set; }

            public override int Count { get; }
        }

        private sealed class FixedSizeListEmptyImpl<T> : LohPooled.FixedSizeList<T>
        {
            public FixedSizeListEmptyImpl();
            public override void CopyTo(T[] array, int arrayIndex);
            public override void CopyTo(Array array, int index);
            public override void Dispose();
            public override IEnumerator<T> DisposeToEnumerator();
            public override IEnumerator<T> GetEnumerator();

            public override T this[int index] { get; set; }

            public override int Count { get; }
        }

        private sealed class FixedSizeListOrdinaryImpl<T> : LohPooled.FixedSizeList<T>
        {
            private readonly LohPooled.OrdinaryList<T> Nested;

            public FixedSizeListOrdinaryImpl(LohPooled.OrdinaryList<T> nested, bool isReadOnly);
            public override void CopyTo(T[] array, int arrayIndex);
            public override void CopyTo(Array array, int index);
            public override void Dispose();
            public override IEnumerator<T> DisposeToEnumerator();
            public override IEnumerator<T> GetEnumerator();

            public override T this[int index] { get; set; }

            public override int Count { get; }
        }

        public class OnceEnumerableCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
        {
            private readonly IEnumerator<T> _Enumerator;
            private readonly int _Count;
            private bool enumerated;

            public OnceEnumerableCollection(LohPooled.EnumeratorWithCount<T> enumeratorWithCount);
            public OnceEnumerableCollection(IEnumerator<T> enumerator, int count);
            public IEnumerator<T> GetEnumerator();
            void ICollection<T>.Add(T item);
            void ICollection<T>.Clear();
            bool ICollection<T>.Contains(T item);
            void ICollection<T>.CopyTo(T[] array, int arrayIndex);
            bool ICollection<T>.Remove(T item);
            void ICollection.CopyTo(Array array, int index);
            IEnumerator IEnumerable.GetEnumerator();

            public int Count { get; }

            bool ICollection<T>.IsReadOnly { get; }

            bool ICollection.IsSynchronized { get; }

            object ICollection.SyncRoot { get; }
        }

        public class OrdinaryDictionary<TKey, TValue> : LohPooled.FinalizeSuppressableBase
        {
            private LohPooled.OrdinaryDictionary<TKey, TValue>.EntryRef[] Buckets;
            private List<LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[]> Entries;
            private readonly int EntriesPageSize;
            private int EntriesCount;
            private int FreeEntriesCount;
            private int version;
            private LohPooled.OrdinaryDictionary<TKey, TValue>.EntryRef FreeList;
            private LohPooled.OrdinaryDictionary<TKey, TValue>.EntryRef NextEntry;
            private IEqualityComparer<TKey> Comparer;
            private int CurrentPrimeModulo;
            private const int DeletedHash = -2147483648;

            public OrdinaryDictionary();
            public OrdinaryDictionary(int capacity);
            public OrdinaryDictionary(int capacity, IEqualityComparer<TKey> comparer);
            public void Add(TKey key, TValue value);
            public void Clear();
            public bool ContainsKey(TKey key);
            protected override void Dispose(bool disposing);
            private void Insert(TKey key, TValue value, bool add);
            private static bool IsDeletedHash(int storedHash);
            private bool NeedResizeBuckets();
            public bool Remove(TKey key);
            private void ResizeBuckets();
            private void ResizeBuckets(int newModulo);
            private static int ToStoredHash(int realHash);
            public bool TryGetValue(TKey key, out TValue value);

            protected int ProtectedCurrentPrimeModulo { get; }

            public TValue this[TKey key] { get; set; }

            public int Count { get; }

            public IEnumerable<TKey> Keys { [IteratorStateMachine(typeof(LohPooled.OrdinaryDictionary<, >.<get_Keys>d__36<,>))] get; }

            public IEnumerable<TValue> Values { [IteratorStateMachine(typeof(LohPooled.OrdinaryDictionary<, >.<get_Values>d__38<,>))] get; }

            public IEnumerable<KeyValuePair<TKey, TValue>> Pairs { [IteratorStateMachine(typeof(LohPooled.OrdinaryDictionary<, >.<get_Pairs>d__40<,>))] get; }

            [CompilerGenerated]
            private sealed class <get_Keys>d__36 : IEnumerable<TKey>, IEnumerable, IEnumerator<TKey>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private TKey <>2__current;
                private int <>l__initialThreadId;
                public LohPooled.OrdinaryDictionary<TKey, TValue> <>4__this;
                private int <cnt>5__1;
                private LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[] <page>5__2;
                private int <closuredVersion>5__3;
                private int <offset>5__4;
                private List<LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[]>.Enumerator <>7__wrap1;

                [DebuggerHidden]
                public <get_Keys>d__36(int <>1__state);
                private void <>m__Finally1();
                private bool MoveNext();
                [DebuggerHidden]
                IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator();
                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                TKey IEnumerator<TKey>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }

            [CompilerGenerated]
            private sealed class <get_Pairs>d__40 : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private KeyValuePair<TKey, TValue> <>2__current;
                private int <>l__initialThreadId;
                public LohPooled.OrdinaryDictionary<TKey, TValue> <>4__this;
                private int <cnt>5__1;
                private LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[] <page>5__2;
                private int <closuredVersion>5__3;
                private int <offset>5__4;
                private List<LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[]>.Enumerator <>7__wrap1;

                [DebuggerHidden]
                public <get_Pairs>d__40(int <>1__state);
                private void <>m__Finally1();
                private bool MoveNext();
                [DebuggerHidden]
                IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator();
                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }

            [CompilerGenerated]
            private sealed class <get_Values>d__38 : IEnumerable<TValue>, IEnumerable, IEnumerator<TValue>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private TValue <>2__current;
                private int <>l__initialThreadId;
                public LohPooled.OrdinaryDictionary<TKey, TValue> <>4__this;
                private int <cnt>5__1;
                private LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[] <page>5__2;
                private int <closuredVersion>5__3;
                private int <offset>5__4;
                private List<LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[]>.Enumerator <>7__wrap1;

                [DebuggerHidden]
                public <get_Values>d__38(int <>1__state);
                private void <>m__Finally1();
                private bool MoveNext();
                [DebuggerHidden]
                IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator();
                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                TValue IEnumerator<TValue>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct Entry
            {
                public int HashCode;
                public LohPooled.OrdinaryDictionary<TKey, TValue>.EntryRef Next;
                public TKey Key;
                public TValue Value;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct EntryRef
            {
                public LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[] Page;
                public int Offset;
                public EntryRef(LohPooled.OrdinaryDictionary<TKey, TValue>.Entry[] _Page, int _Offset);
                public bool IsEmpty { get; }
                public static LohPooled.OrdinaryDictionary<TKey, TValue>.EntryRef Empty { get; }
            }
        }

        public sealed class OrdinaryList<T> : LohPooled.FinalizeSuppressableBase, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
        {
            private int size;
            private T[] items;
            private int version;

            public OrdinaryList();
            public OrdinaryList(IEnumerable<T> collection);
            public OrdinaryList(int capacity);
            public OrdinaryList(int initialSize, bool canBeFilledWithTrash);
            public OrdinaryList(int? hintCapacity, IEnumerable<T> collection);
            public OrdinaryList(int? hintCapacity, IEnumerator<T> collection);
            public void Add(T item);
            public void AddRange(IEnumerable<T> collection);
            public void AddRange(IEnumerator<T> en, int? hintCount = new int?());
            public ReadOnlyCollection<T> AsReadOnly();
            private static int CalcBestNextCapacity(int required, int current);
            public void Clear();
            public bool Contains(T item);
            public void CopyTo(T[] array, int arrayIndex);
            public void CopyTo(Array array, int index);
            protected override void Dispose(bool disposing);
            public IEnumerator<T> DisposeToEnumerator();
            public LohPooled.EnumeratorWithCount<T> DisposeToEnumeratorWithCount();
            public ICollection<T> DisposeToOnceEnumerableWithCount();
            private void EnsureCapacity(int required);
            public bool FillToCapacity(IEnumerator<T> en);
            public LohPooled.OrdinaryList<T>.Enumerator GetEnumerator();
            public int IndexOf(T item);
            public void Insert(int index, T item);
            public void InsertRange(int index, LohPooled.OrdinaryList<T> list);
            public void InsertRange(int index, IEnumerable<T> collection);
            public void InsertRange(int index, List<T> list, int startIndex, int count);
            public void InsertRange(int index, T[] array, int startIndex, int count);
            private void InsertRangeCollection(int index, ICollection<T> collection);
            public bool Remove(T item);
            public void RemoveAt(int index);
            IEnumerator<T> IEnumerable<T>.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator();
            int IList.Add(object value);
            bool IList.Contains(object value);
            int IList.IndexOf(object value);
            void IList.Insert(int index, object value);
            void IList.Remove(object value);

            public int Capacity { get; set; }

            public T this[int index] { get; set; }

            public int Count { get; }

            bool IList.IsReadOnly { get; }

            bool ICollection<T>.IsReadOnly { get; }

            bool IList.IsFixedSize { get; }

            object IList.this[int index] { get; set; }

            bool ICollection.IsSynchronized { get; }

            object ICollection.SyncRoot { get; }

            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
            {
                private LohPooled.OrdinaryList<T> list;
                private int index;
                private int version;
                private T current;
                public T Current { get; }
                object IEnumerator.Current { get; }
                internal Enumerator(LohPooled.OrdinaryList<T> list_);
                public void Dispose();
                public bool MoveNext();
                public void Reset();
            }
        }

        public abstract class PoolBase
        {
            public static bool SuppressPool;
            private int agingTimerStarted;

            protected PoolBase();
            protected abstract void AgingCore();
            protected abstract bool CanStopAging();
            [MethodImpl(MethodImplOptions.NoInlining)]
            private void DoAging();
            public static void FlushPools();
            private static void FlushPoolsCore();
            protected void StartAgingTimerIfNeeded();
        }

        public abstract class PoolForSizedItems<T> : LohPooled.PoolBase where T: class
        {
            private int lck;
            private WeakReference LohList;
            private List<object> _LohListNext;
            private List<object> SmallHotList;
            private List<object> SmallColdList;

            public PoolForSizedItems();
            protected override void AgingCore();
            protected abstract void BeforeReallyPut(T item);
            protected override bool CanStopAging();
            protected abstract LohPooled.PoolForSizedItems<T>.DecideListResult DecideList(int size);
            protected abstract int GetCount(T item);
            private int GetCountOfObjectOrStack(object obj);
            protected void Lock();
            public void Put(T item);
            private void PutCore(List<object> listToPut, T item, int sz);
            public bool TryGet(out T item, int minAcceptabgleSize, int maxAcceptableSize);
            private bool TryGetCore(List<object> list, out T item, int minAcceptableSize, int maxAcceptableSize);
            protected void Unlock();

            protected enum DecideListResult
            {
                public const LohPooled.PoolForSizedItems<T>.DecideListResult ThrowAway = LohPooled.PoolForSizedItems<T>.DecideListResult.ThrowAway;,
                public const LohPooled.PoolForSizedItems<T>.DecideListResult SmallHeap = LohPooled.PoolForSizedItems<T>.DecideListResult.SmallHeap;,
                public const LohPooled.PoolForSizedItems<T>.DecideListResult LargeHeap = LohPooled.PoolForSizedItems<T>.DecideListResult.LargeHeap;
            }

            private sealed class SizedStack : Stack<T>
            {
                public readonly int SizeOfItems;

                public SizedStack(int sizeOfItems);
            }
        }

        public class PoolForUndistinguishableItems<T> : LohPooled.PoolBase where T: class
        {
            private readonly Action<T> OnGet;
            private readonly Func<T, bool> OnPut;
            private readonly Func<T> CreateNew;
            private LohPooled.PoolForUndistinguishableItems<T>.State CurrentState;

            public PoolForUndistinguishableItems(Func<T, bool> onPutCanPut, Action<T> onGet, Func<T> createNew);
            protected override void AgingCore();
            protected override bool CanStopAging();
            public T Get();
            public void Put(T item);
            public bool TryGet(out T item);

            private class State
            {
                public readonly ConcurrentBag<T> HotItems;
                public readonly ConcurrentBag<T> ColdItems;

                public State();
                public State(ConcurrentBag<T> coldItems);
            }
        }

        public static class QuadArraySimple
        {
            public const int QuadsPerInt = 0x10;

            public static int CalcIntsToHoldQuadsCount(int count);
            public static int GetQuad(int[] holder, int index);
            public static void SetQuadBits(int[] holder, int index, int quadBits);
        }

        public static class SortUtils
        {
            public static int SortSeveralThreadsSwitch;

            static SortUtils();
            [IteratorStateMachine(typeof(LohPooled.SortUtils.<DistinctOrdered>d__2<><>))]
            private static IEnumerator<T> DistinctOrdered<T>(IEnumerator<T> sortedEnumerable, Func<T, T, bool> areEq);
            [IteratorStateMachine(typeof(LohPooled.SortUtils.<MergeOrdered>d__1<><>))]
            private static IEnumerator<T> MergeOrdered<T>(IEnumerator<T> leftEnumerator, IEnumerator<T> rightEnumerator, Comparison<T> comparison, bool keepDistinctDistinct);
            public static LohPooled.OnceEnumerableCollection<T> SortSameThread<T>(Comparison<T> comparison, IEnumerable<T> items, bool distinct = false);
            public static LohPooled.OnceEnumerableCollection<T> SortSeveralThreads<T>(Comparison<T> comparer, int itemsCount, IEnumerable<T> items, Action<IEnumerable<T>> prepareCaches, bool distinct = false);
            [IteratorStateMachine(typeof(LohPooled.SortUtils.<SplitEnumerablesToArrays>d__3<><>))]
            private static IEnumerable<Tuple<T[], int>> SplitEnumerablesToArrays<T>(IEnumerable<T> items, int? maxPerPage = new int?());

            [CompilerGenerated]
            private sealed class <DistinctOrdered>d__2<T> : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public IEnumerator<T> sortedEnumerable;
                public Func<T, T, bool> areEq;
                private T <previous>5__1;
                private T <next>5__2;

                [DebuggerHidden]
                public <DistinctOrdered>d__2(int <>1__state);
                private bool MoveNext();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                T IEnumerator<T>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }

            [CompilerGenerated]
            private sealed class <MergeOrdered>d__1<T> : IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                public IEnumerator<T> leftEnumerator;
                public IEnumerator<T> rightEnumerator;
                private bool <leftNotEmpty>5__1;
                private bool <rightNotEmpty>5__2;
                public Comparison<T> comparison;
                private int <cmp>5__3;
                private T <rec>5__4;
                public bool keepDistinctDistinct;
                private T <lec>5__5;

                [DebuggerHidden]
                public <MergeOrdered>d__1(int <>1__state);
                private bool MoveNext();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                T IEnumerator<T>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }

            [CompilerGenerated]
            private sealed class <SplitEnumerablesToArrays>d__3<T> : IEnumerable<Tuple<T[], int>>, IEnumerable, IEnumerator<Tuple<T[], int>>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private Tuple<T[], int> <>2__current;
                private int <>l__initialThreadId;
                private int? maxPerPage;
                public int? <>3__maxPerPage;
                private IEnumerable<T> items;
                public IEnumerable<T> <>3__items;
                private int <pageSize>5__1;
                private int <maxAcceptablePageSize>5__2;
                private IEnumerator<T> <>7__wrap1;

                [DebuggerHidden]
                public <SplitEnumerablesToArrays>d__3(int <>1__state);
                private void <>m__Finally1();
                private bool MoveNext();
                [DebuggerHidden]
                IEnumerator<Tuple<T[], int>> IEnumerable<Tuple<T[], int>>.GetEnumerator();
                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                Tuple<T[], int> IEnumerator<Tuple<T[], int>>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }

            public class ComparisonComparer<V> : IComparer<V>
            {
                private readonly Comparison<V> Comparison;

                public ComparisonComparer(Comparison<V> comparison);
                public int Compare(V x, V y);
            }

            private class SortTaskData2<T>
            {
                private static readonly int PageSize;
                private static readonly int MaxAcceptablePageSize;
                private static readonly int ExpeditedFlushCheckModulo;
                private Exception CancellationToken;
                private readonly ConcurrentQueue<LohPooled.SortUtils.SortTaskData2<T>.Task> TasksWithZeroPriorityToRunForSure;
                private readonly ConcurrentQueue<LohPooled.SortUtils.SortTaskData2<T>.Task> TasksNewlyAdded;
                private int monitor;
                private readonly List<LohPooled.SortUtils.SortTaskData2<T>.Task> TasksPending;
                private bool WorkersCanQuitOnNoJob;
                private int bkgndThreadsToRun;

                static SortTaskData2();
                public SortTaskData2();
                private void AddTask(LohPooled.SortUtils.SortTaskData2<T>.Task task);
                private static int? CalcSplitPageSize(int expectedItemsCountHint);
                public static LohPooled.OnceEnumerableCollection<T> Do(int itemsCount, IEnumerable<T> items, Action<IEnumerable<T>> prepareCaches, Comparison<T> comparison, bool isDistinct);
                private LohPooled.SortUtils.SortTaskData2<T>.Task GetNextTask();
                private void Lock();
                private LohPooled.SortUtils.SortTaskData2<T>.Buffer MainThread(int expectedItemsCountHint, IEnumerable<T> items, Action<IEnumerable<T>> prepareCaches, Comparison<T> comparison, bool isDistinct);
                private LohPooled.SortUtils.SortTaskData2<T>.Buffer MainThreadFetchDataToWorkersAndObtainResultBuffer(int expectedItemsCountHint, IEnumerable<T> items, Action<IEnumerable<T>> prepareCaches, Comparison<T> comparison, bool isDistinct);
                private void StartWorkerIfNeeded();
                private bool Step();
                private bool TryLock();
                private void UnLock();
                private void WorkerThread();

                public class Buffer
                {
                    private volatile bool _Complete;
                    public readonly ConcurrentQueue<LohPooled.SortUtils.SortTaskData2<T>.Chunk> Flow;

                    public Buffer();
                    public void Complete();
                    [IteratorStateMachine(typeof(LohPooled.SortUtils.SortTaskData2<>.Buffer.<DisposeEnumerate>d__9<>))]
                    public IEnumerator<T> DisposeEnumerate();

                    public bool IsNothingToProcessAndNeverWouldBe { get; }

                    public bool IsPending { get; }

                    public bool IsComplete { get; }

                    [CompilerGenerated]
                    private sealed class <DisposeEnumerate>d__9 : IEnumerator<T>, IDisposable, IEnumerator
                    {
                        private int <>1__state;
                        private T <>2__current;
                        public LohPooled.SortUtils.SortTaskData2<T>.Buffer <>4__this;
                        private LohPooled.SortUtils.SortTaskData2<T>.Chunk <ch>5__1;
                        private T[] <arr>5__2;
                        private int <i>5__3;
                        private int <cnt>5__4;

                        [DebuggerHidden]
                        public <DisposeEnumerate>d__9(int <>1__state);
                        private bool MoveNext();
                        [DebuggerHidden]
                        void IEnumerator.Reset();
                        [DebuggerHidden]
                        void IDisposable.Dispose();

                        T IEnumerator<T>.Current { [DebuggerHidden] get; }

                        object IEnumerator.Current { [DebuggerHidden] get; }
                    }
                }

                public sealed class Chunk
                {
                    public readonly T[] Data;
                    public readonly int Count;

                    public Chunk(T[] _Data, int _Count);
                }

                public class LeafTask : LohPooled.SortUtils.SortTaskData2<T>.Task
                {
                    private readonly Func<LohPooled.SortUtils.SortTaskData2<T>.Chunk> Action;
                    private readonly LohPooled.SortUtils.SortTaskData2<T>.Buffer Output;

                    public LeafTask(LohPooled.SortUtils.SortTaskData2<T>.Buffer _Output, Func<LohPooled.SortUtils.SortTaskData2<T>.Chunk> _Action);
                    public override int? GetCurrentPriority();
                    public override LohPooled.SortUtils.SortTaskData2<T>.Task Process();
                }

                public class MergeTask : LohPooled.SortUtils.SortTaskData2<T>.Task
                {
                    private readonly LohPooled.SortUtils.SortTaskData2<T>.MergeTask.Enumer Left;
                    private readonly LohPooled.SortUtils.SortTaskData2<T>.MergeTask.Enumer Right;
                    private readonly LohPooled.SortUtils.SortTaskData2<T>.Buffer Output;
                    private readonly bool KeepDistinctDistinct;
                    private readonly Comparison<T> Comparison;
                    private int incrementedByFlush;
                    private T[] OutputData;
                    private int OutputCount;
                    public bool IsFinalMerger;

                    public MergeTask(LohPooled.SortUtils.SortTaskData2<T>.Buffer _left, LohPooled.SortUtils.SortTaskData2<T>.Buffer _right, LohPooled.SortUtils.SortTaskData2<T>.Buffer _output, Comparison<T> _Comparison, bool _KeepDistinctDistinct);
                    private void Flush();
                    public override int? GetCurrentPriority();
                    private bool NeedFlush();
                    public override LohPooled.SortUtils.SortTaskData2<T>.Task Process();
                    private void Push(T value);
                    private void TryBigLeap(LohPooled.SortUtils.SortTaskData2<T>.MergeTask.Enumer jumper, LohPooled.SortUtils.SortTaskData2<T>.MergeTask.Enumer hurdle);
                    private void TryBigLeapLeft();
                    private void TryBigLeapRight();

                    private class Enumer
                    {
                        public readonly LohPooled.SortUtils.SortTaskData2<T>.Buffer Input;
                        public LohPooled.SortUtils.SortTaskData2<T>.MergeTask.State State;
                        public T[] Data;
                        public int DataCount;
                        public int CurrentIndex;
                        public T CurrentValue;

                        public Enumer(LohPooled.SortUtils.SortTaskData2<T>.Buffer _input);
                        public bool Move();
                        private void MoveCore();
                        public LohPooled.SortUtils.SortTaskData2<T>.Task Suck(LohPooled.SortUtils.SortTaskData2<T>.MergeTask parent);

                        public bool IsTotallyPending { get; }
                    }

                    public enum State
                    {
                        public const LohPooled.SortUtils.SortTaskData2<T>.MergeTask.State Pending = LohPooled.SortUtils.SortTaskData2<T>.MergeTask.State.Pending;,
                        public const LohPooled.SortUtils.SortTaskData2<T>.MergeTask.State EnumerationComplete = LohPooled.SortUtils.SortTaskData2<T>.MergeTask.State.EnumerationComplete;,
                        public const LohPooled.SortUtils.SortTaskData2<T>.MergeTask.State Moved = LohPooled.SortUtils.SortTaskData2<T>.MergeTask.State.Moved;
                    }
                }

                public class PassThroughTask : LohPooled.SortUtils.SortTaskData2<T>.Task
                {
                    private LohPooled.SortUtils.SortTaskData2<T>.Buffer Input;
                    private LohPooled.SortUtils.SortTaskData2<T>.Buffer Output;

                    public PassThroughTask(LohPooled.SortUtils.SortTaskData2<T>.Buffer _Input, LohPooled.SortUtils.SortTaskData2<T>.Buffer _Output);
                    public override int? GetCurrentPriority();
                    public override LohPooled.SortUtils.SortTaskData2<T>.Task Process();
                }

                public abstract class Task
                {
                    protected Task();
                    public abstract int? GetCurrentPriority();
                    public abstract LohPooled.SortUtils.SortTaskData2<T>.Task Process();
                }
            }
        }

        public static class TypeUtils
        {
            private static ConcurrentDictionary<Type, Tuple<bool, int>> Cache;

            static TypeUtils();
            private static Tuple<bool, int> CalcRefsAndSize(Type t);
            public static bool ContainsReferences<T>();
            public static bool ContainsReferences(Type t);
            private static int? GetSizeForPredefinedTypes(Type t);
            private static Tuple<bool, int> GetTuple(Type t);
            public static int GuessArrayElementSize<T>();
            public static int GuessArrayElementSize(Type t);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly LohPooled.TypeUtils.<>c <>9;
                public static Func<FieldInfo, Type> <>9__2_0;

                static <>c();
                internal Type <CalcRefsAndSize>b__2_0(FieldInfo fi);
            }

            private static class Memo<T>
            {
                public static readonly bool IsContainsReferences;
                public static readonly int GuessedArrayElementSize;

                static Memo();
            }
        }
    }
}

