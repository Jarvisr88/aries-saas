namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class ServiceColumnHeaderDropTarget : FixedDropTarget
    {
        public ServiceColumnHeaderDropTarget(GridColumnHeaderBase columnHeader) : base(null)
        {
            this.ColumnHeader = columnHeader;
        }

        protected override Point CorrectDragIndicatorLocation(Point point) => 
            this.GridView.CorrectDragIndicatorLocation(this.ColumnHeader, point);

        protected override int GetDragIndex(int dropIndex, Point pt) => 
            0;

        protected override Orientation GetDropPlaceOrientation(DependencyObject element) => 
            Orientation.Horizontal;

        protected override object GetHeaderButtonOwner(int index) => 
            this.ColumnHeader;

        protected bool IsFirstColumnOfItsType(ColumnBase column)
        {
            switch (column.Fixed)
            {
                case FixedStyle.None:
                    return ((base.FixedNoneVisibleColumnsCount != 0) && (base.TableView.TableViewBehavior.FixedNoneVisibleColumns[0] == column));

                case FixedStyle.Left:
                    return ((base.FixedLeftVisibleColumnsCount != 0) && (base.TableView.TableViewBehavior.FixedLeftVisibleColumns[0] == column));

                case FixedStyle.Right:
                    return ((base.FixedRightVisibleColumnsCount != 0) && (base.TableView.TableViewBehavior.FixedRightVisibleColumns[0] == column));
            }
            return false;
        }

        protected bool IsLastColumnOfItsType(ColumnBase column)
        {
            switch (column.Fixed)
            {
                case FixedStyle.None:
                    return ((base.FixedNoneVisibleColumnsCount != 0) && (base.TableView.TableViewBehavior.FixedNoneVisibleColumns[base.FixedNoneVisibleColumnsCount - 1] == column));

                case FixedStyle.Left:
                    return ((base.FixedLeftVisibleColumnsCount != 0) && (base.TableView.TableViewBehavior.FixedLeftVisibleColumns[base.FixedLeftVisibleColumnsCount - 1] == column));

                case FixedStyle.Right:
                    return ((base.FixedRightVisibleColumnsCount != 0) && (base.TableView.TableViewBehavior.FixedRightVisibleColumns[base.FixedRightVisibleColumnsCount - 1] == column));
            }
            return false;
        }

        protected override IList Children =>
            null;

        protected override int ChildrenCount =>
            1;

        public GridColumnHeaderBase ColumnHeader { get; private set; }

        protected override DataViewBase GridView =>
            DataControlBase.FindCurrentView(this.ColumnHeader);

        protected override UIElement AdornableElement =>
            this.ColumnHeader;

        protected bool ThereIsNoFixedNoneColumns =>
            base.FixedNoneVisibleColumnsCount == 0;

        protected bool ThereIsNoFixedRightColumns =>
            base.FixedRightVisibleColumnsCount == 0;

        protected bool ThereIsNoFixedLeftColumns =>
            base.FixedLeftVisibleColumnsCount == 0;

        protected bool AllowDragToServiceColumn =>
            base.Grid.BandsCore.Count == 0;

        protected ScrollInfoBase ScrollInfo =>
            this.GridView.DataPresenter?.ScrollInfoCore.HorizontalScrollInfo;
    }
}

