namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LayoutGroupProvider : LayoutProviderBase
    {
        private List<List<LayoutItems>> previousColumnsList;
        private int repeatCount;

        static LayoutGroupProvider()
        {
            DetectCyclicColumnsAlignment = true;
        }

        public LayoutGroupProvider(ILayoutGroupModel model) : base(model)
        {
            this.previousColumnsList = new List<List<LayoutItems>>();
        }

        protected virtual void AddItemSizers(FrameworkElements items)
        {
            if (items.Count > 1)
            {
                FrameworkElement[] itemSizerElements = new FrameworkElement[items.Count + 1];
                this.DefineItemSizerElements(items, ItemAlignment.Start, itemSizerElements);
                this.DefineItemSizerElements(items, ItemAlignment.End, itemSizerElements);
                this.DefineItemSizerElements(items, ItemAlignment.Stretch, itemSizerElements);
                int index = 0;
                while (true)
                {
                    while (true)
                    {
                        bool flag;
                        if (index >= items.Count)
                        {
                            return;
                        }
                        FrameworkElement item = items[index];
                        ItemAlignment itemAlignment = this.GetItemAlignment(item);
                        if (itemAlignment == ItemAlignment.Center)
                        {
                            if (!this.IsItemSizable(item))
                            {
                                break;
                            }
                            flag = false;
                        }
                        else if (ReferenceEquals(itemSizerElements[1 + index], item))
                        {
                            flag = false;
                        }
                        else
                        {
                            if (!ReferenceEquals(itemSizerElements[index], item))
                            {
                                break;
                            }
                            flag = true;
                        }
                        this.Parameters.ItemSizers.Add(item, this.GetItemSizerSide(flag));
                        break;
                    }
                    index++;
                }
            }
        }

        public void AlignLayoutItemLabels(FrameworkElement scope, FrameworkElements items)
        {
            LayoutGroup model = this.Model as LayoutGroup;
            if (model != null)
            {
                model.LayoutUpdated -= new EventHandler(this.LayoutGroup_LayoutUpdated);
                model.LayoutUpdated += new EventHandler(this.LayoutGroup_LayoutUpdated);
            }
            LayoutItems layoutItems = new LayoutItems();
            this.GetLayoutItems(items, layoutItems);
            if (layoutItems.Count != 0)
            {
                List<LayoutItems> columns = this.CalculateColumns(scope, layoutItems);
                if ((this.previousColumnsList.Count > 1) && IsLayoutRepeated(this.previousColumnsList, columns))
                {
                    this.repeatCount++;
                }
                if (this.repeatCount < 2)
                {
                    this.previousColumnsList.Add(columns);
                    foreach (LayoutItems items3 in columns)
                    {
                        this.SetLabelWidth(items3, this.CalculateCommonLabelWidth(items3));
                    }
                }
            }
        }

        private static bool AreColumnsEqual(LayoutItems previousColumn, LayoutItems column)
        {
            bool flag = previousColumn.Count == column.Count;
            if (flag)
            {
                for (int i = 0; i < previousColumn.Count; i++)
                {
                    if (previousColumn[i] != column[i])
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

        private static bool AreColumnsListsEqual(List<LayoutItems> previousColumns, List<LayoutItems> columns)
        {
            if (previousColumns.Count != columns.Count)
            {
                return false;
            }
            for (int i = 0; i < previousColumns.Count; i++)
            {
                if (!AreColumnsEqual(previousColumns[i], columns[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public void ArrangeItems(FrameworkElements items)
        {
            this.CreateItemInfos(0.0, 0.0).ArrangeItems(items, new Func<FrameworkElement, ItemAlignment>(this.GetItemAlignment));
        }

        protected virtual Size ArrangeWhenCollapsed(FrameworkElements items, Rect bounds)
        {
            FrameworkElement uncollapsedChild = this.Model.UncollapsedChild;
            foreach (FrameworkElement element2 in items)
            {
                Rect invisibleBounds;
                if (element2.IsLayoutGroup() && ((ILayoutGroup) element2).IsBorderless)
                {
                    invisibleBounds = bounds;
                }
                else
                {
                    invisibleBounds = RectHelper.New(this.GetItemDesiredSize(element2));
                    RectHelper.AlignHorizontally(ref invisibleBounds, bounds, this.GetItemHorizontalAlignment(element2));
                    RectHelper.AlignVertically(ref invisibleBounds, bounds, this.GetItemVerticalAlignment(element2));
                }
                if (!ReferenceEquals(element2, uncollapsedChild))
                {
                    if (this.Model.MeasureUncollapsedChildOnly && (element2.RenderSize.IsZero() && (!element2.IsLayoutGroup() || !((ILayoutGroup) element2).HasNewChildren)))
                    {
                        continue;
                    }
                    invisibleBounds = UIElementExtensions.InvisibleBounds;
                }
                element2.Arrange(invisibleBounds);
            }
            return bounds.Size();
        }

        protected virtual Size ArrangeWhenUncollapsed(FrameworkElements items, Rect bounds)
        {
            ItemInfos infos = this.CreateItemInfos(this.GetItemLength(bounds.Size()), this.Parameters.ItemSpace);
            foreach (FrameworkElement element in items)
            {
                infos.Add(new DevExpress.Xpf.LayoutControl.ItemInfo(this.GetItemLength(this.GetItemDesiredSize(element)), this.GetItemLength(this.GetItemMinSize(element, true)), this.GetItemLength(this.GetItemMaxSize(element, true)), this.GetItemAlignment(element)));
            }
            infos.Calculate();
            double itemOffset = this.GetItemOffset(bounds);
            for (int i = 0; i < items.Count; i++)
            {
                Point itemLocation = new Point();
                Size itemDesiredSize = this.GetItemDesiredSize(items[i]);
                this.SetItemOffset(ref itemLocation, itemOffset + infos[i].Offset);
                this.SetItemLength(ref itemDesiredSize, infos[i].Length);
                Rect rect = new Rect(itemLocation, itemDesiredSize);
                if (this.Model.Orientation == Orientation.Horizontal)
                {
                    RectHelper.AlignVertically(ref rect, bounds, this.GetItemVerticalAlignment(items[i]));
                }
                else
                {
                    RectHelper.AlignHorizontally(ref rect, bounds, this.GetItemHorizontalAlignment(items[i]));
                }
                items[i].Arrange(rect);
            }
            if (this.Parameters.ItemSizers != null)
            {
                this.ArrangeItems(items);
                this.AddItemSizers(items);
            }
            return bounds.Size();
        }

        protected virtual List<LayoutItems> CalculateColumns(FrameworkElement scope, LayoutItems layoutItems)
        {
            List<LayoutItems> list = new List<LayoutItems>();
            List<double> list2 = new List<double>();
            foreach (LayoutItem item in layoutItems)
            {
                Point position = item.GetPosition(scope);
                double num = Math.Round(position.X, 6);
                int index = list2.IndexOf(num);
                if (index == -1)
                {
                    index = list2.Count;
                    list2.Add(num);
                    list.Add(new LayoutItems());
                }
                list[index].Add(item);
            }
            return list;
        }

        protected virtual double CalculateCommonLabelWidth(LayoutItems layoutItems)
        {
            double num = 0.0;
            foreach (LayoutItem item in layoutItems)
            {
                num = Math.Max(num, item.LabelWidth);
            }
            return num;
        }

        protected virtual void CalculateInsertionInfoForEmptyArea(ILayoutGroup control, Rect bounds, Point p, FrameworkElements items, out LayoutItemInsertionKind insertionKind, out FrameworkElement insertionPoint)
        {
            if ((items.Count == 0) && bounds.Contains(p))
            {
                insertionKind = LayoutItemInsertionKind.Inside;
                insertionPoint = control.Control;
            }
            else
            {
                if (control.CollapseMode != LayoutGroupCollapseMode.None)
                {
                    if (control.UncollapsedChild != null)
                    {
                        insertionPoint = control.UncollapsedChild;
                        insertionKind = this.GetInsertionKindForEmptyArea(insertionPoint.GetBounds(), p);
                        return;
                    }
                }
                else
                {
                    this.ArrangeItems(items);
                    Rect itemBounds = bounds;
                    for (int i = 0; i < items.Count; i++)
                    {
                        Rect rect2 = items[i].GetBounds();
                        if (i < (items.Count - 1))
                        {
                            this.SetItemOffsetPlusLength(ref itemBounds, (this.GetItemOffsetPlusLength(rect2) + this.GetItemOffset(items[i + 1].GetBounds())) / 2.0);
                        }
                        else
                        {
                            this.SetItemOffsetPlusLength(ref itemBounds, this.GetItemOffsetPlusLength(bounds));
                        }
                        if (itemBounds.Contains(p))
                        {
                            insertionPoint = items[i];
                            insertionKind = this.GetInsertionKindForEmptyArea(rect2, p);
                            return;
                        }
                        this.SetItemOffset(ref itemBounds, this.GetItemOffsetPlusLength(itemBounds));
                    }
                }
                insertionKind = LayoutItemInsertionKind.None;
                insertionPoint = null;
            }
        }

        protected virtual bool CanAlignLayoutItemLabel(LayoutItem layoutItem)
        {
            HorizontalAlignment itemHorizontalAlignment = this.GetItemHorizontalAlignment(layoutItem);
            return (((itemHorizontalAlignment == HorizontalAlignment.Left) || (itemHorizontalAlignment == HorizontalAlignment.Stretch)) ? (layoutItem.IsLabelVisible && (layoutItem.LabelPosition == LayoutItemLabelPosition.Left)) : false);
        }

        public override void CopyLayoutInfo(FrameworkElement from, FrameworkElement to)
        {
            base.CopyLayoutInfo(from, to);
            DevExpress.Xpf.LayoutControl.LayoutControl.SetAllowHorizontalSizing(to, DevExpress.Xpf.LayoutControl.LayoutControl.GetAllowHorizontalSizing(from));
            DevExpress.Xpf.LayoutControl.LayoutControl.SetAllowVerticalSizing(to, DevExpress.Xpf.LayoutControl.LayoutControl.GetAllowVerticalSizing(from));
            if (from.IsLayoutGroup())
            {
                ILayoutGroup group2 = (ILayoutGroup) from;
                Size actualMaxSize = group2.ActualMaxSize;
                to.MaxWidth = actualMaxSize.Width;
                to.MaxHeight = actualMaxSize.Height;
                to.HorizontalAlignment = group2.ActualHorizontalAlignment;
                to.VerticalAlignment = group2.ActualVerticalAlignment;
            }
            ILayoutGroup parent = from.Parent as ILayoutGroup;
            if (parent != null)
            {
                parent.CopyTabHeaderInfo(from, to);
            }
        }

        protected virtual ItemInfos CreateItemInfos(double availableLength, double itemSpace) => 
            new ItemInfos(availableLength, itemSpace);

        protected virtual void DefineItemSizerElements(FrameworkElements items, ItemAlignment alignment, FrameworkElement[] itemSizerElements)
        {
            for (int i = 0; i < items.Count; i++)
            {
                FrameworkElement item = items[i];
                ItemAlignment itemAlignment = this.GetItemAlignment(item);
                if ((itemAlignment == alignment) && this.IsItemSizable(item))
                {
                    int index = 1 + i;
                    if (itemAlignment == ItemAlignment.End)
                    {
                        index--;
                    }
                    else if ((i > 0) && (i == (items.Count - 1)))
                    {
                        if (itemSizerElements[index - 1] == null)
                        {
                            index--;
                        }
                        else if (itemAlignment == ItemAlignment.Stretch)
                        {
                            break;
                        }
                    }
                    itemSizerElements[index] ??= item;
                }
            }
        }

        public virtual Size GetActualMinOrMaxSize(FrameworkElements items, bool getMinSize, LayoutParametersBase parameters) => 
            (items.Count != 0) ? ((this.Model.CollapseMode != LayoutGroupCollapseMode.None) ? this.GetActualMinOrMaxSizeWhenCollapsed(items, getMinSize, parameters) : this.GetActualMinOrMaxSizeWhenUncollapsed(items, getMinSize, parameters)) : (getMinSize ? SizeHelper.Zero : SizeHelper.Infinite);

        protected virtual Size GetActualMinOrMaxSizeWhenCollapsed(FrameworkElements items, bool getMinSize, LayoutParametersBase parameters)
        {
            if (this.Model.CollapseMode == LayoutGroupCollapseMode.NoChildrenVisible)
            {
                return (getMinSize ? SizeHelper.Zero : SizeHelper.Infinite);
            }
            Size zero = SizeHelper.Zero;
            foreach (FrameworkElement element in items)
            {
                SizeHelper.UpdateMaxSize(ref zero, getMinSize ? this.GetItemMinSize(element, true) : this.GetItemMaxSize(element, true));
            }
            return zero;
        }

        protected virtual Size GetActualMinOrMaxSizeWhenUncollapsed(FrameworkElements items, bool getMinSize, LayoutParametersBase parameters)
        {
            double totalLength = getMinSize ? ((items.Count - 1) * parameters.ItemSpace) : double.PositiveInfinity;
            double totalWidth = 0.0;
            foreach (FrameworkElement element in items)
            {
                Size itemSize = getMinSize ? this.GetItemMinSize(element, true) : this.GetItemMaxSize(element, true);
                Size itemDesiredSize = this.GetItemDesiredSize(element);
                if (getMinSize && ((this.GetItemAlignment(element) != ItemAlignment.Stretch) && (this.GetItemLength(itemDesiredSize) != 0.0)))
                {
                    this.SetItemLength(ref itemSize, this.GetItemLength(itemDesiredSize));
                }
                this.UpdateTotalSize(ref totalLength, ref totalWidth, itemSize);
            }
            return this.GetItemSize(totalLength, totalWidth);
        }

        public virtual Rect GetClipBounds(FrameworkElement control, FrameworkElement child, FrameworkElement relativeTo) => 
            child.GetVisualBounds(relativeTo);

        public virtual HorizontalAlignment GetDesiredHorizontalAlignment(FrameworkElements items)
        {
            HorizontalAlignment left = HorizontalAlignment.Left;
            bool flag = (this.Model.CollapseMode == LayoutGroupCollapseMode.OneChildVisible) || (this.Model.Orientation == Orientation.Vertical);
            bool flag2 = true;
            foreach (FrameworkElement element in items)
            {
                HorizontalAlignment itemHorizontalAlignment = this.GetItemHorizontalAlignment(element);
                if ((flag && (itemHorizontalAlignment == HorizontalAlignment.Stretch)) || (!flag && (!flag2 && (itemHorizontalAlignment != left))))
                {
                    left = HorizontalAlignment.Stretch;
                    break;
                }
                if (flag2)
                {
                    flag2 = false;
                    if (!flag)
                    {
                        left = itemHorizontalAlignment;
                    }
                }
            }
            if ((left == HorizontalAlignment.Stretch) && ((this.Model.CollapseMode == LayoutGroupCollapseMode.NoChildrenVisible) && (this.Model.CollapseDirection == Orientation.Horizontal)))
            {
                left = HorizontalAlignment.Left;
            }
            return left;
        }

        public virtual VerticalAlignment GetDesiredVerticalAlignment(FrameworkElements items)
        {
            VerticalAlignment top = VerticalAlignment.Top;
            bool flag = (this.Model.CollapseMode == LayoutGroupCollapseMode.OneChildVisible) || (this.Model.Orientation == Orientation.Horizontal);
            bool flag2 = true;
            foreach (FrameworkElement element in items)
            {
                VerticalAlignment itemVerticalAlignment = this.GetItemVerticalAlignment(element);
                if ((flag && (itemVerticalAlignment == VerticalAlignment.Stretch)) || (!flag && (!flag2 && (itemVerticalAlignment != top))))
                {
                    top = VerticalAlignment.Stretch;
                    break;
                }
                if (flag2)
                {
                    flag2 = false;
                    if (!flag)
                    {
                        top = itemVerticalAlignment;
                    }
                }
            }
            if ((top == VerticalAlignment.Stretch) && ((this.Model.CollapseMode == LayoutGroupCollapseMode.NoChildrenVisible) && (this.Model.CollapseDirection == Orientation.Vertical)))
            {
                top = VerticalAlignment.Top;
            }
            return top;
        }

        protected virtual void GetExternalInsertionPoints(ILayoutGroup control, FrameworkElement element, FrameworkElement originalDestinationItem, LayoutItemInsertionKind insertionKind, LayoutItemInsertionPoints points)
        {
            ILayoutGroup parent = control.Control.Parent as ILayoutGroup;
            if (control.IsBorderless && parent.IsChildBorderless(control))
            {
                parent.GetInsertionPoints(element, control.Control, originalDestinationItem, insertionKind, points);
            }
            else
            {
                points.Add(new LayoutItemInsertionPoint(control.Control, true));
            }
        }

        public virtual LayoutItemInsertionInfo GetInsertionInfoForEmptyArea(ILayoutGroup control, FrameworkElement element, Point p)
        {
            FrameworkElement element2;
            LayoutItemInsertionInfo info = new LayoutItemInsertionInfo();
            this.CalculateInsertionInfoForEmptyArea(control, control.ClientBounds, p, control.GetLogicalChildren(true), out info.InsertionKind, out element2);
            info.DestinationItem = element2;
            LayoutItemInsertionPoints points = new LayoutItemInsertionPoints();
            this.GetInsertionPoints(control, element, info.DestinationItem, info.DestinationItem, info.InsertionKind, points);
            LayoutItemInsertionPoint point = points.Find(element2);
            point ??= ((points.Count > 0) ? points[points.Count - 1] : null);
            info.InsertionPoint = point;
            return info;
        }

        public virtual LayoutItemInsertionKind GetInsertionKind(ILayoutGroup control, FrameworkElement destinationItem, Point p)
        {
            if (!ReferenceEquals(destinationItem, control) || !control.IsBorderless)
            {
                for (LayoutItemInsertionKind kind = LayoutItemInsertionKind.Left; kind <= LayoutItemInsertionKind.Bottom; kind += 1)
                {
                    Rect rect = this.GetInsertionZoneBounds(control, destinationItem, kind);
                    if (rect.Contains(p))
                    {
                        return kind;
                    }
                }
            }
            return LayoutItemInsertionKind.None;
        }

        protected virtual LayoutItemInsertionKind GetInsertionKindForEmptyArea(Rect itemBounds, Point p)
        {
            LayoutItemInsertionKind result = LayoutItemInsertionKind.None;
            Action action = delegate {
                if (p.X < itemBounds.Left)
                {
                    result = LayoutItemInsertionKind.Left;
                }
                if (p.X >= itemBounds.Right)
                {
                    result = LayoutItemInsertionKind.Right;
                }
            };
            Action action2 = delegate {
                if (p.Y < itemBounds.Top)
                {
                    result = LayoutItemInsertionKind.Top;
                }
                if (p.Y >= itemBounds.Bottom)
                {
                    result = LayoutItemInsertionKind.Bottom;
                }
            };
            if ((this.Model.CollapseMode == LayoutGroupCollapseMode.None) && (this.Model.Orientation == Orientation.Horizontal))
            {
                action2();
                action();
            }
            else
            {
                action();
                action2();
            }
            return result;
        }

        public virtual void GetInsertionPoints(ILayoutGroup control, FrameworkElement element, FrameworkElement destinationItem, FrameworkElement originalDestinationItem, LayoutItemInsertionKind insertionKind, LayoutItemInsertionPoints points)
        {
            if (this.IsInsertionPoint(control, element, destinationItem, originalDestinationItem, insertionKind))
            {
                points.Add(new LayoutItemInsertionPoint(destinationItem, insertionKind == LayoutItemInsertionKind.Inside));
            }
            if (this.IsExternalInsertionPoint(control, element, destinationItem, insertionKind))
            {
                this.GetExternalInsertionPoints(control, element, originalDestinationItem, insertionKind, points);
            }
        }

        public virtual Rect GetInsertionPointZoneBounds(ILayoutGroup control, FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind, int pointIndex, int pointCount)
        {
            Rect rect = this.GetInsertionZoneBounds(control, destinationItem, insertionKind);
            if (insertionKind == LayoutItemInsertionKind.Inside)
            {
                return rect;
            }
            if (insertionKind.GetSide() == null)
            {
                return Rect.Empty;
            }
            DevExpress.Xpf.Core.Side side = insertionKind.GetSide().Value;
            double num = (side.GetOrientation() == Orientation.Horizontal) ? rect.Height : rect.Width;
            double num2 = Math.Floor((double) (num / ((double) pointCount)));
            Rect rect2 = rect;
            RectHelper.Inflate(ref rect2, side, -((pointCount - 1) - pointIndex) * num2);
            if (pointIndex == 0)
            {
                num2 += num - (pointCount * num2);
            }
            switch (side)
            {
                case DevExpress.Xpf.Core.Side.Left:
                    rect2.Width = num2;
                    break;

                case DevExpress.Xpf.Core.Side.Top:
                    rect2.Height = num2;
                    break;

                case DevExpress.Xpf.Core.Side.LeftRight:
                    RectHelper.SetLeft(ref rect2, rect2.Right - num2);
                    break;

                case DevExpress.Xpf.Core.Side.Bottom:
                    RectHelper.SetTop(ref rect2, rect2.Bottom - num2);
                    break;

                default:
                    break;
            }
            return rect2;
        }

        protected virtual Rect GetInsertionZoneBounds(ILayoutGroup control, FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind)
        {
            Rect empty = Rect.Empty;
            if (ReferenceEquals(destinationItem, control) && (!control.IsBorderless && (control.CollapseMode != LayoutGroupCollapseMode.NoChildrenVisible)))
            {
                empty = control.ContentBounds;
            }
            return this.GetInsertionZoneBounds(RectHelper.New(destinationItem.GetSize()), empty, insertionKind);
        }

        protected virtual Rect GetInsertionZoneBounds(Rect bounds, Rect contentBounds, LayoutItemInsertionKind insertionKind)
        {
            if (insertionKind == LayoutItemInsertionKind.Inside)
            {
                return (contentBounds.IsEmpty ? bounds : contentBounds);
            }
            if (insertionKind.GetSide() == null)
            {
                return Rect.Empty;
            }
            DevExpress.Xpf.Core.Side side = insertionKind.GetSide().Value;
            Rect rect = bounds;
            if (rect.Width > rect.Height)
            {
                if (contentBounds.IsEmpty)
                {
                    double num = Math.Ceiling((double) (0.25 * bounds.Width));
                    contentBounds = new Rect();
                    contentBounds.X = bounds.Left + num;
                    contentBounds.Width = bounds.Width - (2.0 * num);
                    contentBounds.Y = Math.Floor((double) ((bounds.Top + bounds.Bottom) / 2.0));
                    contentBounds.Height = 0.0;
                }
                if (side == DevExpress.Xpf.Core.Side.Left)
                {
                    RectHelper.SetRight(ref rect, contentBounds.Left);
                }
                else if (side == DevExpress.Xpf.Core.Side.LeftRight)
                {
                    RectHelper.SetLeft(ref rect, contentBounds.Right);
                }
                else
                {
                    rect.X = contentBounds.X;
                    rect.Width = contentBounds.Width;
                    if (side == DevExpress.Xpf.Core.Side.Top)
                    {
                        RectHelper.SetBottom(ref rect, contentBounds.Top);
                    }
                    else
                    {
                        RectHelper.SetTop(ref rect, contentBounds.Bottom);
                    }
                }
            }
            else
            {
                if (contentBounds.IsEmpty)
                {
                    contentBounds = new Rect();
                    contentBounds.X = Math.Floor((double) ((bounds.Left + bounds.Right) / 2.0));
                    contentBounds.Width = 0.0;
                    double num2 = Math.Ceiling((double) (0.25 * bounds.Height));
                    contentBounds.Y = bounds.Top + num2;
                    contentBounds.Height = bounds.Height - (2.0 * num2);
                }
                if (side == DevExpress.Xpf.Core.Side.Top)
                {
                    RectHelper.SetBottom(ref rect, contentBounds.Top);
                }
                else if (side == DevExpress.Xpf.Core.Side.Bottom)
                {
                    RectHelper.SetTop(ref rect, contentBounds.Bottom);
                }
                else
                {
                    rect.Y = contentBounds.Y;
                    rect.Height = contentBounds.Height;
                    if (side == DevExpress.Xpf.Core.Side.Left)
                    {
                        RectHelper.SetRight(ref rect, contentBounds.Left);
                    }
                    else
                    {
                        RectHelper.SetLeft(ref rect, contentBounds.Right);
                    }
                }
            }
            return rect;
        }

        protected internal ItemAlignment GetItemAlignment(FrameworkElement item) => 
            this.GetItemAlignment(item, this.Model.Orientation == Orientation.Horizontal);

        protected internal ItemAlignment GetItemAlignment(FrameworkElement item, bool horizontal) => 
            !horizontal ? this.GetItemVerticalAlignment(item).GetItemAlignment() : this.GetItemHorizontalAlignment(item).GetItemAlignment();

        protected Size GetItemDesiredSize(FrameworkElement item) => 
            item.IsLayoutGroup() ? ((ILayoutGroup) item).ActualDesiredSize : item.GetDesiredSize();

        public virtual HorizontalAlignment GetItemHorizontalAlignment(FrameworkElement item)
        {
            HorizontalAlignment alignment = item.IsLayoutGroup() ? ((ILayoutGroup) item).ActualHorizontalAlignment : item.HorizontalAlignment;
            if ((alignment == HorizontalAlignment.Stretch) && !double.IsNaN(item.Width))
            {
                alignment = (this.Model.Orientation == Orientation.Horizontal) ? HorizontalAlignment.Left : HorizontalAlignment.Center;
            }
            return alignment;
        }

        protected double GetItemLength(Rect itemBounds) => 
            this.GetItemLength(itemBounds.Size());

        protected double GetItemLength(Size itemSize) => 
            (this.Model.Orientation == Orientation.Horizontal) ? itemSize.Width : itemSize.Height;

        protected Size GetItemMaxSize(FrameworkElement item, bool getActualMaxSize) => 
            this.GetItemMinOrMaxSize(item, (!getActualMaxSize || !item.IsLayoutGroup()) ? item.GetMaxSize() : ((ILayoutGroup) item).ActualMaxSize);

        private Size GetItemMinOrMaxSize(FrameworkElement item, Size originalMinOrMaxSize)
        {
            Size size = originalMinOrMaxSize;
            if (!double.IsNaN(item.Width))
            {
                size.Width = item.GetRealWidth();
            }
            if (!double.IsNaN(item.Height))
            {
                size.Height = item.GetRealHeight();
            }
            SizeHelper.Inflate(ref size, item.Margin);
            return size;
        }

        protected Size GetItemMinSize(FrameworkElement item, bool getActualMinSize) => 
            this.GetItemMinOrMaxSize(item, (!getActualMinSize || !item.IsLayoutGroup()) ? item.GetMinSize() : ((ILayoutGroup) item).ActualMinSize);

        protected double GetItemOffset(Rect itemBounds) => 
            (this.Model.Orientation == Orientation.Horizontal) ? itemBounds.X : itemBounds.Y;

        protected double GetItemOffsetPlusLength(Rect itemBounds) => 
            this.GetItemOffset(itemBounds) + this.GetItemLength(itemBounds);

        protected ItemAlignment GetItemOrthogonalAlignment(FrameworkElement item) => 
            (this.Model.Orientation != Orientation.Horizontal) ? this.GetItemHorizontalAlignment(item).GetItemAlignment() : this.GetItemVerticalAlignment(item).GetItemAlignment();

        protected Size GetItemSize(double itemLength, double itemWidth)
        {
            Size itemSize = new Size();
            this.SetItemLength(ref itemSize, itemLength);
            this.SetItemWidth(ref itemSize, itemWidth);
            return itemSize;
        }

        protected DevExpress.Xpf.Core.Side GetItemSizerSide(bool isStartSizer) => 
            (this.Model.Orientation != Orientation.Horizontal) ? (isStartSizer ? DevExpress.Xpf.Core.Side.Top : DevExpress.Xpf.Core.Side.Bottom) : (isStartSizer ? DevExpress.Xpf.Core.Side.Left : DevExpress.Xpf.Core.Side.LeftRight);

        public virtual VerticalAlignment GetItemVerticalAlignment(FrameworkElement item)
        {
            VerticalAlignment alignment = item.IsLayoutGroup() ? ((ILayoutGroup) item).ActualVerticalAlignment : item.VerticalAlignment;
            if ((alignment == VerticalAlignment.Stretch) && !double.IsNaN(item.Height))
            {
                alignment = (this.Model.Orientation == Orientation.Horizontal) ? VerticalAlignment.Center : VerticalAlignment.Top;
            }
            return alignment;
        }

        protected double GetItemWidth(Size itemSize) => 
            (this.Model.Orientation == Orientation.Horizontal) ? itemSize.Height : itemSize.Width;

        public virtual void GetLayoutItems(FrameworkElements items, LayoutItems layoutItems)
        {
            foreach (FrameworkElement element in items)
            {
                if (!(element is LayoutItem))
                {
                    if (!element.IsLayoutGroup())
                    {
                        continue;
                    }
                    ((ILayoutGroup) element).GetLayoutItems(layoutItems);
                    continue;
                }
                LayoutItem layoutItem = (LayoutItem) element;
                if (this.CanAlignLayoutItemLabel(layoutItem))
                {
                    layoutItems.Add(layoutItem);
                }
            }
        }

        public void InitChildFromGroup(FrameworkElement child, FrameworkElement group)
        {
            ItemAlignment itemAlignment = this.GetItemAlignment(child);
            ItemAlignment alignment = this.GetItemAlignment(group);
            if ((alignment == ItemAlignment.Center) || (alignment == ItemAlignment.End))
            {
                this.SetItemAlignment(child, alignment);
            }
            else if ((itemAlignment == ItemAlignment.Center) || (itemAlignment == ItemAlignment.End))
            {
                this.SetItemAlignment(child, ItemAlignment.Start);
            }
        }

        public virtual void InitGroupForChild(LayoutGroup group, FrameworkElement child)
        {
            Func<ItemAlignment, bool> func = <>c.<>9__16_0 ??= alignment => ((alignment == ItemAlignment.Center) || (alignment == ItemAlignment.End));
            if (((this.Model.CollapseMode != LayoutGroupCollapseMode.None) || (this.Model.Orientation == Orientation.Horizontal)) && func(this.GetItemHorizontalAlignment(child).GetItemAlignment()))
            {
                group.HorizontalAlignment = child.HorizontalAlignment;
            }
            if (((this.Model.CollapseMode != LayoutGroupCollapseMode.None) || (this.Model.Orientation == Orientation.Vertical)) && func(this.GetItemVerticalAlignment(child).GetItemAlignment()))
            {
                group.VerticalAlignment = child.VerticalAlignment;
            }
        }

        protected virtual void InitInsertedElement(FrameworkElement element, FrameworkElement insertionPoint)
        {
            ItemAlignment itemAlignment = this.GetItemAlignment(insertionPoint);
            if ((itemAlignment != ItemAlignment.Start) && (itemAlignment != ItemAlignment.Stretch))
            {
                this.SetItemAlignment(element, itemAlignment);
            }
            else
            {
                ItemAlignment alignment2 = this.GetItemAlignment(element);
                if ((alignment2 != ItemAlignment.Start) && (alignment2 != ItemAlignment.Stretch))
                {
                    this.SetItemAlignment(element, ItemAlignment.Start);
                }
            }
        }

        public virtual void InsertElement(ILayoutGroup control, FrameworkElement element, LayoutItemInsertionPoint insertionPoint, LayoutItemInsertionKind insertionKind)
        {
            DevExpress.Xpf.Core.Side? side = insertionKind.GetSide();
            if (ReferenceEquals(insertionPoint.Element, control) && insertionPoint.IsInternalInsertion)
            {
                if (insertionKind == LayoutItemInsertionKind.Inside)
                {
                    Orientation orientation = (element.Parent is ILayoutGroup) ? ((ILayoutGroup) element.Parent).Orientation : this.Model.Orientation;
                    control.Root.AvailableItems.Remove(element);
                    element.SetParent(control.Control);
                    this.Model.Orientation = orientation;
                    return;
                }
                if (side != null)
                {
                    ILayoutGroup group = control.MoveChildrenToNewGroup();
                    DevExpress.Xpf.LayoutControl.LayoutControl.SetIsUserDefined(group.Control, true);
                    insertionPoint.Element = group.Control;
                    insertionPoint.IsInternalInsertion = false;
                    this.Model.Orientation = this.Model.Orientation.OrthogonalValue();
                }
            }
            if (side != null)
            {
                if ((this.Model.CollapseMode != LayoutGroupCollapseMode.None) || (this.Model.Orientation == side.Value.GetOrientation()))
                {
                    ILayoutGroup group2 = control.MoveChildToNewGroup(insertionPoint.Element);
                    DevExpress.Xpf.LayoutControl.LayoutControl.SetIsUserDefined(group2.Control, true);
                    group2.Orientation = side.Value.GetOrientation().OrthogonalValue();
                    group2.InsertElement(element, insertionPoint, insertionKind);
                }
                else
                {
                    control.Root.AvailableItems.Remove(element);
                    element.SetParent(null);
                    int index = control.Children.IndexOf(insertionPoint.Element);
                    if (side.Value.IsEnd())
                    {
                        index++;
                    }
                    control.Children.Insert(index, element);
                    this.InitInsertedElement(element, insertionPoint.Element);
                }
            }
        }

        public virtual bool IsContentEmpty(FrameworkElements items)
        {
            bool flag = items.Count == 0;
            if (!flag && (this.Model.CollapseMode == LayoutGroupCollapseMode.OneChildVisible))
            {
                foreach (FrameworkElement element in items)
                {
                    flag = element.IsLayoutGroup() && ((ILayoutGroup) element).IsUIEmpty;
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            return flag;
        }

        public virtual bool IsExternalInsertionPoint(ILayoutGroup control, FrameworkElement element, FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind)
        {
            if ((insertionKind.GetSide() == null) || (control.CollapseMode != LayoutGroupCollapseMode.None))
            {
                return false;
            }
            DevExpress.Xpf.Core.Side side = insertionKind.GetSide().Value;
            Rect bounds = destinationItem.GetBounds(control.Control);
            if (control.ChildAreaBounds.GetSideOffset(side) != bounds.GetSideOffset(side))
            {
                return false;
            }
            if (side.GetOrientation() != this.Model.Orientation)
            {
                return (control.IsBorderless && ((ILayoutGroup) control.Control.Parent).IsChildBorderless(control));
            }
            FrameworkElements logicalChildren = control.GetLogicalChildren(true);
            return (logicalChildren.Contains(element) ? (logicalChildren.Count >= 3) : (logicalChildren.Count >= 2));
        }

        protected virtual bool IsInsertionPoint(ILayoutGroup control, FrameworkElement element, FrameworkElement destinationItem, FrameworkElement originalDestinationItem, LayoutItemInsertionKind insertionKind)
        {
            if (ReferenceEquals(destinationItem, element))
            {
                return false;
            }
            if ((insertionKind == LayoutItemInsertionKind.Inside) && (ReferenceEquals(destinationItem, control) || destinationItem.IsLayoutGroup()))
            {
                return true;
            }
            if (control.CollapseMode != LayoutGroupCollapseMode.None)
            {
                return true;
            }
            if (insertionKind.GetSide() == null)
            {
                return false;
            }
            DevExpress.Xpf.Core.Side side = insertionKind.GetSide().Value;
            if (side.GetOrientation() != this.Model.Orientation)
            {
                FrameworkElements arrangedLogicalChildren = control.GetArrangedLogicalChildren(true);
                int index = arrangedLogicalChildren.IndexOf(element);
                if (index != -1)
                {
                    int num2 = arrangedLogicalChildren.IndexOf(destinationItem);
                    if ((side.IsStart() && (num2 == (index + 1))) || (side.IsEnd() && (num2 == (index - 1))))
                    {
                        return false;
                    }
                }
            }
            if (!destinationItem.IsLayoutGroup())
            {
                return true;
            }
            ILayoutGroup group = (ILayoutGroup) destinationItem;
            if (side.GetOrientation() != group.Orientation)
            {
                return (ReferenceEquals(destinationItem, originalDestinationItem) ? (!group.GetLogicalChildren(true).Contains(element) || !group.IsExternalInsertionPoint(element, element, insertionKind)) : false);
            }
            if (ReferenceEquals(destinationItem, originalDestinationItem))
            {
                return true;
            }
            FrameworkElements logicalChildren = group.GetLogicalChildren(true);
            return (!logicalChildren.Contains(element) || ((logicalChildren.Count > 2) || ReferenceEquals(originalDestinationItem, element)));
        }

        protected virtual bool IsItemSizable(FrameworkElement item) => 
            (this.Model.Orientation != Orientation.Horizontal) ? ((item.MinHeight < item.MaxHeight) && DevExpress.Xpf.LayoutControl.LayoutControl.GetAllowVerticalSizing(item)) : ((item.MinWidth < item.MaxWidth) && DevExpress.Xpf.LayoutControl.LayoutControl.GetAllowHorizontalSizing(item));

        internal static bool IsLayoutRepeated(List<List<LayoutItems>> previousColumnsList, List<LayoutItems> columns)
        {
            bool flag;
            if (!DetectCyclicColumnsAlignment)
            {
                return false;
            }
            using (List<List<LayoutItems>>.Enumerator enumerator = previousColumnsList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        List<LayoutItems> current = enumerator.Current;
                        if (!AreColumnsListsEqual(current, columns))
                        {
                            continue;
                        }
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

        public virtual bool IsLayoutUniform(FrameworkElements items)
        {
            bool flag;
            if (this.Model.CollapseMode != LayoutGroupCollapseMode.None)
            {
                return true;
            }
            ItemAlignment? nullable = null;
            using (IEnumerator<FrameworkElement> enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FrameworkElement current = enumerator.Current;
                        ItemAlignment itemAlignment = this.GetItemAlignment(current);
                        if (itemAlignment == ItemAlignment.Stretch)
                        {
                            itemAlignment = ItemAlignment.Start;
                        }
                        if (nullable == null)
                        {
                            nullable = new ItemAlignment?(itemAlignment);
                        }
                        else
                        {
                            ItemAlignment? nullable2 = nullable;
                            if ((itemAlignment == ((ItemAlignment) nullable2.GetValueOrDefault())) ? (nullable2 == null) : true)
                            {
                                flag = false;
                                break;
                            }
                        }
                        continue;
                    }
                    return true;
                }
            }
            return flag;
        }

        private void LayoutGroup_LayoutUpdated(object sender, EventArgs e)
        {
            LayoutGroup model = this.Model as LayoutGroup;
            if (model != null)
            {
                model.LayoutUpdated -= new EventHandler(this.LayoutGroup_LayoutUpdated);
            }
            this.previousColumnsList.Clear();
            this.repeatCount = 0;
        }

        protected virtual void MeasureItem(FrameworkElement item, Size maxSize)
        {
            Size itemMinSize = this.GetItemMinSize(item, false);
            this.SetItemLength(ref maxSize, Math.Max(this.GetItemLength(itemMinSize), this.GetItemLength(maxSize)));
            if (this.GetItemOrthogonalAlignment(item) == ItemAlignment.Stretch)
            {
                this.SetItemWidth(ref maxSize, Math.Max(this.GetItemWidth(itemMinSize), this.GetItemWidth(maxSize)));
            }
            else
            {
                this.SetItemWidth(ref maxSize, double.PositiveInfinity);
            }
            item.Measure(maxSize);
        }

        protected void MeasureItem(FrameworkElement item, Size maxSize, ref double totalLength, ref double totalWidth)
        {
            this.MeasureItem(item, maxSize);
            this.UpdateTotalSize(ref totalLength, ref totalWidth, this.GetItemDesiredSize(item));
        }

        protected virtual void MeasureStretchedItems(FrameworkElements stretchedItems, double availableLength, double availableWidth, ref double totalLength, ref double totalWidth)
        {
            if (double.IsInfinity(availableLength))
            {
                Size itemSize = this.GetItemSize(availableLength, availableWidth);
                foreach (FrameworkElement element in stretchedItems)
                {
                    this.MeasureItem(element, itemSize, ref totalLength, ref totalWidth);
                }
            }
            else
            {
                StretchedLengthsCalculator calculator = new StretchedLengthsCalculator(availableLength);
                foreach (FrameworkElement element2 in stretchedItems)
                {
                    calculator.Add(new DevExpress.Xpf.LayoutControl.ItemInfo(0.0, this.GetItemLength(this.GetItemMinSize(element2, true)), this.GetItemLength(this.GetItemMaxSize(element2, true)), ItemAlignment.Stretch));
                }
                calculator.Calculate();
                bool flag = false;
                int num = 0;
                while (true)
                {
                    if (num >= stretchedItems.Count)
                    {
                        if (flag && !calculator.NeedsMoreLength)
                        {
                            this.UpdateTotalSize(ref totalLength, ref totalWidth, this.GetItemSize(availableLength, 0.0));
                        }
                        else
                        {
                            double num3 = 0.0;
                            foreach (FrameworkElement element3 in stretchedItems)
                            {
                                num3 = Math.Max(num3, this.GetItemLength(this.GetItemDesiredSize(element3)));
                            }
                            double itemLength = 0.0;
                            int num5 = 0;
                            while (true)
                            {
                                if (num5 >= stretchedItems.Count)
                                {
                                    this.UpdateTotalSize(ref totalLength, ref totalWidth, this.GetItemSize(itemLength, 0.0));
                                    break;
                                }
                                if ((calculator[num5].Length < num3) && (calculator[num5].Length != calculator[num5].MaxLength))
                                {
                                    this.MeasureItem(stretchedItems[num5], this.GetItemSize(num3, availableWidth));
                                }
                                itemLength += Math.Min(num3, calculator[num5].MaxLength);
                                num5++;
                            }
                        }
                        foreach (FrameworkElement element4 in stretchedItems)
                        {
                            this.UpdateTotalSize(ref totalLength, ref totalWidth, this.GetItemSize(0.0, this.GetItemWidth(this.GetItemDesiredSize(element4))));
                        }
                        break;
                    }
                    double length = calculator[num].Length;
                    LayoutGroup model = this.Model as LayoutGroup;
                    if (model != null)
                    {
                        Func<DevExpress.Xpf.LayoutControl.LayoutControl, bool> evaluator = <>c.<>9__26_0;
                        if (<>c.<>9__26_0 == null)
                        {
                            Func<DevExpress.Xpf.LayoutControl.LayoutControl, bool> local1 = <>c.<>9__26_0;
                            evaluator = <>c.<>9__26_0 = x => x.UseContentMinSize;
                        }
                        if ((model.Root as DevExpress.Xpf.LayoutControl.LayoutControl).Return<DevExpress.Xpf.LayoutControl.LayoutControl, bool>(evaluator, <>c.<>9__26_1 ??= () => false))
                        {
                            length = double.PositiveInfinity;
                        }
                    }
                    this.MeasureItem(stretchedItems[num], this.GetItemSize(length, availableWidth));
                    flag = flag || (this.GetItemLength(this.GetItemDesiredSize(stretchedItems[num])) == calculator[num].Length);
                    num++;
                }
            }
        }

        protected virtual Size MeasureWhenCollapsed(FrameworkElements items, Size maxSize)
        {
            Size zero = SizeHelper.Zero;
            bool flag = this.Model.CollapseMode == LayoutGroupCollapseMode.NoChildrenVisible;
            if (flag)
            {
                maxSize = UIElementExtensions.InvisibleBounds.Size();
            }
            FrameworkElement uncollapsedChild = this.Model.UncollapsedChild;
            foreach (FrameworkElement element2 in items)
            {
                if (flag || (!this.Model.MeasureUncollapsedChildOnly || ReferenceEquals(element2, uncollapsedChild)))
                {
                    this.MeasureItem(element2, maxSize);
                    if (!flag)
                    {
                        SizeHelper.UpdateMaxSize(ref zero, this.GetItemDesiredSize(element2));
                    }
                }
            }
            return zero;
        }

        protected virtual Size MeasureWhenUncollapsed(FrameworkElements items, Size maxSize)
        {
            double num = (items.Count - 1) * this.Parameters.ItemSpace;
            this.SetItemLength(ref maxSize, Math.Max((double) 0.0, (double) (this.GetItemLength(maxSize) - num)));
            while (true)
            {
                Size itemSize = maxSize;
                this.SetItemLength(ref itemSize, double.PositiveInfinity);
                double totalLength = 0.0;
                double totalWidth = 0.0;
                FrameworkElements stretchedItems = new FrameworkElements();
                foreach (FrameworkElement element in items)
                {
                    if (this.GetItemAlignment(element) == ItemAlignment.Stretch)
                    {
                        stretchedItems.Add(element);
                        continue;
                    }
                    this.MeasureItem(element, itemSize, ref totalLength, ref totalWidth);
                }
                if (stretchedItems.Count != 0)
                {
                    this.MeasureStretchedItems(stretchedItems, Math.Max((double) 0.0, (double) (this.GetItemLength(maxSize) - totalLength)), this.GetItemWidth(maxSize), ref totalLength, ref totalWidth);
                }
                if (totalWidth <= this.GetItemWidth(maxSize))
                {
                    return this.GetItemSize((totalLength != 0.0) ? (totalLength + num) : 0.0, totalWidth);
                }
                this.SetItemWidth(ref maxSize, totalWidth);
            }
        }

        protected override Size OnArrange(FrameworkElements items, Rect bounds, Rect viewPortBounds) => 
            (this.Model.CollapseMode != LayoutGroupCollapseMode.None) ? this.ArrangeWhenCollapsed(items, bounds) : this.ArrangeWhenUncollapsed(items, bounds);

        protected override Size OnMeasure(FrameworkElements items, Size maxSize) => 
            (this.Model.CollapseMode != LayoutGroupCollapseMode.None) ? this.MeasureWhenCollapsed(items, maxSize) : this.MeasureWhenUncollapsed(items, maxSize);

        protected void SetItemAlignment(FrameworkElement item, ItemAlignment alignment)
        {
            if (this.Model.Orientation == Orientation.Horizontal)
            {
                this.SetItemHorizontalAlignment(item, alignment.GetHorizontalAlignment(), false);
            }
            else
            {
                this.SetItemVerticalAlignment(item, alignment.GetVerticalAlignment(), false);
            }
        }

        public virtual void SetItemHorizontalAlignment(FrameworkElement item, HorizontalAlignment value, bool updateWidth)
        {
            bool flag = item.IsPropertyAssigned(FrameworkElement.HorizontalAlignmentProperty);
            HorizontalAlignment horizontalAlignment = item.HorizontalAlignment;
            if (item.IsLayoutGroup() && (value == ((ILayoutGroup) item).DesiredHorizontalAlignment))
            {
                item.ClearValue(FrameworkElement.HorizontalAlignmentProperty);
            }
            else
            {
                item.HorizontalAlignment = value;
            }
            if (item.IsLayoutGroup() && ((item.HorizontalAlignment == horizontalAlignment) && (item.IsPropertyAssigned(FrameworkElement.HorizontalAlignmentProperty) != flag)))
            {
                ((ILayoutGroup) item.Parent).ChildHorizontalAlignmentChanged(item);
            }
            if (updateWidth && ((value == HorizontalAlignment.Stretch) && !double.IsNaN(item.Width)))
            {
                item.Width = double.NaN;
            }
        }

        protected void SetItemLength(ref Rect itemBounds, double length)
        {
            Size itemSize = itemBounds.Size();
            this.SetItemLength(ref itemSize, length);
            RectHelper.SetSize(ref itemBounds, itemSize);
        }

        protected void SetItemLength(ref Size itemSize, double length)
        {
            length = Math.Max(0.0, length);
            if (this.Model.Orientation == Orientation.Horizontal)
            {
                itemSize.Width = length;
            }
            else
            {
                itemSize.Height = length;
            }
        }

        protected void SetItemOffset(ref Point itemLocation, double offset)
        {
            if (this.Model.Orientation == Orientation.Horizontal)
            {
                itemLocation.X = offset;
            }
            else
            {
                itemLocation.Y = offset;
            }
        }

        protected void SetItemOffset(ref Rect itemBounds, double offset)
        {
            Point itemLocation = itemBounds.Location();
            this.SetItemOffset(ref itemLocation, offset);
            RectHelper.SetLocation(ref itemBounds, itemLocation);
        }

        protected void SetItemOffsetPlusLength(ref Rect itemBounds, double offsetPlusLength)
        {
            this.SetItemLength(ref itemBounds, offsetPlusLength - this.GetItemOffset(itemBounds));
        }

        public virtual void SetItemVerticalAlignment(FrameworkElement item, VerticalAlignment value, bool updateHeight)
        {
            bool flag = item.IsPropertyAssigned(FrameworkElement.VerticalAlignmentProperty);
            VerticalAlignment verticalAlignment = item.VerticalAlignment;
            if (item.IsLayoutGroup() && (value == ((ILayoutGroup) item).DesiredVerticalAlignment))
            {
                item.ClearValue(FrameworkElement.VerticalAlignmentProperty);
            }
            else
            {
                item.VerticalAlignment = value;
            }
            if (item.IsLayoutGroup() && ((item.VerticalAlignment == verticalAlignment) && (item.IsPropertyAssigned(FrameworkElement.VerticalAlignmentProperty) != flag)))
            {
                ((ILayoutGroup) item.Parent).ChildVerticalAlignmentChanged(item);
            }
            if (updateHeight && ((value == VerticalAlignment.Stretch) && !double.IsNaN(item.Height)))
            {
                item.Height = double.NaN;
            }
        }

        protected void SetItemWidth(ref Size itemSize, double width)
        {
            if (this.Model.Orientation == Orientation.Horizontal)
            {
                itemSize.Height = width;
            }
            else
            {
                itemSize.Width = width;
            }
        }

        protected virtual void SetLabelWidth(LayoutItems layoutItems, double labelWidth)
        {
            foreach (LayoutItem item in layoutItems)
            {
                item.LabelWidth = labelWidth;
            }
        }

        protected virtual void UpdateTotalSize(ref double totalLength, ref double totalWidth, Size itemSize)
        {
            totalLength += this.GetItemLength(itemSize);
            totalWidth = Math.Max(totalWidth, this.GetItemWidth(itemSize));
        }

        public static bool DetectCyclicColumnsAlignment { get; set; }

        protected ILayoutGroupModel Model =>
            (ILayoutGroupModel) base.Model;

        protected LayoutGroupParameters Parameters =>
            (LayoutGroupParameters) base.Parameters;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutGroupProvider.<>c <>9 = new LayoutGroupProvider.<>c();
            public static Func<ItemAlignment, bool> <>9__16_0;
            public static Func<DevExpress.Xpf.LayoutControl.LayoutControl, bool> <>9__26_0;
            public static Func<bool> <>9__26_1;

            internal bool <InitGroupForChild>b__16_0(ItemAlignment alignment) => 
                (alignment == ItemAlignment.Center) || (alignment == ItemAlignment.End);

            internal bool <MeasureStretchedItems>b__26_0(DevExpress.Xpf.LayoutControl.LayoutControl x) => 
                x.UseContentMinSize;

            internal bool <MeasureStretchedItems>b__26_1() => 
                false;
        }
    }
}

