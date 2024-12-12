namespace DevExpress.Xpf.Printing.PreviewControl.Native.Models
{
    using DevExpress.DocumentView;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Exports;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Preview;
    using DevExpress.XtraPrinting.XamlExport;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class DocumentViewModel : BindableBase, DevExpress.Xpf.DocumentViewer.IDocument, DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, IProgressSettings, INotifyPropertyChanged
    {
        private bool isExporting;
        private readonly PageCollection pages = new PageCollection();
        private ILink link;
        private readonly SearchHelper searchHelper = new SearchHelper();
        private object source;
        private IEnumerable<EditingField> editingFields = Enumerable.Empty<EditingField>();
        protected DocumentPublishEngine publishEngine;
        private readonly DocumentPreviewProgressReflector progressReflector = new DocumentPreviewProgressReflector();
        private IBrickPaintService paintService;
        private ICancellationService cancellationService;
        private DevExpress.Xpf.Printing.Native.DocumentStatus documentStatus;
        private readonly Locker locker = new Locker();
        protected IEnumerable<BookmarkNodeItem> bookmarks;

        public event EventHandler Disposing;

        public event EventHandler DocumentChanged;

        public event EventHandler DocumentCreated;

        public event ExceptionEventHandler DocumentException;

        public event EventHandler StartDocumentCreation;

        protected internal DocumentViewModel()
        {
        }

        internal void AfterDrawPages(int[] pageIndexes)
        {
            this.PrintingSystem.Do<DevExpress.DocumentView.IDocument>(x => x.AfterDrawPages(this, pageIndexes));
        }

        internal void ClearPages()
        {
            this.pages.RemoveAll();
        }

        private void CollectBookmarks(IBookmarkNodeCollection nodes, List<BookmarkNodeItem> previewNodes, ref int currentId)
        {
            int parentId = currentId;
            foreach (BookmarkNode node in nodes)
            {
                currentId++;
                previewNodes.Add(new BookmarkNodeItem(node, currentId, parentId));
                this.CollectBookmarks(node.Nodes, previewNodes, ref currentId);
            }
        }

        public virtual void CreateDocument()
        {
            if ((this.source != null) && (this.PrintingSystem != null))
            {
                if (this.source == this.link)
                {
                    this.link.CreateDocument(true);
                }
                else if (this.source is Stream)
                {
                    this.PrintingSystem.LoadDocument((Stream) this.source);
                }
                else if (this.source is string)
                {
                    this.PrintingSystem.LoadDocument((string) this.source);
                }
            }
        }

        private XpsExportServiceBase CreateXpsExportService() => 
            new XpsExportService(new DelegatePaginator(new Func<int, FrameworkElement>(this.VisualizePage), () => this.PrintingSystem.PageCount));

        public void Dispose()
        {
            if (this.Disposing == null)
            {
                EventHandler disposing = this.Disposing;
            }
            else
            {
                this.Disposing(this, EventArgs.Empty);
            }
            for (int i = this.Pages.Count - 1; i >= 0; i--)
            {
                this.Pages.RemoveAt(i);
            }
            this.RaiseDocumentChanged();
        }

        internal void EnsureBrickOnPage(BrickPagePair pair, Action<BrickPagePair> onEnsured)
        {
            this.PrintingSystem.EnsureBrickOnPage(pair, onEnsured);
        }

        public virtual void Export(ExportOptionsViewModel options)
        {
            try
            {
                this.IsExporting = true;
                this.publishEngine.Export(options);
                this.progressReflector.MaximizeRange();
            }
            catch (Exception exception)
            {
                this.RaiseDocumentException(exception);
            }
            finally
            {
                this.IsExporting = false;
            }
        }

        protected void ForceProgressVisibility(bool show)
        {
            this.ProgressReflector.Visible = show;
            this.RaiseProgressChanged();
        }

        public IEnumerable<BookmarkNodeItem> GetBookmarks()
        {
            IEnumerable<BookmarkNodeItem> enumerable;
            if (!this.IsCreated)
            {
                this.bookmarks = (IEnumerable<BookmarkNodeItem>) (enumerable = null);
                return enumerable;
            }
            if (this.bookmarks != null)
            {
                return this.bookmarks;
            }
            List<BookmarkNodeItem> previewNodes = new List<BookmarkNodeItem>();
            int currentId = 0;
            string bookmark = this.Document.Bookmark;
            string text = bookmark;
            if (bookmark == null)
            {
                string local1 = bookmark;
                text = this.Document.Name;
            }
            previewNodes.Add(new BookmarkNodeItem(new BookmarkNode(text), currentId, -1));
            this.Document.Do<DevExpress.XtraPrinting.Document>(delegate (DevExpress.XtraPrinting.Document x) {
                this.CollectBookmarks(x.BookmarkNodes, previewNodes, ref currentId);
            });
            this.bookmarks = enumerable = previewNodes;
            return enumerable;
        }

        private bool GetIsInProgress()
        {
            Func<bool> fallback = <>c.<>9__81_3;
            if (<>c.<>9__81_3 == null)
            {
                Func<bool> local1 = <>c.<>9__81_3;
                fallback = <>c.<>9__81_3 = () => false;
            }
            return this.PrintingSystem.Return<PrintingSystemBase, bool>(delegate (PrintingSystemBase ps) {
                Func<DocumentPreviewProgressReflector, bool> evaluator = <>c.<>9__81_1;
                if (<>c.<>9__81_1 == null)
                {
                    Func<DocumentPreviewProgressReflector, bool> local1 = <>c.<>9__81_1;
                    evaluator = <>c.<>9__81_1 = x => x.Visible;
                }
                return (ps.ProgressReflector as DocumentPreviewProgressReflector).Return<DocumentPreviewProgressReflector, bool>(evaluator, () => (this.IsCreating || this.IsExporting));
            }, fallback);
        }

        protected void Initialize()
        {
            this.PrintingSystem.Do<PrintingSystemBase>(delegate (PrintingSystemBase ps) {
                this.PreparePrintingSystem(this.PrintingSystem);
                this.Subscribe();
                this.publishEngine = new DocumentPublishEngine(this.PrintingSystem);
            });
            IEnumerable<EditingField> enumerable1 = ((this.Document == null) || !this.IsCreated) ? Enumerable.Empty<EditingField>() : this.PrintingSystem.EditingFields;
            this.EditingFields = enumerable1;
        }

        private bool IsProgressMarquee()
        {
            Func<PrintingSystemBase, bool> evaluator = <>c.<>9__86_0;
            if (<>c.<>9__86_0 == null)
            {
                Func<PrintingSystemBase, bool> local1 = <>c.<>9__86_0;
                evaluator = <>c.<>9__86_0 = x => (x.ProgressReflector.Ranges.Count != 0) && float.IsNaN(x.ProgressReflector.Ranges[0]);
            }
            return this.PrintingSystem.Return<PrintingSystemBase, bool>(evaluator, (<>c.<>9__86_1 ??= () => false));
        }

        public virtual void Load(object source)
        {
            this.DocumentStatus = DevExpress.Xpf.Printing.Native.DocumentStatus.None;
            this.source = source;
            if (source is ILink)
            {
                this.link = (ILink) source;
            }
            else
            {
                try
                {
                    this.link = this.LoadDocument(source);
                }
                catch (Exception exception)
                {
                    this.RaiseDocumentException(exception);
                    return;
                }
            }
            Func<ILink, PrintingSystemBase> evaluator = <>c.<>9__90_0;
            if (<>c.<>9__90_0 == null)
            {
                Func<ILink, PrintingSystemBase> local1 = <>c.<>9__90_0;
                evaluator = <>c.<>9__90_0 = x => (PrintingSystemBase) x.PrintingSystem;
            }
            this.PrintingSystem = this.link.Return<ILink, PrintingSystemBase>(evaluator, <>c.<>9__90_1 ??= ((Func<PrintingSystemBase>) (() => null)));
            this.Initialize();
            base.RaisePropertiesChanged<bool, bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_HasBookmarks)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_CanChangePageSettings)), new ParameterExpression[0]));
        }

        protected virtual ILink LoadDocument(object source)
        {
            SimpleLink link = new SimpleLink();
            if (source is Stream)
            {
                link.PrintingSystem.LoadDocument((Stream) source);
                return link;
            }
            if (!(source is string))
            {
                throw new NotSupportedException(PrintingLocalizer.GetString(PrintingStringId.DocumentSourceNotSupported));
            }
            link.PrintingSystem.LoadDocument((string) source);
            return link;
        }

        private void Locker_Unlocked(object sender, EventArgs e)
        {
            this.locker.Unlocked -= new EventHandler(this.Locker_Unlocked);
            this.ResolveNewPages();
        }

        public void MarkBrick(BookmarkNodeItem bookmark)
        {
            this.PrintingSystem.ClearMarkedBricks();
            BrickPagePair pair = bookmark.BookmarkNode.Pair;
            this.PrintingSystem.MarkBrick(pair.GetBrick(this.PrintingSystem.Pages), pair.GetPage(this.PrintingSystem.Pages));
        }

        protected virtual void OnAfterBuildPages(object sender, EventArgs e)
        {
            IEnumerable<EditingField> enumerable;
            this.DocumentStatus = this.PrintingSystem.Pages.Any<Page>() ? DevExpress.Xpf.Printing.Native.DocumentStatus.None : DevExpress.Xpf.Printing.Native.DocumentStatus.NoPages;
            this.bookmarks = null;
            this.RaiseDocumentCreatingChanged();
            this.cancellationService = this.PrintingSystem.GetService<ICancellationService>();
            this.cancellationService.Do<ICancellationService>(delegate (ICancellationService x) {
                x.StateChanged -= new EventHandler(this.OnCancelationServiceStateChanged);
            });
            DocumentViewModel model1 = this;
            if (enumerable == null)
            {
                model1 = (DocumentViewModel) Enumerable.Empty<EditingField>();
            }
            this.PrintingSystem.EditingFields.EditingFields = (IEnumerable<EditingField>) model1;
            this.RaiseDocumentCreated();
            base.RaisePropertiesChanged<bool, bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_HasBookmarks)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_CanChangePageSettings)), new ParameterExpression[0]));
        }

        protected virtual void OnBeforeBuildPages(object sender, EventArgs e)
        {
            this.DocumentStatus = DevExpress.Xpf.Printing.Native.DocumentStatus.DocumentCreation;
            this.RaiseDocumentCreatingChanged();
            this.Pages.Clear();
            this.PrintingSystem.GetService<ICancellationService>().Do<ICancellationService>(delegate (ICancellationService x) {
                x.StateChanged += new EventHandler(this.OnCancelationServiceStateChanged);
            });
            this.RaiseStartDocumentCreation();
        }

        private void OnCancelationServiceStateChanged(object sender, EventArgs e)
        {
            this.RaiseDocumentCreatingChanged();
        }

        protected virtual void OnCreateDocumentException(object sender, ExceptionEventArgs args)
        {
            if (!args.Handled)
            {
                args.Handled = true;
                this.RaiseDocumentException(args.Exception);
            }
        }

        protected virtual void OnDocumentChanged(object sender, EventArgs e)
        {
            this.ResolveNewPages();
            this.RaiseDocumentCreatingChanged();
            this.RaiseDocumentChanged();
        }

        protected void OnProgressChanged(object sender, EventArgs e)
        {
            base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), typeof(IProgressSettings)), (MethodInfo) methodof(IProgressSettings.get_ProgressPosition)), new ParameterExpression[0]));
            if (this.PrintingSystem.Document.State == DevExpress.XtraPrinting.Native.DocumentState.Created)
            {
                TimeSpan timeout = new TimeSpan(0x186a0L);
                ThreadStart method = <>c.<>9__106_0;
                if (<>c.<>9__106_0 == null)
                {
                    ThreadStart local1 = <>c.<>9__106_0;
                    method = <>c.<>9__106_0 = delegate {
                    };
                }
                Dispatcher.CurrentDispatcher.Invoke(method, timeout, DispatcherPriority.Background, new object[0]);
            }
        }

        private void OnProgressVisibilityChanged(object sender, EventArgs e)
        {
            this.RaiseProgressChanged();
        }

        public BrickPagePair PerformSearch(TextSearchParameter parameter) => 
            this.searchHelper.FindNext(this.PrintingSystem, parameter);

        protected virtual void PreparePrintingSystem(PrintingSystemBase ps)
        {
            if (!(ps.Extender is DocumentPreviewPrintingSystemExtender))
            {
                ps.Extender = new DocumentPreviewPrintingSystemExtender(ps, this.progressReflector);
            }
            if (!ps.ExportOptions.Options.ContainsKey(typeof(XpsExportOptions)))
            {
                ps.ExportOptions.Options.Add(typeof(XpsExportOptions), new XpsExportOptions());
            }
            ps.ReplaceService<IBackgroundService>(new BackgroundService());
            if (!(this.Link is DevExpress.Xpf.Printing.LinkBase))
            {
                ps.ReplaceService<XpsExportServiceBase>(this.CreateXpsExportService());
            }
        }

        public virtual void Print(PrintOptionsViewModel model)
        {
            try
            {
                this.publishEngine.Print(model);
            }
            catch (Exception exception)
            {
                this.RaiseDocumentException(exception);
            }
        }

        public virtual void PrintDirect(string printerName = null)
        {
            try
            {
                this.publishEngine.PrintDirect(printerName);
            }
            catch (Exception exception)
            {
                this.RaiseDocumentException(exception);
            }
        }

        protected internal void RaiseDocumentChanged()
        {
            this.DocumentChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected void RaiseDocumentCreated()
        {
            this.DocumentCreated.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected void RaiseDocumentCreatingChanged()
        {
            base.RaisePropertiesChanged<bool, bool, bool, bool, bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_IsCreating)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_IsLoaded)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_IsCreated)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_CanChangePageSettings)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_CanStopPageBuilding)), new ParameterExpression[0]));
        }

        protected void RaiseDocumentException(Exception e)
        {
            this.DocumentException.Do<ExceptionEventHandler>(x => x(this, new ExceptionEventArgs(e)));
        }

        protected void RaiseProgressChanged()
        {
            base.RaisePropertiesChanged<bool, DevExpress.Xpf.Printing.PreviewControl.Native.ProgressType, int>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_InProgress)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Printing.PreviewControl.Native.ProgressType>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_ProgressType)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_ProgressPosition)), new ParameterExpression[0]));
        }

        protected void RaiseStartDocumentCreation()
        {
            this.StartDocumentCreation.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        public void ResetMarkedBricks()
        {
            Action<PrintingSystemBase> action = <>c.<>9__115_0;
            if (<>c.<>9__115_0 == null)
            {
                Action<PrintingSystemBase> local1 = <>c.<>9__115_0;
                action = <>c.<>9__115_0 = x => x.ClearMarkedBricks();
            }
            this.PrintingSystem.Do<PrintingSystemBase>(action);
        }

        protected void ResolveNewPages()
        {
            if (this.PrintingSystem != null)
            {
                if (this.locker.IsLocked)
                {
                    this.locker.Unlocked += new EventHandler(this.Locker_Unlocked);
                }
                else
                {
                    if (this.IsCreating)
                    {
                        this.DocumentStatus = this.PrintingSystem.Pages.Any<Page>() ? DevExpress.Xpf.Printing.Native.DocumentStatus.None : DevExpress.Xpf.Printing.Native.DocumentStatus.DocumentCreation;
                    }
                    else if (this.IsCreated)
                    {
                        this.DocumentStatus = this.PrintingSystem.Pages.Any<Page>() ? DevExpress.Xpf.Printing.Native.DocumentStatus.None : DevExpress.Xpf.Printing.Native.DocumentStatus.NoPages;
                    }
                    this.locker.DoLockedActionIfNotLocked(delegate {
                        Func<Page, bool> predicate = <>c.<>9__100_1;
                        if (<>c.<>9__100_1 == null)
                        {
                            Func<Page, bool> local1 = <>c.<>9__100_1;
                            predicate = <>c.<>9__100_1 = x => x.Owner != null;
                        }
                        List<Page> psPages = this.PrintingSystem.Pages.Where<Page>(predicate).ToList<Page>();
                        List<PageViewModel> collection = (from x in this.Pages
                            where x.PageIndex >= psPages.Count
                            select x).ToList<PageViewModel>();
                        collection.Reverse();
                        this.pages.RemoveRange(collection);
                        bool flag = this.pages.Count != this.Document.PageCount;
                        List<PageViewModel> list2 = new List<PageViewModel>();
                        List<PageViewModel> items = new List<PageViewModel>();
                        foreach (Page page in psPages)
                        {
                            PageViewModel item = this.pages[page.Index];
                            if (item == null)
                            {
                                list2.Add(PageViewModel.Create(page));
                                continue;
                            }
                            if (!ReferenceEquals(item.PageList, this.PrintingSystem.Pages))
                            {
                                item.PageList = this.PrintingSystem.Pages;
                                item.SyncPageInfo();
                                items.Add(item);
                                continue;
                            }
                            if (item.ShouldUpdate)
                            {
                                item.SyncPageInfo();
                                items.Add(item);
                            }
                            item.ForceInvalidate = flag;
                        }
                        this.pages.RaiseUpdateItems(items);
                        this.pages.AddRange(list2);
                    });
                }
            }
        }

        public virtual void Save(string filePath)
        {
            try
            {
                this.PrintingSystem.SaveDocument(filePath);
            }
            catch (Exception exception)
            {
                this.RaiseDocumentException(exception);
            }
        }

        public void Scale(ScaleOptionsViewModel model)
        {
            if (model.ScaleMode == ScaleMode.AdjustToPercent)
            {
                this.PrintingSystem.Document.ScaleFactor = model.ScaleFactor;
            }
            else
            {
                this.PrintingSystem.Document.AutoFitToPagesWidth = model.PagesToFit;
            }
        }

        public virtual void Send(SendOptionsViewModel options)
        {
            try
            {
                this.IsExporting = true;
                this.publishEngine.Send(options);
                this.progressReflector.MaximizeRange();
            }
            catch (Exception exception)
            {
                this.RaiseDocumentException(exception);
            }
            finally
            {
                this.IsExporting = false;
            }
        }

        public virtual void SetWatermark(DevExpress.XtraPrinting.Drawing.Watermark watermark)
        {
            this.PrintingSystem.Watermark.CopyFrom(watermark);
        }

        public void StopPageBuilding()
        {
            Action<DevExpress.XtraPrinting.Document> action = <>c.<>9__123_0;
            if (<>c.<>9__123_0 == null)
            {
                Action<DevExpress.XtraPrinting.Document> local1 = <>c.<>9__123_0;
                action = <>c.<>9__123_0 = x => x.StopPageBuilding();
            }
            this.Document.Do<DevExpress.XtraPrinting.Document>(action);
        }

        protected internal virtual void Subscribe()
        {
            this.Unsubscribe();
            this.PrintingSystem.Do<PrintingSystemBase>(delegate (PrintingSystemBase ps) {
                ps.BeforeBuildPages += new EventHandler(this.OnBeforeBuildPages);
                ps.DocumentChanged += new EventHandler(this.OnDocumentChanged);
                ps.AfterBuildPages += new EventHandler(this.OnAfterBuildPages);
                ps.CreateDocumentException += new ExceptionEventHandler(this.OnCreateDocumentException);
                ps.ProgressReflector.PositionChanged += new EventHandler(this.OnProgressChanged);
                this.ResolveNewPages();
            });
            this.progressReflector.VisibilityChanged += new EventHandler(this.OnProgressVisibilityChanged);
        }

        protected internal virtual void Unsubscribe()
        {
            this.progressReflector.VisibilityChanged -= new EventHandler(this.OnProgressVisibilityChanged);
            this.PrintingSystem.Do<PrintingSystemBase>(delegate (PrintingSystemBase ps) {
                ps.BeforeBuildPages -= new EventHandler(this.OnBeforeBuildPages);
                ps.DocumentChanged -= new EventHandler(this.OnDocumentChanged);
                ps.AfterBuildPages -= new EventHandler(this.OnAfterBuildPages);
                ps.ProgressReflector.PositionChanged -= new EventHandler(this.OnProgressChanged);
                ps.CreateDocumentException -= new ExceptionEventHandler(this.OnCreateDocumentException);
            });
        }

        private FrameworkElement VisualizePage(int pageIndex)
        {
            BrickPageVisualizer visualizer = new BrickPageVisualizer(TextMeasurementSystem.GdiPlus);
            PSPage drawingPage = (PSPage) this.PrintingSystem.Pages[pageIndex];
            drawingPage.PerformLayout(new PrintingSystemContextWrapper(this.PrintingSystem, drawingPage));
            return visualizer.Visualize(drawingPage, pageIndex, this.PrintingSystem.Pages.Count);
        }

        protected DocumentPreviewProgressReflector ProgressReflector =>
            this.progressReflector;

        protected internal ILink Link =>
            this.link;

        public PrintingSystemBase PrintingSystem { get; protected set; }

        protected internal DevExpress.XtraPrinting.Document Document
        {
            get
            {
                Func<PrintingSystemBase, DevExpress.XtraPrinting.Document> evaluator = <>c.<>9__35_0;
                if (<>c.<>9__35_0 == null)
                {
                    Func<PrintingSystemBase, DevExpress.XtraPrinting.Document> local1 = <>c.<>9__35_0;
                    evaluator = <>c.<>9__35_0 = x => x.Document;
                }
                return this.PrintingSystem.Return<PrintingSystemBase, DevExpress.XtraPrinting.Document>(evaluator, (<>c.<>9__35_1 ??= ((Func<DevExpress.XtraPrinting.Document>) (() => null))));
            }
        }

        internal DevExpress.Xpf.DocumentViewer.NavigationState NavigationState { get; set; }

        protected internal IBrickPaintService PaintService
        {
            get => 
                this.paintService ?? this.PrintingSystem.With<PrintingSystemBase, IBrickPaintService>(delegate (PrintingSystemBase x) {
                    IBrickPaintService service;
                    this.paintService = service = new BrickPaintService(x.EditingFields, x, -1f);
                    return service;
                });
            protected set => 
                this.paintService = value;
        }

        public DevExpress.Xpf.Printing.Native.DocumentStatus DocumentStatus
        {
            get => 
                this.documentStatus;
            protected set
            {
                this.documentStatus = value;
                base.RaisePropertyChanged<DevExpress.Xpf.Printing.Native.DocumentStatus>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Printing.Native.DocumentStatus>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_DocumentStatus)), new ParameterExpression[0]));
            }
        }

        public bool IsLoaded =>
            this.PrintingSystem != null;

        public bool IsCreated =>
            this.IsLoaded && (!this.IsCreating && (this.Pages.Count > 0));

        public bool IsCreating
        {
            get
            {
                Func<DevExpress.XtraPrinting.Document, bool> evaluator = <>c.<>9__51_0;
                if (<>c.<>9__51_0 == null)
                {
                    Func<DevExpress.XtraPrinting.Document, bool> local1 = <>c.<>9__51_0;
                    evaluator = <>c.<>9__51_0 = x => x.IsCreating;
                }
                return this.Document.Return<DevExpress.XtraPrinting.Document, bool>(evaluator, (<>c.<>9__51_1 ??= () => false));
            }
        }

        protected bool IsExporting
        {
            get => 
                this.isExporting;
            set => 
                base.SetProperty<bool>(ref this.isExporting, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_IsExporting)), new ParameterExpression[0]), delegate {
                    CommandManager.InvalidateRequerySuggested();
                    this.RaiseProgressChanged();
                });
        }

        string DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel.DefaultFileName =>
            this.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName;

        string DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel.InitialDirectory =>
            this.PrintingSystem.ExportOptions.PrintPreview.DefaultDirectory;

        ExportFormat DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel.DefaultExportFormat
        {
            get => 
                ExportFormatConverter.ToExportFormat(this.PrintingSystem.ExportOptions.PrintPreview.DefaultExportFormat);
            set => 
                this.PrintingSystem.ExportOptions.PrintPreview.DefaultExportFormat = ExportFormatConverter.ToExportCommand(value);
        }

        ExportFormat DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel.DefaultSendFormat
        {
            get => 
                ExportFormatConverter.ToExportFormat(this.PrintingSystem.ExportOptions.PrintPreview.DefaultSendFormat);
            set => 
                this.PrintingSystem.ExportOptions.PrintPreview.DefaultSendFormat = ExportFormatConverter.ToExportCommand(value);
        }

        IEnumerable<DevExpress.Xpf.DocumentViewer.IPage> DevExpress.Xpf.DocumentViewer.IDocument.Pages =>
            (IEnumerable<DevExpress.Xpf.DocumentViewer.IPage>) this.pages;

        public ObservableCollection<PageViewModel> Pages =>
            this.pages;

        public DevExpress.XtraPrinting.Drawing.Watermark Watermark
        {
            get
            {
                Func<PrintingSystemBase, DevExpress.XtraPrinting.Drawing.Watermark> evaluator = <>c.<>9__70_0;
                if (<>c.<>9__70_0 == null)
                {
                    Func<PrintingSystemBase, DevExpress.XtraPrinting.Drawing.Watermark> local1 = <>c.<>9__70_0;
                    evaluator = <>c.<>9__70_0 = x => x.Watermark;
                }
                return this.PrintingSystem.Return<PrintingSystemBase, DevExpress.XtraPrinting.Drawing.Watermark>(evaluator, (<>c.<>9__70_1 ??= ((Func<DevExpress.XtraPrinting.Drawing.Watermark>) (() => null))));
            }
        }

        public bool CanChangePageSettings
        {
            get
            {
                Func<bool> fallback = <>c.<>9__72_1;
                if (<>c.<>9__72_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__72_1;
                    fallback = <>c.<>9__72_1 = () => false;
                }
                return this.Document.Return<DevExpress.XtraPrinting.Document, bool>(x => (x.IsCreated && (x.CanChangePageSettings && !(this.source is LegacyPrintableComponentLink))), fallback);
            }
        }

        public XtraPageSettingsBase PageSettings
        {
            get
            {
                Func<PrintingSystemBase, XtraPageSettingsBase> evaluator = <>c.<>9__74_0;
                if (<>c.<>9__74_0 == null)
                {
                    Func<PrintingSystemBase, XtraPageSettingsBase> local1 = <>c.<>9__74_0;
                    evaluator = <>c.<>9__74_0 = x => x.PageSettings;
                }
                return this.PrintingSystem.Return<PrintingSystemBase, XtraPageSettingsBase>(evaluator, (<>c.<>9__74_1 ??= ((Func<XtraPageSettingsBase>) (() => null))));
            }
        }

        public IEnumerable<EditingField> EditingFields
        {
            get => 
                this.editingFields;
            private set => 
                base.SetProperty<IEnumerable<EditingField>>(ref this.editingFields, value, System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<EditingField>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentViewModel)), (MethodInfo) methodof(DocumentViewModel.get_EditingFields)), new ParameterExpression[0]));
        }

        public bool InProgress =>
            this.GetIsInProgress();

        public DevExpress.Xpf.Printing.PreviewControl.Native.ProgressType ProgressType =>
            this.IsProgressMarquee() ? DevExpress.Xpf.Printing.PreviewControl.Native.ProgressType.Marquee : DevExpress.Xpf.Printing.PreviewControl.Native.ProgressType.Default;

        public int ProgressPosition
        {
            get
            {
                Func<PrintingSystemBase, int> evaluator = <>c.<>9__85_0;
                if (<>c.<>9__85_0 == null)
                {
                    Func<PrintingSystemBase, int> local1 = <>c.<>9__85_0;
                    evaluator = <>c.<>9__85_0 = x => x.ProgressReflector.PositionCore;
                }
                return this.PrintingSystem.Return<PrintingSystemBase, int>(evaluator, (<>c.<>9__85_1 ??= () => 0));
            }
        }

        public bool HasBookmarks
        {
            get
            {
                Func<bool> fallback = <>c.<>9__110_1;
                if (<>c.<>9__110_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__110_1;
                    fallback = <>c.<>9__110_1 = () => false;
                }
                return this.Document.Return<DevExpress.XtraPrinting.Document, bool>(x => (this.IsCreated && (x.BookmarkNodes.Count != 0)), fallback);
            }
        }

        public bool CanStopPageBuilding
        {
            get
            {
                Func<ICancellationService, bool> evaluator = <>c.<>9__125_0;
                if (<>c.<>9__125_0 == null)
                {
                    Func<ICancellationService, bool> local1 = <>c.<>9__125_0;
                    evaluator = <>c.<>9__125_0 = x => x.CanBeCanceled();
                }
                return this.cancellationService.Return<ICancellationService, bool>(evaluator, () => this.IsCreating);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentViewModel.<>c <>9 = new DocumentViewModel.<>c();
            public static Func<PrintingSystemBase, Document> <>9__35_0;
            public static Func<Document> <>9__35_1;
            public static Func<Document, bool> <>9__51_0;
            public static Func<bool> <>9__51_1;
            public static Func<PrintingSystemBase, Watermark> <>9__70_0;
            public static Func<Watermark> <>9__70_1;
            public static Func<bool> <>9__72_1;
            public static Func<PrintingSystemBase, XtraPageSettingsBase> <>9__74_0;
            public static Func<XtraPageSettingsBase> <>9__74_1;
            public static Func<DocumentPreviewProgressReflector, bool> <>9__81_1;
            public static Func<bool> <>9__81_3;
            public static Func<PrintingSystemBase, int> <>9__85_0;
            public static Func<int> <>9__85_1;
            public static Func<PrintingSystemBase, bool> <>9__86_0;
            public static Func<bool> <>9__86_1;
            public static Func<ILink, PrintingSystemBase> <>9__90_0;
            public static Func<PrintingSystemBase> <>9__90_1;
            public static Func<Page, bool> <>9__100_1;
            public static ThreadStart <>9__106_0;
            public static Func<bool> <>9__110_1;
            public static Action<PrintingSystemBase> <>9__115_0;
            public static Action<Document> <>9__123_0;
            public static Func<ICancellationService, bool> <>9__125_0;

            internal bool <get_CanChangePageSettings>b__72_1() => 
                false;

            internal bool <get_CanStopPageBuilding>b__125_0(ICancellationService x) => 
                x.CanBeCanceled();

            internal Document <get_Document>b__35_0(PrintingSystemBase x) => 
                x.Document;

            internal Document <get_Document>b__35_1() => 
                null;

            internal bool <get_HasBookmarks>b__110_1() => 
                false;

            internal bool <get_IsCreating>b__51_0(Document x) => 
                x.IsCreating;

            internal bool <get_IsCreating>b__51_1() => 
                false;

            internal XtraPageSettingsBase <get_PageSettings>b__74_0(PrintingSystemBase x) => 
                x.PageSettings;

            internal XtraPageSettingsBase <get_PageSettings>b__74_1() => 
                null;

            internal int <get_ProgressPosition>b__85_0(PrintingSystemBase x) => 
                x.ProgressReflector.PositionCore;

            internal int <get_ProgressPosition>b__85_1() => 
                0;

            internal Watermark <get_Watermark>b__70_0(PrintingSystemBase x) => 
                x.Watermark;

            internal Watermark <get_Watermark>b__70_1() => 
                null;

            internal bool <GetIsInProgress>b__81_1(DocumentPreviewProgressReflector x) => 
                x.Visible;

            internal bool <GetIsInProgress>b__81_3() => 
                false;

            internal bool <IsProgressMarquee>b__86_0(PrintingSystemBase x) => 
                (x.ProgressReflector.Ranges.Count != 0) && float.IsNaN(x.ProgressReflector.Ranges[0]);

            internal bool <IsProgressMarquee>b__86_1() => 
                false;

            internal PrintingSystemBase <Load>b__90_0(ILink x) => 
                (PrintingSystemBase) x.PrintingSystem;

            internal PrintingSystemBase <Load>b__90_1() => 
                null;

            internal void <OnProgressChanged>b__106_0()
            {
            }

            internal void <ResetMarkedBricks>b__115_0(PrintingSystemBase x)
            {
                x.ClearMarkedBricks();
            }

            internal bool <ResolveNewPages>b__100_1(Page x) => 
                x.Owner != null;

            internal void <StopPageBuilding>b__123_0(Document x)
            {
                x.StopPageBuilding();
            }
        }
    }
}

