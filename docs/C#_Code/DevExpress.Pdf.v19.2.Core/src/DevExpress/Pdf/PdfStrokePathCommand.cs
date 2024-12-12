namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfStrokePathCommand : PdfCommand
    {
        internal const string Name = "S";
        private static PdfStrokePathCommand instance = new PdfStrokePathCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.StrokePaths();
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("S");
        }

        internal static PdfStrokePathCommand Instance =>
            instance;
    }
}

