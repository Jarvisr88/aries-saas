namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfGoToAction : PdfAction
    {
        private PdfDestination dest;

        public PdfGoToAction(PdfDestination dest, bool compressed) : base(compressed)
        {
            this.dest = dest;
        }

        public override void FillUp()
        {
            base.FillUp();
            if (this.dest != null)
            {
                base.Dictionary.Add("D", this.dest);
            }
        }

        public override string Subtype =>
            "GoTo";
    }
}

