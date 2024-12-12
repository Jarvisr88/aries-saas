namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GridPrintViewInfo : GridViewInfo
    {
        private BandsLayoutBase bandsLayoutCore;
        private readonly List<ColumnBase> visibleColumns;

        public GridPrintViewInfo(DataViewBase view, BandsLayoutBase bandsLayout) : base(view)
        {
            this.visibleColumns = view.PrintableColumns.ToList<ColumnBase>();
            this.bandsLayoutCore = bandsLayout;
        }

        protected override bool GetAutoWidth() => 
            !base.HasStarredColumns();

        public override BandsLayoutBase BandsLayout =>
            this.bandsLayoutCore;

        public override IList<ColumnBase> VisibleColumns =>
            this.visibleColumns;

        internal override double NewItemRowIndent =>
            this.TotalGroupAreaIndent;

        internal override bool IsPrinting =>
            true;
    }
}

