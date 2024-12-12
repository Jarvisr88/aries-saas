namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class DockLayoutManagerViewAdapter : ViewAdapter
    {
        public DockLayoutManagerViewAdapter(DockLayoutManager container)
        {
            base.NotificationSource = container;
        }

        private IView CheckBehindView(IView sourceView, IView behindView, Point screenPoint, bool skipBehindView)
        {
            if ((base.DragService.OperationType != DevExpress.Xpf.Layout.Core.OperationType.ClientDragging) || ((behindView != null) && !base.Views.Contains(behindView)))
            {
                return behindView;
            }
            IView source = sourceView.Adapter.GetView(screenPoint);
            return (skipBehindView ? base.GetBehindViewOverride(source, screenPoint) : source);
        }

        protected override IView GetBehindViewOverride(IView source, Point screenPoint)
        {
            bool flag = base.DragService.OperationType == DevExpress.Xpf.Layout.Core.OperationType.ClientDragging;
            LayoutView view = null;
            LayoutView singleElement = source as LayoutView;
            bool skipBehindView = this.IsHitInDragElement(source, screenPoint);
            if ((this.Container.Linked.Count > 0) && (singleElement != null))
            {
                Func<FloatingView, FloatingWindowPresenter> evaluator = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<FloatingView, FloatingWindowPresenter> local1 = <>c.<>9__3_0;
                    evaluator = <>c.<>9__3_0 = x => x.RootElement as FloatingWindowPresenter;
                }
                Func<FloatingWindowPresenter, FloatingPaneWindow> func2 = <>c.<>9__3_1;
                if (<>c.<>9__3_1 == null)
                {
                    Func<FloatingWindowPresenter, FloatingPaneWindow> local2 = <>c.<>9__3_1;
                    func2 = <>c.<>9__3_1 = x => x.Window;
                }
                FloatingPaneWindow window = (singleElement as FloatingView).With<FloatingView, FloatingWindowPresenter>(evaluator).Return<FloatingWindowPresenter, FloatingPaneWindow>(func2, <>c.<>9__3_2 ??= ((Func<FloatingPaneWindow>) (() => null)));
                Point point = singleElement.ScreenToClient(screenPoint);
                Point point2 = singleElement.RootUIElement.PointToScreen(point);
                IEnumerable<Window> first = NativeHelper.SortWindowsTopToBottom(this.Container.GetAllAffectedWindows());
                if (!flag | skipBehindView)
                {
                    first = first.Except<Window>(window.Yield<FloatingPaneWindow>());
                }
                Window reference = first.FirstOrDefault<Window>(x => (x.IsVisible && !(x is AdornerWindow)) && (LayoutHelper.GetScreenRect(x).Contains(point2) && (!x.GetType().FullName.Contains("WpfVisualTreeService.Adorners.AdornerLayerWindow") && (!x.GetType().FullName.Contains("WpfVisualTreeService.Adorners.AdornerWindow") && !(x is DragCursorWindow)))));
                if (reference != null)
                {
                    BehindViewHitTest test = new BehindViewHitTest();
                    test.HitTest(reference, reference.PointFromScreen(point2));
                    IEnumerable<object> views = test.Views;
                    if (!flag | skipBehindView)
                    {
                        views = views.Except<object>(singleElement.Yield<LayoutView>());
                    }
                    foreach (object obj2 in views)
                    {
                        LayoutView view4 = obj2 as LayoutView;
                        if (view4 != null)
                        {
                            DockLayoutManager container = view4.Container;
                            if ((ReferenceEquals(this.Container, container) || (this.Container.Linked.Contains(container) && (this.Container.GetRealFloatingMode() == container.GetRealFloatingMode()))) && container.IsVisible)
                            {
                                if (view == null)
                                {
                                    view = view4;
                                    continue;
                                }
                                if (view4.RootUIElement.IsDescendantOf(view.RootUIElement))
                                {
                                    view = view4;
                                }
                            }
                        }
                    }
                    if (view != null)
                    {
                        DockLayoutManager container = view.Container;
                        IDockLayoutElement dragItem = base.DragService.DragItem as IDockLayoutElement;
                        UIElement sourceElement = (!flag || ((dragItem == null) || (dragItem.Element == null))) ? singleElement.RootUIElement : dragItem.Element;
                        if (this.GetLogicalParents(container).OfType<Visual>().Any<Visual>(x => x.IsDescendantOf(sourceElement)))
                        {
                            return null;
                        }
                        if (!ReferenceEquals(container, this.Container))
                        {
                            container.LinkedDragService = this.Container.ViewAdapter.DragService;
                        }
                    }
                }
            }
            IView behindView = view ?? base.GetBehindViewOverride(source, screenPoint);
            return this.CheckBehindView(source, behindView, screenPoint, skipBehindView);
        }

        protected override Point GetBehindViewPointOverride(IView source, IView behindView, Point screenPoint)
        {
            if (!ReferenceEquals(behindView.Adapter, this))
            {
                Point point = ((LayoutView) source).ScreenToClient(screenPoint);
                Point point2 = ((LayoutView) source).RootUIElement.PointToScreen(point);
                using (IEnumerator<DockLayoutManager> enumerator = this.Container.Linked.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        DockLayoutManager current = enumerator.Current;
                        if ((current != null) && ReferenceEquals(current.ViewAdapter, behindView.Adapter))
                        {
                            return ((LayoutView) behindView).RootUIElement.PointFromScreen(point2);
                        }
                    }
                }
            }
            return base.GetBehindViewPointOverride(source, behindView, screenPoint);
        }

        private List<DependencyObject> GetLogicalParents(DependencyObject obj)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            while (obj != null)
            {
                list.Add(obj);
                obj = LogicalTreeHelper.GetParent(obj);
            }
            return list;
        }

        private bool IsHitInDragElement(IView view, Point screenPoint)
        {
            IView objA = view.Adapter.GetView(screenPoint);
            IView dragSource = view.Adapter.DragService.DragSource;
            if (!ReferenceEquals(objA, dragSource))
            {
                return false;
            }
            ILayoutElement element = objA.Adapter.CalcHitInfo(objA, dragSource.ScreenToClient(screenPoint)).Element;
            IDockLayoutElement dragItem = (IDockLayoutElement) view.Adapter.DragService.DragItem;
            IDockLayoutElement root = (IDockLayoutElement) ElementHelper.GetRoot(element);
            IDockLayoutElement objB = (IDockLayoutElement) ElementHelper.GetRoot(dragItem);
            return (((!ReferenceEquals(element, dragItem) && !ReferenceEquals(root, objB)) || !ReferenceEquals(dragItem.CheckDragElement(), root.CheckDragElement())) ? ReferenceEquals(dragItem, root) : true);
        }

        private DockLayoutManager Container =>
            base.NotificationSource as DockLayoutManager;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockLayoutManagerViewAdapter.<>c <>9 = new DockLayoutManagerViewAdapter.<>c();
            public static Func<FloatingView, FloatingWindowPresenter> <>9__3_0;
            public static Func<FloatingWindowPresenter, FloatingPaneWindow> <>9__3_1;
            public static Func<FloatingPaneWindow> <>9__3_2;

            internal FloatingWindowPresenter <GetBehindViewOverride>b__3_0(FloatingView x) => 
                x.RootElement as FloatingWindowPresenter;

            internal FloatingPaneWindow <GetBehindViewOverride>b__3_1(FloatingWindowPresenter x) => 
                x.Window;

            internal FloatingPaneWindow <GetBehindViewOverride>b__3_2() => 
                null;
        }

        private class BehindViewHitTest
        {
            private List<object> views = new List<object>();

            public void HitTest(FrameworkElement reference, Point hitPoint)
            {
                VisualTreeHelper.HitTest(reference, new HitTestFilterCallback(this.HitTestFilter), new HitTestResultCallback(this.HitTestResult), new PointHitTestParameters(hitPoint));
            }

            private HitTestFilterBehavior HitTestFilter(DependencyObject potentialHitTestTarget)
            {
                BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(potentialHitTestTarget);
                if ((layoutItem != null) && !(layoutItem is FloatGroup))
                {
                    IView item = layoutItem.Manager.GetView(layoutItem.GetRoot());
                    if ((item != null) && !this.views.Contains(item))
                    {
                        this.views.Add(item);
                    }
                }
                return HitTestFilterBehavior.Continue;
            }

            private HitTestResultBehavior HitTestResult(System.Windows.Media.HitTestResult result) => 
                HitTestResultBehavior.Continue;

            public IEnumerable<object> Views =>
                this.views;
        }
    }
}

