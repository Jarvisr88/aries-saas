namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class DocumentNavigationStrategy : NavigationStrategy
    {
        private readonly Locker pageIndexSyncLocker;
        private readonly Locker backPageIndexSyncLocker;

        public DocumentNavigationStrategy(DevExpress.Xpf.Printing.DocumentPresenterControl presenter) : base(presenter)
        {
            this.pageIndexSyncLocker = typeof(NavigationStrategy).GetField("pageIndexSyncLocker", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this) as Locker;
            this.backPageIndexSyncLocker = typeof(NavigationStrategy).GetField("backPageIndexSyncLocker", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this) as Locker;
        }

        private double CalcPageVerticalOffset(PageWrapper wrapper) => 
            (this.Presenter.ItemsControl.ItemContainerGenerator.ContainerFromItem(wrapper) as FrameworkElement).TranslatePoint(new System.Windows.Point(0.0, 0.0), this.Presenter).Y;

        internal NavigationState GenerateNavigationState() => 
            (base.ScrollViewer != null) ? base.GenerateCurrentState(0.0, base.BehaviorProvider.ZoomFactor, base.BehaviorProvider.ZoomMode) : null;

        public Brick GetBrick(System.Windows.Point point)
        {
            Func<Pair<PageViewModel, Brick>, Brick> evaluator = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<Pair<PageViewModel, Brick>, Brick> local1 = <>c.<>9__13_0;
                evaluator = <>c.<>9__13_0 = x => x.Second;
            }
            return this.GetPageModelBrickPair(point).With<Pair<PageViewModel, Brick>, Brick>(evaluator);
        }

        public Pair<PageViewModel, Brick> GetPageModelBrickPair(System.Windows.Point point)
        {
            PageViewModel pageViewModel = this.GetPageViewModel(point);
            if (pageViewModel == null)
            {
                return null;
            }
            PageWrapper pageWrapper = this.GetPageWrapper(pageViewModel.PageIndex);
            Rect pageRect = pageWrapper.GetPageRect(pageViewModel);
            double num = (this.Presenter.ShowSingleItem ? this.CalcPageVerticalOffset(pageWrapper) : this.PositionCalculator.GetPageVerticalOffset(pageViewModel.PageIndex, 0.0)) + pageRect.Top;
            double num2 = this.Presenter.ItemsPanel.CalcPageHorizontalOffset(pageWrapper.RenderSize.Width) + pageRect.Left;
            double scaleX = this.Presenter.GetScaleX();
            double num4 = this.Presenter.ShowSingleItem ? 0.0 : base.ScrollViewer.VerticalOffset;
            System.Windows.Point relativePoint = new System.Windows.Point((point.X - num2) * scaleX, ((point.Y + num4) - num) * scaleX);
            return new Pair<PageViewModel, Brick>(pageViewModel, pageViewModel.GetBrick(relativePoint, base.BehaviorProvider.ZoomFactor));
        }

        public Rect GetPageRect(int pageIndex)
        {
            PageWrapper pageWrapper = this.GetPageWrapper(pageIndex);
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect((UIElement) this.Presenter.ItemsControl.ItemContainerGenerator.ContainerFromItem(pageWrapper), this.Presenter.ItemsControl);
            Rect pageRect = pageWrapper.GetPageRect(this.Presenter.Document.Pages.Single<PageViewModel>(x => x.PageIndex == pageIndex));
            pageRect.Offset(relativeElementRect.Left, relativeElementRect.Top);
            return pageRect;
        }

        public PageViewModel GetPageViewModel(System.Windows.Point point)
        {
            if ((this.Presenter.Pages == null) || ((this.Presenter.ItemsPanel == null) || (this.Presenter.ScrollViewer == null)))
            {
                return null;
            }
            PageViewModel model = null;
            if (!this.Presenter.ShowSingleItem)
            {
                int pageIndex = this.PositionCalculator.GetPageIndex(point.Y + base.ScrollViewer.VerticalOffset, point.X, new Func<double, double>(base.ItemsPanel.CalcPageHorizontalOffset));
                if (pageIndex == -1)
                {
                    return null;
                }
                model = this.GetPageWrapper(pageIndex).With<PageWrapper, PageViewModel>(delegate (PageWrapper p) {
                    Func<IPage, bool> <>9__1;
                    Func<IPage, bool> predicate = <>9__1;
                    if (<>9__1 == null)
                    {
                        Func<IPage, bool> local1 = <>9__1;
                        predicate = <>9__1 = x => x.PageIndex == pageIndex;
                    }
                    return p.Pages.SingleOrDefault<IPage>(predicate) as PageViewModel;
                });
            }
            else
            {
                int index = base.ItemsPanel.IndexCalculator.VerticalOffsetToIndex(base.ItemsPanel.GetVirtualVerticalOffset());
                if (index == -1)
                {
                    return null;
                }
                PageWrapper item = this.Presenter.Pages.ElementAt<PageWrapper>(index);
                System.Windows.Point point2 = ((UIElement) this.Presenter.ItemsControl.ItemContainerGenerator.ContainerFromItem(item)).TranslatePoint(new System.Windows.Point(0.0, 0.0), this.Presenter);
                using (IEnumerator<IPage> enumerator = item.Pages.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        IPage current = enumerator.Current;
                        Rect pageRect = item.GetPageRect(current);
                        pageRect.Offset(point2.X, point2.Y);
                        if (pageRect.IsInside(point))
                        {
                            return (current as PageViewModel);
                        }
                    }
                }
            }
            return model;
        }

        public PageWrapper GetPageWrapper(int pageIndex)
        {
            int pageWrapperIndex = this.PositionCalculator.GetPageWrapperIndex(pageIndex);
            return this.Presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
        }

        private Rect GetPageWrapperRect(int pageWrapperIndex)
        {
            System.Windows.Size realItemSize = base.ItemsPanel.IndexCalculator.GetRealItemSize(pageWrapperIndex);
            return new Rect(0.0, base.ItemsPanel.IndexCalculator.IndexToVerticalOffset(pageWrapperIndex, false), realItemSize.IsEmpty ? 0.0 : realItemSize.Width, realItemSize.IsEmpty ? 0.0 : realItemSize.Height);
        }

        public override void ProcessPageIndexChanged(int pageIndex)
        {
            if (base.ItemsPanel == null)
            {
                base.ProcessPageIndexChanged(pageIndex);
            }
            else
            {
                this.pageIndexSyncLocker.DoIfNotLocked(delegate {
                    this.backPageIndexSyncLocker.LockOnce();
                    int pageWrapperIndex = this.PositionCalculator.GetPageWrapperIndex(pageIndex);
                    Rect pageWrapperRect = this.GetPageWrapperRect(pageWrapperIndex);
                    bool flag = (this.ItemsPanel.IndexCalculator.VerticalOffsetToIndex(this.ItemsPanel.GetVirtualVerticalOffset()) != -1) ? this.ItemsPanel.IsRectangleVisible(pageWrapperRect) : false;
                    this.ScrollIntoView(pageWrapperIndex, Rect.Empty, flag ? ScrollIntoViewMode.Edge : ScrollIntoViewMode.TopLeft);
                });
            }
        }

        public override void ProcessZoomChanged(ZoomChangedEventArgs e)
        {
            base.ProcessZoomChanged(e);
            Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>c.<>9__7_0;
                action = <>c.<>9__7_0 = x => x.InvalidateRenderCaches();
            }
            this.Presenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
        }

        public void ShowBrick(BrickPagePair brickPagePair, ScrollIntoViewMode? scrollIntoView = new ScrollIntoViewMode?())
        {
            if (brickPagePair.GetBrick(this.Presenter.Document.PrintingSystem.Pages) != null)
            {
                this.ShowBrickCore(brickPagePair, scrollIntoView);
            }
            else
            {
                (this.Presenter.Document as DocumentViewModel).Do<DocumentViewModel>(delegate (DocumentViewModel x) {
                    Action<BrickPagePair> <>9__1;
                    Action<BrickPagePair> onEnsured = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action<BrickPagePair> local1 = <>9__1;
                        onEnsured = <>9__1 = pair => this.ShowBrickCore(pair, scrollIntoView);
                    }
                    x.EnsureBrickOnPage(brickPagePair, onEnsured);
                });
            }
        }

        private void ShowBrickCore(BrickPagePair brickPagePair, ScrollIntoViewMode? scrollIntoView = new ScrollIntoViewMode?())
        {
            int pageIndex = brickPagePair.PageIndex;
            int pageWrapperIndex = this.PositionCalculator.GetPageWrapperIndex(pageIndex);
            RectangleF ef2 = GraphicsUnitConverter.Convert(brickPagePair.GetBrickBounds(this.Presenter.Document.PrintingSystem.Pages), (float) 300f, (float) 96f);
            Rect rect = new Rect(ef2.X * base.BehaviorProvider.ZoomFactor, ef2.Y * base.BehaviorProvider.ZoomFactor, ef2.Width * base.BehaviorProvider.ZoomFactor, ef2.Height * base.BehaviorProvider.ZoomFactor);
            base.ScrollIntoView(pageWrapperIndex, rect, (scrollIntoView != null) ? scrollIntoView.Value : (this.Presenter.IsSearchControlVisible ? ScrollIntoViewMode.Center : ScrollIntoViewMode.TopLeft));
        }

        protected DevExpress.Xpf.Printing.DocumentPresenterControl Presenter =>
            base.Presenter as DevExpress.Xpf.Printing.DocumentPresenterControl;

        internal DevExpress.Xpf.DocumentViewer.PositionCalculator PositionCalculator =>
            base.PositionCalculator;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentNavigationStrategy.<>c <>9 = new DocumentNavigationStrategy.<>c();
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__7_0;
            public static Func<Pair<PageViewModel, Brick>, Brick> <>9__13_0;

            internal Brick <GetBrick>b__13_0(Pair<PageViewModel, Brick> x) => 
                x.Second;

            internal void <ProcessZoomChanged>b__7_0(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.InvalidateRenderCaches();
            }
        }
    }
}

