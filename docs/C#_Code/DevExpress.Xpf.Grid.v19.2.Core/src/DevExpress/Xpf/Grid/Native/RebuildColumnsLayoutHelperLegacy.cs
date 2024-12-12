namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class RebuildColumnsLayoutHelperLegacy : RebuildColumnsLayoutHelperBase
    {
        public RebuildColumnsLayoutHelperLegacy(DataViewBase view) : base(view)
        {
        }

        protected override void ApplyColumnVisibleIndexCore(ColumnBase source, int visibleIndex, int oldVisibleIndex)
        {
            ObservableCollection<ColumnBase> visibleColumns = new ObservableCollection<ColumnBase>(base.View.VisibleColumnsCore);
            visibleColumns.Remove(source);
            if (visibleIndex >= visibleColumns.Count)
            {
                visibleColumns.Add(source);
            }
            else
            {
                visibleColumns.Insert(visibleIndex, source);
            }
            base.UpdateColumnsPositions(visibleColumns);
        }

        public override Tuple<ColumnBase, BandedViewDropPlace> GetColumnDropTarget(ColumnBase source, int targetVisibleIndex, HeaderPresenterType moveFrom) => 
            (base.View.VisibleColumnsCore.Count != 0) ? ((base.View.VisibleColumnsCore.Count > targetVisibleIndex) ? ((targetVisibleIndex >= 0) ? new Tuple<ColumnBase, BandedViewDropPlace>(base.View.VisibleColumnsCore[targetVisibleIndex], BandedViewDropPlace.Left) : new Tuple<ColumnBase, BandedViewDropPlace>(base.View.VisibleColumnsCore[0], BandedViewDropPlace.Left)) : new Tuple<ColumnBase, BandedViewDropPlace>(base.View.VisibleColumnsCore[base.View.VisibleColumnsCore.Count - 1], BandedViewDropPlace.Right)) : new Tuple<ColumnBase, BandedViewDropPlace>(source, BandedViewDropPlace.Left);

        public override int GetFixedLeftColumnsCount(TableViewBehavior tableViewBehavior) => 
            tableViewBehavior.FixedLeftVisibleColumns.Count;

        public override int GetFixedNoneColumnsCount(TableViewBehavior tableViewBehavior) => 
            tableViewBehavior.FixedNoneVisibleColumns.Count;

        public override int GetFixedRightColumnsCount(TableViewBehavior tableViewBehavior) => 
            tableViewBehavior.FixedRightVisibleColumns.Count;

        protected override IList<ColumnBase> RebuildVisibleColumnsCore()
        {
            List<ColumnBase> columns = new List<ColumnBase>();
            bool hasFixedLeftColumns = false;
            for (int i = 0; i < base.Columns.Count; i++)
            {
                ColumnBase column = base.Columns[i];
                column.index = i;
                if (base.IsColumnVisible(column))
                {
                    columns.Add(column);
                    hasFixedLeftColumns |= column.Fixed == FixedStyle.Left;
                }
            }
            base.PatchColumns(columns, hasFixedLeftColumns);
            columns.Sort(new Comparison<ColumnBase>(base.View.VisibleComparison));
            base.UpdateVisibleColumnsPositions(columns);
            return columns;
        }

        protected override bool ApplyVisibleIndex =>
            true;
    }
}

