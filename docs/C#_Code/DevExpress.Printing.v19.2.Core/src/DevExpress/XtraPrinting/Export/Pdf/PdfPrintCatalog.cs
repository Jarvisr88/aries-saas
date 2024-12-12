namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfPrintCatalog : PdfCatalog
    {
        private PdfJavaScriptOwner javaScriptOwner;

        public PdfPrintCatalog(bool compressed) : base(compressed)
        {
            this.javaScriptOwner = new PdfJavaScriptOwner(compressed);
        }

        public override void FillUp()
        {
            base.FillUp();
            this.javaScriptOwner.FillUp();
            PdfDictionary dictionary = new PdfDictionary();
            dictionary.Add("JavaScript", this.javaScriptOwner.Dictionary);
            base.Dictionary.Add("Names", dictionary);
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            this.javaScriptOwner.Register(xRef);
        }

        protected override void WriteContent(StreamWriter writer)
        {
            base.WriteContent(writer);
            this.javaScriptOwner.Write(writer);
        }

        private class PdfJavaScript : PdfDocumentDictionaryObject
        {
            public PdfJavaScript(bool compressed) : base(compressed)
            {
            }

            public override void FillUp()
            {
                base.Dictionary.Add("S", new PdfName("JavaScript"));
                base.Dictionary.Add("JS", new PdfLiteralString("this.print({bUI: true,bSilent: false,bShrinkToFit: true});this.closeDoc();"));
            }
        }

        private class PdfJavaScriptOwner : PdfDocumentDictionaryObject
        {
            private PdfPrintCatalog.PdfJavaScript javaScript;

            public PdfJavaScriptOwner(bool compressed) : base(compressed)
            {
                this.javaScript = new PdfPrintCatalog.PdfJavaScript(compressed);
            }

            public override void FillUp()
            {
                this.javaScript.FillUp();
                PdfArray array = new PdfArray {
                    new PdfLiteralString("0"),
                    this.javaScript.InnerObject
                };
                base.Dictionary.Add("Names", array);
            }

            protected override void RegisterContent(PdfXRef xRef)
            {
                base.RegisterContent(xRef);
                this.javaScript.Register(xRef);
            }

            protected override void WriteContent(StreamWriter writer)
            {
                base.WriteContent(writer);
                this.javaScript.Write(writer);
            }
        }
    }
}

