namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class TileLayoutProvider : FlowLayoutProvider
    {
        public TileLayoutProvider(IFlowLayoutModel model) : base(model)
        {
        }

        protected void AddGroupHeader(object headerSource, Rect headerAreaBounds)
        {
            TileGroupHeader element = this.Parameters.GroupHeaders.Add();
            Binding binding = new Binding();
            binding.Source = headerSource;
            binding.Path = new PropertyPath(TileLayoutControl.GroupHeaderProperty);
            binding.Mode = BindingMode.TwoWay;
            element.SetBinding(ContentControlBase.ContentProperty, binding);
            element.InvalidateParentsOfModifiedChildren();
            element.Measure(headerAreaBounds.Size());
            element.Arrange(this.GetGroupHeaderBounds(headerAreaBounds, element.DesiredSize));
        }

        protected virtual void AddGroupHeaders()
        {
            for (int i = 0; i < base.LayerInfos.Count; i++)
            {
                FlowLayoutLayerInfo info = base.LayerInfos[i];
                if (info.IsHardFlowBreak)
                {
                    this.AddGroupHeader(base.LayoutItems[info.FirstItemIndex], this.GetGroupHeaderAreaBounds(i));
                }
            }
        }

        protected override unsafe void CalculateLayoutForSlotWithMultipleItems(FrameworkElements items, ref int itemIndex, FlowLayoutItemPosition slotPosition, ref FlowLayoutItemSize slotSize, FlowLayoutProvider.MeasureItem measureItem, FlowLayoutProvider.ArrangeItem arrangeItem)
        {
            List<int> itemPositionsInSlot = this.GetItemPositionsInSlot(items, itemIndex);
            FlowLayoutItemPosition position = slotPosition;
            FlowLayoutItemSize size = slotSize;
            FlowLayoutItemPosition position2 = slotPosition;
            FlowLayoutItemSize size2 = slotSize;
            for (int i = itemIndex; i < (itemIndex + itemPositionsInSlot.Count); i++)
            {
                FrameworkElement item = items[i];
                FlowLayoutItemSize itemSize = (i == itemIndex) ? size2 : base.GetItemSize(measureItem(item));
                FlowLayoutItemPosition itemPosition = position2;
                switch (itemPositionsInSlot[i - itemIndex])
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    {
                        double* numPtr1 = &itemPosition.LayerOffset;
                        numPtr1[0] += size2.Width + this.Parameters.ItemSpace;
                        break;
                    }
                    case 2:
                    case 6:
                    {
                        itemPosition.LayerOffset = position.LayerOffset;
                        double* numPtr2 = &itemPosition.ItemOffset;
                        numPtr2[0] += size2.Length + this.Parameters.ItemSpace;
                        break;
                    }
                    case 4:
                    {
                        double* numPtr3 = &position.LayerOffset;
                        numPtr3[0] += this.GetHalfSlotMaxWidth(items[itemIndex], slotSize) + this.Parameters.ItemSpace;
                        itemPosition = position;
                        slotSize.Length = size.Length;
                        size = itemSize;
                        break;
                    }
                    default:
                        break;
                }
                size.Width = Math.Max(size.Width, (itemPosition.LayerOffset + itemSize.Width) - position.LayerOffset);
                size.Length = Math.Max(size.Length, (itemPosition.ItemOffset + itemSize.Length) - position.ItemOffset);
                if (arrangeItem != null)
                {
                    arrangeItem(item, ref itemPosition, ref itemSize);
                }
                position2 = itemPosition;
                size2 = itemSize;
            }
            itemIndex += itemPositionsInSlot.Count - 1;
            slotSize.Width = (position.LayerOffset + size.Width) - slotPosition.LayerOffset;
            slotSize.Length = Math.Max(slotSize.Length, size.Length);
        }

        protected virtual Rect GetGroupBounds(int groupFirstLayerInfoIndex)
        {
            Rect empty = Rect.Empty;
            int num = groupFirstLayerInfoIndex;
            while (true)
            {
                if (num < base.LayerInfos.Count)
                {
                    FlowLayoutLayerInfo layerInfo = base.LayerInfos[num];
                    if ((num <= groupFirstLayerInfoIndex) || !layerInfo.IsHardFlowBreak)
                    {
                        empty.Union(base.GetLayerBounds(layerInfo));
                        num++;
                        continue;
                    }
                }
                return empty;
            }
        }

        protected virtual unsafe Rect GetGroupHeaderAreaBounds(int groupFirstLayerInfoIndex)
        {
            Rect groupBounds = this.GetGroupBounds(groupFirstLayerInfoIndex);
            Rect* rectPtr1 = &groupBounds;
            rectPtr1.Y -= this.Parameters.GroupHeaderSpace;
            groupBounds.Height = double.PositiveInfinity;
            return groupBounds;
        }

        protected virtual unsafe Rect GetGroupHeaderBounds(Rect headerAreaBounds, Size headerSize)
        {
            Rect rect = headerAreaBounds;
            Rect* rectPtr1 = &rect;
            rectPtr1.Y -= headerSize.Height;
            rect.Height = headerSize.Height;
            return rect;
        }

        protected virtual Rect GetHalfSlotBounds(FlowLayoutLayerInfo layerInfo, int slotIndex, bool isFirstHalf)
        {
            Rect slotBounds = base.GetSlotBounds(layerInfo, slotIndex);
            FrameworkElement item = base.LayoutItems[layerInfo.SlotFirstItemIndexes[slotIndex]];
            double halfSlotMaxWidth = this.GetHalfSlotMaxWidth(item, base.GetItemSize(item.GetSize()));
            if (isFirstHalf)
            {
                slotBounds.Width = Math.Min(slotBounds.Width, halfSlotMaxWidth);
            }
            else
            {
                RectHelper.IncLeft(ref slotBounds, halfSlotMaxWidth + this.Parameters.ItemSpace);
            }
            return slotBounds;
        }

        protected virtual void GetHalfSlotFirstAndLastItemIndexes(FlowLayoutLayerInfo layerInfo, int slotIndex, bool isFirstHalf, out int firstItemIndex, out int lastItemIndex)
        {
            int slotFirstItemIndex = layerInfo.SlotFirstItemIndexes[slotIndex];
            List<int> itemPositionsInSlot = this.GetItemPositionsInSlot(base.LayoutItems, slotFirstItemIndex);
            firstItemIndex = slotFirstItemIndex;
            lastItemIndex = (slotFirstItemIndex + itemPositionsInSlot.Count) - 1;
            for (int i = 0; i < itemPositionsInSlot.Count; i++)
            {
                if (itemPositionsInSlot[i] == 4)
                {
                    if (isFirstHalf)
                    {
                        lastItemIndex = (slotFirstItemIndex + i) - 1;
                        return;
                    }
                    firstItemIndex = slotFirstItemIndex + i;
                    return;
                }
            }
        }

        protected virtual int GetHalfSlotItemPlaceIndex(FlowLayoutLayerInfo layerInfo, int slotIndex, bool isFirstHalf, FrameworkElement item, Rect bounds, Point p, out bool isBeforeItemPlace)
        {
            int num;
            int num2;
            Rect elementBounds = this.GetHalfSlotBounds(layerInfo, slotIndex, isFirstHalf);
            if (!this.GetElementHitTestBounds(elementBounds, bounds, slotIndex == 0, slotIndex == (layerInfo.SlotCount - 1)).Contains(p))
            {
                isBeforeItemPlace = false;
                return -1;
            }
            isBeforeItemPlace = this.NeedsHalfSlot(item) && (base.GetLayerOffset(p) < base.GetLayerCenter(elementBounds));
            this.GetHalfSlotFirstAndLastItemIndexes(layerInfo, slotIndex, isFirstHalf, out num, out num2);
            return (isBeforeItemPlace ? num : num2);
        }

        protected virtual double GetHalfSlotMaxWidth(FrameworkElement item, FlowLayoutItemSize itemSize) => 
            (((ITile) item).Size != TileSize.ExtraSmall) ? itemSize.Width : ((2.0 * itemSize.Width) + this.Parameters.ItemSpace);

        protected virtual List<int> GetItemPositionsInSlot(FrameworkElements items, int slotFirstItemIndex)
        {
            List<int> list = new List<int>();
            int item = 0;
            list.Add(item);
            int num2 = slotFirstItemIndex + 1;
            while (true)
            {
                if ((num2 < items.Count) && (!this.NeedsFullSlot(items[num2]) && !FlowLayoutControl.GetIsFlowBreak(items[num2])))
                {
                    int num3 = (((ITile) items[num2 - 1]).Size != TileSize.Small) ? ((((ITile) items[num2]).Size != TileSize.Small) ? (item + 1) : ((item - (item % 4)) + 4)) : (item + 4);
                    if (num3 < 8)
                    {
                        list.Add(num3);
                        item = num3;
                        num2++;
                        continue;
                    }
                }
                return list;
            }
        }

        public override double GetLayerSpace(bool isHardFlowBreak) => 
            isHardFlowBreak ? base.GetLayerSpace(isHardFlowBreak) : this.Parameters.ItemSpace;

        protected override int GetSlotItemPlaceIndex(FlowLayoutLayerInfo layerInfo, int slotIndex, FrameworkElement item, Rect bounds, Point p, out bool isBeforeItemPlace)
        {
            int num = base.GetSlotItemPlaceIndex(layerInfo, slotIndex, item, bounds, p, out isBeforeItemPlace);
            if (num == -1)
            {
                if (!this.NeedsHalfSlot(item))
                {
                    for (int i = layerInfo.SlotFirstItemIndexes[slotIndex]; i <= base.GetSlotLastItemIndex(layerInfo, slotIndex); i++)
                    {
                        FrameworkElement element = base.LayoutItems[i];
                        Rect elementBounds = element.GetBounds();
                        bool isBack = slotIndex == (layerInfo.SlotCount - 1);
                        if (isBack && !this.NeedsHalfSlot(element))
                        {
                            int num3 = this.GetItemPositionsInSlot(base.LayoutItems, layerInfo.SlotFirstItemIndexes[slotIndex])[i - layerInfo.SlotFirstItemIndexes[slotIndex]];
                            isBack = ((num3 == 2) || ((num3 == 3) || (num3 == 6))) || (num3 == 7);
                        }
                        if (this.GetElementHitTestBounds(elementBounds, bounds, slotIndex == 0, isBack).Contains(p))
                        {
                            isBeforeItemPlace = base.GetLayerOffset(p) < base.GetLayerCenter(elementBounds);
                            return i;
                        }
                    }
                }
                num = this.GetHalfSlotItemPlaceIndex(layerInfo, slotIndex, true, item, bounds, p, out isBeforeItemPlace);
                if (num == -1)
                {
                    num = this.GetHalfSlotItemPlaceIndex(layerInfo, slotIndex, false, item, bounds, p, out isBeforeItemPlace);
                }
            }
            return num;
        }

        protected override double GetSlotLength(FrameworkElements items, int itemIndex, FlowLayoutItemSize itemSize)
        {
            double num = base.GetSlotLength(items, itemIndex, itemSize);
            double slotMaxLength = this.GetSlotMaxLength(items[itemIndex], itemSize);
            if (num < slotMaxLength)
            {
                using (List<int>.Enumerator enumerator = this.GetItemPositionsInSlot(items, itemIndex).GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        if (enumerator.Current > 1)
                        {
                            return slotMaxLength;
                        }
                    }
                }
            }
            return num;
        }

        protected override double GetSlotMaxLength(FrameworkElement item, FlowLayoutItemSize itemSize) => 
            (this.NeedsFullSlot(item) || (((ITile) item).Size != TileSize.ExtraSmall)) ? base.GetSlotMaxLength(item, itemSize) : ((2.0 * itemSize.Length) + this.Parameters.ItemSpace);

        protected override double GetSlotMaxWidth(FrameworkElement item, FlowLayoutItemSize itemSize) => 
            !this.NeedsFullSlot(item) ? ((2.0 * this.GetHalfSlotMaxWidth(item, itemSize)) + this.Parameters.ItemSpace) : base.GetSlotMaxWidth(item, itemSize);

        protected override bool NeedsFullSlot(FrameworkElement item)
        {
            ITile tile = item as ITile;
            return (ReferenceEquals(item, this.Model.MaximizedElement) || ((this.Orientation != Orientation.Vertical) || ((tile == null) || ((tile.Size != TileSize.ExtraSmall) && (tile.Size != TileSize.Small)))));
        }

        protected virtual bool NeedsHalfSlot(FrameworkElement item) => 
            (item is ITile) && (((ITile) item).Size == TileSize.Small);

        protected override Size OnArrange(FrameworkElements items, Rect bounds, Rect viewPortBounds)
        {
            Size size = base.OnArrange(items, bounds, viewPortBounds);
            if (this.ShowGroupHeaders)
            {
                this.AddGroupHeaders();
            }
            return size;
        }

        public virtual bool ShowGroupHeaders =>
            (this.Model.MaximizedElement == null) && this.Model.ShowGroupHeaders;

        protected TileLayoutParameters Parameters =>
            base.Parameters as TileLayoutParameters;

        protected ITileLayoutModel Model =>
            (ITileLayoutModel) base.Model;
    }
}

