namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Internal;
    using DevExpress.Xpf.PdfViewer.Themes;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class PdfPresenterControl : DocumentPresenterControl
    {
        private static readonly DependencyPropertyKey SearchParameterPropertyKey;
        public static readonly DependencyProperty SearchParameterProperty;
        public static readonly DependencyProperty AllowCurrentPageHighlightingProperty;
        private static readonly DependencyPropertyKey SelectionRectanglePropertyKey;
        public static readonly DependencyProperty SelectionRectangleProperty;
        public static readonly DependencyProperty EnableCaretAnimationProperty;

        static PdfPresenterControl()
        {
            Type ownerType = typeof(PdfPresenterControl);
            SearchParameterPropertyKey = DependencyPropertyManager.RegisterReadOnly("SearchParameter", typeof(TextSearchParameter), ownerType, new FrameworkPropertyMetadata(null));
            SearchParameterProperty = SearchParameterPropertyKey.DependencyProperty;
            AllowCurrentPageHighlightingProperty = DependencyPropertyManager.Register("AllowCurrentPageHighlighting", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (obj, args) => ((PdfPresenterControl) obj).OnAllowCurrentPageHighlightingChanged((bool) args.NewValue)));
            SelectionRectanglePropertyKey = DependencyPropertyManager.RegisterReadOnly("SelectionRectangle", typeof(DevExpress.Xpf.PdfViewer.SelectionRectangle), ownerType, new FrameworkPropertyMetadata(null));
            SelectionRectangleProperty = SelectionRectanglePropertyKey.DependencyProperty;
            EnableCaretAnimationProperty = DependencyPropertyManager.Register("EnableCaretAnimation", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
        }

        public PdfPresenterControl()
        {
            base.DefaultStyleKey = typeof(PdfPresenterControl);
            Point startPoint = new Point();
            this.SelectionRectangle = new DevExpress.Xpf.PdfViewer.SelectionRectangle(startPoint, Size.Empty);
            this.EditingStrategy = this.CreateEditingStrategy();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        protected virtual void AllowCachePagesChanged(bool newValue)
        {
            if (base.IsInitialized)
            {
                this.UpdateRenderer();
            }
        }

        public void AttachEditorToTree(CellEditorOwner cellEditorOwner, int pageIndex, Func<Rect> rectHandler, double angle)
        {
            PdfPageControl control = (PdfPageControl) LayoutHelper.FindElement(this.ItemsPanel, delegate (FrameworkElement fr) {
                Func<PdfPageControl, bool> <>9__1;
                Func<PdfPageControl, bool> evaluator = <>9__1;
                if (<>9__1 == null)
                {
                    Func<PdfPageControl, bool> local1 = <>9__1;
                    evaluator = <>9__1 = delegate (PdfPageControl x) {
                        Func<IPage, bool> <>9__2;
                        Func<IPage, bool> predicate = <>9__2;
                        if (<>9__2 == null)
                        {
                            Func<IPage, bool> local1 = <>9__2;
                            predicate = <>9__2 = page => page.PageIndex == pageIndex;
                        }
                        return ((PdfPageWrapper) x.DataContext).Pages.Any<IPage>(predicate);
                    };
                }
                return (fr as PdfPageControl).If<PdfPageControl>(evaluator).ReturnSuccess<PdfPageControl>();
            });
            if (control != null)
            {
                this.ActiveEditorOwner = cellEditorOwner;
                cellEditorOwner.Page = control;
                control.AddEditor(cellEditorOwner.VisualHost, rectHandler, angle);
            }
        }

        internal void BringCurrentSelectionPointIntoView()
        {
            this.KeyboardAndMouseController.BringCurrentSelectionPointIntoView();
        }

        protected virtual void CacheSizeChanged(int newValue)
        {
            if (base.IsInitialized)
            {
                this.UpdateRenderer();
            }
        }

        private IEnumerable<PdfElement> CalcCaretObject(PdfPageWrapper pageWrapper, PdfPageViewModel page, PdfCaret caret)
        {
            if (caret.Position.PageIndex != page.PageIndex)
            {
                return null;
            }
            List<PdfElement> list1 = new List<PdfElement>();
            list1.Add(new PdfCaretElement(new SolidColorBrush(this.CaretColor), pageWrapper, base.BehaviorProvider.ZoomFactor, base.BehaviorProvider.RotateAngle, caret, this.EnableCaretAnimation));
            return list1;
        }

        private Rect CalcCaretRect(PdfCaret caret)
        {
            int pageIndex = caret.Position.PageIndex;
            int pageWrapperIndex = this.NavigationStrategy.PositionCalculator.GetPageWrapperIndex(pageIndex);
            PdfPageWrapper wrapper = (PdfPageWrapper) base.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            PdfPageViewModel page = wrapper.Pages.Single<IPage>(x => (x.PageIndex == pageIndex)) as PdfPageViewModel;
            Rect pageRect = wrapper.GetPageRect(page);
            Point point = page.GetPoint(caret.ViewData.TopLeft, base.BehaviorProvider.ZoomFactor, base.BehaviorProvider.RotateAngle);
            point = new Point(point.X + pageRect.Left, point.Y + pageRect.Top);
            Point point2 = page.GetPoint(new PdfPoint(caret.ViewData.TopLeft.X, caret.ViewData.TopLeft.Y - caret.ViewData.Height), base.BehaviorProvider.ZoomFactor, base.BehaviorProvider.RotateAngle);
            return new Rect(point, new Point(point2.X + pageRect.Left, point2.Y + pageRect.Top));
        }

        private void CalcMaxPageWrapperParams(double pageWrapperCenter, out double maxPageWidth, out double maxPageMargin)
        {
            maxPageWidth = 0.0;
            maxPageMargin = 0.0;
            Func<BehaviorProvider, double> evaluator = <>c.<>9__105_0;
            if (<>c.<>9__105_0 == null)
            {
                Func<BehaviorProvider, double> local1 = <>c.<>9__105_0;
                evaluator = <>c.<>9__105_0 = x => x.RotateAngle;
            }
            bool flag = ((base.BehaviorProvider.Return<BehaviorProvider, double>(evaluator, (<>c.<>9__105_1 ??= () => 0.0)) / 90.0) % 2.0) == 0.0;
            foreach (PageWrapper wrapper in base.Pages)
            {
                double num = flag ? wrapper.Pages.Last<IPage>().PageSize.Width : wrapper.Pages.Last<IPage>().PageSize.Height;
                if (num.GreaterThan(maxPageWidth))
                {
                    maxPageWidth = num;
                    maxPageMargin = wrapper.CalcMarginSize().Width;
                }
            }
            maxPageWidth += pageWrapperCenter;
        }

        private Rect CalcPageRect(RenderItem pair, Rect rect)
        {
            Control control = (Control) base.ItemsControl.ItemContainerGenerator.ContainerFromItem(pair.PageWrapper);
            double num = (control.Padding.Left + control.Padding.Right) / 2.0;
            double num2 = (control.Padding.Top + control.Padding.Bottom) / 2.0;
            rect.Inflate(-num, -num2);
            rect.Inflate(0.0, -base.VerticalPageSpacing);
            return rect;
        }

        private double CalcPageWrapperTwoPageCenter()
        {
            double num = 0.0;
            Func<BehaviorProvider, double> evaluator = <>c.<>9__106_0;
            if (<>c.<>9__106_0 == null)
            {
                Func<BehaviorProvider, double> local1 = <>c.<>9__106_0;
                evaluator = <>c.<>9__106_0 = x => x.RotateAngle;
            }
            bool flag = ((base.BehaviorProvider.Return<BehaviorProvider, double>(evaluator, (<>c.<>9__106_1 ??= () => 0.0)) / 90.0) % 2.0) == 0.0;
            foreach (PageWrapper wrapper in base.Pages)
            {
                double num2 = flag ? wrapper.Pages.First<IPage>().PageSize.Width : wrapper.Pages.First<IPage>().PageSize.Height;
                if (num2.GreaterThan(num))
                {
                    num = num2;
                }
            }
            return num;
        }

        protected internal Point CalcPoint(int pageIndex, PdfPoint point)
        {
            int pageWrapperIndex = this.NavigationStrategy.PositionCalculator.GetPageWrapperIndex(pageIndex);
            PdfPageWrapper wrapper = (PdfPageWrapper) base.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            PdfPageViewModel page = (PdfPageViewModel) wrapper.Pages.Single<IPage>(x => (x.PageIndex == pageIndex));
            Point point2 = page.GetPoint(point, base.BehaviorProvider.ZoomFactor, base.BehaviorProvider.RotateAngle);
            Rect pageRect = wrapper.GetPageRect(page);
            return new Point(point2.X + pageRect.Left, point2.Y + pageRect.Top);
        }

        protected internal Rect CalcRect(int pageIndex, PdfPoint topLeft, PdfPoint bottomRight) => 
            new Rect(this.CalcPoint(pageIndex, topLeft), this.CalcPoint(pageIndex, bottomRight));

        private IEnumerable<PdfElement> CalcRenderContent(PdfPageWrapper pageWrapper, PdfPageViewModel page)
        {
            Func<IPdfDocument, PdfCaret> evaluator = <>c.<>9__80_0;
            if (<>c.<>9__80_0 == null)
            {
                Func<IPdfDocument, PdfCaret> local1 = <>c.<>9__80_0;
                evaluator = <>c.<>9__80_0 = x => x.Caret;
            }
            return ((this.Document.With<IPdfDocument, PdfCaret>(evaluator) == null) ? Enumerable.Empty<PdfElement>() : this.CalcCaretObject(pageWrapper, page, this.Document.Caret));
        }

        public Point ConvertDocumentPositionToPoint(PdfDocumentPosition documentPosition) => 
            !this.HasPages ? new Point(0.0, 0.0) : this.NavigationStrategy.ProcessConvertDocumentPosition(documentPosition);

        public PdfDocumentPosition ConvertPointToDocumentPosition(Point point) => 
            !this.HasPages ? null : this.NavigationStrategy.ProcessConvertPoint(point);

        public PdfDocumentPosition ConvertPointToDocumentPosition(Point point, bool inPageBounds)
        {
            if (!this.HasPages)
            {
                return null;
            }
            Point point2 = base.TranslatePoint(new Point(0.0, 0.0), this.ActualPdfViewer);
            Point point3 = new Point(point.X - point2.X, point.Y - point2.Y);
            if (!inPageBounds)
            {
                return this.NavigationStrategy.ProcessConvertPoint(point);
            }
            if (base.IsInBounds(point3))
            {
                return this.NavigationStrategy.ProcessConvertPoint(point);
            }
            return new PdfDocumentPosition(1, new PdfPoint());
        }

        protected virtual InplaceEditingStrategy CreateEditingStrategy() => 
            new InplaceEditingStrategy(this);

        protected override DevExpress.Xpf.DocumentViewer.KeyboardAndMouseController CreateKeyboardAndMouseController() => 
            new PdfKeyboardAndMouseController(this);

        protected override DocumentViewerRenderer CreateNativeRenderer()
        {
            Func<PdfViewerControl, PdfViewerBackend> evaluator = <>c.<>9__72_0;
            if (<>c.<>9__72_0 == null)
            {
                Func<PdfViewerControl, PdfViewerBackend> local1 = <>c.<>9__72_0;
                evaluator = <>c.<>9__72_0 = x => x.ViewerBackend;
            }
            return new PdfViewerDocumentRenderer(this, this.ActualPdfViewer.With<PdfViewerControl, PdfViewerBackend>(evaluator));
        }

        protected override DevExpress.Xpf.DocumentViewer.NavigationStrategy CreateNavigationStrategy() => 
            new PdfNavigationStrategy(this);

        protected override PageWrapper CreatePageWrapper(IPage page)
        {
            PdfPageWrapper wrapper1 = new PdfPageWrapper((PdfPageViewModel) page);
            wrapper1.ZoomFactor = base.BehaviorProvider.ZoomFactor;
            wrapper1.RotateAngle = base.BehaviorProvider.RotateAngle;
            return wrapper1;
        }

        protected override PageWrapper CreatePageWrapper(IEnumerable<IPage> pages)
        {
            PdfPageWrapper wrapper1 = new PdfPageWrapper(pages.Cast<PdfPageViewModel>());
            wrapper1.ZoomFactor = base.BehaviorProvider.ZoomFactor;
            wrapper1.RotateAngle = base.BehaviorProvider.RotateAngle;
            return wrapper1;
        }

        protected override TouchController CreateTouchController() => 
            new PdfTouchController(this);

        public void DetachEditorFromTree()
        {
            if (this.ActiveEditorOwner != null)
            {
                this.ActiveEditorOwner.Page.RemoveEditor();
                this.ActiveEditorOwner = null;
            }
        }

        public void EndEditing()
        {
            this.EditingStrategy.EndEditing();
        }

        internal CellEditor GenerateEditor(PdfEditorSettings editorSettings) => 
            this.EditingStrategy.GenerateEditor(editorSettings);

        internal DrawingBrush GenerateRenderMask(IEnumerable<RenderItem> drawingContent)
        {
            GeometryGroup group = new GeometryGroup();
            foreach (RenderItem item in drawingContent)
            {
                Rect rect = this.CalcPageRect(item, item.Rect);
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
            this.GetDrawingContent(this.ItemsPanel);

        private IEnumerable<RenderItem> GetDrawingContent(DocumentViewerPanel panel)
        {
            List<RenderItem> list = new List<RenderItem>();
            if (panel != null)
            {
                foreach (FrameworkElement element in panel.InternalChildren)
                {
                    PageWrapper wrapper = (PageWrapper) base.ItemsControl.ItemContainerGenerator.ItemFromContainer(element);
                    Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(element, base.ItemsControl);
                    foreach (IPage page in wrapper.Pages)
                    {
                        Rect pageRect = wrapper.GetPageRect(page);
                        pageRect.Offset(relativeElementRect.Left, relativeElementRect.Top);
                        if (this.IsVisibleChild(pageRect))
                        {
                            PdfPageViewModel model = (PdfPageViewModel) page;
                            RenderItem item = new RenderItem();
                            item.Rect = pageRect;
                            item.PageWrapper = wrapper;
                            item.Page = model;
                            item.NeedsInvalidate = model.NeedsInvalidate;
                            item.ForceInvalidate = model.ForceInvalidate;
                            item.TextureType = TextureType.Content;
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public void HideTooltip()
        {
            if (this.ActiveTooltipPage != null)
            {
                this.ActiveTooltipPage.HidePopup();
                this.ActiveTooltipPage = null;
            }
        }

        public PdfHitTestResult HitTest(Point point) => 
            !this.HasPages ? null : this.NavigationStrategy.ProcessHitTest(point);

        protected override void Initialize()
        {
            base.Initialize();
            if (((base.ItemsControl != null) && base.IsInitialized) && this.HasPages)
            {
                this.UpdatePageWrappersProperties();
                this.ImmediateActionsManager.EnqueueAction(new DelegateAction(new Action(this.Update)));
                this.Document.Do<IPdfDocument>(delegate (IPdfDocument x) {
                    Func<BehaviorProvider, int> evaluator = <>c.<>9__107_1;
                    if (<>c.<>9__107_1 == null)
                    {
                        Func<BehaviorProvider, int> local1 = <>c.<>9__107_1;
                        evaluator = <>c.<>9__107_1 = y => y.PageIndex;
                    }
                    x.SetCurrentPage(base.BehaviorProvider.Return<BehaviorProvider, int>(evaluator, <>c.<>9__107_2 ??= () => 1), this.AllowCurrentPageHighlighting);
                });
            }
        }

        protected internal void InvalidateRenderCaches()
        {
            Action<DocumentViewerRenderer> action = <>c.<>9__116_0;
            if (<>c.<>9__116_0 == null)
            {
                Action<DocumentViewerRenderer> local1 = <>c.<>9__116_0;
                action = <>c.<>9__116_0 = x => x.Reset();
            }
            base.Renderer.Do<DocumentViewerRenderer>(action);
        }

        public void InvalidateRenderingOnIdle()
        {
            base.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, delegate {
                if (this.IsInVisualTree())
                {
                    Func<RenderItem, bool> predicate = <>c.<>9__121_1;
                    if (<>c.<>9__121_1 == null)
                    {
                        Func<RenderItem, bool> local1 = <>c.<>9__121_1;
                        predicate = <>c.<>9__121_1 = x => x.NeedsInvalidate;
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

        public bool IsRectangleInView(int pageIndex, Rect rect) => 
            this.NavigationStrategy.IsRectangleInView(pageIndex, rect);

        private bool IsVisibleChild(Rect rect) => 
            rect.IntersectsWith(new Rect(0.0, 0.0, base.ItemsControl.ActualWidth, base.ItemsControl.ActualHeight));

        protected virtual void OnAllowCurrentPageHighlightingChanged(bool newValue)
        {
            this.Document.Do<IPdfDocument>(x => x.SetCurrentPage(this.BehaviorProvider.PageIndex, newValue));
        }

        protected override void OnBehaviorProviderChanged(BehaviorProvider oldValue, BehaviorProvider newValue)
        {
            base.OnBehaviorProviderChanged(oldValue, newValue);
            oldValue.Do<BehaviorProvider>(delegate (BehaviorProvider x) {
                x.PageIndexChanged -= new EventHandler<PageIndexChangedEventArgs>(this.PageIndexChanged);
            });
            newValue.Do<BehaviorProvider>(delegate (BehaviorProvider x) {
                x.PageIndexChanged += new EventHandler<PageIndexChangedEventArgs>(this.PageIndexChanged);
            });
        }

        protected override void OnBehaviorProviderRotateAngleChanged(object sender, RotateAngleChangedEventArgs e)
        {
            base.OnBehaviorProviderRotateAngleChanged(sender, e);
            this.UpdatePageWrappersProperties();
        }

        protected override void OnBehaviorProviderZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            base.OnBehaviorProviderZoomChanged(sender, e);
            Func<PdfViewerControl, PdfViewerBackend> evaluator = <>c.<>9__97_0;
            if (<>c.<>9__97_0 == null)
            {
                Func<PdfViewerControl, PdfViewerBackend> local1 = <>c.<>9__97_0;
                evaluator = <>c.<>9__97_0 = x => x.ViewerBackend;
            }
            Action<PdfViewerBackend> action = <>c.<>9__97_1;
            if (<>c.<>9__97_1 == null)
            {
                Action<PdfViewerBackend> local2 = <>c.<>9__97_1;
                action = <>c.<>9__97_1 = x => x.ClearPageCache();
            }
            this.ActualPdfViewer.With<PdfViewerControl, PdfViewerBackend>(evaluator).Do<PdfViewerBackend>(action);
            if (this.IsInEditing)
            {
                Action<CellEditor> action2 = <>c.<>9__97_2;
                if (<>c.<>9__97_2 == null)
                {
                    Action<CellEditor> local3 = <>c.<>9__97_2;
                    action2 = <>c.<>9__97_2 = x => x.InvalidateEditor();
                }
                (this.ActiveEditorOwner.CurrentCellEditor as CellEditor).Do<CellEditor>(action2);
            }
        }

        protected override void OnDecoratorMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.ReleaseMouseCapture();
            HitTestResult input = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            Func<HitTestResult, DependencyObject> evaluator = <>c.<>9__75_0;
            if (<>c.<>9__75_0 == null)
            {
                Func<HitTestResult, DependencyObject> local1 = <>c.<>9__75_0;
                evaluator = <>c.<>9__75_0 = x => x.VisualHit;
            }
            if (input.With<HitTestResult, DependencyObject>(evaluator) == null)
            {
                this.KeyboardAndMouseController.ReleaseSelectionRectangle();
            }
            else if ((LayoutHelper.FindParentObject<ScrollBar>(input.VisualHit) == null) && this.HasPages)
            {
                this.KeyboardAndMouseController.ProcessMouseLeftButtonUp(e);
            }
            else
            {
                this.KeyboardAndMouseController.ReleaseSelectionRectangle();
            }
        }

        protected override void OnDocumentChanged(IDocument oldValue, IDocument newValue)
        {
            Func<IDocument, PdfDocumentViewModel> evaluator = <>c.<>9__64_0;
            if (<>c.<>9__64_0 == null)
            {
                Func<IDocument, PdfDocumentViewModel> local1 = <>c.<>9__64_0;
                evaluator = <>c.<>9__64_0 = x => x as PdfDocumentViewModel;
            }
            oldValue.With<IDocument, PdfDocumentViewModel>(evaluator).Do<PdfDocumentViewModel>(delegate (PdfDocumentViewModel x) {
                x.DocumentProgressChanged -= new EventHandler<DocumentProgressChangedEventArgs>(this.OnDocumentProgressChanged);
            });
            base.OnDocumentChanged(oldValue, newValue);
            Func<IDocument, PdfDocumentViewModel> func2 = <>c.<>9__64_2;
            if (<>c.<>9__64_2 == null)
            {
                Func<IDocument, PdfDocumentViewModel> local2 = <>c.<>9__64_2;
                func2 = <>c.<>9__64_2 = x => x as PdfDocumentViewModel;
            }
            newValue.With<IDocument, PdfDocumentViewModel>(func2).Do<PdfDocumentViewModel>(delegate (PdfDocumentViewModel x) {
                x.DocumentProgressChanged += new EventHandler<DocumentProgressChangedEventArgs>(this.OnDocumentProgressChanged);
            });
            Action<DocumentViewerRenderer> action = <>c.<>9__64_4;
            if (<>c.<>9__64_4 == null)
            {
                Action<DocumentViewerRenderer> local3 = <>c.<>9__64_4;
                action = <>c.<>9__64_4 = x => x.Reset();
            }
            base.Renderer.Do<DocumentViewerRenderer>(action);
            this.SearchParameter = (newValue != null) ? new TextSearchParameter() : null;
        }

        private void OnDocumentProgressChanged(object sender, DocumentProgressChangedEventArgs args)
        {
            if (args.IsCompleted)
            {
                this.Initialize();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.UpdateRenderer();
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            ThemeManager.AddThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
        }

        protected override void OnScrollViewerViewportChanged(object sender, ScrollChangedEventArgs e)
        {
            base.OnScrollViewerViewportChanged(sender, e);
        }

        protected override void OnShowSingleItemChanged(bool newValue)
        {
            base.OnShowSingleItemChanged(newValue);
            this.ImmediateActionsManager.EnqueueAction(new DelegateAction(() => this.Update()));
        }

        private void OnThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            ThemeTreeWalker treeWalker = ThemeManager.GetTreeWalker(sender);
            if (treeWalker.IsTouch)
            {
                ThemeManager.SetThemeName(sender, ThemeHelper.GetTreewalkerThemeName(treeWalker, true));
            }
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ThemeManager.RemoveThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
        }

        private void PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.Document.Do<IPdfDocument>(x => x.SetCurrentPage(e.PageIndex, this.AllowCurrentPageHighlighting));
        }

        protected override void RenderManagedContent()
        {
            this.UpdatePanel();
        }

        public void ScrollIntoView(PdfTarget target)
        {
            this.NavigationStrategy.ScrollIntoView(target);
        }

        public void ScrollIntoView(int pageIndex)
        {
            this.ScrollIntoView(pageIndex, Rect.Empty, ScrollIntoViewMode.TopLeft);
        }

        public void ScrollIntoView(int pageIndex, Rect rect, ScrollIntoViewMode mode)
        {
            this.NavigationStrategy.ScrollIntoView(pageIndex, rect, mode);
        }

        public void ShowTooltip(PdfToolTipSettings toolTipSettings)
        {
            int pageIndex = toolTipSettings.DocumentArea.PageIndex;
            PdfPageControl control = (PdfPageControl) LayoutHelper.FindElement(this.ItemsPanel, delegate (FrameworkElement fr) {
                Func<PdfPageControl, bool> <>9__1;
                Func<PdfPageControl, bool> evaluator = <>9__1;
                if (<>9__1 == null)
                {
                    Func<PdfPageControl, bool> local1 = <>9__1;
                    evaluator = <>9__1 = delegate (PdfPageControl x) {
                        Func<IPage, bool> <>9__2;
                        Func<IPage, bool> predicate = <>9__2;
                        if (<>9__2 == null)
                        {
                            Func<IPage, bool> local1 = <>9__2;
                            predicate = <>9__2 = page => page.PageIndex == pageIndex;
                        }
                        return ((PdfPageWrapper) x.DataContext).Pages.Any<IPage>(predicate);
                    };
                }
                return (fr as PdfPageControl).If<PdfPageControl>(evaluator).ReturnSuccess<PdfPageControl>();
            });
            if (control != null)
            {
                this.ActiveTooltipPage = control;
                control.ShowPopup(toolTipSettings.Title, toolTipSettings.Text, () => this.CalcRect(pageIndex, toolTipSettings.DocumentArea.Area.TopLeft, toolTipSettings.DocumentArea.Area.BottomRight));
            }
        }

        public void StartEditing(PdfEditorSettings editorSettings, IPdfViewerValueEditingCallBack valueEditing)
        {
            this.EditingStrategy.StartEditing(editorSettings, valueEditing);
        }

        public void Update()
        {
            if (this.HasPages)
            {
                this.UpdateInternal();
            }
        }

        protected virtual void UpdateInternal()
        {
            if (this.ActualPdfViewer != null)
            {
                Func<IPdfDocument, IPdfDocumentSelectionResults> evaluator = <>c.<>9__56_0;
                if (<>c.<>9__56_0 == null)
                {
                    Func<IPdfDocument, IPdfDocumentSelectionResults> local1 = <>c.<>9__56_0;
                    evaluator = <>c.<>9__56_0 = x => x.SelectionResults;
                }
                this.ActualPdfViewer.HasSelection = this.Document.With<IPdfDocument, IPdfDocumentSelectionResults>(evaluator) != null;
            }
            PdfDocumentViewModel document = this.Document as PdfDocumentViewModel;
            if (document != null)
            {
                Func<PdfDocumentState, bool> evaluator = <>c.<>9__56_1;
                if (<>c.<>9__56_1 == null)
                {
                    Func<PdfDocumentState, bool> local2 = <>c.<>9__56_1;
                    evaluator = <>c.<>9__56_1 = x => x.IsDocumentModified;
                }
                document.IsDocumentModified = document.DocumentState.Return<PdfDocumentState, bool>(evaluator, <>c.<>9__56_2 ??= () => false);
            }
            this.UpdatePagesProperties();
            this.RenderContent();
        }

        protected override void UpdatePages()
        {
            base.UpdatePages();
            this.ImmediateActionsManager.EnqueueAction(new DelegateAction(() => this.Update()));
        }

        protected internal virtual void UpdatePagesProperties()
        {
            if (this.HasPages && ((base.BehaviorProvider != null) && (base.Pages != null)))
            {
                foreach (PdfPageWrapper pageWrapper in base.Pages)
                {
                    pageWrapper.Pages.Cast<PdfPageViewModel>().ForEach<PdfPageViewModel>(x => x.RenderContent = this.CalcRenderContent(pageWrapper, x));
                }
            }
        }

        protected virtual void UpdatePageWrappersProperties()
        {
            double maxPageWidth = 0.0;
            double maxPageMargin = 0.0;
            double pageWrapperCenter = 0.0;
            if (base.ColumnsCount == 2)
            {
                pageWrapperCenter = this.CalcPageWrapperTwoPageCenter();
                this.CalcMaxPageWrapperParams(pageWrapperCenter, out maxPageWidth, out maxPageMargin);
            }
            if (base.Pages != null)
            {
                foreach (PdfPageWrapper wrapper in base.Pages)
                {
                    wrapper.PageWrapperWidth = maxPageWidth;
                    wrapper.PageWrapperMargin = maxPageMargin;
                    wrapper.PageWrapperTwoPageCenter = pageWrapperCenter;
                    wrapper.InitializeInternal();
                }
            }
            this.UpdateBehaviorProviderProperties();
        }

        protected internal void UpdatePanel()
        {
            if (this.ItemsPanel != null)
            {
                foreach (UIElement element in this.ItemsPanel.InternalChildren)
                {
                    Action<UIElement> action = <>c.<>9__84_0;
                    if (<>c.<>9__84_0 == null)
                    {
                        Action<UIElement> local1 = <>c.<>9__84_0;
                        action = <>c.<>9__84_0 = x => x.InvalidateVisual();
                    }
                    element.Do<UIElement>(action);
                }
                this.ItemsPanel.InvalidatePanel();
            }
        }

        private void UpdateRenderer()
        {
            if (base.IsInitialized && !this.IsInDesignTool())
            {
                this.UpdateNativeRenderer();
                this.RenderContent();
            }
        }

        public TextSearchParameter SearchParameter
        {
            get => 
                (TextSearchParameter) base.GetValue(SearchParameterProperty);
            private set => 
                base.SetValue(SearchParameterPropertyKey, value);
        }

        public bool AllowCurrentPageHighlighting
        {
            get => 
                (bool) base.GetValue(AllowCurrentPageHighlightingProperty);
            set => 
                base.SetValue(AllowCurrentPageHighlightingProperty, value);
        }

        public CursorModeType CursorMode
        {
            get
            {
                Func<PdfViewerControl, CursorModeType> evaluator = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<PdfViewerControl, CursorModeType> local1 = <>c.<>9__14_0;
                    evaluator = <>c.<>9__14_0 = x => x.CursorMode;
                }
                return this.ActualPdfViewer.Return<PdfViewerControl, CursorModeType>(evaluator, (<>c.<>9__14_1 ??= () => CursorModeType.SelectTool));
            }
        }

        public DevExpress.Xpf.PdfViewer.SelectionRectangle SelectionRectangle
        {
            get => 
                (DevExpress.Xpf.PdfViewer.SelectionRectangle) base.GetValue(SelectionRectangleProperty);
            private set => 
                base.SetValue(SelectionRectanglePropertyKey, value);
        }

        public Color HighlightSelectionColor
        {
            get
            {
                if (this.ActualPdfViewer == null)
                {
                    return Color.FromArgb(0x59, 0x5f, 0x98, 0xc0);
                }
                PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
                resourceKey.ResourceKey = PdfViewerThemeKeys.HighlightSelectionColor;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                SolidColorBrush brush = (SolidColorBrush) base.FindResource(resourceKey);
                Color color = BlendHelper2.CalcPalette(brush.Color);
                return ((this.ActualPdfViewer.ReadLocalValue(PdfViewerControl.HighlightSelectionColorProperty) != DependencyProperty.UnsetValue) ? this.ActualPdfViewer.HighlightSelectionColor : Color.FromArgb((byte) (brush.Opacity * 255.0), color.R, color.G, color.B));
            }
        }

        public bool EnableCaretAnimation
        {
            get => 
                (bool) base.GetValue(EnableCaretAnimationProperty);
            set => 
                base.SetValue(EnableCaretAnimationProperty, value);
        }

        public Color CaretColor
        {
            get
            {
                Func<PdfViewerControl, Color> evaluator = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Func<PdfViewerControl, Color> local1 = <>c.<>9__24_0;
                    evaluator = <>c.<>9__24_0 = x => x.CaretColor;
                }
                return this.ActualPdfViewer.Return<PdfViewerControl, Color>(evaluator, (<>c.<>9__24_1 ??= () => Colors.Black));
            }
        }

        public DevExpress.Xpf.PdfViewer.PdfBehaviorProvider PdfBehaviorProvider =>
            base.BehaviorProvider as DevExpress.Xpf.PdfViewer.PdfBehaviorProvider;

        protected PdfKeyboardAndMouseController KeyboardAndMouseController =>
            base.KeyboardAndMouseController as PdfKeyboardAndMouseController;

        internal PdfNavigationStrategy NavigationStrategy =>
            base.NavigationStrategy as PdfNavigationStrategy;

        protected internal InplaceEditingStrategy EditingStrategy { get; private set; }

        internal IPdfDocument Document =>
            base.Document as IPdfDocument;

        internal bool HasPages =>
            base.HasPages;

        public DocumentViewerPanel ItemsPanel =>
            base.ItemsPanel;

        public System.Windows.Controls.ScrollViewer ScrollViewer =>
            base.ScrollViewer;

        public CellEditorOwner ActiveEditorOwner { get; private set; }

        public bool IsInEditing =>
            this.ActiveEditorOwner != null;

        internal DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager =>
            base.ImmediateActionsManager;

        private PdfPageControl ActiveTooltipPage { get; set; }

        internal PdfViewerControl ActualPdfViewer =>
            base.ActualDocumentViewer as PdfViewerControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPresenterControl.<>c <>9 = new PdfPresenterControl.<>c();
            public static Func<PdfViewerControl, CursorModeType> <>9__14_0;
            public static Func<CursorModeType> <>9__14_1;
            public static Func<PdfViewerControl, Color> <>9__24_0;
            public static Func<Color> <>9__24_1;
            public static Func<IPdfDocument, IPdfDocumentSelectionResults> <>9__56_0;
            public static Func<PdfDocumentState, bool> <>9__56_1;
            public static Func<bool> <>9__56_2;
            public static Func<IDocument, PdfDocumentViewModel> <>9__64_0;
            public static Func<IDocument, PdfDocumentViewModel> <>9__64_2;
            public static Action<DocumentViewerRenderer> <>9__64_4;
            public static Func<PdfViewerControl, PdfViewerBackend> <>9__72_0;
            public static Func<HitTestResult, DependencyObject> <>9__75_0;
            public static Func<IPdfDocument, PdfCaret> <>9__80_0;
            public static Action<UIElement> <>9__84_0;
            public static Func<PdfViewerControl, PdfViewerBackend> <>9__97_0;
            public static Action<PdfViewerBackend> <>9__97_1;
            public static Action<CellEditor> <>9__97_2;
            public static Func<BehaviorProvider, double> <>9__105_0;
            public static Func<double> <>9__105_1;
            public static Func<BehaviorProvider, double> <>9__106_0;
            public static Func<double> <>9__106_1;
            public static Func<BehaviorProvider, int> <>9__107_1;
            public static Func<int> <>9__107_2;
            public static Action<DocumentViewerRenderer> <>9__116_0;
            public static Func<RenderItem, bool> <>9__121_1;

            internal void <.cctor>b__6_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((PdfPresenterControl) obj).OnAllowCurrentPageHighlightingChanged((bool) args.NewValue);
            }

            internal double <CalcMaxPageWrapperParams>b__105_0(BehaviorProvider x) => 
                x.RotateAngle;

            internal double <CalcMaxPageWrapperParams>b__105_1() => 
                0.0;

            internal double <CalcPageWrapperTwoPageCenter>b__106_0(BehaviorProvider x) => 
                x.RotateAngle;

            internal double <CalcPageWrapperTwoPageCenter>b__106_1() => 
                0.0;

            internal PdfCaret <CalcRenderContent>b__80_0(IPdfDocument x) => 
                x.Caret;

            internal PdfViewerBackend <CreateNativeRenderer>b__72_0(PdfViewerControl x) => 
                x.ViewerBackend;

            internal Color <get_CaretColor>b__24_0(PdfViewerControl x) => 
                x.CaretColor;

            internal Color <get_CaretColor>b__24_1() => 
                Colors.Black;

            internal CursorModeType <get_CursorMode>b__14_0(PdfViewerControl x) => 
                x.CursorMode;

            internal CursorModeType <get_CursorMode>b__14_1() => 
                CursorModeType.SelectTool;

            internal int <Initialize>b__107_1(BehaviorProvider y) => 
                y.PageIndex;

            internal int <Initialize>b__107_2() => 
                1;

            internal void <InvalidateRenderCaches>b__116_0(DocumentViewerRenderer x)
            {
                x.Reset();
            }

            internal bool <InvalidateRenderingOnIdle>b__121_1(RenderItem x) => 
                x.NeedsInvalidate;

            internal PdfViewerBackend <OnBehaviorProviderZoomChanged>b__97_0(PdfViewerControl x) => 
                x.ViewerBackend;

            internal void <OnBehaviorProviderZoomChanged>b__97_1(PdfViewerBackend x)
            {
                x.ClearPageCache();
            }

            internal void <OnBehaviorProviderZoomChanged>b__97_2(CellEditor x)
            {
                x.InvalidateEditor();
            }

            internal DependencyObject <OnDecoratorMouseLeftButtonUp>b__75_0(HitTestResult x) => 
                x.VisualHit;

            internal PdfDocumentViewModel <OnDocumentChanged>b__64_0(IDocument x) => 
                x as PdfDocumentViewModel;

            internal PdfDocumentViewModel <OnDocumentChanged>b__64_2(IDocument x) => 
                x as PdfDocumentViewModel;

            internal void <OnDocumentChanged>b__64_4(DocumentViewerRenderer x)
            {
                x.Reset();
            }

            internal IPdfDocumentSelectionResults <UpdateInternal>b__56_0(IPdfDocument x) => 
                x.SelectionResults;

            internal bool <UpdateInternal>b__56_1(PdfDocumentState x) => 
                x.IsDocumentModified;

            internal bool <UpdateInternal>b__56_2() => 
                false;

            internal void <UpdatePanel>b__84_0(UIElement x)
            {
                x.InvalidateVisual();
            }
        }
    }
}

