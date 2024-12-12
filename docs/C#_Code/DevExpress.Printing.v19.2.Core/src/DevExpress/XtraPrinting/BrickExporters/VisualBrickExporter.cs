namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class VisualBrickExporter : BrickExporter
    {
        private PaddingInfo CalcCropping(BrickViewData data);
        private static Bitmap CreateBitmap(int width, int height);
        public BrickImageProviderBase CreateImageProvider();
        private object CreateKey(RectangleF rect, float resolution);
        private void CreatePdfNavigation(IPdfGraphics gr, RectangleF bounds);
        protected void CreatePdfUrlNavigation(IPdfGraphics gr, string url, RectangleF bounds);
        protected virtual Image CreateTableCellImage(ITableExportProvider exportProvider, RectangleF rect);
        protected Image CreateTableCellImageCore(ITableExportProvider exportProvider, RectangleF rect, bool patchTransparentBackground, Action<Graphics> customization);
        protected virtual RectangleF DeflateBorderWidth(RectangleF rect);
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect);
        protected virtual void DrawBackground(IGraphics gr, RectangleF rect);
        private void DrawBorders(IGraphics gr, RectangleF rect);
        protected virtual void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected virtual Image DrawContentToImage(ExportContext exportContext, RectangleF rect);
        public Image DrawContentToImage(PrintingSystemBase ps, ImagesCache container, RectangleF rect, bool patchTransparentBackground, float resolution);
        [Obsolete("Use the CreateImageProvider() method instead")]
        public Image DrawContentToImage(PrintingSystemBase ps, RectangleF rect, bool drawBorders, bool patchTransparentBackground, float resolution);
        [Obsolete("Use the DrawContentToImage(PrintingSystemBase ps, ImagesContainer container, RectangleF rect, bool patchTransparentBackground, float resolution) method instead")]
        public Image DrawContentToImage(PrintingSystemBase ps, ImagesCache container, RectangleF rect, bool drawBorders, bool patchTransparentBackground, float resolution);
        protected virtual void DrawObject(IGraphics gr, RectangleF rect);
        protected void DrawObjectCore(IGraphics gr, RectangleF rect);
        protected virtual void DrawObjectCoreWithClipping(IGraphics gr, RectangleF rect);
        protected virtual void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected internal override void FillHtmlTableCellInternal(IHtmlExportProvider exportProvider);
        protected void FillHtmlTableCellWithImage(IHtmlExportProvider exportProvider);
        protected virtual void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        protected internal override void FillRtfTableCellInternal(IRtfExportProvider exportProvider);
        protected void FillTableCellWithImage(ITableExportProvider exportProvider, ImageSizeMode sizeMode, ImageAlignment align, Rectangle bounds);
        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText);
        protected void FillXlTableCellWithShape(ITableExportProvider exportProvider, ShapeBase shape, TableCellLineInfo lineInfo, Color fillColor, int angle, PaddingInfo padding);
        protected static string GetActualText(string text);
        internal BrickStyle GetAreaStyle(ExportContext exportContext, RectangleF area, RectangleF baseBounds);
        protected Rectangle GetBoundsWithoutBorders(Rectangle bounds);
        protected virtual RectangleF GetClientRect(RectangleF rect, IGraphics gr);
        protected virtual RectangleF GetClipRect(RectangleF rect, IGraphics gr);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected virtual ImageFormat GetImageFormat();
        protected virtual float GetImageResolution();
        protected virtual object[] GetSpecificKeyPart();
        private Image GetTableCellImage(ITableExportProvider exportProvider);
        private void SetGoToArea(IPdfGraphics gr, RectangleF bounds);
        protected internal static void SetHtmlAnchor(DXWebControlBase control, string anchorName, HtmlExportContext htmlExportContext);

        protected DevExpress.XtraPrinting.VisualBrick VisualBrick { get; }

        protected internal BrickPaintBase BrickPaint { get; private set; }

        private IBrickOwner BrickOwner { get; }

        protected BrickStyle Style { get; }

        protected string Text { get; set; }

        public static bool IsMetafileExportAvailable { get; }

        private class BrickImageProvider : BrickImageProviderBase
        {
            private VisualBrickExporter exporter;

            public BrickImageProvider(VisualBrickExporter exporter);
            public override Image CreateContentImage(PrintingSystemBase ps, RectangleF rect, bool patchTransparentBackground, float resolution, ImageFormat imageFormat, Action<Graphics> customization);
            public override Image CreateContentMetafileImage(PrintingSystemBase ps, RectangleF rect, bool patchTransparentBackground);
            public override Image CreateImage(PrintingSystemBase ps, RectangleF rect, float resolution, ImageFormat imageFormat, Action<Graphics> customization);
            public override Image CreateMetafileImage(PrintingSystemBase ps, RectangleF rect);
        }
    }
}

