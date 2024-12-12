namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfURIAction : PdfAction
    {
        private string uri;

        public PdfURIAction(string uri, bool compressed) : base(compressed)
        {
            this.uri = uri;
        }

        public override void FillUp()
        {
            base.FillUp();
            if (this.uri != null)
            {
                base.Dictionary.Add("URI", new PdfLiteralString(this.uri));
            }
        }

        public override string Subtype =>
            "URI";

        public string URI
        {
            get => 
                this.uri;
            set => 
                this.uri = value;
        }
    }
}

