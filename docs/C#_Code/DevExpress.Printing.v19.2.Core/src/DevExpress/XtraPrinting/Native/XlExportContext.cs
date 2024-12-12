namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public class XlExportContext : ExportContext
    {
        private ImagesCache garbageImages;
        private XlExportOptionsBase xlExportOptions;

        public XlExportContext(PrintingSystemBase ps, XlExportOptionsBase xlExportOptions);
        public void Clear();
        public override BrickViewData[] GetData(Brick brick, RectangleF rect, RectangleF clipRect);

        public override bool RawDataMode { get; }

        public XlExportOptionsBase XlExportOptions { get; }

        internal override PageByPageExportOptionsBase ExportOptions { get; }

        public override IImagesContainer GarbageImages { get; }

        public bool IsPageExport { get; }

        private class XlImagesCache : ImagesCache
        {
            protected override void OnRemoveImage(Image image);
        }
    }
}

