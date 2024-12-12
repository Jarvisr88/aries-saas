namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfStartTextLineWithOffsetsCommand : PdfCommand
    {
        internal const string Name = "Td";
        private readonly double xOffset;
        private readonly double yOffset;

        internal PdfStartTextLineWithOffsetsCommand(PdfStack operands)
        {
            this.yOffset = operands.PopDouble();
            this.xOffset = operands.PopDouble();
        }

        public PdfStartTextLineWithOffsetsCommand(double xOffset, double yOffset)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetTextMatrix(this.xOffset, this.yOffset);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.xOffset);
            writer.WriteSpace();
            writer.WriteDouble(this.yOffset);
            writer.WriteSpace();
            writer.WriteString("Td");
        }

        public double XOffset =>
            this.xOffset;

        public double YOffset =>
            this.yOffset;
    }
}

