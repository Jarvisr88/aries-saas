namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PdfCatalog : PdfDocumentDictionaryObject
    {
        private PdfPages pages;
        private PdfOutlines outlines;
        private PdfAcroForm acroForm;
        private PdfMetadata metadata;
        private PdfICCProfile iccProfile;
        private PdfEmbeddedFiles embeddedFiles;
        private bool pdfACompatible;

        protected PdfCatalog(bool compressed) : base(compressed)
        {
            this.pages = new PdfPages(null, base.Compressed);
            this.pages.MediaBox = new PdfRectangle(0f, 0f, 596f, 842f);
            this.outlines = new PdfOutlines(base.Compressed);
            this.acroForm = new PdfAcroForm(base.Compressed);
            this.embeddedFiles = new PdfEmbeddedFiles(base.Compressed);
            this.metadata = new PdfMetadata(base.Compressed);
            this.metadata.HasEmbeddedFiles = this.embeddedFiles.Active;
        }

        public void AddFormField(PdfSignatureWidgetAnnotation annotation)
        {
            this.acroForm.Fields.Add(annotation.InnerObject);
        }

        public static PdfCatalog CreateInstance(bool compressed, bool showPrintDialog) => 
            showPrintDialog ? new PdfPrintCatalog(compressed) : new PdfCatalog(compressed);

        private void CreateOutlineDestinations(PdfOutlineEntryCollection collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                PdfOutlineEntry entry = collection[i];
                int destPageIndex = entry.DestPageIndex;
                PdfPage page = this.Pages.GetPage(ref destPageIndex);
                if (page != null)
                {
                    entry.SetDestination(new PdfDestination(page, entry.DestTop));
                    this.CreateOutlineDestinations(entry.Entries);
                }
            }
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Type", new PdfName("Catalog"));
            base.Dictionary.Add("Pages", this.pages.Dictionary);
            base.FillUp();
            if (this.outlines.Active)
            {
                this.outlines.FillUp();
                base.Dictionary.Add("Outlines", this.outlines.InnerObject);
                base.Dictionary.Add("PageMode", "UseOutlines");
            }
            if (this.acroForm.Active)
            {
                base.Dictionary.Add("AcroForm", this.acroForm.InnerObject);
            }
            if (this.metadata.Active)
            {
                this.metadata.FillUp();
                base.Dictionary.Add("Metadata", this.metadata.InnerObject);
            }
            if (this.pdfACompatible)
            {
                this.ICCProfile.FillUp();
                PdfDictionary dictionary = new PdfDictionary();
                dictionary.Add("Type", "OutputIntent");
                dictionary.Add("S", "GTS_PDFA1");
                dictionary.Add("OutputConditionIdentifier", new PdfLiteralString("sRGB IEC61966-2.1"));
                dictionary.Add("DestOutputProfile", this.ICCProfile.InnerObject);
                PdfArray array = new PdfArray {
                    dictionary
                };
                base.Dictionary.Add("OutputIntents", array);
            }
            if (this.embeddedFiles.Active)
            {
                this.embeddedFiles.FillUp();
                base.Dictionary.Add("AF", this.embeddedFiles.AFArray);
                base.Dictionary.Add("Names", this.embeddedFiles.NamesDictionary);
            }
            this.acroForm.FillUp();
            this.pages.FillUp();
        }

        public void PrepareOutlines()
        {
            this.CreateOutlineDestinations(this.outlines.Entries);
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            this.pages.Register(xRef);
            if (this.outlines.Active)
            {
                this.outlines.Register(xRef);
            }
            if (this.acroForm.Active)
            {
                this.acroForm.Register(xRef);
            }
            if (this.metadata.Active)
            {
                this.metadata.Register(xRef);
            }
            if (this.pdfACompatible)
            {
                this.ICCProfile.Register(xRef);
            }
            if (this.embeddedFiles.Active)
            {
                this.embeddedFiles.Register(xRef);
            }
        }

        protected override void WriteContent(StreamWriter writer)
        {
            base.WriteContent(writer);
            if (this.outlines.Active)
            {
                this.outlines.Write(writer);
            }
            if (this.acroForm.Active)
            {
                this.acroForm.Write(writer);
            }
            if (this.metadata.Active)
            {
                this.metadata.Write(writer);
            }
            if (this.pdfACompatible)
            {
                this.ICCProfile.Write(writer);
            }
            if (this.embeddedFiles.Active)
            {
                this.embeddedFiles.Write(writer);
            }
            this.pages.Write(writer);
        }

        public PdfPages Pages =>
            this.pages;

        public PdfOutlines Outlines =>
            this.outlines;

        public PdfMetadata Metadata =>
            this.metadata;

        public bool PdfACompatible
        {
            get => 
                this.pdfACompatible;
            set => 
                this.pdfACompatible = this.Metadata.PdfACompatible = value;
        }

        public ICollection<PdfAttachment> Attachments
        {
            set
            {
                this.embeddedFiles.SetAttachments(value);
                this.metadata.HasEmbeddedFiles = this.embeddedFiles.Active;
            }
        }

        private PdfICCProfile ICCProfile
        {
            get
            {
                this.iccProfile ??= new PdfICCProfile(base.Compressed);
                return this.iccProfile;
            }
        }
    }
}

