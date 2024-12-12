namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public static class HitTestHelper
    {
        private static HitCache HitTestCache = new HitCache();

        private static HitTestType CheckAdornerDecorator(HitTestResult result)
        {
            AdornerDecorator d = LayoutHelper.FindParentObject<AdornerDecorator>(result.VisualHit);
            if (d != null)
            {
                DependencyObject parent = LayoutHelper.GetParent(d, false);
                if (parent != null)
                {
                    return (HitTestType) parent.GetValue(DockPane.HitTestTypeProperty);
                }
            }
            return HitTestType.Undefined;
        }

        private static HitTestType CheckNonLogicalTree(HitTestResult hitElement)
        {
            HitTestType undefined = HitTestType.Undefined;
            DependencyObject visualHit = hitElement.VisualHit;
            while (true)
            {
                if (visualHit != null)
                {
                    undefined = DockPane.GetHitTestType(visualHit);
                    if (undefined == HitTestType.Undefined)
                    {
                        visualHit = LayoutHelper.GetParent(visualHit, false);
                        continue;
                    }
                }
                return undefined;
            }
        }

        public static bool CheckVisualHitTest(IDockLayoutElement element, Point pt) => 
            CheckVisualHitTest(element, pt, new Func<DependencyObject, DependencyObject, bool>(HitTestHelper.IsVisualChild));

        public static bool CheckVisualHitTest(IDockLayoutElement element, Point pt, Func<DependencyObject, DependencyObject, bool> isVisualChildDelegate)
        {
            UIElement view = element.View;
            UIElement element3 = element.Element;
            if (element is FloatPanePresenterElement)
            {
                element3 = view;
            }
            AutoHideTray tray = view as AutoHideTray;
            if (((tray != null) && !(element3 is AutoHideTray)) && (FindScope<AutoHidePane>(element3 as IUIElement) != null))
            {
                AutoHidePane panel = tray.Panel;
                pt = tray.TranslatePoint(pt, panel);
                view = panel;
            }
            HitTestResult hitResult = GetHitResult(view, pt);
            DependencyObject visualHit = hitResult?.VisualHit;
            return ((visualHit == null) ? false : isVisualChildDelegate(element3, visualHit));
        }

        private static IUIElement FindScope<T>(IUIElement element)
        {
            if (element != null)
            {
                if (element is T)
                {
                    return element;
                }
                for (IUIElement element2 = element.Scope; element2 != null; element2 = element2.Scope)
                {
                    if (element2 is T)
                    {
                        return element2;
                    }
                }
            }
            return null;
        }

        public static HitTestResult GetHitResult(UIElement element, Point hitPoint) => 
            HitTestCache.GetHitResult(element, hitPoint);

        public static HitTestType GetHitTestType(HitTestResult hitResult)
        {
            HitTestType hitTestType = DockPane.GetHitTestType(hitResult.VisualHit);
            if (hitTestType == HitTestType.Undefined)
            {
                hitTestType = CheckAdornerDecorator(hitResult);
                hitTestType ??= CheckNonLogicalTree(hitResult);
            }
            return hitTestType;
        }

        public static HitTestResult HitTest(UIElement element, Point hitPoint, ref HitCache cache)
        {
            cache ??= new HitCache();
            return cache.GetHitResult(element, hitPoint);
        }

        public static bool HitTestTypeEquals(object prevHitResult, object hitResult)
        {
            bool flag = Equals(prevHitResult, hitResult);
            if (!flag)
            {
                if ((hitResult == null) && Equals(HitTestType.Undefined, prevHitResult))
                {
                    return true;
                }
                if ((prevHitResult == null) && Equals(HitTestType.Undefined, hitResult))
                {
                    return true;
                }
            }
            return flag;
        }

        public static bool IsDraggable(DependencyObject element)
        {
            if (element == null)
            {
                return false;
            }
            HitTestType hitTestType = DockPane.GetHitTestType(element);
            return ((hitTestType == HitTestType.Label) || (hitTestType == HitTestType.Header));
        }

        private static bool IsVisualChild(DependencyObject root, DependencyObject child)
        {
            for (DependencyObject obj2 = child; obj2 != null; obj2 = VisualTreeHelper.GetParent(obj2))
            {
                if (ReferenceEquals(obj2, root))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ResetCache()
        {
            HitTestCache.Reset();
        }

        private static bool TryGetControlBox(DependencyObject element, out BaseControlBoxControl result)
        {
            bool flag;
            result = null;
            using (IEnumerator<DependencyObject> enumerator = LayoutItemsHelper.GetEnumerator(element, null))
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        if (!(enumerator.Current is BaseControlBoxControl))
                        {
                            continue;
                        }
                        result = enumerator.Current as BaseControlBoxControl;
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public sealed class HitCache : IDisposable
        {
            private HitCacheHelper hitCacheHelper = new HitCacheHelper();

            public void Dispose()
            {
                this.Reset();
                GC.SuppressFinalize(this);
            }

            public HitTestResult GetHitResult(UIElement element, Point point) => 
                (element != null) ? this.hitCacheHelper.GetHitResult(element, point) : null;

            private static bool IsValid(HitTestResult result) => 
                (result != null) && (LayoutHelper.GetParent(result.VisualHit, false) != null);

            public void Reset()
            {
                this.hitCacheHelper.Reset();
            }

            private class HitCacheHelper
            {
                private const int hitCacheSize = 3;
                private HitCacheResultQueue cacheInternal = new HitCacheResultQueue();

                public HitTestResult GetHitResult(UIElement element, Point point)
                {
                    HitCacheResultQueue cacheInternal = this.cacheInternal;
                    HitTestResult hitTest = cacheInternal.FirstOrDefault<HitCacheResult>(hit => (!hit.Accept(point, element) && (hit.Result != null)), HitCacheResult.Empty).Result;
                    if ((hitTest == null) || !HitTestHelper.HitCache.IsValid(hitTest))
                    {
                        hitTest = new HitTestHelper.TopMostDockLayoutManagerHitResult(element, point).GetHitTest();
                        cacheInternal.Enqueue(new HitCacheResult(element, point, hitTest));
                        if (cacheInternal.Count > 3)
                        {
                            cacheInternal.Dequeue();
                        }
                    }
                    return hitTest;
                }

                public void Reset()
                {
                    HitCacheResultQueue queue = new HitCacheResultQueue();
                    this.cacheInternal = queue;
                }

                private class HitCacheResult
                {
                    public static HitTestHelper.HitCache.HitCacheHelper.HitCacheResult Empty = new HitTestHelper.HitCache.HitCacheHelper.HitCacheResult();
                    private WeakReference lastElement;
                    private Point lastPoint;
                    private WeakReference resultReference;

                    private HitCacheResult()
                    {
                    }

                    public HitCacheResult(UIElement element, Point point, HitTestResult result)
                    {
                        this.lastElement = new WeakReference(element);
                        this.lastPoint = point;
                        this.resultReference = new WeakReference(result);
                    }

                    public bool Accept(Point point, UIElement element) => 
                        (point != this.lastPoint) || this.CheckElement(element);

                    private bool CheckElement(UIElement element) => 
                        (this.lastElement == null) || (this.lastElement.Target != element);

                    public HitTestResult Result =>
                        (this.resultReference != null) ? ((HitTestResult) this.resultReference.Target) : null;
                }

                private class HitCacheResultQueue : ConcurrentQueue<HitTestHelper.HitCache.HitCacheHelper.HitCacheResult>
                {
                    public HitTestHelper.HitCache.HitCacheHelper.HitCacheResult Dequeue()
                    {
                        HitTestHelper.HitCache.HitCacheHelper.HitCacheResult result;
                        base.TryDequeue(out result);
                        return result;
                    }
                }
            }
        }

        private class TopMostDockLayoutManagerHitResult
        {
            private System.Windows.Media.HitTestResult _hitResult;

            public TopMostDockLayoutManagerHitResult(UIElement view, Point hitPoint)
            {
                VisualTreeHelper.HitTest(view, new HitTestFilterCallback(this.NoNestedDockLayoutManager), new HitTestResultCallback(this.HitTestResult), new PointHitTestParameters(hitPoint));
            }

            public System.Windows.Media.HitTestResult GetHitTest() => 
                this._hitResult;

            private HitTestResultBehavior HitTestResult(System.Windows.Media.HitTestResult result)
            {
                this._hitResult = result;
                return HitTestResultBehavior.Stop;
            }

            private bool isVisible(UIElement element) => 
                element.IsVisible;

            private HitTestFilterBehavior NoNestedDockLayoutManager(DependencyObject potentialHitTestTarget) => 
                (!(potentialHitTestTarget is UIElement) || this.isVisible((UIElement) potentialHitTestTarget)) ? (!(potentialHitTestTarget is DockLayoutManager) ? HitTestFilterBehavior.Continue : HitTestFilterBehavior.ContinueSkipChildren) : HitTestFilterBehavior.ContinueSkipSelfAndChildren;
        }
    }
}

