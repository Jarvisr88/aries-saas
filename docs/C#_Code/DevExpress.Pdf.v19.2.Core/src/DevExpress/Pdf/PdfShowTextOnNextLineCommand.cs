namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfShowTextOnNextLineCommand : PdfShowTextCommand
    {
        internal const string Name = "'";

        public PdfShowTextOnNextLineCommand(byte[] text) : base(text)
        {
        }

        internal PdfShowTextOnNextLineCommand(PdfStack operands) : base(operands)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.MoveToNextLine();
            interpreter.DrawString(base.Text, null);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteHexadecimalString(base.Text, -1);
            writer.WriteSpace();
            writer.WriteString("'");
        }
    }
}

