namespace DevExpress.Xpf.Core.HitTest
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public static class HitTestHelper
    {
        private static bool CanSkipElement(UIElement topElement) => 
            (!(topElement is Panel) || (((Panel) topElement).Background != null)) ? ((!(topElement is Control) || (((Control) topElement).Background != null)) ? (!(topElement is ContentPresenter) ? (topElement is ItemsPresenter) : true) : true) : true;

        private static IEnumerable<UIElement> FindElementsInHostCoordinates(UIElement reference, DevExpress.Xpf.Core.HitTest.PointHitTestParameters pointHitTestParameters)
        {
            Point hitPoint = pointHitTestParameters.HitPoint;
            List<UIElement> result = new List<UIElement>();
            FindElementsInHostCoordinatesCore(reference, reference, hitPoint, result);
            return result;
        }

        private static void FindElementsInHostCoordinatesCore(UIElement root, UIElement reference, Point point, List<UIElement> result)
        {
            for (int i = VisualTreeHelper.GetChildrenCount(reference) - 1; i >= 0; i--)
            {
                UIElement child = VisualTreeHelper.GetChild(reference, i) as UIElement;
                if (((child != null) && (child.Visibility != Visibility.Collapsed)) && LayoutHelper.GetRelativeElementRect(child, root).Contains(point))
                {
                    result.Add(child);
                    FindElementsInHostCoordinatesCore(root, child, point, result);
                }
            }
        }

        private static List<UIElement> GetElements(UIElement reference, DevExpress.Xpf.Core.HitTest.PointHitTestParameters pointHitTestParameters) => 
            new List<UIElement>(FindElementsInHostCoordinates(reference, pointHitTestParameters));

        private static List<UIElement> GetElements(UIElement topElement, Point point, bool skipDisabledElements)
        {
            if (skipDisabledElements)
            {
                return GetElements(topElement, new DevExpress.Xpf.Core.HitTest.PointHitTestParameters(point));
            }
            List<UIElement> list = new List<UIElement>();
            UIElement rootElement = GetRootElement(topElement);
            if (rootElement != null)
            {
                if (!IsPointInElementBounds(rootElement, topElement, point))
                {
                    return list;
                }
                if (!CanSkipElement(topElement))
                {
                    list.Add(topElement);
                }
                int childrenCount = VisualTreeHelper.GetChildrenCount(topElement);
                for (int i = 0; i < childrenCount; i++)
                {
                    UIElement child = VisualTreeHelper.GetChild(topElement, i) as UIElement;
                    if ((child != null) && IsPointInElementBounds(rootElement, child, point))
                    {
                        list.AddRange(GetElements(child, point, false));
                    }
                }
            }
            return list;
        }

        private static UIElement GetRootElement(UIElement element) => 
            LayoutHelper.GetTopLevelVisual(element);

        public static DevExpress.Xpf.Core.HitTest.HitTestResult HitTest(UIElement reference, Point point)
        {
            List<UIElement> elements = GetElements(reference, new DevExpress.Xpf.Core.HitTest.PointHitTestParameters(point));
            return ((elements.Count != 0) ? new DevExpress.Xpf.Core.HitTest.HitTestResult(elements[0]) : null);
        }

        public static DevExpress.Xpf.Core.HitTest.HitTestResult HitTest(UIElement reference, Point point, bool skipDisabledElements)
        {
            if (skipDisabledElements)
            {
                return HitTest(reference, point);
            }
            List<UIElement> list = GetElements(reference, point, false);
            return ((list.Count != 0) ? new DevExpress.Xpf.Core.HitTest.HitTestResult(list[list.Count - 1]) : null);
        }

        public static void HitTest(UIElement reference, DevExpress.Xpf.Core.HitTest.HitTestFilterCallback filterCallback, DevExpress.Xpf.Core.HitTest.HitTestResultCallback resultCallback, DevExpress.Xpf.Core.HitTest.PointHitTestParameters hitTestParameters)
        {
            HitTest(reference, filterCallback, resultCallback, hitTestParameters, false, true);
        }

        public static void HitTest(UIElement reference, DevExpress.Xpf.Core.HitTest.HitTestFilterCallback filterCallback, DevExpress.Xpf.Core.HitTest.HitTestResultCallback resultCallback, DevExpress.Xpf.Core.HitTest.PointHitTestParameters hitTestParameters, bool isReverce, bool useFindElementsInHostCoordinates = true)
        {
            if ((resultCallback != null) || (filterCallback != null))
            {
                List<UIElement> list = useFindElementsInHostCoordinates ? GetElements(reference, hitTestParameters) : GetElements(reference, hitTestParameters.HitPoint, false);
                if (!isReverce)
                {
                    list.Reverse();
                }
                DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior behavior = DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior.Continue;
                UIElement objA = null;
                int num = 0;
                while (true)
                {
                    while (true)
                    {
                        if (num >= list.Count)
                        {
                            return;
                        }
                        behavior = RaiseFilterCallback(filterCallback, list[num], ReferenceEquals(objA, null));
                        if (behavior == DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior.Stop)
                        {
                            return;
                        }
                        if (behavior != DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior.ContinueSkipSelf)
                        {
                            if (behavior == DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior.ContinueSkipChildren)
                            {
                                objA = list[num];
                            }
                            else if (behavior == DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior.ContinueSkipSelfAndChildren)
                            {
                                objA = list[num];
                                break;
                            }
                            if (!ReferenceEquals(VisualTreeHelper.GetParent(list[num]), objA))
                            {
                                objA = null;
                                if (RaiseResultCallback(resultCallback, list[num]) == DevExpress.Xpf.Core.HitTest.HitTestResultBehavior.Stop)
                                {
                                    return;
                                }
                            }
                        }
                        break;
                    }
                    num++;
                }
            }
        }

        public static bool IsHitTest(UIElement reference, UIElement topElement, Point point)
        {
            UIElement rootElement = GetRootElement(topElement);
            if ((reference is FrameworkElement) && ((((FrameworkElement) reference).FlowDirection == FlowDirection.RightToLeft) && (rootElement is FrameworkElement)))
            {
                point.X = rootElement.RenderSize.Width - point.X;
            }
            List<UIElement> elements = GetElements(topElement, new DevExpress.Xpf.Core.HitTest.PointHitTestParameters(point));
            return ((elements.Count != 0) ? elements.Contains(reference) : false);
        }

        private static bool IsPointInElementBounds(UIElement root, UIElement element, Point point)
        {
            if ((element.Visibility == Visibility.Collapsed) || !element.IsHitTestVisible)
            {
                return false;
            }
            Rect rect = element.TransformToVisual(root).TransformBounds(new Rect(0.0, 0.0, element.RenderSize.Width, element.RenderSize.Height));
            return ((point.X >= rect.X) && ((point.X <= (rect.X + rect.Width)) && ((point.Y >= rect.Y) && (point.Y <= (rect.Y + rect.Height)))));
        }

        private static DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior RaiseFilterCallback(DevExpress.Xpf.Core.HitTest.HitTestFilterCallback filterCallback, DependencyObject patentialHitTestTarget, bool raise) => 
            ((filterCallback == null) || !raise) ? DevExpress.Xpf.Core.HitTest.HitTestFilterBehavior.Continue : filterCallback(patentialHitTestTarget);

        private static DevExpress.Xpf.Core.HitTest.HitTestResultBehavior RaiseResultCallback(DevExpress.Xpf.Core.HitTest.HitTestResultCallback resultCallback, DependencyObject visualHit) => 
            (resultCallback != null) ? resultCallback(new DevExpress.Xpf.Core.HitTest.HitTestResult(visualHit)) : DevExpress.Xpf.Core.HitTest.HitTestResultBehavior.Continue;

        public static Point TransformPointToRoot(UIElement element, Point pt)
        {
            UIElement rootElement = GetRootElement(element);
            Point point = element.TransformToVisual(rootElement).Transform(pt);
            if ((((FrameworkElement) rootElement).FlowDirection == FlowDirection.RightToLeft) && ((rootElement is ContentControl) && ((((ContentControl) rootElement).Content != null) && (((ContentControl) rootElement).Content is FrameworkElement))))
            {
                point.X = ((FrameworkElement) ((ContentControl) rootElement).Content).ActualWidth - point.X;
            }
            return point;
        }
    }
}

