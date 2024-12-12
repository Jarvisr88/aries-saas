namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCloseAndStrokePathCommand : PdfCommand
    {
        internal const string Name = "s";
        private static PdfCloseAndStrokePathCommand instance = new PdfCloseAndStrokePathCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.ClosePath();
            interpreter.StrokePaths();
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("s");
        }

        internal static PdfCloseAndStrokePathCommand Instance =>
            instance;
    }
}

