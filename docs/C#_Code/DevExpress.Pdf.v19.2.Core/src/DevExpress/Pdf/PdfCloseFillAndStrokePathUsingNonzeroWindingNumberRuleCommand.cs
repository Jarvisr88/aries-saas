namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCloseFillAndStrokePathUsingNonzeroWindingNumberRuleCommand : PdfCommand
    {
        internal const string Name = "b";
        private static PdfCloseFillAndStrokePathUsingNonzeroWindingNumberRuleCommand instance = new PdfCloseFillAndStrokePathUsingNonzeroWindingNumberRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.ClosePath();
            interpreter.FillPaths(true);
            interpreter.StrokePaths();
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("b");
        }

        internal static PdfCloseFillAndStrokePathUsingNonzeroWindingNumberRuleCommand Instance =>
            instance;
    }
}

