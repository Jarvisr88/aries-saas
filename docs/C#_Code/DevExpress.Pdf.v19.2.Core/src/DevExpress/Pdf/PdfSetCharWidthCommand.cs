namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetCharWidthCommand : PdfCommand
    {
        internal const string Name = "d0";
        private readonly double charWidth;

        internal PdfSetCharWidthCommand(PdfStack operands)
        {
            operands.PopDouble();
            this.charWidth = operands.PopDouble();
        }

        public PdfSetCharWidthCommand(double charWidth)
        {
            this.charWidth = charWidth;
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.charWidth);
            writer.WriteSpace();
            writer.WriteInt(0);
            writer.WriteSpace();
            writer.WriteString("d0");
        }

        public double CharWidth =>
            this.charWidth;
    }
}

