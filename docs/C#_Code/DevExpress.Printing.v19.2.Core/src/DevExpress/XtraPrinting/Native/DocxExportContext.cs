namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class DocxExportContext : ExportContext
    {
        private DevExpress.XtraPrinting.DocxExportOptions docxExportOptions;

        public DocxExportContext(PrintingSystemBase ps, DevExpress.XtraPrinting.DocxExportOptions docxExportOptions) : base(ps)
        {
            if (docxExportOptions == null)
            {
                throw new ArgumentNullException("docxExportOptions");
            }
            this.docxExportOptions = docxExportOptions;
        }

        public override BrickViewData[] GetData(Brick brick, RectangleF rect, RectangleF clipRect) => 
            (BrickBaseExporter.GetExporter(this, brick) as BrickExporter).GetDocxData(this, rect, clipRect);

        public DevExpress.XtraPrinting.DocxExportOptions DocxExportOptions =>
            this.docxExportOptions;

        internal override PageByPageExportOptionsBase ExportOptions =>
            this.DocxExportOptions;

        public bool IsPageByPage =>
            this.docxExportOptions.ExportMode == DocxExportMode.SingleFilePageByPage;

        internal bool IsFrameExport =>
            this.IsPageByPage && !this.DocxExportOptions.TableLayout;
    }
}

