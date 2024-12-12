namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using System;

    [BrickExporter(typeof(CellWrapperExporter))]
    public abstract class CellWrapper : ITableCell
    {
        private ITableCell innerCell;

        public CellWrapper(ITableCell innerCell)
        {
            this.innerCell = innerCell;
        }

        string ITableCell.FormatString =>
            this.innerCell.FormatString;

        string ITableCell.XlsxFormatString =>
            this.innerCell.XlsxFormatString;

        object ITableCell.TextValue =>
            this.innerCell.TextValue;

        DefaultBoolean ITableCell.XlsExportNativeFormat =>
            this.innerCell.XlsExportNativeFormat;

        string ITableCell.Url =>
            this.innerCell.Url;

        bool ITableCell.HasCrossReference =>
            this.innerCell.HasCrossReference;

        BrickModifier ITableCell.Modifier =>
            this.innerCell.Modifier;

        internal ITableCell InnerCell =>
            this.innerCell;

        bool ITableCell.ShouldApplyPadding =>
            this.innerCell.ShouldApplyPadding;
    }
}

