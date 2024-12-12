namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class OrderPanelBase : Panel
    {
        public const int InvisibleIndex = -1;
        public const System.Windows.Controls.Orientation DefaultOrientation = System.Windows.Controls.Orientation.Horizontal;
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(OrderPanelBase), new FrameworkPropertyMetadata(System.Windows.Controls.Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure, null, null));
        public static readonly DependencyProperty VisibleIndexProperty = DependencyProperty.RegisterAttached("VisibleIndex", typeof(int), typeof(OrderPanelBase), new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.AffectsParentMeasure));
        public static readonly DependencyProperty ArrangeAccordingToVisibleIndexProperty = DependencyProperty.Register("ArrangeAccordingToVisibleIndex", typeof(bool), typeof(OrderPanelBase), new PropertyMetadata(false));
        public static readonly DependencyProperty PanelProperty = DependencyProperty.RegisterAttached("Panel", typeof(Panel), typeof(OrderPanelBase), new PropertyMetadata(null));

        protected OrderPanelBase()
        {
        }

        protected sealed override Size ArrangeOverride(Size finalSize) => 
            this.ArrangeSortedChildrenOverride(finalSize, GetSortedElements(base.Children, this.ArrangeAccordingToVisibleIndex));

        protected abstract Size ArrangeSortedChildrenOverride(Size finalSize, IList<UIElement> sortedChildren);
        public static Panel GetPanel(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (Panel) element.GetValue(PanelProperty);
        }

        public IList<UIElement> GetSortedChildren() => 
            GetSortedElements(base.Children, this.ArrangeAccordingToVisibleIndex);

        public static IList<UIElement> GetSortedElements(ICollection uiElements, bool arrangeAccordingToVisibleIndex)
        {
            Func<UIElement, int> getVisibleIndexFunc = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<UIElement, int> local1 = <>c.<>9__17_0;
                getVisibleIndexFunc = <>c.<>9__17_0 = item => GetVisibleIndex(item);
            }
            return GetSortedElements<UIElement>(uiElements, arrangeAccordingToVisibleIndex, getVisibleIndexFunc);
        }

        public static IList<T> GetSortedElements<T>(ICollection elements, bool arrangeAccordingToVisibleIndex, Func<T, int> getVisibleIndexFunc)
        {
            List<T> list = new List<T>();
            foreach (T local in elements)
            {
                if (!arrangeAccordingToVisibleIndex || !IsInvisibleIndex(getVisibleIndexFunc(local)))
                {
                    list.Add(local);
                }
            }
            if (arrangeAccordingToVisibleIndex)
            {
                list.Sort(new VisibleIndexComparer<T>(getVisibleIndexFunc));
            }
            return list;
        }

        public static int GetVisibleIndex(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            IOrderPanelElement element2 = element as IOrderPanelElement;
            return ((element2 == null) ? ((int) element.GetValue(VisibleIndexProperty)) : element2.VisibleIndex);
        }

        public static bool IsInvisibleIndex(int index) => 
            index == -1;

        protected sealed override Size MeasureOverride(Size availableSize)
        {
            UpdateChildrenVisibility(base.Children, this.ArrangeAccordingToVisibleIndex);
            return this.MeasureSortedChildrenOverride(availableSize, GetSortedElements(base.Children, this.ArrangeAccordingToVisibleIndex));
        }

        protected abstract Size MeasureSortedChildrenOverride(Size availableSize, IList<UIElement> sortedChildren);
        protected override void OnIsItemsHostChanged(bool oldIsItemsHost, bool newIsItemsHost)
        {
            base.OnIsItemsHostChanged(oldIsItemsHost, newIsItemsHost);
            ItemsControl itemsOwner = ItemsControl.GetItemsOwner(this);
            if (itemsOwner != null)
            {
                SetPanel(itemsOwner, this);
            }
        }

        private static void SetOrderPanelElementVisibleIndex(IOrderPanelElement element, int index)
        {
            int visibleIndex = element.VisibleIndex;
            element.VisibleIndex = index;
            if (visibleIndex != index)
            {
                FrameworkElement parent = LayoutHelper.GetParent((DependencyObject) element, false) as FrameworkElement;
                if (parent != null)
                {
                    parent.InvalidateMeasure();
                }
            }
        }

        public static void SetPanel(DependencyObject element, Panel panel)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(PanelProperty, panel);
        }

        public static void SetVisibleIndex(DependencyObject element, int index)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            IOrderPanelElement element2 = element as IOrderPanelElement;
            if (element2 != null)
            {
                SetOrderPanelElementVisibleIndex(element2, index);
            }
            else
            {
                element.SetValue(VisibleIndexProperty, index);
            }
        }

        private static void UpdateChildrenVisibility(ICollection children, bool arrangeAccordingToVisibleIndex)
        {
            foreach (UIElement element in children)
            {
                UpdateChildVisibility(element, arrangeAccordingToVisibleIndex, GetVisibleIndex(element));
            }
        }

        public static void UpdateChildVisibility(UIElement child, bool arrangeAccordingToVisibleIndex, int visibleIndex)
        {
            Visibility visibility = child.Visibility;
            Visibility visibility2 = ((visibleIndex >= 0) || !arrangeAccordingToVisibleIndex) ? Visibility.Visible : Visibility.Collapsed;
            child.Visibility = visibility2;
            if ((visibility == Visibility.Collapsed) && (visibility2 == Visibility.Visible))
            {
                child.InvalidateMeasure();
            }
        }

        public bool ArrangeAccordingToVisibleIndex
        {
            get => 
                (bool) base.GetValue(ArrangeAccordingToVisibleIndexProperty);
            set => 
                base.SetValue(ArrangeAccordingToVisibleIndexProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        protected SizeHelperBase SizeHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.Orientation);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OrderPanelBase.<>c <>9 = new OrderPanelBase.<>c();
            public static Func<UIElement, int> <>9__17_0;

            internal int <GetSortedElements>b__17_0(UIElement item) => 
                OrderPanelBase.GetVisibleIndex(item);
        }

        private class VisibleIndexComparer<T> : IComparer<T>
        {
            private readonly Func<T, int> getVisibleIndexFunc;

            public VisibleIndexComparer(Func<T, int> getVisibleIndexFunc)
            {
                this.getVisibleIndexFunc = getVisibleIndexFunc;
            }

            public int Compare(T x, T y)
            {
                int index = this.getVisibleIndexFunc(x);
                int num2 = this.getVisibleIndexFunc(y);
                if (OrderPanelBase.IsInvisibleIndex(index) || OrderPanelBase.IsInvisibleIndex(num2))
                {
                    throw new NotImplementedException();
                }
                return (index - num2);
            }
        }
    }
}

