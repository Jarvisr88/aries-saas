namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Drawing;

    public abstract class ExportContext : GraphicsBase
    {
        protected ExportContext(PrintingSystemBase ps);
        public virtual BrickViewData CreateBrickViewData(BrickStyle style, RectangleF bounds, ITableCell tableCell);
        public BrickViewData[] CreateBrickViewDataArray(BrickStyle style, RectangleF bounds, ITableCell tableCell);
        public abstract BrickViewData[] GetData(Brick brick, RectangleF rect, RectangleF clipRect);

        public virtual bool RawDataMode { get; }

        public bool CancelPending { get; }

        public virtual bool AllowEmptyAreas { get; }

        public bool RightToLeftLayout { get; }

        internal virtual PageByPageExportOptionsBase ExportOptions { get; }

        public virtual IImagesContainer GarbageImages { get; }
    }
}

