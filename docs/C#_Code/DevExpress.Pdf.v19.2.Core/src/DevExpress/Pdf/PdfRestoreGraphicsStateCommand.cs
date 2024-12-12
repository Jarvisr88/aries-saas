namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRestoreGraphicsStateCommand : PdfCommand
    {
        internal const string Name = "Q";
        private static PdfRestoreGraphicsStateCommand instance = new PdfRestoreGraphicsStateCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.RestoreGraphicsState();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("Q");
        }

        internal static PdfRestoreGraphicsStateCommand Instance =>
            instance;
    }
}

