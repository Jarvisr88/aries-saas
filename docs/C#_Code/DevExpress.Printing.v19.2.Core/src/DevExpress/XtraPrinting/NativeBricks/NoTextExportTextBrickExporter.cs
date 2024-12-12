namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System.Drawing;

    public class NoTextExportTextBrickExporter : TextBrickExporter
    {
        protected internal override BrickViewData[] GetTextData(ExportContext exportContext, RectangleF rect) => 
            null;
    }
}

