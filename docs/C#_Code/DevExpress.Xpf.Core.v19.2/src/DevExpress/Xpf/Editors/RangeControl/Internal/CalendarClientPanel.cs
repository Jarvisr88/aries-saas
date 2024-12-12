namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.RangeControl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CalendarClientPanel : Panel
    {
        private const double MaxGroupItemWidth = 300.0;
        private const int LeftTextPadding = 15;
        private int freeItemIndex;
        private int freeGroupItemIndex;
        private ContentPresenter selectionMarker;

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (double.IsInfinity(finalSize.Height) || double.IsInfinity(finalSize.Width))
            {
                return base.ArrangeOverride(finalSize);
            }
            if (this.selectionMarker == null)
            {
                this.CreateSelectionMarker();
            }
            this.BeginRender();
            this.Render();
            this.FinishRender();
            return finalSize;
        }

        private void BeginRender()
        {
            this.IsSelectionMarkerVisible = false;
        }

        private Rect CalcTextRect(Rect rect)
        {
            if (rect.Width < 15.0)
            {
                return Rect.Empty;
            }
            RectHelper.Deflate(ref rect, new Thickness(15.0, 0.0, 0.0, 0.0));
            return rect;
        }

        private bool CanSelectFirstPartialItem(double currentComparable) => 
            (this.LayoutInfo.ComparableStart >= currentComparable) && (this.LayoutInfo.ComparableSelectionStart == this.LayoutInfo.ComparableStart);

        private bool CanSelectLastPartialItem(double nextComparable) => 
            (this.LayoutInfo.ComparableEnd <= nextComparable) && (this.LayoutInfo.ComparableSelectionEnd == this.LayoutInfo.ComparableEnd);

        internal void Clear()
        {
            this.ClearItems();
            this.ClearGroupItems();
            this.ClearIndexes();
        }

        private void ClearGroupItems()
        {
            this.GroupItems = null;
            base.Children.Clear();
        }

        private void ClearIndexes()
        {
            this.freeItemIndex = 0;
            this.freeGroupItemIndex = 0;
        }

        private void ClearItems()
        {
            this.Items = null;
            base.Children.Clear();
        }

        private double ComparableToRender(double comparable) => 
            this.ToNormalized(comparable) * base.ActualWidth;

        private bool ContainsPoint(double start, double end, double current) => 
            (current >= start) && (current <= end);

        private IMapping CreateClientMapping(double clientHeight) => 
            new PointMapping(new Point(0.0, 0.0), new Rect(0.0, 0.0, base.ActualWidth, clientHeight));

        private IMapping CreateGroupMapping(double groupHeight) => 
            new PointMapping(new Point(0.0, this.RenderBounds.Size.Height - groupHeight), new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight));

        private void CreateSelectionMarker()
        {
            this.selectionMarker = new ContentPresenter();
            this.selectionMarker.SetValue(Panel.ZIndexProperty, 1);
            this.selectionMarker.ContentTemplate = this.Owner.ZoomOutSelectionMarkerTemplate;
            base.Children.Add(this.selectionMarker);
        }

        protected virtual void FinishRender()
        {
            this.HideItems();
            this.HideIGrouptems();
            this.ClearIndexes();
        }

        private CalendarGroupItem GetGroupItem()
        {
            this.GroupItems ??= new List<CalendarGroupItem>();
            if (this.freeGroupItemIndex >= this.GroupItems.Count)
            {
                CalendarGroupItem item = new CalendarGroupItem();
                if (this.Owner.GroupItemStyle != null)
                {
                    item.Style = this.Owner.GroupItemStyle;
                }
                this.GroupItems.Add(item);
                base.Children.Add(item);
            }
            int freeGroupItemIndex = this.freeGroupItemIndex;
            this.freeGroupItemIndex = freeGroupItemIndex + 1;
            return this.GroupItems[freeGroupItemIndex];
        }

        private CalendarItem GetItem()
        {
            this.Items ??= new List<CalendarItem>();
            if (this.freeItemIndex >= this.Items.Count)
            {
                CalendarItem item = new CalendarItem();
                if (this.Owner.ItemStyle != null)
                {
                    item.Style = this.Owner.ItemStyle;
                }
                this.Items.Add(item);
                base.Children.Add(item);
            }
            int freeItemIndex = this.freeItemIndex;
            this.freeItemIndex = freeItemIndex + 1;
            return this.Items[freeItemIndex];
        }

        private Rect GetItemTextRect(Rect rect)
        {
            RectHelper.Inflate(ref rect, (double) 0.0, -8.0);
            return rect;
        }

        private void HideIGrouptems()
        {
            if (this.GroupItems != null)
            {
                for (int i = this.freeGroupItemIndex; i < this.GroupItems.Count; i++)
                {
                    this.GroupItems[i].Arrange(new Rect(-100.0, 0.0, 1.0, 1.0));
                }
            }
        }

        private void HideItems()
        {
            if (this.Items != null)
            {
                for (int i = this.freeItemIndex; i < this.Items.Count; i++)
                {
                    this.Items[i].Arrange(new Rect(-100.0, 0.0, 1.0, 1.0));
                }
            }
        }

        internal void Invalidate(DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo info, Rect bounds)
        {
            this.LayoutInfo = info;
            this.RenderBounds = bounds;
            base.InvalidateArrange();
        }

        private bool IsInSelectionRange(double currentComparable, double nextComparable) => 
            (this.ContainsPoint(this.LayoutInfo.ComparableSelectionStart, this.LayoutInfo.ComparableRenderVisibleEnd, currentComparable) || this.CanSelectFirstPartialItem(currentComparable)) ? (this.ContainsPoint(this.LayoutInfo.ComparableSelectionStart, this.LayoutInfo.ComparableSelectionEnd, nextComparable) || this.CanSelectLastPartialItem(nextComparable)) : false;

        private void PatchArrangeRect(ref Rect arrangeRect, bool isFirstItem, bool isLastItem)
        {
            if (isFirstItem && (this.LayoutInfo.ComparableVisibleStart == this.LayoutInfo.ComparableStart))
            {
                arrangeRect = new Rect(arrangeRect.Left - 1.0, arrangeRect.Top, arrangeRect.Width + 1.0, arrangeRect.Height);
            }
            else if (isLastItem && (this.LayoutInfo.ComparableVisibleEnd == this.LayoutInfo.ComparableEnd))
            {
                arrangeRect = new Rect(arrangeRect.Left, arrangeRect.Top, arrangeRect.Width + 1.0, arrangeRect.Height);
            }
        }

        internal virtual void Render()
        {
            if (base.ActualWidth >= 1.0)
            {
                this.RenderContent();
            }
        }

        private void RenderContent()
        {
            double groupingHeight = this.Owner.GetGroupingHeight();
            double clientHeight = this.RenderBounds.Size.Height - groupingHeight;
            if (this.Owner.AllowGrouping)
            {
                this.RenderGroupIntervals(this.CreateGroupMapping(groupingHeight));
            }
            this.RenderItemIntervals(this.CreateClientMapping(clientHeight));
        }

        protected void RenderGroupIntervals(IMapping pointMapping)
        {
            this.RenderRects(pointMapping, this.Owner.GroupIntervalFactory, new Func<Rect, Rect>(this.CalcTextRect), true);
        }

        private void RenderItemIntervals(IMapping pointMapping)
        {
            this.RenderItemIntervalsInternal(pointMapping);
        }

        protected void RenderItemIntervalsInternal(IMapping pointMapping)
        {
            this.RenderRects(pointMapping, this.Owner.ItemIntervalFactory, new Func<Rect, Rect>(this.GetItemTextRect), false);
        }

        private void RenderRects(IMapping mapping, IntervalFactory intervalFactory, Func<Rect, Rect> calcTextRect, bool isGroup)
        {
            double comparableRenderVisibleStart = this.LayoutInfo.ComparableRenderVisibleStart;
            object realValue = intervalFactory.Snap(this.Instance.GetRealValue(comparableRenderVisibleStart));
            double comparable = Math.Max(this.Instance.GetComparableValue(realValue), this.LayoutInfo.ComparableStart);
            object obj3 = this.Instance.GetRealValue(comparable);
            double comparableValue = this.Instance.GetComparableValue(obj3);
            double comparableRenderVisibleEnd = this.LayoutInfo.ComparableRenderVisibleEnd;
            object obj4 = intervalFactory.Snap(this.Instance.GetRealValue(comparableRenderVisibleEnd));
            object nextValue = intervalFactory.Snap(obj4);
            nextValue = intervalFactory.GetNextValue(nextValue);
            double num5 = this.Instance.GetComparableValue(nextValue);
            double num6 = comparableValue;
            object obj6 = obj3;
            double length = 0.0;
            while (num6 < num5)
            {
                string longestText;
                CalendarItemBase groupItem;
                object obj7 = intervalFactory.GetNextValue(obj6);
                double num8 = this.Instance.GetComparableValue(obj7);
                double normalValue = this.LayoutInfo.GetNormalValue(num6);
                double x = this.LayoutInfo.GetNormalValue(num8);
                Point point = mapping.GetSnappedPoint(normalValue, 0.0, true);
                Point point2 = mapping.GetSnappedPoint(x, 1.0, true);
                Rect arg = new Rect(point, point2);
                Rect arrangeRect = arg;
                Rect rect3 = calcTextRect(arg);
                if (length == 0.0)
                {
                    length = rect3.Size.Width - 1.0;
                }
                intervalFactory.FormatText(obj6, out longestText, this.Owner.FontSize, length);
                double num11 = num8 - num6;
                if (isGroup)
                {
                    groupItem = this.GetGroupItem();
                    CalendarGroupItem item = (CalendarGroupItem) groupItem;
                    double num15 = this.ComparableToRender(this.LayoutInfo.ComparableVisibleStart);
                    double left = arrangeRect.Left;
                    double right = arrangeRect.Right;
                    double num18 = this.ComparableToRender(this.LayoutInfo.ComparableVisibleEnd);
                    double offset = 0.0;
                    if (num15 > left)
                    {
                        double num20 = num15 - left;
                        offset = num20 + ((num18 < right) ? (num18 - right) : 0.0);
                    }
                    else if (num18 < right)
                    {
                        Size realTextSize = item.GetRealTextSize();
                        offset = Math.Max((double) (((item.GetDefaulTextOffset() * 2.0) + realTextSize.Width) - arrangeRect.Width), (double) (num18 - right));
                    }
                    item.SetTextOffset(offset);
                    if (arrangeRect.Width > 300.0)
                    {
                        longestText = intervalFactory.GetLongestText(obj6);
                    }
                    this.PatchArrangeRect(ref arrangeRect, num6 == comparableValue, (num6 + num11) >= this.LayoutInfo.ComparableEnd);
                }
                else
                {
                    groupItem = this.GetItem();
                    (groupItem as CalendarItem).IsSelected = this.IsInSelectionRange(num6, num8);
                    this.PatchArrangeRect(ref arrangeRect, num6 == comparableValue, num8 >= (num5 - num11));
                    arrangeRect.Width = double.IsNaN(arrangeRect.Width) ? 0.0 : arrangeRect.Width;
                    arrangeRect.Height = double.IsNaN(arrangeRect.Height) ? 0.0 : arrangeRect.Height;
                    double num12 = this.Instance.GetComparableValue(this.Instance.SelectionStart);
                    double num13 = this.Instance.GetComparableValue(this.Instance.SelectionEnd);
                    double num14 = num13 - num12;
                    bool flag = (num6 <= num12) && (num8 >= num13);
                    if (!(!(num14 == 0.0) & flag) || (num14 >= num11))
                    {
                        if (!this.IsSelectionMarkerVisible)
                        {
                            this.selectionMarker.Arrange(new Rect(0.0, 0.0, 0.0, 0.0));
                        }
                    }
                    else
                    {
                        this.IsSelectionMarkerVisible = true;
                        this.selectionMarker.Content = base.DataContext;
                        this.selectionMarker.ContentTemplate = this.Owner.ZoomOutSelectionMarkerTemplate;
                        this.selectionMarker.Measure(arrangeRect.Size);
                        arrangeRect.Width = double.IsNaN(arrangeRect.Width) ? 0.0 : arrangeRect.Width;
                        arrangeRect.Height = double.IsNaN(arrangeRect.Height) ? 0.0 : arrangeRect.Height;
                        if (!double.IsInfinity(arrangeRect.Left) && !double.IsInfinity(arrangeRect.Width))
                        {
                            this.selectionMarker.Arrange(arrangeRect);
                        }
                        else
                        {
                            this.selectionMarker.Arrange(new Rect(0.0, 0.0, 0.0, 0.0));
                        }
                    }
                }
                groupItem.Text = longestText;
                arrangeRect.Width = double.IsNaN(arrangeRect.Width) ? 0.0 : arrangeRect.Width;
                arrangeRect.Height = double.IsNaN(arrangeRect.Height) ? 0.0 : arrangeRect.Height;
                groupItem.Measure(arrangeRect.Size);
                if (!double.IsInfinity(arrangeRect.Left) && !double.IsInfinity(arrangeRect.Width))
                {
                    groupItem.Arrange(arrangeRect);
                }
                else
                {
                    groupItem.Arrange(new Rect(0.0, 0.0, 0.0, 0.0));
                }
                obj6 = obj7;
                num6 = num8;
            }
        }

        internal void SetLayoutInfo(DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo info, Rect bounds)
        {
            this.LayoutInfo = info;
            this.RenderBounds = bounds;
        }

        private double ToNormalized(double value)
        {
            double comparableStart = this.LayoutInfo.ComparableStart;
            double comparableEnd = this.LayoutInfo.ComparableEnd;
            return ((value - comparableStart) / (comparableEnd - comparableStart));
        }

        internal CalendarClient Owner { get; set; }

        private IRangeControlClient Instance =>
            this.Owner;

        private List<CalendarItem> Items { get; set; }

        private List<CalendarGroupItem> GroupItems { get; set; }

        private Rect RenderBounds { get; set; }

        private bool IsSelectionMarkerVisible { get; set; }

        internal DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo LayoutInfo { get; private set; }
    }
}

