namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrintingLinks;
    using System;

    public class LegacyPrintableComponentLink : DevExpress.Xpf.Printing.LinkBase
    {
        private readonly IPrintable printableComponent;
        private readonly PrintableComponentLinkBase legacyLink;

        public LegacyPrintableComponentLink(IPrintable printableComponent) : this(printableComponent, string.Empty)
        {
        }

        public LegacyPrintableComponentLink(IPrintable printableComponent, string documentName) : base(documentName)
        {
            Guard.ArgumentNotNull(printableComponent, "printableComponent");
            this.printableComponent = printableComponent;
            PrintableComponentLinkBase base1 = new PrintableComponentLinkBase(base.PrintingSystem);
            base1.Component = printableComponent;
            this.legacyLink = base1;
            base.PrintingSystem.DocumentFactory = new PSLinkDocumentFactory();
        }

        protected override void CreateDocumentCore(bool buildPagesInBackground, bool applyPageSettings)
        {
            if (applyPageSettings)
            {
                this.legacyLink.PaperKind = base.PaperKind;
                this.legacyLink.CustomPaperSize = base.CustomPaperSize;
                this.legacyLink.Margins = base.Margins;
                this.legacyLink.MinMargins = base.MinMargins;
                this.legacyLink.Landscape = base.Landscape;
            }
            if (!string.IsNullOrEmpty(base.DocumentName))
            {
                base.PrintingSystem.Document.Name = base.DocumentName;
            }
            this.legacyLink.VerticalContentSplitting = base.VerticalContentSplitting;
            this.legacyLink.CreateDocument(buildPagesInBackground);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.legacyLink.Dispose();
            }
            base.Dispose(disposing);
        }

        public IPrintable PrintableComponent =>
            this.printableComponent;

        public bool RightToLeftLayout
        {
            get => 
                this.legacyLink.RightToLeftLayout;
            set => 
                this.legacyLink.RightToLeftLayout = value;
        }
    }
}

