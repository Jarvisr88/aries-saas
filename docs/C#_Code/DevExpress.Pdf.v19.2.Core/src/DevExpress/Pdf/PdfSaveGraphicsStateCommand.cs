namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSaveGraphicsStateCommand : PdfCommand
    {
        internal const string Name = "q";
        private static PdfSaveGraphicsStateCommand instance = new PdfSaveGraphicsStateCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SaveGraphicsState();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("q");
        }

        internal static PdfSaveGraphicsStateCommand Instance =>
            instance;
    }
}

