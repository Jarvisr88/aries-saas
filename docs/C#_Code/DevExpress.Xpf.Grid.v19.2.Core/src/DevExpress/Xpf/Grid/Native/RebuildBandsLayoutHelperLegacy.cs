namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class RebuildBandsLayoutHelperLegacy : RebuildBandsLayoutHelperBase
    {
        public RebuildBandsLayoutHelperLegacy(BandsLayoutBase layout) : base(layout)
        {
        }

        public override void ApplyColumnVisibleIndex(BaseColumn baseColumn, int oldVisibleIndex)
        {
            List<BaseColumn> list = null;
            if (baseColumn is BandBase)
            {
                list = ((BandBase) baseColumn).Owner.VisibleBands.ToList<BaseColumn>();
            }
            else
            {
                Func<BandRow, List<BaseColumn>> evaluator = <>c.<>9__3_1;
                if (<>c.<>9__3_1 == null)
                {
                    Func<BandRow, List<BaseColumn>> local1 = <>c.<>9__3_1;
                    evaluator = <>c.<>9__3_1 = x => x.Columns.ToList<BaseColumn>();
                }
                list = baseColumn.ParentBand.ActualRows.FirstOrDefault<BandRow>(x => (x.Index == BandBase.GetGridRow(baseColumn))).With<BandRow, List<BaseColumn>>(evaluator);
            }
            if (list != null)
            {
                Func<BaseColumn, int> keySelector = <>c.<>9__3_3;
                if (<>c.<>9__3_3 == null)
                {
                    Func<BaseColumn, int> local2 = <>c.<>9__3_3;
                    keySelector = <>c.<>9__3_3 = y => y.VisibleIndex;
                }
                list = (from x in list
                    where !ReferenceEquals(x, baseColumn)
                    select x).OrderBy<BaseColumn, int>(keySelector).ToList<BaseColumn>();
                list.Insert(Math.Max(0, Math.Min(list.Count, baseColumn.VisibleIndex)), baseColumn);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].VisibleIndex = i;
                }
            }
        }

        private List<ColumnBase> RebuildBandLayout(BandBase band)
        {
            List<ColumnBase> list = new List<ColumnBase>();
            band.ActualRows.Clear();
            if (band.Visible && ((band.VisibleBands.Count <= 0) && (band.ColumnsCore.Count != 0)))
            {
                Dictionary<int, BandRow> rows = new Dictionary<int, BandRow>();
                for (int j = 0; j < band.ColumnsCore.Count; j++)
                {
                    ColumnBase col = band.ColumnsCore[j] as ColumnBase;
                    col.index = j;
                    col.BandRow = (!col.Visible || !base.BandsLayout.DataControl.viewCore.IsColumnVisibleInHeaders(col)) ? null : base.UpdateBandRows(col, rows);
                }
                Func<int, int> keySelector = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<int, int> local1 = <>c.<>9__2_0;
                    keySelector = <>c.<>9__2_0 = i => i;
                }
                foreach (int num2 in rows.Keys.OrderBy<int, int>(keySelector))
                {
                    BandRow item = rows[num2];
                    item.Columns.Sort(new Comparison<ColumnBase>(base.BandsLayout.DataControl.viewCore.VisibleComparison));
                    foreach (ColumnBase base3 in item.Columns)
                    {
                        list.Add(base3);
                    }
                    band.ActualRows.Add(item);
                }
            }
            return list;
        }

        protected override IList<ColumnBase> RebuildVisibleColumnsCore(IBandsOwner bandsOwner)
        {
            List<ColumnBase> list = new List<ColumnBase>();
            bandsOwner.VisibleBands.Clear();
            bool hasFixedLeftBands = false;
            for (int i = 0; i < bandsOwner.BandsCore.Count; i++)
            {
                BandBase item = bandsOwner.BandsCore[i] as BandBase;
                item.index = i;
                if (item.Visible)
                {
                    bandsOwner.VisibleBands.Add(item);
                    if (item.Fixed == FixedStyle.Left)
                    {
                        hasFixedLeftBands = true;
                    }
                }
            }
            bandsOwner.VisibleBands.Sort(new Comparison<BandBase>(base.BandsLayout.DataControl.viewCore.VisibleComparison));
            if (base.IsRootBandsOwner(bandsOwner))
            {
                base.BandsLayout.PatchBands(base.BandsLayout.VisibleBands, hasFixedLeftBands);
            }
            for (int j = 0; j < bandsOwner.VisibleBands.Count; j++)
            {
                BandBase base3 = bandsOwner.VisibleBands[j];
                base3.VisibleIndex = j;
                base3.ActualVisibleIndex = j;
                list.AddRange(this.RebuildVisibleColumnsCore(base3));
                list.AddRange(this.RebuildBandLayout(base3));
            }
            return list;
        }

        protected override bool ApplyVisibleIndex =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RebuildBandsLayoutHelperLegacy.<>c <>9 = new RebuildBandsLayoutHelperLegacy.<>c();
            public static Func<int, int> <>9__2_0;
            public static Func<BandRow, List<BaseColumn>> <>9__3_1;
            public static Func<BaseColumn, int> <>9__3_3;

            internal List<BaseColumn> <ApplyColumnVisibleIndex>b__3_1(BandRow x) => 
                x.Columns.ToList<BaseColumn>();

            internal int <ApplyColumnVisibleIndex>b__3_3(BaseColumn y) => 
                y.VisibleIndex;

            internal int <RebuildBandLayout>b__2_0(int i) => 
                i;
        }
    }
}

