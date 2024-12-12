namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class RtfPageLayoutBuilder : PageLayoutBuilder
    {
        public RtfPageLayoutBuilder(Page page, RtfExportContext rtfExportContext) : base(page, rtfExportContext)
        {
        }

        internal override RectangleF GetCorrectClipRect(RectangleF clipRect) => 
            clipRect;

        internal override BrickViewData[] GetData(Brick brick, RectangleF bounds, RectangleF clipRect)
        {
            BrickViewData[] dataArray = ((BrickExporter) BrickBaseExporter.GetExporter(base.exportContext, brick)).GetRtfData((RtfExportContext) base.exportContext, bounds, clipRect);
            if ((dataArray.Length != 2) || !(dataArray[0].Bounds == dataArray[1].Bounds))
            {
                return dataArray;
            }
            return new BrickViewData[] { dataArray[1] };
        }
    }
}

