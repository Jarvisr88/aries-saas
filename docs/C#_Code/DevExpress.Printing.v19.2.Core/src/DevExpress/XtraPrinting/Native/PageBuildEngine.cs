namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PageBuildEngine : IPageBuildEngine
    {
        protected DocumentBand rootBand;
        protected IDocumentProxy document;
        private ReadonlyPageData defaultPageData;
        private ReadonlyPageData actualPageData;
        private CustomPageData nextPageData;
        private RootDocumentBand root;
        protected XPageContentEngine xContentEngine;
        protected PageRowBuilder rowBuilder;
        private Dictionary<long, int[]> bandRanges;

        public PageBuildEngine(RootDocumentBand root, IDocumentProxy document);
        protected PageBuildEngine(RootDocumentBand root, IDocumentProxy document, XPageContentEngine xContentEngine, bool fillEmptySpace);
        public virtual void Abort();
        protected void AddBandRanges(Page lastPage);
        private void AddBottomMargin(PSPage psPage, int rowIndex, DocumentBand band);
        protected static bool AddedBandsAreActual(IList<BandBricksPair> addedBands);
        private void AddMargins(PSPage psPage, Pair<int, int> rowIndexes, IPageSection pageSection);
        protected virtual bool AddPage(HashSet<long> pages, PSPage psPage, YPageContentEngine pageContentEngine);
        private void AddTopMargin(PSPage psPage, int rowIndex, DocumentBand band);
        protected virtual void AfterBuildPage(PSPage page, RectangleF usefulPageArea);
        protected internal void AfterBuildPages();
        private static bool BricksAreEmpty(IEnumerable<BrickBase> bricks);
        protected virtual void Build();
        public void BuildPages(DocumentBand rootBand);
        protected virtual bool CheckPages(int pageCount, YPageContentEngine pageContentEngine);
        protected virtual YPageContentEngine CreateContentEngine(PSPage psPage, YPageContentEngine previous);
        protected virtual PSPage CreatePage(SizeF pageSize);
        protected static ReadonlyPageData CreatePageData(CustomPageData source, ReadonlyPageData defaultData);
        protected virtual PageRowBuilder CreatePageRowBuilder(YPageContentEngine pageContentEngine);
        int IPageBuildEngine.GetBuiltBandIndex(DocumentBand band);
        protected void EnsureProgressReflectorRanges(DocumentBand rootBand);
        [IteratorStateMachine(typeof(PageBuildEngine.<GetBuildingBandIndexes>d__66))]
        public IEnumerable<int> GetBuildingBandIndexes(DocumentBand root);
        private int GetBuiltBandIndex(DocumentBand band);
        public int[] GetPageBandsIndexes(Page page);
        protected Dictionary<Brick, RectangleF> GetPageBricks(DocumentBand docBand, RectangleF bounds, SimplePageRowBuilder builder, int rowIndex);
        protected virtual void InitializeContentEngine(YPageContentEngine contentEngine);
        private void InvalidateActualPageData();
        private void ProcessPage(PSPage page, XPageContentEngine contentEngine);
        protected bool RaiseCreateDocumentException(Exception exception);
        public void RemovePageBandsIndexes(Page page);
        protected virtual void ResetRootBand(DocumentBand rootBand);
        public virtual void Stop();
        private static RectangleF ValidateMinSize(RectangleF rect);
        private static ReadonlyPageData ValidateUsefulPageSize(ReadonlyPageData pageSettingsData);
        public static void ValidateUsefulPageSize(SizeF usefulPagSize);

        bool IPageBuildEngine.Stopped { get; }

        protected bool Stopped { get; private set; }

        protected bool Aborted { get; private set; }

        protected PrintingSystemBase PrintingSystem { get; }

        protected ReadonlyPageData ActualPageData { get; }

        protected CustomPageData NextPageData { get; set; }

        protected SizeF ActualPageSizeF { get; }

        protected virtual RectangleF ActualUsefulPageRectF { get; }

        private bool FillEmptySpace { get; set; }

        protected RootDocumentBand Root { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageBuildEngine.<>c <>9;
            public static Func<BrickBase, bool> <>9__64_0;

            static <>c();
            internal bool <BricksAreEmpty>b__64_0(BrickBase item);
        }

        [CompilerGenerated]
        private sealed class <GetBuildingBandIndexes>d__66 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            private int <>l__initialThreadId;
            private DocumentBand root;
            public DocumentBand <>3__root;
            public PageBuildEngine <>4__this;
            private int <index>5__1;
            private IEnumerator<int> <>7__wrap1;

            [DebuggerHidden]
            public <GetBuildingBandIndexes>d__66(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<int> IEnumerable<int>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            int IEnumerator<int>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

