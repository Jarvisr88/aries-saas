namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptExchOperator : PdfPostScriptOperator
    {
        public const string Token = "exch";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            interpreter.Stack.Exchange();
        }
    }
}

