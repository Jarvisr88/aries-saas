namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PdfNavigationStrategy : NavigationStrategy
    {
        public PdfNavigationStrategy(PdfPresenterControl presenter) : base(presenter)
        {
        }

        private PdfPoint CalcAnchorPoint(double? x, double? y, int pageIndex)
        {
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(pageIndex);
            PdfPoint point = ((PdfPageWrapper) base.Presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex)).CalcTopLeftAngle(base.Presenter.BehaviorProvider.RotateAngle, pageIndex);
            double? nullable = x;
            nullable = y;
            return new PdfPoint((nullable != null) ? nullable.GetValueOrDefault() : point.X, (nullable != null) ? nullable.GetValueOrDefault() : point.Y);
        }

        protected internal PdfDocumentPosition CalcDocumentPosition(Point point)
        {
            int pageIndex = base.Presenter.ShowSingleItem ? base.ItemsPanel.IndexCalculator.VerticalOffsetToIndex(base.ItemsPanel.GetVirtualVerticalOffset()) : base.PositionCalculator.GetPageIndex(point.Y + base.ScrollViewer.VerticalOffset, point.X, new Func<double, double>(base.ItemsPanel.CalcPageHorizontalOffset));
            if (pageIndex == -1)
            {
                return new PdfDocumentPosition(1, new PdfPoint());
            }
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(pageIndex);
            PageWrapper wrapper = this.DocumentPresenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            PdfPageViewModel page = wrapper.Pages.Single<IPage>(x => (x.PageIndex == pageIndex)) as PdfPageViewModel;
            Rect pageRect = wrapper.GetPageRect(page);
            double num2 = base.PositionCalculator.GetPageVerticalOffset(pageIndex, 0.0) + pageRect.Top;
            double num3 = base.ItemsPanel.CalcPageHorizontalOffset(wrapper.RenderSize.Width) + pageRect.Left;
            double y = base.Presenter.ShowSingleItem ? ((point.Y - base.ItemsPanel.CalcStartPosition(true)) - pageRect.Top) : ((point.Y + base.ScrollViewer.VerticalOffset) - num2);
            Point point2 = new Point(point.X - num3, y);
            return new PdfDocumentPosition(pageIndex + 1, page.GetPdfPoint(point2, this.BehaviorProvider.ZoomFactor, this.BehaviorProvider.RotateAngle));
        }

        private PdfDocumentPosition CalcDocumentPositionInternal(Point point)
        {
            Point position = this.DocumentPresenter.GetPosition(this.DocumentPresenter.ActualPdfViewer);
            Point point3 = new Point(point.X - position.X, point.Y - position.Y);
            return this.CalcDocumentPosition(point3);
        }

        private double CalcFitZoomFactor(int pageIndex)
        {
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(pageIndex);
            PageWrapper wrapper = base.Presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            Size renderSize = wrapper.RenderSize;
            Size size2 = wrapper.CalcMarginSize();
            double num2 = !this.UsePageHeight(renderSize) ? (this.BehaviorProvider.Viewport.Width / (renderSize.Width - size2.Width)) : (this.BehaviorProvider.Viewport.Height / (renderSize.Height - size2.Height));
            return Math.Min(5.0, Math.Max((double) 0.1, (double) (this.BehaviorProvider.ZoomFactor * num2)));
        }

        private Point CalcTargetOffset(PdfPoint pdfPoint, int pageIndex)
        {
            Point point = this.DocumentPresenter.CalcPoint(pageIndex, pdfPoint);
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(pageIndex);
            Size renderSize = ((PdfPageWrapper) base.Presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex)).RenderSize;
            return new Point(point.X / renderSize.Width, point.Y / renderSize.Height);
        }

        private Point CalcTargetOffset(double? x, double? y, int pageIndex)
        {
            PdfPoint pdfPoint = this.CalcAnchorPoint(x, y, pageIndex);
            Point point2 = this.CalcTargetOffset(pdfPoint, pageIndex);
            return new Point((x != null) ? point2.X : 0.0, (y != null) ? point2.Y : 0.0);
        }

        protected internal Point CalcViewerPosition(PdfDocumentPosition position)
        {
            int pageIndex = position.PageIndex;
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(pageIndex);
            PageWrapper wrapper = this.DocumentPresenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            PdfPageViewModel model = wrapper.Pages.Single<IPage>(x => (x.PageIndex == pageIndex)) as PdfPageViewModel;
            double num2 = base.ItemsPanel.CalcPageHorizontalOffset(wrapper.RenderSize.Width) + model.Margin.Left;
            double num3 = base.PositionCalculator.GetPageVerticalOffset(pageIndex, 0.0) + model.Margin.Top;
            Point point = model.GetPoint(position.Point, this.BehaviorProvider.ZoomFactor, this.BehaviorProvider.RotateAngle);
            int num4 = base.ItemsPanel.IndexCalculator.VerticalOffsetToIndex(base.Presenter.ShowSingleItem ? base.ItemsPanel.GetVirtualVerticalOffset() : base.ItemsPanel.VerticalOffset);
            double num5 = (point.Y + base.ItemsPanel.CalcStartPosition(base.Presenter.ShowSingleItem)) + model.Margin.Top;
            if (num4 != pageWrapperIndex)
            {
                int num7 = num4;
                while (true)
                {
                    Size realItemSize;
                    if (num7 <= pageWrapperIndex)
                    {
                        while (num7 < pageWrapperIndex)
                        {
                            realItemSize = base.ItemsPanel.IndexCalculator.GetRealItemSize(num7++);
                            num5 += realItemSize.Height;
                        }
                        break;
                    }
                    realItemSize = base.ItemsPanel.IndexCalculator.GetRealItemSize(num7--);
                    num5 -= realItemSize.Height;
                }
            }
            return new Point((num2 + point.X) - base.ScrollViewer.HorizontalOffset, base.Presenter.ShowSingleItem ? num5 : ((num3 + point.Y) - base.ScrollViewer.VerticalOffset));
        }

        public Point ProcessConvertDocumentPosition(PdfDocumentPosition documentPosition)
        {
            Point position = this.DocumentPresenter.GetPosition(this.DocumentPresenter.ActualPdfViewer);
            Point point2 = this.CalcViewerPosition(documentPosition);
            return new Point((point2.X + position.X) + base.ScrollViewer.HorizontalOffset, point2.Y + position.Y);
        }

        public PdfDocumentPosition ProcessConvertPoint(Point point) => 
            this.CalcDocumentPositionInternal(point);

        public PdfHitTestResult ProcessHitTest(Point point)
        {
            PdfDocumentPosition position = this.CalcDocumentPositionInternal(point);
            PdfDocumentContent content = this.DocumentPresenter.Document.HitTest(position);
            return new PdfHitTestResult(content.DocumentPosition, content.ContentType, content.IsSelected);
        }

        public override void ProcessScrollViewerScrollChanged(ScrollChangedEventArgs e)
        {
            base.ProcessScrollViewerScrollChanged(e);
            Func<PdfPresenterControl, PdfViewerControl> evaluator = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<PdfPresenterControl, PdfViewerControl> local1 = <>c.<>9__26_0;
                evaluator = <>c.<>9__26_0 = x => x.ActualPdfViewer;
            }
            Func<PdfViewerControl, InteractionProvider> func2 = <>c.<>9__26_1;
            if (<>c.<>9__26_1 == null)
            {
                Func<PdfViewerControl, InteractionProvider> local2 = <>c.<>9__26_1;
                func2 = <>c.<>9__26_1 = x => x.InteractionProvider;
            }
            Action<InteractionProvider> action = <>c.<>9__26_2;
            if (<>c.<>9__26_2 == null)
            {
                Action<InteractionProvider> local3 = <>c.<>9__26_2;
                action = <>c.<>9__26_2 = x => x.ScrollViewerScrollChanged();
            }
            this.DocumentPresenter.With<PdfPresenterControl, PdfViewerControl>(evaluator).With<PdfViewerControl, InteractionProvider>(func2).Do<InteractionProvider>(action);
        }

        private void ProcessTargetEnsureCaretVisibility(PdfTarget target)
        {
            double valueOrDefault = target.X.GetValueOrDefault();
            double y = target.Y.GetValueOrDefault();
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(target.PageIndex);
            base.ScrollIntoView(pageWrapperIndex, this.DocumentPresenter.CalcRect(target.PageIndex, new PdfPoint(valueOrDefault, y - target.Height), new PdfPoint(valueOrDefault + 1.0, y)), ScrollIntoViewMode.Edge);
        }

        private void ProcessTargetEnsureVisibility(PdfTarget target)
        {
            int pageIndex = target.PageIndex;
            double valueOrDefault = target.X.GetValueOrDefault();
            double y = target.Y.GetValueOrDefault();
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(pageIndex);
            base.ScrollIntoView(pageWrapperIndex, this.DocumentPresenter.CalcRect(pageIndex, new PdfPoint(valueOrDefault, y), new PdfPoint(valueOrDefault + target.Width, y + target.Height)), ScrollIntoViewMode.Center);
        }

        private void ProcessTargetFit(PdfTarget target)
        {
            NavigationState state = new NavigationState();
            state.Angle = this.BehaviorProvider.RotateAngle;
            state.PageIndex = target.PageIndex;
            state.ZoomFactor = this.CalcFitZoomFactor(target.PageIndex);
            state.ZoomMode = ZoomMode.PageLevel;
            state.OffsetX = 0.0;
            state.OffsetY = 0.0;
            base.ChangePosition(state);
        }

        private void ProcessTargetFitHorizontally(PdfTarget target)
        {
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(target.PageIndex);
            Size pageSize = base.Presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex).PageSize;
            double num2 = this.BehaviorProvider.Viewport.Width / pageSize.Width;
            int pageIndex = target.PageIndex;
            PdfPoint pdfPoint = this.CalcAnchorPoint(0.0, target.Y, pageIndex);
            Point point2 = this.CalcTargetOffset(pdfPoint, target.PageIndex);
            NavigationState state1 = new NavigationState();
            state1.Angle = this.BehaviorProvider.RotateAngle;
            state1.PageIndex = target.PageIndex;
            state1.ZoomFactor = num2;
            state1.ZoomMode = ZoomMode.FitToWidth;
            state1.OffsetX = 0.0;
            state1.OffsetY = point2.Y;
            NavigationState state = state1;
            base.ChangePosition(state);
        }

        private void ProcessTargetFitRectangle(PdfTarget target)
        {
            int pageIndex = target.PageIndex;
            double valueOrDefault = target.X.GetValueOrDefault();
            double y = target.Y.GetValueOrDefault();
            Rect rect = this.DocumentPresenter.CalcRect(pageIndex, new PdfPoint(valueOrDefault, y - target.Height), new PdfPoint(valueOrDefault + target.Width, y));
            double zoomFactor = base.CalcFitRectangleZoomFactor(this.BehaviorProvider.Viewport, rect);
            Point point = this.DocumentPresenter.CalcPoint(pageIndex, new PdfPoint(valueOrDefault, y));
            base.ScrollToAnchorPoint(zoomFactor, base.CalcHorizontalAnchorPointForFitRectangle(rect, point.X), base.CalcVerticalAnchorPointForFitRectangle(rect, point.Y), UndoActionType.Scroll);
        }

        private void ProcessTargetFitVertically(PdfTarget target)
        {
            int pageWrapperIndex = base.PositionCalculator.GetPageWrapperIndex(target.PageIndex);
            PageWrapper wrapper = base.Presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            Size renderSize = wrapper.RenderSize;
            Size size2 = wrapper.CalcMarginSize();
            double num2 = this.BehaviorProvider.Viewport.Height / (renderSize.Height - size2.Height);
            int pageIndex = target.PageIndex;
            PdfPoint pdfPoint = this.CalcAnchorPoint(target.X, 0.0, pageIndex);
            Point point2 = this.CalcTargetOffset(pdfPoint, pageIndex);
            NavigationState state1 = new NavigationState();
            state1.Angle = this.BehaviorProvider.RotateAngle;
            state1.PageIndex = target.PageIndex;
            state1.ZoomFactor = num2;
            state1.ZoomMode = ZoomMode.Custom;
            state1.OffsetX = point2.X;
            state1.OffsetY = 0.0;
            NavigationState state = state1;
            base.ChangePosition(state);
        }

        private void ProcessTargetXYZ(PdfTarget target)
        {
            int pageIndex = target.PageIndex;
            Point point = this.CalcTargetOffset(target.X, target.Y, pageIndex);
            double num2 = Math.Min(Math.Max(target.Zoom.GetValueOrDefault(this.BehaviorProvider.ZoomFactor), this.BehaviorProvider.GetMinZoomFactor()), this.BehaviorProvider.GetMaxZoomFactor());
            NavigationState state1 = new NavigationState();
            state1.Angle = this.BehaviorProvider.RotateAngle;
            state1.PageIndex = pageIndex;
            state1.ZoomFactor = num2;
            state1.ZoomMode = ZoomMode.Custom;
            state1.OffsetX = point.X;
            state1.OffsetY = point.Y;
            NavigationState state = state1;
            base.ChangePosition(state);
        }

        public void ScrollIntoView(PdfTarget target)
        {
            if ((target != null) && this.DocumentPresenter.HasPages)
            {
                PdfTargetScroll scroll = target as PdfTargetScroll;
                if (scroll != null)
                {
                    if (scroll.InCenter)
                    {
                        this.ProcessTargetEnsureVisibility(target);
                    }
                    else
                    {
                        this.ProcessTargetEnsureCaretVisibility(target);
                    }
                }
                else
                {
                    switch (target.Mode)
                    {
                        case PdfTargetMode.XYZ:
                            this.ProcessTargetXYZ(target);
                            return;

                        case PdfTargetMode.Fit:
                        case PdfTargetMode.FitBBox:
                            this.ProcessTargetFit(target);
                            return;

                        case PdfTargetMode.FitHorizontally:
                        case PdfTargetMode.FitBBoxHorizontally:
                            this.ProcessTargetFitHorizontally(target);
                            return;

                        case PdfTargetMode.FitVertically:
                        case PdfTargetMode.FitBBoxVertically:
                            this.ProcessTargetFitVertically(target);
                            return;

                        case PdfTargetMode.FitRectangle:
                            this.ProcessTargetFitRectangle(target);
                            return;
                    }
                }
            }
        }

        protected override void UpdatePagesRotateAngleInternal(double rotateAngle)
        {
            base.UpdatePagesRotateAngleInternal(rotateAngle);
            Action<PdfPresenterControl> action = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Action<PdfPresenterControl> local1 = <>c.<>9__27_0;
                action = <>c.<>9__27_0 = x => x.InvalidateRenderCaches();
            }
            this.DocumentPresenter.Do<PdfPresenterControl>(action);
        }

        private bool UsePageHeight(Size pageSize)
        {
            double num = this.BehaviorProvider.Viewport.Width / this.BehaviorProvider.Viewport.Height;
            return pageSize.Width.LessThan((pageSize.Height * num));
        }

        private PdfPresenterControl DocumentPresenter =>
            base.Presenter as PdfPresenterControl;

        public DevExpress.Xpf.PdfViewer.SelectionRectangle SelectionRectangle =>
            this.DocumentPresenter.SelectionRectangle;

        protected PdfBehaviorProvider BehaviorProvider =>
            (PdfBehaviorProvider) base.BehaviorProvider;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfNavigationStrategy.<>c <>9 = new PdfNavigationStrategy.<>c();
            public static Func<PdfPresenterControl, PdfViewerControl> <>9__26_0;
            public static Func<PdfViewerControl, InteractionProvider> <>9__26_1;
            public static Action<InteractionProvider> <>9__26_2;
            public static Action<PdfPresenterControl> <>9__27_0;

            internal PdfViewerControl <ProcessScrollViewerScrollChanged>b__26_0(PdfPresenterControl x) => 
                x.ActualPdfViewer;

            internal InteractionProvider <ProcessScrollViewerScrollChanged>b__26_1(PdfViewerControl x) => 
                x.InteractionProvider;

            internal void <ProcessScrollViewerScrollChanged>b__26_2(InteractionProvider x)
            {
                x.ScrollViewerScrollChanged();
            }

            internal void <UpdatePagesRotateAngleInternal>b__27_0(PdfPresenterControl x)
            {
                x.InvalidateRenderCaches();
            }
        }
    }
}

