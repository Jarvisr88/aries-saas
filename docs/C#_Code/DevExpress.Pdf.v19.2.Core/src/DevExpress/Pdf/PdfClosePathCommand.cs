namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfClosePathCommand : PdfCommand
    {
        internal const string Name = "h";
        private static PdfClosePathCommand instance = new PdfClosePathCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.ClosePath();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("h");
        }

        internal static PdfClosePathCommand Instance =>
            instance;
    }
}

