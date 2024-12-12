namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class ThumbnailsViewerPresenterControl : DocumentPresenterControl
    {
        public ThumbnailsViewerPresenterControl()
        {
            base.DefaultStyleKey = typeof(ThumbnailsViewerPresenterControl);
        }

        private Rect CalcPageRect(RenderItem pair, Rect rect)
        {
            Control control = (Control) base.ItemsControl.ItemContainerGenerator.ContainerFromItem(pair.PageWrapper);
            double num = (control.Padding.Left + control.Padding.Right) / 2.0;
            double num2 = (control.Padding.Top + control.Padding.Bottom) / 2.0;
            rect.Inflate(-num, -num2);
            return rect;
        }

        protected override KeyboardAndMouseController CreateKeyboardAndMouseController() => 
            new ThumbnailsKeyboardAndMouseController(this);

        protected override DocumentViewerRenderer CreateNativeRenderer()
        {
            Func<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings> evaluator = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings> local1 = <>c.<>9__10_0;
                evaluator = <>c.<>9__10_0 = x => x.ActualSettings;
            }
            Func<PdfThumbnailsViewerSettings, PdfViewerBackend> func2 = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Func<PdfThumbnailsViewerSettings, PdfViewerBackend> local2 = <>c.<>9__10_1;
                func2 = <>c.<>9__10_1 = x => x.ViewerBackend;
            }
            return new PdfViewerThumbnailsDocumentRenderer(this, new TextureCache(), this.ThumbnailsViewer.With<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings>(evaluator).Return<PdfThumbnailsViewerSettings, PdfViewerBackend>(func2, <>c.<>9__10_2 ??= ((Func<PdfViewerBackend>) (() => null))));
        }

        protected override PageWrapper CreatePageWrapper(IEnumerable<IPage> pages)
        {
            ThumbnailPageWrapper wrapper1 = new ThumbnailPageWrapper(pages);
            wrapper1.RotateAngle = base.BehaviorProvider.RotateAngle;
            return wrapper1;
        }

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
                            ThumbnailPageViewModel model = (ThumbnailPageViewModel) page;
                            RenderItem item = new RenderItem();
                            item.Rect = pageRect;
                            item.PageWrapper = wrapper;
                            item.Page = model;
                            item.NeedsInvalidate = model.NeedsInvalidate;
                            item.ForceInvalidate = model.ForceInvalidate;
                            item.TextureType = TextureType.Thumbnail;
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        internal void InvalidateRender()
        {
            base.ImmediateActionsManager.EnqueueAction(new Action(this.RenderContent));
        }

        public void InvalidateRenderingOnIdle()
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

        private bool IsVisibleChild(Rect rect) => 
            rect.IntersectsWith(new Rect(0.0, 0.0, base.ItemsControl.ActualWidth, base.ItemsControl.ActualHeight));

        protected override void OnBehaviorProviderPageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            int pageWrapperIndex = base.NavigationStrategy.PositionCalculator.GetPageWrapperIndex(e.PageIndex);
            base.NavigationStrategy.ScrollIntoView(pageWrapperIndex, Rect.Empty, ScrollIntoViewMode.Edge);
        }

        protected override void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            this.InvalidateRender();
        }

        internal void UpdateNativeRendererInternal()
        {
            this.UpdateNativeRenderer();
        }

        protected override void UpdatePages()
        {
            base.UpdatePages();
            base.ImmediateActionsManager.EnqueueAction(new DelegateAction(new Action(this.InvalidateRender)));
        }

        internal void UpdatePagesInternal()
        {
            this.UpdatePages();
        }

        protected internal PdfThumbnailsViewerControl ThumbnailsViewer =>
            base.ActualDocumentViewer as PdfThumbnailsViewerControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsViewerPresenterControl.<>c <>9 = new ThumbnailsViewerPresenterControl.<>c();
            public static Func<RenderItem, bool> <>9__8_1;
            public static Func<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings> <>9__10_0;
            public static Func<PdfThumbnailsViewerSettings, PdfViewerBackend> <>9__10_1;
            public static Func<PdfViewerBackend> <>9__10_2;

            internal PdfThumbnailsViewerSettings <CreateNativeRenderer>b__10_0(PdfThumbnailsViewerControl x) => 
                x.ActualSettings;

            internal PdfViewerBackend <CreateNativeRenderer>b__10_1(PdfThumbnailsViewerSettings x) => 
                x.ViewerBackend;

            internal PdfViewerBackend <CreateNativeRenderer>b__10_2() => 
                null;

            internal bool <InvalidateRenderingOnIdle>b__8_1(RenderItem x) => 
                x.NeedsInvalidate;
        }
    }
}

