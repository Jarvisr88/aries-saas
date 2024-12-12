namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    public class PdfDocumentViewModel : BindableBase, DevExpress.Xpf.DocumentViewer.IDocument, IPdfDocument, IDisposable
    {
        private DevExpress.Pdf.PdfDocument document;
        private PdfDocumentStateController documentStateController;
        private readonly PdfFontStorage fontStorage = new PdfFontStorage();
        private IList<PdfPageViewModel> pages;
        private IPdfDocumentSelectionResults documentSelection;
        private readonly IPdfViewerController viewerController;
        private readonly PdfViewerBackend viewerBackend;
        private string filePath;
        private long fileSize;
        private bool isLoaded;
        private bool isLoadingFailed;
        private bool isDocumentModified;
        private bool isCancelled;

        public event EventHandler<DocumentProgressChangedEventArgs> DocumentProgressChanged;

        public event EventHandler<RequestPasswordEventArgs> RequestPassword;

        public PdfDocumentViewModel(IPdfViewerController viewerController, PdfViewerBackend viewerBackend)
        {
            this.viewerController = viewerController;
            this.viewerBackend = viewerBackend;
        }

        public void ApplyFormData(PdfFormData formData)
        {
            Func<PdfDocumentState, PdfFormData> evaluator = <>c.<>9__115_0;
            if (<>c.<>9__115_0 == null)
            {
                Func<PdfDocumentState, PdfFormData> local1 = <>c.<>9__115_0;
                evaluator = <>c.<>9__115_0 = x => x.FormData;
            }
            this.DocumentState.With<PdfDocumentState, PdfFormData>(evaluator).Do<PdfFormData>(x => x.Apply(formData));
        }

        public virtual IEnumerable<int> CalcPrintPages(IEnumerable<PdfOutlineTreeListItem> items, bool useAsRange) => 
            ((this.DocumentState == null) || !this.IsLoaded) ? Enumerable.Empty<int>() : ((IEnumerable<int>) this.DocumentState.Outlines.GetPrintPageNumbers(items, useAsRange));

        public void CancelLoadDocument()
        {
            this.IsCancelled = true;
            Action<CancellationTokenSource> action = <>c.<>9__99_0;
            if (<>c.<>9__99_0 == null)
            {
                Action<CancellationTokenSource> local1 = <>c.<>9__99_0;
                action = <>c.<>9__99_0 = x => x.Cancel();
            }
            this.cancellationTokenSource.Do<CancellationTokenSource>(action);
            this.DocumentProgressChanged.Do<EventHandler<DocumentProgressChangedEventArgs>>(x => x(this, new DocumentProgressChangedEventArgs(true, 0L, 0L)));
        }

        public virtual bool CanPrintPages(IEnumerable<PdfOutlineTreeListItem> items, bool useAsRange)
        {
            if (!this.IsLoaded)
            {
                return false;
            }
            Func<bool> fallback = <>c.<>9__118_1;
            if (<>c.<>9__118_1 == null)
            {
                Func<bool> local1 = <>c.<>9__118_1;
                fallback = <>c.<>9__118_1 = () => false;
            }
            return this.DocumentState.Return<PdfDocumentState, bool>(x => x.Outlines.CanPrintPages(items, useAsRange), fallback);
        }

        public virtual IEnumerable<PdfFileAttachmentListItem> CreateAttachments()
        {
            Func<PdfDocumentState, IList<PdfFileAttachmentListItem>> evaluator = <>c.<>9__121_0;
            if (<>c.<>9__121_0 == null)
            {
                Func<PdfDocumentState, IList<PdfFileAttachmentListItem>> local1 = <>c.<>9__121_0;
                evaluator = <>c.<>9__121_0 = x => x.FileAttachments;
            }
            IList<PdfFileAttachmentListItem> collection = this.DocumentState.With<PdfDocumentState, IList<PdfFileAttachmentListItem>>(evaluator);
            return ((collection != null) ? new ObservableCollection<PdfFileAttachmentListItem>(collection) : new ObservableCollection<PdfFileAttachmentListItem>());
        }

        public virtual BitmapSource CreateBitmap(int pageIndex, int largestEdgeLength)
        {
            if (largestEdgeLength < 1)
            {
                throw new ArgumentOutOfRangeException("largestEdgeLength");
            }
            using (Bitmap bitmap = this.viewerBackend.CreateBitmap(pageIndex + 1, largestEdgeLength))
            {
                return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public virtual IEnumerable<PdfOutlineTreeListItem> CreateOutlines() => 
            !this.IsLoaded ? Enumerable.Empty<PdfOutlineTreeListItem>() : new ObservableCollection<PdfOutlineTreeListItem>(this.DocumentState.Outlines);

        string IPdfDocument.GetText(PdfDocumentArea area) => 
            this.GetTextInternal(area);

        string IPdfDocument.GetText(PdfDocumentPosition start, PdfDocumentPosition end) => 
            this.GetTextInternal(start, end);

        PdfDocumentContent IPdfDocument.HitTest(PdfDocumentPosition position) => 
            this.DataSelector.GetContentInfo(position);

        PdfTextSearchResults IPdfDocument.PerformSearch(TextSearchParameter searchParameter)
        {
            this.SelectionResults = null;
            PdfTextSearchParameters parameters1 = new PdfTextSearchParameters();
            parameters1.CaseSensitive = searchParameter.IsCaseSensitive;
            parameters1.Direction = (PdfTextSearchDirection) searchParameter.SearchDirection;
            parameters1.WholeWords = searchParameter.WholeWord;
            PdfTextSearchParameters parameters = parameters1;
            return this.documentStateController.FindText(searchParameter.Text, parameters, searchParameter.CurrentPage, null);
        }

        void IPdfDocument.PerformSelection(PdfDocumentSelectionParameter selectionParameter)
        {
            if (selectionParameter == null)
            {
                this.DataSelector.ClearSelection();
            }
            else
            {
                switch (selectionParameter.SelectionAction)
                {
                    case PdfSelectionAction.SelectText:
                        this.DataSelector.SelectText(selectionParameter.Position, selectionParameter.EndPosition);
                        return;

                    case PdfSelectionAction.SelectViaArea:
                        this.DataSelector.Select(selectionParameter.Area);
                        return;

                    case PdfSelectionAction.ClearSelection:
                        this.DataSelector.ClearSelection();
                        return;

                    case PdfSelectionAction.SelectAllText:
                        this.DataSelector.SelectAllText();
                        return;

                    case PdfSelectionAction.StartSelection:
                        this.DataSelector.StartSelection(selectionParameter.Position);
                        return;

                    case PdfSelectionAction.ContinueSelection:
                        this.DataSelector.PerformSelection(selectionParameter.Position);
                        return;

                    case PdfSelectionAction.EndSelection:
                        this.DataSelector.EndSelection(selectionParameter.Position);
                        return;

                    case PdfSelectionAction.SelectWord:
                        this.DataSelector.SelectWord(selectionParameter.Position);
                        return;

                    case PdfSelectionAction.SelectLine:
                        this.DataSelector.SelectLine(selectionParameter.Position);
                        return;

                    case PdfSelectionAction.SelectPage:
                        this.DataSelector.SelectPage(selectionParameter.Position);
                        return;

                    case PdfSelectionAction.SelectImage:
                        this.DataSelector.StartSelection(selectionParameter.Position);
                        this.DataSelector.EndSelection(selectionParameter.Position);
                        return;

                    case PdfSelectionAction.SetSelection:
                        break;

                    case PdfSelectionAction.SelectLeft:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.Left);
                        return;

                    case PdfSelectionAction.SelectUp:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.Up);
                        return;

                    case PdfSelectionAction.SelectRight:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.Right);
                        return;

                    case PdfSelectionAction.SelectDown:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.Down);
                        return;

                    case PdfSelectionAction.SelectLineStart:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.LineStart);
                        return;

                    case PdfSelectionAction.SelectLineEnd:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.LineEnd);
                        return;

                    case PdfSelectionAction.SelectNextWord:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.NextWord);
                        return;

                    case PdfSelectionAction.SelectPreviousWord:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.PreviousWord);
                        return;

                    case PdfSelectionAction.SelectDocumentStart:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.DocumentStart);
                        return;

                    case PdfSelectionAction.SelectDocumentEnd:
                        this.DataSelector.SelectWithCaret(PdfMovementDirection.DocumentEnd);
                        return;

                    case PdfSelectionAction.MoveLeft:
                        this.MoveCaret(PdfMovementDirection.Left);
                        return;

                    case PdfSelectionAction.MoveUp:
                        this.MoveCaret(PdfMovementDirection.Up);
                        return;

                    case PdfSelectionAction.MoveRight:
                        this.MoveCaret(PdfMovementDirection.Right);
                        return;

                    case PdfSelectionAction.MoveDown:
                        this.MoveCaret(PdfMovementDirection.Down);
                        return;

                    case PdfSelectionAction.MoveLineStart:
                        this.MoveCaret(PdfMovementDirection.LineStart);
                        return;

                    case PdfSelectionAction.MoveLineEnd:
                        this.MoveCaret(PdfMovementDirection.LineEnd);
                        return;

                    case PdfSelectionAction.MoveNextWord:
                        this.MoveCaret(PdfMovementDirection.NextWord);
                        return;

                    case PdfSelectionAction.MovePreviousWord:
                        this.MoveCaret(PdfMovementDirection.PreviousWord);
                        break;

                    case PdfSelectionAction.MoveDocumentStart:
                        this.MoveCaret(PdfMovementDirection.DocumentStart);
                        return;

                    case PdfSelectionAction.MoveDocumentEnd:
                        this.MoveCaret(PdfMovementDirection.DocumentEnd);
                        return;

                    default:
                        return;
                }
            }
        }

        void IPdfDocument.SetCurrentPage(int index, bool allowCurrentPageHighlighting)
        {
            this.SetCurrentPageInternal(index, allowCurrentPageHighlighting);
        }

        void IPdfDocument.UpdateDocumentRotateAngle(double angle)
        {
            this.viewerBackend.RotationAngle = (int) angle;
        }

        void IPdfDocument.UpdateDocumentSelection()
        {
            this.SelectionResults = this.DocumentState.SelectionState.HasSelection ? new PdfDocumentSelectionResults(this) : null;
        }

        public virtual IPdfDocumentProperties GetDocumentProperties() => 
            new PdfDocumentProperties(this);

        protected virtual Stream GetDocumentStream(object source) => 
            new StreamProvider().GetStream(source);

        public PdfFormData GetFormData()
        {
            Func<PdfDocumentState, PdfFormData> evaluator = <>c.<>9__117_0;
            if (<>c.<>9__117_0 == null)
            {
                Func<PdfDocumentState, PdfFormData> local1 = <>c.<>9__117_0;
                evaluator = <>c.<>9__117_0 = x => x.FormData;
            }
            return this.DocumentState.With<PdfDocumentState, PdfFormData>(evaluator);
        }

        public virtual IPdfDocumentOutlinesViewerProperties GetOutlinesViewerProperties() => 
            new PdfDocumentOutlinesViewerProperties();

        private SecureString GetPassword(string path, int tryNumber)
        {
            RequestPasswordEventArgs args = this.RaiseRequestPasswordEvent(path, tryNumber);
            return (!args.Handled ? null : args.Password);
        }

        protected virtual string GetTextInternal(PdfDocumentArea area)
        {
            Func<PdfTextSelection, string> evaluator = <>c.<>9__87_0;
            if (<>c.<>9__87_0 == null)
            {
                Func<PdfTextSelection, string> local1 = <>c.<>9__87_0;
                evaluator = <>c.<>9__87_0 = x => x.Text;
            }
            return this.DataSelector.GetTextSelection(area).Return<PdfTextSelection, string>(evaluator, (<>c.<>9__87_1 ??= () => string.Empty));
        }

        protected virtual string GetTextInternal(PdfDocumentPosition start, PdfDocumentPosition end)
        {
            Func<PdfTextSelection, string> evaluator = <>c.<>9__88_0;
            if (<>c.<>9__88_0 == null)
            {
                Func<PdfTextSelection, string> local1 = <>c.<>9__88_0;
                evaluator = <>c.<>9__88_0 = x => x.Text;
            }
            return this.DataSelector.GetTextSelection(start, end).Return<PdfTextSelection, string>(evaluator, (<>c.<>9__88_1 ??= () => string.Empty));
        }

        private void Initialize()
        {
            if (this.document != null)
            {
                ObservableCollection<PdfPageViewModel> observables = new ObservableCollection<PdfPageViewModel>();
                int num = 0;
                this.viewerBackend.SetDocument(this.document);
                foreach (PdfPage page in this.document.Pages)
                {
                    observables.Add(new PdfPageViewModel(page, num++));
                }
                this.Pages = observables;
                this.documentStateController = new PdfDocumentStateController(this.viewerController, this.DocumentState);
                this.IsLoaded = true;
            }
        }

        private bool IsUserStream(object source) => 
            source is Stream;

        public void LoadDocument(object source, bool detachStreamOnLoadComplete)
        {
            if (Net45Detector.IsNet45())
            {
                this.LoadDocumentAsync(source, detachStreamOnLoadComplete);
            }
            else
            {
                this.LoadDocumentSync(source, detachStreamOnLoadComplete);
                this.DocumentProgressChanged.Do<EventHandler<DocumentProgressChangedEventArgs>>(x => x(this, new DocumentProgressChangedEventArgs(true, 0L, 0L)));
            }
        }

        protected virtual void LoadDocument(Stream stream, string path, bool detachStreamOnLoadComplete, bool internalDispose, Func<string, int, SecureString> getPasswordAction)
        {
            Stream stream2 = this.ValidateStream(stream);
            this.document = PdfDocumentReader.Read(stream2, detachStreamOnLoadComplete, number => PdfSecureStringAccessor.FromSecureString(getPasswordAction(path, number)));
            Func<CancellationTokenSource, bool> evaluator = <>c.<>9__102_1;
            if (<>c.<>9__102_1 == null)
            {
                Func<CancellationTokenSource, bool> local1 = <>c.<>9__102_1;
                evaluator = <>c.<>9__102_1 = x => x.IsCancellationRequested;
            }
            if (!this.cancellationTokenSource.Return<CancellationTokenSource, bool>(evaluator, (<>c.<>9__102_2 ??= () => false)))
            {
                this.filePath = path;
                this.fileSize = stream2.Length;
                if (!detachStreamOnLoadComplete & internalDispose)
                {
                    this.FileStream = stream;
                }
                else if (internalDispose)
                {
                    stream.Dispose();
                }
            }
        }

        private void LoadDocumentAsync(object source, bool detachStreamOnLoadComplete)
        {
            <>c__DisplayClass97_0 class_;
            this.cancellationTokenSource = new CancellationTokenSource();
            LoadDocumentState state1 = new LoadDocumentState();
            state1.Current = new DispatcherSynchronizationContext();
            LoadDocumentState state = state1;
            Task.Factory.StartNew<Stream>(() => this.GetDocumentStream(source)).ContinueWith(delegate (Task<Stream> task, object state) {
                if (task.IsFaulted && (task.Exception != null))
                {
                    throw task.Exception;
                }
                GetPasswordState getPasswordState = new GetPasswordState(new Func<string, int, SecureString>(class_.GetPassword));
                SynchronizationContext context = ((LoadDocumentState) state).Current;
                Func<string, int, SecureString> getPasswordAction = delegate (string path, int number) {
                    context.Send(currentState => ((GetPasswordState) currentState).GetPassword(path, number), getPasswordState);
                    return getPasswordState.Password;
                };
                this.LoadDocument(task.Result, source as string, detachStreamOnLoadComplete, !this.IsUserStream(source), getPasswordAction);
            }, state, this.cancellationTokenSource.Token, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default).ContinueWith(delegate (Task task) {
                if (task.IsFaulted && (task.Exception != null))
                {
                    throw task.Exception;
                }
                SynchronizationContext.Current.Post(state => this.Initialize(), null);
            }, this.cancellationTokenSource.Token, TaskContinuationOptions.NotOnCanceled, TaskScheduler.FromCurrentSynchronizationContext()).ContinueWith(delegate (Task task) {
                this.IsLoadingFailed = task.IsFaulted || task.IsCanceled;
                this.LoadingException = task.Exception;
                SynchronizationContext.Current.Post(state => this.DocumentProgressChanged.Do<EventHandler<DocumentProgressChangedEventArgs>>(x => x(this, new DocumentProgressChangedEventArgs(true, 0L, 0L))), null);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void LoadDocumentSync(object source, bool detachStreamOnLoadComplete)
        {
            this.LoadDocument(this.GetDocumentStream(source), source as string, detachStreamOnLoadComplete, !this.IsUserStream(source), new Func<string, int, SecureString>(this.GetPassword));
            this.Initialize();
            this.DocumentProgressChanged.Do<EventHandler<DocumentProgressChangedEventArgs>>(x => x(this, new DocumentProgressChangedEventArgs(true, 0L, 0L)));
        }

        private void MoveCaret(PdfMovementDirection direction)
        {
            if (this.Caret != null)
            {
                this.DataSelector.MoveCaret(direction);
            }
        }

        public virtual void NavigateToOutline(PdfOutlineTreeListItem item)
        {
            if (this.IsLoaded)
            {
                this.documentStateController.ExecuteInteractiveOperation(item.InteractiveOperation);
            }
        }

        public void Print(PdfPrinterSettings print, int currentPageNumber, bool showPrintStatus)
        {
            this.viewerBackend.Print(print, currentPageNumber, showPrintStatus);
        }

        [Obsolete("Use the Print(PdfPrinterSettings print, int currentPageNumber, bool showPrintStatus) overload of this method instead.")]
        public void Print(PdfPrinterSettings print, int currentPageNumber, bool showPrintStatus, int maxPrintingDpi)
        {
            this.Print(print, currentPageNumber, showPrintStatus);
        }

        private RequestPasswordEventArgs RaiseRequestPasswordEvent(string path, int tryNumber)
        {
            RequestPasswordEventArgs e = new RequestPasswordEventArgs(path, tryNumber);
            if (this.RequestPassword != null)
            {
                this.RequestPassword(this, e);
            }
            return e;
        }

        public void SaveDocument(Stream stream, bool detachStreamOnLoadComplete, PdfSaveOptions options)
        {
            this.SaveDocument(stream, detachStreamOnLoadComplete, options, null);
        }

        public void SaveDocument(string path, bool detachStreamOnLoadComplete, PdfSaveOptions options)
        {
            this.SaveDocument(path, detachStreamOnLoadComplete, options, null);
        }

        internal void SaveDocument(Stream stream, bool detachStreamOnLoadComplete, PdfSaveOptions options, Action<int> progressChanged)
        {
            PdfDocumentWritingHelper.Write(stream, this.document, options, detachStreamOnLoadComplete, progressChanged);
        }

        internal void SaveDocument(string path, bool detachStreamOnLoadComplete, PdfSaveOptions options, Action<int> progressChanged)
        {
            try
            {
                System.IO.FileStream stream = PdfDocumentWritingHelper.Write(path, this.document, options, detachStreamOnLoadComplete, this.FileStream, progressChanged);
                this.FileStream = stream;
            }
            catch (Exception exception)
            {
                if (!(exception is PdfFileOverwriteException))
                {
                    throw;
                }
                if (this.FileStream != null)
                {
                    this.FileStream = new System.IO.FileStream(this.filePath, FileMode.Open, FileAccess.Read);
                    this.document.DocumentCatalog.Objects.UpdateStream(this.FileStream);
                }
                throw exception.InnerException;
            }
        }

        public void SaveFormData(string filePath, PdfFormDataFormat formDataFormat)
        {
            Func<PdfDocumentState, PdfFormData> evaluator = <>c.<>9__116_0;
            if (<>c.<>9__116_0 == null)
            {
                Func<PdfDocumentState, PdfFormData> local1 = <>c.<>9__116_0;
                evaluator = <>c.<>9__116_0 = x => x.FormData;
            }
            this.DocumentState.With<PdfDocumentState, PdfFormData>(evaluator).Do<PdfFormData>(x => x.Save(filePath, formDataFormat));
        }

        protected virtual void SetCurrentPageInternal(int index, bool allowCurrentPageHighlighting)
        {
            Func<IList<PdfPageViewModel>, bool> evaluator = <>c.<>9__89_0;
            if (<>c.<>9__89_0 == null)
            {
                Func<IList<PdfPageViewModel>, bool> local1 = <>c.<>9__89_0;
                evaluator = <>c.<>9__89_0 = x => x.Any<PdfPageViewModel>();
            }
            if (this.Pages.Return<IList<PdfPageViewModel>, bool>(evaluator, <>c.<>9__89_1 ??= () => false))
            {
                foreach (PdfPageViewModel model in this.Pages)
                {
                    model.IsSelected = allowCurrentPageHighlighting && (model.PageIndex == index);
                }
            }
        }

        void IDisposable.Dispose()
        {
            this.viewerBackend.SetDocument(null);
            Action<Stream> action = <>c.<>9__111_0;
            if (<>c.<>9__111_0 == null)
            {
                Action<Stream> local1 = <>c.<>9__111_0;
                action = <>c.<>9__111_0 = x => x.Dispose();
            }
            this.FileStream.Do<Stream>(action);
            this.fontStorage.Dispose();
            Action<PdfDocumentStateController> action2 = <>c.<>9__111_1;
            if (<>c.<>9__111_1 == null)
            {
                Action<PdfDocumentStateController> local2 = <>c.<>9__111_1;
                action2 = <>c.<>9__111_1 = x => x.Dispose();
            }
            this.documentStateController.Do<PdfDocumentStateController>(action2);
        }

        private Stream ValidateStream(Stream stream) => 
            !stream.CanSeek ? new MemoryStream(stream.CopyAllBytes()) : stream;

        private PdfDataSelector DataSelector =>
            this.documentStateController.DataSelector;

        public PdfDocumentStateController DocumentStateController =>
            this.documentStateController;

        public IPdfDocumentSelectionResults SelectionResults
        {
            get => 
                this.documentSelection;
            private set => 
                base.SetProperty<IPdfDocumentSelectionResults>(ref this.documentSelection, value, System.Linq.Expressions.Expression.Lambda<Func<IPdfDocumentSelectionResults>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfDocumentViewModel)), (MethodInfo) methodof(PdfDocumentViewModel.get_SelectionResults)), new ParameterExpression[0]));
        }

        public IList<PdfPageViewModel> Pages
        {
            get => 
                this.pages;
            private set => 
                base.SetProperty<IList<PdfPageViewModel>>(ref this.pages, value, System.Linq.Expressions.Expression.Lambda<Func<IList<PdfPageViewModel>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfDocumentViewModel)), (MethodInfo) methodof(PdfDocumentViewModel.get_Pages)), new ParameterExpression[0]));
        }

        public bool IsLoadingFailed
        {
            get => 
                this.isLoadingFailed;
            private set => 
                base.SetProperty<bool>(ref this.isLoadingFailed, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfDocumentViewModel)), (MethodInfo) methodof(PdfDocumentViewModel.get_IsLoadingFailed)), new ParameterExpression[0]));
        }

        public bool IsLoaded
        {
            get => 
                this.isLoaded;
            private set => 
                base.SetProperty<bool>(ref this.isLoaded, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfDocumentViewModel)), (MethodInfo) methodof(PdfDocumentViewModel.get_IsLoaded)), new ParameterExpression[0]));
        }

        public bool IsCancelled
        {
            get => 
                this.isCancelled;
            private set => 
                base.SetProperty<bool>(ref this.isCancelled, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfDocumentViewModel)), (MethodInfo) methodof(PdfDocumentViewModel.get_IsCancelled)), new ParameterExpression[0]));
        }

        public bool IsDocumentModified
        {
            get => 
                this.isDocumentModified;
            internal set => 
                base.SetProperty<bool>(ref this.isDocumentModified, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfDocumentViewModel)), (MethodInfo) methodof(PdfDocumentViewModel.get_IsDocumentModified)), new ParameterExpression[0]));
        }

        public PdfCaret Caret
        {
            get
            {
                Func<PdfDocumentState, PdfSelectionState> evaluator = <>c.<>9__38_0;
                if (<>c.<>9__38_0 == null)
                {
                    Func<PdfDocumentState, PdfSelectionState> local1 = <>c.<>9__38_0;
                    evaluator = <>c.<>9__38_0 = x => x.SelectionState;
                }
                Func<PdfSelectionState, PdfCaret> func2 = <>c.<>9__38_1;
                if (<>c.<>9__38_1 == null)
                {
                    Func<PdfSelectionState, PdfCaret> local2 = <>c.<>9__38_1;
                    func2 = <>c.<>9__38_1 = x => x.Caret;
                }
                return this.DocumentState.With<PdfDocumentState, PdfSelectionState>(evaluator).With<PdfSelectionState, PdfCaret>(func2);
            }
        }

        public long ImageCacheSize
        {
            get => 
                this.viewerBackend.ImageCacheSize;
            set => 
                this.viewerBackend.ImageCacheSize = value;
        }

        bool IPdfDocument.HasSelection =>
            this.SelectionResults != null;

        public bool HasOutlines
        {
            get
            {
                if (!this.IsLoaded)
                {
                    return false;
                }
                Func<PdfDocumentState, bool> evaluator = <>c.<>9__46_0;
                if (<>c.<>9__46_0 == null)
                {
                    Func<PdfDocumentState, bool> local1 = <>c.<>9__46_0;
                    evaluator = <>c.<>9__46_0 = x => (x.Outlines != null) && (x.Outlines.Count > 0);
                }
                return this.DocumentState.If<PdfDocumentState>(evaluator).ReturnSuccess<PdfDocumentState>();
            }
        }

        public bool HasAttachments
        {
            get
            {
                if (!this.IsLoaded)
                {
                    return false;
                }
                Func<PdfDocumentState, bool> evaluator = <>c.<>9__48_0;
                if (<>c.<>9__48_0 == null)
                {
                    Func<PdfDocumentState, bool> local1 = <>c.<>9__48_0;
                    evaluator = <>c.<>9__48_0 = x => (x.FileAttachments != null) && (x.FileAttachments.Count > 0);
                }
                return this.DocumentState.If<PdfDocumentState>(evaluator).ReturnSuccess<PdfDocumentState>();
            }
        }

        IEnumerable<IPdfPage> IPdfDocument.Pages =>
            (IEnumerable<IPdfPage>) this.Pages;

        internal DevExpress.Pdf.PdfDocument PdfDocument =>
            this.document;

        public string FilePath =>
            this.filePath;

        public long FileSize =>
            this.fileSize;

        private Stream FileStream { get; set; }

        private CancellationTokenSource cancellationTokenSource { get; set; }

        public PdfDocumentState DocumentState =>
            this.viewerBackend.DocumentState;

        public bool HasInteractiveForm =>
            (this.document != null) && (this.document.AcroForm != null);

        internal PdfViewerBackend ViewerBackend =>
            this.viewerBackend;

        internal Exception LoadingException { get; private set; }

        IEnumerable<IPage> DevExpress.Xpf.DocumentViewer.IDocument.Pages
        {
            get
            {
                Func<IList<PdfPageViewModel>, IEnumerable<IPage>> evaluator = <>c.<>9__113_0;
                if (<>c.<>9__113_0 == null)
                {
                    Func<IList<PdfPageViewModel>, IEnumerable<IPage>> local1 = <>c.<>9__113_0;
                    evaluator = <>c.<>9__113_0 = x => (IEnumerable<IPage>) x;
                }
                return this.Pages.Return<IList<PdfPageViewModel>, IEnumerable<IPage>>(evaluator, new Func<IEnumerable<IPage>>(Enumerable.Empty<IPage>));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfDocumentViewModel.<>c <>9 = new PdfDocumentViewModel.<>c();
            public static Func<PdfDocumentState, PdfSelectionState> <>9__38_0;
            public static Func<PdfSelectionState, PdfCaret> <>9__38_1;
            public static Func<PdfDocumentState, bool> <>9__46_0;
            public static Func<PdfDocumentState, bool> <>9__48_0;
            public static Func<PdfTextSelection, string> <>9__87_0;
            public static Func<string> <>9__87_1;
            public static Func<PdfTextSelection, string> <>9__88_0;
            public static Func<string> <>9__88_1;
            public static Func<IList<PdfPageViewModel>, bool> <>9__89_0;
            public static Func<bool> <>9__89_1;
            public static Action<CancellationTokenSource> <>9__99_0;
            public static Func<CancellationTokenSource, bool> <>9__102_1;
            public static Func<bool> <>9__102_2;
            public static Action<Stream> <>9__111_0;
            public static Action<PdfDocumentStateController> <>9__111_1;
            public static Func<IList<PdfPageViewModel>, IEnumerable<IPage>> <>9__113_0;
            public static Func<PdfDocumentState, PdfFormData> <>9__115_0;
            public static Func<PdfDocumentState, PdfFormData> <>9__116_0;
            public static Func<PdfDocumentState, PdfFormData> <>9__117_0;
            public static Func<bool> <>9__118_1;
            public static Func<PdfDocumentState, IList<PdfFileAttachmentListItem>> <>9__121_0;

            internal PdfFormData <ApplyFormData>b__115_0(PdfDocumentState x) => 
                x.FormData;

            internal void <CancelLoadDocument>b__99_0(CancellationTokenSource x)
            {
                x.Cancel();
            }

            internal bool <CanPrintPages>b__118_1() => 
                false;

            internal IList<PdfFileAttachmentListItem> <CreateAttachments>b__121_0(PdfDocumentState x) => 
                x.FileAttachments;

            internal IEnumerable<IPage> <DevExpress.Xpf.DocumentViewer.IDocument.get_Pages>b__113_0(IList<PdfPageViewModel> x) => 
                (IEnumerable<IPage>) x;

            internal PdfSelectionState <get_Caret>b__38_0(PdfDocumentState x) => 
                x.SelectionState;

            internal PdfCaret <get_Caret>b__38_1(PdfSelectionState x) => 
                x.Caret;

            internal bool <get_HasAttachments>b__48_0(PdfDocumentState x) => 
                (x.FileAttachments != null) && (x.FileAttachments.Count > 0);

            internal bool <get_HasOutlines>b__46_0(PdfDocumentState x) => 
                (x.Outlines != null) && (x.Outlines.Count > 0);

            internal PdfFormData <GetFormData>b__117_0(PdfDocumentState x) => 
                x.FormData;

            internal string <GetTextInternal>b__87_0(PdfTextSelection x) => 
                x.Text;

            internal string <GetTextInternal>b__87_1() => 
                string.Empty;

            internal string <GetTextInternal>b__88_0(PdfTextSelection x) => 
                x.Text;

            internal string <GetTextInternal>b__88_1() => 
                string.Empty;

            internal bool <LoadDocument>b__102_1(CancellationTokenSource x) => 
                x.IsCancellationRequested;

            internal bool <LoadDocument>b__102_2() => 
                false;

            internal PdfFormData <SaveFormData>b__116_0(PdfDocumentState x) => 
                x.FormData;

            internal bool <SetCurrentPageInternal>b__89_0(IList<PdfPageViewModel> x) => 
                x.Any<PdfPageViewModel>();

            internal bool <SetCurrentPageInternal>b__89_1() => 
                false;

            internal void <System.IDisposable.Dispose>b__111_0(Stream x)
            {
                x.Dispose();
            }

            internal void <System.IDisposable.Dispose>b__111_1(PdfDocumentStateController x)
            {
                x.Dispose();
            }
        }
    }
}

