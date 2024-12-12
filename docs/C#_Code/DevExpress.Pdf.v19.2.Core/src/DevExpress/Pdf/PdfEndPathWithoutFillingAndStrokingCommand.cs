namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfEndPathWithoutFillingAndStrokingCommand : PdfCommand
    {
        internal const string Name = "n";
        private static PdfEndPathWithoutFillingAndStrokingCommand instance = new PdfEndPathWithoutFillingAndStrokingCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("n");
        }

        internal static PdfEndPathWithoutFillingAndStrokingCommand Instance =>
            instance;
    }
}

