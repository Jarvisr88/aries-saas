namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetWordSpacingCommand : PdfCommand
    {
        internal const string Name = "Tw";
        private readonly double wordSpacing;

        internal PdfSetWordSpacingCommand(PdfStack operands)
        {
            this.wordSpacing = operands.PopDouble();
        }

        public PdfSetWordSpacingCommand(double wordSpacing)
        {
            this.wordSpacing = wordSpacing;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetWordSpacing(this.wordSpacing);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.wordSpacing);
            writer.WriteSpace();
            writer.WriteString("Tw");
        }

        public double WordSpacing =>
            this.wordSpacing;
    }
}

