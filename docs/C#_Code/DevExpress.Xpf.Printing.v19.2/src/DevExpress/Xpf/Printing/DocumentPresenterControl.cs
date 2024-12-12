namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Editing;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Rendering;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class DocumentPresenterControl : DevExpress.Xpf.DocumentViewer.DocumentPresenterControl, IPagesPresenter, ISupportInvalidateRenderingOnIdle
    {
        private static readonly Type ownerType = typeof(DevExpress.Xpf.Printing.DocumentPresenterControl);
        private static readonly DependencyPropertyKey RenderedSourcePropertyKey = DependencyPropertyManager.RegisterReadOnly("RenderedSource", typeof(ImageSource), ownerType, new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty RenderedSourceProperty = RenderedSourcePropertyKey.DependencyProperty;
        private static readonly DependencyPropertyKey RenderMaskPropertyKey = DependencyPropertyManager.RegisterReadOnly("RenderMask", typeof(DrawingBrush), ownerType, new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty RenderMaskProperty = RenderMaskPropertyKey.DependencyProperty;
        private static readonly DependencyPropertyKey SelectionRectanglePropertyKey = DependencyPropertyManager.RegisterReadOnly("SelectionRectangle", typeof(DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle), ownerType, new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty SelectionRectangleProperty = SelectionRectanglePropertyKey.DependencyProperty;
        public static readonly DependencyProperty HighlightSelectionColorProperty;
        private static readonly DependencyPropertyKey SearchParameterPropertyKey;
        public static readonly DependencyProperty SearchParameterProperty;
        private Locker renderLocker = new Locker();

        static DocumentPresenterControl()
        {
            HighlightSelectionColorProperty = DependencyPropertyManager.Register("HighlightSelectionColor", typeof(Color), ownerType, new FrameworkPropertyMetadata(Color.FromArgb(0x59, 0x60, 0x98, 0xc0), (d, e) => ((DevExpress.Xpf.Printing.DocumentPresenterControl) d).OnSelectionColorChanged((Color) e.NewValue)));
            SearchParameterPropertyKey = DependencyPropertyManager.RegisterReadOnly("SearchParameter", typeof(TextSearchParameter), ownerType, new FrameworkPropertyMetadata(null));
            SearchParameterProperty = SearchParameterPropertyKey.DependencyProperty;
        }

        public DocumentPresenterControl()
        {
            base.DefaultStyleKey = ownerType;
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            DrawingBrush brush1 = new DrawingBrush();
            brush1.Drawing = new GeometryDrawing();
            this.RenderMask = brush1;
            this.InteractionProvider = this.CreateDefaultInteractionProvider();
            this.EditingStrategy = this.CreateDefaultEditingStrategy();
            Point startPoint = new Point();
            this.SelectionRectangle = new DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle(startPoint, Size.Empty);
        }

        internal void AttachEditorToTree(DocumentInplaceEditorOwner editorOwner, int pageIndex, Func<Rect> rectHandler, double angle)
        {
            PreviewPageControl control = (PreviewPageControl) LayoutHelper.FindElement(base.ItemsPanel, delegate (FrameworkElement fr) {
                Func<PageControl, bool> <>9__1;
                Func<PageControl, bool> evaluator = <>9__1;
                if (<>9__1 == null)
                {
                    Func<PageControl, bool> local1 = <>9__1;
                    evaluator = <>9__1 = delegate (PageControl x) {
                        Func<IPage, bool> <>9__2;
                        Func<IPage, bool> predicate = <>9__2;
                        if (<>9__2 == null)
                        {
                            Func<IPage, bool> local1 = <>9__2;
                            predicate = <>9__2 = page => page.PageIndex == pageIndex;
                        }
                        return ((PageWrapper) x.DataContext).Pages.Any<IPage>(predicate);
                    };
                }
                return (fr as PageControl).If<PageControl>(evaluator).ReturnSuccess<PageControl>();
            });
            if (control != null)
            {
                this.ActiveEditorOwner = editorOwner;
                editorOwner.Page = control;
                control.AddEditor(editorOwner.VisualHost, rectHandler, angle);
            }
        }

        private Rect CalcPageRect(RenderItem pair, Rect rect)
        {
            PageWrapper item = base.Pages.FirstOrDefault<PageWrapper>(delegate (PageWrapper x) {
                Func<IPage, bool> <>9__1;
                Func<IPage, bool> predicate = <>9__1;
                if (<>9__1 == null)
                {
                    Func<IPage, bool> local1 = <>9__1;
                    predicate = <>9__1 = p => p.PageIndex == pair.Page.PageIndex;
                }
                return x.Pages.Any<IPage>(predicate);
            });
            if (item != null)
            {
                Control control = (Control) base.ItemsControl.ItemContainerGenerator.ContainerFromItem(item);
                double num = (control.Padding.Left + control.Padding.Right) / 2.0;
                double num2 = (control.Padding.Top + control.Padding.Bottom) / 2.0;
                rect.Inflate(-num, -num2);
            }
            return rect;
        }

        private DevExpress.Xpf.Printing.PreviewControl.Native.EditingStrategy CreateDefaultEditingStrategy() => 
            new DevExpress.Xpf.Printing.PreviewControl.Native.EditingStrategy(this);

        protected virtual DevExpress.Xpf.Printing.PreviewControl.Native.InteractionProvider CreateDefaultInteractionProvider() => 
            new DevExpress.Xpf.Printing.PreviewControl.Native.InteractionProvider(this);

        protected override KeyboardAndMouseController CreateKeyboardAndMouseController() => 
            new UserInputController(this);

        protected sealed override DocumentViewerRenderer CreateNativeRenderer() => 
            new DocumentPreviewRenderer(this);

        protected override DevExpress.Xpf.DocumentViewer.NavigationStrategy CreateNavigationStrategy() => 
            new DocumentNavigationStrategy(this);

        protected override PageWrapper CreatePageWrapper(IPage page)
        {
            PreviewPageWrapper wrapper1 = new PreviewPageWrapper(page);
            wrapper1.ZoomFactor = base.BehaviorProvider.ZoomFactor;
            return wrapper1;
        }

        protected override PageWrapper CreatePageWrapper(IEnumerable<IPage> pages)
        {
            PreviewPageWrapper wrapper1 = new PreviewPageWrapper(pages);
            wrapper1.ZoomFactor = base.BehaviorProvider.ZoomFactor;
            wrapper1.HorizontalPageSpacing = base.HorizontalPageSpacing;
            wrapper1.VerticalPageSpacing = base.VerticalPageSpacing;
            return wrapper1;
        }

        internal void DetachEditorFromTree()
        {
            if (this.ActiveEditorOwner != null)
            {
                this.ActiveEditorOwner.Page.RemoveEditor();
                this.ActiveEditorOwner = null;
            }
        }

        void ISupportInvalidateRenderingOnIdle.InvalidateRenderingOnIdle()
        {
            base.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, delegate {
                if (this.IsInVisualTree())
                {
                    Func<RenderItem, bool> predicate = <>c.<>9__115_1;
                    if (<>c.<>9__115_1 == null)
                    {
                        Func<RenderItem, bool> local1 = <>c.<>9__115_1;
                        predicate = <>c.<>9__115_1 = x => x.NeedsInvalidate;
                    }
                    RenderItem item = this.GetDrawingContent().FirstOrDefault<RenderItem>(predicate);
                    if (item != null)
                    {
                        item.Page.ForceInvalidate = true;
                        this.RenderContent();
                    }
                }
            });
        }

        protected override ObservableCollection<PageWrapper> GeneratePageWrappers(IEnumerable<IPage> pages) => 
            !pages.Any<IPage>() ? new ObservableCollection<PageWrapper>() : base.GeneratePageWrappers(pages);

        internal DrawingBrush GenerateRenderMask(IEnumerable<RenderItem> drawingContent)
        {
            GeometryGroup group = new GeometryGroup();
            foreach (RenderItem item in drawingContent)
            {
                Rect rect = this.CalcPageRect(item, item.Rectangle);
                Geometry geometry = new RectangleGeometry(rect);
                group.Children.Add(geometry);
            }
            GeometryDrawing drawing = new GeometryDrawing();
            drawing.Geometry = group;
            drawing.Brush = Brushes.Green;
            drawing.Pen = new Pen();
            DrawingBrush brush1 = new DrawingBrush(drawing);
            brush1.AlignmentX = AlignmentX.Left;
            brush1.AlignmentY = AlignmentY.Top;
            brush1.Stretch = Stretch.None;
            brush1.ViewboxUnits = BrushMappingMode.Absolute;
            return brush1;
        }

        protected internal IEnumerable<RenderItem> GetDrawingContent() => 
            this.GetDrawingContent(base.ItemsPanel);

        private IEnumerable<RenderItem> GetDrawingContent(DocumentViewerPanel panel)
        {
            List<RenderItem> list = new List<RenderItem>();
            if (panel != null)
            {
                Func<Pair<IPage, Rect>, RenderItem> selector = <>c.<>9__107_0;
                if (<>c.<>9__107_0 == null)
                {
                    Func<Pair<IPage, Rect>, RenderItem> local1 = <>c.<>9__107_0;
                    selector = <>c.<>9__107_0 = delegate (Pair<IPage, Rect> x) {
                        IDocumentPage first = (IDocumentPage) x.First;
                        RenderItem item1 = new RenderItem();
                        item1.Rectangle = x.Second;
                        item1.Page = first;
                        item1.NeedsInvalidate = first.NeedsInvalidate;
                        item1.ForceInvalidate = first.ForceInvalidate;
                        item1.TextureType = TextureType.Content;
                        return item1;
                    };
                }
                list.AddRange(this.GetVisiblePagesCore().Select<Pair<IPage, Rect>, RenderItem>(selector));
            }
            return list;
        }

        private Size GetMaxPageSize()
        {
            Func<PageWrapper, Size> sizeSelector = <>c.<>9__73_0;
            if (<>c.<>9__73_0 == null)
            {
                Func<PageWrapper, Size> local1 = <>c.<>9__73_0;
                sizeSelector = <>c.<>9__73_0 = x => x.PageSize;
            }
            return this.GetMaxPageSize(sizeSelector);
        }

        private Size GetMaxPageSize(Func<PageWrapper, Size> sizeSelector)
        {
            if (base.Pages == null)
            {
                return Size.Empty;
            }
            double num = 0.0;
            Size empty = Size.Empty;
            foreach (PageWrapper wrapper in base.Pages)
            {
                Size size2 = sizeSelector(wrapper);
                double num2 = sizeSelector(wrapper).Width * size2.Height;
                if (num2 > num)
                {
                    empty = sizeSelector(wrapper);
                    num = num2;
                }
            }
            return empty;
        }

        private Size GetMaxPageVisibleSize()
        {
            Func<PageWrapper, Size> sizeSelector = <>c.<>9__74_0;
            if (<>c.<>9__74_0 == null)
            {
                Func<PageWrapper, Size> local1 = <>c.<>9__74_0;
                sizeSelector = <>c.<>9__74_0 = x => x.VisibleSize;
            }
            return this.GetMaxPageSize(sizeSelector);
        }

        public IEnumerable<Pair<Page, RectangleF>> GetPages()
        {
            Func<Pair<IPage, Rect>, Pair<Page, RectangleF>> selector = <>c.<>9__113_0;
            if (<>c.<>9__113_0 == null)
            {
                Func<Pair<IPage, Rect>, Pair<Page, RectangleF>> local1 = <>c.<>9__113_0;
                selector = <>c.<>9__113_0 = x => new Pair<Page, RectangleF>(((IDocumentPage) x.First).Page, x.Second.ToWinFormsRectangle());
            }
            return this.GetVisiblePagesCore().Select<Pair<IPage, Rect>, Pair<Page, RectangleF>>(selector).ToList<Pair<Page, RectangleF>>();
        }

        private IEnumerable<Pair<IPage, Rect>> GetVisiblePagesCore()
        {
            List<Pair<IPage, Rect>> list = new List<Pair<IPage, Rect>>();
            if (base.ItemsPanel != null)
            {
                foreach (FrameworkElement element in base.ItemsPanel.InternalChildren)
                {
                    PageWrapper wrapper = (PageWrapper) base.ItemsControl.ItemContainerGenerator.ItemFromContainer(element);
                    Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(element, base.ItemsControl);
                    foreach (IPage page in wrapper.Pages)
                    {
                        Rect pageRect = wrapper.GetPageRect(page);
                        pageRect.Offset(relativeElementRect.Left, relativeElementRect.Top);
                        if (this.IsVisibleChild(pageRect))
                        {
                            list.Add(new Pair<IPage, Rect>(page, pageRect));
                        }
                    }
                }
            }
            return list;
        }

        protected override void Initialize()
        {
            if ((base.ItemsControl != null) && (base.IsInitialized && ((base.BehaviorProvider != null) && (this.ScrollViewer != null))))
            {
                Func<IDocumentViewModel, bool> evaluator = <>c.<>9__77_0;
                if (<>c.<>9__77_0 == null)
                {
                    Func<IDocumentViewModel, bool> local1 = <>c.<>9__77_0;
                    evaluator = <>c.<>9__77_0 = x => x.IsLoaded;
                }
                this.Pages = this.Document.Return<IDocumentViewModel, bool>(evaluator, (<>c.<>9__77_1 ??= () => false)) ? this.GeneratePageWrappers((IEnumerable<IPage>) this.Document.Pages) : null;
                base.ItemsControl.PageDisplayMode = base.PageDisplayMode;
                base.ItemsControl.ItemsSource = base.Pages;
                base.ItemsPanel.ShowSingleItem = base.ShowSingleItem;
                this.NavigationStrategy.GenerateStartUpState();
                this.UpdateBehaviorProviderProperties();
                base.NativeImage.Invalidate();
                DocumentViewModel documentModel = this.Document as DocumentViewModel;
                if (documentModel != null)
                {
                    if (documentModel.NavigationState != null)
                    {
                        base.ImmediateActionsManager.EnqueueAction(delegate {
                            Action <>9__3;
                            Action action = <>9__3;
                            if (<>9__3 == null)
                            {
                                Action local1 = <>9__3;
                                action = <>9__3 = delegate {
                                    documentModel.NavigationState.Do<NavigationState>(new Action<NavigationState>(this.NavigationStrategy.ChangePosition));
                                    documentModel.NavigationState = null;
                                };
                            }
                            this.ImmediateActionsManager.EnqueueAction(action);
                        });
                    }
                    else if (base.Pages != null)
                    {
                        base.ImmediateActionsManager.EnqueueAction(new DelegateAction(() => this.NavigationStrategy.ScrollToStartUp()));
                    }
                    base.ImmediateActionsManager.EnqueueAction(new Action(this.Update));
                }
            }
        }

        private void InvalidatePage(object sender, EventArgs e)
        {
            this.RenderContent();
        }

        protected internal void InvalidateRenderCaches()
        {
            this.Renderer.Reset();
        }

        private bool IsVisibleChild(Rect rect) => 
            rect.IntersectsWith(new Rect(0.0, 0.0, base.ItemsControl.ActualWidth, base.ItemsControl.ActualHeight));

        private void OnAddPages(NotifyCollectionChangedEventArgs e)
        {
            if ((base.Pages == null) || (base.PageDisplayMode == PageDisplayMode.Wrap))
            {
                this.Pages = (!this.Document.IsLoaded || !this.Document.Pages.Any<PageViewModel>()) ? null : this.Document.With<IDocumentViewModel, ObservableCollection<PageWrapper>>(x => this.GeneratePageWrappers((IEnumerable<IPage>) this.Document.Pages));
                base.ItemsControl.ItemsSource = base.Pages;
            }
            else if (base.PageDisplayMode != PageDisplayMode.Columns)
            {
                foreach (IPage page in e.NewItems.Cast<IPage>())
                {
                    base.Pages.Add(this.CreatePageWrapper(page));
                }
            }
            else
            {
                PageWrapper wrapper = base.Pages.LastOrDefault<PageWrapper>();
                if (wrapper == null)
                {
                    this.Pages = (!this.Document.IsLoaded || !this.Document.Pages.Any<PageViewModel>()) ? null : this.Document.With<IDocumentViewModel, ObservableCollection<PageWrapper>>(x => this.GeneratePageWrappers((IEnumerable<IPage>) this.Document.Pages));
                    base.ItemsControl.ItemsSource = base.Pages;
                }
                else
                {
                    IEnumerable<IPage> pages = Enumerable.Empty<IPage>();
                    bool flag = false;
                    if (wrapper.Pages.Count<IPage>() >= base.ColumnsCount)
                    {
                        int pageIndex = wrapper.Pages.Last<IPage>().PageIndex;
                        pages = pages = (IEnumerable<IPage>) this.Document.Pages.Skip<PageViewModel>((pageIndex + 1));
                    }
                    else
                    {
                        flag = true;
                        int pageIndex = wrapper.Pages.First<IPage>().PageIndex;
                        pages = (IEnumerable<IPage>) this.Document.Pages.Skip<PageViewModel>(pageIndex);
                    }
                    ObservableCollection<PageWrapper> source = this.GeneratePageWrappers(pages);
                    if (flag)
                    {
                        base.Pages[base.Pages.Count - 1] = source[0];
                    }
                    foreach (PageWrapper wrapper2 in flag ? source.Skip<PageWrapper>(1) : source)
                    {
                        base.Pages.Add(wrapper2);
                    }
                }
            }
            this.UpdateBehaviorProviderProperties();
            this.NavigationStrategy.GenerateStartUpState();
        }

        protected override void OnBehaviorProviderChanged(BehaviorProvider oldValue, BehaviorProvider newValue)
        {
            base.OnBehaviorProviderChanged(oldValue, newValue);
            if (oldValue != null)
            {
                oldValue.ZoomChanged -= new EventHandler<ZoomChangedEventArgs>(this.OnZoomChanged);
            }
            if (newValue != null)
            {
                newValue.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(this.OnZoomChanged);
            }
            if (this.SelectionService == null)
            {
                DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService service1 = new DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService(this);
                service1.Zoom = (float) newValue.ZoomFactor;
                this.SelectionService = service1;
                this.SelectionService.InvalidatePage += new EventHandler(this.InvalidatePage);
            }
        }

        protected override void OnDocumentChanged(IDocument oldValue, IDocument newValue)
        {
            base.OnDocumentChanged(oldValue, newValue);
            if (base.BehaviorProvider != null)
            {
                Action<DocumentNavigationStrategy> action1 = <>c.<>9__68_0;
                if (<>c.<>9__68_0 == null)
                {
                    Action<DocumentNavigationStrategy> local1 = <>c.<>9__68_0;
                    action1 = <>c.<>9__68_0 = x => x.ScrollToStartUp();
                }
                this.NavigationStrategy.Do<DocumentNavigationStrategy>(action1);
            }
            (oldValue as DocumentViewModel).Do<DocumentViewModel>(delegate (DocumentViewModel x) {
                x.Pages.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnPagesCollectionChanged);
                x.DocumentChanged -= new EventHandler(this.OnInnerDocumentChanged);
                x.Disposing -= new EventHandler(this.OnDocumentDisposing);
            });
            Action<DocumentPreviewRenderer> action = <>c.<>9__68_2;
            if (<>c.<>9__68_2 == null)
            {
                Action<DocumentPreviewRenderer> local2 = <>c.<>9__68_2;
                action = <>c.<>9__68_2 = x => x.Reset();
            }
            this.Renderer.Do<DocumentPreviewRenderer>(action);
            Action<DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService> action3 = <>c.<>9__68_3;
            if (<>c.<>9__68_3 == null)
            {
                Action<DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService> local3 = <>c.<>9__68_3;
                action3 = <>c.<>9__68_3 = x => x.ResetAll();
            }
            this.SelectionService.Do<DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService>(action3);
            (newValue as DocumentViewModel).Do<DocumentViewModel>(delegate (DocumentViewModel x) {
                x.Pages.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnPagesCollectionChanged);
                x.DocumentChanged += new EventHandler(this.OnInnerDocumentChanged);
                x.Disposing += new EventHandler(this.OnDocumentDisposing);
            });
            TextSearchParameter parameter1 = new TextSearchParameter();
            Func<BehaviorProvider, int> evaluator = <>c.<>9__68_5;
            if (<>c.<>9__68_5 == null)
            {
                Func<BehaviorProvider, int> local4 = <>c.<>9__68_5;
                evaluator = <>c.<>9__68_5 = x => x.PageIndex + 1;
            }
            new TextSearchParameter().CurrentPage = base.BehaviorProvider.Return<BehaviorProvider, int>(evaluator, <>c.<>9__68_6 ??= () => 1);
            this.SearchParameter = new TextSearchParameter();
        }

        private void OnDocumentDisposing(object sender, EventArgs e)
        {
            DocumentViewModel model = sender as DocumentViewModel;
            model.Pages.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnPagesCollectionChanged);
            model.DocumentChanged -= new EventHandler(this.OnInnerDocumentChanged);
            model.Disposing -= new EventHandler(this.OnDocumentDisposing);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            Action<Decorator> action = <>c.<>9__88_0;
            if (<>c.<>9__88_0 == null)
            {
                Action<Decorator> local1 = <>c.<>9__88_0;
                action = <>c.<>9__88_0 = x => x.Focus();
            }
            this.PresenterDecorator.Do<Decorator>(action);
        }

        private void OnInnerDocumentChanged(object sender, EventArgs e)
        {
            Func<Pair<Page, RectangleF>, int> selector = <>c.<>9__72_0;
            if (<>c.<>9__72_0 == null)
            {
                Func<Pair<Page, RectangleF>, int> local1 = <>c.<>9__72_0;
                selector = <>c.<>9__72_0 = x => x.First.Index;
            }
            int[] pages = this.GetPages().Select<Pair<Page, RectangleF>, int>(selector).ToArray<int>();
            (this.Document as DocumentViewModel).Do<DocumentViewModel>(x => x.AfterDrawPages(pages));
            if (!this.Document.IsRemoteReportDocumentSource())
            {
                Action<DocumentPreviewControl> action = <>c.<>9__72_2;
                if (<>c.<>9__72_2 == null)
                {
                    Action<DocumentPreviewControl> local2 = <>c.<>9__72_2;
                    action = <>c.<>9__72_2 = x => x.ThumbnailsSettings.RaiseInvalidate();
                }
                this.ActualDocumentViewer.Do<DocumentPreviewControl>(action);
            }
            this.Update();
        }

        protected override void OnItemsControlLoaded(object sender, RoutedEventArgs e)
        {
            base.OnItemsControlLoaded(sender, e);
            this.IsContentLoaded = true;
            base.ItemsPanel.Do<DocumentViewerPanel>(delegate (DocumentViewerPanel x) {
                x.RequestBringIntoView += new RequestBringIntoViewEventHandler(this.OnItemsPanel_RequestBringIntoView);
            });
            Func<DocumentPreviewControl, DocumentCommandProvider> evaluator = <>c.<>9__98_1;
            if (<>c.<>9__98_1 == null)
            {
                Func<DocumentPreviewControl, DocumentCommandProvider> local1 = <>c.<>9__98_1;
                evaluator = <>c.<>9__98_1 = x => x.ActualCommandProvider;
            }
            Action<DocumentCommandProvider> action = <>c.<>9__98_2;
            if (<>c.<>9__98_2 == null)
            {
                Action<DocumentCommandProvider> local2 = <>c.<>9__98_2;
                action = <>c.<>9__98_2 = x => x.UpdateCommands();
            }
            this.ActualDocumentViewer.With<DocumentPreviewControl, DocumentCommandProvider>(evaluator).Do<DocumentCommandProvider>(action);
            this.PresenterDecorator = ((PageSelector) base.ItemsControl).PresenterDecorator;
            if (base.IsFocused)
            {
                Action<Decorator> action2 = <>c.<>9__98_3;
                if (<>c.<>9__98_3 == null)
                {
                    Action<Decorator> local3 = <>c.<>9__98_3;
                    action2 = <>c.<>9__98_3 = x => x.Focus();
                }
                this.PresenterDecorator.Do<Decorator>(action2);
            }
            if (!this.Document.IsRemoteReportDocumentSource())
            {
                Action<DocumentPreviewControl> action3 = <>c.<>9__98_4;
                if (<>c.<>9__98_4 == null)
                {
                    Action<DocumentPreviewControl> local4 = <>c.<>9__98_4;
                    action3 = <>c.<>9__98_4 = x => x.ThumbnailsSettings.RaiseInvalidate();
                }
                this.ActualDocumentViewer.Do<DocumentPreviewControl>(action3);
            }
        }

        private void OnItemsPanel_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Action<DocumentPreviewRenderer> action = <>c.<>9__64_0;
            if (<>c.<>9__64_0 == null)
            {
                Action<DocumentPreviewRenderer> local1 = <>c.<>9__64_0;
                action = <>c.<>9__64_0 = x => x.UpdateInnerRenderer();
            }
            this.Renderer.Do<DocumentPreviewRenderer>(action);
        }

        private void OnPagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Replace)
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    Action<ObservableCollection<PageWrapper>> action = <>c.<>9__70_7;
                    if (<>c.<>9__70_7 == null)
                    {
                        Action<ObservableCollection<PageWrapper>> local3 = <>c.<>9__70_7;
                        action = <>c.<>9__70_7 = x => x.Clear();
                    }
                    base.Pages.Do<ObservableCollection<PageWrapper>>(action);
                    this.NavigationStrategy.GenerateStartUpState();
                }
                else if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    this.OnAddPages(e);
                }
                else
                {
                    this.Pages = (!this.Document.IsLoaded || !this.Document.Pages.Any<PageViewModel>()) ? null : this.Document.With<IDocumentViewModel, ObservableCollection<PageWrapper>>(x => this.GeneratePageWrappers((IEnumerable<IPage>) this.Document.Pages));
                    base.ItemsControl.ItemsSource = base.Pages;
                }
            }
            else
            {
                foreach (PageViewModel page in e.NewItems.OfType<PageViewModel>())
                {
                    PageWrapper item = base.Pages.Single<PageWrapper>(delegate (PageWrapper x) {
                        Func<IPage, bool> <>9__1;
                        Func<IPage, bool> predicate = <>9__1;
                        if (<>9__1 == null)
                        {
                            Func<IPage, bool> local1 = <>9__1;
                            predicate = <>9__1 = p => p.PageIndex == page.PageIndex;
                        }
                        return x.Pages.Any<IPage>(predicate);
                    });
                    int index = base.Pages.IndexOf(item);
                    IPage oldPage = item.Pages.Single<IPage>(x => x.PageIndex == page.PageIndex);
                    int num2 = item.Pages.IndexOf<IPage>(x => ReferenceEquals(x, oldPage));
                    List<IPage> list = new List<IPage>(item.Pages) {
                        [num2] = page
                    };
                    PageWrapper wrapper2 = this.CreatePageWrapper(list);
                    wrapper2.HorizontalPageSpacing = base.HorizontalPageSpacing;
                    wrapper2.VerticalPageSpacing = base.VerticalPageSpacing;
                    base.Pages[index] = wrapper2;
                    Func<Pair<Page, RectangleF>, int> selector = <>c.<>9__70_4;
                    if (<>c.<>9__70_4 == null)
                    {
                        Func<Pair<Page, RectangleF>, int> local1 = <>c.<>9__70_4;
                        selector = <>c.<>9__70_4 = x => x.First.Index;
                    }
                    int[] pages = this.GetPages().Select<Pair<Page, RectangleF>, int>(selector).ToArray<int>();
                    (this.Document as DocumentViewModel).Do<DocumentViewModel>(x => x.AfterDrawPages(pages));
                }
                Action<DocumentViewerPanel> action = <>c.<>9__70_6;
                if (<>c.<>9__70_6 == null)
                {
                    Action<DocumentViewerPanel> local2 = <>c.<>9__70_6;
                    action = <>c.<>9__70_6 = x => x.UpdateLayout();
                }
                base.ItemsPanel.Do<DocumentViewerPanel>(action);
            }
            base.BehaviorProvider.Do<BehaviorProvider>(x => x.PageSize = this.GetMaxPageSize());
            base.BehaviorProvider.Do<BehaviorProvider>(x => x.PageVisibleSize = this.GetMaxPageVisibleSize());
            if (!this.Document.IsRemoteReportDocumentSource())
            {
                Action<DocumentPreviewControl> action = <>c.<>9__70_11;
                if (<>c.<>9__70_11 == null)
                {
                    Action<DocumentPreviewControl> local4 = <>c.<>9__70_11;
                    action = <>c.<>9__70_11 = x => x.ThumbnailsSettings.RaiseInvalidate();
                }
                this.ActualDocumentViewer.Do<DocumentPreviewControl>(action);
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
            if (this.InteractionProvider.IsInSelecting)
            {
                e.Handled = true;
            }
        }

        protected override void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            base.OnScrollViewerScrollChanged(sender, e);
            this.InteractionProvider.OnScrollChanged(e);
            this.Document.Do<IDocumentViewModel>(delegate (IDocumentViewModel document) {
                if (document.Pages.Count > 0)
                {
                    Func<Pair<Page, RectangleF>, int> selector = <>c.<>9__78_1;
                    if (<>c.<>9__78_1 == null)
                    {
                        Func<Pair<Page, RectangleF>, int> local1 = <>c.<>9__78_1;
                        selector = <>c.<>9__78_1 = x => x.First.Index;
                    }
                    int[] pages = this.GetPages().Select<Pair<Page, RectangleF>, int>(selector).ToArray<int>();
                    (document as DocumentViewModel).Do<DocumentViewModel>(x => x.AfterDrawPages(pages));
                }
            });
        }

        protected void OnSelectionColorChanged(Color color)
        {
            this.SelectionService.Do<DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService>(delegate (DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService x) {
                x.SelectionColor = color.ToWinFormsColor();
                if (this.SelectionService.HasSelection)
                {
                    this.Update();
                }
            });
        }

        private void OnZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            this.SelectionService.Do<DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService>(x => x.Zoom = (float) e.ZoomFactor);
        }

        protected override void RenderContent()
        {
            this.UpdatePanel();
            this.renderLocker.DoLockedActionIfNotLocked(new Action(this.RenderNativeContent));
        }

        public void Update()
        {
            if (base.IsLoaded)
            {
                Func<IDocumentViewModel, bool> evaluator = <>c.<>9__94_0;
                if (<>c.<>9__94_0 == null)
                {
                    Func<IDocumentViewModel, bool> local1 = <>c.<>9__94_0;
                    evaluator = <>c.<>9__94_0 = x => x.IsLoaded;
                }
                if (this.Document.Return<IDocumentViewModel, bool>(evaluator, <>c.<>9__94_1 ??= () => false))
                {
                    this.UpdateInternal();
                }
            }
        }

        protected override void UpdateBehaviorProviderProperties()
        {
            this.PreviewBehaviorProvider.Do<DevExpress.Xpf.Printing.PreviewControl.Native.PreviewBehaviorProvider>(x => x.PageDisplayMode = base.PageDisplayMode);
            this.PreviewBehaviorProvider.Do<DevExpress.Xpf.Printing.PreviewControl.Native.PreviewBehaviorProvider>(delegate (DevExpress.Xpf.Printing.PreviewControl.Native.PreviewBehaviorProvider x) {
                Func<PageWrapper, Size> sizeSelector = <>c.<>9__67_2;
                if (<>c.<>9__67_2 == null)
                {
                    Func<PageWrapper, Size> local1 = <>c.<>9__67_2;
                    sizeSelector = <>c.<>9__67_2 = delegate (PageWrapper p) {
                        if (!p.Pages.Any<IPage>())
                        {
                            return Size.Empty;
                        }
                        double num = 0.0;
                        Size empty = Size.Empty;
                        foreach (IPage page in p.Pages)
                        {
                            Size pageSize = page.PageSize;
                            double num2 = page.PageSize.Width * pageSize.Height;
                            if (num2 > num)
                            {
                                empty = page.PageSize;
                                num = num2;
                            }
                        }
                        return empty;
                    };
                }
                x.RealPageSize = this.GetMaxPageSize(sizeSelector);
            });
            base.UpdateBehaviorProviderProperties();
        }

        protected virtual void UpdateInternal()
        {
            this.RenderContent();
        }

        protected override void UpdatePages()
        {
            base.UpdatePages();
            base.ImmediateActionsManager.EnqueueAction(new Action(this.Update));
        }

        protected internal void UpdatePanel()
        {
            if (base.ItemsPanel != null)
            {
                foreach (UIElement element in base.ItemsPanel.InternalChildren)
                {
                    element.InvalidateVisual();
                }
                base.ItemsPanel.InvalidatePanel();
            }
        }

        internal DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService SelectionService { get; set; }

        internal bool IsContentLoaded { get; private set; }

        private Decorator PresenterDecorator { get; set; }

        protected internal DocumentNavigationStrategy NavigationStrategy =>
            base.NavigationStrategy as DocumentNavigationStrategy;

        protected internal DocumentPreviewControl ActualDocumentViewer =>
            base.ActualDocumentViewer as DocumentPreviewControl;

        internal UserInputController InputController =>
            base.KeyboardAndMouseController as UserInputController;

        public IDocumentViewModel Document =>
            (IDocumentViewModel) base.Document;

        protected internal DevExpress.Xpf.Printing.PreviewControl.Native.InteractionProvider InteractionProvider { get; private set; }

        protected internal DevExpress.Xpf.Printing.PreviewControl.Native.EditingStrategy EditingStrategy { get; private set; }

        protected internal DocumentPreviewRenderer Renderer =>
            base.Renderer as DocumentPreviewRenderer;

        internal DevExpress.Xpf.Printing.PreviewControl.Native.PreviewBehaviorProvider PreviewBehaviorProvider =>
            base.BehaviorProvider as DevExpress.Xpf.Printing.PreviewControl.Native.PreviewBehaviorProvider;

        public Color HighlightSelectionColor
        {
            get => 
                (Color) base.GetValue(HighlightSelectionColorProperty);
            set => 
                base.SetValue(HighlightSelectionColorProperty, value);
        }

        public CursorModeType CursorMode
        {
            get
            {
                Func<DocumentPreviewControl, CursorModeType> evaluator = <>c.<>9__46_0;
                if (<>c.<>9__46_0 == null)
                {
                    Func<DocumentPreviewControl, CursorModeType> local1 = <>c.<>9__46_0;
                    evaluator = <>c.<>9__46_0 = x => x.CursorMode;
                }
                return this.ActualDocumentViewer.Return<DocumentPreviewControl, CursorModeType>(evaluator, (<>c.<>9__46_1 ??= () => CursorModeType.SelectTool));
            }
        }

        public ImageSource RenderedSource
        {
            get => 
                (ImageSource) base.GetValue(RenderedSourceProperty);
            private set => 
                base.SetValue(RenderedSourcePropertyKey, value);
        }

        public DrawingBrush RenderMask
        {
            get => 
                (DrawingBrush) base.GetValue(RenderMaskProperty);
            private set => 
                base.SetValue(RenderMaskPropertyKey, value);
        }

        public DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle SelectionRectangle
        {
            get => 
                (DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle) base.GetValue(SelectionRectangleProperty);
            private set => 
                base.SetValue(SelectionRectanglePropertyKey, value);
        }

        public TextSearchParameter SearchParameter
        {
            get => 
                (TextSearchParameter) base.GetValue(SearchParameterProperty);
            private set => 
                base.SetValue(SearchParameterPropertyKey, value);
        }

        public System.Windows.Controls.ScrollViewer ScrollViewer =>
            base.ScrollViewer;

        public DocumentInplaceEditorOwner ActiveEditorOwner { get; private set; }

        public bool IsInEditing =>
            this.ActiveEditorOwner != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Printing.DocumentPresenterControl.<>c <>9 = new DevExpress.Xpf.Printing.DocumentPresenterControl.<>c();
            public static Func<DocumentPreviewControl, CursorModeType> <>9__46_0;
            public static Func<CursorModeType> <>9__46_1;
            public static Action<DocumentPreviewRenderer> <>9__64_0;
            public static Func<PageWrapper, Size> <>9__67_2;
            public static Action<DocumentNavigationStrategy> <>9__68_0;
            public static Action<DocumentPreviewRenderer> <>9__68_2;
            public static Action<SelectionService> <>9__68_3;
            public static Func<BehaviorProvider, int> <>9__68_5;
            public static Func<int> <>9__68_6;
            public static Func<Pair<Page, RectangleF>, int> <>9__70_4;
            public static Action<DocumentViewerPanel> <>9__70_6;
            public static Action<ObservableCollection<PageWrapper>> <>9__70_7;
            public static Action<DocumentPreviewControl> <>9__70_11;
            public static Func<Pair<Page, RectangleF>, int> <>9__72_0;
            public static Action<DocumentPreviewControl> <>9__72_2;
            public static Func<PageWrapper, Size> <>9__73_0;
            public static Func<PageWrapper, Size> <>9__74_0;
            public static Func<IDocumentViewModel, bool> <>9__77_0;
            public static Func<bool> <>9__77_1;
            public static Func<Pair<Page, RectangleF>, int> <>9__78_1;
            public static Action<Decorator> <>9__88_0;
            public static Func<IDocumentViewModel, bool> <>9__94_0;
            public static Func<bool> <>9__94_1;
            public static Func<DocumentPreviewControl, DocumentCommandProvider> <>9__98_1;
            public static Action<DocumentCommandProvider> <>9__98_2;
            public static Action<Decorator> <>9__98_3;
            public static Action<DocumentPreviewControl> <>9__98_4;
            public static Func<Pair<IPage, Rect>, RenderItem> <>9__107_0;
            public static Func<Pair<IPage, Rect>, Pair<Page, RectangleF>> <>9__113_0;
            public static Func<RenderItem, bool> <>9__115_1;

            internal void <.cctor>b__61_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Printing.DocumentPresenterControl) d).OnSelectionColorChanged((Color) e.NewValue);
            }

            internal bool <DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.ISupportInvalidateRenderingOnIdle.InvalidateRenderingOnIdle>b__115_1(RenderItem x) => 
                x.NeedsInvalidate;

            internal CursorModeType <get_CursorMode>b__46_0(DocumentPreviewControl x) => 
                x.CursorMode;

            internal CursorModeType <get_CursorMode>b__46_1() => 
                CursorModeType.SelectTool;

            internal RenderItem <GetDrawingContent>b__107_0(Pair<IPage, Rect> x)
            {
                IDocumentPage first = (IDocumentPage) x.First;
                RenderItem item1 = new RenderItem();
                item1.Rectangle = x.Second;
                item1.Page = first;
                item1.NeedsInvalidate = first.NeedsInvalidate;
                item1.ForceInvalidate = first.ForceInvalidate;
                item1.TextureType = TextureType.Content;
                return item1;
            }

            internal Size <GetMaxPageSize>b__73_0(PageWrapper x) => 
                x.PageSize;

            internal Size <GetMaxPageVisibleSize>b__74_0(PageWrapper x) => 
                x.VisibleSize;

            internal Pair<Page, RectangleF> <GetPages>b__113_0(Pair<IPage, Rect> x) => 
                new Pair<Page, RectangleF>(((IDocumentPage) x.First).Page, x.Second.ToWinFormsRectangle());

            internal bool <Initialize>b__77_0(IDocumentViewModel x) => 
                x.IsLoaded;

            internal bool <Initialize>b__77_1() => 
                false;

            internal void <OnDocumentChanged>b__68_0(DocumentNavigationStrategy x)
            {
                x.ScrollToStartUp();
            }

            internal void <OnDocumentChanged>b__68_2(DocumentPreviewRenderer x)
            {
                x.Reset();
            }

            internal void <OnDocumentChanged>b__68_3(SelectionService x)
            {
                x.ResetAll();
            }

            internal int <OnDocumentChanged>b__68_5(BehaviorProvider x) => 
                x.PageIndex + 1;

            internal int <OnDocumentChanged>b__68_6() => 
                1;

            internal void <OnGotFocus>b__88_0(Decorator x)
            {
                x.Focus();
            }

            internal int <OnInnerDocumentChanged>b__72_0(Pair<Page, RectangleF> x) => 
                x.First.Index;

            internal void <OnInnerDocumentChanged>b__72_2(DocumentPreviewControl x)
            {
                x.ThumbnailsSettings.RaiseInvalidate();
            }

            internal DocumentCommandProvider <OnItemsControlLoaded>b__98_1(DocumentPreviewControl x) => 
                x.ActualCommandProvider;

            internal void <OnItemsControlLoaded>b__98_2(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <OnItemsControlLoaded>b__98_3(Decorator x)
            {
                x.Focus();
            }

            internal void <OnItemsControlLoaded>b__98_4(DocumentPreviewControl x)
            {
                x.ThumbnailsSettings.RaiseInvalidate();
            }

            internal void <OnLoaded>b__64_0(DocumentPreviewRenderer x)
            {
                x.UpdateInnerRenderer();
            }

            internal void <OnPagesCollectionChanged>b__70_11(DocumentPreviewControl x)
            {
                x.ThumbnailsSettings.RaiseInvalidate();
            }

            internal int <OnPagesCollectionChanged>b__70_4(Pair<Page, RectangleF> x) => 
                x.First.Index;

            internal void <OnPagesCollectionChanged>b__70_6(DocumentViewerPanel x)
            {
                x.UpdateLayout();
            }

            internal void <OnPagesCollectionChanged>b__70_7(ObservableCollection<PageWrapper> x)
            {
                x.Clear();
            }

            internal int <OnScrollViewerScrollChanged>b__78_1(Pair<Page, RectangleF> x) => 
                x.First.Index;

            internal bool <Update>b__94_0(IDocumentViewModel x) => 
                x.IsLoaded;

            internal bool <Update>b__94_1() => 
                false;

            internal Size <UpdateBehaviorProviderProperties>b__67_2(PageWrapper p)
            {
                if (!p.Pages.Any<IPage>())
                {
                    return Size.Empty;
                }
                double num = 0.0;
                Size empty = Size.Empty;
                foreach (IPage page in p.Pages)
                {
                    Size pageSize = page.PageSize;
                    double num2 = page.PageSize.Width * pageSize.Height;
                    if (num2 > num)
                    {
                        empty = page.PageSize;
                        num = num2;
                    }
                }
                return empty;
            }
        }
    }
}

