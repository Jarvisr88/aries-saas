namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfModifyClippingPathUsingEvenOddRuleCommand : PdfCommand
    {
        internal const string Name = "W*";
        private static PdfModifyClippingPathUsingEvenOddRuleCommand instance = new PdfModifyClippingPathUsingEvenOddRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.Clip(false);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("W*");
        }

        internal static PdfModifyClippingPathUsingEvenOddRuleCommand Instance =>
            instance;
    }
}

