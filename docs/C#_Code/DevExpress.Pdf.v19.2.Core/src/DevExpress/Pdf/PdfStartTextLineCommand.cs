namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfStartTextLineCommand : PdfCommand
    {
        internal const string Name = "T*";
        private static PdfStartTextLineCommand instance = new PdfStartTextLineCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.MoveToNextLine();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("T*");
        }

        internal static PdfStartTextLineCommand Instance =>
            instance;
    }
}

