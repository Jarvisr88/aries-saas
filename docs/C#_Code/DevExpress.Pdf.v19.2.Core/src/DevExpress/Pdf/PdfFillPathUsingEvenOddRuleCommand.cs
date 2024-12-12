namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfFillPathUsingEvenOddRuleCommand : PdfCommand
    {
        internal const string Name = "f*";
        private static PdfFillPathUsingEvenOddRuleCommand instance = new PdfFillPathUsingEvenOddRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.FillPaths(false);
            interpreter.ClipAndClearPaths();
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("f*");
        }

        internal static PdfFillPathUsingEvenOddRuleCommand Instance =>
            instance;
    }
}

