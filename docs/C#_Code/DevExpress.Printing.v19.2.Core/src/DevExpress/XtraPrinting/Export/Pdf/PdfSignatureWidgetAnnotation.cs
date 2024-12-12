namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfSignatureWidgetAnnotation : PdfAnnotation
    {
        private PdfPage page;
        private PdfSignature sig;

        public PdfSignatureWidgetAnnotation(PdfPage page, PdfSignature sig, bool compressed) : base(compressed)
        {
            this.page = page;
            this.sig = sig;
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Dictionary.Add("FT", "Sig");
            base.Dictionary.Add("T", new PdfLiteralString("Signature1"));
            base.Dictionary.Add("F", 0x84);
            base.Dictionary.Add("P", this.page.InnerObject);
            if (this.sig != null)
            {
                base.Dictionary.Add("V", this.sig.InnerObject);
                this.sig.FillUp();
            }
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            if (this.sig != null)
            {
                this.sig.Register(xRef);
            }
        }

        public override string Subtype =>
            "Widget";
    }
}

