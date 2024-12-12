namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptComment : PdfPostScriptOperator
    {
        private readonly string text;

        public PdfPostScriptComment(string text)
        {
            this.text = text;
        }

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
        }

        public string Text =>
            this.text;
    }
}

