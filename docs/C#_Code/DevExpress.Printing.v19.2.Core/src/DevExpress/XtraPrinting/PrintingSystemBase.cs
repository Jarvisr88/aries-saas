namespace DevExpress.XtraPrinting
{
    using DevExpress.DocumentView;
    using DevExpress.Printing.Core;
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Export.Imaging;
    using DevExpress.XtraPrinting.Export.Pdf;
    using DevExpress.XtraPrinting.Export.Rtf;
    using DevExpress.XtraPrinting.Export.Text;
    using DevExpress.XtraPrinting.Export.Web;
    using DevExpress.XtraPrinting.Export.Xl;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.DrillDown;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class PrintingSystemBase : Component, ISupportInitialize, IPrintingSystem, IPrintingSystemContext, System.IServiceProvider, IDocument, IServiceContainer, IPageRepository, IPageInfoFormatProvider, IMaxPageSizeProvider
    {
        private IList<IPage> innerPages;
        internal const bool ContinuousPageNumberingDefaultValue = true;
        private static string userName = string.Empty;
        protected XtraPageSettingsBase xtraSettings;
        private DevExpress.XtraPrinting.Document fDocument;
        private BrickGraphics brickGraphics;
        private GdiHashtable gdi;
        private int initializingCounter;
        private bool cancelPending;
        private bool locked;
        private bool continuousPageNumbering;
        private LinkBase activeLink;
        private BrickPagePairCollection markedPairs;
        private DevExpress.XtraPrinting.Drawing.Watermark watermark;
        private bool showMarginsWarning;
        private bool showPrintStatusDialog;
        private DevExpress.XtraPrinting.ExportOptions exportOptions;
        private bool raisingAfterChange;
        private IObjectContainer[] containers;
        protected DevExpress.XtraPrinting.ProgressReflector fProgressReflector;
        private DevExpress.XtraPrinting.ProgressReflector subscribedProgressReflector;
        protected DevExpress.XtraPrinting.ProgressReflector fFakedReflector;
        private bool isMetric;
        private bool isDisposed;
        private int disposeCount;
        private BrickPaintBase brickPainter;
        private IPrintingSystemExtenderBase extender;
        private NonSafeServiceContainer defaultServiceContainer;
        private ServiceContainer serviceContainer;
        private ImageItemCollection imageResources;
        private readonly Dictionary<int, Measurer> measurers;
        private EditingFieldCollection editingFields;
        private static readonly object AfterMarginsChangeEvent = new object();
        private static readonly object BeforeMarginsChangeEvent = new object();
        private static readonly object BeforePagePaintEvent = new object();
        private static readonly object AfterPagePaintEvent = new object();
        private static readonly object AfterPagePrintEvent = new object();
        private static readonly object AfterBandPrintEvent = new object();
        private static readonly object BeforeChangeEvent = new object();
        private static readonly object AfterChangeEvent = new object();
        private static readonly object PageSettingsChangedEvent = new object();
        private static readonly object ScaleFactorChangedEvent = new object();
        private static readonly object StartPrintEvent = new object();
        private static readonly object EndPrintEvent = new object();
        private static readonly object PrintProgressEvent = new object();
        private static readonly object BeforeBuildPagesEvent = new object();
        private static readonly object DocumentChangedEvent = new object();
        private static readonly object FillEmptySpaceEvent = new object();
        private static readonly object AfterBuildPagesEvent = new object();
        private static readonly object CreateDocumentExceptionEvent = new object();
        private static readonly object PageInsertCompleteEvent = new object();
        private static readonly object XlSheetCreatedEvent = new object();
        private string format;
        private int startPageNumber;
        private IPageRepository pageRepository;
        private DevExpress.XtraPrinting.BrickExporters.ExportersFactory exportersFactory;
        private RichTextBox richTextBoxForDrawing;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event PageEventHandler AfterBandPrint
        {
            add
            {
                base.Events.AddHandler(AfterBandPrintEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(AfterBandPrintEvent, value);
            }
        }

        public event EventHandler AfterBuildPages
        {
            add
            {
                base.Events.AddHandler(AfterBuildPagesEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(AfterBuildPagesEvent, value);
            }
        }

        [Category("Property Changed")]
        public event ChangeEventHandler AfterChange
        {
            add
            {
                base.Events.AddHandler(AfterChangeEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(AfterChangeEvent, value);
            }
        }

        [Category("Property Changed")]
        public event MarginsChangeEventHandler AfterMarginsChange
        {
            add
            {
                base.Events.AddHandler(AfterMarginsChangeEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(AfterMarginsChangeEvent, value);
            }
        }

        [Category("Printing")]
        public event PagePaintEventHandler AfterPagePaint
        {
            add
            {
                base.Events.AddHandler(AfterPagePaintEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(AfterPagePaintEvent, value);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event PageEventHandler AfterPagePrint
        {
            add
            {
                base.Events.AddHandler(AfterPagePrintEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(AfterPagePrintEvent, value);
            }
        }

        public event EventHandler BeforeBuildPages
        {
            add
            {
                base.Events.AddHandler(BeforeBuildPagesEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(BeforeBuildPagesEvent, value);
            }
        }

        [Category("Property Changed")]
        public event ChangeEventHandler BeforeChange
        {
            add
            {
                base.Events.AddHandler(BeforeChangeEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(BeforeChangeEvent, value);
            }
        }

        [Category("Property Changed")]
        public event MarginsChangeEventHandler BeforeMarginsChange
        {
            add
            {
                base.Events.AddHandler(BeforeMarginsChangeEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(BeforeMarginsChangeEvent, value);
            }
        }

        [Category("Printing")]
        public event PagePaintEventHandler BeforePagePaint
        {
            add
            {
                base.Events.AddHandler(BeforePagePaintEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(BeforePagePaintEvent, value);
            }
        }

        [Category("Document Creation")]
        public event ExceptionEventHandler CreateDocumentException
        {
            add
            {
                base.Events.AddHandler(CreateDocumentExceptionEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateDocumentExceptionEvent, value);
            }
        }

        event EventHandler IDocument.DocumentChanged
        {
            add
            {
                this.DocumentChanged += value;
            }
            remove
            {
                this.DocumentChanged -= value;
            }
        }

        event EventHandler IDocument.PageBackgrChanged
        {
            add
            {
                this.pageBackgrChanged += value;
            }
            remove
            {
                this.pageBackgrChanged -= value;
            }
        }

        internal event EventHandler DocumentChanged
        {
            add
            {
                base.Events.AddHandler(DocumentChangedEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(DocumentChangedEvent, value);
            }
        }

        [Category("Property Changed")]
        public event EventHandler<EditingFieldEventArgs> EditingFieldChanged;

        internal event EventHandler<EditingFieldEventArgs> EditingFieldReadOnlyChanged;

        [Category("Printing")]
        public event EventHandler EndPrint
        {
            add
            {
                base.Events.AddHandler(EndPrintEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(EndPrintEvent, value);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event EmptySpaceEventHandler FillEmptySpace
        {
            add
            {
                base.Events.AddHandler(FillEmptySpaceEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(FillEmptySpaceEvent, value);
            }
        }

        private event EventHandler pageBackgrChanged;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event PageInsertCompleteEventHandler PageInsertComplete
        {
            add
            {
                base.Events.AddHandler(PageInsertCompleteEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(PageInsertCompleteEvent, value);
            }
        }

        [Category("Property Changed")]
        public event EventHandler PageSettingsChanged
        {
            add
            {
                base.Events.AddHandler(PageSettingsChangedEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(PageSettingsChangedEvent, value);
            }
        }

        [Category("Printing")]
        public event PrintProgressEventHandler PrintProgress
        {
            add
            {
                base.Events.AddHandler(PrintProgressEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(PrintProgressEvent, value);
            }
        }

        [Category("Property Changed")]
        public event EventHandler ScaleFactorChanged
        {
            add
            {
                base.Events.AddHandler(ScaleFactorChangedEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(ScaleFactorChangedEvent, value);
            }
        }

        [Category("Printing")]
        public event PrintDocumentEventHandler StartPrint
        {
            add
            {
                base.Events.AddHandler(StartPrintEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(StartPrintEvent, value);
            }
        }

        [Category("Printing")]
        public event XlSheetCreatedEventHandler XlSheetCreated
        {
            add
            {
                base.Events.AddHandler(XlSheetCreatedEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(XlSheetCreatedEvent, value);
            }
        }

        static PrintingSystemBase()
        {
            PrintingSystemXmlSerializer.RegisterConverter(PaddingInfoConverter.Instance);
            PrintingSystemXmlSerializer.RegisterConverter(BrickStringFormatConverter.Instance);
            PrintingSystemXmlSerializer.RegisterConverter(RectangleDFConverter.Instance);
            PrintingSystemXmlSerializer.RegisterConverter(ImageSourceConverter.Instance);
            DrillDownKey.EnsureStaticConstructor();
        }

        public PrintingSystemBase() : this(null)
        {
        }

        public PrintingSystemBase(IContainer container) : this(container, new DevExpress.XtraPrinting.ExportOptions())
        {
        }

        protected PrintingSystemBase(IContainer container, DevExpress.XtraPrinting.ExportOptions exportOptions)
        {
            this.continuousPageNumbering = true;
            this.markedPairs = new BrickPagePairCollection();
            this.showMarginsWarning = true;
            this.showPrintStatusDialog = true;
            this.fFakedReflector = new DevExpress.XtraPrinting.ProgressReflector();
            this.isMetric = RegionInfo.CurrentRegion.IsMetric;
            this.imageResources = new ImageItemCollection();
            this.measurers = new Dictionary<int, Measurer>();
            this.format = PageInfoTextBrick.GetPageNumberFormat(null, PageInfo.Number);
            this.startPageNumber = 1;
            if (exportOptions == null)
            {
                throw new ArgumentNullException("exportOptions");
            }
            this.exportOptions = exportOptions;
            if (container != null)
            {
                container.Add(this);
            }
            this.brickGraphics = new BrickGraphics(this);
            this.gdi = new GdiHashtable();
            this.containers = new IObjectContainer[] { new StylesContainer(), new StylesContainer(), new ImagesContainer(), new ImagesCache(), new FontCacheContainer() };
            this.defaultServiceContainer = new NonSafeServiceContainer();
            Func<object> creator = <>c.<>9__69_0;
            if (<>c.<>9__69_0 == null)
            {
                Func<object> local1 = <>c.<>9__69_0;
                creator = <>c.<>9__69_0 = () => new GdiPlusMeasurer();
            }
            this.defaultServiceContainer.AddNonSafeService(typeof(Measurer), creator);
            ServiceCreatorCallback callback = <>c.<>9__69_1;
            if (<>c.<>9__69_1 == null)
            {
                ServiceCreatorCallback local2 = <>c.<>9__69_1;
                callback = <>c.<>9__69_1 = (ServiceCreatorCallback) ((c, t) => new GdiPlusGraphicsModifier());
            }
            this.defaultServiceContainer.AddService(typeof(GraphicsModifier), callback);
            ServiceCreatorCallback callback2 = <>c.<>9__69_2;
            if (<>c.<>9__69_2 == null)
            {
                ServiceCreatorCallback local3 = <>c.<>9__69_2;
                callback2 = <>c.<>9__69_2 = (ServiceCreatorCallback) ((c, t) => new PageContentService());
            }
            this.defaultServiceContainer.AddService(typeof(IPageContentService), callback2);
            ServiceCreatorCallback callback3 = <>c.<>9__69_3;
            if (<>c.<>9__69_3 == null)
            {
                ServiceCreatorCallback local4 = <>c.<>9__69_3;
                callback3 = <>c.<>9__69_3 = (ServiceCreatorCallback) ((c, t) => new MergeBrickHelper());
            }
            this.defaultServiceContainer.AddService(typeof(MergeBrickHelper), callback3);
            this.defaultServiceContainer.AddService(typeof(IFontCacheService), this.Fonts);
            this.defaultServiceContainer.AddService(typeof(IPageSettingsService), new PageSettingsService(this));
            this.defaultServiceContainer.AddService(typeof(IPageDataService), new PageDataService());
            this.serviceContainer = new ServiceContainer(this.defaultServiceContainer);
        }

        public void AddCommandHandler(ICommandHandler handler)
        {
            this.Extender.AddCommandHandler(handler);
        }

        public void AddService(System.Type serviceType, ServiceCreatorCallback callback)
        {
            this.serviceContainer.AddService(serviceType, callback);
        }

        public void AddService(System.Type serviceType, object serviceInstance)
        {
            if (this.serviceContainer != null)
            {
                this.serviceContainer.AddService(serviceType, serviceInstance);
            }
        }

        public void AddService(System.Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            this.serviceContainer.AddService(serviceType, callback, promote);
        }

        public void AddService(System.Type serviceType, object serviceInstance, bool promote)
        {
            this.serviceContainer.AddService(serviceType, serviceInstance, promote);
        }

        protected virtual void AfterDrawPagesCore(object syncObj, int[] pageIndices)
        {
        }

        private void AfterExport()
        {
            this.RemoveService(typeof(IBrickPublisher), true);
            this.GarbageStyles.Clear();
            this.GarbageImages.Clear();
        }

        private void AfterPdfExport()
        {
            this.AfterExport();
            this.RemoveService(typeof(IDeferredBorderPainter), true);
        }

        protected virtual void BeforeDrawPagesCore(object syncObj)
        {
        }

        private void BeforeExport()
        {
            this.RemoveService(typeof(IBrickPublisher), true);
            this.AddService(typeof(IBrickPublisher), new ExportBrickPublisher(), true);
        }

        private void BeforePdfExport()
        {
            this.BeforeExport();
            this.AddService(typeof(IDeferredBorderPainter), new DeferredBorderPainter(), true);
        }

        public void Begin()
        {
            ((ISupportInitialize) this).BeginInit();
            this.ClearContent();
            this.FinalizeLink(this.activeLink);
            this.brickGraphics.Init();
            this.brickGraphics.DefaultBrickStyle = BrickStyle.CreateDefault();
            this.Document.Begin();
            this.Extender.OnBeginCreateDocument();
        }

        public void BeginSubreport(PointF offset)
        {
            this.BeginSubreportInternal(null, offset);
        }

        internal void BeginSubreportInternal(DocumentBand docBand, PointF offset)
        {
            this.Document.BeginReport(docBand, GraphicsUnitConverter.Convert(offset, this.Graph.Dpi, (float) 300f));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Cancel()
        {
            this.cancelPending = true;
        }

        private static void CheckExportMode(object currentMode, object prohibitedMode, PreviewStringId id)
        {
            if (Equals(currentMode, prohibitedMode))
            {
                string message = PreviewLocalizer.GetString(id);
                if (message != string.Empty)
                {
                    ExceptionHelper.ThrowInvalidOperationException(message);
                }
            }
        }

        public void ClearContent()
        {
            if (this.fDocument != null)
            {
                this.fDocument.ClearContent();
            }
            this.DisposeImageResources();
            this.ClearObjectContainers();
        }

        internal void ClearEditingFields()
        {
            if (this.editingFields != null)
            {
                this.editingFields.Clear();
            }
        }

        internal void ClearMarkedBricks()
        {
            this.markedPairs.Clear();
        }

        private void ClearObjectContainers()
        {
            foreach (IObjectContainer container in this.containers)
            {
                container.Clear();
            }
        }

        private void ClearWaterMark()
        {
            if (this.watermark != null)
            {
                this.watermark.Dispose();
                this.watermark = null;
            }
        }

        private static void CloseStream(bool compressed, Stream stream)
        {
            if (compressed)
            {
                stream.Close();
            }
        }

        public virtual IBrick CreateBrick(string typeName) => 
            (typeName != "TextBrick") ? ((IBrick) Activator.CreateInstance(null, "DevExpress.XtraPrinting." + typeName).Unwrap()) : new TextBrick();

        protected virtual BrickPaintBase CreateBrickPaint() => 
            new BrickPaint(this.Gdi);

        protected virtual DevExpress.XtraPrinting.Native.PrintingDocument CreateDocument() => 
            new PSLinkDocument(this);

        private IDocxExportProvider CreateDocxExportProvider(Stream stream, DocxExportOptions options)
        {
            string str = (options.ExportMode == DocxExportMode.SingleFilePageByPage) ? "DocxPageExportProvider" : "DocxExportProvider";
            System.Type type = System.Type.GetType($"{"DevExpress.XtraPrinting.Export.Docx"}.{str}, {"DevExpress.RichEdit.v19.2.Export, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"}");
            if (type == null)
            {
                return null;
            }
            ProgressMaster master = ((ProgressMaster) this.serviceContainer.GetService(typeof(ProgressMaster))) ?? new ProgressMaster(this.ProgressReflector, options);
            object[] args = new object[] { stream, this.Document, options, master };
            return (Activator.CreateInstance(type, args) as IDocxExportProvider);
        }

        internal static ChangeEventArgs CreateEventArgs(string eventName, object[,] parameters)
        {
            ChangeEventArgs args = new ChangeEventArgs(eventName);
            if (parameters != null)
            {
                int length = parameters.GetLength(0);
                for (int i = 0; i < length; i++)
                {
                    args.Add((string) parameters[i, 0], parameters[i, 1]);
                }
            }
            return args;
        }

        protected virtual IPrintingSystemExtenderBase CreateExtender() => 
            new PrintingSystemExtenderPrint(this);

        private static Stream CreateFileStreamForRead(string filePath) => 
            new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        private HtmlDocumentBuilder CreateHtmlDocumentBuilder(HtmlExportOptions options) => 
            new HtmlDocumentBuilder(options, this.brickGraphics.PageBackColor);

        protected internal HtmlTableBuilder CreateHtmlTableBuilder() => 
            new HtmlTableBuilder();

        internal static XmlXtraSerializer CreateIndependentPagesSerializer() => 
            new IndependentPagesPrintingSystemSerializer();

        internal static XmlXtraSerializer CreateNormalSerializer() => 
            new PrintingSystemXmlSerializer();

        public Page CreatePage() => 
            this.CreatePSPage();

        protected virtual XtraPageSettingsBase CreatePageSettings() => 
            new XtraPageSettingsBase(this);

        internal PSPage CreatePSPage() => 
            new PSPage(new ReadonlyPageData(this.PageSettings.Data));

        protected RtfDocumentProviderBase CreateRtfExportProvider(Stream stream, RtfExportOptions options) => 
            (options.ExportMode != RtfExportMode.SingleFilePageByPage) ? ((RtfDocumentProviderBase) new RtfExportProvider(stream, this.Document, options)) : ((RtfDocumentProviderBase) new RtfPageExportProvider(stream, this.Document, options));

        private void CreateTextDocument(Stream stream, TextExportOptionsBase options, bool insertSpaces)
        {
            float[] ranges = new float[] { 1f, 1f };
            this.ProgressReflector.SetProgressRanges(ranges);
            StreamWriter writer = new StreamWriter(stream, options.Encoding);
            this.BeforeExport();
            try
            {
                this.ProgressReflector.InitializeRange(5);
                TextExportContext exportContext = new TextExportContext(this);
                LayoutControlCollection layoutControls = null;
                this.ProgressReflector.EnsureRangeDecrement(() => layoutControls = this.CreateTextLayoutBuilder(exportContext, options).BuildLayoutControls());
                DevExpress.XtraPrinting.ProgressReflector progressReflector = this.ProgressReflector;
                progressReflector.RangeValue++;
                TextDocument.CreateDocument(layoutControls, this.Document, writer, options, insertSpaces);
            }
            finally
            {
                this.ProgressReflector.MaximizeRange();
                writer.Flush();
                this.AfterExport();
            }
        }

        protected TextLayoutBuilder CreateTextLayoutBuilder(TextExportContext exportContext, TextExportOptionsBase options)
        {
            CsvExportOptions options2 = options as CsvExportOptions;
            bool flag = options2 != null;
            return new TextLayoutBuilder(this.Document, exportContext, options.GetActualSeparator(), options.QuoteStringsWithSeparators, options.TextExportMode, !flag, flag && options2.RequireEncodeExecutableContent);
        }

        void IDocument.AfterDrawPages(object syncObj, int[] pageIndices)
        {
            this.AfterDrawPagesCore(syncObj, pageIndices);
        }

        void IDocument.BeforeDrawPages(object syncObj)
        {
            this.BeforeDrawPagesCore(syncObj);
        }

        bool IPageRepository.TryGetPageByID(long id, out Page page, out int index) => 
            this.PageRepository.TryGetPageByID(id, out page, out index);

        bool IPageRepository.TryGetPageByIndex(int index, out Page page) => 
            this.PageRepository.TryGetPageByIndex(index, out page);

        IImageBrick IPrintingSystem.CreateImageBrick() => 
            new ImageBrick();

        IPanelBrick IPrintingSystem.CreatePanelBrick() => 
            new PanelBrick();

        IProgressBarBrick IPrintingSystem.CreateProgressBarBrick() => 
            new ProgressBarBrick();

        IRichTextBrick IPrintingSystem.CreateRichTextBrick() => 
            BrickFactory.CreateBrick("RichText") as IRichTextBrick;

        ITextBrick IPrintingSystem.CreateTextBrick() => 
            new TextBrick();

        ITrackBarBrick IPrintingSystem.CreateTrackBarBrick() => 
            new TrackBarBrick();

        void IPrintingSystem.SetCommandVisibility(PrintingSystemCommand command, bool visible)
        {
            this.SetCommandVisibility(command, visible);
        }

        bool IPrintingSystemContext.CanPublish(Brick brick)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                base.Dispose(disposing);
            }
            else
            {
                this.UnsubscribeEvents(this.subscribedProgressReflector);
                this.subscribedProgressReflector = null;
                this.StartDispose();
                if (this.gdi != null)
                {
                    this.gdi.Dispose();
                    this.gdi = null;
                }
                this.DisposeDocument();
                this.markedPairs.Clear();
                this.DisposeRichTextBoxForDrawing();
                this.ClearWaterMark();
                base.Dispose(disposing);
                if (this.brickGraphics != null)
                {
                    ((IDisposable) this.brickGraphics).Dispose();
                    this.brickGraphics = null;
                }
                if (this.xtraSettings != null)
                {
                    this.xtraSettings.Dispose();
                    this.xtraSettings = null;
                }
                if (this.serviceContainer != null)
                {
                    this.serviceContainer.Dispose();
                    this.serviceContainer = null;
                }
                if (this.defaultServiceContainer != null)
                {
                    this.defaultServiceContainer.Dispose();
                    this.defaultServiceContainer = null;
                }
                if (this.extender != null)
                {
                    this.extender.Clear();
                    this.extender = null;
                }
                this.EndDispose();
            }
        }

        private void DisposeDocucmentCore()
        {
            if (!this.DocumentIsDisposed(this.fDocument))
            {
                this.fDocument.Dispose();
                this.fDocument = null;
            }
            if (this.innerPages != null)
            {
                this.innerPages.Clear();
                this.innerPages = null;
            }
        }

        private void DisposeDocument()
        {
            this.DisposeDocucmentCore();
            this.DisposeImageResources();
            this.ClearObjectContainers();
        }

        private void DisposeImageResources()
        {
            this.ImageResources.Dispose();
        }

        private void DisposeRichTextBoxForDrawing()
        {
            if (this.richTextBoxForDrawing != null)
            {
                this.richTextBoxForDrawing.Dispose();
                this.richTextBoxForDrawing = null;
            }
        }

        private bool DocumentIsDisposed(DevExpress.XtraPrinting.Document doc) => 
            (doc == null) || doc.IsDisposed;

        [Obsolete("Use the SetCommandVisibility(PrintingSystemCommand command, CommandVisibility visibility) method instead"), DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public void EnableCommand(PrintingSystemCommand command, bool enabled)
        {
            throw new MethodAccessException();
        }

        internal void EnableCommandInternal(PrintingSystemCommand command, bool enabled)
        {
            this.Extender.EnableCommand(command, enabled);
        }

        public void End()
        {
            this.End(false);
        }

        internal void End(LinkBase link)
        {
            this.End(link, false);
        }

        public void End(bool buildForInstantPreview)
        {
            this.brickGraphics.Clear();
            this.Document.End(buildForInstantPreview);
            ((ISupportInitialize) this).EndInit();
            this.ResetDocumentIfCancel();
        }

        internal void End(LinkBase link, bool buildPagesInBackground)
        {
            this.activeLink = link;
            this.End(buildPagesInBackground);
        }

        protected void EndDispose()
        {
            this.disposeCount--;
            this.isDisposed = this.disposeCount <= 0;
        }

        public void EndSubreport()
        {
            this.Document.EndReport();
        }

        protected internal virtual void EnsureBrickOnPage(BrickPagePair pair, Action<BrickPagePair> callback)
        {
            callback(pair);
        }

        public void ExecCommand(PrintingSystemCommand command)
        {
            this.ExecCommand(command, new object[0]);
        }

        public void ExecCommand(PrintingSystemCommand command, object[] args)
        {
            this.Extender.ExecCommand(command, args);
        }

        private void ExecDelayedAction()
        {
            if ((this.DelayedAction & PrintingSystemAction.CreateDocument) > PrintingSystemAction.None)
            {
                if (this.activeLink != null)
                {
                    this.activeLink.CreateDocument();
                }
            }
            else if ((this.DelayedAction & PrintingSystemAction.HandleNewPageSettings) > PrintingSystemAction.None)
            {
                this.Document.HandleNewPageSettings();
            }
            else if ((this.DelayedAction & PrintingSystemAction.HandleNewScaleFactor) > PrintingSystemAction.None)
            {
                this.Document.HandleNewScaleFactor();
            }
            this.DelayedAction = PrintingSystemAction.None;
        }

        public void ExportToCsv(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToCsv(stream, this.exportOptions.Csv);
        }

        public void ExportToCsv(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToCsv(filePath, this.exportOptions.Csv);
        }

        public virtual void ExportToCsv(Stream stream, CsvExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.BeforeExport();
            try
            {
                this.CreateTextDocument(stream, options, false);
            }
            finally
            {
                this.AfterExport();
            }
        }

        public void ExportToCsv(string filePath, CsvExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            FileExportHelper.Do(filePath, stream => this.ExportToCsv(stream, options));
        }

        public void ExportToDocx(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToDocx(stream, this.exportOptions.Docx);
        }

        public void ExportToDocx(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToDocx(filePath, this.exportOptions.Docx);
        }

        public virtual void ExportToDocx(Stream stream, DocxExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.BeforeExport();
            try
            {
                IDocxExportProvider provider = this.CreateDocxExportProvider(stream, options);
                if (provider == null)
                {
                    throw new Exception($"Cannot export to DOCX because the following assembly or one of its dependencies could not be loaded: 
DevExpress.RichEdit.v{"19.2"}.Export.dll.");
                }
                if ((options.ExportMode == DocxExportMode.SingleFile) && !this.DocumentIsDeserialized)
                {
                    float[] ranges = new float[] { 1f, 4f };
                    this.ProgressReflector.SetProgressRanges(ranges);
                }
                provider.CreateDocument();
            }
            finally
            {
                this.AfterExport();
            }
        }

        public void ExportToDocx(string filePath, DocxExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            FileExportHelper.Do(filePath, stream => this.ExportToDocx(stream, options));
        }

        private void ExportToExcel(Stream stream, XlExportOptionsBase options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.BeforeExport();
            XlExportContext exportContext = new XlExportContext(this, options);
            try
            {
                new ExcelExportProvider(stream, exportContext, ((ProgressMaster) this.serviceContainer.GetService(typeof(ProgressMaster))) ?? new ProgressMaster(this.ProgressReflector, options)).CreateDocument(this.Document);
            }
            finally
            {
                exportContext.Clear();
                this.AfterExport();
            }
        }

        public void ExportToHtml(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToHtml(stream, this.exportOptions.Html);
        }

        public void ExportToHtml(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToHtml(filePath, this.exportOptions.Html);
        }

        public virtual void ExportToHtml(Stream stream, HtmlExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            CheckExportMode(options.ExportMode, HtmlExportMode.DifferentFiles, PreviewStringId.Msg_NoDifferentFilesInStream);
            HtmlDocumentBuilder builder = this.CreateHtmlDocumentBuilder(options);
            IImageRepository imageRepository = builder.CreateImageRepository(stream);
            this.BeforeExport();
            try
            {
                builder.CreateDocument(this.Document, stream, imageRepository);
            }
            finally
            {
                imageRepository.Dispose();
                this.AfterExport();
            }
        }

        public void ExportToHtml(string filePath, HtmlExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExportToHtmlInternal(filePath, options);
        }

        internal string[] ExportToHtmlInternal(string filePath, HtmlExportOptions options)
        {
            this.exportOptions.Html.ExportMode = options.ExportMode;
            if (options.ExportMode == HtmlExportMode.DifferentFiles)
            {
                string[] strArray;
                IImageRepository commonRepository = HtmlDocumentBuilder.CreatePSImageRepository(filePath, options.EmbedImagesInHTML);
                HtmlExportOptions tempOptions = (HtmlExportOptions) ExportOptionsHelper.CloneOptions(options);
                tempOptions.ExportMode = HtmlExportMode.SingleFilePageByPage;
                PagesExportHelper helper = new PagesExportHelper(this, tempOptions);
                this.BeforeExport();
                try
                {
                    strArray = helper.Execute(2 * helper.PageCount, filePath, delegate (Stream stream) {
                        tempOptions.Title = PagesExportHelper.GetStringWithPageIndex(options.Title, tempOptions.PageRange, this.PageCount);
                        this.CreateHtmlDocumentBuilder(tempOptions).CreateDocument(this.Document, stream, commonRepository);
                    });
                }
                finally
                {
                    commonRepository.Dispose();
                    this.AfterExport();
                }
                return strArray;
            }
            if (options.ExportMode == HtmlExportMode.SingleFilePageByPage)
            {
                int[] pageIndices = options.GetPageIndices(this.PageCount);
                this.ProgressReflector.Do(2 * pageIndices.Length, delegate {
                    Action1<Stream> <>9__2;
                    Action1<Stream> action = <>9__2;
                    if (<>9__2 == null)
                    {
                        Action1<Stream> local1 = <>9__2;
                        action = <>9__2 = stream => this.ExportToHtml(stream, options);
                    }
                    FileExportHelper.Do(filePath, action);
                });
            }
            else
            {
                float[] singleArray3;
                if (this.DocumentIsDeserialized)
                {
                    singleArray3 = new float[] { 1f };
                }
                else
                {
                    singleArray3 = new float[] { 1f, 1f };
                }
                float[] ranges = singleArray3;
                this.ProgressReflector.SetProgressRanges(ranges);
                FileExportHelper.Do(filePath, delegate (Stream stream) {
                    this.ExportToHtml(stream, options);
                });
            }
            if (!File.Exists(filePath))
            {
                return new string[0];
            }
            return new string[] { filePath };
        }

        public void ExportToImage(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToImage(stream, this.exportOptions.Image);
        }

        public void ExportToImage(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToImage(filePath, this.exportOptions.Image);
        }

        public virtual void ExportToImage(Stream stream, ImageExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            CheckExportMode(options.ExportMode, ImageExportMode.DifferentFiles, PreviewStringId.Msg_NoDifferentFilesInStream);
            if (this.exportOptions.Image.ExportMode == ImageExportMode.SingleFilePageByPage)
            {
                this.ProgressReflector.InitializeRange(ExportOptionsHelper.GetPageIndices(options, this.Document.PageCount).Length);
            }
            ImageDocumentBuilderBase base2 = (options.ExportMode == ImageExportMode.SingleFile) ? ((ImageDocumentBuilderBase) new ImageSinglePageDocumentBuilder(this, options)) : ((ImageDocumentBuilderBase) new ImageSinglePageByPageDocumentBuilder(this, options));
            this.BeforeExport();
            try
            {
                base2.CreateDocument(stream);
            }
            finally
            {
                this.AfterExport();
            }
        }

        public virtual void ExportToImage(Stream stream, ImageFormat format)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(format, "format");
            this.ExportToImage(stream, ExportOptionsHelper.ChangeOldImageProperties(this.exportOptions.Image, format));
        }

        public void ExportToImage(string filePath, ImageExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExportToImageInternal(filePath, options);
        }

        public void ExportToImage(string filePath, ImageFormat format)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(format, "format");
            this.ExportToImage(filePath, ExportOptionsHelper.ChangeOldImageProperties(this.exportOptions.Image, format));
        }

        internal string[] ExportToImageInternal(string filePath, ImageExportOptions options)
        {
            this.exportOptions.Image.ExportMode = options.ExportMode;
            if (options.ExportMode == ImageExportMode.DifferentFiles)
            {
                ImageExportOptions tempOptions = (ImageExportOptions) ExportOptionsHelper.CloneOptions(options);
                tempOptions.ExportMode = ImageExportMode.SingleFilePageByPage;
                PagesExportHelper helper = new PagesExportHelper(this, tempOptions);
                return helper.Execute(helper.PageCount, filePath, delegate (Stream stream) {
                    this.ExportToImage(stream, tempOptions);
                });
            }
            try
            {
                FileExportHelper.Do(filePath, delegate (Stream stream) {
                    this.ExportToImage(stream, options);
                });
            }
            finally
            {
                this.ProgressReflector.MaximizeRange();
            }
            if (!File.Exists(filePath))
            {
                return new string[0];
            }
            return new string[] { filePath };
        }

        public AlternateView ExportToMail() => 
            this.ExportToMail(this.exportOptions.MailMessage);

        public AlternateView ExportToMail(MailMessageExportOptions options)
        {
            AlternateView view;
            Guard.ArgumentNotNull(options, "options");
            CheckExportMode(options.ExportMode, HtmlExportMode.DifferentFiles, PreviewStringId.Msg_NoDifferentFilesInStream);
            MailDocumentBuilder builder = new MailDocumentBuilder(options);
            IImageRepository imageRepository = builder.CreateImageRepository(null);
            this.BeforeExport();
            try
            {
                view = builder.CreateDocument(this.Document, imageRepository);
            }
            finally
            {
                imageRepository.Dispose();
                this.AfterExport();
            }
            return view;
        }

        public MailMessage ExportToMail(string from, string to, string subject)
        {
            MailMessage message = new MailMessage(from, to, subject, string.Empty);
            message.AlternateViews.Add(this.ExportToMail(this.exportOptions.MailMessage));
            return message;
        }

        public MailMessage ExportToMail(MailMessageExportOptions options, string from, string to, string subject)
        {
            MailMessage message = new MailMessage(from, to, subject, string.Empty);
            message.AlternateViews.Add(this.ExportToMail(options));
            return message;
        }

        public virtual void ExportToMht(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToMht(stream, this.exportOptions.Mht);
        }

        public void ExportToMht(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToMht(filePath, this.exportOptions.Mht);
        }

        public virtual void ExportToMht(Stream stream, MhtExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            CheckExportMode(options.ExportMode, HtmlExportMode.DifferentFiles, PreviewStringId.Msg_NoDifferentFilesInStream);
            MhtDocumentBuilder builder = new MhtDocumentBuilder(options, this.brickGraphics.PageBackColor);
            IImageRepository imageRepository = builder.CreateImageRepository(stream);
            this.BeforeExport();
            try
            {
                builder.CreateDocument(this.Document, stream, imageRepository);
            }
            finally
            {
                imageRepository.Dispose();
                this.AfterExport();
            }
        }

        public void ExportToMht(string filePath, MhtExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExportToMhtInternal(filePath, options);
        }

        internal string[] ExportToMhtInternal(string filePath, MhtExportOptions options)
        {
            this.exportOptions.Mht.ExportMode = options.ExportMode;
            if (options.ExportMode == HtmlExportMode.DifferentFiles)
            {
                MhtExportOptions tempOptions = (MhtExportOptions) ExportOptionsHelper.CloneOptions(options);
                tempOptions.ExportMode = HtmlExportMode.SingleFilePageByPage;
                PagesExportHelper helper = new PagesExportHelper(this, tempOptions);
                return helper.Execute(2 * helper.PageCount, filePath, delegate (Stream stream) {
                    tempOptions.Title = PagesExportHelper.GetStringWithPageIndex(options.Title, tempOptions.PageRange, this.PageCount);
                    this.ExportToMht(stream, tempOptions);
                });
            }
            if (options.ExportMode == HtmlExportMode.SingleFilePageByPage)
            {
                int[] pageIndices = options.GetPageIndices(this.PageCount);
                this.ProgressReflector.Do(2 * pageIndices.Length, delegate {
                    Action1<Stream> <>9__2;
                    Action1<Stream> action = <>9__2;
                    if (<>9__2 == null)
                    {
                        Action1<Stream> local1 = <>9__2;
                        action = <>9__2 = stream => this.ExportToMht(stream, options);
                    }
                    FileExportHelper.Do(filePath, action);
                });
            }
            else
            {
                float[] ranges = new float[] { 1f, 1f };
                this.ProgressReflector.SetProgressRanges(ranges);
                this.ProgressReflector.Do(2, delegate {
                    Action1<Stream> <>9__4;
                    Action1<Stream> action = <>9__4;
                    if (<>9__4 == null)
                    {
                        Action1<Stream> local1 = <>9__4;
                        action = <>9__4 = stream => this.ExportToMht(stream, options);
                    }
                    FileExportHelper.Do(filePath, action);
                });
            }
            if (!File.Exists(filePath))
            {
                return new string[0];
            }
            return new string[] { filePath };
        }

        public virtual void ExportToPdf(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToPdf(stream, this.ExportOptions.Pdf);
        }

        public void ExportToPdf(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToPdf(filePath, this.ExportOptions.Pdf);
        }

        public virtual void ExportToPdf(Stream stream, PdfExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            ICustomExportToPdfService service = ((System.IServiceProvider) this).GetService(typeof(ICustomExportToPdfService)) as ICustomExportToPdfService;
            if (service != null)
            {
                service.ExportToPdf(stream, this.Document, options);
            }
            else
            {
                this.BeforePdfExport();
                try
                {
                    IStreamingDocument document = this.Document as IStreamingDocument;
                    if ((document != null) && (document.BuiltPageCount == 0))
                    {
                        PdfDocumentBuilder.CreateDocument(stream, document, options);
                    }
                    else
                    {
                        PdfDocumentBuilder.CreateDocument(stream, this.Document, options, this.Document is PartiallyDeserializedDocument);
                    }
                }
                finally
                {
                    this.AfterPdfExport();
                }
            }
        }

        public void ExportToPdf(string filePath, PdfExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            FileExportHelper.Do(filePath, stream => this.ExportToPdf(stream, options));
        }

        public void ExportToRtf(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToRtf(stream, this.exportOptions.Rtf);
        }

        public void ExportToRtf(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToRtf(filePath, this.exportOptions.Rtf);
        }

        public virtual void ExportToRtf(Stream stream, RtfExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.BeforeExport();
            try
            {
                this.CreateRtfExportProvider(stream, options).CreateDocument();
            }
            finally
            {
                this.AfterExport();
            }
        }

        public void ExportToRtf(string filePath, RtfExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            FileExportHelper.Do(filePath, stream => this.ExportToRtf(stream, options));
        }

        public void ExportToText(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToText(stream, this.exportOptions.Text);
        }

        public void ExportToText(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToText(filePath, this.exportOptions.Text);
        }

        public virtual void ExportToText(Stream stream, TextExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.CreateTextDocument(stream, options, true);
        }

        public void ExportToText(string filePath, TextExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            FileExportHelper.Do(filePath, stream => this.ExportToText(stream, options));
        }

        internal string[] ExportToXlInternal(string filePath, XlExportOptionsBase options)
        {
            string[] strArray;
            Guard.ArgumentNotNull(options, "options");
            try
            {
                if (options is XlsExportOptions)
                {
                    this.exportOptions.Xls.ExportMode = (options as XlsExportOptions).ExportMode;
                }
                else
                {
                    this.exportOptions.Xlsx.ExportMode = (options as XlsxExportOptions).ExportMode;
                }
                this.AddService(typeof(ProgressMaster), new ProgressMaster(this.ProgressReflector, options));
                if (options.ExportModeBase == ExportModeBase.DifferentFiles)
                {
                    float[] ranges = new float[] { 1f };
                    this.ProgressReflector.SetProgressRanges(ranges);
                    XlExportOptionsBase tempOptions = (XlExportOptionsBase) options.CloneOptions();
                    PagesExportHelper helper = new PagesExportHelper(this, tempOptions);
                    strArray = helper.Execute(helper.PageCount, filePath, delegate (Stream stream) {
                        this.ExportToExcel(stream, tempOptions);
                        DevExpress.XtraPrinting.ProgressReflector progressReflector = this.ProgressReflector;
                        progressReflector.RangeValue++;
                    });
                }
                else
                {
                    string[] textArray2;
                    if ((options.ExportModeBase == ExportModeBase.SingleFile) && !this.DocumentIsDeserialized)
                    {
                        float[] ranges = new float[] { 1f, 4f };
                        this.ProgressReflector.SetProgressRanges(ranges);
                    }
                    FileExportHelper.Do(filePath, delegate (Stream stream) {
                        this.ExportToExcel(stream, options);
                    });
                    if (!File.Exists(filePath))
                    {
                        textArray2 = new string[0];
                    }
                    else
                    {
                        textArray2 = new string[] { filePath };
                    }
                    strArray = textArray2;
                }
            }
            finally
            {
                this.RemoveService(typeof(ProgressMaster));
            }
            return strArray;
        }

        public void ExportToXls(Stream stream)
        {
            this.ExportToXls(stream, this.exportOptions.Xls);
        }

        public void ExportToXls(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToXls(filePath, this.exportOptions.Xls);
        }

        public virtual void ExportToXls(Stream stream, XlsExportOptions options)
        {
            this.ExportToExcel(stream, options);
        }

        public void ExportToXls(string filePath, XlsExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExportToXlInternal(filePath, options);
        }

        public void ExportToXlsx(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToXlsx(stream, this.exportOptions.Xlsx);
        }

        public void ExportToXlsx(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToXlsx(filePath, this.exportOptions.Xlsx);
        }

        public virtual void ExportToXlsx(Stream stream, XlsxExportOptions options)
        {
            this.ExportToExcel(stream, options);
        }

        public void ExportToXlsx(string filePath, XlsxExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExportToXlInternal(filePath, options);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ExportToXps(Stream stream, XpsExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            XpsExportServiceBase service = ((System.IServiceProvider) this).GetService(typeof(XpsExportServiceBase)) as XpsExportServiceBase;
            if (service == null)
            {
                throw new NotSupportedException();
            }
            this.BeforeExport();
            try
            {
                service.Export(stream, options, this.ProgressReflector);
            }
            finally
            {
                this.AfterExport();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void ExportToXps(string filePath, XpsExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                this.ExportToXps(stream, options);
            }
        }

        internal virtual void FinalizeLink(LinkBase link)
        {
            if ((this.activeLink != null) && Equals(this.activeLink, link))
            {
                this.activeLink.BeforeDestroy();
                this.activeLink = null;
            }
        }

        public static System.Type GetBrickExporter(System.Type brickType) => 
            DevExpress.XtraPrinting.BrickExporters.ExportersFactory.GetExporterType(brickType);

        public virtual CommandVisibility GetCommandVisibility(PrintingSystemCommand command) => 
            this.Extender.GetCommandVisibility(command);

        internal PrintingSystemCommand GetFirstVisibleCommand(PrintingSystemCommand[] commands)
        {
            foreach (PrintingSystemCommand command in commands)
            {
                if (!this.IsNoneVisibleCommand(command))
                {
                    return command;
                }
            }
            return PrintingSystemCommand.None;
        }

        public ICollection GetIntersectedBricks() => 
            new IntersectedBricksHelper(this).IntersectedBricks;

        internal Brick[] GetMarkedBricks(Page page) => 
            this.markedPairs.GetBricks(this.Pages, page.Index);

        private static Stream GetStreamInstance(bool compressed, Stream baseStream, CompressionMode compressionMode) => 
            compressed ? new GZipStream(baseStream, compressionMode, true) : baseStream;

        public void HighlightIntersectedBricks(Color backColor)
        {
            new IntersectedBricksHelper(this).HighlightIntersectedBricks(backColor);
        }

        public void InsertPageBreak(float pos)
        {
            this.Document.InsertPageBreak(GraphicsUnitConverter.Convert(pos, this.brickGraphics.Dpi, (float) 300f));
        }

        public void InsertPageBreak(float pos, Margins margins, PaperKind? paperKind, Size pageSize, bool? landscape)
        {
            float num = GraphicsUnitConverter.Convert(pos, this.brickGraphics.Dpi, (float) 300f);
            CustomPageData nextPageData = new CustomPageData();
            nextPageData.Margins = margins;
            nextPageData.PaperKind = paperKind;
            nextPageData.PageSize = pageSize;
            nextPageData.Landscape = landscape;
            this.Document.InsertPageBreak(num, nextPageData);
        }

        internal bool IsNoneVisibleCommand(PrintingSystemCommand command) => 
            this.GetCommandVisibility(command) == CommandVisibility.None;

        internal static bool IsVisible(Control control) => 
            (control != null) && control.Visible;

        public void LoadDocument(Stream stream)
        {
            try
            {
                this.PrepareLoading();
                this.SetDocument(new DeserializedDocument(this));
                stream.Seek(0L, SeekOrigin.Begin);
                if (DeflateStreamsArchiveReader.IsValidStream(stream))
                {
                    this.Document.Deserialize(stream, CreateIndependentPagesSerializer());
                }
                else
                {
                    stream.Seek(0L, SeekOrigin.Begin);
                    byte[] buffer = new byte["?xml".Length + 4];
                    stream.Read(buffer, 0, buffer.Length);
                    StringBuilder builder = new StringBuilder();
                    int index = 0;
                    while (true)
                    {
                        if (index >= buffer.Length)
                        {
                            stream.Seek(0L, SeekOrigin.Begin);
                            stream = GetStreamInstance(!builder.ToString().Contains("?xml"), stream, CompressionMode.Decompress);
                            try
                            {
                                this.Document.Deserialize(stream, CreateNormalSerializer());
                            }
                            finally
                            {
                                bool flag;
                                CloseStream(flag, stream);
                            }
                            break;
                        }
                        builder.Append((char) buffer[index]);
                        index++;
                    }
                }
            }
            catch
            {
                throw new InvalidDataException(PreviewLocalizer.GetString(PreviewStringId.Msg_CannotLoadDocument));
            }
        }

        public void LoadDocument(string filePath)
        {
            using (Stream stream = CreateFileStreamForRead(filePath))
            {
                this.LoadDocument(stream);
            }
        }

        internal void LoadVirtualDocument(Stream stream)
        {
            this.LoadVirtualDocumentCore(stream, false);
        }

        internal void LoadVirtualDocument(string filePath)
        {
            this.LoadVirtualDocumentCore(CreateFileStreamForRead(filePath), true);
        }

        private void LoadVirtualDocumentCore(Stream stream, bool disposeStream)
        {
            this.PrepareLoading();
            this.SetDocument(new VirtualDocument(this, stream, disposeStream));
        }

        public void Lock()
        {
            this.locked = true;
        }

        public void MarkBrick(Brick brick, Page page)
        {
            if ((page != null) && (brick != null))
            {
                this.markedPairs.Add(BrickPagePair.Create(brick, page));
            }
        }

        protected internal virtual void OnAfterBandPrint(PageEventArgs e)
        {
            PageEventHandler handler = (PageEventHandler) base.Events[AfterBandPrintEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected internal void OnAfterBuildPages(EventArgs e)
        {
            EventHandler handler = base.Events[AfterBuildPagesEvent] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void OnAfterChange(ChangeEventArgs e)
        {
            if (!this.Initializing)
            {
                ChangeEventHandler handler = (ChangeEventHandler) base.Events[AfterChangeEvent];
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        internal void OnAfterMarginsChange(MarginSide side, float value)
        {
            if (!this.Initializing)
            {
                if (this.activeLink != null)
                {
                    this.activeLink.UpdatePageSettingsInternal();
                }
                try
                {
                    this.raisingAfterChange = true;
                    MarginsChangeEventHandler handler = (MarginsChangeEventHandler) base.Events[AfterMarginsChangeEvent];
                    if (handler != null)
                    {
                        handler(this, new MarginsChangeEventArgs(side, value));
                    }
                    object[] parameters = new object[,] { { "Side", Convert.ToString(side) }, { "Value", value } };
                    ChangeEventArgs e = CreateEventArgs("AfterMarginsChange", parameters);
                    this.OnAfterChange(e);
                }
                finally
                {
                    this.raisingAfterChange = false;
                    this.ExecDelayedAction();
                }
            }
        }

        protected internal virtual void OnAfterPagePaint(PagePaintEventArgs e)
        {
            if (!this.Initializing)
            {
                PagePaintEventHandler handler = (PagePaintEventHandler) base.Events[AfterPagePaintEvent];
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        protected internal virtual void OnAfterPagePrint(PageEventArgs e)
        {
            PageEventHandler handler = (PageEventHandler) base.Events[AfterPagePrintEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected internal void OnBeforeBuildPages(EventArgs e)
        {
            EventHandler handler = base.Events[BeforeBuildPagesEvent] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
            this.GetService<IPageContentService>().OnBeforeBuildPages();
        }

        internal void OnBeforeChange(ChangeEventArgs e)
        {
            if (!this.Initializing)
            {
                ChangeEventHandler handler = (ChangeEventHandler) base.Events[BeforeChangeEvent];
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        internal float OnBeforeMarginsChange(MarginSide side, float value)
        {
            if (this.Initializing)
            {
                return value;
            }
            MarginsChangeEventArgs e = new MarginsChangeEventArgs(side, value);
            MarginsChangeEventHandler handler = (MarginsChangeEventHandler) base.Events[BeforeMarginsChangeEvent];
            if (handler != null)
            {
                handler(this, e);
            }
            object[] parameters = new object[,] { { "Side", Convert.ToString(side) }, { "Value", e.Value } };
            ChangeEventArgs args2 = CreateEventArgs("BeforeMarginsChange", parameters);
            this.OnBeforeChange(args2);
            return (float) args2.ValueOf("Value");
        }

        internal void OnBeforePagePaint(PagePaintEventArgs e)
        {
            if (!this.Initializing)
            {
                PagePaintEventHandler handler = (PagePaintEventHandler) base.Events[BeforePagePaintEvent];
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        protected internal virtual void OnClearPages()
        {
            this.ClearMarkedBricks();
            if (this.editingFields != null)
            {
                Action<EditingField> action = <>c.<>9__324_0;
                if (<>c.<>9__324_0 == null)
                {
                    Action<EditingField> local1 = <>c.<>9__324_0;
                    action = <>c.<>9__324_0 = item => item.InvalidatePageInfo();
                }
                this.editingFields.ForEach<EditingField>(action);
            }
        }

        protected virtual void OnCreateDocumentException(ExceptionEventArgs args)
        {
            this.Document.OnCreateException();
            ExceptionEventHandler handler = base.Events[CreateDocumentExceptionEvent] as ExceptionEventHandler;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        protected internal void OnDocumentChanged(EventArgs e)
        {
            EventHandler handler = base.Events[DocumentChangedEvent] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void OnEndPrint(EventArgs e)
        {
            EventHandler handler = base.Events[EndPrintEvent] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected internal void OnFillEmptySpace(EmptySpaceEventArgs e)
        {
            EmptySpaceEventHandler handler = base.Events[FillEmptySpaceEvent] as EmptySpaceEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected internal void OnPageInsertComplete(PageInsertCompleteEventArgs e)
        {
            PageInsertCompleteEventHandler handler = base.Events[PageInsertCompleteEvent] as PageInsertCompleteEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void OnPageSettingsChanged()
        {
            if (!this.Initializing)
            {
                if (this.activeLink != null)
                {
                    this.activeLink.UpdatePageSettingsInternal();
                }
                try
                {
                    this.raisingAfterChange = true;
                    EventHandler handler = (EventHandler) base.Events[PageSettingsChangedEvent];
                    if (handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                    this.OnAfterChange(CreateEventArgs("PageSettingsChanged", null));
                }
                finally
                {
                    this.raisingAfterChange = false;
                    this.ExecDelayedAction();
                }
            }
        }

        internal void OnPrintProgress(PrintProgressEventArgs e)
        {
            PrintProgressEventHandler handler = base.Events[PrintProgressEvent] as PrintProgressEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnProgressPositionChanged(object sender, EventArgs e)
        {
            ChangeEventArgs args = new ChangeEventArgs("ProgressPositionChanged");
            args.Add("ProgressPosition", ((DevExpress.XtraPrinting.ProgressReflector) sender).PositionCore);
            args.Add("ProgressMaximum", ((DevExpress.XtraPrinting.ProgressReflector) sender).MaximumCore);
            this.OnAfterChange(args);
        }

        internal void OnScaleFactorChanged()
        {
            try
            {
                this.raisingAfterChange = true;
                EventHandler handler = (EventHandler) base.Events[ScaleFactorChangedEvent];
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
                this.OnAfterChange(CreateEventArgs("ScaleFactorChanged", null));
            }
            finally
            {
                this.raisingAfterChange = false;
                this.ExecDelayedAction();
            }
        }

        internal void OnStartPrint(PrintDocumentEventArgs e)
        {
            if (!this.Initializing)
            {
                PrintDocumentEventHandler handler = base.Events[StartPrintEvent] as PrintDocumentEventHandler;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        internal void OnXlSheetCreated(ExcelExportProvider provider, XlSheetCreatedEventArgs e)
        {
            XlSheetCreatedEventHandler handler = base.Events[XlSheetCreatedEvent] as XlSheetCreatedEventHandler;
            if (handler != null)
            {
                handler(provider, e);
            }
        }

        private void PrepareLoading()
        {
            this.FinalizeLink(this.activeLink);
            this.DisposeDocument();
            this.ClearWaterMark();
            this.PageSettings.AssignDefaultPageSettings();
        }

        protected internal virtual void PrintDocument(System.Drawing.Printing.PrintDocument pd)
        {
            this.BeforeExport();
            try
            {
                pd.Print();
            }
            finally
            {
                this.AfterExport();
            }
        }

        internal bool RaiseCreateDocumentException(Exception exception)
        {
            ExceptionEventArgs args = new ExceptionEventArgs(exception);
            this.OnCreateDocumentException(args);
            return !args.Handled;
        }

        internal void RaisePageBackgrChanged(EventArgs e)
        {
            if (this.pageBackgrChanged != null)
            {
                this.pageBackgrChanged(this, e);
            }
        }

        public void RemoveCommandHandler(ICommandHandler handler)
        {
            this.Extender.RemoveCommandHandler(handler);
        }

        public void RemoveService(System.Type serviceType)
        {
            this.measurers.Clear();
            this.serviceContainer.RemoveService(serviceType);
        }

        public void RemoveService(System.Type serviceType, bool promote)
        {
            this.measurers.Clear();
            this.serviceContainer.RemoveService(serviceType, promote);
        }

        protected internal void ResetCancelPending()
        {
            this.cancelPending = false;
        }

        protected internal void ResetDocumentIfCancel()
        {
            if (this.CancelPending)
            {
                this.ClearContent();
            }
        }

        public void ResetProgressReflector()
        {
            this.fProgressReflector = null;
        }

        public void SaveDocument(Stream stream)
        {
            this.SaveDocument(stream, this.ExportOptions.NativeFormat);
        }

        public void SaveDocument(string filePath)
        {
            this.SaveDocument(filePath, this.ExportOptions.NativeFormat);
        }

        public void SaveDocument(Stream stream, NativeFormatOptions options)
        {
            if (this.Document.IsCreating)
            {
                throw new InvalidOperationException("Can't save document while creating.");
            }
            stream = GetStreamInstance(options.Compressed, stream, CompressionMode.Compress);
            this.Document.Serialize(stream, CreateNormalSerializer());
            CloseStream(options.Compressed, stream);
        }

        public void SaveDocument(string filePath, NativeFormatOptions options)
        {
            FileExportHelper.Do(filePath, stream => this.SaveDocument(stream, options));
        }

        internal void SaveIndependentPages(Stream stream)
        {
            this.Document.Serialize(stream, CreateIndependentPagesSerializer());
        }

        public static void SetBrickExporter(System.Type brickType, System.Type exporterType)
        {
            DevExpress.XtraPrinting.BrickExporters.ExportersFactory.SetExporter(brickType, exporterType);
        }

        public void SetCommandVisibility(PrintingSystemCommand command, CommandVisibility visibility)
        {
            this.SetCommandVisibility(command, visibility, Priority.High);
        }

        public void SetCommandVisibility(PrintingSystemCommand[] commands, CommandVisibility visibility)
        {
            this.SetCommandVisibility(commands, visibility, Priority.High);
        }

        protected void SetCommandVisibility(PrintingSystemCommand command, bool visible)
        {
            CommandVisibility visibility = visible ? CommandVisibility.All : CommandVisibility.None;
            this.SetCommandVisibility(command, visibility, Priority.Low);
        }

        internal void SetCommandVisibility(PrintingSystemCommand command, CommandVisibility visibility, Priority priority)
        {
            PrintingSystemCommand[] commands = new PrintingSystemCommand[] { command };
            this.SetCommandVisibility(commands, visibility, priority);
        }

        internal void SetCommandVisibility(PrintingSystemCommand[] commands, CommandVisibility visibility, Priority priority)
        {
            this.Extender.SetCommandVisibility(commands, visibility, priority);
        }

        internal void SetDocument(DevExpress.XtraPrinting.Document doc)
        {
            if (!ReferenceEquals(doc, this.fDocument))
            {
                this.DisposeDocucmentCore();
                this.fDocument = doc;
            }
        }

        private bool ShouldSerializeExportOptions() => 
            this.exportOptions.ShouldSerialize();

        protected void StartDispose()
        {
            this.disposeCount++;
        }

        protected void SubscribeProgressEvents(DevExpress.XtraPrinting.ProgressReflector progressReflector)
        {
            if (progressReflector != null)
            {
                progressReflector.PositionChanged += new EventHandler(this.OnProgressPositionChanged);
            }
        }

        void ISupportInitialize.BeginInit()
        {
            this.initializingCounter++;
        }

        void ISupportInitialize.EndInit()
        {
            if (this.initializingCounter != 0)
            {
                this.initializingCounter--;
            }
        }

        object System.IServiceProvider.GetService(System.Type serviceType) => 
            (serviceType == typeof(IServiceContainer)) ? this : this.serviceContainer.GetService(serviceType);

        public void Unlock()
        {
            this.locked = false;
        }

        public void UnmarkBrick(Brick brick, Page page)
        {
            if ((page != null) && (brick != null))
            {
                this.markedPairs.Remove(BrickPagePair.Create(brick, page));
            }
        }

        protected void UnsubscribeEvents(DevExpress.XtraPrinting.ProgressReflector progressReflector)
        {
            if (progressReflector != null)
            {
                progressReflector.PositionChanged -= new EventHandler(this.OnProgressPositionChanged);
            }
        }

        IList<IPage> IDocument.Pages
        {
            get
            {
                this.innerPages ??= new ListWrapper<IPage, Page>(this.Document.Pages);
                return this.innerPages;
            }
        }

        bool IDocument.IsCreating =>
            this.Document.IsCreating;

        bool IDocument.IsRightToLeftLayout =>
            this.Document.RightToLeftLayout;

        bool IDocument.IsEmpty =>
            this.Document.IsEmpty;

        IPageSettings IDocument.PageSettings =>
            this.PageSettings;

        SizeF IMaxPageSizeProvider.MaxPageSize
        {
            get
            {
                IMaxPageSizeProvider document = this.Document as IMaxPageSizeProvider;
                return ((document != null) ? document.MaxPageSize : SizeF.Empty);
            }
        }

        public static string UserName
        {
            get => 
                !string.IsNullOrEmpty(userName) ? userName : SystemInformation.UserName;
            set => 
                userName = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditingFieldCollection EditingFields
        {
            get
            {
                this.editingFields ??= new EditingFieldCollection(this, delegate (EditingField item) {
                    if (this.EditingFieldChanged != null)
                    {
                        this.EditingFieldChanged(this, new EditingFieldEventArgs(item));
                    }
                }, delegate (EditingField item) {
                    if (this.EditingFieldReadOnlyChanged != null)
                    {
                        this.EditingFieldReadOnlyChanged(this, new EditingFieldEventArgs(item));
                    }
                });
                return this.editingFields;
            }
        }

        internal PrintingSystemAction DelayedAction { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CancelPending =>
            this.cancelPending;

        internal IPrintingSystemExtenderBase Extender
        {
            get
            {
                this.extender ??= this.CreateExtender();
                return this.extender;
            }
            set => 
                this.extender = value;
        }

        internal BrickPaintBase BrickPainter
        {
            get
            {
                this.brickPainter ??= this.CreateBrickPaint();
                return this.brickPainter;
            }
            set => 
                this.brickPainter = value;
        }

        internal bool DocumentIsDeserialized =>
            this.Document is DeserializedDocument;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DevExpress.XtraPrinting.ProgressReflector ProgressReflector
        {
            get
            {
                DevExpress.XtraPrinting.ProgressReflector objA = (this.fProgressReflector != null) ? this.fProgressReflector : ((this.Extender.ActiveProgressReflector != null) ? this.Extender.ActiveProgressReflector : this.fFakedReflector);
                if (!ReferenceEquals(objA, this.subscribedProgressReflector))
                {
                    this.UnsubscribeEvents(this.subscribedProgressReflector);
                    this.SubscribeProgressEvents(objA);
                    this.subscribedProgressReflector = objA;
                }
                return objA;
            }
            set => 
                this.fProgressReflector = value;
        }

        [Obsolete("Use the ExportOptions.PrintPreview.DefaultSendFormat property instead"), DXHelpExclude(true), DefaultValue(0x29), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public PrintingSystemCommand SendDefault
        {
            get => 
                this.ExportOptions.PrintPreview.DefaultSendFormat;
            set => 
                this.ExportOptions.PrintPreview.DefaultSendFormat = value;
        }

        [Obsolete("Use the ExportOptions.PrintPreview.DefaultExportFormat property instead"), DXHelpExclude(true), DefaultValue(30), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public PrintingSystemCommand ExportDefault
        {
            get => 
                this.ExportOptions.PrintPreview.DefaultExportFormat;
            set => 
                this.ExportOptions.PrintPreview.DefaultExportFormat = value;
        }

        private bool Initializing =>
            this.initializingCounter != 0;

        string IPrintingSystem.Version =>
            typeof(PrintingSystemBase).Assembly.GetName().Version.ToString();

        IImagesContainer IPrintingSystem.Images =>
            this.Images;

        internal bool RaisingAfterChange =>
            this.raisingAfterChange;

        [Description(""), Category("Appearance")]
        public ImageItemCollection ImageResources =>
            this.imageResources;

        [Description("Gets or sets a value indicating whether pages should be renumbered following reordering in the PrintingSystemBase.Pages collection."), Category("Behavior"), DefaultValue(true)]
        public bool ContinuousPageNumbering
        {
            get => 
                this.continuousPageNumbering;
            set => 
                this.continuousPageNumbering = value;
        }

        internal StylesContainer Styles =>
            (StylesContainer) this.containers[0];

        internal StylesContainer GarbageStyles =>
            (StylesContainer) this.containers[1];

        internal ImagesContainer Images =>
            (ImagesContainer) this.containers[2];

        internal ImagesCache GarbageImages =>
            (ImagesCache) this.containers[3];

        internal FontCacheContainer Fonts =>
            (FontCacheContainer) this.containers[4];

        [Description("Specifies the culture that is used to display date-time values by the XRPageInfo control."), Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CultureInfo Culture { get; set; }

        [Description("Gets the settings used to specify export parameters when exporting a printing system's document."), Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DevExpress.XtraPrinting.ExportOptions ExportOptions =>
            this.exportOptions;

        [Description("Gets or sets a value which specifies whether an error message is shown when the page margins are set outside the printable area."), Category("Behavior"), DefaultValue(true)]
        public bool ShowMarginsWarning
        {
            get => 
                this.showMarginsWarning;
            set => 
                this.showMarginsWarning = value;
        }

        [Description("Specifies whether or not to show a print status dialog when printing a document."), Category("Behavior"), DefaultValue(true)]
        public bool ShowPrintStatusDialog
        {
            get => 
                this.showPrintStatusDialog;
            set => 
                this.showPrintStatusDialog = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XtraPageSettingsBase PageSettings
        {
            get
            {
                this.xtraSettings ??= this.CreatePageSettings();
                return this.xtraSettings;
            }
        }

        [Browsable(false)]
        public BrickGraphics Graph =>
            this.brickGraphics;

        [Browsable(false)]
        public DevExpress.XtraPrinting.Document Document
        {
            get
            {
                if (this.DocumentIsDisposed(this.fDocument))
                {
                    this.fDocument = this.CreateDocument();
                }
                return this.fDocument;
            }
        }

        [Browsable(false)]
        public DevExpress.XtraPrinting.Native.PrintingDocument PrintingDocument =>
            (DevExpress.XtraPrinting.Native.PrintingDocument) this.Document;

        [Browsable(false)]
        public int PageCount =>
            this.Document.PageCount;

        [Browsable(false)]
        public Margins PageMargins =>
            this.PageSettings.Margins;

        [Browsable(false)]
        public Rectangle PageBounds =>
            this.PageSettings.Bounds;

        [Browsable(false)]
        public bool Landscape =>
            this.PageSettings.Landscape;

        [Description("Gets the document's watermark."), Category("Appearance")]
        public virtual DevExpress.XtraPrinting.Drawing.Watermark Watermark
        {
            get
            {
                this.watermark ??= new DevExpress.XtraPrinting.Drawing.Watermark();
                return this.watermark;
            }
        }

        [Browsable(false)]
        public PageList Pages =>
            this.Document.Pages;

        internal bool IsMetric
        {
            get => 
                this.isMetric;
            set => 
                this.isMetric = value;
        }

        internal GdiHashtable Gdi =>
            this.gdi;

        internal bool Locked =>
            this.locked;

        internal LinkBase ActiveLink =>
            this.activeLink;

        Page IPrintingSystemContext.DrawingPage =>
            null;

        PrintingSystemBase IPrintingSystemContext.PrintingSystem =>
            this;

        Measurer IPrintingSystemContext.Measurer
        {
            get
            {
                Measurer service;
                int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                if (!this.measurers.TryGetValue(managedThreadId, out service))
                {
                    service = ((System.IServiceProvider) this).GetService(typeof(Measurer)) as Measurer;
                    if (service != null)
                    {
                        this.measurers.Add(managedThreadId, service);
                    }
                }
                return service;
            }
        }

        internal bool IsDisposed =>
            this.isDisposed;

        internal bool IsDisposing =>
            this.disposeCount > 0;

        string IPageInfoFormatProvider.Format
        {
            get => 
                this.format;
            set => 
                this.format = value;
        }

        PageInfo IPageInfoFormatProvider.PageInfo { get; set; }

        int IPageInfoFormatProvider.StartPageNumber
        {
            get => 
                this.startPageNumber;
            set => 
                this.startPageNumber = value;
        }

        internal IPageRepository PageRepository
        {
            get => 
                this.pageRepository ?? this.Pages;
            set
            {
                if (!ReferenceEquals(this.pageRepository, value))
                {
                    this.pageRepository = value;
                    Action<EditingField> action = <>c.<>9__343_0;
                    if (<>c.<>9__343_0 == null)
                    {
                        Action<EditingField> local1 = <>c.<>9__343_0;
                        action = <>c.<>9__343_0 = item => item.UpdatePageIndex();
                    }
                    this.EditingFields.ForEach<EditingField>(action);
                }
            }
        }

        int IPrintingSystem.AutoFitToPagesWidth
        {
            get => 
                this.Document.AutoFitToPagesWidth;
            set => 
                this.Document.AutoFitToPagesWidth = value;
        }

        internal RichTextBox RichTextBoxForDrawing
        {
            get
            {
                if (this.richTextBoxForDrawing == null)
                {
                    this.richTextBoxForDrawing = new RichTextBoxEx();
                    PSNativeMethods.ForceCreateHandle(this.richTextBoxForDrawing);
                }
                return this.richTextBoxForDrawing;
            }
        }

        [Browsable(false)]
        internal DevExpress.XtraPrinting.BrickExporters.ExportersFactory ExportersFactory
        {
            get
            {
                this.exportersFactory ??= new DevExpress.XtraPrinting.BrickExporters.ExportersFactory();
                return this.exportersFactory;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintingSystemBase.<>c <>9 = new PrintingSystemBase.<>c();
            public static Func<object> <>9__69_0;
            public static ServiceCreatorCallback <>9__69_1;
            public static ServiceCreatorCallback <>9__69_2;
            public static ServiceCreatorCallback <>9__69_3;
            public static Action<EditingField> <>9__324_0;
            public static Action<EditingField> <>9__343_0;

            internal object <.ctor>b__69_0() => 
                new GdiPlusMeasurer();

            internal object <.ctor>b__69_1(IServiceContainer c, System.Type t) => 
                new GdiPlusGraphicsModifier();

            internal object <.ctor>b__69_2(IServiceContainer c, System.Type t) => 
                new PageContentService();

            internal object <.ctor>b__69_3(IServiceContainer c, System.Type t) => 
                new MergeBrickHelper();

            internal void <OnClearPages>b__324_0(EditingField item)
            {
                item.InvalidatePageInfo();
            }

            internal void <set_PageRepository>b__343_0(EditingField item)
            {
                item.UpdatePageIndex();
            }
        }

        private class IntersectedBricksHelper
        {
            private ICollection intersectedBricks;
            private PrintingSystemBase ps;

            public IntersectedBricksHelper(PrintingSystemBase ps)
            {
                this.ps = ps;
            }

            private IDictionary<BrickBase, RectangleF> CollectPageBrickRects(Page page)
            {
                Dictionary<BrickBase, RectangleF> dictionary = new Dictionary<BrickBase, RectangleF>();
                BrickEnumerator enumerator = new BrickEnumerator(page);
                while (enumerator.MoveNext())
                {
                    if (!enumerator.CurrentBrick.IsVisible || (enumerator.CurrentBrick is CompositeBrick))
                    {
                        continue;
                    }
                    RectangleF currentBrickVisibleBounds = enumerator.GetCurrentBrickVisibleBounds();
                    dictionary.Add(enumerator.CurrentBrick, currentBrickVisibleBounds);
                }
                return dictionary;
            }

            private IEnumerable<BrickBase> GetInnerIntersectedBricks(PanelBrick panelBrick)
            {
                List<BrickBase> list = new List<BrickBase>();
                foreach (Brick brick in panelBrick.InnerBrickList)
                {
                    RectangleF rect = brick.Rect;
                    if ((rect.Left < 0f) || ((rect.Top < 0f) || (FloatsComparer.Default.FirstGreaterSecond((double) rect.Right, (double) panelBrick.Width) || FloatsComparer.Default.FirstGreaterSecond((double) rect.Bottom, (double) panelBrick.Height))))
                    {
                        list.Add(brick);
                        continue;
                    }
                    if (brick is PanelBrick)
                    {
                        list.AddRange(this.GetInnerIntersectedBricks((PanelBrick) brick));
                    }
                    foreach (Brick brick2 in panelBrick.InnerBrickList)
                    {
                        if (!ReferenceEquals(brick, brick2) && !FloatsComparer.Default.RectangleIsEmpty(RectangleF.Intersect(brick.Rect, brick2.Rect)))
                        {
                            list.Add(brick);
                        }
                    }
                }
                return list;
            }

            private ICollection GetIntersectedBricks()
            {
                HashSet<BrickBase> source = new HashSet<BrickBase>();
                foreach (Page page in this.ps.Document.Pages)
                {
                    page.PerformLayout(new PrintingSystemContextWrapper(this.ps, page));
                    source.UnionWith(this.GetIntersectedBricks(page));
                }
                return source.ToArray<BrickBase>();
            }

            private IEnumerable<BrickBase> GetIntersectedBricks(Page page)
            {
                List<BrickBase> list = new List<BrickBase>();
                IDictionary<BrickBase, RectangleF> dictionary = this.CollectPageBrickRects(page);
                foreach (KeyValuePair<BrickBase, RectangleF> pair in dictionary)
                {
                    if (pair.Key is PanelBrick)
                    {
                        list.AddRange(this.GetInnerIntersectedBricks((PanelBrick) pair.Key));
                    }
                    foreach (KeyValuePair<BrickBase, RectangleF> pair2 in dictionary)
                    {
                        if ((pair.Key != pair2.Key) && !FloatsComparer.Default.RectangleIsEmpty(RectangleF.Intersect(pair.Value, pair2.Value)))
                        {
                            list.Add(pair.Key);
                        }
                    }
                }
                return list;
            }

            public void HighlightIntersectedBricks(Color backColor)
            {
                foreach (Brick brick in this.IntersectedBricks)
                {
                    VisualBrick brick2 = brick as VisualBrick;
                    if (brick2 != null)
                    {
                        brick2.BackColor = backColor;
                    }
                }
            }

            public ICollection IntersectedBricks
            {
                get
                {
                    this.intersectedBricks ??= this.GetIntersectedBricks();
                    return this.intersectedBricks;
                }
            }
        }
    }
}

