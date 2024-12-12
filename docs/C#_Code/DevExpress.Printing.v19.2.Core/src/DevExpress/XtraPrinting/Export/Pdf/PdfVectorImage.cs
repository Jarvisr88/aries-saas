namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Printing.Core.PdfExport.Metafile;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;

    public class PdfVectorImage : PdfImageBase, IPdfContentsOwner, IXObjectsOwner, IPdfDocumentOwner
    {
        private PdfFonts fonts;
        private MetafileInfo metafileInfo;
        private PdfDocument document;
        private DevExpress.XtraPrinting.Native.Measurer measurer;
        private PdfXObjects xObjects;
        private IPdfDocumentOwner documentInfo;
        private SizeF imageSizeInPoints;
        private PdfObjectCollection<PdfTransparencyGS> transparencyGSCollection;
        private PdfObjectCollection<PdfShading> shadingCollection;
        private PdfObjectCollection<PdfPattern> patternCollection;

        internal PdfVectorImage(IPdfDocumentOwner documentInfo, MetafileInfo metafileInfo, PdfDocument document, string name, bool compressed) : base(name, compressed)
        {
            this.fonts = new PdfFonts();
            this.xObjects = new PdfXObjects();
            this.transparencyGSCollection = new PdfObjectCollection<PdfTransparencyGS>();
            this.shadingCollection = new PdfObjectCollection<PdfShading>();
            this.patternCollection = new PdfObjectCollection<PdfPattern>();
            this.document = document;
            this.documentInfo = documentInfo;
            this.measurer = new GdiPlusMeasurer();
            this.metafileInfo = metafileInfo;
            PdfDrawContext context = PdfDrawContext.Create(base.Stream, this, new PdfHashtable());
            GraphicsUnit point = GraphicsUnit.Point;
            RectangleF bounds = metafileInfo.GetBounds(ref point);
            this.imageSizeInPoints = new SizeF(GraphicsUnitConverter.Convert(bounds.Size.Width, metafileInfo.HorizontalResolution, (float) 72f), GraphicsUnitConverter.Convert(bounds.Size.Height, metafileInfo.VerticalResolution, (float) 72f));
            if (metafileInfo.Metafile != null)
            {
                PdfGraphicsImpl graphics = new PdfGraphicsImpl(this, context, this.imageSizeInPoints, document, this) {
                    PageUnit = point
                };
                new MetaImage(this, metafileInfo.Metafile, context, graphics, bounds.Location).Write();
                graphics.ResetTransform();
            }
        }

        public PdfVectorImage(IPdfDocumentOwner documentInfo, System.Drawing.Image image, PdfDocument document, string name, bool compressed) : this(documentInfo, new MetafileInfo((Metafile) image), document, name, compressed)
        {
        }

        public void AddNewXObject(PdfXObject xObject)
        {
            this.xObjects.Add(xObject);
        }

        public PdfImageBase CreateImage(System.Drawing.Image image) => 
            this.document.CreatePdfImage(this.documentInfo, this, image, Color.Empty);

        void IXObjectsOwner.AddExistingXObject(PdfXObject xObject)
        {
            this.xObjects.AddUnique(xObject);
        }

        void IXObjectsOwner.AddPattern(PdfPattern pattern)
        {
            this.patternCollection.AddUnique(pattern);
        }

        void IXObjectsOwner.AddShading(PdfShading shading)
        {
            this.shadingCollection.AddUnique(shading);
        }

        void IXObjectsOwner.AddTransparencyGS(PdfTransparencyGS transparencyGS)
        {
            this.transparencyGSCollection.AddUnique(transparencyGS);
        }

        private void FillFonts(PdfDictionary resourceDictionary)
        {
            if (this.fonts.Count > 0)
            {
                PdfDictionary dictionary = new PdfDictionary();
                int num = 0;
                while (true)
                {
                    if (num >= this.fonts.Count)
                    {
                        resourceDictionary.Add("Font", dictionary);
                        break;
                    }
                    dictionary.Add(this.fonts[num].Name, this.fonts[num].Dictionary);
                    num++;
                }
            }
        }

        public override void FillUp()
        {
            base.FillUp();
            this.xObjects.FillUp();
            base.Attributes.Add("Subtype", "Form");
            PdfDictionary resourceDictionary = new PdfDictionary();
            PdfDictionary dictionary2 = this.xObjects.CreateDictionary();
            if (dictionary2 != null)
            {
                resourceDictionary.Add("XObject", dictionary2);
            }
            this.FillFonts(resourceDictionary);
            PdfArray array = new PdfArray { 
                "PDF",
                "Text",
                "ImageB",
                "ImageC",
                "ImageI"
            };
            resourceDictionary.Add("ProcSet", array);
            resourceDictionary.AddIfNotNull("ExtGState", this.transparencyGSCollection.CreateDictionary());
            resourceDictionary.AddIfNotNull("Shading", this.shadingCollection.CreateDictionary());
            resourceDictionary.AddIfNotNull("Pattern", this.patternCollection.CreateDictionary());
            base.Attributes.Add("Resources", resourceDictionary);
            SizeF boundingBox = this.GetBoundingBox();
            PdfArray array2 = new PdfArray {
                0,
                0,
                new PdfDouble((double) boundingBox.Width),
                new PdfDouble((double) boundingBox.Height)
            };
            base.Attributes.Add("BBox", array2);
            base.Attributes.Add("FormType", 1);
        }

        private SizeF GetBoundingBox() => 
            this.imageSizeInPoints;

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            this.xObjects.Register(xRef);
            this.fonts.Register(xRef);
            this.transparencyGSCollection.Register(xRef);
            this.shadingCollection.Register(xRef);
            this.patternCollection.Register(xRef);
        }

        public override Matrix Transform(RectangleF correctedBounds) => 
            new Matrix(correctedBounds.Width / this.imageSizeInPoints.Width, 0f, 0f, correctedBounds.Height / this.imageSizeInPoints.Height, correctedBounds.X, correctedBounds.Y);

        protected override void WriteContent(StreamWriter writer)
        {
            this.xObjects.Write(writer);
            base.WriteContent(writer);
        }

        PdfFonts IPdfContentsOwner.Fonts =>
            this.fonts;

        public PdfNeverEmbeddedFonts NeverEmbeddedFonts =>
            this.documentInfo.NeverEmbeddedFonts;

        public PdfImageCache ImageCache =>
            this.documentInfo.ImageCache;

        public DevExpress.XtraPrinting.Native.Measurer Measurer =>
            this.documentInfo.MetafileMeasurer;

        public DevExpress.XtraPrinting.Native.Measurer MetafileMeasurer =>
            this.documentInfo.MetafileMeasurer;

        public bool ScaleStrings =>
            this.documentInfo.ScaleStrings;
    }
}

