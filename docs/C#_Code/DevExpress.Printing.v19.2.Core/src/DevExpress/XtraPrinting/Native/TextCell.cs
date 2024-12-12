namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using System;

    [BrickExporter(typeof(TextCellExporter))]
    public class TextCell : CellWrapper
    {
        private string text;

        public TextCell(ITableCell innerCell, string text) : base(innerCell)
        {
            this.text = text;
        }

        internal string Text =>
            this.text;
    }
}

