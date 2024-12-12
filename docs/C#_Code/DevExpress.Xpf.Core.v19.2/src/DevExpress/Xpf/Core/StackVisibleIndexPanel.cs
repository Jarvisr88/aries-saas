namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class StackVisibleIndexPanel : OrderPanelBase
    {
        public static Size ArrangeElements(Size finalSize, IList<UIElement> elements, SizeHelperBase sizeHelper)
        {
            Point location = new Point();
            Func<UIElement, Size> getDesiredSizeFunc = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<UIElement, Size> local1 = <>c.<>9__0_0;
                getDesiredSizeFunc = <>c.<>9__0_0 = child => child.DesiredSize;
            }
            return ArrangeElements<UIElement>(new Rect(location, finalSize), elements, sizeHelper, getDesiredSizeFunc, <>c.<>9__0_1 ??= (panelSize, childSize) => childSize, <>c.<>9__0_2 ??= delegate (UIElement child, Rect rect, bool isVisible) {
                child.Arrange(rect);
            });
        }

        public static Size ArrangeElements<T>(Rect finalRect, IList<T> elements, SizeHelperBase sizeHelper, Func<T, Size> getDesiredSizeFunc, Func<double, double, double> getActualSize, Action<T, Rect, bool> arrangeAction)
        {
            double defineSize = sizeHelper.GetDefineSize(finalRect.Size());
            Size size = finalRect.Size();
            double num2 = 0.0;
            foreach (T local in elements)
            {
                Point point = sizeHelper.CreatePoint(sizeHelper.GetDefinePoint(finalRect.Location()) + num2, sizeHelper.GetSecondaryPoint(finalRect.Location()));
                finalRect.X = point.X;
                finalRect.Y = point.Y;
                Size size2 = getDesiredSizeFunc(local);
                num2 = sizeHelper.GetDefineSize(size2);
                Size size3 = sizeHelper.CreateSize(num2, Math.Max(sizeHelper.GetSecondarySize(finalRect.Size()), sizeHelper.GetSecondarySize(size2)));
                RectHelper.SetSize(ref finalRect, sizeHelper.CreateSize(getActualSize(defineSize, sizeHelper.GetDefineSize(size3)), sizeHelper.GetSecondarySize(size3)));
                arrangeAction(local, finalRect, defineSize > 0.0);
                defineSize -= sizeHelper.GetDefineSize(finalRect.Size());
            }
            return size;
        }

        protected override Size ArrangeSortedChildrenOverride(Size finalSize, IList<UIElement> sortedChildren) => 
            ArrangeElements(finalSize, sortedChildren, base.SizeHelper);

        public static Size MeasureElements(Size availableSize, IList<UIElement> elements, SizeHelperBase sizeHelper)
        {
            Func<Size, UIElement, Size> measureFunc = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<Size, UIElement, Size> local1 = <>c.<>9__3_0;
                measureFunc = <>c.<>9__3_0 = delegate (Size size, UIElement child) {
                    child.Measure(size);
                    return child.DesiredSize;
                };
            }
            return MeasureElements<UIElement>(availableSize, elements, sizeHelper, measureFunc);
        }

        public static Size MeasureElements<T>(Size availableSize, IList<T> elements, SizeHelperBase sizeHelper, Func<Size, T, Size> measureFunc)
        {
            availableSize = sizeHelper.CreateSize(double.PositiveInfinity, sizeHelper.GetSecondarySize(availableSize));
            double defineSize = 0.0;
            double num2 = 0.0;
            foreach (T local in elements)
            {
                Size size = measureFunc(availableSize, local);
                num2 = Math.Max(num2, sizeHelper.GetSecondarySize(size));
                defineSize += sizeHelper.GetDefineSize(size);
            }
            return sizeHelper.CreateSize(defineSize, num2);
        }

        protected override Size MeasureSortedChildrenOverride(Size availableSize, IList<UIElement> sortedChildren) => 
            MeasureElements(availableSize, sortedChildren, base.SizeHelper);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StackVisibleIndexPanel.<>c <>9 = new StackVisibleIndexPanel.<>c();
            public static Func<UIElement, Size> <>9__0_0;
            public static Func<double, double, double> <>9__0_1;
            public static Action<UIElement, Rect, bool> <>9__0_2;
            public static Func<Size, UIElement, Size> <>9__3_0;

            internal Size <ArrangeElements>b__0_0(UIElement child) => 
                child.DesiredSize;

            internal double <ArrangeElements>b__0_1(double panelSize, double childSize) => 
                childSize;

            internal void <ArrangeElements>b__0_2(UIElement child, Rect rect, bool isVisible)
            {
                child.Arrange(rect);
            }

            internal Size <MeasureElements>b__3_0(Size size, UIElement child)
            {
                child.Measure(size);
                return child.DesiredSize;
            }
        }
    }
}

