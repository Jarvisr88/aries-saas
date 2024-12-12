namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfModifyClippingPathUsingNonzeroWindingNumberRuleCommand : PdfCommand
    {
        internal const string Name = "W";
        private static PdfModifyClippingPathUsingNonzeroWindingNumberRuleCommand instance = new PdfModifyClippingPathUsingNonzeroWindingNumberRuleCommand();

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.Clip(true);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteString("W");
        }

        internal static PdfModifyClippingPathUsingNonzeroWindingNumberRuleCommand Instance =>
            instance;
    }
}

