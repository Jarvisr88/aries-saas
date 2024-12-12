namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetTextRiseCommand : PdfCommand
    {
        internal const string Name = "Ts";
        private readonly double textRise;

        internal PdfSetTextRiseCommand(PdfStack operands)
        {
            this.textRise = operands.PopDouble();
        }

        public PdfSetTextRiseCommand(double textRise)
        {
            this.textRise = textRise;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetTextRise(this.textRise);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.textRise);
            writer.WriteSpace();
            writer.WriteString("Ts");
        }

        public double TextRise =>
            this.textRise;
    }
}

