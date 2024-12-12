namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    [SerializationContext(typeof(PrintingSystemSerializationContext))]
    public abstract class Document : IDisposable, IXtraSerializable, IXtraSortableProperties, IXtraRootSerializationObject, ISerializationCacheProvider
    {
        private const string DefaultName = "Document";
        protected PageList fPages;
        protected PrintingSystemBase ps;
        private string name = "Document";
        private bool correctImportBrickBounds;
        private BookmarkNode rootBookmark;
        private bool isDisposed;
        private bool canChangePageSettings = true;
        private bool canAutoFitToPagesWidth = true;
        protected DocumentState state;
        private Guid contentIdentity = Guid.Empty;
        private List<PageDataWithIndices> pageData;
        protected ObjectCache styles;
        protected ObjectCache images;
        protected ObjectCache bricks;

        protected Document(PrintingSystemBase ps)
        {
            this.ps = ps;
            this.rootBookmark = this.CreateRootBookmarkNode("Document");
            ps.AfterMarginsChange += new MarginsChangeEventHandler(this.ps_MarginsChanged);
            ps.PageSettingsChanged += new EventHandler(this.ps_PageSettingsChanged);
            ps.ScaleFactorChanged += new EventHandler(this.ps_ScaleFactorChanged);
        }

        protected internal abstract void AddBrick(Brick brick);
        internal virtual void AddBrickObjectsToCache(Brick brick)
        {
            this.bricks.AddSerializationObject(brick);
            this.AddRealBrickObjectsToCache(brick);
            this.bricks.StopCollectSharedObjects();
            this.AddBrickObjectsToCacheRecursive(brick);
            this.bricks.StartCollectSharedObjects();
            this.AddStyleToCache(brick);
            this.AddImageToCache(brick);
        }

        protected void AddBrickObjectsToCacheRecursive(Brick brick)
        {
            if (!(brick is CheckBoxTextBrick))
            {
                foreach (Brick brick2 in brick.Bricks)
                {
                    this.AddBrickObjectsToCache(brick2);
                }
            }
        }

        protected void AddImageToCache(Brick brick)
        {
            ImageBrick brick2 = brick as ImageBrick;
            if ((brick2 != null) && brick2.ShouldSerializeImageEntryInternal())
            {
                this.images.AddSerializationObject(brick2.ImageEntry);
            }
            ICheckBoxBrick brick3 = brick as ICheckBoxBrick;
            if (brick3 != null)
            {
                foreach (CheckBoxGlyph glyph in brick3.CustomGlyphs)
                {
                    this.images.AddSerializationObject(glyph.ImageEntry);
                }
            }
        }

        protected void AddRealBrickObjectsToCache(Brick brick)
        {
            Brick realBrick = brick.GetRealBrick();
            if (!ReferenceEquals(realBrick, brick))
            {
                this.AddBrickObjectsToCache(realBrick);
            }
        }

        protected internal abstract DocumentBand AddReportContainer();
        protected void AddStyleToCache(Brick brick)
        {
            VisualBrick brick2 = brick as VisualBrick;
            if (brick2 != null)
            {
                this.styles.AddSerializationObject(brick2.Style);
            }
        }

        protected void AdjustNavigationPair(Brick brick)
        {
            VisualBrick brick2 = brick as VisualBrick;
            BrickPagePair navigationPair = brick2?.NavigationPair;
            if ((navigationPair != null) && (navigationPair.PageID != -1L))
            {
                brick2.NavigationPair = BrickPagePair.Create(navigationPair.BrickIndices, this.Pages.GetPageIndexByID(navigationPair.PageID), navigationPair.PageID, navigationPair.BrickBounds);
            }
        }

        protected virtual void AfterSerialize()
        {
            this.ClearPageData();
            this.NullCaches();
            this.ps.ProgressReflector.MaximizeRange();
        }

        protected internal virtual void Begin()
        {
            this.correctImportBrickBounds = false;
        }

        protected internal abstract void BeginReport(DocumentBand docBand, PointF offset);
        protected virtual void Clear()
        {
            this.ps.ClearEditingFields();
            int count = 0;
            if (this.fPages != null)
            {
                count = this.fPages.Count;
                this.DisposePages();
                this.fPages.Clear();
            }
            Action<GroupingManager> callback = <>c.<>9__153_0;
            if (<>c.<>9__153_0 == null)
            {
                Action<GroupingManager> local1 = <>c.<>9__153_0;
                callback = <>c.<>9__153_0 = groupingManager => groupingManager.Clear();
            }
            this.ps.PerformIfNotNull<GroupingManager>(callback);
            this.state = DocumentState.Empty;
            this.contentIdentity = Guid.Empty;
            if (count > 0)
            {
                this.OnContentChanged();
            }
        }

        internal virtual void ClearContent()
        {
            this.Clear();
        }

        protected void ClearPageData()
        {
            this.pageData = null;
        }

        private void CollectPageData(IList<Page> pages)
        {
            Dictionary<ReadonlyPageData, StringBuilder> dictionary = new Dictionary<ReadonlyPageData, StringBuilder>();
            for (int i = 0; i < pages.Count; i++)
            {
                ReadonlyPageData pageData = pages[i].PageData;
                if (!dictionary.ContainsKey(pageData))
                {
                    dictionary.Add(pageData, new StringBuilder(i.ToString()));
                }
                else
                {
                    dictionary[pageData].Append(',');
                    dictionary[pageData].Append(i.ToString());
                }
            }
            this.pageData = new List<PageDataWithIndices>();
            foreach (KeyValuePair<ReadonlyPageData, StringBuilder> pair in dictionary)
            {
                this.pageData.Add(new PageDataWithIndices(pair.Key, pair.Value.ToString()));
            }
        }

        internal virtual BrickPagePair CreateBrickPagePair(XtraPropertyInfo pageIndexProperty, XtraPropertyInfo brickIndicesProperty)
        {
            if (brickIndicesProperty == null)
            {
                return BrickPagePair.Empty;
            }
            int pageIndex = int.Parse(pageIndexProperty.Value.ToString());
            int[] brickIndexes = BrickPagePairHelper.ParseIndices(brickIndicesProperty.Value.ToString());
            if (this.ps != null)
            {
                IBrickPagePairFactory service = ((IServiceProvider) this.ps).GetService(typeof(IBrickPagePairFactory)) as IBrickPagePairFactory;
                if (service != null)
                {
                    return service.CreateBrickPagePair(brickIndexes, pageIndex);
                }
            }
            return BrickPagePair.Create(brickIndexes, this.Pages[pageIndex]);
        }

        protected abstract PageList CreatePageList();
        protected virtual BookmarkNode CreateRootBookmarkNode(string name) => 
            new RootBookmarkNode(name);

        protected virtual void CreateSerializationObjects()
        {
            this.styles = new ObjectCache();
            this.images = new ObjectCache(ImageEntry.ImageEntryEqualityComparer.Instance);
            this.bricks = new ObjectCache();
        }

        internal void Deserialize(Stream stream, XtraSerializer serializer)
        {
            Predicate<int> predicate = <>c.<>9__103_0;
            if (<>c.<>9__103_0 == null)
            {
                Predicate<int> local1 = <>c.<>9__103_0;
                predicate = <>c.<>9__103_0 = index => true;
            }
            DocumentDeserializationCollection objects = new DocumentDeserializationCollection(this, predicate);
            objects.Add(new DocumentSerializationOptions(this));
            objects.Add(this.GetDeserializationContinuousExportInfo());
            this.DeserializeCore(stream, serializer, objects);
        }

        protected void DeserializeCore(Stream stream, XtraSerializer serializer, DocumentSerializationCollection objects)
        {
            serializer.DeserializeObjects(this, objects, stream, string.Empty, null);
        }

        void IXtraRootSerializationObject.AfterSerialize()
        {
            this.AfterSerialize();
        }

        SerializationInfo IXtraRootSerializationObject.GetIndexByObject(string propertyName, object obj) => 
            this.GetIndexByObjectCore(propertyName, obj);

        object IXtraRootSerializationObject.GetObjectByIndex(string propertyName, int index) => 
            this.GetObjectByIndexCore(propertyName, index);

        bool IXtraSortableProperties.ShouldSortProperties() => 
            true;

        void IXtraSerializable.OnEndDeserializing(string restoredVersion)
        {
            this.ClearPageData();
            this.OnEndDeserializingCore();
        }

        void IXtraSerializable.OnEndSerializing()
        {
            this.OnEndSerializingCore();
        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e)
        {
            this.pageData = new List<PageDataWithIndices>();
            this.OnStartDeserializingCore();
        }

        void IXtraSerializable.OnStartSerializing()
        {
        }

        void ISerializationCacheProvider.AddBrick(object obj, XtraItemEventArgs e)
        {
            this.bricks.AddDeserializationObject(obj, e);
        }

        void ISerializationCacheProvider.AddImage(object obj, XtraItemEventArgs e)
        {
            this.images.AddDeserializationObject(obj, e);
        }

        void ISerializationCacheProvider.AddStyle(object obj, XtraItemEventArgs e)
        {
            this.styles.AddDeserializationObject(obj, e);
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
        }

        protected internal virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Clear();
                if (this.ps != null)
                {
                    this.ps.AfterMarginsChange -= new MarginsChangeEventHandler(this.ps_MarginsChanged);
                    this.ps.PageSettingsChanged -= new EventHandler(this.ps_PageSettingsChanged);
                    this.ps.ScaleFactorChanged -= new EventHandler(this.ps_ScaleFactorChanged);
                }
                this.isDisposed = true;
            }
        }

        protected virtual void DisposePages()
        {
            foreach (IDisposable disposable in this.fPages)
            {
                disposable.Dispose();
            }
        }

        protected internal abstract void End(bool buildPagesInBackground);
        protected internal abstract void EndReport();
        protected internal virtual void ForceLoad()
        {
        }

        protected virtual object[] GetAvailableExportModes(Type exportModeType)
        {
            if (exportModeType == typeof(ImageExportMode))
            {
                return new object[] { ImageExportMode.SingleFilePageByPage, ImageExportMode.DifferentFiles };
            }
            if (exportModeType == typeof(RtfExportMode))
            {
                return new object[] { RtfExportMode.SingleFilePageByPage };
            }
            if (exportModeType == typeof(DocxExportMode))
            {
                return new object[] { DocxExportMode.SingleFilePageByPage };
            }
            if (exportModeType == typeof(HtmlExportMode))
            {
                return new object[] { HtmlExportMode.SingleFilePageByPage, HtmlExportMode.DifferentFiles };
            }
            if (exportModeType == typeof(XlsExportMode))
            {
                return new object[] { XlsExportMode.SingleFilePageByPage, XlsExportMode.DifferentFiles };
            }
            if (!(exportModeType == typeof(XlsxExportMode)))
            {
                return null;
            }
            return new object[] { XlsxExportMode.SingleFilePageByPage, XlsxExportMode.DifferentFiles };
        }

        public Brick GetBrick(BookmarkNode bookmarkNode) => 
            ((bookmarkNode.PageIndex < 0) || (bookmarkNode.PageIndex > this.Pages.Count)) ? null : ((Brick) this.Pages[bookmarkNode.PageIndex].GetBrickByIndices(bookmarkNode.Pair.BrickIndices));

        protected internal abstract ContinuousExportInfo GetContinuousExportInfo();
        internal virtual ContinuousExportInfo GetDeserializationContinuousExportInfo() => 
            new DeserializedContinuousExportInfo();

        protected virtual SerializationInfo GetIndexByObjectCore(string propertyName, object obj)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(propertyName);
            if (num > 0x8d663932)
            {
                if (num > 0xcfb8a8f6)
                {
                    if (num == 0xd0670c2b)
                    {
                        if (propertyName == "Bricks")
                        {
                            goto TR_0003;
                        }
                    }
                    else if ((num == 0xded3ca7a) && (propertyName == "Brick"))
                    {
                        goto TR_0003;
                    }
                }
                else if (num == 0xa18f2279)
                {
                    if (propertyName == "InnerBricks")
                    {
                        goto TR_0003;
                    }
                }
                else if ((num == 0xcfb8a8f6) && (propertyName == "SharedImages"))
                {
                    goto TR_0006;
                }
                goto TR_0001;
            }
            else if (num > 0x5467ec76)
            {
                if (num == 0x74a02b1e)
                {
                    if (propertyName == "SharedStyles")
                    {
                        goto TR_0000;
                    }
                }
                else if ((num == 0x8d663932) && (propertyName == "ImageEntry"))
                {
                    goto TR_0006;
                }
                goto TR_0001;
            }
            else if (num == 0x9c6cd1e)
            {
                if (propertyName != "SharedBricks")
                {
                    goto TR_0001;
                }
            }
            else
            {
                if ((num == 0x5467ec76) && (propertyName == "Style"))
                {
                    goto TR_0000;
                }
                goto TR_0001;
            }
            goto TR_0003;
        TR_0000:
            return this.styles.GetIndexByObject(obj);
        TR_0001:
            return ExceptionHelper.ThrowInvalidOperationException<SerializationInfo>();
        TR_0003:
            return this.bricks.GetIndexByObject(obj);
        TR_0006:
            return this.images.GetIndexByObject(obj);
        }

        protected virtual object GetObjectByIndexCore(string propertyName, int index) => 
            ExceptionHelper.ThrowInvalidOperationException<object>();

        protected internal abstract void HandleNewPageSettings();
        protected internal abstract void HandleNewScaleFactor();
        protected internal abstract void InsertPageBreak(float pos);
        protected internal abstract void InsertPageBreak(float pos, CustomPageData nextPageData);
        protected internal virtual void LoadPage(int pageIndex)
        {
        }

        protected virtual void NullCaches()
        {
            this.styles = null;
            this.images = null;
            this.bricks = null;
        }

        protected internal virtual void OnClearPages()
        {
            this.BookmarkNodes.Clear();
            this.ps.OnClearPages();
            this.CanChangePageSettings = true;
        }

        protected internal void OnContentChanged()
        {
            this.ps.OnDocumentChanged(EventArgs.Empty);
        }

        internal void OnCreateException()
        {
            this.state = DocumentState.Empty;
        }

        protected virtual void OnEndDeserializingCore()
        {
            ExceptionHelper.ThrowInvalidOperationException();
        }

        protected virtual void OnEndSerializingCore()
        {
        }

        protected virtual void OnStartDeserializingCore()
        {
            ExceptionHelper.ThrowInvalidOperationException();
        }

        protected void PreparePageSerialization(IList<Page> pages)
        {
            this.CollectPageData(pages);
            this.CreateSerializationObjects();
            foreach (Page page in pages)
            {
                PageBrickEnumerator enumerator2 = new PageBrickEnumerator(page);
                while (enumerator2.MoveNext())
                {
                    this.AddBrickObjectsToCache(enumerator2.CurrentBrick);
                }
            }
            this.bricks.StopCollectSharedObjects();
            this.images.StopCollectSharedObjects();
            this.styles.StopCollectSharedObjects();
        }

        private void ps_MarginsChanged(object sender, MarginsChangeEventArgs e)
        {
            if (!this.IsDisposed)
            {
                this.ps.DelayedAction |= PrintingSystemAction.HandleNewPageSettings;
            }
        }

        private void ps_PageSettingsChanged(object sender, EventArgs e)
        {
            if (!this.IsDisposed && !this.IsCreating)
            {
                this.ps.DelayedAction |= PrintingSystemAction.HandleNewPageSettings;
            }
        }

        private void ps_ScaleFactorChanged(object sender, EventArgs e)
        {
            if (!this.IsDisposed)
            {
                this.ps.DelayedAction |= PrintingSystemAction.HandleNewScaleFactor;
            }
        }

        internal virtual void Serialize(Stream stream, XtraSerializer serializer)
        {
            float[] ranges = new float[] { 1f, 10f };
            this.ps.ProgressReflector.SetProgressRanges(ranges);
            ContinuousExportInfo continuousExportInfo = null;
            this.ps.ProgressReflector.EnsureRangeDecrement(() => continuousExportInfo = this.GetContinuousExportInfo());
            this.SerializeCore(stream, serializer, continuousExportInfo, this.Pages);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SerializeCore(Stream stream, XtraSerializer serializer, ContinuousExportInfo info, IList<Page> pages)
        {
            Document document = this;
            lock (document)
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.Pages.Count)
                    {
                        DocumentSerializationCollection objects = new DocumentSerializationCollection();
                        objects.Add(new DocumentSerializationOptions(this, pages.Count));
                        Predicate<int> predicate = <>c.<>9__102_0;
                        if (<>c.<>9__102_0 == null)
                        {
                            Predicate<int> local1 = <>c.<>9__102_0;
                            predicate = <>c.<>9__102_0 = index => true;
                        }
                        objects.AddRange(pages, predicate);
                        objects.Add(info);
                        this.ps.ProgressReflector.InitializeRange(pages.Count);
                        this.PreparePageSerialization(pages);
                        info.ExecuteExport(new ContinuousExportBrickCollector(this), this.PrintingSystem);
                        try
                        {
                            this.ps.PageRepository = new PageRepository(pages);
                            serializer.SerializeObjects(this, objects, stream, string.Empty, null);
                        }
                        finally
                        {
                            this.ps.PageRepository = null;
                        }
                        break;
                    }
                    Page drawingPage = this.Pages[num];
                    drawingPage.PerformLayout(new PrintingSystemContextWrapper(this.ps, drawingPage));
                    num++;
                }
            }
        }

        public abstract void ShowFromNewPage(Brick brick);
        protected internal abstract void StopPageBuilding();
        public virtual void UpdateBaseOffset()
        {
        }

        public Guid ContentIdentity
        {
            get
            {
                if (this.contentIdentity == Guid.Empty)
                {
                    this.contentIdentity = Guid.NewGuid();
                }
                return this.contentIdentity;
            }
        }

        internal DocumentState State =>
            this.state;

        public bool RightToLeftLayout { get; set; }

        internal bool IsCreated =>
            this.state == DocumentState.Created;

        [Description("Indicates whether or not any changes were made to the Document instance after it was created.")]
        public bool IsModified { get; internal set; }

        [Description("Indicates whether or not the document is still being created.")]
        public virtual bool IsCreating =>
            (this.state == DocumentState.Creating) || (this.state == DocumentState.PostProcessing);

        internal bool IsDisposed =>
            this.isDisposed;

        [Description("Specifies the document name.")]
        public string Name
        {
            get => 
                this.name;
            set
            {
                if (this.name == this.Bookmark)
                {
                    this.Bookmark = value;
                }
                this.ps.ExportOptions.UpdateDefaultFileName(this.name, value);
                this.name = value;
            }
        }

        [Description("Specifies the text of a root bookmark displayed in the Document Map.")]
        public string Bookmark
        {
            get => 
                this.rootBookmark.Text;
            set => 
                this.rootBookmark.Text = value;
        }

        internal bool CorrectImportBrickBounds
        {
            get => 
                this.correctImportBrickBounds;
            set => 
                this.correctImportBrickBounds = value;
        }

        [Description("Provides access to the collection of pages within the current document.")]
        public PageList Pages
        {
            get
            {
                this.fPages ??= this.CreatePageList();
                return this.fPages;
            }
        }

        [Description("Indicates whether or not the current Document instance is locked.")]
        public virtual bool IsLocked =>
            false;

        [Description("For internal use. Specifies the base offset point for the created document.")]
        public virtual PointF BaseOffset
        {
            get => 
                PointF.Empty;
            set
            {
            }
        }

        internal Page FirstPage =>
            this.Pages.First;

        internal Page LastPage =>
            this.Pages.Last;

        [Description("Indicates the total number of pages within a document.")]
        public virtual int PageCount =>
            this.Pages.Count;

        [Description("Provides access to the Printing System for the current document.")]
        public PrintingSystemBase PrintingSystem =>
            this.ps;

        [Description("Provides access to the collection of bookmarks available in the document and displayed in the Document Map of Print Preview.")]
        public IBookmarkNodeCollection BookmarkNodes =>
            this.rootBookmark.Nodes;

        [Description("Specifies the number of virtual pages to fit into one physical page, so their total width is the same as the width of a physical page.")]
        public abstract int AutoFitToPagesWidth { get; set; }

        internal bool CanAutoFitToPagesWidth
        {
            get => 
                this.canAutoFitToPagesWidth;
            set => 
                this.canAutoFitToPagesWidth = value;
        }

        [Description("Specifies the document's scale factor (in fractions of 1).")]
        public abstract float ScaleFactor { get; set; }

        protected internal RectangleF UsefulPageRect =>
            this.ps.PageSettings.UsefulPageRectF;

        protected internal abstract RectangleF PageFootBounds { get; }

        protected internal abstract RectangleF PageHeadBounds { get; }

        protected internal abstract float MinPageHeight { get; }

        protected internal abstract float MinPageWidth { get; }

        public virtual bool IsEmpty =>
            this.PageCount == 0;

        internal DevExpress.XtraPrinting.ProgressReflector ProgressReflector =>
            this.ps.ProgressReflector;

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public virtual bool CanPerformContinuousExport =>
            !this.IsModified;

        internal List<PageDataWithIndices> PageData =>
            this.pageData;

        internal ObjectCache BricksSerializationCache =>
            this.bricks;

        internal ObjectCache StylesSerializationCache =>
            this.styles;

        internal ObjectCache ImagesSerializationCache =>
            this.images;

        internal BookmarkNode RootBookmark =>
            this.rootBookmark;

        [Description("Specifies whether or not the document's page settings can be changed.")]
        public bool CanChangePageSettings
        {
            get => 
                this.canChangePageSettings && !this.IsCreating;
            set => 
                this.canChangePageSettings = value;
        }

        internal virtual bool CanScale =>
            this.CanRecreatePages;

        internal bool CanRecreatePages =>
            (this.Pages.Count > 0) && this.CanChangePageSettings;

        internal DevExpress.XtraPrinting.Native.AvailableExportModes AvailableExportModes
        {
            get
            {
                Converter<object, RtfExportMode> converter = <>c.<>9__140_0;
                if (<>c.<>9__140_0 == null)
                {
                    Converter<object, RtfExportMode> local1 = <>c.<>9__140_0;
                    converter = <>c.<>9__140_0 = obj => (RtfExportMode) obj;
                }
                return new DevExpress.XtraPrinting.Native.AvailableExportModes(ArrayHelper.ConvertAll<object, RtfExportMode>(this.GetAvailableExportModes(typeof(RtfExportMode)), converter), ArrayHelper.ConvertAll<object, DocxExportMode>(this.GetAvailableExportModes(typeof(DocxExportMode)), <>c.<>9__140_1 ??= obj => ((DocxExportMode) obj)), ArrayHelper.ConvertAll<object, HtmlExportMode>(this.GetAvailableExportModes(typeof(HtmlExportMode)), <>c.<>9__140_2 ??= obj => ((HtmlExportMode) obj)), ArrayHelper.ConvertAll<object, ImageExportMode>(this.GetAvailableExportModes(typeof(ImageExportMode)), <>c.<>9__140_3 ??= obj => ((ImageExportMode) obj)), ArrayHelper.ConvertAll<object, XlsExportMode>(this.GetAvailableExportModes(typeof(XlsExportMode)), <>c.<>9__140_4 ??= obj => ((XlsExportMode) obj)), ArrayHelper.ConvertAll<object, XlsxExportMode>(this.GetAvailableExportModes(typeof(XlsxExportMode)), <>c.<>9__140_5 ??= obj => ((XlsxExportMode) obj)));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Document.<>c <>9 = new Document.<>c();
            public static Predicate<int> <>9__102_0;
            public static Predicate<int> <>9__103_0;
            public static Converter<object, RtfExportMode> <>9__140_0;
            public static Converter<object, DocxExportMode> <>9__140_1;
            public static Converter<object, HtmlExportMode> <>9__140_2;
            public static Converter<object, ImageExportMode> <>9__140_3;
            public static Converter<object, XlsExportMode> <>9__140_4;
            public static Converter<object, XlsxExportMode> <>9__140_5;
            public static Action<GroupingManager> <>9__153_0;

            internal void <Clear>b__153_0(GroupingManager groupingManager)
            {
                groupingManager.Clear();
            }

            internal bool <Deserialize>b__103_0(int index) => 
                true;

            internal RtfExportMode <get_AvailableExportModes>b__140_0(object obj) => 
                (RtfExportMode) obj;

            internal DocxExportMode <get_AvailableExportModes>b__140_1(object obj) => 
                (DocxExportMode) obj;

            internal HtmlExportMode <get_AvailableExportModes>b__140_2(object obj) => 
                (HtmlExportMode) obj;

            internal ImageExportMode <get_AvailableExportModes>b__140_3(object obj) => 
                (ImageExportMode) obj;

            internal XlsExportMode <get_AvailableExportModes>b__140_4(object obj) => 
                (XlsExportMode) obj;

            internal XlsxExportMode <get_AvailableExportModes>b__140_5(object obj) => 
                (XlsxExportMode) obj;

            internal bool <SerializeCore>b__102_0(int index) => 
                true;
        }

        protected class ContinuousExportBrickCollector : IBrickExportVisitor
        {
            private Document document;

            public ContinuousExportBrickCollector(Document document)
            {
                this.document = document;
            }

            void IBrickExportVisitor.ExportBrick(double horizontalOffset, double verticalOffset, Brick brick)
            {
                this.document.AddBrickObjectsToCache(brick);
            }
        }
    }
}

