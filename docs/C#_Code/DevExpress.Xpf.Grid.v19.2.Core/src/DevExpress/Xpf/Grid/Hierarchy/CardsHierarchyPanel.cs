namespace DevExpress.Xpf.Grid.Hierarchy
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CardsHierarchyPanel : HierarchyPanel
    {
        public static readonly DependencyProperty CardMarginProperty;
        public static readonly DependencyProperty ContainerIndentProperty;
        public static readonly DependencyProperty FixedSizeProperty;
        public static readonly DependencyProperty SeparatorThicknessProperty;
        public static readonly DependencyProperty MaxCardCountInRowProperty;
        public static readonly DependencyProperty CardAlignmentProperty;
        private static readonly DependencyPropertyKey SeparatorInfoPropertyKey;
        public static readonly DependencyProperty SeparatorInfoProperty;
        private Size CardMarginCached;
        private List<FrameworkElement> hiddenItems;
        private int tryCount;
        private double? rest;
        private int actualGenerateItemsOffset;
        private double oldOffset;
        private int scrollDiff;
        private int? lastRowIndex;
        private int? visibleIndex;
        private int? previousGenerateOffset;
        private int rowCardCount;

        static CardsHierarchyPanel()
        {
            Thickness defaultValue = new Thickness();
            CardMarginProperty = DependencyProperty.Register("CardMargin", typeof(Thickness), typeof(CardsHierarchyPanel), new FrameworkPropertyMetadata(defaultValue, (d, e) => ((CardsHierarchyPanel) d).OnCardMarginChanged()));
            ContainerIndentProperty = DependencyProperty.Register("ContainerIndent", typeof(double), typeof(CardsHierarchyPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            FixedSizeProperty = DependencyProperty.Register("FixedSize", typeof(double), typeof(CardsHierarchyPanel), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            SeparatorThicknessProperty = DependencyProperty.Register("SeparatorThickness", typeof(double), typeof(CardsHierarchyPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            MaxCardCountInRowProperty = DependencyProperty.Register("MaxCardCountInRow", typeof(int), typeof(CardsHierarchyPanel), new FrameworkPropertyMetadata(0x7fffffff, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CardAlignmentProperty = DependencyProperty.Register("CardAlignment", typeof(Alignment), typeof(CardsHierarchyPanel), new FrameworkPropertyMetadata(Alignment.Near, FrameworkPropertyMetadataOptions.AffectsArrange));
            SeparatorInfoPropertyKey = DependencyProperty.RegisterReadOnly("SeparatorInfo", typeof(IList<DevExpress.Xpf.Grid.Hierarchy.SeparatorInfo>), typeof(CardsHierarchyPanel), new PropertyMetadata(null));
            SeparatorInfoProperty = SeparatorInfoPropertyKey.DependencyProperty;
        }

        private void AdjustScrollBar(double value)
        {
            this.oldOffset = value;
            base.DataPresenter.SetDefineScrollOffset(this.oldOffset, false);
            this.rest = null;
        }

        private void AdjustScrollBarByDelta(double delta)
        {
            this.AdjustScrollBar(Math.Round((double) (base.DataPresenter.ActualScrollOffset + delta), 4));
        }

        protected override Size ArrangeItemsContainer(double offset, Size availableSize, IItemsContainer itemsContainer, int level)
        {
            this.UpdateSeparatorInfo(availableSize);
            if (itemsContainer == null)
            {
                return new Size();
            }
            double num = offset;
            foreach (CardRowInfo info in this.RowsInfo)
            {
                if (base.SizeHelper.GetDefineSize(info.RenderSize) > 0.0)
                {
                    num = offset;
                }
                double arrangeSecondaryOffset = this.GetArrangeSecondaryOffset(info, availableSize);
                foreach (IItem item in info.Elements)
                {
                    Point location = base.SizeHelper.CreatePoint(num + this.GetRowOffset(), arrangeSecondaryOffset);
                    double defineSize = base.SizeHelper.GetDefineSize(info.RenderSize);
                    if (!item.IsItemsContainer)
                    {
                        location.Offset(this.CardMargin.Left, this.CardMargin.Top);
                        defineSize = Math.Max((double) 0.0, (double) (defineSize - base.SizeHelper.GetDefineSize(new Size(this.CardMargin.Left, this.CardMargin.Top))));
                        if (info.HasSeparator)
                        {
                            defineSize = Math.Max((double) 0.0, (double) (defineSize - Math.Min(this.SeparatorThickness, (this.SeparatorThickness + base.SizeHelper.GetDefineSize(info.Size)) - base.SizeHelper.GetDefineSize(info.RenderSize))));
                        }
                        defineSize = Math.Max((double) 0.0, (double) (defineSize - Math.Max((double) 0.0, (double) (base.SizeHelper.GetDefineSize(new Size(this.CardMargin.Right, this.CardMargin.Bottom)) - (base.SizeHelper.GetDefineSize(info.Size) - base.SizeHelper.GetDefineSize(info.RenderSize))))));
                    }
                    double secondarySize = this.GetSecondarySize(item, availableSize, info);
                    Size size = base.SizeHelper.CreateSize(defineSize, secondarySize);
                    base.ArrangeItem(item, new Rect(location, size), base.SizeHelper.GetDefineSize(size) > 0.0, info.Level);
                    arrangeSecondaryOffset += secondarySize;
                    if (!item.IsItemsContainer)
                    {
                        arrangeSecondaryOffset += base.SizeHelper.GetSecondarySize(this.CardMarginCached);
                    }
                }
                offset += base.SizeHelper.GetDefineSize(info.RenderSize);
                num += base.SizeHelper.GetDefineSize(info.Size);
            }
            foreach (UIElement element in this.hiddenItems)
            {
                element.Arrange(new Rect(new Point(double.MinValue, double.MinValue), element.DesiredSize));
            }
            return itemsContainer.DesiredSize;
        }

        internal double CalcExtent(double defineSize, int? visibleIndex = new int?())
        {
            defineSize -= this.GetRowOffset();
            return ((base.DataPresenter != null) ? (base.AllowPerPixelScrolling ? ((this.GroupInfo.Count != 0) ? this.CalcExtentGrouped(defineSize, visibleIndex) : this.CalcExtentUngrouped(defineSize, visibleIndex)) : ((visibleIndex != null) ? ((double) visibleIndex.Value) : ((double) base.DataPresenter.ItemCount))) : 0.0);
        }

        private double CalcExtentGrouped(double defineSize, int? visibleIndex)
        {
            int num3;
            int num4;
            int? nullable;
            int num5;
            int num6;
            int actualScrollOffset = (int) base.DataPresenter.ActualScrollOffset;
            int startScrollIndex = this.NodeContainer.StartScrollIndex;
            this.FindGroupIndex(out num3, out num4);
            if (visibleIndex != null)
            {
                nullable = visibleIndex;
                num5 = startScrollIndex;
                if ((nullable.GetValueOrDefault() < num5) ? (nullable != null) : false)
                {
                    num6 = num3;
                    goto TR_0013;
                }
            }
            int num9 = 0;
            while (true)
            {
                if (num9 < this.RowsInfo.Count)
                {
                    CardRowInfo info2 = this.RowsInfo[num9];
                    if (defineSize > 0.0)
                    {
                        startScrollIndex += info2.Elements.Count;
                        nullable = visibleIndex;
                        num5 = startScrollIndex;
                        if ((nullable.GetValueOrDefault() < num5) ? (nullable != null) : false)
                        {
                            return (double) actualScrollOffset;
                        }
                        actualScrollOffset++;
                        if (startScrollIndex >= base.DataPresenter.ItemCount)
                        {
                            return (double) actualScrollOffset;
                        }
                        if (!info2.IsItemsContainer)
                        {
                            if (num4 == -1)
                            {
                                num4 = 0;
                            }
                            num4 += info2.Elements.Count;
                        }
                        else
                        {
                            if (num4 != -1)
                            {
                                num3++;
                                num4 = -1;
                            }
                            if (num3 >= this.GroupInfo.Count)
                            {
                                return (double) actualScrollOffset;
                            }
                            GroupRowInfo info3 = this.GroupInfo[num3];
                            if (!info3.Expanded)
                            {
                                for (int j = num3 + 1; (j < this.GroupInfo.Count) && ((this.GroupInfo[j].Level - 1) == info3.Level); j++)
                                {
                                    num3++;
                                }
                            }
                            if ((info3.Level == this.LastGroupLevel) && info3.Expanded)
                            {
                                num4 = 0;
                            }
                            else
                            {
                                num3++;
                            }
                        }
                        defineSize -= base.SizeHelper.GetDefineSize(info2.RenderSize);
                        num9++;
                        continue;
                    }
                }
                for (int i = num3; i < this.GroupInfo.Count; i++)
                {
                    GroupRowInfo info4 = this.GroupInfo[i];
                    if ((info4.ParentGroup == null) || info4.ParentGroup.Expanded)
                    {
                        if ((i > num3) || (num4 == -1))
                        {
                            actualScrollOffset++;
                        }
                        if (info4.Expanded && (info4.Level == this.LastGroupLevel))
                        {
                            int childControllerRowCount = info4.ChildControllerRowCount;
                            if ((i == num3) && (num4 > 0))
                            {
                                childControllerRowCount -= num4;
                            }
                            actualScrollOffset += (int) Math.Ceiling((double) (((double) childControllerRowCount) / ((double) this.rowCardCount)));
                        }
                    }
                }
                return (double) actualScrollOffset;
            }
        TR_0004:
            if ((num6 != num3) || (num4 != -1))
            {
                startScrollIndex--;
                actualScrollOffset--;
            }
            num6--;
        TR_0013:
            while (true)
            {
                if (num6 < 0)
                {
                    break;
                }
                GroupRowInfo info = this.GroupInfo[num6];
                nullable = visibleIndex;
                if ((startScrollIndex == nullable.GetValueOrDefault()) ? (nullable != null) : false)
                {
                    break;
                }
                if (info.Expanded && (info.Level == this.LastGroupLevel))
                {
                    int? nullable1;
                    int childControllerRowCount = info.ChildControllerRowCount;
                    if (num6 == num3)
                    {
                        childControllerRowCount = (num4 >= 0) ? num4 : 0;
                    }
                    int num8 = startScrollIndex;
                    int? nullable2 = visibleIndex;
                    if (nullable2 != null)
                    {
                        nullable1 = new int?(num8 - nullable2.GetValueOrDefault());
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    nullable = nullable1;
                    num5 = childControllerRowCount;
                    if (!((nullable.GetValueOrDefault() <= num5) ? (nullable != null) : false))
                    {
                        startScrollIndex -= childControllerRowCount;
                        actualScrollOffset -= (int) Math.Ceiling((double) (((double) childControllerRowCount) / ((double) this.rowCardCount)));
                        goto TR_0004;
                    }
                    else
                    {
                        actualScrollOffset -= (int) Math.Ceiling((double) (((double) (startScrollIndex - visibleIndex.Value)) / ((double) this.rowCardCount)));
                    }
                    break;
                }
                goto TR_0004;
            }
            return (double) actualScrollOffset;
        }

        private double CalcExtentSimple()
        {
            if (this.GroupInfo.Count == 0)
            {
                return (double) ((int) Math.Ceiling((double) (((double) base.DataPresenter.ItemCount) / ((double) this.rowCardCount))));
            }
            double num = 0.0;
            foreach (GroupRowInfo info in this.GroupInfo)
            {
                if ((info.ParentGroup == null) || info.ParentGroup.Expanded)
                {
                    num++;
                    if (info.Expanded && (info.Level == this.LastGroupLevel))
                    {
                        num += Math.Ceiling((double) (((double) info.ChildControllerRowCount) / ((double) this.rowCardCount)));
                    }
                }
            }
            return num;
        }

        private double CalcExtentUngrouped(double defineSize, int? visibleIndex)
        {
            double num5;
            int actualScrollOffset = (int) base.DataPresenter.ActualScrollOffset;
            int startScrollIndex = this.NodeContainer.StartScrollIndex;
            if (visibleIndex != null)
            {
                int? nullable = visibleIndex;
                int num4 = startScrollIndex;
                if ((nullable.GetValueOrDefault() < num4) ? (nullable != null) : false)
                {
                    return (actualScrollOffset - Math.Ceiling((double) (((double) (startScrollIndex - visibleIndex.Value)) / ((double) this.rowCardCount))));
                }
            }
            using (List<CardRowInfo>.Enumerator enumerator = this.RowsInfo.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        CardRowInfo current = enumerator.Current;
                        if (defineSize > 0.0)
                        {
                            startScrollIndex += current.Elements.Count;
                            if ((visibleIndex != null) && ((startScrollIndex - 1) >= visibleIndex.Value))
                            {
                                num5 = actualScrollOffset;
                                break;
                            }
                            actualScrollOffset++;
                            defineSize -= base.SizeHelper.GetDefineSize(current.RenderSize);
                            continue;
                        }
                    }
                    int num3 = (visibleIndex != null) ? visibleIndex.Value : base.DataPresenter.ItemCount;
                    return Math.Ceiling((double) (actualScrollOffset + (((double) (num3 - startScrollIndex)) / ((double) this.rowCardCount))));
                }
            }
            return num5;
        }

        internal int CalcGenerateItemsOffset(double scrollOffset)
        {
            if (!base.AllowPerPixelScrolling)
            {
                return (int) scrollOffset;
            }
            if (base.DataPresenter.DataControl == null)
            {
                return 0;
            }
            int num = ((int) scrollOffset) - ((int) this.oldOffset);
            return ((num != 0) ? ((this.GroupInfo.Count != 0) ? ((num <= 0) ? this.CalcGroupedOffsetBack(scrollOffset) : this.CalcGroupedOffsetForward(scrollOffset)) : this.CalcOffset(scrollOffset)) : this.actualGenerateItemsOffset);
        }

        private int CalcGroupedOffsetBack(double scrollOffset)
        {
            int num3;
            int num4;
            int startScrollIndex = this.NodeContainer.StartScrollIndex;
            int oldOffset = (int) this.oldOffset;
            this.FindGroupIndex(out num3, out num4);
            int num5 = num3;
            while (num5 >= 0)
            {
                GroupRowInfo info = this.GroupInfo[num5];
                if (oldOffset != ((int) scrollOffset))
                {
                    if (info.Expanded && (info.Level == this.LastGroupLevel))
                    {
                        int childControllerRowCount = info.ChildControllerRowCount;
                        if (num5 == num3)
                        {
                            childControllerRowCount = (num4 >= 0) ? num4 : 0;
                        }
                        int num7 = (int) Math.Ceiling((double) (((double) childControllerRowCount) / ((double) this.rowCardCount)));
                        if ((oldOffset - num7) < scrollOffset)
                        {
                            startScrollIndex -= Math.Min(childControllerRowCount, (oldOffset - ((int) scrollOffset)) * this.rowCardCount);
                            break;
                        }
                        startScrollIndex -= childControllerRowCount;
                        oldOffset -= num7;
                    }
                    if (oldOffset != ((int) scrollOffset))
                    {
                        if ((num5 != num3) || (num4 != -1))
                        {
                            startScrollIndex--;
                            oldOffset--;
                        }
                        num5--;
                        continue;
                    }
                }
                break;
            }
            return startScrollIndex;
        }

        private int CalcGroupedOffsetForward(double scrollOffset)
        {
            int num3;
            int num4;
            int num6;
            int startScrollIndex = this.NodeContainer.StartScrollIndex;
            int oldOffset = (int) this.oldOffset;
            this.FindGroupIndex(out num3, out num4);
            int num5 = 0;
            while (true)
            {
                if (num5 >= (this.RowsInfo.Count - 1))
                {
                    num6 = num3;
                    break;
                }
                CardRowInfo info = this.RowsInfo[num5];
                if (oldOffset == ((int) scrollOffset))
                {
                    return startScrollIndex;
                }
                oldOffset++;
                startScrollIndex += info.Elements.Count;
                if (info.IsItemsContainer)
                {
                    if (num5 != 0)
                    {
                        num3++;
                    }
                    num4 = 0;
                }
                else
                {
                    if (num4 == -1)
                    {
                        num4 = 0;
                    }
                    num4 += info.Elements.Count;
                }
                num5++;
            }
            while (true)
            {
                while (true)
                {
                    if (num6 < this.GroupInfo.Count)
                    {
                        GroupRowInfo info2 = this.GroupInfo[num6];
                        if (oldOffset == ((int) scrollOffset))
                        {
                            return startScrollIndex;
                        }
                        else
                        {
                            if ((num6 > num3) || (num4 == -1))
                            {
                                startScrollIndex++;
                                oldOffset++;
                            }
                            if (!info2.Expanded || (info2.Level != this.LastGroupLevel))
                            {
                                break;
                            }
                            int childControllerRowCount = info2.ChildControllerRowCount;
                            if ((num6 == num3) && (num4 > 0))
                            {
                                childControllerRowCount -= num4;
                            }
                            int num8 = (int) Math.Ceiling((double) (((double) childControllerRowCount) / ((double) this.rowCardCount)));
                            if ((oldOffset + num8) <= scrollOffset)
                            {
                                startScrollIndex += childControllerRowCount;
                                oldOffset += num8;
                                break;
                            }
                            return (startScrollIndex + ((((int) scrollOffset) - oldOffset) * this.rowCardCount));
                        }
                    }
                    else
                    {
                        return startScrollIndex;
                    }
                    break;
                }
                num6++;
            }
        }

        private int CalcOffset(double scrollOffset)
        {
            int num = ((int) scrollOffset) - ((int) this.oldOffset);
            int startScrollIndex = this.NodeContainer.StartScrollIndex;
            if ((num > 0) && (num < this.RowsInfo.Count))
            {
                for (int j = 0; j < num; j++)
                {
                    startScrollIndex += this.RowsInfo[j].Elements.Count;
                }
                return startScrollIndex;
            }
            if ((num >= 0) || (-num >= this.RowsInfo.Count))
            {
                return (startScrollIndex + (num * this.rowCardCount));
            }
            for (int i = 0; i < -num; i++)
            {
                startScrollIndex -= this.RowsInfo[i].Elements.Count;
            }
            return startScrollIndex;
        }

        protected override void CalcViewportCore(Size availableSize)
        {
            if (!this.ScrollOutOfBounds())
            {
                base.Viewport = 0.0;
                double num = 0.0;
                int num2 = 0;
                double defineSize = base.SizeHelper.GetDefineSize(availableSize);
                foreach (CardRowInfo info in this.RowsInfo)
                {
                    double num4 = base.SizeHelper.GetDefineSize(info.RenderSize);
                    if (ReferenceEquals(info, this.GetRowInfo()))
                    {
                        num4 += this.GetRowOffset();
                    }
                    double num5 = base.SizeHelper.GetDefineSize(info.Size);
                    double num6 = Math.Max(0.0, Math.Min((double) (num4 / num5), (double) (defineSize / num5)));
                    if ((num6 == 1.0) || base.AllowPerPixelScrolling)
                    {
                        this.Viewport = base.Viewport + (num6 * (base.AllowPerPixelScrolling ? ((double) 1) : ((double) info.Elements.Count)));
                    }
                    if (num6 == 1.0)
                    {
                        base.FullyVisibleItemsCount += info.Elements.Count;
                    }
                    if (!info.IsItemsContainer && (defineSize >= 0.0))
                    {
                        foreach (IItem item in info.Elements)
                        {
                            num2++;
                            num += base.SizeHelper.GetSecondarySize(item.Element.DesiredSize) + base.SizeHelper.GetSecondarySize(this.CardMarginCached);
                        }
                    }
                    defineSize -= num4;
                }
                this.rowCardCount = Math.Min(Math.Max(1, (int) Math.Floor((double) ((base.SizeHelper.GetSecondarySize(availableSize) / num) * num2))), this.MaxCardCountInRow);
                base.Viewport = Math.Round(base.Viewport, 4);
                int? visibleIndex = null;
                this.Extent = this.CalcExtent(base.SizeHelper.GetDefineSize(availableSize), visibleIndex);
            }
        }

        internal void ClearRows()
        {
            this.RowsInfo.Clear();
        }

        private bool CorrectGenerateItemsOffset(double defineSize)
        {
            if (base.DataPresenter != null)
            {
                if (!this.FillFirstRowIfNeeded())
                {
                    return false;
                }
                if (!this.CorrectGenerateItemsOffsetCore(defineSize))
                {
                    this.NodeContainer.ReGenerateItems(this.GenerateItemsOffset, this.NodeContainer.ItemCount, false);
                    return false;
                }
                this.previousGenerateOffset = null;
                this.scrollDiff = 0;
            }
            return true;
        }

        private bool CorrectGenerateItemsOffsetCore(double defineSize)
        {
            int num;
            int num2;
            int? nullable1;
            if (this.visibleIndex == null)
            {
                return true;
            }
            if (this.FindRowItem(this.visibleIndex.Value, out num, out num2))
            {
                return this.EnsureOffset(num, num2);
            }
            if (this.actualGenerateItemsOffset <= 0)
            {
                this.actualGenerateItemsOffset = 0;
                this.visibleIndex = null;
                return true;
            }
            if (this.RowsInfo.Count > 0)
            {
                nullable1 = new int?(this.GetControllerIndex(this.RowsInfo[0].Elements.Last<IItem>()));
            }
            else
            {
                nullable1 = null;
            }
            this.lastRowIndex = nullable1;
            this.visibleIndex = null;
            return false;
        }

        private bool CorrectScrollParametersIfScrollOutOfBounds()
        {
            if (base.DataPresenter == null)
            {
                return true;
            }
            if (!this.ScrollOutOfBounds())
            {
                return true;
            }
            this.oldOffset = Math.Max(0.0, Math.Min((double) (this.Extent - base.Viewport), (double) (this.CalcExtentSimple() - 1.0)));
            this.actualGenerateItemsOffset = Math.Max(0, base.DataPresenter.ItemCount - 1);
            base.DataPresenter.SetDefineScrollOffset(this.oldOffset, false);
            this.lastRowIndex = new int?(this.actualGenerateItemsOffset);
            this.NodeContainer.ReGenerateItems(this.GenerateItemsOffset, 1, false);
            return false;
        }

        private bool EnsureOffset(int rowIndex, int itemIndex)
        {
            if ((this.scrollDiff < 0) && (rowIndex > -this.scrollDiff))
            {
                for (int i = 0; i < (rowIndex + this.scrollDiff); i++)
                {
                    this.actualGenerateItemsOffset += this.RowsInfo[i].Elements.Count;
                }
                return false;
            }
            if (((itemIndex == 0) || (rowIndex == 0)) && (this.actualGenerateItemsOffset <= 0))
            {
                this.actualGenerateItemsOffset = 0;
                this.visibleIndex = null;
                return false;
            }
            if (itemIndex == 0)
            {
                this.visibleIndex = null;
                if (this.RowsInfo[0].IsItemsContainer)
                {
                    return true;
                }
                this.lastRowIndex = new int?(this.GetControllerIndex(this.RowsInfo[0].Elements.Last<IItem>()));
                return false;
            }
            if (rowIndex == 0)
            {
                this.previousGenerateOffset = new int?(this.actualGenerateItemsOffset);
                this.actualGenerateItemsOffset--;
                return false;
            }
            bool flag = false;
            int num2 = 0;
            while (true)
            {
                if (num2 < rowIndex)
                {
                    if (!this.RowsInfo[num2].IsItemsContainer)
                    {
                        num2++;
                        continue;
                    }
                    flag = true;
                }
                if (!flag || (this.previousGenerateOffset != null))
                {
                    this.actualGenerateItemsOffset++;
                }
                if (flag || ((this.previousGenerateOffset != null) && (this.previousGenerateOffset.Value == this.actualGenerateItemsOffset)))
                {
                    this.visibleIndex = null;
                }
                return false;
            }
        }

        private bool FillFirstRowIfNeeded()
        {
            if (this.lastRowIndex == null)
            {
                return true;
            }
            if (this.RowsInfo.Count != 0)
            {
                int? lastRowIndex = this.lastRowIndex;
                int controllerIndex = this.GetControllerIndex(this.RowsInfo[0].Elements.Last<IItem>());
                if (!((lastRowIndex.GetValueOrDefault() == controllerIndex) ? (lastRowIndex != null) : false))
                {
                    this.actualGenerateItemsOffset++;
                    this.NodeContainer.ReGenerateItems(this.GenerateItemsOffset, this.NodeContainer.ItemCount - 1, false);
                    this.lastRowIndex = null;
                    return false;
                }
            }
            if ((this.actualGenerateItemsOffset <= 0) || this.RowsInfo[0].IsItemsContainer)
            {
                this.lastRowIndex = null;
                return true;
            }
            this.actualGenerateItemsOffset--;
            this.NodeContainer.ReGenerateItems(this.GenerateItemsOffset, this.NodeContainer.ItemCount + 1, false);
            return false;
        }

        private void FindGroupIndex(out int groupIndex, out int rowIndex)
        {
            rowIndex = -1;
            groupIndex = 0;
            int num = 0;
            while (num < this.NodeContainer.StartScrollIndex)
            {
                num++;
                GroupRowInfo info = this.GroupInfo[groupIndex];
                if (!info.Expanded)
                {
                    for (int i = groupIndex + 1; (i < this.GroupInfo.Count) && ((this.GroupInfo[i].Level - 1) == info.Level); i++)
                    {
                        groupIndex++;
                    }
                }
                if (num == this.NodeContainer.StartScrollIndex)
                {
                    if (info.Level == this.LastGroupLevel)
                    {
                        rowIndex = 0;
                        return;
                    }
                    groupIndex++;
                    return;
                }
                if (!info.Expanded || (info.Level != this.LastGroupLevel))
                {
                    groupIndex++;
                }
                else
                {
                    if ((num + info.ChildControllerRowCount) > this.NodeContainer.StartScrollIndex)
                    {
                        rowIndex = this.NodeContainer.StartScrollIndex - num;
                        return;
                    }
                    num += info.ChildControllerRowCount;
                    if (groupIndex == (this.GroupInfo.Count - 1))
                    {
                        rowIndex = info.ChildControllerRowCount;
                        return;
                    }
                    groupIndex++;
                }
            }
        }

        private bool FindRowItem(int controllerIndex, out int rowIndex, out int itemIndex)
        {
            rowIndex = -1;
            itemIndex = -1;
            int num = 0;
            while (num < this.RowsInfo.Count)
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 >= this.RowsInfo[num].Elements.Count)
                    {
                        num++;
                        break;
                    }
                    IItem item = this.RowsInfo[num].Elements[num2];
                    if (this.GetControllerIndex(item) == controllerIndex)
                    {
                        rowIndex = num;
                        itemIndex = num2;
                        return true;
                    }
                    num2++;
                }
            }
            return false;
        }

        private bool GenerateItemsIfNeeded(double defineSize)
        {
            if ((base.DataPresenter == null) || !base.AllowPerPixelScrolling)
            {
                return true;
            }
            double num = (double) (((decimal) base.DataPresenter.ActualScrollOffset) - ((decimal) Math.Floor(base.DataPresenter.ActualScrollOffset)));
            if ((((int) base.DataPresenter.ActualScrollOffset) == 0) && (this.GenerateItemsOffset > 0))
            {
                this.oldOffset = Math.Ceiling((double) (((double) this.GenerateItemsOffset) / ((double) this.rowCardCount))) + num;
                base.DataPresenter.SetDefineScrollOffset(this.oldOffset, false);
                return false;
            }
            if ((this.GenerateItemsOffset == 0) && (base.DataPresenter.ActualScrollOffset > 1.0))
            {
                this.oldOffset = num;
                base.DataPresenter.SetDefineScrollOffset(this.oldOffset, false);
                return false;
            }
            for (int i = 0; i < this.RowsInfo.Count; i++)
            {
                if ((defineSize <= 0.0) && ((this.RowsInfo.Count - i) > 1))
                {
                    if (this.rest == null)
                    {
                        return true;
                    }
                    double num3 = base.SizeHelper.GetDefineSize(this.GetRowInfo().RenderSize);
                    this.AdjustScrollBarByDelta((num3 - this.rest.Value) / num3);
                    return false;
                }
                defineSize -= base.SizeHelper.GetDefineSize(this.RowsInfo[i].Size);
            }
            if (!this.NodeContainer.IsFinished)
            {
                this.NodeContainer.GenerateItems(1);
                return false;
            }
            defineSize = Math.Round(defineSize, 4);
            if ((base.DataPresenter.ActualScrollOffset == 0.0) || (defineSize <= 0.0))
            {
                if ((this.rest != null) && (defineSize < 0.0))
                {
                    this.AdjustScrollBarByDelta(-defineSize / base.SizeHelper.GetDefineSize(this.GetRowInfo().RenderSize));
                    return false;
                }
                this.rest = null;
                return true;
            }
            if (-this.GetRowOffset() == defineSize)
            {
                this.AdjustScrollBar((double) base.DataPresenter.ScrollOffset);
                return false;
            }
            if (-this.GetRowOffset() > defineSize)
            {
                this.AdjustScrollBarByDelta(-defineSize / base.SizeHelper.GetDefineSize(this.GetRowInfo().RenderSize));
                return false;
            }
            this.rest = new double?(defineSize);
            base.DataPresenter.SetDefineScrollOffset((num > 0.0) ? Math.Floor(base.DataPresenter.ActualScrollOffset) : (base.DataPresenter.ActualScrollOffset - 1.0), false);
            this.NodeContainer.ReGenerateItems(this.GenerateItemsOffset, this.NodeContainer.ItemCount + 1, false);
            return false;
        }

        private double GetArrangeSecondaryOffset(CardRowInfo rowInfo, Size availableSize)
        {
            double secondarySize = base.SizeHelper.GetSecondarySize(availableSize);
            double num2 = 0.0;
            if (base.Orientation == Orientation.Vertical)
            {
                num2 += this.ContainerIndent * rowInfo.Level;
            }
            else
            {
                secondarySize -= this.ContainerIndent * rowInfo.Level;
            }
            if (!rowInfo.IsItemsContainer)
            {
                double num3 = (secondarySize - base.SizeHelper.GetSecondarySize(rowInfo.Size)) - num2;
                if (this.CardAlignment == Alignment.Center)
                {
                    num2 += Math.Floor((double) (num3 / 2.0));
                }
                else if (this.CardAlignment == Alignment.Far)
                {
                    num2 += num3;
                }
            }
            return num2;
        }

        private Size GetContainerSize(Size availableSize, double defineSize) => 
            base.SizeHelper.CreateSize(Math.Max((double) 0.0, (double) (base.SizeHelper.GetDefineSize(availableSize) - defineSize)), Math.Max((double) 0.0, (double) (base.SizeHelper.GetSecondarySize(availableSize) - this.ContainerIndent)));

        private int GetControllerIndex(IItem item) => 
            ((RowData) item).ControllerVisibleIndex;

        private double GetDefineSize(IItem item) => 
            (item.IsItemsContainer || double.IsNaN(this.FixedSize)) ? base.SizeHelper.GetDefineSize(item.Element.DesiredSize) : this.FixedSize;

        private Size GetMeasureSize(IItem item) => 
            base.SizeHelper.CreateSize((item.IsItemsContainer || double.IsNaN(this.FixedSize)) ? double.PositiveInfinity : this.FixedSize, double.PositiveInfinity);

        private CardRowInfo GetOrCreateCurrentRowInfo(bool newRow, int level, bool isItemsContainer, List<CardRowInfo> rows)
        {
            if (!newRow && (rows.Count != 0))
            {
                return rows.Last<CardRowInfo>();
            }
            if (rows.Count != 0)
            {
                CardRowInfo rowInfo = rows.Last<CardRowInfo>();
                if ((rowInfo.Level == level) && !rowInfo.IsItemsContainer)
                {
                    rowInfo.HasSeparator = true;
                    this.UpdateRowInfo(rowInfo, base.SizeHelper.GetDefineSize(rowInfo.Size) + this.SeparatorThickness, base.SizeHelper.GetSecondarySize(rowInfo.Size));
                }
            }
            CardRowInfo info1 = new CardRowInfo();
            info1.Elements = new List<IItem>();
            info1.Level = level;
            info1.IsItemsContainer = isItemsContainer;
            info1.HasSeparator = false;
            CardRowInfo item = info1;
            rows.Add(item);
            return item;
        }

        private CardRowInfo GetRowInfo() => 
            this.RowsInfo.First<CardRowInfo>();

        internal double GetRowOffset() => 
            ((this.RowsInfo == null) || ((this.RowsInfo.Count == 0) || (base.DataPresenter == null))) ? 0.0 : Math.Round((double) ((Math.Floor(base.DataPresenter.ActualScrollOffset) - base.DataPresenter.ActualScrollOffset) * base.SizeHelper.GetDefineSize(this.GetRowInfo().Size)));

        private double GetSecondarySize(IItem item, Size availableSize, CardRowInfo rowInfo)
        {
            double num = base.SizeHelper.GetSecondarySize(availableSize) - (rowInfo.Level * this.ContainerIndent);
            if (item.IsItemsContainer)
            {
                return num;
            }
            double secondarySize = base.SizeHelper.GetSecondarySize(item.Element.DesiredSize);
            if (this.CardAlignment == Alignment.Stretch)
            {
                secondarySize += (num - base.SizeHelper.GetSecondarySize(rowInfo.Size)) / ((double) rowInfo.Elements.Count);
            }
            return secondarySize;
        }

        private bool MeasureItem(IItem item, Size availableSize, int currentRowCount, ref double defineSize, ref double secondarySize, ref double maxDefineSize, ref double maxSecondarySize)
        {
            item.Element.Measure(this.GetMeasureSize(item));
            if (!item.IsRowVisible)
            {
                return false;
            }
            double num = this.GetDefineSize(item);
            double num2 = base.SizeHelper.GetSecondarySize(item.Element.DesiredSize);
            if (!item.IsItemsContainer)
            {
                num += base.SizeHelper.GetDefineSize(this.CardMarginCached);
                num2 += base.SizeHelper.GetSecondarySize(this.CardMarginCached);
            }
            bool flag = (item.IsItemsContainer || ((num2 + secondarySize) > base.SizeHelper.GetSecondarySize(availableSize))) || (currentRowCount == this.MaxCardCountInRow);
            if (!flag)
            {
                secondarySize += num2;
                maxDefineSize = Math.Max(maxDefineSize, num);
            }
            else
            {
                secondarySize = num2;
                defineSize += maxDefineSize;
                maxDefineSize = num;
            }
            maxSecondarySize = Math.Max(maxSecondarySize, secondarySize);
            return flag;
        }

        protected override Size MeasureItemsContainer(Size availableSize, IItemsContainer itemsContainer)
        {
            if (itemsContainer == null)
            {
                return new Size();
            }
            this.oldOffset = (base.DataPresenter != null) ? base.DataPresenter.ActualScrollOffset : 0.0;
            if (!this.CorrectScrollParametersIfScrollOutOfBounds())
            {
                base.ValidateTree(base.ItemsContainer);
            }
            this.hiddenItems = new List<FrameworkElement>();
            this.RowsInfo = this.MeasureItemsContainer(0.0, availableSize, itemsContainer, 0);
            this.MaxSecondarySize = base.SizeHelper.GetSecondarySize(itemsContainer.DesiredSize);
            return itemsContainer.DesiredSize;
        }

        private List<CardRowInfo> MeasureItemsContainer(double offset, Size availableSize, IItemsContainer itemsContainer, int level)
        {
            List<CardRowInfo> source = new List<CardRowInfo>();
            double maxSecondarySize = 0.0;
            double defineSize = 0.0;
            double maxDefineSize = 0.0;
            double secondarySize = 0.0;
            foreach (IItem item in base.GetSortedChildrenElements(itemsContainer))
            {
                bool newRow = this.MeasureItem(item, availableSize, (source.Count > 0) ? source.Last<CardRowInfo>().Elements.Count : 0, ref defineSize, ref secondarySize, ref maxDefineSize, ref maxSecondarySize);
                if (newRow && (!item.IsItemsContainer && (source.Count != 0)))
                {
                    defineSize += this.SeparatorThickness;
                }
                if (!item.IsRowVisible)
                {
                    this.hiddenItems.Add(item.Element);
                }
                else
                {
                    CardRowInfo rowInfo = this.GetOrCreateCurrentRowInfo(newRow, level, item.IsItemsContainer, source);
                    rowInfo.Elements.Add(item);
                    this.UpdateRowInfo(rowInfo, maxDefineSize, secondarySize);
                }
                source.AddRange(this.MeasureItemsContainer(defineSize + offset, this.GetContainerSize(availableSize, defineSize), item.ItemsContainer, level + 1));
                defineSize += base.SizeHelper.GetDefineSize(item.ItemsContainer.DesiredSize);
                double num6 = base.SizeHelper.GetSecondarySize(item.ItemsContainer.DesiredSize);
                if (num6 != 0.0)
                {
                    maxSecondarySize = Math.Max(maxSecondarySize, num6 + this.ContainerIndent);
                }
            }
            defineSize += maxDefineSize;
            itemsContainer.RenderSize = base.SizeHelper.CreateSize(defineSize, maxSecondarySize);
            double num5 = Math.Ceiling((double) (defineSize * itemsContainer.AnimationProgress));
            itemsContainer.DesiredSize = base.SizeHelper.CreateSize(num5, maxSecondarySize);
            this.UpdateRowInfoSize(source, num5);
            return source;
        }

        private void OnCardMarginChanged()
        {
            this.CardMarginCached = new Size(this.CardMargin.Left + this.CardMargin.Right, this.CardMargin.Top + this.CardMargin.Bottom);
            base.InvalidateMeasure();
        }

        internal void OnDefineScrollInfoChanged()
        {
            if ((base.DataPresenter != null) && (this.oldOffset != base.DataPresenter.ActualScrollOffset))
            {
                this.actualGenerateItemsOffset = this.CalcGenerateItemsOffset(base.DataPresenter.ActualScrollOffset);
                if (((((int) base.DataPresenter.ActualScrollOffset) - ((int) this.oldOffset)) < 0) && ((this.RowsInfo.Count > 0) && base.AllowPerPixelScrolling))
                {
                    this.visibleIndex = new int?(this.GetControllerIndex(this.RowsInfo[0].Elements[0]));
                }
                this.scrollDiff = ((int) base.DataPresenter.ActualScrollOffset) - ((int) this.oldOffset);
                base.InvalidateMeasure();
            }
        }

        internal bool OnLayoutUpdated()
        {
            Size lastConstraint = base.DataPresenter.LastConstraint;
            if (this.CorrectGenerateItemsOffset(base.SizeHelper.GetSecondarySize(lastConstraint)) && this.GenerateItemsIfNeeded(base.SizeHelper.GetDefineSize(lastConstraint) - this.GetRowOffset()))
            {
                this.tryCount = 0;
                return true;
            }
            this.tryCount++;
            if (this.tryCount > 100)
            {
                this.tryCount = 0;
                return false;
            }
            base.InvalidateMeasure();
            return false;
        }

        private bool ScrollOutOfBounds() => 
            (base.DataPresenter != null) && ((this.actualGenerateItemsOffset >= base.DataPresenter.ItemCount) && (this.actualGenerateItemsOffset != 0));

        internal bool TryFindRowElement(int index, out int rowIndex, out int elementIndex)
        {
            rowIndex = 0;
            elementIndex = 0;
            int num = 0;
            while (num < this.RowsInfo.Count)
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 >= this.RowsInfo[num].Elements.Count)
                    {
                        num++;
                        break;
                    }
                    if (index == this.GetControllerIndex(this.RowsInfo[num].Elements[num2]))
                    {
                        rowIndex = num;
                        elementIndex = num2;
                        return true;
                    }
                    num2++;
                }
            }
            return false;
        }

        private void UpdateRowInfo(CardRowInfo rowInfo, double maxDefineSize, double secondarySize)
        {
            rowInfo.Size = base.SizeHelper.CreateSize(maxDefineSize, secondarySize);
            rowInfo.RenderSize = rowInfo.Size;
        }

        private void UpdateRowInfoSize(List<CardRowInfo> info, double availableSize)
        {
            foreach (CardRowInfo info2 in info)
            {
                info2.RenderSize = base.SizeHelper.CreateSize(Math.Min(base.SizeHelper.GetDefineSize(info2.RenderSize), availableSize), base.SizeHelper.GetSecondarySize(info2.RenderSize));
                availableSize -= base.SizeHelper.GetDefineSize(info2.RenderSize);
            }
        }

        private void UpdateSeparatorInfo(Size availableSize)
        {
            this.SeparatorInfo ??= new ObservableCollection<DevExpress.Xpf.Grid.Hierarchy.SeparatorInfo>();
            int num = 0;
            double num2 = 0.0;
            foreach (CardRowInfo info in this.RowsInfo)
            {
                if (info.IsItemsContainer || !info.HasSeparator)
                {
                    num2 += base.SizeHelper.GetDefineSize(info.Size);
                    continue;
                }
                if (this.SeparatorInfo.Count == num)
                {
                    DevExpress.Xpf.Grid.Hierarchy.SeparatorInfo item = new DevExpress.Xpf.Grid.Hierarchy.SeparatorInfo();
                    item.RowIndex = num + 1;
                    this.SeparatorInfo.Add(item);
                }
                DevExpress.Xpf.Grid.Hierarchy.SeparatorInfo info2 = this.SeparatorInfo[num];
                info2.Orientation = base.Orientation;
                info2.Length = base.SizeHelper.GetSecondarySize(availableSize) - (info.Level * this.ContainerIndent);
                num2 += base.SizeHelper.GetDefineSize(info.Size) - this.SeparatorThickness;
                Point point = base.SizeHelper.CreatePoint(num2 + this.GetRowOffset(), 0.0);
                info2.Margin = new Thickness(point.X, point.Y, 0.0, 0.0);
                info2.IsVisible = ((this.SeparatorThickness - base.SizeHelper.GetDefineSize(info.Size)) + base.SizeHelper.GetDefineSize(info.RenderSize)) >= 0.0;
                num2 += this.SeparatorThickness;
                num++;
            }
            for (int i = num; i < this.SeparatorInfo.Count; i++)
            {
                this.SeparatorInfo[i].IsVisible = false;
            }
        }

        public Thickness CardMargin
        {
            get => 
                (Thickness) base.GetValue(CardMarginProperty);
            set => 
                base.SetValue(CardMarginProperty, value);
        }

        public double ContainerIndent
        {
            get => 
                (double) base.GetValue(ContainerIndentProperty);
            set => 
                base.SetValue(ContainerIndentProperty, value);
        }

        public double FixedSize
        {
            get => 
                (double) base.GetValue(FixedSizeProperty);
            set => 
                base.SetValue(FixedSizeProperty, value);
        }

        public double SeparatorThickness
        {
            get => 
                (double) base.GetValue(SeparatorThicknessProperty);
            set => 
                base.SetValue(SeparatorThicknessProperty, value);
        }

        public int MaxCardCountInRow
        {
            get => 
                (int) base.GetValue(MaxCardCountInRowProperty);
            set => 
                base.SetValue(MaxCardCountInRowProperty, value);
        }

        public Alignment CardAlignment
        {
            get => 
                (Alignment) base.GetValue(CardAlignmentProperty);
            set => 
                base.SetValue(CardAlignmentProperty, value);
        }

        public IList<DevExpress.Xpf.Grid.Hierarchy.SeparatorInfo> SeparatorInfo
        {
            get => 
                (IList<DevExpress.Xpf.Grid.Hierarchy.SeparatorInfo>) base.GetValue(SeparatorInfoProperty);
            private set => 
                base.SetValue(SeparatorInfoPropertyKey, value);
        }

        internal double MaxSecondarySize { get; private set; }

        internal List<CardRowInfo> RowsInfo { get; private set; }

        private DetailNodeContainer NodeContainer =>
            base.DataPresenter.View.RootNodeContainer;

        internal int GenerateItemsOffset =>
            Math.Max(0, Math.Min(this.actualGenerateItemsOffset, base.DataPresenter.ItemCount - 1));

        internal double Extent { get; private set; }

        private GroupRowInfoCollection GroupInfo =>
            base.DataPresenter.DataControl.DataProviderBase.DataController.GroupInfo;

        private int LastGroupLevel =>
            base.DataPresenter.DataControl.DataProviderBase.DataController.GroupedColumnCount - 1;

        internal int RowItemCount =>
            ((this.RowsInfo == null) || (this.RowsInfo.Count == 0)) ? 0 : this.GetRowInfo().Elements.Count;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CardsHierarchyPanel.<>c <>9 = new CardsHierarchyPanel.<>c();

            internal void <.cctor>b__104_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CardsHierarchyPanel) d).OnCardMarginChanged();
            }
        }
    }
}

