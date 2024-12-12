namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class NormalHorizontalNavigationStrategy : HorizontalNavigationStrategyBase
    {
        public static readonly NormalHorizontalNavigationStrategy NormalHorizontalNavigationStrategyInstance = new NormalHorizontalNavigationStrategy();

        protected NormalHorizontalNavigationStrategy()
        {
        }

        public virtual void MakeCellVisible(TableViewBehavior behavior)
        {
            if (!behavior.View.IsUpdateCellDataEnqueued && behavior.View.RootDataPresenter.IsMeasureValid)
            {
                behavior.MakeCurrentCellVisible();
            }
            else
            {
                behavior.View.ImmediateActionsManager.EnqueueAction(new Action(behavior.MakeCellVisible));
            }
        }

        public virtual void UpdateFixedNoneCellData(ColumnsRowDataBase rowData, TableViewBehavior behavior)
        {
            rowData.UpdateFixedNoneCellData(false);
        }

        public virtual void UpdateViewportVisibleColumns(TableViewBehavior behavior)
        {
            behavior.TableView.ViewportVisibleColumns = null;
            behavior.ResetHorizontalVirtualizationOffset();
        }
    }
}

