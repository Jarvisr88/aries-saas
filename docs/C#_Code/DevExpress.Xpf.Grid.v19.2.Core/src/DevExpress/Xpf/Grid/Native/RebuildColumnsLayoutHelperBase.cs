namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class RebuildColumnsLayoutHelperBase
    {
        protected readonly DataViewBase View;

        public RebuildColumnsLayoutHelperBase(DataViewBase view)
        {
            this.View = view;
        }

        public void ApplyColumnVisibleIndex(BaseColumn source, int oldVisibleIndex)
        {
            if ((this.View.DataControl != null) && (this.View.DataControl.BandsLayoutCore != null))
            {
                this.View.DataControl.BandsLayoutCore.ApplyColumnVisibleIndex(source, oldVisibleIndex);
            }
            else
            {
                ColumnBase base2 = (ColumnBase) source;
                this.ApplyColumnVisibleIndexCore(base2, CorrectVisibleIndex(this.Columns, base2, true), oldVisibleIndex);
            }
        }

        protected abstract void ApplyColumnVisibleIndexCore(ColumnBase source, int visibleIndex, int oldVisibleIndex);
        private void AssignColumnPosition(ColumnBase column, int index, int visibleColumnsCount)
        {
            if (this.View is ITableView)
            {
                AssignColumnPosition(column, index, visibleColumnsCount, (ITableView) this.View);
            }
        }

        internal static void AssignColumnPosition(ColumnBase column, int index, int visibleColumnsCount, ITableView view)
        {
            column.ColumnPosition = GetColumnPosition(index, view.ShowIndicator, ((DataViewBase) view).IsRootView, view.ActualShowDetailHeader);
            column.IsLast = index == (visibleColumnsCount - 1);
            column.IsFirst = index == 0;
        }

        internal static int CorrectVisibleIndex(IList parentCollection, BaseColumn source, bool applyCkeckBoxColumnCorrection)
        {
            int num = parentCollection.Contains(source) ? (parentCollection.Count - 1) : parentCollection.Count;
            if (applyCkeckBoxColumnCorrection && source.View.IsCheckBoxSelectorColumnVisible)
            {
                num++;
            }
            int num2 = Math.Max(Math.Min(source.VisibleIndex, num), 0);
            return ((!applyCkeckBoxColumnCorrection || (!source.View.IsCheckBoxSelectorColumnVisible || (num2 != 0))) ? num2 : 1);
        }

        public abstract Tuple<ColumnBase, BandedViewDropPlace> GetColumnDropTarget(ColumnBase source, int targetVisibleIndex, HeaderPresenterType moveFrom);
        private static ColumnPosition GetColumnPosition(int index, bool showIndicator, bool isRootView, bool showDetailButtons) => 
            (!((index == 0) & isRootView) || (showIndicator || showDetailButtons)) ? ColumnPosition.Middle : ColumnPosition.Left;

        public abstract int GetFixedLeftColumnsCount(TableViewBehavior tableViewBehavior);
        public abstract int GetFixedNoneColumnsCount(TableViewBehavior tableViewBehavior);
        public abstract int GetFixedRightColumnsCount(TableViewBehavior tableViewBehavior);
        protected bool IsColumnVisible(ColumnBase column) => 
            column.Visible && this.View.IsColumnVisibleInHeaders(column);

        protected void PatchColumns(IList<ColumnBase> columns, bool hasFixedLeftColumns)
        {
            if (this.View.IsCheckBoxSelectorColumnVisible)
            {
                this.View.CheckBoxSelectorColumn.Fixed = hasFixedLeftColumns ? FixedStyle.Left : FixedStyle.None;
                columns.Insert(0, this.View.CheckBoxSelectorColumn);
            }
        }

        public void RebuildColumns()
        {
            IList<ColumnBase> visibleColumns = null;
            this.View.UpdateVisibleIndexesLocker.DoLockedAction<IList<ColumnBase>>(delegate {
                IList<ColumnBase> list;
                visibleColumns = list = this.RebuildVisibleColumns();
                return list;
            });
            this.View.AssignVisibleColumns(visibleColumns);
        }

        private IList<ColumnBase> RebuildVisibleColumns() => 
            (!(this.View is ITableView) || (this.View.DataControl.BandsLayoutCore == null)) ? this.RebuildVisibleColumnsCore() : this.View.DataControl.BandsLayoutCore.RebuildBandsVisibleColumns();

        protected abstract IList<ColumnBase> RebuildVisibleColumnsCore();
        public void UpdateColumnsPositions(IList<ColumnBase> visibleColumns)
        {
            this.View.UpdateVisibleIndexesLocker.DoLockedAction(() => this.UpdateVisibleColumnsPositions(visibleColumns));
            this.View.AssignVisibleColumns(visibleColumns);
        }

        protected void UpdateVisibleColumnsPositions(IList<ColumnBase> visibleColumnsList)
        {
            if ((this.View is ITableView) && (this.View.DataControl.BandsLayoutCore != null))
            {
                this.View.DataControl.BandsLayoutCore.UpdateVisibleColumnsAndBands(visibleColumnsList, null);
            }
            else
            {
                for (int i = 0; i < visibleColumnsList.Count; i++)
                {
                    ColumnBase element = visibleColumnsList[i];
                    BaseColumn.SetActualVisibleIndex(element, i);
                    if (this.ApplyVisibleIndex)
                    {
                        BaseColumn.SetVisibleIndex(element, i);
                    }
                    this.AssignColumnPosition(element, i, visibleColumnsList.Count);
                }
            }
        }

        protected IColumnCollection Columns =>
            this.View.ColumnsCore;

        protected abstract bool ApplyVisibleIndex { get; }
    }
}

