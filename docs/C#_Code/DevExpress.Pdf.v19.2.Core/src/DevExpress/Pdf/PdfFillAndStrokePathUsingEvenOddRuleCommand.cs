namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfFillAndStrokePathUsingEvenOddRuleCommand : PdfCommand
    {
        internal const string Name = "B*";
        private static PdfFillAndStrokePathUsingEvenOddRuleCommand instance = new PdfFillAndStrokePathUsingEvenOddRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.FillPaths(false);
            interpreter.StrokePaths();
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("B*");
        }

        internal static PdfFillAndStrokePathUsingEvenOddRuleCommand Instance =>
            instance;
    }
}

