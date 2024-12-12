namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PdfGraphicsDocument : PdfDisposableObject, IPdfDocumentContext
    {
        private readonly PdfExportFontManager fontCache;
        private readonly PdfXObjectResourceCache imageCache;
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly bool disposeFontCache;
        private readonly PdfStrokingAlphaCache strokingAlphaCache;
        private readonly PdfNonStrokingAlphaCache nonStrokingAlphaCache;
        private readonly PdfShadingCache shadingCache;
        private readonly PdfHatchPatternCache hatchPatternCache;

        public PdfGraphicsDocument(PdfDocumentCatalog documentCatalog, PdfExportFontManager fontCache)
        {
            this.strokingAlphaCache = new PdfStrokingAlphaCache();
            this.nonStrokingAlphaCache = new PdfNonStrokingAlphaCache();
            this.documentCatalog = documentCatalog;
            this.fontCache = fontCache;
            this.imageCache = new PdfXObjectResourceCache(this);
            this.shadingCache = new PdfShadingCache(documentCatalog);
            this.hatchPatternCache = new PdfHatchPatternCache(documentCatalog);
        }

        public PdfGraphicsDocument(PdfDocumentCatalog documentCatalog, int metafileMinimalRasterizationResolution = 0)
        {
            this.strokingAlphaCache = new PdfStrokingAlphaCache();
            this.nonStrokingAlphaCache = new PdfNonStrokingAlphaCache();
            this.documentCatalog = documentCatalog;
            this.disposeFontCache = true;
            this.fontCache = new PdfExportFontManager(this);
            this.imageCache = new PdfXObjectResourceCache(this);
            this.shadingCache = new PdfShadingCache(documentCatalog);
            this.hatchPatternCache = new PdfHatchPatternCache(documentCatalog);
            this.<MetafileMinimalRasterizationResolution>k__BackingField = metafileMinimalRasterizationResolution;
        }

        public bool CheckCollectionId(Guid id) => 
            (this.documentCatalog.Objects.Id == id) && (this.fontCache.ObjectsId == id);

        void IPdfDocumentContext.NotifyFontChanged(PdfFont font)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.disposeFontCache)
                {
                    this.fontCache.UpdateFonts();
                    this.fontCache.Dispose();
                }
                this.imageCache.Dispose();
            }
        }

        public PdfExportFontManager FontCache =>
            this.fontCache;

        public PdfXObjectResourceCache ImageCache =>
            this.imageCache;

        public PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;

        public PdfStrokingAlphaCache StrokingAlphaCache =>
            this.strokingAlphaCache;

        public PdfNonStrokingAlphaCache NonStrokingAlphaCache =>
            this.nonStrokingAlphaCache;

        public PdfShadingCache ShadingCache =>
            this.shadingCache;

        public PdfHatchPatternCache HatchPatternCache =>
            this.hatchPatternCache;

        public int MetafileMinimalRasterizationResolution { get; }

        PdfDocumentCatalog IPdfDocumentContext.DocumentCatalog =>
            this.documentCatalog;
    }
}

