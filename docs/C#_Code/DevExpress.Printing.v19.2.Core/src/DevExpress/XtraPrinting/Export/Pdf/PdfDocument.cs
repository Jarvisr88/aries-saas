namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography;

    public class PdfDocument : IDisposable
    {
        private PdfHeader header;
        private PdfXRef xRef = new PdfXRef();
        private PdfTrailer trailer;
        private PdfCatalog catalog;
        private PdfFonts fonts = new PdfFonts();
        private PdfAnnotations annotations = new PdfAnnotations();
        private PdfTransparencyGSCollection transparencyGS;
        private PdfShadingCollection shading;
        private PdfPatternCollection patterns;
        private PdfInfo info;
        private IList destinationInfos = new ArrayList();
        private int imageCount;
        private bool compressed;
        private bool pdfACompatible;
        private bool convertImagesToJpeg;
        private PdfJpegImageQuality jpegImageQuality = PdfJpegImageQuality.Highest;
        private PdfEncryption encryption;
        private PdfSignature signature;
        private byte[] id;

        public PdfDocument(bool compressed, bool showPrintDialog)
        {
            this.compressed = compressed;
            this.trailer = new PdfTrailer(this.xRef);
            this.catalog = PdfCatalog.CreateInstance(compressed, showPrintDialog);
            this.info = new PdfInfo(compressed);
            this.encryption = new PdfEncryption(this);
            this.signature = new PdfSignature();
            this.header = new PdfHeader(this);
            this.transparencyGS = new PdfTransparencyGSCollection(compressed);
            this.shading = new PdfShadingCollection(compressed);
            this.patterns = new PdfPatternCollection(compressed);
        }

        public int AddDestinationInfo(DestinationInfo info) => 
            ((info == null) || this.destinationInfos.Contains(info)) ? -1 : this.destinationInfos.Add(info);

        protected internal void AfterWrite()
        {
            this.destinationInfos.Clear();
        }

        protected internal void BeforeWrite()
        {
            this.catalog.PrepareOutlines();
            this.CreateGotoActions();
            this.PrepareSignature();
        }

        public void Calculate()
        {
            if (this.encryption.IsEncryptionOn || this.PdfACompatible)
            {
                this.id = this.CreateDocumentId();
            }
            if (this.encryption.IsEncryptionOn)
            {
                this.encryption.Calculate();
            }
        }

        private byte[] CreateDocumentId()
        {
            long totalMemory = GC.GetTotalMemory(false);
            string s = (DateTime.Now.Ticks + Environment.TickCount) + "+" + totalMemory;
            return new MD5CryptoServiceProvider().ComputeHash(DXEncoding.ASCII.GetBytes(s));
        }

        private void CreateGotoActions()
        {
            foreach (DestinationInfo info in this.DestinationInfos)
            {
                PdfPage page = this.GetPage(info.DestPageIndex);
                if ((page != null) && (info.LinkPage != null))
                {
                    PdfAction action = new PdfGoToAction(new PdfDestination(page, info.DestTop), this.compressed);
                    PdfAnnotation annotation = this.CreateLinkAnnotation(action, DevExpress.XtraPrinting.Export.Pdf.Utils.ToPdfRectangle(info.LinkArea));
                    info.LinkPage.AddAnnotation(annotation);
                }
            }
        }

        private string CreateImageName()
        {
            int imageCount = this.imageCount;
            this.imageCount = imageCount + 1;
            return $"Img{imageCount}";
        }

        internal PdfLinkAnnotation CreateLinkAnnotation(PdfAction action, PdfRectangle rect)
        {
            PdfLinkAnnotation annotation = new PdfLinkAnnotation(action, rect, this.compressed) {
                PdfACompatible = this.PdfACompatible
            };
            this.RegisterAnnotation(annotation);
            return annotation;
        }

        protected internal PdfDestination CreatePdfDestination(DestinationInfo info)
        {
            PdfPage page = this.GetPage(info.DestPageIndex);
            return ((page != null) ? new PdfDestination(page, info.DestTop) : null);
        }

        private PdfImageBase CreatePdfImage(IPdfDocumentOwner documentInfo, System.Drawing.Image image) => 
            PdfImageBase.CreateInstance(documentInfo, image, this, this.CreateImageName(), this.compressed, this.convertImagesToJpeg, this.jpegImageQuality);

        private PdfImageBase CreatePdfImage(IPdfDocumentOwner documentInfo, System.Drawing.Image image, Color actualBackColor)
        {
            if (DXColor.IsEmpty(actualBackColor))
            {
                return this.CreatePdfImage(documentInfo, image);
            }
            using (System.Drawing.Image image2 = BitmapCreator.CreateBitmapWithResolutionLimit(image, actualBackColor))
            {
                return this.CreatePdfImage(documentInfo, image2);
            }
        }

        public PdfImageBase CreatePdfImage(IPdfDocumentOwner documentInfo, IXObjectsOwner xObjectsOwner, System.Drawing.Image image, Color actualBackColor)
        {
            PdfImageBase xObject = this.CreatePdfImage(documentInfo, image, actualBackColor);
            xObjectsOwner.AddNewXObject(xObject);
            if (xObject.MaskImage != null)
            {
                xObjectsOwner.AddNewXObject(xObject.MaskImage);
            }
            return xObject;
        }

        public void Dispose()
        {
            this.fonts.DisposeAndClear();
        }

        private void FillUp()
        {
            this.info.FillUp();
            this.catalog.FillUp();
            this.fonts.FillUp();
            this.annotations.FillUp();
            this.encryption.FillUp();
            this.transparencyGS.FillUp();
            this.shading.FillUp();
            this.patterns.FillUp();
        }

        private void FillXRef()
        {
            this.catalog.Register(this.xRef);
            this.fonts.Register(this.xRef);
            this.annotations.Register(this.xRef);
            this.info.Register(this.xRef);
            if (this.encryption.IsEncryptionOn)
            {
                this.encryption.Register(this.xRef);
            }
            this.transparencyGS.Register(this.xRef);
            this.shading.Register(this.xRef);
            this.patterns.Register(this.xRef);
        }

        private PdfFont FindFont(Font font) => 
            this.fonts.FindFont(font);

        public PdfPage GetPage(int index) => 
            (index >= 0) ? this.catalog.Pages.GetPage(ref index) : null;

        private void PrepareSignature()
        {
            if (this.signature.Active)
            {
                int index = 0;
                PdfPage page = this.catalog.Pages.GetPage(ref index);
                if (page != null)
                {
                    PdfSignatureWidgetAnnotation annotation = new PdfSignatureWidgetAnnotation(page, this.signature, this.Compressed);
                    page.AddAnnotation(annotation);
                    this.annotations.AddUnique(annotation);
                    this.catalog.AddFormField(annotation);
                }
            }
        }

        private void PreWrite()
        {
            this.FillXRef();
            this.FillUp();
            this.trailer.Attributes.Add("Size", new PdfNumber(this.xRef.Count));
            this.trailer.Attributes.Add("Info", this.info.Dictionary);
            this.trailer.Attributes.Add("Root", this.catalog.Dictionary);
            if (this.encryption.IsEncryptionOn)
            {
                this.trailer.Attributes.Add("Encrypt", this.encryption.Dictionary);
            }
            if (this.encryption.IsEncryptionOn || this.PdfACompatible)
            {
                PdfArray array = new PdfArray {
                    new PdfHexadecimalString(this.id),
                    new PdfHexadecimalString(this.id)
                };
                this.trailer.Attributes.Add("ID", array);
            }
        }

        public PdfAnnotation RegisterAnnotation(PdfAnnotation annotation)
        {
            this.annotations.AddUnique(annotation);
            return annotation;
        }

        public PdfFont RegisterFont(Font font) => 
            this.fonts.RegisterFont(font, this.compressed);

        public PdfFont RegisterFontSmart(Font original, ref string actualString, ref Font newFont)
        {
            Font font = original;
            if (original.Unit != GraphicsUnit.Point)
            {
                font = newFont = new Font(original.FontFamily, FontSizeHelper.GetSizeInPoints(original), original.Style);
            }
            PdfFont font2 = this.FindFont(font);
            if (font2 == null)
            {
                Font font3 = font;
                try
                {
                    font2 = this.RegisterFont(font3);
                }
                catch (Exception exception)
                {
                    Tracer.TraceError("DXperience.Reporting", exception);
                    if (PrintingSettings.PassPdfDrawingExceptions)
                    {
                        throw;
                    }
                    actualString = PreviewLocalizer.GetString(PreviewStringId.Msg_NotSupportedFont);
                    using (Font font5 = new Font("Tahoma", 8f))
                    {
                        font2 = this.RegisterFont(font5);
                    }
                }
            }
            return font2;
        }

        public void Write(StreamWriter writer, ProgressReflector progressReflector)
        {
            this.PreWrite();
            this.catalog.Write(writer);
            progressReflector.RangeValue++;
            this.fonts.Write(writer);
            progressReflector.RangeValue++;
            this.annotations.Write(writer);
            this.transparencyGS.Write(writer);
            this.shading.Write(writer);
            this.patterns.Write(writer);
            this.info.Write(writer);
            if (this.encryption.IsEncryptionOn)
            {
                this.encryption.Write(writer);
            }
            if (this.signature.Active)
            {
                this.signature.Write(writer);
            }
            this.xRef.Write(writer);
            this.trailer.Write(writer);
            if (this.signature.Active)
            {
                this.signature.Finish(writer);
            }
        }

        public void WriteHeader(StreamWriter writer)
        {
            this.header.Write(writer);
        }

        public PdfHeader Header =>
            this.header;

        public PdfCatalog Catalog =>
            this.catalog;

        public PdfXRef XRef =>
            this.xRef;

        public PdfTrailer Trailer =>
            this.trailer;

        public int PageCount =>
            this.catalog.Pages.LeafCount;

        public IList DestinationInfos =>
            this.destinationInfos;

        public PdfFonts Fonts =>
            this.fonts;

        public PdfInfo Info =>
            this.info;

        public PdfEncryption Encryption =>
            this.encryption;

        public PdfSignature Signature =>
            this.signature;

        public bool Compressed =>
            this.compressed;

        public bool ConvertImagesToJpeg
        {
            get => 
                this.convertImagesToJpeg;
            set => 
                this.convertImagesToJpeg = value;
        }

        public PdfJpegImageQuality JpegImageQuality
        {
            get => 
                this.jpegImageQuality;
            set => 
                this.jpegImageQuality = value;
        }

        public byte[] ID =>
            this.id;

        internal PdfTransparencyGSCollection TransparencyGS =>
            this.transparencyGS;

        internal PdfShadingCollection Shading =>
            this.shading;

        internal PdfPatternCollection Patterns =>
            this.patterns;

        public bool PdfACompatible
        {
            get => 
                this.pdfACompatible;
            set => 
                this.pdfACompatible = this.Catalog.PdfACompatible = value;
        }
    }
}

