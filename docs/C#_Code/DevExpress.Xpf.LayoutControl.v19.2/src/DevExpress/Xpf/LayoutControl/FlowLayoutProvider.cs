namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FlowLayoutProvider : LayoutProviderBase
    {
        public FlowLayoutProvider(IFlowLayoutModel model) : base(model)
        {
            this.LayerInfos = new List<FlowLayoutLayerInfo>();
        }

        protected void AddLayerSeparator(Rect areaBounds, double layerEnd, FlowLayoutItemSize separatorSize)
        {
            this.Parameters.LayerSeparators.Add(this.Orientation, this.GetLayerSeparatorBounds(areaBounds, layerEnd, separatorSize));
        }

        protected virtual void AddLayerSeparators(Rect bounds, Rect viewPortBounds)
        {
            FlowLayoutItemSize separatorSize = this.CalculateLayerSeparatorSize(viewPortBounds);
            double layerStart = this.GetLayerStart(bounds);
            for (int i = 0; i < Math.Max(1, this.LayerInfos.Count - 1); i++)
            {
                FlowLayoutLayerInfo info = this.LayerInfos[i];
                if (i > 0)
                {
                    layerStart += this.GetLayerDistance(info.IsHardFlowBreak);
                }
                layerStart += info.Size.Width;
                this.AddLayerSeparator(bounds, layerStart, separatorSize);
            }
        }

        protected virtual FlowLayoutItemSize CalculateLayerSeparatorSize(Rect viewPortBounds)
        {
            FlowLayoutItemSize size = new FlowLayoutItemSize(this.LayerSeparatorThickness, this.GetLayerContentLength(viewPortBounds));
            foreach (FlowLayoutLayerInfo info in this.LayerInfos)
            {
                size.Length = Math.Max(size.Length, info.Size.Length);
            }
            return size;
        }

        public virtual double CalculateLayerWidthChange(LayerSeparator separator, Point positionChange)
        {
            int num = 1 + this.Parameters.LayerSeparators.IndexOf(separator);
            double num2 = (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? positionChange.Y : positionChange.X;
            return (this.LayerWidthChangeDirection * Math.Round((double) (num2 / ((double) num))));
        }

        protected virtual unsafe void CalculateLayout(FrameworkElements items, Rect bounds, CalculateMaximizedElementIndex calculateMaximizedElementIndex, MeasureItem measureItem, ArrangeItem arrangeItem)
        {
            int maximizedElementIndex = calculateMaximizedElementIndex(items);
            if (maximizedElementIndex != -1)
            {
                this.PrepareMaximizedElement(items, maximizedElementIndex);
            }
            double layerContentStart = this.GetLayerContentStart(bounds);
            double itemOffset = layerContentStart;
            FlowLayoutItemPosition position = new FlowLayoutItemPosition(this.GetLayerStart(bounds), itemOffset);
            this.LayoutItems = items;
            this.LayerInfos.Clear();
            FlowLayoutLayerInfo item = null;
            for (int i = 0; i < items.Count; i++)
            {
                FrameworkElement element = items[i];
                FlowLayoutItemSize itemSize = this.GetItemSize(measureItem(element));
                if (i == 0)
                {
                    item = new FlowLayoutLayerInfo(0, true, position);
                    this.LayerInfos.Add(item);
                }
                else
                {
                    bool isHardFlowBreak = false;
                    if (((maximizedElementIndex != -1) || !this.IsFlowBreak(element, bounds, itemOffset, this.GetSlotLength(items, i, itemSize), out isHardFlowBreak)) && ((maximizedElementIndex == -1) || ((i != (maximizedElementIndex + 1)) && (i != maximizedElementIndex))))
                    {
                        position.ItemOffset = itemOffset;
                    }
                    else
                    {
                        double* numPtr1 = &position.LayerOffset;
                        numPtr1[0] += item.Size.Width + this.GetLayerDistance(isHardFlowBreak);
                        position.ItemOffset = layerContentStart;
                        item = new FlowLayoutLayerInfo(i, isHardFlowBreak, position);
                        this.LayerInfos.Add(item);
                    }
                }
                item.SlotFirstItemIndexes.Add(i);
                FlowLayoutItemPosition slotPosition = position;
                FlowLayoutItemSize slotSize = itemSize;
                if (this.IsSlotWithMultipleItems(items, i))
                {
                    this.CalculateLayoutForSlotWithMultipleItems(items, ref i, slotPosition, ref slotSize, measureItem, arrangeItem);
                }
                else if (arrangeItem != null)
                {
                    arrangeItem(element, ref slotPosition, ref slotSize);
                }
                itemOffset = slotPosition.ItemOffset;
                item.Size.Length = (itemOffset + slotSize.Length) - layerContentStart;
                item.Size.Width = Math.Max(item.Size.Width, slotSize.Width);
                itemOffset += this.GetSlotMaxLength(element, itemSize) + this.Parameters.ItemSpace;
            }
        }

        protected virtual void CalculateLayoutForSlotWithMultipleItems(FrameworkElements items, ref int itemIndex, FlowLayoutItemPosition slotPosition, ref FlowLayoutItemSize slotSize, MeasureItem measureItem, ArrangeItem arrangeItem)
        {
        }

        protected virtual int CalculateMaximizedElementIndexForArrange(FrameworkElements items) => 
            (this.Model.MaximizedElement != null) ? (((this.Model.MaximizedElementPosition == MaximizedElementPosition.Left) || (this.Model.MaximizedElementPosition == MaximizedElementPosition.Top)) ? 0 : (items.Count - 1)) : -1;

        protected virtual Size CalculateMaximizedElementSize(FrameworkElements items, Rect viewPortBounds)
        {
            double width = Math.Max((double) 0.0, (double) (this.GetLayerWidth(viewPortBounds) - (this.GetLayerDistance(false) + this.GetSlotMaxWidth(items))));
            return this.GetItemSize(new FlowLayoutItemSize(width, this.GetLayerContentLength(viewPortBounds)));
        }

        public override void CopyLayoutInfo(FrameworkElement from, FrameworkElement to)
        {
            base.CopyLayoutInfo(from, to);
            FlowLayoutControl.SetIsFlowBreak(to, FlowLayoutControl.GetIsFlowBreak(from));
        }

        protected virtual unsafe Rect GetElementHitTestBounds(Rect elementBounds, Rect bounds, bool isFront, bool isBack)
        {
            FlowLayoutItemPosition position;
            FlowLayoutItemSize size;
            this.GetItemPositionAndSize(elementBounds, out position, out size);
            double* numPtr1 = &size.Width;
            numPtr1[0] += this.Parameters.ItemSpace;
            if (isFront)
            {
                double num = Math.Max((double) 0.0, (double) (position.ItemOffset - this.GetLayerContentStart(bounds)));
                double* numPtr2 = &position.ItemOffset;
                numPtr2[0] -= num;
                double* numPtr3 = &size.Length;
                numPtr3[0] += num;
            }
            if (isBack)
            {
                size.Length = Math.Max((double) 0.0, (double) (this.GetLayerContentEnd(bounds) - position.ItemOffset));
            }
            else
            {
                double* numPtr4 = &size.Length;
                numPtr4[0] += this.Parameters.ItemSpace;
            }
            return this.GetItemBounds(position, size);
        }

        protected Rect GetItemBounds(FlowLayoutItemPosition itemPosition, FlowLayoutItemSize itemSize)
        {
            double height = Math.Max(0.0, itemSize.Length);
            double width = Math.Max(0.0, itemSize.Width);
            return ((this.Orientation != System.Windows.Controls.Orientation.Horizontal) ? new Rect(itemPosition.LayerOffset, itemPosition.ItemOffset, width, height) : new Rect(itemPosition.ItemOffset, itemPosition.LayerOffset, height, width));
        }

        public virtual int GetItemIndex(UIElementCollection items, int visibleIndex) => 
            (visibleIndex != this.LayoutItems.Count) ? ((this.LayoutItems[visibleIndex] != this.Model.MaximizedElement) ? items.IndexOf(this.LayoutItems[visibleIndex]) : ((visibleIndex != 0) ? (items.IndexOf(this.LayoutItems[visibleIndex - 1]) + 1) : items.IndexOf(this.LayoutItems[visibleIndex + 1]))) : ((this.LayoutItems[this.LayoutItems.Count - 1] != this.Model.MaximizedElement) ? (items.IndexOf(this.LayoutItems[this.LayoutItems.Count - 1]) + 1) : (items.IndexOf(this.LayoutItems[this.LayoutItems.Count - 2]) + 1));

        public virtual int GetItemPlaceIndex(FrameworkElement item, Rect bounds, Point p, out FlowBreakKind flowBreakKind)
        {
            flowBreakKind = FlowBreakKind.None;
            if (!bounds.Contains(p) || (this.LayoutItems.Count == 0))
            {
                return -1;
            }
            int num = 0;
            while (true)
            {
                int firstItemIndex;
                if (num < this.LayerInfos.Count)
                {
                    FlowLayoutLayerInfo layerInfo = this.LayerInfos[num];
                    Rect rect = this.GetElementHitTestBounds(this.GetLayerBounds(layerInfo), bounds, true, true);
                    if (rect.Contains(p))
                    {
                        for (int i = 0; i < layerInfo.SlotCount; i++)
                        {
                            Rect slotBounds = this.GetSlotBounds(layerInfo, i);
                            rect = this.GetElementHitTestBounds(slotBounds, bounds, i == 0, i == (layerInfo.SlotCount - 1));
                            if (rect.Contains(p))
                            {
                                bool flag;
                                int num3 = this.GetSlotItemPlaceIndex(layerInfo, i, item, bounds, p, out flag);
                                if (!flag)
                                {
                                    num3++;
                                }
                                else if ((num3 == 0) || FlowLayoutControl.GetIsFlowBreak(this.LayoutItems[num3]))
                                {
                                    flowBreakKind = FlowBreakKind.Existing;
                                }
                                return num3;
                            }
                        }
                    }
                    num++;
                    continue;
                }
                using (List<FlowLayoutLayerInfo>.Enumerator enumerator = this.LayerInfos.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            FlowLayoutLayerInfo current = enumerator.Current;
                            if (!current.IsHardFlowBreak || !this.GetLayerSpaceBounds(current, bounds).Contains(p))
                            {
                                continue;
                            }
                            if (this.CanAddHardFlowBreaks)
                            {
                                flowBreakKind = FlowBreakKind.New;
                            }
                            firstItemIndex = current.FirstItemIndex;
                        }
                        else
                        {
                            break;
                        }
                        break;
                    }
                }
                return firstItemIndex;
            }
            if (!this.GetRemainderBounds(bounds, this.GetLayerBounds(this.GetLayerInfo(this.LayoutItems.Count - 1))).Contains(p))
            {
                return -1;
            }
            if (this.CanAddHardFlowBreaks)
            {
                flowBreakKind = FlowBreakKind.New;
            }
            return this.LayoutItems.Count;
        }

        protected void GetItemPositionAndSize(Rect itemBounds, out FlowLayoutItemPosition itemPosition, out FlowLayoutItemSize itemSize)
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                itemPosition = new FlowLayoutItemPosition(itemBounds.Y, itemBounds.X);
                itemSize = new FlowLayoutItemSize(itemBounds.Height, itemBounds.Width);
            }
            else
            {
                itemPosition = new FlowLayoutItemPosition(itemBounds.X, itemBounds.Y);
                itemSize = new FlowLayoutItemSize(itemBounds.Width, itemBounds.Height);
            }
        }

        protected Size GetItemSize(FlowLayoutItemSize itemSize) => 
            this.GetItemBounds(new FlowLayoutItemPosition(0.0, 0.0), itemSize).Size();

        protected FlowLayoutItemSize GetItemSize(Size itemSize)
        {
            FlowLayoutItemPosition position;
            FlowLayoutItemSize size;
            this.GetItemPositionAndSize(RectHelper.New(itemSize), out position, out size);
            return size;
        }

        protected Rect GetLayerBounds(FlowLayoutLayerInfo layerInfo) => 
            this.GetItemBounds(layerInfo.Position, layerInfo.Size);

        protected double GetLayerCenter(Rect bounds) => 
            (this.GetLayerStart(bounds) + this.GetLayerEnd(bounds)) / 2.0;

        protected double GetLayerContentCenter(Rect bounds) => 
            (this.GetLayerContentStart(bounds) + this.GetLayerContentEnd(bounds)) / 2.0;

        protected double GetLayerContentEnd(Rect bounds) => 
            this.GetLayerContentStart(bounds) + this.GetLayerContentLength(bounds);

        protected double GetLayerContentLength(Rect bounds) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? bounds.Width : bounds.Height;

        protected double GetLayerContentOffset(Point p) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? p.X : p.Y;

        protected double GetLayerContentStart(Rect bounds) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? bounds.Left : bounds.Top;

        public virtual double GetLayerDistance(bool isHardFlowBreak) => 
            !this.ShowLayerSeparators ? this.GetLayerSpace(isHardFlowBreak) : ((this.GetLayerSpace(isHardFlowBreak) + this.LayerSeparatorThickness) + this.GetLayerSpace(isHardFlowBreak));

        protected double GetLayerEnd(Rect bounds) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? bounds.Bottom : bounds.Right;

        protected FlowLayoutLayerInfo GetLayerInfo(int itemIndex)
        {
            for (int i = 0; i < this.LayerInfos.Count; i++)
            {
                if (itemIndex < this.LayerInfos[i].FirstItemIndex)
                {
                    return ((i == 0) ? null : this.LayerInfos[i - 1]);
                }
            }
            return ((this.LayerInfos.Count != 0) ? this.LayerInfos[this.LayerInfos.Count - 1] : null);
        }

        public double GetLayerMaxWidth(Size itemsMaxSize) => 
            this.GetItemSize(itemsMaxSize).Width;

        public double GetLayerMinWidth(Size itemsMinSize) => 
            this.GetItemSize(itemsMinSize).Width;

        protected double GetLayerOffset(Point p) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? p.Y : p.X;

        protected virtual Rect GetLayerSeparatorBounds(Rect areaBounds, double layerEnd, FlowLayoutItemSize separatorSize) => 
            this.GetItemBounds(new FlowLayoutItemPosition(layerEnd + this.Parameters.LayerSpace, this.GetLayerContentStart(areaBounds)), separatorSize);

        public virtual double GetLayerSpace(bool isHardFlowBreak) => 
            this.Parameters.LayerSpace;

        protected Rect GetLayerSpaceBounds(FlowLayoutLayerInfo layerInfo, Rect bounds)
        {
            FlowLayoutItemPosition itemPosition = new FlowLayoutItemPosition(0.0, this.GetLayerContentStart(bounds));
            FlowLayoutItemSize itemSize = new FlowLayoutItemSize(0.0, this.GetLayerContentLength(bounds)) {
                Width = (layerInfo.FirstItemIndex != 0) ? this.GetLayerDistance(layerInfo.IsHardFlowBreak) : Math.Max((double) 0.0, (double) (layerInfo.Position.LayerOffset - this.GetLayerStart(bounds)))
            };
            itemPosition.LayerOffset = layerInfo.Position.LayerOffset - itemSize.Width;
            return this.GetItemBounds(itemPosition, itemSize);
        }

        protected double GetLayerStart(Rect bounds) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? bounds.Top : bounds.Left;

        protected double GetLayerWidth(Rect bounds) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? bounds.Height : bounds.Width;

        public double GetLayerWidth(FrameworkElements items, out Size itemSize)
        {
            double num3;
            double positiveInfinity = double.PositiveInfinity;
            itemSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            using (IEnumerator<FrameworkElement> enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FrameworkElement current = enumerator.Current;
                        if (ReferenceEquals(current, this.Model.MaximizedElement) || !current.IsVisible)
                        {
                            continue;
                        }
                        double width = this.GetItemSize(current.GetSize()).Width;
                        if (double.IsPositiveInfinity(positiveInfinity) || (width == positiveInfinity))
                        {
                            positiveInfinity = width;
                            continue;
                        }
                        num3 = double.PositiveInfinity;
                    }
                    else
                    {
                        if (double.IsPositiveInfinity(positiveInfinity))
                        {
                            positiveInfinity = 0.0;
                        }
                        itemSize = this.GetItemSize(new FlowLayoutItemSize(positiveInfinity, double.PositiveInfinity));
                        return positiveInfinity;
                    }
                    break;
                }
            }
            return num3;
        }

        protected unsafe Rect GetRemainderBounds(Rect bounds, Rect lastLayerBounds)
        {
            FlowLayoutItemPosition position;
            FlowLayoutItemSize size;
            this.GetItemPositionAndSize(bounds, out position, out size);
            double num = this.GetLayerEnd(lastLayerBounds) - position.LayerOffset;
            double* numPtr1 = &position.LayerOffset;
            numPtr1[0] += num;
            size.Width = Math.Max((double) 0.0, (double) (size.Width - num));
            return this.GetItemBounds(position, size);
        }

        protected Rect GetSlotBounds(FlowLayoutLayerInfo layerInfo, int slotIndex)
        {
            FlowLayoutItemPosition position;
            FlowLayoutItemSize size;
            FlowLayoutItemPosition position2;
            FlowLayoutItemSize size2;
            this.GetItemPositionAndSize(this.GetLayerBounds(layerInfo), out position, out size);
            int num = layerInfo.SlotFirstItemIndexes[slotIndex];
            this.GetItemPositionAndSize(this.LayoutItems[num].GetBounds(), out position2, out size2);
            size.Length = (position.ItemOffset + size.Length) - position2.ItemOffset;
            position.ItemOffset = position2.ItemOffset;
            if (slotIndex < (layerInfo.SlotCount - 1))
            {
                num = layerInfo.SlotFirstItemIndexes[slotIndex + 1];
                this.GetItemPositionAndSize(this.LayoutItems[num].GetBounds(), out position2, out size2);
                size.Length = (position2.ItemOffset - this.Parameters.ItemSpace) - position.ItemOffset;
            }
            return this.GetItemBounds(position, size);
        }

        protected virtual int GetSlotItemPlaceIndex(FlowLayoutLayerInfo layerInfo, int slotIndex, FrameworkElement item, Rect bounds, Point p, out bool isBeforeItemPlace)
        {
            int num = layerInfo.SlotFirstItemIndexes[slotIndex];
            if (!this.NeedsFullSlot(item) && !this.NeedsFullSlot(this.LayoutItems[num]))
            {
                isBeforeItemPlace = false;
                return -1;
            }
            isBeforeItemPlace = this.GetLayerContentOffset(p) < this.GetLayerContentCenter(this.GetSlotBounds(layerInfo, slotIndex));
            return (!isBeforeItemPlace ? this.GetSlotLastItemIndex(layerInfo, slotIndex) : num);
        }

        protected int GetSlotLastItemIndex(FlowLayoutLayerInfo layerInfo, int slotIndex)
        {
            if (slotIndex != (layerInfo.SlotCount - 1))
            {
                return (layerInfo.SlotFirstItemIndexes[slotIndex + 1] - 1);
            }
            int index = this.LayerInfos.IndexOf(layerInfo);
            return ((index != (this.LayerInfos.Count - 1)) ? (this.LayerInfos[index + 1].FirstItemIndex - 1) : (this.LayoutItems.Count - 1));
        }

        protected virtual double GetSlotLength(FrameworkElements items, int itemIndex, FlowLayoutItemSize itemSize) => 
            itemSize.Length;

        protected virtual double GetSlotMaxLength(FrameworkElement item, FlowLayoutItemSize itemSize) => 
            itemSize.Length;

        protected double GetSlotMaxWidth(FrameworkElements items)
        {
            double num = 0.0;
            for (int i = 0; i < items.Count; i++)
            {
                FrameworkElement objA = items[i];
                if (!ReferenceEquals(objA, this.Model.MaximizedElement))
                {
                    num = Math.Max(num, this.GetSlotMaxWidth(objA, this.GetItemSize(objA.GetDesiredSize())));
                }
            }
            return num;
        }

        protected virtual double GetSlotMaxWidth(FrameworkElement item, FlowLayoutItemSize itemSize) => 
            itemSize.Width;

        protected virtual bool IsFlowBreak(FrameworkElement item, Rect bounds, double slotOffset, double slotLength, out bool isHardFlowBreak)
        {
            isHardFlowBreak = FlowLayoutControl.GetIsFlowBreak(item);
            return (!this.StretchContent && (isHardFlowBreak || (this.Parameters.BreakFlowToFit && ((slotOffset + slotLength) > this.GetLayerContentEnd(bounds)))));
        }

        protected bool IsSlotWithMultipleItems(FrameworkElements items, int slotFirstItemIndex)
        {
            if (this.NeedsFullSlot(items[slotFirstItemIndex]) || (slotFirstItemIndex >= (items.Count - 1)))
            {
                return false;
            }
            FrameworkElement item = items[slotFirstItemIndex + 1];
            return (!this.NeedsFullSlot(item) && !FlowLayoutControl.GetIsFlowBreak(item));
        }

        protected virtual bool NeedsFullSlot(FrameworkElement item) => 
            true;

        public unsafe void OffsetLayerSeparators(double layerWidthChange)
        {
            if (layerWidthChange != 0.0)
            {
                layerWidthChange *= this.LayerWidthChangeDirection;
                for (int i = 0; i < this.Parameters.LayerSeparators.Count; i++)
                {
                    FlowLayoutItemPosition position;
                    FlowLayoutItemSize size;
                    FrameworkElement element = this.Parameters.LayerSeparators[i];
                    this.GetItemPositionAndSize(element.GetBounds(), out position, out size);
                    double* numPtr1 = &position.LayerOffset;
                    numPtr1[0] += (1 + i) * layerWidthChange;
                    element.Arrange(this.GetItemBounds(position, size));
                }
            }
        }

        protected override Size OnArrange(FrameworkElements items, Rect bounds, Rect viewPortBounds)
        {
            this.CalculateLayout(items, bounds, new CalculateMaximizedElementIndex(this.CalculateMaximizedElementIndexForArrange), item => !ReferenceEquals(item, this.Model.MaximizedElement) ? item.GetDesiredSize() : this.CalculateMaximizedElementSize(items, viewPortBounds), delegate (FrameworkElement item, ref FlowLayoutItemPosition itemPosition, ref FlowLayoutItemSize itemSize) {
                if (ReferenceEquals(item, this.Model.MaximizedElement))
                {
                    itemPosition.ItemOffset = this.GetLayerContentStart(viewPortBounds);
                }
                else
                {
                    itemSize = this.GetItemSize(item.DesiredSize);
                    if (this.StretchContent)
                    {
                        itemSize.Width = this.GetLayerWidth(viewPortBounds);
                    }
                }
                item.Arrange(this.GetItemBounds(itemPosition, itemSize));
            });
            if (this.ShowLayerSeparators && (items.Count != 0))
            {
                this.AddLayerSeparators(bounds, viewPortBounds);
            }
            return bounds.Size();
        }

        protected override unsafe Size OnMeasure(FrameworkElements items, Size maxSize)
        {
            this.CalculateLayout(items, RectHelper.New(maxSize), elements => (this.Model.MaximizedElement == null) ? -1 : (elements.Count - 1), delegate (FrameworkElement item) {
                Size size;
                if (ReferenceEquals(item, this.Model.MaximizedElement))
                {
                    size = this.CalculateMaximizedElementSize(items, RectHelper.New(maxSize));
                }
                else
                {
                    FlowLayoutItemSize size2 = new FlowLayoutItemSize(double.PositiveInfinity, double.PositiveInfinity);
                    if (this.StretchContent)
                    {
                        size2.Width = this.GetLayerWidth(RectHelper.New(maxSize));
                    }
                    size = this.GetItemSize(size2);
                }
                item.Measure(size);
                return item.GetDesiredSize();
            }, null);
            FlowLayoutItemSize itemSize = new FlowLayoutItemSize(0.0, 0.0);
            if (items.Count != 0)
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.LayerInfos.Count)
                    {
                        if (this.ShowLayerSeparators && (this.LayerInfos.Count == 1))
                        {
                            double* numPtr3 = &itemSize.Width;
                            numPtr3[0] += this.GetLayerDistance(false);
                        }
                        break;
                    }
                    FlowLayoutLayerInfo info = this.LayerInfos[num];
                    if (num > 0)
                    {
                        double* numPtr1 = &itemSize.Width;
                        numPtr1[0] += this.GetLayerDistance(info.IsHardFlowBreak);
                    }
                    double* numPtr2 = &itemSize.Width;
                    numPtr2[0] += info.Size.Width;
                    itemSize.Length = Math.Max(itemSize.Length, info.Size.Length);
                    num++;
                }
            }
            return this.GetItemSize(itemSize);
        }

        protected override void OnParametersChanged()
        {
            base.OnParametersChanged();
            if (this.ShowLayerSeparators)
            {
                this.LayerSeparatorThickness = this.Parameters.LayerSeparators.SeparatorThickness;
            }
        }

        protected virtual void PrepareMaximizedElement(FrameworkElements items, int maximizedElementIndex)
        {
            items.Remove(this.Model.MaximizedElement);
            items.Insert(maximizedElementIndex, this.Model.MaximizedElement);
        }

        public void SetLayerWidth(FrameworkElements items, double value, out Size itemSize)
        {
            Size size;
            itemSize = this.GetItemSize(new FlowLayoutItemSize(value, double.PositiveInfinity));
            if (this.GetLayerWidth(items, out size) != value)
            {
                foreach (FrameworkElement element in items)
                {
                    if (!ReferenceEquals(element, this.Model.MaximizedElement))
                    {
                        if (!double.IsPositiveInfinity(itemSize.Width))
                        {
                            element.Width = itemSize.Width;
                        }
                        if (!double.IsPositiveInfinity(itemSize.Height))
                        {
                            element.Height = itemSize.Height;
                        }
                    }
                }
            }
        }

        public virtual void UpdateChildrenBounds(ref Rect bounds, Rect maximizedElementBounds)
        {
            if (bounds.IsEmpty)
            {
                bounds = maximizedElementBounds;
            }
            else if ((this.Model.MaximizedElementPosition != MaximizedElementPosition.Left) && (this.Model.MaximizedElementPosition != MaximizedElementPosition.Right))
            {
                RectHelper.SetTop(ref bounds, Math.Min(bounds.Top, maximizedElementBounds.Top));
                RectHelper.SetBottom(ref bounds, Math.Max(bounds.Bottom, maximizedElementBounds.Bottom));
            }
            else
            {
                RectHelper.SetLeft(ref bounds, Math.Min(bounds.Left, maximizedElementBounds.Left));
                RectHelper.SetRight(ref bounds, Math.Max(bounds.Right, maximizedElementBounds.Right));
            }
        }

        public override unsafe void UpdateScrollableAreaBounds(ref Rect bounds)
        {
            base.UpdateScrollableAreaBounds(ref bounds);
            if (this.Model.MaximizedElement != null)
            {
                FlowLayoutItemPosition position;
                FlowLayoutItemPosition position2;
                FlowLayoutItemSize size;
                FlowLayoutItemSize size2;
                this.GetItemPositionAndSize(bounds, out position, out size);
                this.GetItemPositionAndSize(this.Model.MaximizedElement.GetBounds(), out position2, out size2);
                if ((this.Model.MaximizedElementPosition != MaximizedElementPosition.Left) && (this.Model.MaximizedElementPosition != MaximizedElementPosition.Top))
                {
                    double* numPtr3 = &size.Width;
                    numPtr3[0] -= (position.LayerOffset + size.Width) - position2.LayerOffset;
                }
                else
                {
                    double num = (position2.LayerOffset + size2.Width) - position.LayerOffset;
                    double* numPtr1 = &position.LayerOffset;
                    numPtr1[0] += num;
                    double* numPtr2 = &size.Width;
                    numPtr2[0] -= num;
                }
                size.Width = Math.Max(0.0, size.Width);
                bounds = this.GetItemBounds(position, size);
            }
        }

        public virtual bool CanAddHardFlowBreaks =>
            ReferenceEquals(this.Model.MaximizedElement, null);

        public double LayerSeparatorThickness { get; private set; }

        public FrameworkElements LayoutItems { get; protected set; }

        public virtual System.Windows.Controls.Orientation Orientation =>
            (this.Model.MaximizedElement != null) ? (((this.Model.MaximizedElementPosition == MaximizedElementPosition.Left) || (this.Model.MaximizedElementPosition == MaximizedElementPosition.Right)) ? System.Windows.Controls.Orientation.Vertical : System.Windows.Controls.Orientation.Horizontal) : this.Model.Orientation;

        public virtual bool ShowLayerSeparators =>
            !this.StretchContent && this.Parameters.ShowLayerSeparators;

        public virtual bool StretchContent =>
            (this.Model.MaximizedElement == null) && this.Parameters.StretchContent;

        protected IFlowLayoutModel Model =>
            (IFlowLayoutModel) base.Model;

        protected List<FlowLayoutLayerInfo> LayerInfos { get; private set; }

        protected double LayerWidthChangeDirection =>
            ((this.Model.MaximizedElement == null) || ((this.Model.MaximizedElementPosition != MaximizedElementPosition.Left) && (this.Model.MaximizedElementPosition != MaximizedElementPosition.Top))) ? 1.0 : -1.0;

        protected FlowLayoutParameters Parameters =>
            base.Parameters as FlowLayoutParameters;

        protected delegate void ArrangeItem(FrameworkElement item, ref FlowLayoutItemPosition itemPosition, ref FlowLayoutItemSize itemSize);

        protected delegate int CalculateMaximizedElementIndex(FrameworkElements items);

        protected delegate Size MeasureItem(FrameworkElement item);
    }
}

