namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfEndTextCommand : PdfCommand
    {
        internal const string Name = "ET";
        private static PdfEndTextCommand instance = new PdfEndTextCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.EndText();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("ET");
        }

        internal static PdfEndTextCommand Instance =>
            instance;
    }
}

