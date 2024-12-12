namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;

    internal class XECheckBoxBrickExporter : CheckBoxBrickExporter
    {
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);

        internal DevExpress.XtraPrinting.CheckBoxBrick CheckBoxBrick { get; }
    }
}

