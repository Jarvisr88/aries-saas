namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfFillAndStrokePathUsingNonzeroWindingNumberRuleCommand : PdfCommand
    {
        internal const string Name = "B";
        private static PdfFillAndStrokePathUsingNonzeroWindingNumberRuleCommand instance = new PdfFillAndStrokePathUsingNonzeroWindingNumberRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.FillPaths(true);
            interpreter.StrokePaths();
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("B");
        }

        internal static PdfFillAndStrokePathUsingNonzeroWindingNumberRuleCommand Instance =>
            instance;
    }
}

