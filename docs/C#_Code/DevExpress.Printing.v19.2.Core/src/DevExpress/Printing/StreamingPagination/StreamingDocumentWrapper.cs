namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class StreamingDocumentWrapper : Document, IStreamingDocument
    {
        private readonly IStoredIDProvider storedIdProvider;
        private PSUpdatedObjects updatedObjects;
        private PrintingDocument document;
        private ContinuousExportInfo continuousInfo;
        private IList<Page> pages;

        public StreamingDocumentWrapper(PrintingDocument document) : base(document.PrintingSystem)
        {
            this.storedIdProvider = new StoredIDProvider();
            this.updatedObjects = new PSUpdatedObjects();
            this.document = document;
            base.IsModified = document.IsModified;
            base.Name = document.Name;
            this.pages = document.Pages;
            PartiallyDeserializedDocument document2 = document as PartiallyDeserializedDocument;
            if (document2 != null)
            {
                this.updatedObjects = document2.UpdatedObjects;
                this.storedIdProvider.SetLastID(document2.SerializationOptions.LastStoredID);
            }
        }

        protected internal override void AddBrick(Brick brick)
        {
            throw new NotSupportedException();
        }

        internal override void AddBrickObjectsToCache(Brick brick)
        {
            base.AddRealBrickObjectsToCache(brick);
            base.AddBrickObjectsToCacheRecursive(brick);
            base.AddStyleToCache(brick);
        }

        protected internal override DocumentBand AddReportContainer()
        {
            throw new NotSupportedException();
        }

        protected internal override void BeginReport(DocumentBand docBand, PointF offset)
        {
            throw new NotSupportedException();
        }

        protected override PageList CreatePageList() => 
            this.document.Pages;

        void IStreamingDocument.AddBrickToUpdate(PageInfoTextBrickBase brick, int pageIndex)
        {
            throw new NotSupportedException();
        }

        IStreamingPageExporter IStreamingDocument.CreateExporter(IStreamingPageExportProvider provider) => 
            new StreamingPageExporter(provider, this.NavigationBuilder, this, new PageBuildEngine(this.document.Root, this.document));

        [IteratorStateMachine(typeof(<DevExpress-Printing-StreamingPagination-IStreamingDocument-EnumeratePages>d__36))]
        IEnumerable<Page> IStreamingDocument.EnumeratePages()
        {
            <DevExpress-Printing-StreamingPagination-IStreamingDocument-EnumeratePages>d__36 d__1 = new <DevExpress-Printing-StreamingPagination-IStreamingDocument-EnumeratePages>d__36(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        void IStreamingDocument.FinishBuild()
        {
            this.pages = new List<Page>();
        }

        int IStreamingDocument.GetBuiltBandIndex(DocumentBand band) => 
            0;

        ContinuousExportInfo IStreamingDocument.GetContinuousExportInfo() => 
            this.GetContinuousExportInfo();

        int[] IStreamingDocument.GetPageBandsIndexes(Page page) => 
            new int[0];

        void IStreamingDocument.PrepareDocumentSerialization()
        {
            this.CreateSerializationObjects();
        }

        void IStreamingDocument.PrepareSerialization(ContinuousExportInfo info)
        {
            info.ExecuteExport(new Document.ContinuousExportBrickCollector(this), base.PrintingSystem);
        }

        void IStreamingDocument.PrepareSerialization(Page page)
        {
            List<Page> pages = new List<Page>();
            pages.Add(page);
            base.PreparePageSerialization(pages);
            page.PerformLayout(new PrintingSystemContextWrapper(base.ps, page));
            foreach (VisualBrick brick in page.AllBricks().OfType<VisualBrick>())
            {
                StoredID objectID = this.storedIdProvider.SetNextID(brick);
                BrickPagePair navigationPair = brick.NavigationPair;
                if ((navigationPair != null) && (navigationPair.PageID != -1L))
                {
                    this.UpdatedObjects.UpdateProperty(objectID, PSUpdatedObjects.NavigationPageIdProperty, brick.NavigationPageId);
                    this.UpdatedObjects.UpdateProperty(objectID, PSUpdatedObjects.NavigationBrickIndicesProperty, brick.NavigationBrickIndices);
                    this.UpdatedObjects.UpdateProperty(objectID, PSUpdatedObjects.NavigationBrickBoundsProperty, navigationPair.BrickBounds);
                }
            }
        }

        void IStreamingDocument.RemovePageBandsIndexes(Page page)
        {
        }

        protected internal override void End(bool buildPagesInBackground)
        {
            throw new NotSupportedException();
        }

        protected internal override void EndReport()
        {
            throw new NotSupportedException();
        }

        protected internal override ContinuousExportInfo GetContinuousExportInfo()
        {
            ContinuousExportInfo continuousInfo = this.continuousInfo;
            if (this.continuousInfo == null)
            {
                ContinuousExportInfo local1 = this.continuousInfo;
                continuousInfo = this.continuousInfo = new StreamingContinuousInfoWrapper(this.document.GetContinuousExportInfo());
            }
            return continuousInfo;
        }

        protected override SerializationInfo GetIndexByObjectCore(string propertyName, object obj) => 
            ((propertyName == "SharedStyles") || (propertyName == "Style")) ? base.styles.GetIndexByObject(obj) : null;

        protected internal override void HandleNewPageSettings()
        {
            throw new NotSupportedException();
        }

        protected internal override void HandleNewScaleFactor()
        {
            throw new NotSupportedException();
        }

        protected internal override void InsertPageBreak(float pos)
        {
            throw new NotSupportedException();
        }

        protected internal override void InsertPageBreak(float pos, CustomPageData nextPageData)
        {
            throw new NotSupportedException();
        }

        public override void ShowFromNewPage(Brick brick)
        {
            throw new NotSupportedException();
        }

        protected internal override void StopPageBuilding()
        {
            throw new NotSupportedException();
        }

        public PSUpdatedObjects UpdatedObjects =>
            this.updatedObjects;

        public IStreamingNavigationBuilder NavigationBuilder { get; set; }

        float IStreamingDocument.MaxBrickRight =>
            0f;

        bool IStreamingDocument.BuildContinuousInfo =>
            true;

        int IStreamingDocument.BuiltPageCount =>
            this.document.PageCount;

        RootDocumentBand IStreamingDocument.Root =>
            this.document.Root;

        bool IStreamingDocument.BookmarkDuplicateSuppress =>
            this.document.BookmarkDuplicateSuppress;

        BookmarkNode IStreamingDocument.RootBookmark =>
            this.document.RootBookmark;

        IBookmarkNodeCollection IStreamingDocument.BookmarkNodes =>
            this.document.BookmarkNodes;

        PrintingSystemBase IStreamingDocument.PrintingSystem =>
            this.document.PrintingSystem;

        bool IStreamingDocument.AllowMultiThreading =>
            false;

        NavigationInfo IStreamingDocument.NavigationInfo =>
            this.document.NavigationInfo;

        IList<Page> IStreamingDocument.Pages =>
            this.pages;

        public override int AutoFitToPagesWidth
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override float ScaleFactor
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected internal override RectangleF PageFootBounds
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        protected internal override RectangleF PageHeadBounds
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        protected internal override float MinPageHeight
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        protected internal override float MinPageWidth
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Printing-StreamingPagination-IStreamingDocument-EnumeratePages>d__36 : IEnumerable<Page>, IEnumerable, IEnumerator<Page>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Page <>2__current;
            private int <>l__initialThreadId;
            public StreamingDocumentWrapper <>4__this;
            private IEnumerator<Page> <>7__wrap1;

            [DebuggerHidden]
            public <DevExpress-Printing-StreamingPagination-IStreamingDocument-EnumeratePages>d__36(int <>1__state)
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
                        if (this.<>4__this.pages != null)
                        {
                            this.<>7__wrap1 = this.<>4__this.pages.GetEnumerator();
                            this.<>1__state = -3;
                        }
                        else
                        {
                            return false;
                        }
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
                StreamingDocumentWrapper.<DevExpress-Printing-StreamingPagination-IStreamingDocument-EnumeratePages>d__36 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new StreamingDocumentWrapper.<DevExpress-Printing-StreamingPagination-IStreamingDocument-EnumeratePages>d__36(0) {
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

