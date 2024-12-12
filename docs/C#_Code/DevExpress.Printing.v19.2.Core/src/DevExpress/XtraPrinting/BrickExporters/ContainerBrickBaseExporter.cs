namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class ContainerBrickBaseExporter : PanelBrickExporter
    {
        protected override void ValidateInnerData(BrickViewData[] innerData, ExportContext exportContext);
    }
}

