namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class ColumnHeaderDropTargetBase : HeaderDropTargetBase
    {
        public ColumnHeaderDropTargetBase(Panel panel) : base(panel)
        {
        }

        protected sealed override bool CanDrop(UIElement source, int dropIndex)
        {
            if ((dropIndex < 0) || (this.Grid.ColumnsCore.Count == 0))
            {
                return false;
            }
            ColumnBase columnFromDragSource = DropTargetHelper.GetColumnFromDragSource(source) as ColumnBase;
            if (!columnFromDragSource.CanBeGroupedByDataControlOwner() && this.DenyDropIfGroupingIsNotAllowed(BaseGridColumnHeader.GetHeaderPresenterTypeFromGridColumnHeader(source)))
            {
                return false;
            }
            DevExpress.Xpf.Grid.HeaderPresenterType headerPresenterTypeFromGridColumnHeader = BaseGridColumnHeader.GetHeaderPresenterTypeFromGridColumnHeader(source);
            return ((!this.Grid.DataView.AllowPartialGroupingCore || ((columnFromDragSource.GroupIndexCore == -1) || (headerPresenterTypeFromGridColumnHeader != DevExpress.Xpf.Grid.HeaderPresenterType.Headers))) ? this.CanDropCore(dropIndex, columnFromDragSource, headerPresenterTypeFromGridColumnHeader) : false);
        }

        protected abstract bool CanDropCore(int dropIndex, ColumnBase sourceColumn, DevExpress.Xpf.Grid.HeaderPresenterType headerPresenterType);
        protected virtual bool ContainsColumn(FrameworkElement element, ColumnBase column) => 
            element.DataContext == column;

        protected override Point CorrectDragIndicatorLocation(Point point) => 
            BaseGridColumnHeader.GetCorrectDragIndicatorLocation(base.Panel) ? this.GridView.CorrectDragIndicatorLocation(base.Panel, point) : base.CorrectDragIndicatorLocation(point);

        protected override HeaderDropTargetBase.HeaderHitTestResult CreateHitTestResult() => 
            new ColumnHeaderHitTestResult();

        protected abstract bool DenyDropIfGroupingIsNotAllowed(DevExpress.Xpf.Grid.HeaderPresenterType sourceType);
        private ColumnBase GetColumnByDragElement(UIElement element) => 
            BaseGridColumnHeader.GetColumnByDragElement(element);

        private DevExpress.Xpf.Grid.HeaderPresenterType GetColumnPresenterTypeByDragElement(UIElement element) => 
            BaseGridColumnHeader.GetHeaderPresenterTypeFromGridColumnHeader(element);

        protected virtual IList GetColumnsCollection(ColumnBase sourceColumn) => 
            this.Grid.ColumnsCore;

        protected ColumnBase GetDependentColumnFromDragSource(UIElement source)
        {
            ColumnBase columnFromDragSource = DropTargetHelper.GetColumnFromDragSource(source) as ColumnBase;
            return CloneDetailHelper.SafeGetDependentCollectionItem<ColumnBase>(columnFromDragSource, columnFromDragSource.View.ColumnsCore, this.GetColumnsCollection(columnFromDragSource));
        }

        protected override Orientation GetDropPlaceOrientation(DependencyObject element) => 
            BaseGridHeader.GetDropPlaceOrientation(element);

        protected override bool GetIsFarCorner(HeaderDropTargetBase.HeaderHitTestResult result, Point point)
        {
            FrameworkElement headerElement = result.HeaderElement as FrameworkElement;
            if (headerElement == null)
            {
                return false;
            }
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(headerElement, base.Panel);
            return ((this.GetDropPlaceOrientation(headerElement) != Orientation.Horizontal) ? ((((FrameworkElement) result.HeaderElement).ActualHeight / 2.0) < (point.Y - relativeElementRect.Y)) : ((((FrameworkElement) result.HeaderElement).ActualWidth / 2.0) < (point.X - relativeElementRect.X)));
        }

        protected override int GetRelativeVisibleIndex(UIElement element)
        {
            if (this.Grid.DataView.ColumnsCore.Count == 0)
            {
                return -1;
            }
            ColumnBase dependentColumnFromDragSource = this.GetDependentColumnFromDragSource(element);
            return this.GetVisibleIndex(dependentColumnFromDragSource);
        }

        protected virtual DataViewBase GetViewForDrop(UIElement source) => 
            this.GridView;

        protected IList<ColumnBase> GetVisibleColumnsCollectionFromDragElement(TableViewBehavior viewBehavior, UIElement element)
        {
            ColumnBase columnByDragElement = this.GetColumnByDragElement(element);
            if (columnByDragElement == null)
            {
                return null;
            }
            switch (columnByDragElement.Fixed)
            {
                case FixedStyle.None:
                    return viewBehavior.FixedNoneVisibleColumns;

                case FixedStyle.Left:
                    return viewBehavior.FixedLeftVisibleColumns;
            }
            return viewBehavior.FixedRightVisibleColumns;
        }

        protected virtual int GetVisibleIndex(ColumnBase column) => 
            this.IndexOfColumnInChildrenCollection(column);

        protected override int GetVisibleIndex(DependencyObject obj, bool useVisibleIndexCorrection) => 
            !this.UseLegacyColumnVisibleIndexes ? (useVisibleIndexCorrection ? BaseColumn.GetActualCollectionIndex(obj) : BaseColumn.GetVisibleIndex(obj)) : (BaseColumn.GetActualVisibleIndex(obj) - this.DropIndexCorrection);

        private int IndexOfColumnInChildrenCollection(ColumnBase column)
        {
            for (int i = 0; i < this.Children.Count; i++)
            {
                if (this.ContainsColumn((FrameworkElement) this.Children[i], column))
                {
                    return (this.UseLegacyColumnVisibleIndexes ? i : this.GetVisibleIndex(column, true));
                }
            }
            return -1;
        }

        protected override bool IsSameSource(UIElement element) => 
            this.HeaderPresenterType == BaseGridColumnHeader.GetHeaderPresenterTypeFromGridColumnHeader(element);

        protected override void MoveColumnTo(UIElement source, int dropIndex)
        {
            this.MoveColumnToGroupMerge(source, dropIndex, MergeGroupPosition.None);
        }

        protected void MoveColumnToGroupMerge(UIElement source, int dropIndex, MergeGroupPosition mergeGroupPosition)
        {
            if (this.UseLegacyColumnVisibleIndexes)
            {
                dropIndex += this.DropIndexCorrection;
            }
            this.GetViewForDrop(source).MoveColumnTo(this.GetDependentColumnFromDragSource(source), dropIndex, this.GetColumnPresenterTypeByDragElement(source), this.HeaderPresenterType, mergeGroupPosition);
        }

        protected override void SetColumnHeaderDragIndicatorSize(DependencyObject element, double value)
        {
            DataViewBase.SetColumnHeaderDragIndicatorSize(element, value);
        }

        protected override void SetDropPlaceOrientation(DependencyObject element, Orientation value)
        {
            BaseGridHeader.SetDropPlaceOrientation(element, value);
        }

        protected virtual DataViewBase GridView =>
            DataControlBase.FindCurrentView(base.Panel);

        protected DataControlBase Grid =>
            this.GridView?.DataControl;

        protected override FrameworkElement GridElement =>
            this.Grid;

        protected override FrameworkElement DragIndicatorTemplateSource =>
            this.GridView;

        protected bool UseLegacyColumnVisibleIndexes =>
            this.GridView.UseLegacyColumnVisibleIndexes;

        private DevExpress.Xpf.Grid.HeaderPresenterType HeaderPresenterType =>
            BaseGridColumnHeader.GetHeaderPresenterTypeFromLocalValue(this.AdornableElement);

        protected override string DragIndicatorTemplatePropertyName =>
            "ColumnHeaderDragIndicatorTemplate";

        protected override string HeaderButtonTemplateName =>
            "PART_LayoutPanel";

        protected class ColumnHeaderHitTestResult : HeaderDropTargetBase.HeaderHitTestResult
        {
            protected override DropPlace GetDropPlace(DependencyObject visualHit) => 
                DropPlace.None;

            protected override DependencyObject GetGridColumn(DependencyObject visualHit)
            {
                FrameworkElement element = LayoutHelper.FindParentObject<BaseGridHeader>(visualHit);
                return ((element == null) ? null : BaseGridHeader.GetGridColumn(element));
            }
        }
    }
}

