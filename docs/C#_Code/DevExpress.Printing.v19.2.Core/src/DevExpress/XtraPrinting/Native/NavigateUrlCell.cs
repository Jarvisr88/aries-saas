namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using System;

    [BrickExporter(typeof(NavigateUrlCellExporter))]
    public class NavigateUrlCell : CellWrapper
    {
        private readonly DevExpress.XtraPrinting.VisualBrick brick;

        public NavigateUrlCell(ITableCell innerCell, DevExpress.XtraPrinting.VisualBrick brick) : base(innerCell)
        {
            this.brick = brick;
        }

        internal DevExpress.XtraPrinting.VisualBrick VisualBrick =>
            this.brick;
    }
}

