namespace DevExpress.XtraPrinting.Native.Navigation
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class SearchHelperBase
    {
        private int brickIndex;
        private FindState state;
        private BrickPagePairCollection brickPagePairs;
        private Guid documentID;

        public SearchHelperBase();
        private int CalcIndex(SearchDirection searchDirection);
        public BrickPagePair CircleFindNext(PrintingSystemBase ps, string text, SearchDirection searchDirection, bool matchWholeWord, bool matchCase);
        private void FindNextCore(PrintingSystemBase ps, string text, SearchDirection searchDirection, bool matchWholeWord, bool matchCase);
        protected int GetBrickIndex(SearchDirection searchDirection, bool isSearchFinished);
        public Task GetSearchTask(PrintingSystemBase printingSystem, Action<List<SearchData>> fillSearchResultAction, TextBrickSelector selector, CancellationTokenSource ts, IEnumerable<BookmarkNode> bookmarkNodes);
        public void Reset();
        public void ResetSearchResults();
        public bool SearchFinished(SearchDirection searchDirection);
        internal BrickPagePairCollection SelectBrickPagePairs(IPrintingSystemContext context, string text, bool matchWholeWord, bool matchCase);
        [IteratorStateMachine(typeof(SearchHelperBase.<SplitPages>d__15))]
        private IEnumerable<List<Page>> SplitPages(List<Page> pages, int batchSize = 5);
        private void UpdatePages(PrintingDocument document);

        [CompilerGenerated]
        private sealed class <SplitPages>d__15 : IEnumerable<List<Page>>, IEnumerable, IEnumerator<List<Page>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private List<Page> <>2__current;
            private int <>l__initialThreadId;
            private List<Page> pages;
            public List<Page> <>3__pages;
            private int batchSize;
            public int <>3__batchSize;
            private int <i>5__1;

            [DebuggerHidden]
            public <SplitPages>d__15(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<List<Page>> IEnumerable<List<Page>>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            List<Page> IEnumerator<List<Page>>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        private class CustomNavigateHelper : NavigateHelper
        {
            private readonly IPrintingSystemContext context;

            public CustomNavigateHelper(IPrintingSystemContext context);
            protected override bool CanIterateBrick(NestedBrickIterator iterator);
            protected override void PreprocessBrick(NestedBrickIterator iterator, IPageItem pageItem);
        }

        private class SimplePageRepository : IPageRepository
        {
            private readonly IEnumerable<Page> pages;

            public SimplePageRepository(IEnumerable<Page> pages);
            bool IPageRepository.TryGetPageByID(long id, out Page page, out int index);
            bool IPageRepository.TryGetPageByIndex(int index, out Page page);
        }
    }
}

