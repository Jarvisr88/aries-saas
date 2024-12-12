namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    internal abstract class RebuildBandsLayoutHelperBase
    {
        protected readonly BandsLayoutBase BandsLayout;

        public RebuildBandsLayoutHelperBase(BandsLayoutBase layout)
        {
            this.BandsLayout = layout;
        }

        public abstract void ApplyColumnVisibleIndex(BaseColumn baseColumn, int oldVisibleIndex);
        protected int GetActualBandRowIndex(ColumnBase column) => 
            this.BandsLayout.AllowBandMultiRow ? BandBase.GetGridRow(column) : 0;

        protected bool IsRootBandsOwner(IBandsOwner bandsOwner) => 
            ReferenceEquals(bandsOwner, this.BandsLayout);

        public IList<ColumnBase> RebuildVisibleColumns() => 
            new ObservableCollection<ColumnBase>(this.RebuildVisibleColumnsCore(this.BandsLayout));

        protected abstract IList<ColumnBase> RebuildVisibleColumnsCore(IBandsOwner bandsOwner);
        protected BandRow UpdateBandRows(ColumnBase column, Dictionary<int, BandRow> rows)
        {
            int actualBandRowIndex = this.GetActualBandRowIndex(column);
            BandRow row = null;
            if (!rows.TryGetValue(actualBandRowIndex, out row))
            {
                BandRow row1 = new BandRow();
                row1.Columns = new List<ColumnBase>();
                row1.Index = actualBandRowIndex;
                row = row1;
                rows[actualBandRowIndex] = row;
            }
            row.Columns.Add(column);
            return row;
        }

        public void UpdateVisibleColumnsPositions(IList<ColumnBase> visibleColumns)
        {
            for (int i = 0; i < visibleColumns.Count; i++)
            {
                ColumnBase base2 = visibleColumns[i];
                base2.ActualVisibleIndex = i;
                if (this.ApplyVisibleIndex)
                {
                    base2.VisibleIndex = i;
                }
                base2.IsFirst = i == 0;
                base2.IsLast = i == (visibleColumns.Count - 1);
                base2.UpdateHasTopElement();
                base2.UpdateHasBottomElement();
            }
        }

        protected abstract bool ApplyVisibleIndex { get; }
    }
}

