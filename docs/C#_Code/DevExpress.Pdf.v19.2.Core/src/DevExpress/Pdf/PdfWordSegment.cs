namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfWordSegment
    {
        private readonly PdfWordPart part;

        internal PdfWordSegment(PdfWordPart part)
        {
            this.part = part;
        }

        public PdfOrientedRectangle Rectangle =>
            this.part.Rectangle;

        public IList<PdfCharacter> Characters =>
            this.part.Characters;

        public string Text =>
            this.part.Text;
    }
}

