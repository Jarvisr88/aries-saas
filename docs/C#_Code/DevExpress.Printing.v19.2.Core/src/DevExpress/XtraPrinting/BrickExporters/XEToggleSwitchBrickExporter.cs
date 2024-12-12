namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;

    internal class XEToggleSwitchBrickExporter : ToggleSwitchBrickExporter
    {
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);

        internal DevExpress.XtraPrinting.ToggleSwitchBrick ToggleSwitchBrick { get; }
    }
}

