namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptGetDictionaryElementOperator : PdfPostScriptOperator
    {
        private readonly string key;

        public PdfPostScriptGetDictionaryElementOperator(string key)
        {
            this.key = key;
        }

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
        }

        public string Key =>
            this.key;
    }
}

