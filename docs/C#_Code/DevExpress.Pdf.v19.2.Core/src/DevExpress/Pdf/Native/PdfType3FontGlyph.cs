namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfType3FontGlyph
    {
        private readonly PdfCommandList commands;
        private bool? isSafeForCaching;

        public PdfType3FontGlyph(PdfCommandList commands)
        {
            this.commands = commands;
        }

        public PdfCommandList Commands =>
            this.commands;

        public bool IsSafeForCaching
        {
            get
            {
                if (this.isSafeForCaching == null)
                {
                    this.isSafeForCaching = false;
                    foreach (PdfCommand command in this.commands)
                    {
                        if (command is PdfPaintImageCommand)
                        {
                            this.isSafeForCaching = true;
                            break;
                        }
                    }
                }
                return this.isSafeForCaching.Value;
            }
        }
    }
}

