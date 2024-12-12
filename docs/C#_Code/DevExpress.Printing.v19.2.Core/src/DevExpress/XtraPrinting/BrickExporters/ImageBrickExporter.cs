namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class ImageBrickExporter : VisualBrickExporter
    {
        private bool CanSnapImage(float dpi);
        protected override System.Drawing.Image CreateTableCellImage(ITableExportProvider exportProvider, RectangleF rect);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected override System.Drawing.Image DrawContentToImage(ExportContext exportContext, RectangleF rect);
        private void DrawImage(IGraphics gr, RectangleF imageRect);
        private void DrawSvgImage(IGraphics gr, RectangleF imageRect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCellWithImage(ITableExportProvider exportProvider);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected override RectangleF GetClipRect(RectangleF rect, IGraphics gr);
        private float GetEffectiveResolution();
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected override ImageFormat GetImageFormat();
        protected override float GetImageResolution();
        protected SizeF GetResolutionImageSize(IPrintingSystemContext context);
        protected override object[] GetSpecificKeyPart();
        private System.Drawing.Image GetTileImage(System.Drawing.Image baseImage, Size clientSize);
        private static PointF SnapPointF(IGraphics gr, PointF point);

        private DevExpress.XtraPrinting.ImageBrick ImageBrick { get; }

        public ImageSizeMode SizeMode { get; set; }

        public DevExpress.XtraPrinting.ImageAlignment ImageAlignment { get; }

        public DevExpress.XtraPrinting.Drawing.ImageSource ImageSource { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public System.Drawing.Image Image { get; }
    }
}

