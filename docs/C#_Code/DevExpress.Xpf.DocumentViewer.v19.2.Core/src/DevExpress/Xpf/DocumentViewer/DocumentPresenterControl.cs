namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer.Extensions;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class DocumentPresenterControl : Control
    {
        public static readonly DependencyProperty PagesProperty;
        public static readonly DependencyProperty IsSearchControlVisibleProperty;
        private DocumentViewerRenderer nativeRenderer;
        private IDocument document;
        private DevExpress.Xpf.DocumentViewer.BehaviorProvider behaviorProvider;
        private DevExpress.Xpf.DocumentViewer.PageDisplayMode pageDisplayMode;
        private int columnsCount = 1;
        private bool showCoverPage;
        private double horizontalPageSpacing;
        private double verticalPageSpacing;
        private bool showSingleItem;
        private readonly DevExpress.Xpf.Editors.ImmediateActionsManager immediateActionsManager;

        static DocumentPresenterControl()
        {
            Type ownerType = typeof(DocumentPresenterControl);
            PagesProperty = DependencyPropertyManager.Register("Pages", typeof(ObservableCollection<PageWrapper>), ownerType, new FrameworkPropertyMetadata(null));
            IsSearchControlVisibleProperty = DependencyPropertyManager.Register("IsSearchControlVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
        }

        public DocumentPresenterControl()
        {
            base.DefaultStyleKey = typeof(DocumentPresenterControl);
            this.NavigationStrategy = this.CreateNavigationStrategy();
            this.KeyboardAndMouseController = this.CreateKeyboardAndMouseController();
            this.TouchController = this.CreateTouchController();
            this.immediateActionsManager = new DevExpress.Xpf.Editors.ImmediateActionsManager(this);
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.ImmediateActionsManager.EnqueueAction(new Action(this.UpdateNativeRenderer));
        }

        protected virtual Size CalcPagesMarginSize()
        {
            Size size = new Size();
            foreach (PageWrapper wrapper in this.Pages)
            {
                Size size2 = wrapper.CalcMarginSize();
                if ((size2.Width * size2.Height).GreaterThanOrClose(size.Width * size.Height))
                {
                    size = size2;
                }
            }
            return size;
        }

        protected virtual void ColumnsCountChanged(int oldValue, int newValue)
        {
            this.Initialize();
        }

        protected virtual DevExpress.Xpf.DocumentViewer.KeyboardAndMouseController CreateKeyboardAndMouseController() => 
            new DevExpress.Xpf.DocumentViewer.KeyboardAndMouseController(this);

        protected virtual DocumentViewerRenderer CreateNativeRenderer() => 
            new DocumentViewerRenderer(this);

        protected virtual DevExpress.Xpf.DocumentViewer.NavigationStrategy CreateNavigationStrategy() => 
            new DevExpress.Xpf.DocumentViewer.NavigationStrategy(this);

        protected virtual PageWrapper CreatePageWrapper(IPage page) => 
            new PageWrapper(page);

        protected virtual PageWrapper CreatePageWrapper(IEnumerable<IPage> pages) => 
            new PageWrapper(pages);

        protected virtual DevExpress.Xpf.DocumentViewer.TouchController CreateTouchController() => 
            new DevExpress.Xpf.DocumentViewer.TouchController(this);

        protected virtual ObservableCollection<PageWrapper> GeneratePageWrappers(IEnumerable<IPage> pages)
        {
            List<PageWrapper> list = new List<PageWrapper>();
            switch (this.PageDisplayMode)
            {
                case DevExpress.Xpf.DocumentViewer.PageDisplayMode.Single:
                    list = pages.Select<IPage, PageWrapper>(new Func<IPage, PageWrapper>(this.CreatePageWrapper)).ToList<PageWrapper>();
                    break;

                case DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns:
                {
                    if (!this.ShowCoverPage)
                    {
                        list = pages.Partition<IPage>(this.ColumnsCount).Select<IPage[], PageWrapper>(new Func<IPage[], PageWrapper>(this.CreatePageWrapper)).ToList<PageWrapper>();
                    }
                    else
                    {
                        IPage[] pageArray1 = new IPage[] { pages.First<IPage>() };
                        list.Add(this.CreatePageWrapper(pageArray1));
                        list[0].IsCoverPage = true;
                        list.AddRange(pages.Skip<IPage>(1).Partition<IPage>(this.ColumnsCount).Select<IPage[], PageWrapper>(new Func<IPage[], PageWrapper>(this.CreatePageWrapper)));
                    }
                    Action<PageWrapper> action = <>c.<>9__140_0;
                    if (<>c.<>9__140_0 == null)
                    {
                        Action<PageWrapper> local1 = <>c.<>9__140_0;
                        action = <>c.<>9__140_0 = delegate (PageWrapper x) {
                            x.IsColumnMode = true;
                        };
                    }
                    list.ForEach(action);
                    break;
                }
                case DevExpress.Xpf.DocumentViewer.PageDisplayMode.Wrap:
                {
                    list = this.WrapPages(pages).Select<IPage[], PageWrapper>(new Func<IPage[], PageWrapper>(this.CreatePageWrapper)).ToList<PageWrapper>();
                    int maxPageCount = 0;
                    list.ForEach(delegate (PageWrapper x) {
                        maxPageCount = Math.Max(maxPageCount, x.Pages.Count<IPage>());
                    });
                    list.ForEach(delegate (PageWrapper x) {
                        x.MaxPageCount = maxPageCount;
                        x.PageDisplayMode = this.PageDisplayMode;
                        x.ZoomFactor = this.BehaviorProvider.ZoomFactor;
                        x.Viewport = this.ActualDocumentViewer.ActualBehaviorProvider.Viewport;
                    });
                    break;
                }
                default:
                    break;
            }
            list.ForEach(delegate (PageWrapper x) {
                x.HorizontalPageSpacing = this.HorizontalPageSpacing;
            });
            list.ForEach(delegate (PageWrapper x) {
                x.VerticalPageSpacing = this.VerticalPageSpacing;
            });
            return new ObservableCollection<PageWrapper>(list);
        }

        private Size GetMaxPageSize()
        {
            Func<PageWrapper, Size> sizeSelector = <>c.<>9__94_0;
            if (<>c.<>9__94_0 == null)
            {
                Func<PageWrapper, Size> local1 = <>c.<>9__94_0;
                sizeSelector = <>c.<>9__94_0 = x => x.PageSize;
            }
            return this.GetMaxPageSize(sizeSelector);
        }

        private Size GetMaxPageSize(Func<PageWrapper, Size> sizeSelector)
        {
            if (!this.HasPages)
            {
                return Size.Empty;
            }
            double num = 0.0;
            double num2 = 0.0;
            foreach (PageWrapper wrapper in this.Pages)
            {
                num = Math.Max(num, sizeSelector(wrapper).Width);
                Size size = sizeSelector(wrapper);
                num2 = Math.Max(num2, size.Height);
            }
            return new Size(num, num2);
        }

        private Size GetMaxPageVisibleSize()
        {
            Func<PageWrapper, Size> sizeSelector = <>c.<>9__95_0;
            if (<>c.<>9__95_0 == null)
            {
                Func<PageWrapper, Size> local1 = <>c.<>9__95_0;
                sizeSelector = <>c.<>9__95_0 = x => x.VisibleSize;
            }
            return this.GetMaxPageSize(sizeSelector);
        }

        protected virtual Size GetViewportSize()
        {
            if (this.ItemsPanel != null)
            {
                Func<DocumentViewerItemsControl, bool> evaluator = <>c.<>9__118_0;
                if (<>c.<>9__118_0 == null)
                {
                    Func<DocumentViewerItemsControl, bool> local1 = <>c.<>9__118_0;
                    evaluator = <>c.<>9__118_0 = x => x.IsInitialized;
                }
                if (this.ItemsControl.Return<DocumentViewerItemsControl, bool>(evaluator, (<>c.<>9__118_1 ??= () => false)) && this.HasPages)
                {
                    Size size = this.CalcPagesMarginSize();
                    return new Size(Math.Max((double) 0.0, (double) (this.ItemsPanel.ActualWidth - size.Width)), Math.Max((double) 0.0, (double) (this.ItemsPanel.ActualHeight - size.Height)));
                }
            }
            return Size.Empty;
        }

        protected virtual void HorizontalPageSpacingChanged(double oldValue, double newValue)
        {
            this.Initialize();
        }

        protected virtual void Initialize()
        {
            if ((this.ItemsControl != null) && (base.IsInitialized && ((this.BehaviorProvider != null) && (this.ScrollViewer != null))))
            {
                Func<IDocument, bool> evaluator = <>c.<>9__139_0;
                if (<>c.<>9__139_0 == null)
                {
                    Func<IDocument, bool> local1 = <>c.<>9__139_0;
                    evaluator = <>c.<>9__139_0 = x => x.IsLoaded;
                }
                this.Pages = this.Document.Return<IDocument, bool>(evaluator, (<>c.<>9__139_1 ??= () => false)) ? this.GeneratePageWrappers(this.Document.Pages) : null;
                this.ItemsControl.PageDisplayMode = this.PageDisplayMode;
                this.ItemsControl.ItemsSource = this.Pages;
                this.ItemsPanel.ShowSingleItem = this.ShowSingleItem;
                this.NavigationStrategy.GenerateStartUpState();
                this.UpdateBehaviorProviderProperties();
                this.NativeImage.Invalidate();
                if (this.Pages != null)
                {
                    this.ImmediateActionsManager.EnqueueAction(new DelegateAction(() => this.NavigationStrategy.ScrollToStartUp()));
                }
            }
        }

        protected bool IsInBounds(Point point)
        {
            if (!this.HasPages)
            {
                return false;
            }
            HitTestResult input = VisualTreeHelper.HitTest(this, point);
            Func<HitTestResult, DependencyObject> evaluator = <>c.<>9__136_0;
            if (<>c.<>9__136_0 == null)
            {
                Func<HitTestResult, DependencyObject> local1 = <>c.<>9__136_0;
                evaluator = <>c.<>9__136_0 = x => x.VisualHit;
            }
            return ((input.With<HitTestResult, DependencyObject>(evaluator) != null) ? (LayoutHelper.FindParentObject<ScrollBar>(input.VisualHit) == null) : false);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ItemsControl.Do<DocumentViewerItemsControl>(new Action<DocumentViewerItemsControl>(this.UnsubscribeFromEvents));
            this.ItemsControl = base.GetTemplateChild("PART_ItemsControl") as DocumentViewerItemsControl;
            this.ItemsControl.Do<DocumentViewerItemsControl>(new Action<DocumentViewerItemsControl>(this.SubscribeToEvents));
            Action<DevExpress.Xpf.Editors.NativeImage> action = <>c.<>9__105_0;
            if (<>c.<>9__105_0 == null)
            {
                Action<DevExpress.Xpf.Editors.NativeImage> local1 = <>c.<>9__105_0;
                action = <>c.<>9__105_0 = x => x.Renderer = null;
            }
            this.NativeImage.Do<DevExpress.Xpf.Editors.NativeImage>(action);
            this.NativeImage = base.GetTemplateChild("PART_NativeImage") as DevExpress.Xpf.Editors.NativeImage;
            this.NativeImage.Do<DevExpress.Xpf.Editors.NativeImage>(x => x.Renderer = this.Renderer);
        }

        protected virtual void OnBehaviorProviderChanged(DevExpress.Xpf.DocumentViewer.BehaviorProvider oldValue, DevExpress.Xpf.DocumentViewer.BehaviorProvider newValue)
        {
        }

        private void OnBehaviorProviderChangedInternal(DevExpress.Xpf.DocumentViewer.BehaviorProvider oldValue, DevExpress.Xpf.DocumentViewer.BehaviorProvider newValue)
        {
            oldValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.ZoomChanged -= new EventHandler<ZoomChangedEventArgs>(this.OnBehaviorProviderZoomChanged);
            });
            oldValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.PageIndexChanged -= new EventHandler<PageIndexChangedEventArgs>(this.OnBehaviorProviderPageIndexChanged);
            });
            oldValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.RotateAngleChanged -= new EventHandler<RotateAngleChangedEventArgs>(this.OnBehaviorProviderRotateAngleChanged);
            });
            this.UpdateBehaviorProviderProperties();
            newValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(this.OnBehaviorProviderZoomChanged);
            });
            newValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.PageIndexChanged += new EventHandler<PageIndexChangedEventArgs>(this.OnBehaviorProviderPageIndexChanged);
            });
            newValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.RotateAngleChanged += new EventHandler<RotateAngleChangedEventArgs>(this.OnBehaviorProviderRotateAngleChanged);
            });
            this.OnBehaviorProviderChanged(oldValue, newValue);
        }

        protected virtual void OnBehaviorProviderPageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.NavigationStrategy.ProcessPageIndexChanged(e.PageIndex);
        }

        protected virtual void OnBehaviorProviderRotateAngleChanged(object sender, RotateAngleChangedEventArgs e)
        {
            if (this.HasPages)
            {
                this.NavigationStrategy.ProcessRotateAngleChanged(e);
            }
        }

        protected virtual void OnBehaviorProviderZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            if (this.HasPages)
            {
                this.NavigationStrategy.ProcessZoomChanged(e);
            }
            if (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Wrap)
            {
                this.UpdatePages();
            }
        }

        protected virtual void OnDecoratorKeyDown(object sender, KeyEventArgs e)
        {
            if (this.HasPages)
            {
                this.KeyboardAndMouseController.Do<DevExpress.Xpf.DocumentViewer.KeyboardAndMouseController>(x => x.ProcessKeyDown(e));
            }
        }

        protected virtual void OnDecoratorManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.Manipulators.Count<IManipulator>() == 1)
            {
                Point position = e.Manipulators.First<IManipulator>().GetPosition(this);
                if (!this.IsInBounds(position))
                {
                    e.Cancel();
                    return;
                }
            }
            if (this.HasPages)
            {
                this.TouchController.ProcessManipulationCompleted(e);
            }
        }

        protected virtual void OnDecoratorManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.Manipulators.Count<IManipulator>() == 1)
            {
                Point position = e.Manipulators.First<IManipulator>().GetPosition(this);
                if (!this.IsInBounds(position))
                {
                    e.Cancel();
                    return;
                }
            }
            if (this.HasPages)
            {
                this.TouchController.ProcessManipulationDelta(e);
            }
        }

        protected virtual void OnDecoratorManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            if (e.Manipulators.Count<IManipulator>() == 1)
            {
                Point position = e.Manipulators.First<IManipulator>().GetPosition(this);
                if (!this.IsInBounds(position))
                {
                    e.Cancel();
                    return;
                }
            }
            if (this.HasPages)
            {
                this.TouchController.ProcessManipulationInertiaStarting(e);
            }
        }

        protected virtual void OnDecoratorManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            if (e.Manipulators.Count<IManipulator>() == 1)
            {
                Point position = e.Manipulators.First<IManipulator>().GetPosition(this);
                if (!this.IsInBounds(position))
                {
                    e.Cancel();
                    return;
                }
            }
            if (this.HasPages)
            {
                this.TouchController.ProcessManipulationStarted(e);
            }
        }

        protected virtual void OnDecoratorManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            if (e.Manipulators.Count<IManipulator>() == 1)
            {
                Point position = e.Manipulators.First<IManipulator>().GetPosition(this);
                if (!this.IsInBounds(position))
                {
                    e.Cancel();
                    return;
                }
            }
            if (this.HasPages)
            {
                this.TouchController.ProcessManipulationStarting(e);
            }
        }

        protected virtual void OnDecoratorMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsInBounds(e.GetPosition(this)))
            {
                this.KeyboardAndMouseController.ProcessMouseLeftButtonDown(e);
            }
        }

        protected virtual void OnDecoratorMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.ReleaseMouseCapture();
            if (this.IsInBounds(e.GetPosition(this)))
            {
                this.KeyboardAndMouseController.ProcessMouseLeftButtonUp(e);
            }
        }

        protected virtual void OnDecoratorMouseMove(object sender, MouseEventArgs e)
        {
            if (this.HasPages)
            {
                this.KeyboardAndMouseController.ProcessMouseMove(e);
            }
        }

        protected virtual void OnDecoratorStylusSystemGesture(object sender, StylusSystemGestureEventArgs e)
        {
            if (this.IsInBounds(e.GetPosition(this)))
            {
                this.TouchController.ProcessStylusSystemGesture(e);
            }
        }

        protected virtual void OnDocumentChanged(IDocument oldValue, IDocument newValue)
        {
        }

        private void OnDocumentChangedInternal(IDocument oldValue, IDocument newValue)
        {
            this.OnDocumentChanged(oldValue, newValue);
            this.Initialize();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            Action<DocumentPresenterDecorator> action = <>c.<>9__111_0;
            if (<>c.<>9__111_0 == null)
            {
                Action<DocumentPresenterDecorator> local1 = <>c.<>9__111_0;
                action = <>c.<>9__111_0 = x => x.Focus();
            }
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(action);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.ActualDocumentViewer.Do<IDocumentViewerControl>(x => x.AttachDocumentPresenterControl(this));
        }

        protected virtual void OnItemsControlLoaded(object sender, RoutedEventArgs e)
        {
            this.ItemsControl.PageDisplayMode = this.PageDisplayMode;
            this.ItemsPanel = this.ItemsControl.Panel;
            this.ScrollViewer = this.ItemsControl.ScrollViewer;
            this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(delegate (System.Windows.Controls.ScrollViewer x) {
                x.ScrollChanged += new ScrollChangedEventHandler(this.OnScrollViewerScrollChangedInternal);
            });
            this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(delegate (System.Windows.Controls.ScrollViewer x) {
                x.Loaded += new RoutedEventHandler(this.OnScrollViewerLoaded);
            });
            this.PresenterDecorator = this.ItemsControl.PresenterDecorator;
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewKeyDown += new KeyEventHandler(this.OnDecoratorKeyDown);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.OnDecoratorMouseLeftButtonDown);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.OnDecoratorMouseLeftButtonUp);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewMouseMove += new MouseEventHandler(this.OnDecoratorMouseMove);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(this.OnDecoratorManipulationStarting);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnDecoratorManipulationStarted);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnDecoratorManipulationDelta);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnDecoratorManipulationCompleted);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationInertiaStarting += new EventHandler<ManipulationInertiaStartingEventArgs>(this.OnDecoratorManipulationInertiaStarting);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewStylusSystemGesture += new StylusSystemGestureEventHandler(this.OnDecoratorStylusSystemGesture);
            });
            this.Initialize();
        }

        protected virtual void OnItemsControlUnloaded(object sender, RoutedEventArgs e)
        {
            this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(delegate (System.Windows.Controls.ScrollViewer x) {
                x.ScrollChanged -= new ScrollChangedEventHandler(this.OnScrollViewerScrollChangedInternal);
            });
            this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(delegate (System.Windows.Controls.ScrollViewer x) {
                x.Loaded -= new RoutedEventHandler(this.OnScrollViewerLoaded);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewKeyDown -= new KeyEventHandler(this.OnDecoratorKeyDown);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.OnDecoratorMouseLeftButtonDown);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(this.OnDecoratorMouseLeftButtonUp);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewMouseMove -= new MouseEventHandler(this.OnDecoratorMouseMove);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationStarting -= new EventHandler<ManipulationStartingEventArgs>(this.OnDecoratorManipulationStarting);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnDecoratorManipulationStarted);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.OnDecoratorManipulationDelta);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnDecoratorManipulationCompleted);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.ManipulationInertiaStarting -= new EventHandler<ManipulationInertiaStartingEventArgs>(this.OnDecoratorManipulationInertiaStarting);
            });
            this.PresenterDecorator.Do<DocumentPresenterDecorator>(delegate (DocumentPresenterDecorator x) {
                x.PreviewStylusSystemGesture -= new StylusSystemGestureEventHandler(this.OnDecoratorStylusSystemGesture);
            });
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.ImmediateActionsManager.ExecuteActions();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            this.ActualDocumentViewer.Do<IDocumentViewerControl>(x => x.AttachDocumentPresenterControl(this));
            this.NavigationStrategy.GenerateStartUpState();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (this.HasPages)
            {
                this.KeyboardAndMouseController.ProcessKeyDown(e);
            }
        }

        protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseRightButtonDown(e);
            if (this.HasPages)
            {
                this.KeyboardAndMouseController.ProcessMouseRightButtonDown(e);
            }
        }

        protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseRightButtonUp(e);
            if (this.HasPages)
            {
                this.KeyboardAndMouseController.ProcessMouseRightButtonUp(e);
            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
            if (this.HasPages)
            {
                this.KeyboardAndMouseController.ProcessMouseWheel(e);
            }
        }

        private void OnScrollViewerLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateViewportSize();
        }

        protected virtual void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (this.HasPages)
            {
                this.NavigationStrategy.ProcessScrollViewerScrollChanged(e);
                this.RenderContent();
            }
        }

        private void OnScrollViewerScrollChangedInternal(object sender, ScrollChangedEventArgs e)
        {
            if (e.Source == e.OriginalSource)
            {
                if (!e.ViewportHeightChange.IsZero() || !e.ViewportWidthChange.IsZero())
                {
                    this.OnScrollViewerViewportChanged(sender, e);
                }
                this.OnScrollViewerScrollChanged(sender, e);
            }
        }

        protected virtual void OnScrollViewerViewportChanged(object sender, ScrollChangedEventArgs e)
        {
            this.UpdateViewportSize();
        }

        protected virtual void OnSelectedIndexChanged(int oldValue, int newValue)
        {
        }

        protected virtual void OnShowSingleItemChanged(bool newValue)
        {
            this.ItemsPanel.Do<DocumentViewerPanel>(x => x.ShowSingleItem = newValue);
        }

        protected virtual void PageDisplayModeChanged(DevExpress.Xpf.DocumentViewer.PageDisplayMode oldValue, DevExpress.Xpf.DocumentViewer.PageDisplayMode newValue)
        {
            this.Initialize();
            this.ItemsControl.Do<DocumentViewerItemsControl>(x => x.PageDisplayMode = newValue);
        }

        protected virtual void RenderContent()
        {
            this.RenderNativeContent();
            this.RenderManagedContent();
        }

        protected virtual void RenderManagedContent()
        {
        }

        protected virtual void RenderNativeContent()
        {
            Action<DocumentViewerRenderer> action = <>c.<>9__154_0;
            if (<>c.<>9__154_0 == null)
            {
                Action<DocumentViewerRenderer> local1 = <>c.<>9__154_0;
                action = <>c.<>9__154_0 = x => x.Invalidate();
            }
            this.Renderer.Do<DocumentViewerRenderer>(action);
        }

        public void Scroll(ScrollCommand command)
        {
            this.NavigationStrategy.ProcessScroll(command);
        }

        public void ScrollToHorizontalOffset(double offset)
        {
            this.NavigationStrategy.ProcessScrollTo(offset, false);
        }

        public void ScrollToVerticalOffset(double offset)
        {
            this.NavigationStrategy.ProcessScrollTo(offset, true);
        }

        protected virtual void ShowCoverPageChanged(bool newValue)
        {
            this.Initialize();
        }

        protected virtual void SubscribeToEvents(DocumentViewerItemsControl itemsControl)
        {
            itemsControl.Loaded += new RoutedEventHandler(this.OnItemsControlLoaded);
            itemsControl.Unloaded += new RoutedEventHandler(this.OnItemsControlUnloaded);
        }

        protected virtual void UnsubscribeFromEvents(DocumentViewerItemsControl itemsControl)
        {
            itemsControl.Loaded -= new RoutedEventHandler(this.OnItemsControlLoaded);
            itemsControl.Unloaded -= new RoutedEventHandler(this.OnItemsControlUnloaded);
        }

        protected virtual void UpdateBehaviorProviderProperties()
        {
            this.BehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.PageSize = this.GetMaxPageSize());
            this.BehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.PageVisibleSize = this.GetMaxPageVisibleSize());
            this.BehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.Viewport = this.GetViewportSize());
        }

        protected virtual void UpdateNativeRenderer()
        {
            this.nativeRenderer = this.CreateNativeRenderer();
            this.NativeImage.Do<DevExpress.Xpf.Editors.NativeImage>(x => x.Renderer = this.nativeRenderer);
        }

        protected virtual void UpdatePages()
        {
            Func<IDocument, bool> evaluator = <>c.<>9__149_0;
            if (<>c.<>9__149_0 == null)
            {
                Func<IDocument, bool> local1 = <>c.<>9__149_0;
                evaluator = <>c.<>9__149_0 = x => x.IsLoaded;
            }
            this.Pages = ((IDocument) evaluator).Return<IDocument, bool>(evaluator, (<>c.<>9__149_1 ??= () => false)) ? this.GeneratePageWrappers(this.Document.Pages) : null;
            this.ItemsControl.ItemsSource = this.Pages;
            this.UpdateBehaviorProviderProperties();
        }

        private void UpdateViewportSize()
        {
            this.BehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.Viewport = this.GetViewportSize());
            if (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Wrap)
            {
                this.UpdatePages();
            }
        }

        protected virtual void VerticalPageSpacingChanged(double oldValue, double newValue)
        {
            this.Initialize();
        }

        protected virtual IEnumerable<IPage[]> WrapPages(IEnumerable<IPage> pages)
        {
            if ((this.BehaviorProvider.ZoomMode != ZoomMode.Custom) && (this.BehaviorProvider.ZoomMode != ZoomMode.ActualSize))
            {
                Func<IPage, IPage[]> selector = <>c.<>9__141_0;
                if (<>c.<>9__141_0 == null)
                {
                    Func<IPage, IPage[]> local1 = <>c.<>9__141_0;
                    selector = <>c.<>9__141_0 = x => new IPage[] { x };
                }
                return pages.Select<IPage, IPage[]>(selector);
            }
            Size viewportSize = this.GetViewportSize();
            List<IPage[]> list = new List<IPage[]>();
            double num = 0.0;
            List<IPage> list2 = new List<IPage>();
            foreach (IPage page in pages)
            {
                Size pageSize = page.PageSize;
                Thickness margin = page.Margin;
                double num2 = ((pageSize.Width * this.BehaviorProvider.ZoomFactor) + page.Margin.Left) + margin.Right;
                if ((num + num2).GreaterThan(viewportSize.Width) && (list2.Count > 0))
                {
                    list.Add(list2.ToArray());
                    list2.Clear();
                    num = num2;
                }
                list2.Add(page);
            }
            if (list2.Count > 0)
            {
                list.Add(list2.ToArray());
            }
            return list;
        }

        protected DocumentViewerRenderer Renderer =>
            this.nativeRenderer;

        public IDocument Document
        {
            get => 
                this.document;
            internal set
            {
                Func<bool> fallback = <>c.<>9__18_1;
                if (<>c.<>9__18_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__18_1;
                    fallback = <>c.<>9__18_1 = () => false;
                }
                if (!this.document.Return<IDocument, bool>(x => x.Equals(value), fallback))
                {
                    IDocument oldValue = this.document;
                    this.document = value;
                    this.OnDocumentChangedInternal(oldValue, value);
                }
            }
        }

        public DevExpress.Xpf.DocumentViewer.BehaviorProvider BehaviorProvider
        {
            get => 
                this.behaviorProvider;
            internal set
            {
                Func<bool> fallback = <>c.<>9__21_1;
                if (<>c.<>9__21_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__21_1;
                    fallback = <>c.<>9__21_1 = () => false;
                }
                if (!this.behaviorProvider.Return<DevExpress.Xpf.DocumentViewer.BehaviorProvider, bool>(x => x.Equals(value), fallback))
                {
                    DevExpress.Xpf.DocumentViewer.BehaviorProvider behaviorProvider = this.behaviorProvider;
                    this.behaviorProvider = value;
                    this.OnBehaviorProviderChangedInternal(behaviorProvider, value);
                }
            }
        }

        public DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode
        {
            get => 
                this.pageDisplayMode;
            set
            {
                if (this.pageDisplayMode != value)
                {
                    DevExpress.Xpf.DocumentViewer.PageDisplayMode pageDisplayMode = this.pageDisplayMode;
                    this.pageDisplayMode = value;
                    this.PageDisplayModeChanged(pageDisplayMode, value);
                }
            }
        }

        public int ColumnsCount
        {
            get => 
                this.columnsCount;
            set
            {
                if (this.columnsCount != value)
                {
                    int columnsCount = this.columnsCount;
                    this.columnsCount = value;
                    this.ColumnsCountChanged(columnsCount, value);
                }
            }
        }

        public bool ShowCoverPage
        {
            get => 
                this.showCoverPage;
            set
            {
                if (this.showCoverPage != value)
                {
                    this.showCoverPage = value;
                    this.ShowCoverPageChanged(value);
                }
            }
        }

        public double HorizontalPageSpacing
        {
            get => 
                this.horizontalPageSpacing;
            set
            {
                if (!this.horizontalPageSpacing.AreClose(value))
                {
                    double horizontalPageSpacing = this.horizontalPageSpacing;
                    this.horizontalPageSpacing = value;
                    this.HorizontalPageSpacingChanged(horizontalPageSpacing, value);
                }
            }
        }

        public double VerticalPageSpacing
        {
            get => 
                this.verticalPageSpacing;
            set
            {
                if (!this.verticalPageSpacing.AreClose(value))
                {
                    double verticalPageSpacing = this.verticalPageSpacing;
                    this.verticalPageSpacing = value;
                    this.VerticalPageSpacingChanged(verticalPageSpacing, value);
                }
            }
        }

        public bool ShowSingleItem
        {
            get => 
                this.showSingleItem;
            set
            {
                if (this.showSingleItem != value)
                {
                    this.showSingleItem = value;
                    this.OnShowSingleItemChanged(value);
                }
            }
        }

        public ObservableCollection<PageWrapper> Pages
        {
            get => 
                (ObservableCollection<PageWrapper>) base.GetValue(PagesProperty);
            set => 
                base.SetValue(PagesProperty, value);
        }

        public bool IsSearchControlVisible
        {
            get => 
                (bool) base.GetValue(IsSearchControlVisibleProperty);
            set => 
                base.SetValue(IsSearchControlVisibleProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.UndoRedoManager UndoRedoManager
        {
            get
            {
                Func<IDocumentViewerControl, DevExpress.Xpf.DocumentViewer.UndoRedoManager> evaluator = <>c.<>9__47_0;
                if (<>c.<>9__47_0 == null)
                {
                    Func<IDocumentViewerControl, DevExpress.Xpf.DocumentViewer.UndoRedoManager> local1 = <>c.<>9__47_0;
                    evaluator = <>c.<>9__47_0 = x => x.UndoRedoManager;
                }
                return this.ActualDocumentViewer.With<IDocumentViewerControl, DevExpress.Xpf.DocumentViewer.UndoRedoManager>(evaluator);
            }
        }

        protected internal DocumentViewerItemsControl ItemsControl { get; private set; }

        protected internal DevExpress.Xpf.Editors.NativeImage NativeImage { get; private set; }

        private INativeImageRendererCallback NativeImageRendererCallback { get; set; }

        protected internal DocumentViewerPanel ItemsPanel { get; private set; }

        protected internal System.Windows.Controls.ScrollViewer ScrollViewer { get; private set; }

        protected internal IDocumentViewerControl ActualDocumentViewer =>
            DocumentViewerControl.GetActualViewer(this);

        protected internal DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager =>
            this.immediateActionsManager;

        protected internal bool HasPages
        {
            get
            {
                if (this.Document == null)
                {
                    return false;
                }
                Func<ObservableCollection<PageWrapper>, bool> evaluator = <>c.<>9__73_0;
                if (<>c.<>9__73_0 == null)
                {
                    Func<ObservableCollection<PageWrapper>, bool> local1 = <>c.<>9__73_0;
                    evaluator = <>c.<>9__73_0 = x => x.Any<PageWrapper>();
                }
                return this.Pages.Return<ObservableCollection<PageWrapper>, bool>(evaluator, (<>c.<>9__73_1 ??= () => false));
            }
        }

        protected internal DevExpress.Xpf.DocumentViewer.NavigationStrategy NavigationStrategy { get; private set; }

        protected internal DevExpress.Xpf.DocumentViewer.KeyboardAndMouseController KeyboardAndMouseController { get; private set; }

        protected DevExpress.Xpf.DocumentViewer.TouchController TouchController { get; private set; }

        internal DocumentPresenterDecorator PresenterDecorator { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPresenterControl.<>c <>9 = new DocumentPresenterControl.<>c();
            public static Func<bool> <>9__18_1;
            public static Func<bool> <>9__21_1;
            public static Func<IDocumentViewerControl, UndoRedoManager> <>9__47_0;
            public static Func<ObservableCollection<PageWrapper>, bool> <>9__73_0;
            public static Func<bool> <>9__73_1;
            public static Func<PageWrapper, Size> <>9__94_0;
            public static Func<PageWrapper, Size> <>9__95_0;
            public static Action<NativeImage> <>9__105_0;
            public static Action<DocumentPresenterDecorator> <>9__111_0;
            public static Func<DocumentViewerItemsControl, bool> <>9__118_0;
            public static Func<bool> <>9__118_1;
            public static Func<HitTestResult, DependencyObject> <>9__136_0;
            public static Func<IDocument, bool> <>9__139_0;
            public static Func<bool> <>9__139_1;
            public static Action<PageWrapper> <>9__140_0;
            public static Func<IPage, IPage[]> <>9__141_0;
            public static Func<IDocument, bool> <>9__149_0;
            public static Func<bool> <>9__149_1;
            public static Action<DocumentViewerRenderer> <>9__154_0;

            internal void <GeneratePageWrappers>b__140_0(PageWrapper x)
            {
                x.IsColumnMode = true;
            }

            internal bool <get_HasPages>b__73_0(ObservableCollection<PageWrapper> x) => 
                x.Any<PageWrapper>();

            internal bool <get_HasPages>b__73_1() => 
                false;

            internal UndoRedoManager <get_UndoRedoManager>b__47_0(IDocumentViewerControl x) => 
                x.UndoRedoManager;

            internal Size <GetMaxPageSize>b__94_0(PageWrapper x) => 
                x.PageSize;

            internal Size <GetMaxPageVisibleSize>b__95_0(PageWrapper x) => 
                x.VisibleSize;

            internal bool <GetViewportSize>b__118_0(DocumentViewerItemsControl x) => 
                x.IsInitialized;

            internal bool <GetViewportSize>b__118_1() => 
                false;

            internal bool <Initialize>b__139_0(IDocument x) => 
                x.IsLoaded;

            internal bool <Initialize>b__139_1() => 
                false;

            internal DependencyObject <IsInBounds>b__136_0(HitTestResult x) => 
                x.VisualHit;

            internal void <OnApplyTemplate>b__105_0(NativeImage x)
            {
                x.Renderer = null;
            }

            internal void <OnGotFocus>b__111_0(DocumentPresenterDecorator x)
            {
                x.Focus();
            }

            internal void <RenderNativeContent>b__154_0(DocumentViewerRenderer x)
            {
                x.Invalidate();
            }

            internal bool <set_BehaviorProvider>b__21_1() => 
                false;

            internal bool <set_Document>b__18_1() => 
                false;

            internal bool <UpdatePages>b__149_0(IDocument x) => 
                x.IsLoaded;

            internal bool <UpdatePages>b__149_1() => 
                false;

            internal IPage[] <WrapPages>b__141_0(IPage x) => 
                new IPage[] { x };
        }
    }
}

