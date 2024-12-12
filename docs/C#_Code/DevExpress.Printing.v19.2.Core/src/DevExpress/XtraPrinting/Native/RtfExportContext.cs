namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Export.Rtf;
    using System;
    using System.Drawing;

    public class RtfExportContext : ExportContext
    {
        private DevExpress.XtraPrinting.Export.Rtf.RtfExportHelper rtfExportHelper;
        private DevExpress.XtraPrinting.RtfExportOptions rtfExportOptions;

        public RtfExportContext(PrintingSystemBase ps, DevExpress.XtraPrinting.RtfExportOptions rtfExportOptions, DevExpress.XtraPrinting.Export.Rtf.RtfExportHelper rtfExportHelper);
        public override BrickViewData[] GetData(Brick brick, RectangleF rect, RectangleF clipRect);

        public DevExpress.XtraPrinting.Export.Rtf.RtfExportHelper RtfExportHelper { get; }

        public DevExpress.XtraPrinting.RtfExportOptions RtfExportOptions { get; }

        internal override PageByPageExportOptionsBase ExportOptions { get; }

        public bool IsPageByPage { get; }
    }
}

