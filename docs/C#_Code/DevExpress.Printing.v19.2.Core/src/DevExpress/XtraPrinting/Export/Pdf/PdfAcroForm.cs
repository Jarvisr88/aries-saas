namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfAcroForm : PdfDocumentDictionaryObject
    {
        private PdfArray fields;

        public PdfAcroForm(bool compressed) : base(compressed)
        {
            this.fields = new PdfArray();
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Fields", this.fields);
            base.Dictionary.Add("SigFlags", 3);
        }

        public PdfArray Fields =>
            this.fields;

        public bool Active =>
            this.Fields.Count > 0;
    }
}

