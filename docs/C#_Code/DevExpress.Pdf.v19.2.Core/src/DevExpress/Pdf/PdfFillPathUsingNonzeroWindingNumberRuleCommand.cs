namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfFillPathUsingNonzeroWindingNumberRuleCommand : PdfCommand
    {
        internal const string Name = "f";
        private static PdfFillPathUsingNonzeroWindingNumberRuleCommand instance = new PdfFillPathUsingNonzeroWindingNumberRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.FillPaths(true);
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("f");
        }

        internal static PdfFillPathUsingNonzeroWindingNumberRuleCommand Instance =>
            instance;
    }
}

