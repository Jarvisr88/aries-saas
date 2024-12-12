namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class VisibleListSourceRowCollection : IDisposable
    {
        protected DataControllerBase controller;
        private VisibleToSourceRowsMapper _Mapper;
        private string appliedFilterExpression;
        private bool hasUserFilter;
        public static int SeveralThreadsSortThreshold;
        public static int SeveralThreadsSortThresholdManyColumns;
        public static int? SeveralThreadsSortThreadsOverride;
        private int _ThreadSafetyEnforcerCounter;

        static VisibleListSourceRowCollection();
        public VisibleListSourceRowCollection(DataControllerBase controller);
        public void Assign(ICollection<int> records);
        public void Assign(IEnumerable<int> records, int count);
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void BC5101MapperCorruptionDetectionFailOnEnter();
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void BC5101MapperCorruptionDetectionFailOnLeave();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BC5101MapperCorruptionDetectionSectionEnter();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BC5101MapperCorruptionDetectionSectionLeave();
        private Comparison<int> BuildFindControllerRowForInsertExpressiveComparer(DataColumnSortInfo[] sortInfoCollection, int listSourceRow);
        private void BuildSortDelegatesAndChooseDegreeOfParallelism(DataColumnSortInfo[] sortInfo, Func<int, int> handlesToSourceIndexMapper, out Comparison<int> comparison, out Action<IEnumerable<int>> prepareCache, out Action disposeAfterUse, out bool parallelsortable);
        public void Clear();
        public void ClearAndForceNonIdentity();
        private void ClearMapper();
        private VisibleListSourceRowCollection CloneBase();
        public VisibleListSourceRowCollection ClonePersistent();
        public VisibleListSourceRowCollection CloneThatWouldBeForSureModifiedAndOrForgottenBeforeAnythingHappensToOriginal();
        public bool Contains(int listSourceRow);
        private void CreateHandlesToSourceIndicesMapAndMapper(out int[] handles2SourceIndicesMap, out Func<int, int> mapper);
        private VisibleToSourceRowsMapper CreateMapper();
        private VisibleToSourceRowsMapper CreateMapper(IEnumerable<int> state, int count);
        public void Dispose();
        public int FindControllerRowForInsert(DataColumnSortInfo[] sortInfoCollection, int listSourceRow, int? oldVisiblePosition = new int?());
        private int FindControllerRowForInsertExpressive(DataColumnSortInfo[] sortInfoCollection, int listSourceRow, int? oldVisiblePosition);
        private int FindControllerRowForInsertExpressiveCore(int listSourceRow, int? oldVisiblePosition, Comparison<int> comparison);
        public void ForceNonIdentity();
        public int GetListSourceRow(int visibleRow);
        private VisibleToSourceRowsMapper GetMapper();
        private VisibleToSourceRowsMapper GetMapperAfterNonIdentityCheck();
        private VisibleToSourceRowsMapper GetMapperForChange();
        protected VisibleToSourceRowsMapper GetMapperForSetRange();
        public int? GetVisibleRow(int listSourceRow);
        public int HideSourceRow(int listSourceRow);
        public void Init(int[] list, int? count = new int?(), string appliedFilterExpression = "", bool hasUserFilter = false);
        public void InsertHiddenRow(int listSourceRow);
        public void InsertVisibleRow(int sourceRowIndex, int visibleRowIndex);
        private static bool IsBigEnoughForSmart(int visibleCount);
        private static bool IsTooBigForDumb(int visibleCount);
        private static bool IsTooSmallForSmart(int visibleCount);
        public void MoveSourcePosition(int oldSourcePosition, int newSourcePosition);
        public void MoveVisiblePosition(int oldVisibleIndex, int newVisibleIndex);
        public int RemoveSourceRow(int listSourceRow);
        private VisibleToSourceRowsMapper ReplaceMapper(VisibleToSourceRowsMapper newMapper);
        public void SetRange(int startPos, int[] newValues);
        public void ShowRow(int sourceRowIndex, int visibleIndex);
        public void SortRows(DataColumnSortInfo[] sortInfo);
        public int[] ToArray();
        public IEnumerable<int> ToEnumerable();
        public int UnsafeGetListSourceRow(int visibleRow);
        public Func<int, int> UnsafeGetListSourceRowGetter();

        public string AppliedFilterExpression { get; }

        public bool HasUserFilter { get; }

        public static int SeveralThreadsSortThreads { get; }

        public bool IsIdentity { get; }

        public int VisibleRowCount { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisibleListSourceRowCollection.<>c <>9;
            public static Func<int, int> <>9__33_0;

            static <>c();
            internal int <UnsafeGetListSourceRowGetter>b__33_0(int vr);
        }

        public abstract class SpecificRowCacheBuilder : GenericInvoker<Func<Delegate, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>>, VisibleListSourceRowCollection.SpecificRowCacheBuilder.Impl<object>>
        {
            protected SpecificRowCacheBuilder();

            public class Impl<T> : VisibleListSourceRowCollection.SpecificRowCacheBuilder
            {
                protected override Func<Delegate, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> CreateInvoker();
                private static Func<int, T> MakeOneRowCachedCore(Func<int, T> getter, int listSourceRowCached);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly VisibleListSourceRowCollection.SpecificRowCacheBuilder.Impl<T>.<>c <>9;
                    public static Func<Delegate, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> <>9__1_0;

                    static <>c();
                    internal Tuple<Delegate, Action<IEnumerable<int>>, Action> <CreateInvoker>b__1_0(Delegate simpleGetter, int sourceRowIndexForCache);
                }
            }
        }

        public abstract class ValuesGetterCacheBuilder : GenericInvoker<Func<int, Func<int, int>, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>>, VisibleListSourceRowCollection.ValuesGetterCacheBuilder.Impl<object>>
        {
            protected ValuesGetterCacheBuilder();

            public class Impl<T> : VisibleListSourceRowCollection.ValuesGetterCacheBuilder
            {
                private static Func<int, T> CreateFullyCachedAccessor(T[] cache);
                private static Action<IEnumerable<int>> CreateFullyCachedCacheLoader(Func<int, int> handlesToSourceRowsMapper, Func<int, T> getter, T[] cache);
                private static Action<IEnumerable<int>> CreateFullyCachedCacheLoaderIdentity(Func<int, T> getter, T[] cache);
                private static Action CreateFullyCachedDisposer(T[] cache);
                protected override Func<int, Func<int, int>, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> CreateInvoker();
                private static Func<int, T> CreateLazyCachedAccessor(Func<int, int> handlesToSourceRowsMapper, Func<int, T> getter, bool[] flags, T[] cache);
                private static Func<int, T> CreateLazyCachedAccessorIdentity(Func<int, T> getter, bool[] flags, T[] cache);
                private static Action CreateLazyCachedDisposer(bool[] flags, T[] cache);
                private static Tuple<Delegate, Action<IEnumerable<int>>, Action> Make(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T> getter, bool isThreadSafe, int pos);
                private static Func<int, T> Make(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T> getter, bool isThreadSafe, int pos, out Action<IEnumerable<int>> prepareCache, out Action disposeAfterUse);
                private static Func<int, T> MakeFullyCached(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T> getter, out Action<IEnumerable<int>> prepareCache, out Action disposeAfterUse);
                private static Func<int, T> MakeLazyCached(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T> getter, out Action<IEnumerable<int>> prepareCache, out Action disposeAfterUse);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly VisibleListSourceRowCollection.ValuesGetterCacheBuilder.Impl<T>.<>c <>9;
                    public static Func<int, Func<int, int>, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> <>9__11_0;

                    static <>c();
                    internal Tuple<Delegate, Action<IEnumerable<int>>, Action> <CreateInvoker>b__11_0(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Delegate getter, bool isThreadSafe, int pos);
                }
            }
        }

        public abstract class ValuesGetterCacheBuilder_Nullable : GenericInvoker<Func<int, Func<int, int>, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>>, VisibleListSourceRowCollection.ValuesGetterCacheBuilder_Nullable.Impl<int>>
        {
            protected ValuesGetterCacheBuilder_Nullable();

            public class Impl<T> : VisibleListSourceRowCollection.ValuesGetterCacheBuilder_Nullable where T: struct
            {
                private const byte NotCached = 0;
                private const byte NullCached = 1;
                private const byte ValueCached = 2;

                private static Func<int, T?> CreateFullyCachedAccessor(T[] cache, bool[] nullFlags);
                private static Action<IEnumerable<int>> CreateFullyCachedCacheLoader(Func<int, int> handlesToSourceRowsMapper, Func<int, T?> getter, T[] cache, bool[] nullFlags);
                private static Action<IEnumerable<int>> CreateFullyCachedCacheLoaderIdentity(Func<int, T?> getter, T[] cache, bool[] nullFlags);
                protected override Func<int, Func<int, int>, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> CreateInvoker();
                private static Func<int, T?> CreateLazyCachedAccessor(Func<int, int> handlesToSourceRowsMapper, Func<int, T?> getter, byte[] flags, T[] cache);
                private static Func<int, T?> CreateLazyCachedAccessorIdentity(Func<int, T?> getter, byte[] flags, T[] cache);
                private static Tuple<Delegate, Action<IEnumerable<int>>, Action> Make(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T?> getter, bool isThreadSafe, int pos);
                private static Func<int, T?> Make(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T?> getter, bool isThreadSafe, int pos, out Action<IEnumerable<int>> prepareCache, out Action disposeAfterUse);
                private static Func<int, T?> MakeFullyCached(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T?> getter, out Action<IEnumerable<int>> prepareCache, out Action disposeAfterUse);
                private static Func<int, T?> MakeLazyCached(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Func<int, T?> getter, out Action<IEnumerable<int>> prepareCache, out Action disposeAfterUse);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly VisibleListSourceRowCollection.ValuesGetterCacheBuilder_Nullable.Impl<T>.<>c <>9;
                    public static Func<int, Func<int, int>, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> <>9__12_0;

                    static <>c();
                    internal Tuple<Delegate, Action<IEnumerable<int>>, Action> <CreateInvoker>b__12_0(int rowsCount, Func<int, int> handlesToSourceRowsMapper, Delegate getter, bool isThreadSafe, int pos);
                }
            }
        }
    }
}

