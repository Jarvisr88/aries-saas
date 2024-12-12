namespace DevExpress.Xpf.Grid.Hierarchy
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class HierarchyPanel : Panel
    {
        internal Dictionary<IItem, object> cache = new Dictionary<IItem, object>();
        public static readonly DependencyProperty ItemsContainerProperty;
        public static readonly DependencyProperty OrientationProperty;
        private static readonly DependencyPropertyKey IsItemVisiblePropertyKey;
        public static readonly DependencyProperty IsItemVisibleProperty;
        public static readonly DependencyProperty FixedElementsProperty;
        private static readonly DependencyPropertyKey FixedElementsPropertyKey;
        public static readonly DependencyProperty DataPresenterProperty;
        private List<FrameworkElement> fixedElements;
        private Dictionary<int, List<IItem>> hierarchy;

        static HierarchyPanel()
        {
            ItemsContainerProperty = DependencyPropertyManager.Register("ItemsContainer", typeof(IRootItemsContainer), typeof(HierarchyPanel), new PropertyMetadata(null, (d, e) => ((HierarchyPanel) d).OnItemsContainerChanged((IRootItemsContainer) e.OldValue)));
            OrientationProperty = DependencyPropertyManager.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(HierarchyPanel), new PropertyMetadata(System.Windows.Controls.Orientation.Vertical, (d, e) => ((HierarchyPanel) d).OnOrientationChanged()));
            IsItemVisiblePropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("IsItemVisible", typeof(bool), typeof(HierarchyPanel), new FrameworkPropertyMetadata(true));
            IsItemVisibleProperty = IsItemVisiblePropertyKey.DependencyProperty;
            FixedElementsPropertyKey = DependencyPropertyManager.RegisterReadOnly("FixedElements", typeof(IList<FrameworkElement>), typeof(HierarchyPanel), new FrameworkPropertyMetadata(null));
            FixedElementsProperty = FixedElementsPropertyKey.DependencyProperty;
            DataPresenterProperty = DependencyPropertyManager.Register("DataPresenter", typeof(DataPresenterBase), typeof(HierarchyPanel), new PropertyMetadata(null, (d, e) => ((HierarchyPanel) d).OnDataPresenterChanged((DataPresenterBase) e.OldValue)));
        }

        private void AddToHierarchy(IItem item, int level)
        {
            List<IItem> list = null;
            if (!this.hierarchy.TryGetValue(level, out list))
            {
                list = new List<IItem>();
                this.hierarchy[level] = list;
            }
            list.Add(item);
        }

        protected unsafe void ArrangeItem(IItem item, Rect rect, bool isVisible, int level)
        {
            if (item.IsFixedItem)
            {
                this.fixedElements.Add(item.Element);
            }
            double scrollElementOffset = GetScrollElementOffset(item, ReferenceEquals(item, this.ItemsContainer.ScrollItem) ? this.ItemsContainer.ScrollItemOffset : 0.0);
            if (scrollElementOffset != 0.0)
            {
                RectangleGeometry geometry1 = new RectangleGeometry();
                geometry1.Rect = new Rect(new Point(0.0, -scrollElementOffset), rect.Size());
                item.Element.Clip = geometry1;
            }
            else if (item.Element.Clip != null)
            {
                item.Element.Clip = null;
            }
            Rect* rectPtr1 = &rect;
            rectPtr1.Y += scrollElementOffset;
            Rect* rectPtr2 = &rect;
            rectPtr2.Height -= scrollElementOffset;
            SetIsItemVisible((item as DependencyObject) ?? item.Element, isVisible);
            this.AddToHierarchy(item, level);
            if (item.AdditionalElement == null)
            {
                item.Element.Arrange(rect);
            }
            else
            {
                double gridAreaWidth = item.GridAreaWidth;
                item.Element.Arrange(new Rect(rect.X, rect.Y, Math.Min(rect.Width, Math.Max(0.0, gridAreaWidth)), rect.Height));
                item.AdditionalElement.Arrange(new Rect(Math.Max((double) 0.0, (double) (gridAreaWidth + item.AdditionalElementOffset)), rect.Y, Math.Min(rect.Width, item.AdditionalElementWidth), rect.Height));
            }
            Func<DataPresenterBase, DataViewBase> evaluator = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Func<DataPresenterBase, DataViewBase> local2 = <>c.<>9__48_0;
                evaluator = <>c.<>9__48_0 = x => x.View;
            }
            this.DataPresenter.With<DataPresenterBase, DataViewBase>(evaluator).Do<DataViewBase>(x => x.OnArrangeRow(item, rect.Y, rect.Height));
        }

        protected virtual Size ArrangeItemsContainer(double offset, Size availableSize, IItemsContainer itemsContainer, int level)
        {
            double num = 0.0;
            double defineSize = (itemsContainer != null) ? (this.SizeHelper.GetDefineSize(itemsContainer.RenderSize) * itemsContainer.AnimationProgress) : 0.0;
            IList<IItem> sortedChildrenElements = this.GetSortedChildrenElements(itemsContainer);
            foreach (IItem item in sortedChildrenElements)
            {
                if (item.FixedRowPosition == FixedRowPosition.Top)
                {
                    this.FixedTopRowsHeight += this.SizeHelper.GetDefineSize(item.Element.DesiredSize);
                }
                if (item.FixedRowPosition == FixedRowPosition.Bottom)
                {
                    this.FixedBottomRowsHeight += this.SizeHelper.GetDefineSize(item.Element.DesiredSize);
                }
            }
            double definePoint = this.SizeHelper.GetDefineSize(this.GetAvailableSize(availableSize)) - this.FixedBottomRowsHeight;
            foreach (IItem item2 in sortedChildrenElements)
            {
                double num4 = this.SizeHelper.GetDefineSize(item2.Element.DesiredSize);
                if (item2.AdditionalElement != null)
                {
                    num4 = Math.Max(num4, this.SizeHelper.GetDefineSize(item2.AdditionalElement.DesiredSize));
                }
                double num5 = Math.Max((double) 0.0, (double) (num4 + this.GetItemOffset(item2)));
                if (item2.FixedRowPosition == FixedRowPosition.Bottom)
                {
                    Rect rect = new Rect(this.SizeHelper.CreatePoint(definePoint, 0.0), this.SizeHelper.CreateSize(Math.Max(0.0, Math.Min(num5, defineSize - num)), this.SizeHelper.GetSecondarySize(availableSize)));
                    this.ArrangeItem(item2, rect, (defineSize - num) > 0.0, level);
                    definePoint += num5;
                }
                else
                {
                    double num6 = -num;
                    num6 = (!this.HasFixedRows || this.HasCellMerging) ? (num6 + defineSize) : (num6 + (this.SizeHelper.GetDefineSize(this.GetAvailableSize(availableSize)) - this.FixedBottomRowsHeight));
                    Rect rect = new Rect(this.SizeHelper.CreatePoint(num + offset, 0.0), this.SizeHelper.CreateSize(Math.Max(0.0, Math.Min(num5, Math.Min(num6, this.SizeHelper.GetDefineSize(availableSize)))), this.SizeHelper.GetSecondarySize(availableSize)));
                    this.ArrangeItem(item2, rect, (defineSize - num) > 0.0, level);
                    num += num5;
                    num += this.SizeHelper.GetDefineSize(this.ArrangeItemsContainer(num + offset, this.SizeHelper.CreateSize(Math.Max((double) 0.0, (double) (this.SizeHelper.GetDefineSize(availableSize) - num)), this.SizeHelper.GetSecondarySize(availableSize)), item2.ItemsContainer, level + 1));
                }
            }
            return this.SizeHelper.CreateSize(defineSize, this.SizeHelper.GetSecondarySize(availableSize));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Func<DataPresenterBase, DataViewBase> evaluator = <>c.<>9__46_0;
            if (<>c.<>9__46_0 == null)
            {
                Func<DataPresenterBase, DataViewBase> local1 = <>c.<>9__46_0;
                evaluator = <>c.<>9__46_0 = x => x.View;
            }
            Action<DataViewBase> action = <>c.<>9__46_1;
            if (<>c.<>9__46_1 == null)
            {
                Action<DataViewBase> local2 = <>c.<>9__46_1;
                action = <>c.<>9__46_1 = delegate (DataViewBase x) {
                    x.OnBeginArrangeRows();
                };
            }
            this.DataPresenter.With<DataPresenterBase, DataViewBase>(evaluator).Do<DataViewBase>(action);
            this.fixedElements = new List<FrameworkElement>();
            this.hierarchy = new Dictionary<int, List<IItem>>();
            this.FixedTopRowsHeight = 0.0;
            this.FixedBottomRowsHeight = 0.0;
            this.ArrangeItemsContainer(0.0, finalSize, this.ItemsContainer, 0);
            this.FixedElements = this.fixedElements;
            this.AssignZIndex();
            Func<DataPresenterBase, DataViewBase> func2 = <>c.<>9__46_2;
            if (<>c.<>9__46_2 == null)
            {
                Func<DataPresenterBase, DataViewBase> local3 = <>c.<>9__46_2;
                func2 = <>c.<>9__46_2 = x => x.View;
            }
            Action<DataViewBase> action2 = <>c.<>9__46_3;
            if (<>c.<>9__46_3 == null)
            {
                Action<DataViewBase> local4 = <>c.<>9__46_3;
                action2 = <>c.<>9__46_3 = delegate (DataViewBase x) {
                    x.OnEndArrangeRows();
                };
            }
            this.DataPresenter.With<DataPresenterBase, DataViewBase>(func2).Do<DataViewBase>(action2);
            return finalSize;
        }

        private void AssignZIndex()
        {
            int num = 0;
            Func<int, int> keySelector = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Func<int, int> local1 = <>c.<>9__37_0;
                keySelector = <>c.<>9__37_0 = x => x;
            }
            foreach (int num2 in this.hierarchy.Keys.OrderByDescending<int, int>(keySelector))
            {
                List<IItem> source = this.hierarchy[num2];
                if (this.HasFixedRows)
                {
                    Func<IItem, FixedRowPosition> func1 = <>c.<>9__37_1;
                    if (<>c.<>9__37_1 == null)
                    {
                        Func<IItem, FixedRowPosition> local2 = <>c.<>9__37_1;
                        func1 = <>c.<>9__37_1 = x => x.FixedRowPosition;
                    }
                    source = source.OrderBy<IItem, FixedRowPosition>(func1).ToList<IItem>();
                }
                foreach (IItem item in source)
                {
                    SetZIndex(item.Element, num++);
                    if (item.AdditionalElement != null)
                    {
                        SetZIndex(item.AdditionalElement, num++);
                    }
                }
            }
        }

        internal void CalcViewport(Size availableSize)
        {
            this.FullyVisibleItemsCount = 0;
            this.CalcViewportCore(availableSize);
            if (this.Viewport == 0.0)
            {
                this.Viewport = 1.0;
            }
        }

        private double CalcViewport(Size availableSize, IItemsContainer itemsContainer)
        {
            double defineSize = this.SizeHelper.GetDefineSize(availableSize);
            foreach (IItem item in this.GetSortedChildrenElements(itemsContainer))
            {
                double num2 = this.SizeHelper.GetDefineSize(item.Element.DesiredSize);
                if (item.AdditionalElement != null)
                {
                    num2 = Math.Max(num2, this.SizeHelper.GetDefineSize(item.AdditionalElement.DesiredSize));
                }
                if (item.FixedRowPosition != FixedRowPosition.None)
                {
                    defineSize -= num2;
                }
                else
                {
                    double itemOffset = this.GetItemOffset(item);
                    double num4 = Math.Min(num2 + itemOffset, Math.Max(0.0, defineSize)) / num2;
                    defineSize -= num2 + itemOffset;
                    if (num2 != 0.0)
                    {
                        if ((num4 == 1.0) || this.AllowPerPixelScrolling)
                        {
                            this.Viewport += num4;
                        }
                        if (num4 == 1.0)
                        {
                            int fullyVisibleItemsCount = this.FullyVisibleItemsCount;
                            this.FullyVisibleItemsCount = fullyVisibleItemsCount + 1;
                        }
                    }
                    defineSize = this.CalcViewport(this.SizeHelper.CreateSize(Math.Max(0.0, defineSize), this.SizeHelper.GetSecondarySize(availableSize)), item.ItemsContainer);
                }
            }
            return defineSize;
        }

        protected virtual void CalcViewportCore(Size availableSize)
        {
            this.Viewport = 0.0;
            this.CalcViewport(availableSize, this.ItemsContainer);
        }

        private void ClearFixedElements()
        {
            this.FixedElements = null;
        }

        internal static void DetachItem(IItem item)
        {
            HierarchyPanel parent = (HierarchyPanel) VisualTreeHelper.GetParent(item.Element);
            if (parent != null)
            {
                parent.Children.Remove(item.Element);
                if (item.AdditionalElement != null)
                {
                    parent.Children.Remove(item.AdditionalElement);
                }
                parent.cache.Remove(item);
                parent.ClearFixedElements();
            }
        }

        private Size GetAvailableSize(Size availableSize) => 
            (this.DataPresenter == null) ? availableSize : this.DataPresenterCore.LastConstraint;

        public static bool GetIsItemVisible(DependencyObject element) => 
            (bool) element.GetValue(IsItemVisibleProperty);

        private double GetItemOffset(IItem item) => 
            GetScrollElementOffset(item, ReferenceEquals(item, this.ItemsContainer.ScrollItem) ? this.ItemsContainer.ScrollItemOffset : 0.0);

        internal static double GetScrollElementOffset(IItem item, double scrollOffset)
        {
            double height = item.Element.DesiredSize.Height;
            if (item.AdditionalElement != null)
            {
                height = Math.Max(height, item.AdditionalElement.DesiredSize.Height);
            }
            return -DXArranger.Round(height * scrollOffset, 0);
        }

        protected IList<IItem> GetSortedChildrenElements(IItemsContainer itemsContainer)
        {
            if (itemsContainer == null)
            {
                return new IItem[0];
            }
            Func<IItem, IItem> cast = <>c.<>9__66_0;
            if (<>c.<>9__66_0 == null)
            {
                Func<IItem, IItem> local1 = <>c.<>9__66_0;
                cast = <>c.<>9__66_0 = item => item;
            }
            return OrderPanelBase.GetSortedElements<IItem>(new SimpleBridgeList<IItem, IItem>(itemsContainer.Items, cast, null), true, <>c.<>9__66_1 ??= item => item.VisibleIndex);
        }

        private void ItemsContainer_HierarchyChanged(object sender, HierarchyChangedEventArgs e)
        {
            if (e.ChangeType == HierarchyChangeType.ItemRemoved)
            {
                e.Item.Element.Visibility = Visibility.Collapsed;
            }
            base.InvalidateMeasure();
        }

        protected virtual Size MeasureItemsContainer(Size availableSize, IItemsContainer itemsContainer)
        {
            availableSize = this.SizeHelper.CreateSize(double.PositiveInfinity, this.SizeHelper.GetSecondarySize(availableSize));
            double defineSize = 0.0;
            double num2 = 0.0;
            foreach (IItem item in this.GetSortedChildrenElements(itemsContainer))
            {
                item.Element.Measure(new Size(Math.Max((double) 0.0, (double) ((availableSize.Width - item.AdditionalElementWidth) - item.AdditionalElementOffset)), availableSize.Height));
                double num3 = this.SizeHelper.GetDefineSize(item.Element.DesiredSize);
                if (item.AdditionalElement != null)
                {
                    item.AdditionalElement.Measure(new Size(Math.Min(availableSize.Width, item.AdditionalElementWidth), availableSize.Height));
                    num3 = Math.Max(num3, this.SizeHelper.GetDefineSize(item.AdditionalElement.DesiredSize));
                }
                defineSize += Math.Max((double) 0.0, (double) (num3 + this.GetItemOffset(item)));
                item.ItemsContainer.DesiredSize = this.MeasureItemsContainer(this.SizeHelper.CreateSize(this.SizeHelper.GetDefineSize(availableSize) - defineSize, this.SizeHelper.GetSecondarySize(availableSize)), item.ItemsContainer);
                defineSize += this.SizeHelper.GetDefineSize(item.ItemsContainer.DesiredSize);
                num2 = Math.Max(Math.Max(num2, Math.Min(this.SizeHelper.GetSecondarySize(availableSize), (this.SizeHelper.GetSecondarySize(item.Element.DesiredSize) + item.AdditionalElementWidth) + item.AdditionalElementOffset)), this.SizeHelper.GetSecondarySize(item.ItemsContainer.DesiredSize));
            }
            if (itemsContainer == null)
            {
                return new Size();
            }
            itemsContainer.RenderSize = this.SizeHelper.CreateSize(defineSize, num2);
            return this.SizeHelper.CreateSize(defineSize * itemsContainer.AnimationProgress, num2);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            this.ValidateTree(this.ItemsContainer);
            Size size = DXArranger.Ceiling(this.MeasureItemsContainer(availableSize, this.ItemsContainer));
            this.CalcViewport(this.GetAvailableSize(availableSize));
            return size;
        }

        private void OnDataPresenterChanged(DataPresenterBase oldValue)
        {
            this.DataPresenterCore = this.DataPresenter;
            if (oldValue != null)
            {
                oldValue.Panel = null;
            }
            if (this.DataPresenterCore != null)
            {
                this.DataPresenterCore.Panel = this;
            }
        }

        private void OnItemsContainerChanged(IRootItemsContainer oldValue)
        {
            if (oldValue != null)
            {
                oldValue.HierarchyChanged -= new HierarchyChangedEventHandler(this.ItemsContainer_HierarchyChanged);
            }
            if (this.ItemsContainer != null)
            {
                this.ItemsContainer.HierarchyChanged += new HierarchyChangedEventHandler(this.ItemsContainer_HierarchyChanged);
            }
            base.Children.Clear();
            this.cache.Clear();
            this.ClearFixedElements();
            base.InvalidateMeasure();
        }

        private void OnOrientationChanged()
        {
            base.InvalidateMeasure();
        }

        private static void SetIsItemVisible(DependencyObject element, bool value)
        {
            element.SetValue(IsItemVisiblePropertyKey, value);
        }

        protected void ValidateTree(IItemsContainer itemsContainer)
        {
            if (itemsContainer != null)
            {
                foreach (IItem item in itemsContainer.Items)
                {
                    if (!this.cache.ContainsKey(item))
                    {
                        DetachItem(item);
                        base.Children.Add(item.Element);
                        if (item.AdditionalElement != null)
                        {
                            base.Children.Add(item.AdditionalElement);
                        }
                        this.cache.Add(item, null);
                    }
                    OrderPanelBase.UpdateChildVisibility(item.Element, true, item.VisibleIndex);
                    if (item.AdditionalElement != null)
                    {
                        OrderPanelBase.UpdateChildVisibility(item.AdditionalElement, true, item.VisibleIndex);
                    }
                    this.ValidateTree(item.ItemsContainer);
                }
            }
        }

        public IRootItemsContainer ItemsContainer
        {
            get => 
                (IRootItemsContainer) base.GetValue(ItemsContainerProperty);
            set => 
                base.SetValue(ItemsContainerProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public IList<FrameworkElement> FixedElements
        {
            get => 
                (IList<FrameworkElement>) base.GetValue(FixedElementsProperty);
            private set => 
                base.SetValue(FixedElementsPropertyKey, value);
        }

        private DataPresenterBase DataPresenterCore { get; set; }

        public DataPresenterBase DataPresenter
        {
            get => 
                (DataPresenterBase) base.GetValue(DataPresenterProperty);
            set => 
                base.SetValue(DataPresenterProperty, value);
        }

        protected SizeHelperBase SizeHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.Orientation);

        protected bool HasFixedRows =>
            (this.DataPresenterCore != null) && ((this.DataPresenterCore.View != null) && this.DataPresenterCore.View.HasFixedRows);

        protected bool HasCellMerging =>
            (this.DataPresenterCore != null) && ((this.DataPresenterCore.View != null) && this.DataPresenterCore.View.ActualAllowCellMerge);

        public double FixedTopRowsHeight { get; private set; }

        public double FixedBottomRowsHeight { get; private set; }

        internal double Viewport { get; set; }

        internal int FullyVisibleItemsCount { get; set; }

        protected bool AllowPerPixelScrolling =>
            (this.DataPresenterCore == null) || ((this.DataPresenterCore.View == null) || this.DataPresenterCore.View.ViewBehavior.AllowPerPixelScrolling);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HierarchyPanel.<>c <>9 = new HierarchyPanel.<>c();
            public static Func<int, int> <>9__37_0;
            public static Func<IItem, FixedRowPosition> <>9__37_1;
            public static Func<DataPresenterBase, DataViewBase> <>9__46_0;
            public static Action<DataViewBase> <>9__46_1;
            public static Func<DataPresenterBase, DataViewBase> <>9__46_2;
            public static Action<DataViewBase> <>9__46_3;
            public static Func<DataPresenterBase, DataViewBase> <>9__48_0;
            public static Func<IItem, IItem> <>9__66_0;
            public static Func<IItem, int> <>9__66_1;

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((HierarchyPanel) d).OnItemsContainerChanged((IRootItemsContainer) e.OldValue);
            }

            internal void <.cctor>b__9_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((HierarchyPanel) d).OnOrientationChanged();
            }

            internal void <.cctor>b__9_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((HierarchyPanel) d).OnDataPresenterChanged((DataPresenterBase) e.OldValue);
            }

            internal DataViewBase <ArrangeItem>b__48_0(DataPresenterBase x) => 
                x.View;

            internal DataViewBase <ArrangeOverride>b__46_0(DataPresenterBase x) => 
                x.View;

            internal void <ArrangeOverride>b__46_1(DataViewBase x)
            {
                x.OnBeginArrangeRows();
            }

            internal DataViewBase <ArrangeOverride>b__46_2(DataPresenterBase x) => 
                x.View;

            internal void <ArrangeOverride>b__46_3(DataViewBase x)
            {
                x.OnEndArrangeRows();
            }

            internal int <AssignZIndex>b__37_0(int x) => 
                x;

            internal FixedRowPosition <AssignZIndex>b__37_1(IItem x) => 
                x.FixedRowPosition;

            internal IItem <GetSortedChildrenElements>b__66_0(IItem item) => 
                item;

            internal int <GetSortedChildrenElements>b__66_1(IItem item) => 
                item.VisibleIndex;
        }
    }
}

