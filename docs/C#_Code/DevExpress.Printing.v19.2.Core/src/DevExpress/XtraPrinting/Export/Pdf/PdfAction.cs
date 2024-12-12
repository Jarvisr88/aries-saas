namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public abstract class PdfAction : PdfDocumentDictionaryObject
    {
        protected PdfAction(bool compressed) : base(compressed)
        {
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Type", "Action");
            base.Dictionary.Add("S", this.Subtype);
        }

        public abstract string Subtype { get; }
    }
}

