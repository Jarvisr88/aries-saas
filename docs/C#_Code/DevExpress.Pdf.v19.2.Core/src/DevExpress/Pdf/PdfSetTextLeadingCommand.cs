namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetTextLeadingCommand : PdfCommand
    {
        internal const string Name = "TL";
        private readonly double textLeading;

        internal PdfSetTextLeadingCommand(PdfStack operands)
        {
            this.textLeading = operands.PopDouble();
        }

        public PdfSetTextLeadingCommand(double textLeading)
        {
            this.textLeading = textLeading;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetTextLeading(this.textLeading);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.textLeading);
            writer.WriteSpace();
            writer.WriteString("TL");
        }

        public double TextLeading =>
            this.textLeading;
    }
}

