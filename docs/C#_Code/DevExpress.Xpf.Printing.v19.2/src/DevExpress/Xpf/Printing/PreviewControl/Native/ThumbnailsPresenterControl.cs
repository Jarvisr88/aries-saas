namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Rendering;
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

    public class ThumbnailsPresenterControl : DocumentPresenterControl, ISupportInvalidateRenderingOnIdle
    {
        public ThumbnailsPresenterControl()
        {
            this.SetDefaultStyleKey(typeof(ThumbnailsPresenterControl));
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
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

        protected override KeyboardAndMouseController CreateKeyboardAndMouseController() => 
            new ThumbnailsInputController(this);

        protected override DocumentViewerRenderer CreateNativeRenderer() => 
            new ThumbnailsRenderer(this);

        void ISupportInvalidateRenderingOnIdle.InvalidateRenderingOnIdle()
        {
            base.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, delegate {
                if (this.IsInVisualTree())
                {
                    Func<RenderItem, bool> predicate = <>c.<>9__8_1;
                    if (<>c.<>9__8_1 == null)
                    {
                        Func<RenderItem, bool> local1 = <>c.<>9__8_1;
                        predicate = <>c.<>9__8_1 = x => x.NeedsInvalidate;
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

        protected override ObservableCollection<PageWrapper> GeneratePageWrappers(IEnumerable<IPage> pages)
        {
            IEnumerable<IPage> filteredPages = this.GetFilteredPages();
            return base.GeneratePageWrappers(filteredPages);
        }

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
                            IDocumentPage page2 = (IDocumentPage) page;
                            RenderItem item = new RenderItem();
                            item.Rectangle = pageRect;
                            item.Page = page2;
                            item.NeedsInvalidate = page2.NeedsInvalidate;
                            item.ForceInvalidate = page2.ForceInvalidate;
                            item.TextureType = TextureType.Thumbnail;
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        private IEnumerable<IPage> GetFilteredPages() => 
            ((this.ThumbnailsViewer.FilteredPageIndices == null) || !this.ThumbnailsViewer.FilteredPageIndices.Any<int>()) ? base.Document.Pages : (from x in base.Document.Pages
                where this.ThumbnailsViewer.FilteredPageIndices.Contains(x.PageIndex)
                select x).ToList<IPage>();

        private bool IsVisibleChild(Rect rect) => 
            rect.IntersectsWith(new Rect(0.0, 0.0, base.ItemsControl.ActualWidth, base.ItemsControl.ActualHeight));

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.ImmediateActionsManager.EnqueueAction(delegate {
                this.UpdatePages();
                Action<DocumentViewerPanel> action = <>c.<>9__16_1;
                if (<>c.<>9__16_1 == null)
                {
                    Action<DocumentViewerPanel> local1 = <>c.<>9__16_1;
                    action = <>c.<>9__16_1 = x => x.UpdateLayout();
                }
                base.ItemsPanel.Do<DocumentViewerPanel>(action);
            });
            base.ImmediateActionsManager.EnqueueAction(() => this.RenderContent());
        }

        protected override void OnDocumentChanged(IDocument oldValue, IDocument newValue)
        {
            base.OnDocumentChanged(oldValue, newValue);
            (oldValue as ThumbnailsDocumentViewModel).Do<ThumbnailsDocumentViewModel>(delegate (ThumbnailsDocumentViewModel x) {
                x.Pages.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            });
            (newValue as ThumbnailsDocumentViewModel).Do<ThumbnailsDocumentViewModel>(delegate (ThumbnailsDocumentViewModel x) {
                x.Pages.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            });
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, () => this.RenderContent());
        }

        protected override void OnScrollViewerViewportChanged(object sender, ScrollChangedEventArgs e)
        {
            base.OnScrollViewerViewportChanged(sender, e);
            base.ImmediateActionsManager.EnqueueAction(new Action(this.RenderContent));
        }

        protected override void UpdateBehaviorProviderProperties()
        {
            base.UpdateBehaviorProviderProperties();
            if ((base.BehaviorProvider != null) && !base.BehaviorProvider.Viewport.IsEmpty)
            {
                base.Pages.ForEach<PageWrapper>(x => x.Viewport = base.BehaviorProvider.Viewport);
            }
        }

        internal void UpdateContent()
        {
            this.RenderContent();
        }

        internal void UpdatePagesInternal()
        {
            this.UpdatePages();
            this.RenderContent();
            base.ImmediateActionsManager.EnqueueAction(new Action(this.RenderContent));
        }

        protected internal ThumbnailsViewerControl ThumbnailsViewer =>
            base.ActualDocumentViewer as ThumbnailsViewerControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsPresenterControl.<>c <>9 = new ThumbnailsPresenterControl.<>c();
            public static Func<RenderItem, bool> <>9__8_1;
            public static Action<DocumentViewerPanel> <>9__16_1;

            internal bool <DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.ISupportInvalidateRenderingOnIdle.InvalidateRenderingOnIdle>b__8_1(RenderItem x) => 
                x.NeedsInvalidate;

            internal void <OnCollectionChanged>b__16_1(DocumentViewerPanel x)
            {
                x.UpdateLayout();
            }
        }

        protected class ThumbnailsInputController : KeyboardAndMouseController
        {
            public ThumbnailsInputController(ThumbnailsPresenterControl documentPresenter) : base(documentPresenter)
            {
            }

            private int GetPageWrapperIndex()
            {
                int pageIndex = this.Viewer.SelectedPageNumber - 1;
                return ((pageIndex != -1) ? base.presenter.NavigationStrategy.PositionCalculator.GetPageWrapperIndex(pageIndex) : -1);
            }

            private void NavigateToNextPage()
            {
                if (this.Viewer.Document.Pages.Count<ThumbnailPageViewModel>() > this.Viewer.SelectedPageNumber)
                {
                    ThumbnailsViewerControl viewer = this.Viewer;
                    viewer.SelectedPageNumber++;
                }
            }

            private void NavigateToNextPageWrapper()
            {
                int pageWrapperIndex = this.GetPageWrapperIndex();
                if (pageWrapperIndex != -1)
                {
                    if (base.presenter.Pages.Count <= (pageWrapperIndex + 1))
                    {
                        this.NavigateToNextPage();
                    }
                    else
                    {
                        int num2 = base.presenter.Pages[pageWrapperIndex].Pages.Count<IPage>();
                        ThumbnailsViewerControl viewer = this.Viewer;
                        viewer.SelectedPageNumber += num2;
                    }
                }
            }

            private void NavigateToPreviousPage()
            {
                if (this.Viewer.SelectedPageNumber > 1)
                {
                    ThumbnailsViewerControl viewer = this.Viewer;
                    viewer.SelectedPageNumber--;
                }
            }

            private void NavigateToPreviousPageWrapper()
            {
                int pageWrapperIndex = this.GetPageWrapperIndex();
                if (pageWrapperIndex != -1)
                {
                    if (pageWrapperIndex <= 0)
                    {
                        this.NavigateToPreviousPage();
                    }
                    else
                    {
                        int num2 = base.presenter.Pages[pageWrapperIndex].Pages.Count<IPage>();
                        ThumbnailsViewerControl viewer = this.Viewer;
                        viewer.SelectedPageNumber -= num2;
                    }
                }
            }

            public override void ProcessKeyDown(KeyEventArgs e)
            {
                if (this.Viewer != null)
                {
                    switch (e.Key)
                    {
                        case Key.Prior:
                            base.presenter.NavigationStrategy.ProcessScroll(ScrollCommand.PageUp);
                            e.Handled = true;
                            return;

                        case Key.Next:
                            base.presenter.NavigationStrategy.ProcessScroll(ScrollCommand.PageDown);
                            e.Handled = true;
                            return;

                        case Key.End:
                            base.presenter.NavigationStrategy.ProcessScroll(ScrollCommand.End);
                            e.Handled = true;
                            return;

                        case Key.Home:
                            base.presenter.NavigationStrategy.ProcessScroll(ScrollCommand.Home);
                            e.Handled = true;
                            return;

                        case Key.Left:
                            this.NavigateToPreviousPage();
                            e.Handled = true;
                            return;

                        case Key.Up:
                            this.NavigateToPreviousPageWrapper();
                            e.Handled = true;
                            return;

                        case Key.Right:
                            this.NavigateToNextPage();
                            e.Handled = true;
                            return;

                        case Key.Down:
                            this.NavigateToNextPageWrapper();
                            e.Handled = true;
                            return;
                    }
                }
            }

            public override void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
            {
                base.ProcessMouseLeftButtonUp(e);
                Point position = e.GetPosition(base.presenter);
                int index = base.presenter.NavigationStrategy.PositionCalculator.GetPageIndex(position.Y + base.presenter.ScrollViewer.VerticalOffset, position.X + base.presenter.ScrollViewer.HorizontalOffset, new Func<double, double>(base.ItemsPanel.CalcPageHorizontalOffset));
                if (index != -1)
                {
                    (base.presenter.ActualDocumentViewer as ThumbnailsViewerControl).Do<ThumbnailsViewerControl>(x => x.SelectedPageNumber = index + 1);
                }
            }

            public override void ProcessMouseWheel(MouseWheelEventArgs e)
            {
            }

            private ThumbnailsViewerControl Viewer =>
                base.presenter.ActualDocumentViewer as ThumbnailsViewerControl;
        }
    }
}

