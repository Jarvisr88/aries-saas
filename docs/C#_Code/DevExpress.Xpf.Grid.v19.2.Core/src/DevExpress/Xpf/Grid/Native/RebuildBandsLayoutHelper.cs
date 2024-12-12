namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class RebuildBandsLayoutHelper : RebuildBandsLayoutHelperBase
    {
        public RebuildBandsLayoutHelper(BandsLayoutBase layout) : base(layout)
        {
        }

        public override void ApplyColumnVisibleIndex(BaseColumn baseColumn, int oldVisibleIndex)
        {
            if (baseColumn is BandBase)
            {
                this.AppylBandVisibleIndexCore((BandBase) baseColumn, oldVisibleIndex);
            }
            else
            {
                this.ApplyColumnVisibleIndexCore((ColumnBase) baseColumn, oldVisibleIndex);
            }
        }

        private void ApplyColumnVisibleIndexCore(ColumnBase source, int oldVisibleIndex)
        {
            int targetVisibleIndex = -1;
            if (source.ParentBand != null)
            {
                RebuildColumnsLayoutHelper.ApplyVisibleIndexInBounds(this.GetAllBandsRows(source.ParentBand)[base.GetActualBandRowIndex(source)].Columns, source, oldVisibleIndex, delegate (BaseColumn column, bool isMoveToRight) {
                    if (isMoveToRight || (targetVisibleIndex == -1))
                    {
                        targetVisibleIndex = column.VisibleIndex;
                    }
                });
            }
            if (targetVisibleIndex == -1)
            {
                targetVisibleIndex = oldVisibleIndex;
            }
            BaseColumn.SetVisibleIndex(source, targetVisibleIndex);
        }

        private void AppylBandVisibleIndexCore(BandBase source, int oldVisibleIndex)
        {
            IList bandsCore = source.Owner.BandsCore;
            int index = RebuildColumnsLayoutHelperBase.CorrectVisibleIndex(bandsCore, source, ReferenceEquals(source.Owner, base.BandsLayout));
            BaseColumn.SetVisibleIndex(source, index);
            RebuildColumnsLayoutHelper.ApplyVisibleIndexInBounds(bandsCore, source, oldVisibleIndex);
        }

        private Dictionary<int, BandRow> GetAllBandsRows(BandBase band)
        {
            Dictionary<int, BandRow> rows = new Dictionary<int, BandRow>();
            for (int i = 0; i < band.ColumnsCore.Count; i++)
            {
                ColumnBase column = band.ColumnsCore[i] as ColumnBase;
                column.index = i;
                base.UpdateBandRows(column, rows);
            }
            return rows;
        }

        private int RebuildBandLayout(BandBase band, bool isBandActualVisible, List<ColumnBase> visibleColumns, int columnVisibleIndexOffset)
        {
            band.ActualRows.Clear();
            Dictionary<int, BandRow> allBandsRows = this.GetAllBandsRows(band);
            Func<int, int> keySelector = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<int, int> local1 = <>c.<>9__4_0;
                keySelector = <>c.<>9__4_0 = i => i;
            }
            foreach (int num in allBandsRows.Keys.OrderBy<int, int>(keySelector))
            {
                BandRow row = allBandsRows[num];
                row.Columns.Sort(new Comparison<ColumnBase>(base.BandsLayout.DataControl.viewCore.VisibleComparison));
                BandRow row1 = new BandRow();
                row1.Index = row.Index;
                row1.Columns = new List<ColumnBase>();
                BandRow item = row1;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= row.Columns.Count)
                    {
                        columnVisibleIndexOffset += row.Columns.Count;
                        if (item.Columns.Count > 0)
                        {
                            band.ActualRows.Add(item);
                        }
                        break;
                    }
                    ColumnBase col = row.Columns[num2];
                    col.VisibleIndex = columnVisibleIndexOffset + num2;
                    if (!isBandActualVisible || ((band.VisibleBands.Count > 0) || (!col.Visible || !base.BandsLayout.DataControl.viewCore.IsColumnVisibleInHeaders(col))))
                    {
                        col.BandRow = null;
                    }
                    else
                    {
                        item.Columns.Add(col);
                        visibleColumns.Add(col);
                        col.BandRow = item;
                    }
                    num2++;
                }
            }
            return columnVisibleIndexOffset;
        }

        private int RebuildBandsCore(IBandsOwner bandsOwner, bool isBandVisible, List<ColumnBase> visibleColumns, int columnVisibleIndexOffset)
        {
            bandsOwner.VisibleBands.Clear();
            bool hasFixedLeftBands = false;
            List<BandBase> bands = new List<BandBase>();
            for (int i = 0; i < bandsOwner.BandsCore.Count; i++)
            {
                BandBase item = bandsOwner.BandsCore[i] as BandBase;
                item.index = i;
                bands.Add(item);
                if (item.Visible && (item.Fixed == FixedStyle.Left))
                {
                    hasFixedLeftBands = true;
                }
            }
            bands.Sort(new Comparison<BandBase>(base.BandsLayout.DataControl.viewCore.VisibleComparison));
            if (base.IsRootBandsOwner(bandsOwner))
            {
                base.BandsLayout.PatchBands(bands, hasFixedLeftBands);
            }
            int num = 0;
            for (int j = 0; j < bands.Count; j++)
            {
                BandBase item = bands[j];
                item.VisibleIndex = j;
                if (!item.Visible)
                {
                    num++;
                }
                else
                {
                    bandsOwner.VisibleBands.Add(item);
                    item.ActualVisibleIndex = j - num;
                }
                bool flag2 = isBandVisible && item.Visible;
                columnVisibleIndexOffset = this.RebuildBandsCore(item, flag2, visibleColumns, columnVisibleIndexOffset);
                columnVisibleIndexOffset = this.RebuildBandLayout(item, flag2, visibleColumns, columnVisibleIndexOffset);
            }
            return columnVisibleIndexOffset;
        }

        protected override IList<ColumnBase> RebuildVisibleColumnsCore(IBandsOwner bandsOwner)
        {
            List<ColumnBase> visibleColumns = new List<ColumnBase>();
            this.RebuildBandsCore(bandsOwner, true, visibleColumns, 0);
            return visibleColumns;
        }

        protected override bool ApplyVisibleIndex =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RebuildBandsLayoutHelper.<>c <>9 = new RebuildBandsLayoutHelper.<>c();
            public static Func<int, int> <>9__4_0;

            internal int <RebuildBandLayout>b__4_0(int i) => 
                i;
        }
    }
}

