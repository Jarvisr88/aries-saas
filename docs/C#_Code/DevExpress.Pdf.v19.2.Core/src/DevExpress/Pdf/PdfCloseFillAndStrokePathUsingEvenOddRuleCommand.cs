namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCloseFillAndStrokePathUsingEvenOddRuleCommand : PdfCommand
    {
        internal const string Name = "b*";
        private static PdfCloseFillAndStrokePathUsingEvenOddRuleCommand instance = new PdfCloseFillAndStrokePathUsingEvenOddRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.ClosePath();
            interpreter.FillPaths(false);
            interpreter.StrokePaths();
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("b*");
        }

        internal static PdfCloseFillAndStrokePathUsingEvenOddRuleCommand Instance =>
            instance;
    }
}

