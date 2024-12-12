namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class VisibleToSourceRowsSmartMapper : VisibleToSourceRowsMapper
    {
        private readonly VisibleToSourceRowsSmartMapper.Catalog VisibleCatalog;
        private readonly VisibleToSourceRowsSmartMapper.Catalog SourceCatalog;

        public VisibleToSourceRowsSmartMapper(VisibleToSourceRowsMapper parentMapper);
        private VisibleToSourceRowsSmartMapper(IEnumerable<int> visibleSourceIndices, int count);
        public override void Dispose();
        public override int GetListSourceIndex(int visibleIndex);
        public override int? GetVisibleIndex(int listSourceIndex);
        public override int? HideRow(int sourceIndex);
        private void InsertHiddenRow(int sourceIndex);
        public override void InsertRow(int sourceIndex, int? visibleIndex = new int?());
        public override void MoveSourcePosition(int oldSourcePosition, int newSourcePosition);
        public override void MoveVisiblePosition(int oldVisibleIndex, int newVisibleIndex);
        public override int? RemoveRow(int sourceIndex);
        public override void ShowRow(int sourceIndex, int visibleIndex);
        public override int[] ToArray();
        public override IEnumerable<int> ToEnumerable();
        [Conditional("DEBUGTEST")]
        private void ValidateCatalogs();

        public override int VisibleRowCount { get; }

        public override bool IsReadOnly { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisibleToSourceRowsSmartMapper.<>c <>9;
            public static Func<int?, int> <>9__20_0;
            public static Func<int?, int> <>9__21_0;

            static <>c();
            internal int <ToArray>b__20_0(int? i);
            internal int <ToEnumerable>b__21_0(int? v);
        }

        private class Catalog
        {
            private readonly List<VisibleToSourceRowsSmartMapper.Page> HandlesToPages;
            private readonly Queue<int> FreeHandles;
            private VisibleToSourceRowsSmartMapper.Page FirstPage;
            private VisibleToSourceRowsSmartMapper.Page LastPage;
            private readonly List<VisibleToSourceRowsSmartMapper.Page> Milestones;
            private bool MileStonesComplete;
            public VisibleToSourceRowsSmartMapper.Catalog OtherCatalog;
            public const int MinRecordsPerPageExceptFirstAndLast = 500;
            private const int MileStoneRange = 500;
            public const int PageCapacity = 0x400;
            private const int PageFill = 0x3e8;

            public Catalog();
            public VisibleToSourceRowsSmartMapper.Page CreateFirstPage();
            public VisibleToSourceRowsSmartMapper.Page CreateNewPageAfter(VisibleToSourceRowsSmartMapper.Page pageToInsertAfter);
            public VisibleToSourceRowsSmartMapper.Page CreateNewPageBefore(VisibleToSourceRowsSmartMapper.Page pageToInsertBefore);
            private VisibleToSourceRowsSmartMapper.Page CreatePageWithHandle();
            public void Dispose();
            public void FixMileStones(VisibleToSourceRowsSmartMapper.Page page);
            public int GetContainedItemsCount();
            [IteratorStateMachine(typeof(VisibleToSourceRowsSmartMapper.Catalog.<GetEnumerable>d__20))]
            public IEnumerable<int?> GetEnumerable();
            private VisibleToSourceRowsSmartMapper.PageAndOffset GetInsertable(int indexToInsert);
            private VisibleToSourceRowsSmartMapper.Page GetMileStone(int targetMileStone);
            public int? GetOtherIndex(int thisIndex);
            public VisibleToSourceRowsSmartMapper.PageAndOffset GetPageAndOffsetForIndex(int index);
            public VisibleToSourceRowsSmartMapper.PageAndOffset GetPageAndOffsetForIndex(int index, bool padEmptyIfNeeded);
            public VisibleToSourceRowsSmartMapper.Page GetPageByHandle(int handle);
            public VisibleToSourceRowsSmartMapper.PageAndOffset Insert(int indexToInsert, VisibleToSourceRowsSmartMapper.Record valueToInsert);
            private VisibleToSourceRowsSmartMapper.PageAndOffset InsertEmpty(int indexToInsert);
            public void RemovePage(VisibleToSourceRowsSmartMapper.Page page);
            public void TruncateExcessiveEmpty(int countExisistingForSure);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly VisibleToSourceRowsSmartMapper.Catalog.<>c <>9;
                public static Action<VisibleToSourceRowsSmartMapper.Page> <>9__25_0;
                public static Action<VisibleToSourceRowsSmartMapper.Page> <>9__25_1;

                static <>c();
                internal void <InsertEmpty>b__25_0(VisibleToSourceRowsSmartMapper.Page p);
                internal void <InsertEmpty>b__25_1(VisibleToSourceRowsSmartMapper.Page p);
            }

            [CompilerGenerated]
            private sealed class <GetEnumerable>d__20 : IEnumerable<int?>, IEnumerable, IEnumerator<int?>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private int? <>2__current;
                private int <>l__initialThreadId;
                public VisibleToSourceRowsSmartMapper.Catalog <>4__this;
                private VisibleToSourceRowsSmartMapper.Page <page>5__1;
                private IEnumerator<int?> <>7__wrap1;

                [DebuggerHidden]
                public <GetEnumerable>d__20(int <>1__state);
                private void <>m__Finally1();
                private bool MoveNext();
                [DebuggerHidden]
                IEnumerator<int?> IEnumerable<int?>.GetEnumerator();
                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                int? IEnumerator<int?>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }
        }

        private class Page
        {
            public readonly VisibleToSourceRowsSmartMapper.Catalog ThisCatalog;
            public readonly VisibleToSourceRowsSmartMapper.Catalog OtherCatalog;
            public readonly int PageHandle;
            public VisibleToSourceRowsSmartMapper.Page NextPage;
            public VisibleToSourceRowsSmartMapper.Page PrevPage;
            public int PageStart;
            public int Count;
            public int NonEmptyCount;
            public VisibleToSourceRowsSmartMapper.Record[] Data;

            public Page(VisibleToSourceRowsSmartMapper.Catalog thisCatalog, int pageHandle);
            public bool Contains(int globalIndex);
            public void EmptyIfNeeded();
            public void EnforceNonEmptyData();
            private void FixMileStones();
            public void FixOtherSideRecords(int offsetFirstToFix, int offsetBeyondLastToFix);
            public void ForAllPagesAfterDo(Action<VisibleToSourceRowsSmartMapper.Page> action);
            [IteratorStateMachine(typeof(VisibleToSourceRowsSmartMapper.Page.<GetEnumerable>d__13))]
            public IEnumerable<int?> GetEnumerable();
            public int? GetOtherIndex(int offsetFromPageStart);
            public int? HideThereAndRemoveFromOther(int sourceOffset);
            public void RemoveRecord(int offset);
            public int? RemoveThereAndRemoveFromOther(int offset);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly VisibleToSourceRowsSmartMapper.Page.<>c <>9;
                public static Action<VisibleToSourceRowsSmartMapper.Page> <>9__18_0;

                static <>c();
                internal void <RemoveRecord>b__18_0(VisibleToSourceRowsSmartMapper.Page p);
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PageAndOffset
        {
            public readonly DevExpress.Data.Helpers.VisibleToSourceRowsSmartMapper.Page Page;
            public readonly int Offset;
            public static readonly VisibleToSourceRowsSmartMapper.PageAndOffset Empty;
            public PageAndOffset(DevExpress.Data.Helpers.VisibleToSourceRowsSmartMapper.Page _Page, int _Offset);
            public bool IsEmpty { get; }
            public static implicit operator VisibleToSourceRowsSmartMapper.Record(VisibleToSourceRowsSmartMapper.PageAndOffset pof);
            public override string ToString();
            static PageAndOffset();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Record
        {
            public int PageHandle;
            public int OffsetWithinPage;
            private const int EmptyPageHandle = -1;
            public static readonly VisibleToSourceRowsSmartMapper.Record Empty;
            private static LohPooled.PoolForUndistinguishableItems<VisibleToSourceRowsSmartMapper.Record[]> DataPagesPool;
            public bool IsEmpty { get; }
            public Record(int pageHandle, int offset);
            public override string ToString();
            public static void PoolPageData(VisibleToSourceRowsSmartMapper.Record[] pageData);
            public static VisibleToSourceRowsSmartMapper.Record[] GetOrCreatePageData();
            static Record();
            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly VisibleToSourceRowsSmartMapper.Record.<>c <>9;

                static <>c();
                internal VisibleToSourceRowsSmartMapper.Record[] <.cctor>b__12_0();
            }
        }
    }
}

