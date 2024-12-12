namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    public class InteractionProvider : BindableBase, IPdfViewerController, IPdfPrintingCallbackProvider
    {
        private readonly DispatcherSynchronizationContext synchronizationContext = new DispatcherSynchronizationContext();
        private DocumentViewerControl documentViewer;
        private bool performEnsureVisibleCompleted;

        public event EventHandler CurrentPageIndexChanged;

        public event EventHandler EnsureVisibleCompleted;

        public bool AllowAccessToPublicHyperlink(Uri uri) => 
            this.Viewer.AllowAccessToPublicHyperlink(uri);

        public bool AllowOpenNewDocument(string documentPath, bool openInNewWindow, PdfTarget target) => 
            this.Viewer.RaiseRequestOpeningReferencedDocumentSource(documentPath, openInNewWindow, target);

        private System.Windows.Point CalcPagePosition(int pageIndex)
        {
            PositionCalculator positionCalculator = this.DocumentPresenter.NavigationStrategy.PositionCalculator;
            int pageWrapperIndex = positionCalculator.GetPageWrapperIndex(pageIndex);
            PageWrapper wrapper = this.DocumentPresenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            return new System.Windows.Point(positionCalculator.GetPageWrapperOffset(pageIndex), positionCalculator.GetPageWrapperVerticalOffset(pageWrapperIndex));
        }

        public bool CanOpen(Uri uri) => 
            this.AllowAccessToPublicHyperlink(uri);

        public void CloseEditor()
        {
            Action<PdfPresenterControl> action = <>c.<>9__77_0;
            if (<>c.<>9__77_0 == null)
            {
                Action<PdfPresenterControl> local1 = <>c.<>9__77_0;
                action = <>c.<>9__77_0 = x => x.EndEditing();
            }
            this.DocumentPresenter.Do<PdfPresenterControl>(action);
        }

        public void CommitEditor()
        {
            Func<PdfPresenterControl, CellEditorOwner> evaluator = <>c.<>9__80_0;
            if (<>c.<>9__80_0 == null)
            {
                Func<PdfPresenterControl, CellEditorOwner> local1 = <>c.<>9__80_0;
                evaluator = <>c.<>9__80_0 = x => x.ActiveEditorOwner;
            }
            Action<CellEditorOwner> action = <>c.<>9__80_1;
            if (<>c.<>9__80_1 == null)
            {
                Action<CellEditorOwner> local2 = <>c.<>9__80_1;
                action = <>c.<>9__80_1 = x => x.CurrentCellEditor.CommitEditor(true);
            }
            this.DocumentPresenter.With<PdfPresenterControl, CellEditorOwner>(evaluator).Do<CellEditorOwner>(action);
        }

        private IControllerAction CreateContextMenuBarItem(string name)
        {
            BarItemLink link1 = new BarItemLink();
            link1.BarItemName = name;
            BarItemLink link = link1;
            CollectionAction.SetContainerName(link, "pmContextMenu");
            return link;
        }

        private BarItemLink CreateContextMenuSeparator()
        {
            BarItemLinkSeparator separator = new BarItemLinkSeparator();
            CollectionAction.SetContainerName(separator, "pmContextMenu");
            return separator;
        }

        void IPdfPrintingCallbackProvider.RaisePrintPageEvent(DevExpress.Pdf.PdfPrintPageEventArgs e)
        {
            this.Viewer.Do<PdfViewerControl>(x => x.RaisePrintPage(e));
        }

        void IPdfPrintingCallbackProvider.RaiseQueryPageSettingsEvent(DevExpress.Pdf.PdfQueryPageSettingsEventArgs e)
        {
            this.Viewer.Do<PdfViewerControl>(x => x.RaiseQueryPageSettings(e));
        }

        void IPdfViewerController.InvalidateAnnotation(int pageIndex)
        {
            Func<PdfThumbnailsViewerControl, DevExpress.Xpf.DocumentViewer.IDocument> evaluator = <>c.<>9__90_0;
            if (<>c.<>9__90_0 == null)
            {
                Func<PdfThumbnailsViewerControl, DevExpress.Xpf.DocumentViewer.IDocument> local1 = <>c.<>9__90_0;
                evaluator = <>c.<>9__90_0 = x => x.Document;
            }
            Action<ThumbnailPageViewModel> action = <>c.<>9__90_2;
            if (<>c.<>9__90_2 == null)
            {
                Action<ThumbnailPageViewModel> local2 = <>c.<>9__90_2;
                action = <>c.<>9__90_2 = x => x.ForceInvalidate = true;
            }
            (LayoutHelper.FindElementByType<PdfThumbnailsViewerControl>(this.Viewer).With<PdfThumbnailsViewerControl, DevExpress.Xpf.DocumentViewer.IDocument>(evaluator) as ThumbnailsDocumentViewModel).With<ThumbnailsDocumentViewModel, ThumbnailPageViewModel>(x => (x.Pages.ElementAt<IPage>(pageIndex) as ThumbnailPageViewModel)).Do<ThumbnailPageViewModel>(action);
        }

        bool IPdfViewerController.OpenFileAttachment(PdfFileAttachment attachment) => 
            this.OpenAttachment(attachment);

        string IPdfViewerController.SaveFileAttachment(PdfFileAttachment attachment) => 
            this.SaveAttachment(attachment);

        public void EnsureVisible(int pageIndex, PdfRectangle bounds, bool inCenter)
        {
            this.performEnsureVisibleCompleted = true;
            this.DocumentPresenter.Do<PdfPresenterControl>(x => x.ScrollIntoView(new PdfTargetScroll(pageIndex, bounds, inCenter)));
        }

        public PdfPoint GetClientPoint(PdfDocumentPosition endPosition)
        {
            int pageIndex = endPosition.PageIndex;
            System.Windows.Point point = (this.documentViewer.Document.Pages.ElementAt<IPage>(pageIndex) as PdfPageViewModel).GetPoint(endPosition.Point, this.BehaviorProvider.ZoomFactor, this.BehaviorProvider.RotateAngle);
            System.Windows.Point point2 = this.CalcPagePosition(pageIndex);
            return new PdfPoint(point.X + point2.X, point.Y + point2.Y);
        }

        public Bitmap GetEditorBitmap(PdfEditorSettings settings)
        {
            CellEditor visual = this.DocumentPresenter.With<PdfPresenterControl, CellEditor>(x => x.GenerateEditor(settings));
            if (visual == null)
            {
                return null;
            }
            RenderTargetBitmap source = new RenderTargetBitmap((int) visual.ActualWidth, (int) visual.ActualHeight, 96.0, 96.0, PixelFormats.Pbgra32);
            source.Render(visual);
            using (MemoryStream stream = new MemoryStream())
            {
                new PngBitmapEncoder { Frames = { BitmapFrame.Create(source) } }.Save(stream);
                return new Bitmap(stream);
            }
        }

        public PdfTextMarkupAnnotationDefaultSetting GetTextMarkupAnnotationSettings(PdfTextMarkupAnnotationType type)
        {
            System.Drawing.Color color;
            Func<PdfViewerControl, PdfMarkupToolsSettings> evaluator = <>c.<>9__81_0;
            if (<>c.<>9__81_0 == null)
            {
                Func<PdfViewerControl, PdfMarkupToolsSettings> local1 = <>c.<>9__81_0;
                evaluator = <>c.<>9__81_0 = x => x.MarkupToolsSettings;
            }
            PdfMarkupToolsSettings settings = this.Viewer.With<PdfViewerControl, PdfMarkupToolsSettings>(evaluator);
            if (settings == null)
            {
                return new PdfTextMarkupAnnotationDefaultSetting(System.Drawing.Color.Transparent, "", "");
            }
            string defaultSubject = string.Empty;
            if (type == PdfTextMarkupAnnotationType.Highlight)
            {
                color = System.Drawing.Color.FromArgb(settings.TextHighlightColor.A, settings.TextHighlightColor.R, settings.TextHighlightColor.G, settings.TextHighlightColor.B);
                defaultSubject = settings.TextHighlightDefaultSubject;
            }
            else if (type != PdfTextMarkupAnnotationType.StrikeOut)
            {
                color = System.Drawing.Color.FromArgb(settings.TextUnderlineColor.A, settings.TextUnderlineColor.R, settings.TextUnderlineColor.G, settings.TextUnderlineColor.B);
                defaultSubject = settings.TextUnderlineDefaultSubject;
            }
            else
            {
                color = System.Drawing.Color.FromArgb(settings.TextStrikethroughColor.A, settings.TextStrikethroughColor.R, settings.TextStrikethroughColor.G, settings.TextStrikethroughColor.B);
                defaultSubject = settings.TextStrikethroughDefaultSubject;
            }
            return new PdfTextMarkupAnnotationDefaultSetting(color, settings.Author, defaultSubject);
        }

        public void GoToFirstPage()
        {
            this.CommandProvider.PaginationCommand.TryExecute(1);
        }

        public void GoToLastPage()
        {
            this.CommandProvider.PaginationCommand.TryExecute(this.documentViewer.PageCount);
        }

        public void GoToNextPage()
        {
            this.CommandProvider.NextPageCommand.TryExecute(null);
        }

        public void GoToPreviousPage()
        {
            this.CommandProvider.PreviousPageCommand.TryExecute(null);
        }

        public void HideTooltip()
        {
            Action<PdfPresenterControl> action = <>c.<>9__78_0;
            if (<>c.<>9__78_0 == null)
            {
                Action<PdfPresenterControl> local1 = <>c.<>9__78_0;
                action = <>c.<>9__78_0 = x => x.HideTooltip();
            }
            this.DocumentPresenter.Do<PdfPresenterControl>(action);
        }

        public void Invalidate(PdfDocumentStateChangedFlags flags)
        {
            if (flags.HasFlag(PdfDocumentStateChangedFlags.Selection))
            {
                this.UpdateSelection();
            }
            if (flags.HasFlag(PdfDocumentStateChangedFlags.AllContent))
            {
                this.Viewer.ViewerBackend.ClearPageCache();
            }
            Action<PdfPresenterControl> action = <>c.<>9__64_0;
            if (<>c.<>9__64_0 == null)
            {
                Action<PdfPresenterControl> local1 = <>c.<>9__64_0;
                action = <>c.<>9__64_0 = x => x.Update();
            }
            this.DocumentPresenter.Do<PdfPresenterControl>(action);
        }

        private void OnCurrentPageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            if (this.CurrentPageIndexChanged == null)
            {
                EventHandler currentPageIndexChanged = this.CurrentPageIndexChanged;
            }
            else
            {
                this.CurrentPageIndexChanged(sender, e);
            }
        }

        internal void OnLoaded()
        {
            this.BehaviorProvider.PageIndexChanged += new EventHandler<PageIndexChangedEventArgs>(this.OnCurrentPageIndexChanged);
        }

        internal void OnUnloaded()
        {
            this.BehaviorProvider.PageIndexChanged -= new EventHandler<PageIndexChangedEventArgs>(this.OnCurrentPageIndexChanged);
        }

        public bool OpenAttachment(PdfFileAttachment fileAttachment) => 
            this.Viewer.OpenAttachmentInternal(fileAttachment);

        public bool OpenDocument(string documentPath, PdfTarget target, bool openInNewWindow)
        {
            if (this.AllowOpenNewDocument(documentPath, openInNewWindow, target))
            {
                this.OpenNewDocument(documentPath, openInNewWindow, target);
            }
            return true;
        }

        private void OpenNewDocument(string documentPath, PdfTarget target)
        {
            if (!File.Exists(documentPath))
            {
                string path = Path.Combine(Path.GetDirectoryName(this.DocumentFilePath), documentPath);
                if (File.Exists(path))
                {
                    documentPath = path;
                }
            }
            this.Viewer.DocumentSource = documentPath;
        }

        public void OpenNewDocument(string documentPath, bool openInNewWindow, PdfTarget target)
        {
            if (openInNewWindow)
            {
                this.RunExternalProgram(documentPath);
            }
            else
            {
                this.OpenNewDocument(documentPath, target);
            }
        }

        public bool RaiseAnnotationDeletingEvent(int pageNumber, PdfRectangle rectangle, string name)
        {
            Func<PdfViewerControl, PdfThumbnailsViewerSettings> evaluator = <>c.<>9__88_0;
            if (<>c.<>9__88_0 == null)
            {
                Func<PdfViewerControl, PdfThumbnailsViewerSettings> local1 = <>c.<>9__88_0;
                evaluator = <>c.<>9__88_0 = x => x.ActualThumbnailsViewerSettings;
            }
            Action<PdfThumbnailsViewerSettings> action = <>c.<>9__88_1;
            if (<>c.<>9__88_1 == null)
            {
                Action<PdfThumbnailsViewerSettings> local2 = <>c.<>9__88_1;
                action = <>c.<>9__88_1 = delegate (PdfThumbnailsViewerSettings x) {
                    x.RaiseInvalidate();
                };
            }
            this.Viewer.With<PdfViewerControl, PdfThumbnailsViewerSettings>(evaluator).Do<PdfThumbnailsViewerSettings>(action);
            return !this.Viewer.Return<PdfViewerControl, bool>(x => x.RaiseAnnotationDeletingEvent(pageNumber, rectangle, name), (<>c.<>9__88_3 ??= () => false));
        }

        public void RaiseFormFieldGotFocus(string fieldName)
        {
        }

        public void RaiseFormFieldLostFocus(string fieldName)
        {
        }

        public void RaiseMarkupAnnotationGotFocusEvent(PdfMarkupAnnotationInfo info)
        {
            this.Viewer.Do<PdfViewerControl>(x => x.RaiseMarkupAnnotationGotFocusEvent(info));
        }

        public void RaiseMarkupAnnotationLostFocusEvent(PdfMarkupAnnotationInfo info)
        {
            this.Viewer.Do<PdfViewerControl>(x => x.RaiseMarkupAnnotationLostFocusEvent(info));
        }

        public void RaiseSelectionContinuedEvent(PdfDocumentPosition position)
        {
            SelectionEventArgs e = new SelectionEventArgs(position);
            e.RoutedEvent = PdfViewerControl.SelectionContinuedEvent;
            this.Viewer.RaiseEvent(e);
        }

        public void RaiseSelectionEndedEvent(PdfDocumentPosition position)
        {
            SelectionEventArgs e = new SelectionEventArgs(position);
            e.RoutedEvent = PdfViewerControl.SelectionEndedEvent;
            this.Viewer.RaiseEvent(e);
        }

        public void RaiseSelectionStartedEvent(PdfDocumentPosition position)
        {
            SelectionEventArgs e = new SelectionEventArgs(position);
            e.RoutedEvent = PdfViewerControl.SelectionStartedEvent;
            this.Viewer.RaiseEvent(e);
        }

        public void RaiseTextMarkupAnnotationCreatedEvent(string annotaionName)
        {
            this.Viewer.Do<PdfViewerControl>(x => x.RaiseTextMarkupAnnotationCreatedEvent(annotaionName));
        }

        public bool RaiseTextMarkupAnnotationCreatingEvent(string selectedText, PdfViewerTextMarkupAnnotationBuilder builder)
        {
            Func<PdfViewerControl, PdfThumbnailsViewerSettings> evaluator = <>c.<>9__84_0;
            if (<>c.<>9__84_0 == null)
            {
                Func<PdfViewerControl, PdfThumbnailsViewerSettings> local1 = <>c.<>9__84_0;
                evaluator = <>c.<>9__84_0 = x => x.ActualThumbnailsViewerSettings;
            }
            Action<PdfThumbnailsViewerSettings> action = <>c.<>9__84_1;
            if (<>c.<>9__84_1 == null)
            {
                Action<PdfThumbnailsViewerSettings> local2 = <>c.<>9__84_1;
                action = <>c.<>9__84_1 = delegate (PdfThumbnailsViewerSettings x) {
                    x.RaiseInvalidate();
                };
            }
            this.Viewer.With<PdfViewerControl, PdfThumbnailsViewerSettings>(evaluator).Do<PdfThumbnailsViewerSettings>(action);
            return this.Viewer.Return<PdfViewerControl, bool>(x => x.RaiseTextMarkupAnnotationCreatingEvent(selectedText, builder), (<>c.<>9__84_3 ??= () => false));
        }

        private void RunExternalProgram(string documentPath)
        {
            SafeProcess.Start(documentPath, null, null);
        }

        public void RunPrintDialog()
        {
            this.ShowPrintDialog();
        }

        public string SaveAttachment(PdfFileAttachment fileAttachment) => 
            this.Viewer.SaveAttachmentInternal(fileAttachment);

        internal void ScrollViewerScrollChanged()
        {
            if (this.performEnsureVisibleCompleted)
            {
                if (this.EnsureVisibleCompleted == null)
                {
                    EventHandler ensureVisibleCompleted = this.EnsureVisibleCompleted;
                }
                else
                {
                    this.EnsureVisibleCompleted(this, EventArgs.Empty);
                }
            }
            this.performEnsureVisibleCompleted = false;
        }

        public void ShowDocumentPosition(PdfTarget target)
        {
            this.DocumentPresenter.Do<PdfPresenterControl>(x => x.ScrollIntoView(target));
        }

        public void ShowEditor(PdfEditorSettings editorSettings, IPdfViewerValueEditingCallBack editorCallback)
        {
            this.DocumentPresenter.Do<PdfPresenterControl>(x => x.StartEditing(editorSettings, editorCallback));
        }

        public void ShowPopupMenu(PdfPopupMenuContext context)
        {
            List<IControllerAction> list = new List<IControllerAction>();
            Action<PopupMenu> action = <>c.<>9__89_0;
            if (<>c.<>9__89_0 == null)
            {
                Action<PopupMenu> local1 = <>c.<>9__89_0;
                action = <>c.<>9__89_0 = x => x.Items.Clear();
            }
            (BarManager.GetDXContextMenu(this.DocumentPresenter) as PopupMenu).Do<PopupMenu>(action);
            if (context.HasFlag(PdfPopupMenuContext.AnnotationProperties))
            {
                list.Add(this.CreateContextMenuBarItem("bDeleteAnnotation"));
                list.Add(this.CreateContextMenuSeparator());
                list.Add(this.CreateContextMenuBarItem("bAnnotationProperties"));
            }
            if (context.HasFlag(PdfPopupMenuContext.Selection))
            {
                list.Add(this.CreateContextMenuBarItem("bCopy"));
                list.Add(this.CreateContextMenuSeparator());
            }
            if (context.HasFlag(PdfPopupMenuContext.TextMarkup))
            {
                list.Add(this.CreateContextMenuBarItem("bHighlightSelectedText"));
                list.Add(this.CreateContextMenuBarItem("bStrikethroughSelectedText"));
                list.Add(this.CreateContextMenuBarItem("bUnderlineSelectedText"));
            }
            if (context == PdfPopupMenuContext.None)
            {
                list.Add(this.CreateContextMenuBarItem("bSelectTool"));
                list.Add(this.CreateContextMenuBarItem("bHandTool"));
                list.Add(this.CreateContextMenuBarItem("bMarqueeZoom"));
                list.Add(this.CreateContextMenuSeparator());
                if (this.Viewer.UndoRedoManager.CanUndo)
                {
                    list.Add(this.CreateContextMenuBarItem("bPreviousView"));
                }
                if (this.Viewer.UndoRedoManager.CanRedo)
                {
                    list.Add(this.CreateContextMenuBarItem("bNextView"));
                }
                list.Add(this.CreateContextMenuSeparator());
                list.Add(this.CreateContextMenuBarItem("bClockwiseRotate"));
                list.Add(this.CreateContextMenuSeparator());
                list.Add(this.CreateContextMenuBarItem("bPrintContext"));
                list.Add(this.CreateContextMenuBarItem("bFindContext"));
                list.Add(this.CreateContextMenuSeparator());
                list.Add(this.CreateContextMenuBarItem("bSelectAll"));
                list.Add(this.CreateContextMenuSeparator());
                list.Add(this.CreateContextMenuBarItem("bProperties"));
            }
            PopupMenuShowingEventArgs args = this.Viewer.RaisePopupMenuShowingEvent();
            if (!args.Cancel)
            {
                list.AddRange(args.Actions);
                this.CommandProvider.ContextMenuActions = new ObservableCollection<IControllerAction>(list);
            }
        }

        public void ShowPrintDialog()
        {
            this.Viewer.Print();
        }

        public void ShowTooltip(PdfToolTipSettings tooltipSettings)
        {
            this.DocumentPresenter.Do<PdfPresenterControl>(x => x.ShowTooltip(tooltipSettings));
        }

        public void SynchronizeWithMousePosition()
        {
            Action<PdfPresenterControl> action = <>c.<>9__65_0;
            if (<>c.<>9__65_0 == null)
            {
                Action<PdfPresenterControl> local1 = <>c.<>9__65_0;
                action = <>c.<>9__65_0 = x => x.BringCurrentSelectionPointIntoView();
            }
            this.DocumentPresenter.Do<PdfPresenterControl>(action);
        }

        public void UpdateSelection()
        {
            ((IPdfDocument) this.DocumentViewer.Document).UpdateDocumentSelection();
            this.Viewer.UpdateSelection();
        }

        private PdfViewerControl Viewer =>
            (PdfViewerControl) this.DocumentViewer;

        public bool ReadOnly =>
            this.Viewer.IsReadOnly;

        public int CurrentPageIndex =>
            this.documentViewer.CurrentPageNumber - 1;

        public System.Threading.SynchronizationContext SynchronizationContext =>
            this.synchronizationContext;

        public PdfViewerTool ViewerTool =>
            (PdfViewerTool) this.Viewer.CursorMode;

        protected internal PdfPresenterControl DocumentPresenter =>
            this.Viewer.DocumentPresenter;

        protected internal PdfBehaviorProvider BehaviorProvider =>
            (PdfBehaviorProvider) this.Viewer.ActualBehaviorProvider;

        protected internal PdfCommandProvider CommandProvider =>
            (PdfCommandProvider) this.Viewer.ActualCommandProvider;

        protected internal DocumentViewerControl DocumentViewer
        {
            get => 
                this.documentViewer;
            set
            {
                if (!Equals(this.documentViewer, value))
                {
                    this.documentViewer = value;
                }
            }
        }

        public bool SupportsRotatedEditors =>
            true;

        public bool SupportsSemiTransparentEditors =>
            true;

        public string DocumentFilePath
        {
            get
            {
                Func<PdfDocumentViewModel, string> evaluator = <>c.<>9__27_0;
                if (<>c.<>9__27_0 == null)
                {
                    Func<PdfDocumentViewModel, string> local1 = <>c.<>9__27_0;
                    evaluator = <>c.<>9__27_0 = x => x.FilePath;
                }
                return (this.Viewer.Document as PdfDocumentViewModel).Return<PdfDocumentViewModel, string>(evaluator, (<>c.<>9__27_1 ??= () => string.Empty));
            }
        }

        public bool ShowPrintStatusDialog { get; internal set; }

        public bool ResetSearchOnPageChange =>
            this.Viewer.ContinueSearchFrom == PdfContinueSearchFrom.CurrentPage;

        public System.Drawing.Color SelectionColor =>
            this.DocumentPresenter.HighlightSelectionColor.ToWinFormsColor();

        public float DpiScale =>
            (float) ScreenHelper.ScaleX;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InteractionProvider.<>c <>9 = new InteractionProvider.<>c();
            public static Func<PdfDocumentViewModel, string> <>9__27_0;
            public static Func<string> <>9__27_1;
            public static Action<PdfPresenterControl> <>9__64_0;
            public static Action<PdfPresenterControl> <>9__65_0;
            public static Action<PdfPresenterControl> <>9__77_0;
            public static Action<PdfPresenterControl> <>9__78_0;
            public static Func<PdfPresenterControl, CellEditorOwner> <>9__80_0;
            public static Action<CellEditorOwner> <>9__80_1;
            public static Func<PdfViewerControl, PdfMarkupToolsSettings> <>9__81_0;
            public static Func<PdfViewerControl, PdfThumbnailsViewerSettings> <>9__84_0;
            public static Action<PdfThumbnailsViewerSettings> <>9__84_1;
            public static Func<bool> <>9__84_3;
            public static Func<PdfViewerControl, PdfThumbnailsViewerSettings> <>9__88_0;
            public static Action<PdfThumbnailsViewerSettings> <>9__88_1;
            public static Func<bool> <>9__88_3;
            public static Action<PopupMenu> <>9__89_0;
            public static Func<PdfThumbnailsViewerControl, DevExpress.Xpf.DocumentViewer.IDocument> <>9__90_0;
            public static Action<ThumbnailPageViewModel> <>9__90_2;

            internal void <CloseEditor>b__77_0(PdfPresenterControl x)
            {
                x.EndEditing();
            }

            internal CellEditorOwner <CommitEditor>b__80_0(PdfPresenterControl x) => 
                x.ActiveEditorOwner;

            internal void <CommitEditor>b__80_1(CellEditorOwner x)
            {
                x.CurrentCellEditor.CommitEditor(true);
            }

            internal DevExpress.Xpf.DocumentViewer.IDocument <DevExpress.Pdf.Drawing.IPdfViewerController.InvalidateAnnotation>b__90_0(PdfThumbnailsViewerControl x) => 
                x.Document;

            internal void <DevExpress.Pdf.Drawing.IPdfViewerController.InvalidateAnnotation>b__90_2(ThumbnailPageViewModel x)
            {
                x.ForceInvalidate = true;
            }

            internal string <get_DocumentFilePath>b__27_0(PdfDocumentViewModel x) => 
                x.FilePath;

            internal string <get_DocumentFilePath>b__27_1() => 
                string.Empty;

            internal PdfMarkupToolsSettings <GetTextMarkupAnnotationSettings>b__81_0(PdfViewerControl x) => 
                x.MarkupToolsSettings;

            internal void <HideTooltip>b__78_0(PdfPresenterControl x)
            {
                x.HideTooltip();
            }

            internal void <Invalidate>b__64_0(PdfPresenterControl x)
            {
                x.Update();
            }

            internal PdfThumbnailsViewerSettings <RaiseAnnotationDeletingEvent>b__88_0(PdfViewerControl x) => 
                x.ActualThumbnailsViewerSettings;

            internal void <RaiseAnnotationDeletingEvent>b__88_1(PdfThumbnailsViewerSettings x)
            {
                x.RaiseInvalidate();
            }

            internal bool <RaiseAnnotationDeletingEvent>b__88_3() => 
                false;

            internal PdfThumbnailsViewerSettings <RaiseTextMarkupAnnotationCreatingEvent>b__84_0(PdfViewerControl x) => 
                x.ActualThumbnailsViewerSettings;

            internal void <RaiseTextMarkupAnnotationCreatingEvent>b__84_1(PdfThumbnailsViewerSettings x)
            {
                x.RaiseInvalidate();
            }

            internal bool <RaiseTextMarkupAnnotationCreatingEvent>b__84_3() => 
                false;

            internal void <ShowPopupMenu>b__89_0(PopupMenu x)
            {
                x.Items.Clear();
            }

            internal void <SynchronizeWithMousePosition>b__65_0(PdfPresenterControl x)
            {
                x.BringCurrentSelectionPointIntoView();
            }
        }
    }
}

