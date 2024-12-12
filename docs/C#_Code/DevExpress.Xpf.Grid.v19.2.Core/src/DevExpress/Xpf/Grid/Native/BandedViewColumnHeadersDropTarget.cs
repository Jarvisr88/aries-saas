namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class BandedViewColumnHeadersDropTarget : DropTargetBase
    {
        private FixedStyle fixedStyle;

        public BandedViewColumnHeadersDropTarget(Panel panel, FixedStyle fixedStyle) : base(panel)
        {
            this.fixedStyle = fixedStyle;
        }

        protected bool AreSameColumns(BaseColumn source, BaseColumn target)
        {
            BaseColumn objA = source;
            if (source.View.DataControl.IsOriginationDataControlCore())
            {
                objA = source.CreateCloneAccessor()(base.Grid);
            }
            return ReferenceEquals(objA, target);
        }

        private double CalcVerticalDragIndicatorSize(Point point, double width) => 
            this.GridView.CalcVerticalDragIndicatorSize(base.Panel, point, width);

        private bool CanDrop(BaseColumn sourceColumn, BaseGridHeader target, HeaderPresenterType moveFrom) => 
            sourceColumn.CanDropTo(target.BaseColumn) && (!this.DenyDropToAnotherParent(sourceColumn, target.BaseColumn, moveFrom) && !this.IsCheckBoxSelectorColumn(target.BaseColumn));

        protected override bool CanDropCore(UIElement source, Point pt, out object dragAnchor, bool isDrag)
        {
            dragAnchor = pt;
            BaseColumn columnFromDragSource = this.GetColumnFromDragSource(source);
            if ((columnFromDragSource == null) || !this.IsCompatibleSource(columnFromDragSource))
            {
                return false;
            }
            BaseGridHeader headerElement = base.GetColumnHeaderHitTestResult(pt).HeaderElement as BaseGridHeader;
            HeaderPresenterType headerPresenterType = ColumnBase.GetHeaderPresenterType(source);
            DataControlBase grid = base.Grid;
            if ((headerPresenterType == HeaderPresenterType.GroupPanel) && !grid.GetOriginationDataControl().DataControlOwner.CanGroupColumn((ColumnBase) columnFromDragSource))
            {
                return false;
            }
            if (headerElement != null)
            {
                return (this.GetDropPlace(columnFromDragSource, headerElement, this.GetLocation(headerElement, pt), headerPresenterType) != BandedViewDropPlace.None);
            }
            BandBase bandByOffset = this.GetBandByOffset(pt.X);
            return ((bandByOffset != null) && (columnFromDragSource.CanDropTo(bandByOffset) && !this.DenyDropToAnotherParent(columnFromDragSource, bandByOffset, headerPresenterType)));
        }

        protected virtual bool DenyDropToAnotherParent(BaseColumn column, BaseColumn target, HeaderPresenterType moveFrom) => 
            (!column.IsBand || !ReferenceEquals(((BandBase) column).Owner, target)) ? (!this.AreSameColumns(column.ParentBandInternal, target.ParentBandInternal) && !DesignerHelper.GetValue(column, column.AllowChangeParent, true)) : false;

        protected virtual bool DropToTopBottomEdges(BaseColumn sourceColumn, BaseGridHeader target, Point point, HeaderPresenterType moveFrom, out BandedViewDropPlace dropPlace)
        {
            dropPlace = BandedViewDropPlace.None;
            if (sourceColumn.IsBand)
            {
                dropPlace = BandedViewDropPlace.Top;
                return true;
            }
            if (!base.Grid.BandsLayoutCore.AllowBandMultiRow)
            {
                return false;
            }
            if (ReferenceEquals(sourceColumn, target.BaseColumn) && (sourceColumn.BandRow.Columns.Count == 1))
            {
                return (moveFrom != HeaderPresenterType.GroupPanel);
            }
            int index = target.BaseColumn.ParentBandInternal.ActualRows.IndexOf(target.BaseColumn.BandRow);
            int num2 = sourceColumn.ParentBandInternal.ActualRows.IndexOf(sourceColumn.BandRow);
            bool flag = ReferenceEquals(target.BaseColumn.ParentBandInternal, sourceColumn.ParentBandInternal);
            if ((point.Y < 4.0) && (!((num2 == (index - 1)) & flag) || ((sourceColumn.BandRow == null) || (sourceColumn.BandRow.Columns.Count != 1))))
            {
                dropPlace = BandedViewDropPlace.Top;
                return true;
            }
            if ((point.Y <= (target.ActualHeight - 4.0)) || (((num2 == (index + 1)) & flag) && ((sourceColumn.BandRow != null) && (sourceColumn.BandRow.Columns.Count == 1))))
            {
                return false;
            }
            dropPlace = BandedViewDropPlace.Bottom;
            return true;
        }

        protected virtual Point GetAdornerLocationOffset(BaseColumn sourceColumn, BaseGridHeader header, BandedViewDropPlace dropPlace)
        {
            double x = 0.0;
            double y = 0.0;
            if ((((dropPlace == BandedViewDropPlace.Top) || (dropPlace == BandedViewDropPlace.Bottom)) && this.ShouldCalcOffsetForOtherColumns) && (header != null))
            {
                for (int i = header.BaseColumn.BandRow.Columns.IndexOf((ColumnBase) header.BaseColumn) - 1; i >= 0; i--)
                {
                    x -= header.BaseColumn.BandRow.Columns[i].ActualHeaderWidth;
                }
            }
            if (dropPlace == BandedViewDropPlace.Bottom)
            {
                y += header.ActualHeight;
            }
            if (dropPlace == BandedViewDropPlace.Right)
            {
                x += header.ActualWidth;
            }
            return new Point(x, y);
        }

        private BandBase GetBandByOffset(double offset)
        {
            double actualOffset = 0.0;
            return this.GetBandByOffset(this.GetBandsCollection(this.fixedStyle), offset, ref actualOffset);
        }

        private BandBase GetBandByOffset(IEnumerable bands, double offset, ref double actualOffset)
        {
            BandBase base2 = null;
            BandBase base4;
            using (IEnumerator enumerator = bands.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BandBase current = (BandBase) enumerator.Current;
                        if ((actualOffset + current.ActualHeaderWidth) < offset)
                        {
                            actualOffset += current.ActualHeaderWidth;
                            continue;
                        }
                        if (current.VisibleBands.Count == 0)
                        {
                            base4 = current;
                        }
                        else
                        {
                            base2 = this.GetBandByOffset(current.VisibleBands, offset, ref actualOffset);
                            if (base2 == null)
                            {
                                continue;
                            }
                            base4 = base2;
                        }
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return base4;
        }

        private IEnumerable GetBandsCollection(FixedStyle fixedStyle) => 
            (fixedStyle != FixedStyle.Left) ? ((fixedStyle != FixedStyle.Right) ? this.GridView.DataControl.BandsLayoutCore.FixedNoneVisibleBands : this.GridView.DataControl.BandsLayoutCore.FixedRightVisibleBands) : this.GridView.DataControl.BandsLayoutCore.FixedLeftVisibleBands;

        private BandedViewDropPlace GetCheckBoxSelectorBandDropPlace(BaseGridHeader target, Point point) => 
            this.IsFarSide(target, point) ? BandedViewDropPlace.Right : BandedViewDropPlace.None;

        private BaseColumn GetColumnFromDragSource(UIElement source) => 
            DropTargetHelper.GetColumnFromDragSource(source);

        protected virtual BandedViewDropPlace GetDropPlace(BaseColumn sourceColumn, BaseGridHeader target, Point point, HeaderPresenterType moveFrom)
        {
            if (!this.CanDrop(sourceColumn, target, moveFrom))
            {
                return BandedViewDropPlace.None;
            }
            if (this.IsCheckBoxSelectorBand(target.BaseColumn))
            {
                return this.GetCheckBoxSelectorBandDropPlace(target, point);
            }
            BandedViewDropPlace none = BandedViewDropPlace.None;
            if (this.DropToTopBottomEdges(sourceColumn, target, point, moveFrom, out none))
            {
                return none;
            }
            bool flag = this.IsFarSide(target, point);
            if ((sourceColumn != null) && (this.IsSameRow(sourceColumn, target.BaseColumn, moveFrom) && sourceColumn.Visible))
            {
                if ((sourceColumn.ActualVisibleIndex == (target.BaseColumn.ActualVisibleIndex - 1)) && !flag)
                {
                    return BandedViewDropPlace.None;
                }
                if (sourceColumn.ActualVisibleIndex == target.BaseColumn.ActualVisibleIndex)
                {
                    return ((moveFrom == HeaderPresenterType.GroupPanel) ? BandedViewDropPlace.Left : BandedViewDropPlace.None);
                }
                if ((sourceColumn.ActualVisibleIndex == (target.BaseColumn.ActualVisibleIndex + 1)) & flag)
                {
                    return BandedViewDropPlace.None;
                }
            }
            return (flag ? BandedViewDropPlace.Right : BandedViewDropPlace.Left);
        }

        private FixedStyle GetFixedDropPlace(BaseColumn column) => 
            (this.fixedStyle == FixedStyle.None) ? (((column.Fixed != FixedStyle.Left) || (this.GridView.DataControl.BandsLayoutCore.FixedLeftVisibleBands.Count != 0)) ? (((column.Fixed != FixedStyle.Right) || (this.GridView.DataControl.BandsLayoutCore.FixedRightVisibleBands.Count != 0)) ? FixedStyle.None : FixedStyle.Right) : FixedStyle.Left) : FixedStyle.None;

        private Point GetLocation(Rect rect, Point point)
        {
            PointHelper.Offset(ref point, -rect.Left, -rect.Top);
            return point;
        }

        protected Point GetLocation(UIElement relativeTo, Point point) => 
            this.GetLocation(LayoutHelper.GetRelativeElementRect(relativeTo, this.AdornableElement), point);

        private Rect GetTargetHeaderRelativeRect(BaseGridHeader header, Point point, BaseColumn sourceColumn)
        {
            if (header == null)
            {
                double x = 0.0;
                return new Rect(x, 0.0, this.GetBandByOffset(this.GetBandsCollection(this.fixedStyle), point.X, ref x).ActualHeaderWidth, base.Panel.ActualHeight);
            }
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(header, this.AdornableElement);
            if (header.BaseColumn.IsBand && !sourceColumn.IsBand)
            {
                relativeElementRect.Height = this.GridView.HeadersPanel.ActualHeight;
            }
            return relativeElementRect;
        }

        protected bool IsCheckBoxSelectorBand(BaseColumn column) => 
            column.IsServiceColumn() && column.IsBand;

        protected bool IsCheckBoxSelectorColumn(BaseColumn column) => 
            column.IsServiceColumn() && !column.IsBand;

        protected virtual bool IsCompatibleSource(BaseColumn column) => 
            !column.IsBand || ((this.GridView.DataControl.BandsLayoutCore.GetRootBand(column.ParentBandInternal).Fixed == this.fixedStyle) || (this.GetFixedDropPlace(column) != FixedStyle.None));

        private bool IsFarSide(FrameworkElement element, Point point) => 
            point.X > (element.ActualWidth / 2.0);

        protected virtual bool IsSameRow(BaseColumn column, BaseColumn target, HeaderPresenterType moveFrom) => 
            ReferenceEquals(column.BandRow, target.BandRow);

        protected virtual void MoveBandColumn(BaseColumn sourceColumn, BaseGridHeader targetHeader, Point point, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            BandedViewDropPlace left = BandedViewDropPlace.Left;
            if (targetHeader != null)
            {
                left = this.GetDropPlace(sourceColumn, targetHeader, this.GetLocation(targetHeader, point), moveFrom);
            }
            if (sourceColumn.IsBand)
            {
                left = BandedViewDropPlace.Bottom;
            }
            BaseColumn target = (targetHeader != null) ? targetHeader.BaseColumn : this.GetBandByOffset(point.X);
            BandBase source = sourceColumn as BandBase;
            if (source != null)
            {
                this.GridView.DataControl.BandsLayoutCore.MoveBandTo(source, target.ParentBandInternal, left, moveFrom, useLegacyColumnVisibleIndexes);
            }
            else
            {
                this.GridView.DataControl.BandsLayoutCore.MoveColumnTo(sourceColumn, target, left, moveFrom, useLegacyColumnVisibleIndexes);
            }
        }

        protected override void MoveColumnToCore(UIElement source, object dropAnchor)
        {
            Point point = (Point) dropAnchor;
            BaseColumn columnFromDragSource = this.GetColumnFromDragSource(source);
            if (this.GetFixedDropPlace(columnFromDragSource) != FixedStyle.None)
            {
                columnFromDragSource.Visible = true;
            }
            else
            {
                this.MoveBandColumn(columnFromDragSource, base.GetColumnHeaderHitTestResult(point).HeaderElement as BaseGridHeader, point, ColumnBase.GetHeaderPresenterType(source), this.GridView.UseLegacyColumnVisibleIndexes);
            }
        }

        private void UpdateDragAdornerLocation(Point location)
        {
            base.UpdatePopupLocation(this.CorrectDragIndicatorLocation(location));
        }

        protected override unsafe void UpdateDragAdornerLocationCore(UIElement sourceElement, object headerAnchor)
        {
            if (base.DragIndicator != null)
            {
                BaseColumn columnFromDragSource = this.GetColumnFromDragSource(sourceElement);
                FixedStyle fixedDropPlace = this.GetFixedDropPlace(columnFromDragSource);
                if (fixedDropPlace != FixedStyle.None)
                {
                    this.UpdateDragAdornerLocation(new Point((fixedDropPlace == FixedStyle.Left) ? 0.0 : base.Panel.ActualWidth, 0.0));
                    this.SetDropPlaceOrientation(base.DragIndicator, Orientation.Horizontal);
                    this.SetColumnHeaderDragIndicatorSize(base.DragIndicator, base.Panel.ActualHeight);
                }
                else
                {
                    Point pt = (Point) headerAnchor;
                    BaseGridHeader headerElement = base.GetColumnHeaderHitTestResult(pt).HeaderElement as BaseGridHeader;
                    Rect rect = this.GetTargetHeaderRelativeRect(headerElement, pt, columnFromDragSource);
                    BandedViewDropPlace left = BandedViewDropPlace.Left;
                    if (headerElement != null)
                    {
                        left = this.GetDropPlace(columnFromDragSource, headerElement, this.GetLocation(rect, pt), ColumnBase.GetHeaderPresenterType(sourceElement));
                    }
                    else if (columnFromDragSource.IsBand)
                    {
                        left = BandedViewDropPlace.Top;
                    }
                    Point p = rect.Location();
                    Point point3 = this.GetAdornerLocationOffset(columnFromDragSource, headerElement, left);
                    PointHelper.Offset(ref p, point3.X, point3.Y);
                    if (columnFromDragSource.IsBand && ((headerElement == null) || !headerElement.BaseColumn.IsBand))
                    {
                        p.Y = 0.0;
                    }
                    if ((left == BandedViewDropPlace.Top) || (left == BandedViewDropPlace.Bottom))
                    {
                        this.SetDropPlaceOrientation(base.DragIndicator, Orientation.Vertical);
                        if ((headerElement != null) && ((headerElement.BaseColumn != null) && (headerElement.BaseColumn.ActualBandLeftSeparatorWidth > 0.0)))
                        {
                            Point* pointPtr1 = &p;
                            pointPtr1.X += headerElement.BaseColumn.ActualBandLeftSeparatorWidth;
                        }
                        this.SetColumnHeaderDragIndicatorSize(base.DragIndicator, this.CalcVerticalDragIndicatorSize(p, (headerElement != null) ? (headerElement.BaseColumn.ParentBandInternal.ActualHeaderWidth - headerElement.BaseColumn.ActualBandRightSeparatorWidthCore) : rect.Width));
                    }
                    else
                    {
                        this.SetDropPlaceOrientation(base.DragIndicator, Orientation.Horizontal);
                        this.SetColumnHeaderDragIndicatorSize(base.DragIndicator, rect.Height);
                        if (left != BandedViewDropPlace.Left)
                        {
                            if ((left == BandedViewDropPlace.Right) && ((headerElement != null) && ((headerElement.BaseColumn != null) && (headerElement.BaseColumn.ActualBandRightSeparatorWidthCore > 0.0))))
                            {
                                Point* pointPtr3 = &p;
                                pointPtr3.X -= headerElement.BaseColumn.ActualBandRightSeparatorWidthCore;
                            }
                        }
                        else if ((headerElement != null) && ((headerElement.BaseColumn != null) && (headerElement.BaseColumn.ActualBandLeftSeparatorWidthCore > 0.0)))
                        {
                            Point* pointPtr2 = &p;
                            pointPtr2.X += headerElement.BaseColumn.ActualBandLeftSeparatorWidthCore;
                        }
                    }
                    this.UpdateDragAdornerLocation(p);
                }
            }
        }

        protected virtual bool ShouldCalcOffsetForOtherColumns =>
            true;
    }
}

