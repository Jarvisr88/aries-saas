namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfStartTextLineWithOffsetsAndLeadingCommand : PdfStartTextLineWithOffsetsCommand
    {
        internal const string Name = "TD";

        internal PdfStartTextLineWithOffsetsAndLeadingCommand(PdfStack operands) : base(operands)
        {
        }

        public PdfStartTextLineWithOffsetsAndLeadingCommand(double xOffset, double yOffset) : base(xOffset, yOffset)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetTextLeading(-base.YOffset);
            interpreter.SetTextMatrix(base.XOffset, base.YOffset);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(base.XOffset);
            writer.WriteSpace();
            writer.WriteDouble(base.YOffset);
            writer.WriteSpace();
            writer.WriteString("TD");
        }
    }
}

