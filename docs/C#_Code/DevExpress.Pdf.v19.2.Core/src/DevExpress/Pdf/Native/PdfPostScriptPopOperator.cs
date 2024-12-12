namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptPopOperator : PdfPostScriptOperator
    {
        public const string Token = "pop";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            interpreter.Stack.Pop(true);
        }
    }
}

