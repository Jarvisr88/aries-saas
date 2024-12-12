namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBeginTextCommand : PdfCommand
    {
        internal const string Name = "BT";
        private static PdfBeginTextCommand instance = new PdfBeginTextCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.BeginText();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("BT");
        }

        internal static PdfBeginTextCommand Instance =>
            instance;
    }
}

