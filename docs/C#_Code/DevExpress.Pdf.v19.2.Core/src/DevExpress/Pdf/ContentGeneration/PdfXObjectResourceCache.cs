namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class PdfXObjectResourceCache : PdfResourceCache<PdfXObjectCachedResource>
    {
        private readonly PdfGraphicsDocument graphicsDocument;
        private readonly bool extractSMask;
        private bool convertImagesToJpeg;
        private long jpegQuality = 100;

        public PdfXObjectResourceCache(PdfGraphicsDocument graphicsDocument)
        {
            this.graphicsDocument = graphicsDocument;
            PdfDocumentCatalog documentCatalog = graphicsDocument.DocumentCatalog;
            this.extractSMask = (documentCatalog.CreationOptions == null) || (documentCatalog.CreationOptions.Compatibility != PdfCompatibility.PdfA1b);
        }

        private PdfXObjectCachedResource AddImage(PdfResourcesCacheKey key, Func<PdfImageToXObjectConverter> createConverter) => 
            base.CacheObject(key, () => this.CreateCachedResource(createConverter()));

        private PdfXObjectCachedResource AddImage(PdfResourcesCacheKey key, Image image, Func<PdfImageToXObjectConverter> createConverter) => 
            (!image.FrameDimensionsList.Contains(FrameDimension.Page.Guid) || (image.GetFrameCount(FrameDimension.Page) <= 1)) ? this.AddImage(key, createConverter) : this.CreateCachedResource(createConverter());

        private PdfXObjectCachedResource AddMetafile(object source, Metafile metafile) => 
            base.CacheObject(new PdfResourcesCacheKey(source), () => PdfXObjectMetafileCachedResource.Create(metafile, this.graphicsDocument));

        public PdfXObjectCachedResource AddXObject(Image image)
        {
            Metafile metafile = image as Metafile;
            return ((metafile == null) ? this.AddImage(new PdfResourcesCacheKey(image), image, () => PdfImageToXObjectConverter.Create((Bitmap) image, this.convertImagesToJpeg, this.jpegQuality, this.extractSMask)) : this.AddMetafile(image, metafile));
        }

        public PdfXObjectCachedResource AddXObject(Stream imageStream) => 
            this.AddXObject(imageStream, imageStream);

        public PdfXObjectCachedResource AddXObject(byte[] imageData)
        {
            using (MemoryStream stream = new MemoryStream(imageData))
            {
                return this.AddXObject(imageData, stream);
            }
        }

        public PdfXObjectCachedResource AddXObject(EmfMetafile emfMetafile) => 
            base.CacheObject(new PdfResourcesCacheKey(emfMetafile), () => PdfXObjectMetafileCachedResource.Create(emfMetafile, this.graphicsDocument));

        private PdfXObjectCachedResource AddXObject(object cacheKey, Stream imageStream)
        {
            PdfRecognizedImageInfo imageInfo = PdfRecognizedImageInfo.DetectImage(imageStream);
            imageStream.Position = 0L;
            return ((imageInfo.Type != PdfRecognizedImageFormat.Metafile) ? this.AddImage(new PdfResourcesCacheKey(cacheKey), () => PdfImageToXObjectConverter.Create(imageInfo, imageStream, this.convertImagesToJpeg, this.jpegQuality, this.extractSMask)) : base.CacheObject(new PdfResourcesCacheKey(cacheKey), () => PdfXObjectMetafileCachedResource.Create(imageStream, this.graphicsDocument)));
        }

        public PdfXObjectCachedResource AddXObject(Image image, Color backgroundColor, int resolution)
        {
            Bitmap bmp = PdfMetafileToBitmapConverter.ConvertToBitmap(image, backgroundColor, resolution);
            PdfXObjectCachedResource resource = this.AddImage(new PdfResourcesCacheCompositeKey(image, backgroundColor, resolution), image, () => PdfImageToXObjectConverter.Create(bmp, this.convertImagesToJpeg, this.jpegQuality, this.extractSMask));
            if (!ReferenceEquals(bmp, image))
            {
                bmp.Dispose();
            }
            return resource;
        }

        private PdfXObjectCachedResource CreateCachedResource(PdfImageToXObjectConverter converter) => 
            new PdfXObjectImageCachedResource(converter.Width, converter.Height, this.graphicsDocument.DocumentCatalog.Objects.AddResolvedObject(converter.GetXObject()));

        public bool ConvertImagesToJpeg
        {
            get => 
                this.convertImagesToJpeg;
            set => 
                this.convertImagesToJpeg = value;
        }

        public long JpegQuality
        {
            get => 
                this.jpegQuality;
            set => 
                this.jpegQuality = value;
        }

        public bool ExtractSMask =>
            this.extractSMask;
    }
}

