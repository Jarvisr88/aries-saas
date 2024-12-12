namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class TableOfContentsLineBrickExporter : RowBrickExporter
    {
        private string GetCaptionText();
        private static bool IsPagedExport(ExportContext exportContext);
        protected override void ValidateInnerData(BrickViewData[] innerData, ExportContext exportContext);

        private DevExpress.XtraPrinting.Native.TableOfContentsLineBrick TableOfContentsLineBrick { get; }
    }
}

