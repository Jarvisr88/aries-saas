namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PSStreamingDocument : PSDocument, IDocumentProxy, IStreamingDocument
    {
        private bool buildContinuousInfo;
        private IStreamingPageBuildEngine pageBuildEngine;
        private IStreamingExportPageBuildEngine exportEngine;
        private readonly Indexator indexator;
        private readonly PSUpdatedObjects updatedObjects;
        private readonly List<Pair<PageInfoTextBrickBase, int>> bricksToUpdate;
        private readonly ObjectCache continuousBrickCache;

        public PSStreamingDocument(PrintingSystemBase ps, bool allowMultiThreading, bool buildContinuousInfo = false) : base(ps)
        {
            this.indexator = new Indexator();
            this.updatedObjects = new PSUpdatedObjects();
            this.bricksToUpdate = new List<Pair<PageInfoTextBrickBase, int>>();
            this.AllowMultiThreading = allowMultiThreading;
            this.buildContinuousInfo = buildContinuousInfo;
            this.continuousBrickCache = new ObjectCache(this.indexator);
        }

        internal override void AddBrickObjectsToCache(Brick brick)
        {
            base.AdjustNavigationPair(brick);
            base.AddBrickObjectsToCache(brick);
        }

        public void AddBrickToUpdate(PageInfoTextBrickBase brick, int pageIndex)
        {
            this.bricksToUpdate.Add(new Pair<PageInfoTextBrickBase, int>(brick, pageIndex));
        }

        protected internal override void AfterBuild()
        {
        }

        public IStreamingPageExporter CreateExporter(IStreamingPageExportProvider provider)
        {
            this.AllowCleanupBands = provider.AllowCleanupBands;
            return (!this.AllowMultiThreading ? new StreamingPageExporter(provider, this.NavigationBuilder, this, (DevExpress.XtraPrinting.Native.PageBuildEngine) this.PageBuildEngine) : new MultiThreadPageExporter(provider, this.NavigationBuilder, this, (DevExpress.XtraPrinting.Native.PageBuildEngine) this.PageBuildEngine));
        }

        protected override DevExpress.XtraPrinting.Native.PageBuildEngine CreatePageBuildEngine(bool buildPagesInBackground, bool rollPaper)
        {
            if (rollPaper | buildPagesInBackground)
            {
                return base.CreatePageBuildEngine(buildPagesInBackground, rollPaper);
            }
            this.PageBuildEngine.AfterBuild += new Action0(this.HandleAfterBuild);
            return (DevExpress.XtraPrinting.Native.PageBuildEngine) this.PageBuildEngine;
        }

        protected override PageList CreatePageList() => 
            new StreamingPageList(this);

        protected override void CreateSerializationObjects()
        {
            this.bricks = !ReferenceEquals(base.bricks, this.continuousBrickCache) ? new ObjectCache(this.indexator) : this.continuousBrickCache;
            base.styles = new ObjectCache();
            base.images = new ObjectCache(ImageEntry.ImageEntryEqualityComparer.Instance);
        }

        internal virtual IStreamingExportPageBuildEngine CreateStreamingExportPageBuildEngine() => 
            new StreamingExportPageBuildEngine(this);

        internal virtual IStreamingPageBuildEngine CreateStreamingPageBuildEngine() => 
            new StreamingPageBuildEngine(this);

        ContinuousExportInfo IStreamingDocument.GetContinuousExportInfo() => 
            this.GetContinuousExportInfo();

        public IEnumerable<Page> EnumeratePages() => 
            this.buildContinuousInfo ? this.EnumeratePagesWithContinuousInfo() : this.EnumeratePagesCore();

        [IteratorStateMachine(typeof(<EnumeratePagesCore>d__57))]
        private IEnumerable<Page> EnumeratePagesCore()
        {
            <EnumeratePagesCore>d__57 d__1 = new <EnumeratePagesCore>d__57(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        [IteratorStateMachine(typeof(<EnumeratePagesWithContinuousInfo>d__56))]
        private IEnumerable<Page> EnumeratePagesWithContinuousInfo()
        {
            <EnumeratePagesWithContinuousInfo>d__56 d__1 = new <EnumeratePagesWithContinuousInfo>d__56(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        public void FinishBuild()
        {
            this.PageBuildEngine.OnAfterBuild();
            foreach (Pair<PageInfoTextBrickBase, int> pair in this.bricksToUpdate)
            {
                pair.First.Text = pair.First.GetTextInfo(base.PrintingSystem, new DeferredPageItem(pair.Second, this.PageCount));
            }
            base.state = DocumentState.Created;
        }

        public int GetBuiltBandIndex(DocumentBand band) => 
            this.PageBuildEngine.GetBuiltBandIndex(band);

        protected internal override ContinuousExportInfo GetContinuousExportInfo() => 
            (!this.buildContinuousInfo || (this.exportEngine == null)) ? ContinuousExportInfo.Empty : this.exportEngine.CreateContinuousExportInfo();

        private static int[] GetMinimalIndexes(int[] indexes1, int[] indexes2)
        {
            for (int i = 0; (i < indexes1.Length) && (i < indexes2.Length); i++)
            {
                if (indexes1[i] < indexes2[i])
                {
                    return indexes1;
                }
                if (indexes1[i] > indexes2[i])
                {
                    return indexes2;
                }
            }
            return ((indexes1.Length < indexes2.Length) ? indexes1 : indexes2);
        }

        public int[] GetPageBandsIndexes(Page page) => 
            this.PageBuildEngine.GetPageBandsIndexes(page);

        internal void HandleAfterBuild()
        {
            base.AfterBuild();
            this.PageBuildEngine.AfterBuild -= new Action0(this.HandleAfterBuild);
        }

        private void IterateExportEngine()
        {
            this.exportEngine ??= this.CreateStreamingExportPageBuildEngine();
            if (base.ps.GetService<GroupingManager>() == null)
            {
                this.exportEngine.FillPage(base.Root, this.PageBuildEngine);
            }
            else
            {
                try
                {
                    base.ps.RemoveService<GroupingManager>();
                    this.exportEngine.FillPage(base.Root, this.PageBuildEngine);
                }
                finally
                {
                    GroupingManager manager;
                    base.ps.AddService<GroupingManager>(manager);
                }
            }
            int[] minimalIndexes = GetMinimalIndexes(((DevExpress.XtraPrinting.Native.PageBuildEngine) this.PageBuildEngine).GetBuildingBandIndexes(base.Root).ToArray<int>(), ((DevExpress.XtraPrinting.Native.PageBuildEngine) this.exportEngine).GetBuildingBandIndexes(base.Root).ToArray<int>());
            base.Root.CleanupBands(minimalIndexes, 0);
        }

        protected override void NullCaches()
        {
            base.styles = null;
            base.images = null;
            if (!this.AllowCleanupBands && ((base.bricks != null) && !ReferenceEquals(base.bricks, this.continuousBrickCache)))
            {
                this.continuousBrickCache.Merge(base.bricks);
            }
            base.bricks = null;
        }

        protected internal override void OnClearPages()
        {
            base.CanChangePageSettings = true;
        }

        protected override void OnEndDeserializingCore()
        {
        }

        protected override void OnStartDeserializingCore()
        {
            this.CreateSerializationObjects();
        }

        public void PrepareDocumentSerialization()
        {
            this.CreateSerializationObjects();
        }

        public void PrepareSerialization(ContinuousExportInfo info)
        {
            info.ExecuteExport(new Document.ContinuousExportBrickCollector(this), base.PrintingSystem);
        }

        public void PrepareSerialization(Page page)
        {
            List<Page> pages = new List<Page>();
            pages.Add(page);
            base.PreparePageSerialization(pages);
        }

        public void RemovePageBandsIndexes(Page page)
        {
            this.PageBuildEngine.RemovePageBandsIndexes(page);
        }

        public bool BuildContinuousInfo
        {
            get => 
                this.buildContinuousInfo;
            set => 
                this.buildContinuousInfo = value;
        }

        public PSUpdatedObjects UpdatedObjects =>
            this.updatedObjects;

        internal IStreamingPageBuildEngine PageBuildEngine
        {
            get
            {
                IStreamingPageBuildEngine engine;
                if (this.pageBuildEngine != null)
                {
                    return this.pageBuildEngine;
                }
                this.pageBuildEngine = engine = this.CreateStreamingPageBuildEngine();
                return engine;
            }
        }

        public virtual int BuiltPageCount =>
            ((StreamingPageList) base.Pages).PageIndexesCount;

        bool IStreamingDocument.BookmarkDuplicateSuppress =>
            base.BookmarkDuplicateSuppress;

        BookmarkNode IStreamingDocument.RootBookmark =>
            base.RootBookmark;

        NavigationInfo IStreamingDocument.NavigationInfo =>
            base.NavigationInfo;

        IList<Page> IStreamingDocument.Pages =>
            base.Pages;

        public bool AllowMultiThreading { get; private set; }

        public IStreamingNavigationBuilder NavigationBuilder { get; set; }

        public float MaxBrickRight { get; set; }

        protected bool AllowCleanupBands { get; set; }

        [CompilerGenerated]
        private sealed class <EnumeratePagesCore>d__57 : IEnumerable<Page>, IEnumerable, IEnumerator<Page>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Page <>2__current;
            private int <>l__initialThreadId;
            public PSStreamingDocument <>4__this;
            private AfterPrintOnPageProcessor <processor>5__1;
            private IEnumerator<Page> <>7__wrap1;

            [DebuggerHidden]
            public <EnumeratePagesCore>d__57(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
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
                        this.<processor>5__1 = new AfterPrintOnPageProcessor(this.<>4__this);
                        this.<>7__wrap1 = this.<>4__this.PageBuildEngine.EnumeratePages(this.<processor>5__1).GetEnumerator();
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
                        this.<>4__this.MaxBrickRight = this.<processor>5__1.MaxBrickRight;
                        flag = false;
                    }
                    else
                    {
                        Page current = this.<>7__wrap1.Current;
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
            IEnumerator<Page> IEnumerable<Page>.GetEnumerator()
            {
                PSStreamingDocument.<EnumeratePagesCore>d__57 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new PSStreamingDocument.<EnumeratePagesCore>d__57(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.XtraPrinting.Page>.GetEnumerator();

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

            Page IEnumerator<Page>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <EnumeratePagesWithContinuousInfo>d__56 : IEnumerable<Page>, IEnumerable, IEnumerator<Page>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Page <>2__current;
            private int <>l__initialThreadId;
            public PSStreamingDocument <>4__this;
            private AfterPrintOnPageProcessor <processor>5__1;
            private IEnumerator<Page> <>7__wrap1;

            [DebuggerHidden]
            public <EnumeratePagesWithContinuousInfo>d__56(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
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
                        this.<processor>5__1 = new AfterPrintOnPageProcessor(this.<>4__this);
                        this.<>7__wrap1 = this.<>4__this.PageBuildEngine.EnumeratePages(this.<processor>5__1).GetEnumerator();
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
                    if (this.<>7__wrap1.MoveNext())
                    {
                        Page current = this.<>7__wrap1.Current;
                        this.<>4__this.IterateExportEngine();
                        this.<>2__current = current;
                        this.<>1__state = 1;
                        flag = true;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        this.<>4__this.MaxBrickRight = this.<processor>5__1.MaxBrickRight;
                        if (this.<>4__this.exportEngine != null)
                        {
                            this.<>4__this.IterateExportEngine();
                            this.<>4__this.exportEngine.AfterFillPage(this.<>4__this.Root);
                        }
                        flag = false;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<Page> IEnumerable<Page>.GetEnumerator()
            {
                PSStreamingDocument.<EnumeratePagesWithContinuousInfo>d__56 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new PSStreamingDocument.<EnumeratePagesWithContinuousInfo>d__56(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.XtraPrinting.Page>.GetEnumerator();

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

            Page IEnumerator<Page>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

