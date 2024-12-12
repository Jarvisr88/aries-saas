namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(NullTableCellExporter))]
    public sealed class NullTableCell : ITableCell
    {
        public static readonly ITableCell Instance = new NullTableCell();

        private NullTableCell()
        {
        }

        bool ITableCell.ShouldApplyPadding =>
            false;

        string ITableCell.FormatString =>
            null;

        string ITableCell.XlsxFormatString =>
            null;

        object ITableCell.TextValue =>
            null;

        BrickModifier ITableCell.Modifier =>
            BrickModifier.None;

        DefaultBoolean ITableCell.XlsExportNativeFormat =>
            DefaultBoolean.Default;

        string ITableCell.Url =>
            null;

        bool ITableCell.HasCrossReference =>
            false;
    }
}

