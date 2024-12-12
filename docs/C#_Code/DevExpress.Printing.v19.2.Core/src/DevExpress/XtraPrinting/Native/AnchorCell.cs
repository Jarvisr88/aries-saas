namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using System;

    [BrickExporter(typeof(AnchorCellExporter))]
    public class AnchorCell : CellWrapper
    {
        private string anchorName;

        public AnchorCell(ITableCell innerCell, string anchorName) : base(innerCell)
        {
            this.anchorName = anchorName;
        }

        internal string AnchorName =>
            this.anchorName;
    }
}

