namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public static class FrameworkElementExtensions
    {
        public static void ApplyStyleValuesToPropertiesWithLocalValues(this FrameworkElement element)
        {
            if (element.Style != null)
            {
                foreach (Setter setter in element.Style.Setters)
                {
                    if (element.IsPropertyAssigned(setter.Property))
                    {
                        element.SetValue(setter.Property, setter.Value);
                    }
                }
            }
        }

        public static void ClipToBounds(this FrameworkElement element)
        {
            RectangleGeometry geometry1 = new RectangleGeometry();
            geometry1.Rect = RectHelper.New(element.GetSize());
            element.Clip = geometry1;
        }

        public static bool Contains(this FrameworkElement element, Point absolutePosition)
        {
            DependencyObject firstHit = null;
            HitTestResultCallback resultCallback = <>c.<>9__38_1;
            if (<>c.<>9__38_1 == null)
            {
                HitTestResultCallback local1 = <>c.<>9__38_1;
                resultCallback = <>c.<>9__38_1 = hitTestResult => HitTestResultBehavior.Stop;
            }
            VisualTreeHelper.HitTest(element, delegate (DependencyObject hitElement) {
                firstHit = hitElement;
                return HitTestFilterBehavior.Stop;
            }, resultCallback, new PointHitTestParameters(element.MapPointFromScreen(absolutePosition)));
            return ReferenceEquals(firstHit, element);
        }

        public static UIElement FindElement(this FrameworkElement element, Point absolutePosition, Func<UIElement, bool> condition)
        {
            UIElement result = null;
            HitTestResultCallback resultCallback = <>c.<>9__39_1;
            if (<>c.<>9__39_1 == null)
            {
                HitTestResultCallback local1 = <>c.<>9__39_1;
                resultCallback = <>c.<>9__39_1 = hitTestResult => HitTestResultBehavior.Continue;
            }
            VisualTreeHelper.HitTest(element, delegate (DependencyObject hitElement) {
                result = hitElement as UIElement;
                return ((result == null) || !condition(result)) ? HitTestFilterBehavior.Continue : HitTestFilterBehavior.Stop;
            }, resultCallback, new PointHitTestParameters(element.MapPointFromScreen(absolutePosition)));
            return result;
        }

        public static Rect GetBounds(this FrameworkElement element) => 
            element.GetBounds(element.GetParent() as FrameworkElement);

        public static Rect GetBounds(this FrameworkElement element, FrameworkElement relativeTo) => 
            element.MapRect(new Rect(new Point(0.0, 0.0), element.GetSize()), relativeTo);

        public static bool GetIsClipped(this FrameworkElement element) => 
            FrameworkElementHelper.GetIsClipped(element);

        public static double GetLeft(this FrameworkElement element) => 
            element.GetPosition().X;

        public static Size GetMaxSize(this FrameworkElement element) => 
            new Size(element.MaxWidth, element.MaxHeight);

        public static Size GetMinSize(this FrameworkElement element) => 
            new Size(element.MinWidth, element.MinHeight);

        public static DependencyObject GetParent(this FrameworkElement element)
        {
            DependencyObject parent = element.Parent;
            return VisualTreeHelper.GetParent(element);
        }

        public static Point GetPosition(this FrameworkElement element) => 
            element.GetPosition(element.GetParent() as FrameworkElement);

        public static Point GetPosition(this FrameworkElement element, FrameworkElement relativeTo) => 
            element.GetBounds(relativeTo).Location();

        public static double GetRealHeight(this FrameworkElement element) => 
            Math.Max(element.MinHeight, Math.Min(element.Height, element.MaxHeight));

        public static double GetRealWidth(this FrameworkElement element) => 
            Math.Max(element.MinWidth, Math.Min(element.Width, element.MaxWidth));

        public static FrameworkElement GetRootParent(this FrameworkElement element)
        {
            DependencyObject visualParent = element;
            while (true)
            {
                FrameworkElement element2 = (FrameworkElement) visualParent;
                visualParent = element2.GetVisualParent();
                if (visualParent == null)
                {
                    return element2;
                }
            }
        }

        public static Size GetSize(this FrameworkElement element) => 
            new Size(element.ActualWidth, element.ActualHeight);

        public static DependencyObject GetTemplatedParent(this FrameworkElement element) => 
            element.TemplatedParent;

        public static object GetToolTip(this FrameworkElement element) => 
            FrameworkElementHelper.GetToolTip(element);

        public static double GetTop(this FrameworkElement element) => 
            element.GetPosition().Y;

        public static Rect GetVisualBounds(this FrameworkElement element) => 
            element.GetVisualBounds(element.GetParent() as FrameworkElement, false);

        public static Rect GetVisualBounds(this FrameworkElement element, FrameworkElement relativeTo) => 
            element.GetVisualBounds(relativeTo, true);

        public static Rect GetVisualBounds(this FrameworkElement element, FrameworkElement relativeTo, bool checkParentBounds)
        {
            Rect rect = element.MapRect(new Rect(new Point(0.0, 0.0), element.GetVisualSize()), relativeTo);
            if (checkParentBounds)
            {
                FrameworkElement element2 = VisualTreeHelper.GetParent(element).FindElementByTypeInParents<FrameworkElement>(null);
                if (element2 != null)
                {
                    rect.Intersect(element2.GetVisualBounds(relativeTo));
                }
            }
            return rect;
        }

        public static FrameworkElement GetVisualParent(this FrameworkElement element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            while ((parent != null) && !(parent is FrameworkElement))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if ((parent == null) && (element.Parent is Popup))
            {
                parent = (FrameworkElement) element.Parent;
            }
            return ((parent != null) ? ((FrameworkElement) parent) : null);
        }

        public static Size GetVisualSize(this FrameworkElement element)
        {
            Size size = LayoutInformation.GetLayoutSlot(element).Size();
            SizeHelper.Deflate(ref size, element.Margin);
            Size size2 = element.GetSize();
            size2.Width = Math.Min(size2.Width, size.Width);
            size2.Height = Math.Min(size2.Height, size.Height);
            return size2;
        }

        public static int GetZIndex(this FrameworkElement element) => 
            Panel.GetZIndex(element);

        public static void InvalidateMeasureEx(this UIElement obj)
        {
            IFrameworkElement element = obj as IFrameworkElement;
            if (element != null)
            {
                element.InvalidateMeasureEx();
            }
        }

        public static bool IsInVisualTree(this FrameworkElement element) => 
            !IsFullyTrusted ? ReferenceEquals(element.GetRootParent(), Application.Current.MainWindow) : (PresentationSource.FromVisual(element) != null);

        public static bool IsLoaded(this FrameworkElement element) => 
            LayoutHelper.IsElementLoaded(element);

        public static bool IsReallyVisible(this FrameworkElement obj) => 
            obj.GetVisible() && ((obj.Width != 0.0) && !(obj.Height == 0.0));

        public static bool IsVisible(this FrameworkElement element) => 
            element.IsVisible;

        public static void MeasureEx(this UIElement obj, Size availableSize)
        {
            obj.Measure(availableSize);
        }

        public static void RemoveFromVisualTree(this FrameworkElement obj)
        {
            DependencyObject parent = obj.GetParent();
            if (parent != null)
            {
                ContentPresenter presenter = parent as ContentPresenter;
                if (presenter != null)
                {
                    presenter.RemoveFromVisualTree();
                }
                ContentControl control = parent as ContentControl;
                if (control != null)
                {
                    control.Content = null;
                }
                Panel panel = parent as Panel;
                if (panel != null)
                {
                    panel.Children.Remove(obj);
                }
            }
        }

        public static void SetBounds(this FrameworkElement element, Rect bounds)
        {
            if (!bounds.IsEmpty)
            {
                element.SetLeft(bounds.Left);
                element.SetTop(bounds.Top);
            }
            element.Width = bounds.Width;
            element.Height = bounds.Height;
        }

        public static void SetIsClipped(this FrameworkElement element, bool value)
        {
            FrameworkElementHelper.SetIsClipped(element, value);
        }

        public static void SetLeft(this FrameworkElement element, double value)
        {
            Canvas.SetLeft(element, value);
        }

        public static void SetParent(this FrameworkElement element, DependencyObject value)
        {
            if (!ReferenceEquals(element.Parent, value))
            {
                DependencyObject parent = element.Parent;
                if (parent is Panel)
                {
                    ((Panel) parent).Children.Remove(element);
                }
                else if (parent is Border)
                {
                    ((Border) parent).Child = null;
                }
                if (value is Panel)
                {
                    ((Panel) value).Children.Add(element);
                }
                else if (value is Border)
                {
                    ((Border) value).Child = element;
                }
            }
        }

        public static void SetSize(this FrameworkElement element, Size value)
        {
            element.Width = value.Width;
            element.Height = value.Height;
        }

        public static void SetToolTip(this FrameworkElement element, object toolTip)
        {
            FrameworkElementHelper.SetToolTip(element, toolTip);
        }

        public static void SetTop(this FrameworkElement element, double value)
        {
            Canvas.SetTop(element, value);
        }

        public static void SetZIndex(this FrameworkElement element, int value)
        {
            Panel.SetZIndex(element, value);
        }

        private static bool IsFullyTrusted =>
            AppDomain.CurrentDomain.IsFullyTrusted;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FrameworkElementExtensions.<>c <>9 = new FrameworkElementExtensions.<>c();
            public static HitTestResultCallback <>9__38_1;
            public static HitTestResultCallback <>9__39_1;

            internal HitTestResultBehavior <Contains>b__38_1(HitTestResult hitTestResult) => 
                HitTestResultBehavior.Stop;

            internal HitTestResultBehavior <FindElement>b__39_1(HitTestResult hitTestResult) => 
                HitTestResultBehavior.Continue;
        }
    }
}

