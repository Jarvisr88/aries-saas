namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class FixedDropTarget : DropTarget
    {
        protected FixedDropTarget(Panel panel) : base(panel)
        {
        }

        private ColumnBase GetColumn(FrameworkElement element) => 
            element.DataContext as ColumnBase;

        protected override int GetFirstActualCollectionIndex() => 
            (this.Children.Count > 0) ? BaseColumn.GetActualCollectionIndex(this.GetColumn((FrameworkElement) this.Children[0])) : 0;

        protected int GetFirstActualVisibleIndex() => 
            (this.Children.Count > 0) ? BaseColumn.GetActualVisibleIndex(this.GetColumn((FrameworkElement) this.Children[0])) : 0;

        protected int GetFirstVisibleIndex() => 
            (this.Children.Count > 0) ? BaseColumn.GetVisibleIndex(this.GetColumn((FrameworkElement) this.Children[0])) : 0;

        protected bool WouldBeFirstFixedNoneColumn(ColumnBase sourceColumn) => 
            (sourceColumn.Fixed == FixedStyle.None) && (this.FixedNoneVisibleColumnsCount == 0);

        protected ITableView TableView =>
            (ITableView) this.GridView;

        protected override IList Children =>
            (IList) ((OrderPanelBase) base.Panel).GetSortedChildren();

        protected int FixedNoneColumnsCount =>
            base.UseLegacyColumnVisibleIndexes ? this.FixedNoneVisibleColumnsCount : this.TableView.TableViewBehavior.GetFixedNoneColumnsCount();

        protected int FixedLeftColumnsCount =>
            base.UseLegacyColumnVisibleIndexes ? this.FixedLeftVisibleColumnsCount : this.TableView.TableViewBehavior.GetFixedLeftColumnsCount();

        protected int FixedRightColumnsCount =>
            base.UseLegacyColumnVisibleIndexes ? this.FixedRightVisibleColumnsCount : this.TableView.TableViewBehavior.GetFixedRightColumnsCount();

        protected int FixedNoneVisibleColumnsCount =>
            this.TableView.TableViewBehavior.FixedNoneVisibleColumns.Count;

        protected int FixedLeftVisibleColumnsCount =>
            this.TableView.TableViewBehavior.FixedLeftVisibleColumns.Count;

        protected int FixedRightVisibleColumnsCount =>
            this.TableView.TableViewBehavior.FixedRightVisibleColumns.Count;
    }
}

