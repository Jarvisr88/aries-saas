namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfOutlines : PdfOutlineItem
    {
        public PdfOutlines(bool compressed) : base(null, compressed)
        {
        }

        public bool Active =>
            base.Entries.Count > 0;
    }
}

