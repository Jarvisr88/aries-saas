namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class GridCachedItemsControl : CachedItemsControl
    {
        private int rowMarginControlVisibleIndex = -1;
        private ColumnBase treeColumn;

        protected override void AssignVisibleIndex(FrameworkElement presenter, object item)
        {
            GridColumnData data = (GridColumnData) item;
            OrderPanelBase.SetVisibleIndex(presenter, ((this.rowMarginControlVisibleIndex < 0) || (data.VisibleIndex < this.rowMarginControlVisibleIndex)) ? data.VisibleIndex : (data.VisibleIndex + 1));
        }

        protected virtual Control CreateRowMarginControl() => 
            null;

        private int GetRowMarginControlVisibleIndex()
        {
            int visibleIndex;
            using (IEnumerator enumerator = base.ItemsSource.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        GridColumnData current = (GridColumnData) enumerator.Current;
                        GridColumnData data2 = current;
                        if ((data2 == null) || !this.View.IsTreeColumn(current.Column))
                        {
                            continue;
                        }
                        this.treeColumn = current.Column;
                        visibleIndex = current.VisibleIndex;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return visibleIndex;
        }

        protected override void OnCollectionChanged()
        {
            this.UpdateRowMarginControlVisibleIndex();
            base.OnCollectionChanged();
        }

        private void UpdateRowMarginControlVisibleIndex()
        {
            if (this.View != null)
            {
                this.treeColumn = null;
                this.rowMarginControlVisibleIndex = (this.View.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) ? this.GetRowMarginControlVisibleIndex() : -1;
            }
        }

        protected virtual void ValidateRowMarginControl()
        {
            if ((this.View != null) && (this.View.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl))
            {
                if ((this.RowMarginControl == null) && (this.rowMarginControlVisibleIndex > -1))
                {
                    this.RowMarginControl = this.CreateRowMarginControl();
                }
                if (this.RowMarginControl != null)
                {
                    OrderPanelBase.SetVisibleIndex(this.RowMarginControl, this.rowMarginControlVisibleIndex);
                    if (this.treeColumn != null)
                    {
                        ColumnBase.SetNavigationIndex(this.RowMarginControl, BaseColumn.GetActualVisibleIndex(this.treeColumn));
                    }
                    if ((base.Panel != null) && !base.Panel.Children.Contains(this.RowMarginControl))
                    {
                        base.Panel.Children.Add(this.RowMarginControl);
                    }
                }
            }
        }

        protected override void ValidateVisualTree()
        {
            base.ValidateVisualTree();
            this.ValidateRowMarginControl();
        }

        protected internal Control RowMarginControl { get; set; }

        protected internal bool IsRowMarginControlVisible =>
            (this.RowMarginControl != null) && (this.rowMarginControlVisibleIndex > -1);

        protected virtual DataViewBase View =>
            null;
    }
}

